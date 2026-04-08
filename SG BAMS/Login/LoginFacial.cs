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
        /// Obtiene o establece el usuario a validar.
        /// </summary>
        /// <value>
        /// El usuario a validar.
        /// </value>
        public string UsuarioAValidar { get; set; }
        /// <summary>
        /// Obtiene o establece el rol asignado.
        /// </summary>
        /// <value>
        /// El rol asignado.
        /// </value>
        public int RolAsignado { get; set; }

        /// <summary>
        /// La cámara
        /// </summary>
        private VideoCapture camara;
        /// <summary>
        /// El reconocedor facial
        /// </summary>
        private LBPHFaceRecognizer recognizer = new LBPHFaceRecognizer(1, 8, 8, 8, 200);
        /// <summary>
        /// El detector de rostros
        /// </summary>
        private CascadeClassifier faceDetector = new CascadeClassifier("haarcascade_frontalface_default.xml");

        /// <summary>
        /// El contador de éxitos
        /// </summary>
        private int contadorExito = 0;
        /// <summary>
        /// Los votos necesarios para validar
        /// </summary>
        private const int VOTOS_PARA_VALIDAR = 2;
        /// <summary>
        /// El umbral de distancia
        /// </summary>
        private const double UMBRAL_DISTANCIA = 130;

        /// <summary>
        /// La etiqueta del usuario válido
        /// </summary>
        private int etiquetaUsuarioValido = -1;

        /// <summary>
        /// El token de cancelación
        /// </summary>
        private CancellationTokenSource cts;
        /// <summary>
        /// Indicador de procesamiento
        /// </summary>
        private volatile bool _procesando = false;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="LoginFacial"/>.
        /// </summary>
        public LoginFacial()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

        }

        /// <summary>
        /// Maneja el evento Load del control LoginFacial.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
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
                EntrenarModelo(archivosUsuario);
                this.Invoke(new Action(IniciarCamara));
            });
        }

        /// <summary>
        /// Regresa al formulario de login.
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
        /// Entrena el modelo facial.
        /// </summary>
        /// <param name="todosLosArchivos">Todos los archivos de rostros.</param>
        private void EntrenarModelo(List<string> archivosDelUsuario)
        {
            var rostros = new List<Image<Gray, byte>>();
            var etiquetas = new List<int>();

            etiquetaUsuarioValido = 1;

            foreach (var archivo in archivosDelUsuario)
            {
                var img = new Image<Gray, byte>(archivo).Resize(100, 100, Inter.Linear);
                AplicarPreprocesado(img);
                AgregarConVariantes(img, rostros, etiquetas, etiquetaUsuarioValido);
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
        /// Agrega variantes de la imagen para mejorar el entrenamiento.
        /// </summary>
        /// <param name="base_">La imagen base.</param>
        /// <param name="lista">La lista de rostros.</param>
        /// <param name="etiquetas">La lista de etiquetas.</param>
        /// <param name="etiqueta">La etiqueta actual.</param>
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
        /// El temporizador de la cámara
        /// </summary>
        private System.Windows.Forms.Timer timerCamara;

        /// <summary>
        /// Inicia la cámara.
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
        /// Maneja el evento Tick del control TimerCamara.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
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
        /// Procesa el frame de video.
        /// </summary>
        /// <param name="m">La matriz del frame.</param>
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
        /// Prepara la imagen en escala de grises para la detección.
        /// </summary>
        /// <param name="frame">El frame original.</param>
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
        /// Aplica preprocesado a la imagen.
        /// </summary>
        /// <param name="imagen">La imagen a procesar.</param>
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
        /// Aplica corrección gamma a la imagen.
        /// </summary>
        /// <param name="imagen">La imagen.</param>
        /// <param name="gamma">El valor gamma.</param>
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
        /// Simula iluminación lateral.
        /// </summary>
        /// <param name="original">La imagen original.</param>
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
        /// Actualiza el estado del proceso.
        /// </summary>
        /// <param name="texto">El texto a mostrar.</param>
        /// <param name="color">El color del texto.</param>
        private void ActualizarEstado(string texto, Color color)
        {
            if (lblEstado.InvokeRequired)
                lblEstado.Invoke(new Action(() => { lblEstado.Text = texto; lblEstado.ForeColor = color; }));
            else
            { lblEstado.Text = texto; lblEstado.ForeColor = color; }
        }

        /// <summary>
        /// Finaliza el proceso de login facial.
        /// </summary>
        /// <param name="resultado">El resultado del proceso.</param>
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
        /// Maneja el evento FormClosing del formulario.
        /// </summary>
        /// <param name="e">Una instancia <see cref="T:System.Windows.Forms.FormClosingEventArgs" /> que contiene los datos del evento.</param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            cts?.Cancel();
            timerCamara?.Stop();
            timerCamara?.Dispose();
            if (camara != null) { camara.Dispose(); camara = null; }
            base.OnFormClosing(e);
        }

        /// <summary>
        /// Maneja el evento Click del control btnCancelar1.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnCancelar1_Click(object sender, EventArgs e) => RegresarAlLogin();

        /// <summary>
        /// Maneja el evento Click del control btnReintentar1.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnReintentar1_Click(object sender, EventArgs e)
        {
            contadorExito = 0;
            ActualizarEstado("Reintentando escaneo...", Color.Black);
        }
    }
}