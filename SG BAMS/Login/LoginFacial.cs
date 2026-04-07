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
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class LoginFacial : Form
    {
        /// <summary>
        /// Gets or sets the usuario a validar.
        /// </summary>
        /// <value>
        /// The usuario a validar.
        /// </value>
        public string UsuarioAValidar { get; set; }
        /// <summary>
        /// Gets or sets the rol asignado.
        /// </summary>
        /// <value>
        /// The rol asignado.
        /// </value>
        public int RolAsignado { get; set; }

        /// <summary>
        /// The camara
        /// </summary>
        private VideoCapture camara;
        /// <summary>
        /// The recognizer
        /// </summary>
        private LBPHFaceRecognizer recognizer = new LBPHFaceRecognizer(1, 8, 8, 8, 200);
        /// <summary>
        /// The face detector
        /// </summary>
        private CascadeClassifier faceDetector = new CascadeClassifier("haarcascade_frontalface_default.xml");

        /// <summary>
        /// The contador exito
        /// </summary>
        private int contadorExito = 0;
        /// <summary>
        /// The votos para validar
        /// </summary>
        private const int VOTOS_PARA_VALIDAR = 2;
        /// <summary>
        /// The umbral distancia
        /// </summary>
        private const double UMBRAL_DISTANCIA = 130;

        /// <summary>
        /// The etiqueta usuario valido
        /// </summary>
        private int etiquetaUsuarioValido = -1;

        /// <summary>
        /// The CTS
        /// </summary>
        private CancellationTokenSource cts;
        /// <summary>
        /// The procesando
        /// </summary>
        private volatile bool _procesando = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginFacial"/> class.
        /// </summary>
        public LoginFacial() { InitializeComponent(); }

        /// <summary>
        /// Handles the Load event of the LoginFacial control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void LoginFacial_Load(object sender, EventArgs e)
        {
            var todosLosArchivos = Directory.GetFiles(clsSoporte.DirectorioRostros, "*.jpg").ToList();

            if (todosLosArchivos.Count == 0)
            {
                MessageBox.Show("No hay registros faciales registrados en el sistema.", "Sin registros",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                RegresarAlLogin();
                return; 
            }

            var archivosUsuario = todosLosArchivos
                .Where(f => Path.GetFileNameWithoutExtension(f) == UsuarioAValidar ||
                            Path.GetFileNameWithoutExtension(f).StartsWith(UsuarioAValidar + "_"))
                .ToList();

            if (archivosUsuario.Count == 0)
            {
                MessageBox.Show($"El usuario '{UsuarioAValidar}' no tiene un registro facial.", "Sin registro facial",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                RegresarAlLogin();
                return; 
            }

            ActualizarEstado("Cargando modelo facial...", Color.Gray);
            Task.Run(() =>
            {
                EntrenarModelo(todosLosArchivos);
                this.Invoke(new Action(IniciarCamara));
            });
        }

        /// <summary>
        /// Regresars the al login.
        /// </summary>
        private void RegresarAlLogin()
        {
            cts?.Cancel();
            timerCamara?.Stop();
            timerCamara?.Dispose();
            if (camara != null) { camara.Stop(); camara.Dispose(); camara = null; }

            Form loginOriginal = Application.OpenForms["Login"];
            loginOriginal?.Show();

            this.BeginInvoke(new Action(() => this.Close()));
        }

        /// <summary>
        /// Entrenars the modelo.
        /// </summary>
        /// <param name="todosLosArchivos">The todos los archivos.</param>
        private void EntrenarModelo(List<string> todosLosArchivos)
        {
            var rostros = new List<Image<Gray, byte>>();
            var etiquetas = new List<int>();

            var usuarios = todosLosArchivos
                .GroupBy(f =>
                {
                    string nombre = Path.GetFileNameWithoutExtension(f);
                    int idx = nombre.IndexOf('_');
                    return idx >= 0 ? nombre.Substring(0, idx) : nombre;
                })
                .ToList();

            int etiquetaActual = 1;
            foreach (var grupo in usuarios)
            {
                string nombreUsuario = grupo.Key;
                int etiqueta = etiquetaActual++;

                if (nombreUsuario == UsuarioAValidar)
                    etiquetaUsuarioValido = etiqueta;

                foreach (var archivo in grupo)
                {
                    var img = new Image<Gray, byte>(archivo).Resize(100, 100, Inter.Linear);
                    AplicarPreprocesado(img);
                    AgregarConVariantes(img, rostros, etiquetas, etiqueta);
                }
            }

            using (var vR = new VectorOfMat())
            using (var vE = new VectorOfInt(etiquetas.ToArray()))
            {
                foreach (var img in rostros) vR.Push(img.Mat);
                recognizer.Train(vR, vE);
            }
            foreach (var img in rostros) img.Dispose();
        }

        /// <summary>
        /// Agregars the con variantes.
        /// </summary>
        /// <param name="base_">The base.</param>
        /// <param name="lista">The lista.</param>
        /// <param name="etiquetas">The etiquetas.</param>
        /// <param name="etiqueta">The etiqueta.</param>
        private void AgregarConVariantes(Image<Gray, byte> base_,
            List<Image<Gray, byte>> lista, List<int> etiquetas, int etiqueta)
        {
            void Add(Image<Gray, byte> img) { lista.Add(img); etiquetas.Add(etiqueta); }
            Add(base_);
            Add(base_.Flip(FlipType.Horizontal));
            var b1 = base_.Clone(); b1._Mul(1.6); Add(b1);
            var b2 = base_.Clone(); b2._Mul(1.3); Add(b2);
            var b3 = base_.Clone(); b3._Mul(0.7); Add(b3);
            var b4 = base_.Clone(); b4._Mul(0.4); Add(b4);
            Add(SimularLuzLateral(base_));
        }

        /// <summary>
        /// The timer camara
        /// </summary>
        private System.Windows.Forms.Timer timerCamara;

        /// <summary>
        /// Iniciars the camara.
        /// </summary>
        private void IniciarCamara()
        {
            camara = new VideoCapture(0);
            cts = new CancellationTokenSource();
            timerCamara = new System.Windows.Forms.Timer { Interval = 66 };
            timerCamara.Tick += TimerCamara_Tick;
            timerCamara.Start();
            ActualizarEstado("Coloca tu rostro frente a la cámara", Color.Gray);
        }

        /// <summary>
        /// Handles the Tick event of the TimerCamara control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Procesars the frame.
        /// </summary>
        /// <param name="m">The m.</param>
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
                            scaleFactor: 1.08,
                            minNeighbors: 2,
                            minSize: new Size(35, 35),
                            maxSize: new Size(600, 600));
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

                        bool coincide = resultado.Label == etiquetaUsuarioValido
                                        && resultado.Distance < UMBRAL_DISTANCIA;

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
                            string msg = resultado.Label != etiquetaUsuarioValido && resultado.Label != -1
                                ? "Rostro no autorizado"
                                : $"Ajusta posición... (dist: {resultado.Distance:F0})";
                            ActualizarEstado(msg, Color.Red);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ProcesarFrame: " + ex.Message);
            }
        }

        /// <summary>
        /// Preparars the gris para deteccion.
        /// </summary>
        /// <param name="frame">The frame.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Aplicars the preprocesado.
        /// </summary>
        /// <param name="imagen">The imagen.</param>
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

        /// <summary>
        /// Aplicars the gamma.
        /// </summary>
        /// <param name="imagen">The imagen.</param>
        /// <param name="gamma">The gamma.</param>
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

        /// <summary>
        /// Simulars the luz lateral.
        /// </summary>
        /// <param name="original">The original.</param>
        /// <returns></returns>
        private Image<Gray, byte> SimularLuzLateral(Image<Gray, byte> original)
        {
            var res = original.Clone();
            int mitad = res.Width / 2;
            for (int y = 0; y < res.Height; y++)
                for (int x = 0; x < mitad; x++)
                    res.Data[y, x, 0] = (byte)(res.Data[y, x, 0] * 0.5);
            return res;
        }

        /// <summary>
        /// Actualizars the estado.
        /// </summary>
        /// <param name="texto">The texto.</param>
        /// <param name="color">The color.</param>
        private void ActualizarEstado(string texto, Color color)
        {
            if (lblEstado.InvokeRequired)
                lblEstado.Invoke(new Action(() => { lblEstado.Text = texto; lblEstado.ForeColor = color; }));
            else
            { lblEstado.Text = texto; lblEstado.ForeColor = color; }
        }

        /// <summary>
        /// Finalizars the specified resultado.
        /// </summary>
        /// <param name="resultado">The resultado.</param>
        private void Finalizar(DialogResult resultado)
        {
            cts?.Cancel();
            timerCamara?.Stop();
            timerCamara?.Dispose();
            if (camara != null) { camara.Stop(); camara.Dispose(); camara = null; }

            if (resultado == DialogResult.OK)
            {
                Form loginOriginal = Application.OpenForms["Login"];
                loginOriginal?.Hide();

                switch (RolAsignado)
                {
                    case 1:
                        new SG_BAMS.MenuPrincipalAdm().Show();
                        break;
                    case 2:
                        new SG_BAMS.MenuPrincipalEmp().Show();
                        break;
                    default:
                        MessageBox.Show("Rol no reconocido.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        loginOriginal?.Show();
                        break;
                }
            }
            else
            {
                Form loginOriginal = Application.OpenForms["Login"];
                loginOriginal?.Show();
            }

            this.Close();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Form.FormClosing" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.FormClosingEventArgs" /> that contains the event data.</param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            cts?.Cancel();
            timerCamara?.Stop();
            timerCamara?.Dispose();
            if (camara != null) { camara.Dispose(); camara = null; }
            base.OnFormClosing(e);
        }

        /// <summary>
        /// Handles the Click event of the btnCancelar1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnCancelar1_Click(object sender, EventArgs e) => RegresarAlLogin();

        /// <summary>
        /// Handles the Click event of the btnReintentar1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnReintentar1_Click(object sender, EventArgs e)
        {
            contadorExito = 0;
            ActualizarEstado("Reintentando escaneo...", Color.Black);
        }
    }
}