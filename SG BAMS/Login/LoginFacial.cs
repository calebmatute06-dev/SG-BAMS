using Emgu.CV;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using SG_BAMS.Administracion_de_BAMS.Usuarios;
using Emgu.CV.CvEnum;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS.Login
{
    public partial class LoginFacial : Form
    {
        public string UsuarioAValidar { get; set; }
        public int RolAsignado { get; set; }

        private VideoCapture camara;
        private LBPHFaceRecognizer recognizer = new LBPHFaceRecognizer(1, 8, 8, 8, 100);
        private CascadeClassifier faceDetector = new CascadeClassifier("haarcascade_frontalface_default.xml");

        private int contadorExito = 0;
        private const int VOTOS_PARA_VALIDAR = 4;
        private const double UMBRAL_DISTANCIA = 85;

        private CancellationTokenSource cts;
        private volatile bool _procesando = false;

        public LoginFacial() { InitializeComponent(); }

        private void LoginFacial_Load(object sender, EventArgs e)
        {
            var archivos = Directory.GetFiles(clsSoporte.DirectorioRostros, "*.jpg")
                .Where(f => Path.GetFileNameWithoutExtension(f) == UsuarioAValidar ||
                            Path.GetFileNameWithoutExtension(f).StartsWith(UsuarioAValidar + "_"))
                .ToList();

            if (archivos.Count == 0)
            {
                MessageBox.Show("No tienes un registro facial.", "Error");
                Finalizar(DialogResult.Abort);
                return;
            }

            ActualizarEstado("Cargando modelo facial...", Color.Gray);
            Task.Run(() =>
            {
                EntrenarModelo(archivos);
                this.Invoke(new Action(IniciarCamara));
            });
        }

        private void EntrenarModelo(List<string> archivos)
        {
            var rostros = new List<Image<Gray, byte>>();
            var etiquetas = new List<int>();

            foreach (var archivo in archivos)
            {
                var img = new Image<Gray, byte>(archivo).Resize(100, 100, Inter.Linear);
                AplicarPreprocesado(img);
                AgregarConVariantes(img, rostros, etiquetas);
            }

            using (var vR = new VectorOfMat())
            using (var vE = new VectorOfInt(etiquetas.ToArray()))
            {
                foreach (var img in rostros) vR.Push(img.Mat);
                recognizer.Train(vR, vE);
            }
            foreach (var img in rostros) img.Dispose();
        }

        private void AgregarConVariantes(Image<Gray, byte> base_,
            List<Image<Gray, byte>> lista, List<int> etiquetas)
        {
            void Add(Image<Gray, byte> img) { lista.Add(img); etiquetas.Add(1); }
            Add(base_);
            Add(base_.Flip(FlipType.Horizontal));
            var b1 = base_.Clone(); b1._Mul(1.6); Add(b1);
            var b2 = base_.Clone(); b2._Mul(1.3); Add(b2);
            var b3 = base_.Clone(); b3._Mul(0.7); Add(b3);
            var b4 = base_.Clone(); b4._Mul(0.4); Add(b4);
            Add(SimularLuzLateral(base_));
        }


        private System.Windows.Forms.Timer timerCamara;

        private void IniciarCamara()
        {
            camara = new VideoCapture(0);
            cts = new CancellationTokenSource();
            timerCamara = new System.Windows.Forms.Timer { Interval = 66 };
            timerCamara.Tick += TimerCamara_Tick;
            timerCamara.Start();
            ActualizarEstado("Coloca tu rostro frente a la cámara", Color.Gray);
        }

        private void TimerCamara_Tick(object sender, EventArgs e)
        {
            if (camara == null || _procesando) return;

            Mat m = new Mat();
            camara.Read(m);
            if (m.IsEmpty) { m.Dispose(); return; }

            using (var frameUI = m.ToImage<Bgr, byte>())
            {
                if (picValidar.Image != null) picValidar.Image.Dispose();
                picValidar.Image = frameUI.ToBitmap();
            }

            _procesando = true;
            var token = cts.Token;
            Task.Run(() =>
            {
                try { if (!token.IsCancellationRequested) ProcesarFrame(m); }
                finally { m.Dispose(); _procesando = false; }
            }, token);
        }


        private void ProcesarFrame(Mat m)
        {
            try
            {
                using (var frame = m.ToImage<Bgr, byte>())
                {
                    Image<Gray, byte> grisNormalizado = PrepararGrisParaDeteccion(frame);

                    Rectangle[] rostrosDetectados;
                    using (grisNormalizado)
                    {
                        rostrosDetectados = faceDetector.DetectMultiScale(
                            grisNormalizado,
                            scaleFactor: 1.05,  
                            minNeighbors: 3,      
                            minSize: new Size(50, 50),
                            maxSize: new Size(500, 500));
                    }

                    if (rostrosDetectados.Length == 0)
                    {
                        contadorExito = Math.Max(0, contadorExito - 1);
                        ActualizarEstado("Coloca tu rostro frente a la cámara", Color.Gray);
                        return;
                    }

                    var mejor = rostrosDetectados.OrderByDescending(r => r.Width * r.Height).First();
                    int mg = (int)(mejor.Width * 0.10);
                    int rx = Math.Max(0, mejor.X - mg);
                    int ry = Math.Max(0, mejor.Y - mg);
                    int rw = Math.Min(frame.Width - rx, mejor.Width + mg * 2);
                    int rh = Math.Min(frame.Height - ry, mejor.Height + mg * 2);

                    using (var rostroRecortado = frame.Convert<Gray, byte>()
                                                      .GetSubRect(new Rectangle(rx, ry, rw, rh))
                                                      .Clone())
                    using (var rostroProcesado = rostroRecortado.Resize(100, 100, Inter.Linear))
                    {
                        AplicarPreprocesado(rostroProcesado);
                        var resultado = recognizer.Predict(rostroProcesado);
                        bool coincide = resultado.Label != -1 && resultado.Distance < UMBRAL_DISTANCIA;

                        if (coincide)
                        {
                            contadorExito++;
                            ActualizarEstado($"Verificando... ({contadorExito}/{VOTOS_PARA_VALIDAR})", Color.DodgerBlue);
                            if (contadorExito >= VOTOS_PARA_VALIDAR)
                                this.Invoke(new Action(() => Finalizar(DialogResult.OK)));
                        }
                        else
                        {
                            contadorExito = Math.Max(0, contadorExito - 1);
                            ActualizarEstado($"Ajusta posición... (dist: {resultado.Distance:F0})", Color.Orange);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ProcesarFrame: " + ex.Message);
            }
        }

        private Image<Gray, byte> PrepararGrisParaDeteccion(Image<Bgr, byte> frame)
        {
            var gris = frame.Convert<Gray, byte>();

            using (Mat m = gris.Mat)
            {
                double media = CvInvoke.Mean(m).V0;
                if (media < 80) AplicarGamma(m, 0.45);  
                else if (media < 110) AplicarGamma(m, 0.7);   
                else if (media > 180) AplicarGamma(m, 2.2);   
                else if (media > 150) AplicarGamma(m, 1.6);   

                CvInvoke.CLAHE(m, 4.0, new Size(4, 4), m);
                CvInvoke.EqualizeHist(m, m);
                CvInvoke.GaussianBlur(m, m, new Size(3, 3), 0);
            }

            return gris;
        }

        private void AplicarPreprocesado(Image<Gray, byte> imagen)
        {
            using (Mat m = imagen.Mat)
            {
                double media = CvInvoke.Mean(m).V0;
                if (media < 85) AplicarGamma(m, 0.5);
                else if (media > 170) AplicarGamma(m, 2.0);

                CvInvoke.CLAHE(m, 3.0, new Size(8, 8), m);

                using (Mat temp = new Mat())
                {
                    CvInvoke.BilateralFilter(m, temp, 9, 75, 75);
                    temp.CopyTo(m);
                }

                CvInvoke.EqualizeHist(m, m);
            }
        }

        private void AplicarGamma(Mat imagen, double gamma)
        {
            byte[] lut = new byte[256];
            for (int i = 0; i < 256; i++)
                lut[i] = (byte)Math.Min(255, Math.Pow(i / 255.0, 1.0 / gamma) * 255.0);

            using (Mat lutM = new Mat(1, 256, DepthType.Cv8U, 1))
            {
                System.Runtime.InteropServices.Marshal.Copy(lut, 0, lutM.DataPointer, 256);
                CvInvoke.LUT(imagen, lutM, imagen);
            }
        }

        private Image<Gray, byte> SimularLuzLateral(Image<Gray, byte> original)
        {
            var res = original.Clone();
            int mitad = res.Width / 2;
            for (int y = 0; y < res.Height; y++)
                for (int x = 0; x < mitad; x++)
                    res.Data[y, x, 0] = (byte)(res.Data[y, x, 0] * 0.5);
            return res;
        }

        private void ActualizarEstado(string texto, Color color)
        {
            if (lblEstado.InvokeRequired)
                lblEstado.Invoke(new Action(() => { lblEstado.Text = texto; lblEstado.ForeColor = color; }));
            else
            { lblEstado.Text = texto; lblEstado.ForeColor = color; }
        }

        private void Finalizar(DialogResult resultado)
        {
            cts?.Cancel();
            timerCamara?.Stop();
            timerCamara?.Dispose();
            if (camara != null) { camara.Stop(); camara.Dispose(); camara = null; }

            if (resultado == DialogResult.OK)
            {
                switch (RolAsignado)
                {
                    case 1: new MenuPrincipalAdm().Show(); break;
                    case 2: new MenuPrincipalEmp().Show(); break;
                }
            }
            else
            {
                Form loginOriginal = Application.OpenForms["Login"];
                if (loginOriginal != null) loginOriginal.Show();
            }
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            cts?.Cancel();
            timerCamara?.Stop();
            timerCamara?.Dispose();
            if (camara != null) { camara.Dispose(); camara = null; }
            base.OnFormClosing(e);
        }

        private void btnCancelar1_Click(object sender, EventArgs e) => Finalizar(DialogResult.Cancel);

        private void btnReintentar1_Click(object sender, EventArgs e)
        {
            contadorExito = 0;
            ActualizarEstado("Reintentando escaneo...", Color.Black);
        }
    }
}