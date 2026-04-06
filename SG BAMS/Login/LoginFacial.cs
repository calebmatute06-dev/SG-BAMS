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
        private int contadorFallo = 0;
        private const int VOTOS_PARA_VALIDAR = 4;
        private const double UMBRAL_DISTANCIA = 85;

        public LoginFacial()
        {
            InitializeComponent();
        }

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

            List<Image<Gray, byte>> rostrosEntrenamiento = new List<Image<Gray, byte>>();
            List<int> etiquetas = new List<int>();

            foreach (var archivo in archivos)
            {
                var imgOriginal = new Image<Gray, byte>(archivo).Resize(100, 100, Inter.Linear);
                AplicarPreprocesado(imgOriginal);
                rostrosEntrenamiento.Add(imgOriginal);
                etiquetas.Add(1);

                var imgFlip = imgOriginal.Flip(FlipType.Horizontal);
                rostrosEntrenamiento.Add(imgFlip);
                etiquetas.Add(1);

                var imgMuyBrillante = imgOriginal.Clone();
                imgMuyBrillante._Mul(1.6);
                rostrosEntrenamiento.Add(imgMuyBrillante);
                etiquetas.Add(1);

                var imgBrillo = imgOriginal.Clone();
                imgBrillo._Mul(1.3);
                rostrosEntrenamiento.Add(imgBrillo);
                etiquetas.Add(1);

                var imgOscura = imgOriginal.Clone();
                imgOscura._Mul(0.7);
                rostrosEntrenamiento.Add(imgOscura);
                etiquetas.Add(1);

                var imgMuyOscura = imgOriginal.Clone();
                imgMuyOscura._Mul(0.4);
                rostrosEntrenamiento.Add(imgMuyOscura);
                etiquetas.Add(1);

                var imgLuzLateral = SimularLuzLateral(imgOriginal);
                rostrosEntrenamiento.Add(imgLuzLateral);
                etiquetas.Add(1);
            }

            using (VectorOfMat vRostros = new VectorOfMat())
            using (VectorOfInt vEtiquetas = new VectorOfInt(etiquetas.ToArray()))
            {
                foreach (var img in rostrosEntrenamiento)
                    vRostros.Push(img.Mat);

                recognizer.Train(vRostros, vEtiquetas);
            }

            foreach (var img in rostrosEntrenamiento) img.Dispose();

            camara = new VideoCapture(0);
            Application.Idle += ProcesoValidacion;
        }

        private Image<Gray, byte> SimularLuzLateral(Image<Gray, byte> original)
        {
            var resultado = original.Clone();
            int mitad = resultado.Width / 2;
            for (int y = 0; y < resultado.Height; y++)
            {
                for (int x = 0; x < mitad; x++)
                {
                    byte val = resultado.Data[y, x, 0];
                    resultado.Data[y, x, 0] = (byte)Math.Min(255, val * 0.5);
                }
            }
            return resultado;
        }

        private void AplicarPreprocesado(Image<Gray, byte> imagen)
        {
            using (Mat m = imagen.Mat)
            {
                double media = CvInvoke.Mean(m).V0;
                double gamma = 1.0;
                if (media < 85)
                    gamma = 0.5;          
                else if (media > 170)
                    gamma = 2.0;         

                if (gamma != 1.0)
                    AplicarGamma(m, gamma);

                CvInvoke.CLAHE(m, 3.0, new Size(8, 8), m);

                Mat temp = new Mat();
                CvInvoke.BilateralFilter(m, temp, 9, 75, 75);
                temp.CopyTo(m);
                temp.Dispose();

                CvInvoke.EqualizeHist(m, m);
            }
        }

        private void AplicarGamma(Mat imagen, double gamma)
        {
            byte[] lut = new byte[256];
            for (int i = 0; i < 256; i++)
                lut[i] = (byte)Math.Min(255, Math.Pow(i / 255.0, 1.0 / gamma) * 255.0);

            using (Mat lutMat = new Mat(1, 256, DepthType.Cv8U, 1))
            {
                lutMat.SetTo(lut.Select(b => (object)b).ToArray());
                Mat lutM = new Mat(1, 256, DepthType.Cv8U, 1);
                System.Runtime.InteropServices.Marshal.Copy(
                    lut.Select(b => (byte)b).ToArray(), 0,
                    lutM.DataPointer, 256);
                CvInvoke.LUT(imagen, lutM, imagen);
                lutM.Dispose();
            }
        }

        private void ProcesoValidacion(object sender, EventArgs e)
        {
            if (camara == null) return;
            try
            {
                using (Mat m = new Mat())
                {
                    camara.Read(m);
                    if (m.IsEmpty) return;

                    using (var frame = m.ToImage<Bgr, byte>())
                    {
                        var rostroActual = DetectarRostroMejorado(frame);

                        if (rostroActual != null)
                        {
                            using (var rostroProcesado = rostroActual.Resize(100, 100, Inter.Linear))
                            {
                                AplicarPreprocesado(rostroProcesado);
                                var resultado = recognizer.Predict(rostroProcesado);
                                bool coincide = resultado.Label != -1 && resultado.Distance < UMBRAL_DISTANCIA;

                                if (coincide)
                                {
                                    contadorExito++;
                                    contadorFallo = 0;
                                    ActualizarEstado($"Verificando... ({contadorExito}/{VOTOS_PARA_VALIDAR})", Color.DodgerBlue);
                                }
                                else
                                {
                                    contadorFallo++;
                                    contadorExito = Math.Max(0, contadorExito - 1);
                                    ActualizarEstado($"Buscando rostro... (dist: {resultado.Distance:F0})", Color.Orange);
                                }

                                if (contadorExito >= VOTOS_PARA_VALIDAR)
                                {
                                    Finalizar(DialogResult.OK);
                                    return;
                                }
                            }
                            rostroActual.Dispose();
                        }
                        else
                        {
                            contadorExito = Math.Max(0, contadorExito - 1);
                            ActualizarEstado("Coloca tu rostro frente a la cámara", Color.Gray);
                        }

                        if (picValidar.InvokeRequired)
                            picValidar.Invoke(new Action(() => RefrescarImagen(frame)));
                        else
                            RefrescarImagen(frame);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
            }
        }

        private void RefrescarImagen(Image<Bgr, byte> frame)
        {
            if (picValidar.Image != null) picValidar.Image.Dispose();
            picValidar.Image = frame.ToBitmap();
        }

        private void ActualizarEstado(string texto, Color color)
        {
            if (lblEstado.InvokeRequired)
                lblEstado.Invoke(new Action(() => { lblEstado.Text = texto; lblEstado.ForeColor = color; }));
            else
            { lblEstado.Text = texto; lblEstado.ForeColor = color; }
        }

        private Image<Gray, byte> DetectarRostroMejorado(Image<Bgr, byte> frame)
        {
            using (var gris = frame.Convert<Gray, byte>())
            {
                CvInvoke.EqualizeHist(gris.Mat, gris.Mat);

                var rostros = faceDetector.DetectMultiScale(
                    gris, scaleFactor: 1.1, minNeighbors: 4,
                    minSize: new Size(60, 60), maxSize: new Size(400, 400));

                if (rostros.Length == 0) return null;

                var mejorRostro = rostros.OrderByDescending(r => r.Width * r.Height).First();

                int margen = (int)(mejorRostro.Width * 0.10);
                int x = Math.Max(0, mejorRostro.X - margen);
                int y = Math.Max(0, mejorRostro.Y - margen);
                int w = Math.Min(frame.Width - x, mejorRostro.Width + margen * 2);
                int h = Math.Min(frame.Height - y, mejorRostro.Height + margen * 2);

                return frame.Convert<Gray, byte>().GetSubRect(new Rectangle(x, y, w, h)).Clone();
            }
        }

        private void Finalizar(DialogResult resultado)
        {
            Application.Idle -= ProcesoValidacion;
            if (camara != null) { camara.Stop(); camara.Dispose(); camara = null; }

            if (resultado == DialogResult.OK)
            {
                switch (RolAsignado)
                {
                    case 1: new MenuPrincipalAdm().Show(); break;
                    case 2: new MenuPrincipalEmp().Show(); break;
                }
                this.Close();
            }
            else
            {
                Form loginOriginal = Application.OpenForms["Login"];
                if (loginOriginal != null) loginOriginal.Show();
                this.Close();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.Idle -= ProcesoValidacion;
            if (camara != null) { camara.Dispose(); camara = null; }
            base.OnFormClosing(e);
        }

        private void btnCancelar1_Click(object sender, EventArgs e) => Finalizar(DialogResult.Cancel);

        private void btnReintentar1_Click(object sender, EventArgs e)
        {
            contadorExito = 0;
            contadorFallo = 0;
            ActualizarEstado("Reintentando escaneo...", Color.Black);
        }
    }
}