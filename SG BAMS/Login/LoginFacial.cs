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
        private LBPHFaceRecognizer recognizer = new LBPHFaceRecognizer(1, 8, 8, 8, 100);
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
        private const int VOTOS_PARA_VALIDAR = 8;
        /// <summary>
        /// The umbral distancia base
        /// </summary>
        private const double UMBRAL_DISTANCIA_BASE = 95;
        /// <summary>
        /// The umbral poca luz
        /// </summary>
        private const double UMBRAL_POCA_LUZ = 110;
        /// <summary>
        /// The umbral luz intensa
        /// </summary>
        private const double UMBRAL_LUZ_INTENSA = 100;

        /// <summary>
        /// The etiqueta a nombre
        /// </summary>
        private Dictionary<int, string> etiquetaANombre = new Dictionary<int, string>();
        /// <summary>
        /// The etiqueta usuario valido
        /// </summary>
        private int etiquetaUsuarioValido = -1;
        /// <summary>
        /// The total usuarios en modelo
        /// </summary>
        private int totalUsuariosEnModelo = 0;

        /// <summary>
        /// The CTS
        /// </summary>
        private CancellationTokenSource cts;
        /// <summary>
        /// The procesando
        /// </summary>
        private volatile bool _procesando = false;
        /// <summary>
        /// The timer camara
        /// </summary>
        private System.Windows.Forms.Timer timerCamara;
        /// <summary>
        /// The historial distancias
        /// </summary>
        private List<double> historialDistancias = new List<double>();
        /// <summary>
        /// The historial etiquetas
        /// </summary>
        private List<int> historialEtiquetas = new List<int>();
        /// <summary>
        /// The historial size
        /// </summary>
        private const int HISTORIAL_SIZE = 8;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginFacial"/> class.
        /// </summary>
        public LoginFacial()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        /// <summary>
        /// Handles the Load event of the LoginFacial control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void LoginFacial_Load(object sender, EventArgs e)
        {
            if (!File.Exists("haarcascade_frontalface_default.xml"))
            {
                MessageBox.Show("No se encuentra el archivo haarcascade_frontalface_default.xml", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                RegresarAlLogin();
                return;
            }

            var todosLosArchivos = Directory.GetFiles(DetectorRostroService.DirectorioRostros, "*.jpg").ToList();
            if (todosLosArchivos.Count == 0)
            {
                MessageBox.Show("No hay registros faciales registrados en el sistema.", "Sin registros",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                RegresarAlLogin();
                return;
            }

            var usuariosUnicos = todosLosArchivos
                .Select(f =>
                {
                    string nombre = Path.GetFileNameWithoutExtension(f);
                    int idx = nombre.IndexOf('_');
                    return idx >= 0 ? nombre.Substring(0, idx) : nombre;
                })
                .Distinct()
                .Count();

            if (usuariosUnicos <= 1)
            {
                MessageBox.Show("ERROR DE SEGURIDAD: Solo hay un usuario registrado.\n\n" +
                    "El reconocimiento facial NO funcionará correctamente.\n\n" +
                    "Registra al menos 2 usuarios diferentes con 10-15 fotos cada uno.",
                    "Configuración Incorrecta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                RegresarAlLogin();
                return;
            }

            ActualizarEstado("Cargando modelo facial...", Color.Gray);
            Task.Run(() =>
            {
                EntrenarModeloConTodosLosUsuarios(todosLosArchivos);
                this.Invoke(new Action(() =>
                {
                    if (totalUsuariosEnModelo <= 1)
                    {
                        MessageBox.Show("ERROR: El modelo no se entrenó correctamente.",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        RegresarAlLogin();
                        return;
                    }
                    IniciarCamara();
                }));
            });
        }

        /// <summary>
        /// Regresars the al login.
        /// </summary>
        private void RegresarAlLogin()
        {
            cts?.Cancel();
            if (timerCamara != null)
            {
                timerCamara.Stop();
                timerCamara.Dispose();
            }
            if (camara != null) { camara.Stop(); camara.Dispose(); camara = null; }

            Form loginOriginal = Application.OpenForms["Login"];
            loginOriginal?.Show();
            this.BeginInvoke(new Action(() => this.Close()));
        }

        /// <summary>
        /// Entrenars the modelo con todos los usuarios.
        /// </summary>
        /// <param name="todosLosArchivos">The todos los archivos.</param>
        /// <exception cref="System.Exception">No se encontró la etiqueta para el usuario a validar.</exception>
        private void EntrenarModeloConTodosLosUsuarios(List<string> todosLosArchivos)
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

            totalUsuariosEnModelo = usuarios.Count();
            int etiquetaActual = 1;

            foreach (var grupo in usuarios)
            {
                string nombreUsuario = grupo.Key;
                int etiqueta = etiquetaActual++;
                etiquetaANombre[etiqueta] = nombreUsuario;

                if (nombreUsuario == UsuarioAValidar)
                    etiquetaUsuarioValido = etiqueta;

                foreach (var archivo in grupo)
                {
                    var img = new Image<Gray, byte>(archivo).Resize(100, 100, Inter.Linear);
                    AplicarPreprocesamiento(img);
                    AgregarConVariantes(img, rostros, etiquetas, etiqueta);
                }
            }

            if (etiquetaUsuarioValido == -1)
                throw new Exception("No se encontró la etiqueta para el usuario a validar.");

            using (var vR = new VectorOfMat())
            using (var vE = new VectorOfInt(etiquetas.ToArray()))
            {
                foreach (var img in rostros) vR.Push(img.Mat);
                recognizer.Train(vR, vE);
            }

            foreach (var img in rostros) img.Dispose();

            System.Diagnostics.Debug.WriteLine($"[INFO] Entrenamiento completado. Usuarios: {totalUsuariosEnModelo}, Total imágenes: {rostros.Count}");
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


            var brilloAlto = base_.Clone(); brilloAlto._Mul(1.4); Add(brilloAlto);
            var brilloBajo = base_.Clone(); brilloBajo._Mul(0.7); Add(brilloBajo);
        }

        /// <summary>
        /// Iniciars the camara.
        /// </summary>
        private void IniciarCamara()
        {
            camara = new VideoCapture(0);
            cts = new CancellationTokenSource();
            timerCamara = new System.Windows.Forms.Timer { Interval = 80 };
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
        /// Calculars the distancia promedio.
        /// </summary>
        /// <returns></returns>
        private double CalcularDistanciaPromedio()
        {
            if (historialDistancias.Count == 0) return 999;
            return historialDistancias.Average();
        }

        /// <summary>
        /// Obteners the etiqueta mas frecuente.
        /// </summary>
        /// <returns></returns>
        private int ObtenerEtiquetaMasFrecuente()
        {
            if (historialEtiquetas.Count == 0) return -1;
            return historialEtiquetas
                .GroupBy(x => x)
                .OrderByDescending(g => g.Count())
                .First()
                .Key;
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
                    Image<Gray, byte> gris = frame.Convert<Gray, byte>();

                    CvInvoke.EqualizeHist(gris, gris);
                    CvInvoke.GaussianBlur(gris, gris, new Size(3, 3), 0);

                    Rectangle[] rostrosDetectados = faceDetector.DetectMultiScale(
                        gris, scaleFactor: 1.08, minNeighbors: 6,
                        minSize: new Size(80, 80), maxSize: new Size(500, 500));

                    if (rostrosDetectados.Length == 0)
                    {
                        contadorExito = Math.Max(0, contadorExito - 1);
                        ActualizarEstado("Coloca tu rostro frente a la cámara", Color.Gray);
                        return;
                    }

                    var mejor = rostrosDetectados.OrderByDescending(r => r.Width * r.Height).First();

                    if (mejor.Width < 90 || mejor.Height < 90)
                    {
                        contadorExito = Math.Max(0, contadorExito - 1);
                        ActualizarEstado("Acércate más a la cámara", Color.Orange);
                        return;
                    }

                    int rx = Math.Max(0, mejor.X - 10);
                    int ry = Math.Max(0, mejor.Y - 10);
                    int rw = Math.Min(frame.Width - rx, mejor.Width + 20);
                    int rh = Math.Min(frame.Height - ry, mejor.Height + 20);

                    using (var rostroRecortado = gris.GetSubRect(new Rectangle(rx, ry, rw, rh)).Clone())
                    using (var rostroProcesado = rostroRecortado.Resize(100, 100, Inter.Linear))
                    {
                        CvInvoke.EqualizeHist(rostroProcesado, rostroProcesado);
                        CvInvoke.CLAHE(rostroProcesado, 2.0, new Size(8, 8), rostroProcesado);

                        var resultado = recognizer.Predict(rostroProcesado);
                        double distancia = resultado.Distance;

                        historialDistancias.Add(distancia);
                        historialEtiquetas.Add(resultado.Label);
                        if (historialDistancias.Count > HISTORIAL_SIZE)
                            historialDistancias.RemoveAt(0);
                        if (historialEtiquetas.Count > HISTORIAL_SIZE)
                            historialEtiquetas.RemoveAt(0);

                        double distanciaPromedio = CalcularDistanciaPromedio();
                        int etiquetaFrecuente = ObtenerEtiquetaMasFrecuente();


                        MCvScalar media = CvInvoke.Mean(rostroProcesado.Mat);
                        double brillo = media.V0;
                        double umbralActual = UMBRAL_DISTANCIA_BASE;
                        if (brillo < 70) umbralActual = UMBRAL_POCA_LUZ;
                        else if (brillo > 180) umbralActual = UMBRAL_LUZ_INTENSA;

                        bool coincide = etiquetaFrecuente == etiquetaUsuarioValido &&
                                        distanciaPromedio < umbralActual &&
                                        historialEtiquetas.Count >= 4;

                        System.Diagnostics.Debug.WriteLine($"[DEBUG] Usuario: {UsuarioAValidar}, Label: {resultado.Label}, Frec: {etiquetaFrecuente}, Dist: {distancia:F1}, Prom: {distanciaPromedio:F1}, Umbral: {umbralActual}, Brillo: {brillo:F0}");

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
                            string msg;
                            if (etiquetaFrecuente != -1 && etiquetaFrecuente != etiquetaUsuarioValido)
                            {
                                string nombreDetectado = etiquetaANombre.ContainsKey(etiquetaFrecuente)
                                    ? etiquetaANombre[etiquetaFrecuente]
                                    : "desconocido";
                                msg = $"Rostro detectado: {nombreDetectado}";
                            }
                            else if (distanciaPromedio >= umbralActual)
                            {
                                msg = $"Confianza baja - Acércate más";
                            }
                            else
                            {
                                msg = "Ajusta tu posición";
                            }
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
        /// Aplicars the preprocesamiento.
        /// </summary>
        /// <param name="imagen">The imagen.</param>
        private void AplicarPreprocesamiento(Image<Gray, byte> imagen)
        {
            using (Mat m = imagen.Mat)
            {
                CvInvoke.EqualizeHist(m, m);
                CvInvoke.CLAHE(m, 2.0, new Size(8, 8), m);
                CvInvoke.GaussianBlur(m, m, new Size(3, 3), 0.5);
            }
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
                lblEstado.Text = texto; lblEstado.ForeColor = color;
        }

        /// <summary>
        /// Finalizars the specified resultado.
        /// </summary>
        /// <param name="resultado">The resultado.</param>
        private void Finalizar(DialogResult resultado)
        {
            cts?.Cancel();
            if (timerCamara != null)
            {
                timerCamara.Stop();
                timerCamara.Dispose();
            }
            if (camara != null) { camara.Stop(); camara.Dispose(); camara = null; }

            if (resultado == DialogResult.OK)
            {
                Form loginOriginal = Application.OpenForms["Login"];
                loginOriginal?.Hide();
                switch (RolAsignado)
                {
                    case 1: new MenuPrincipalAdm().Show(); break;
                    case 2: new MenuPrincipalEmp().Show(); break;
                    default:
                        MessageBox.Show("Rol no reconocido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (timerCamara != null)
            {
                timerCamara.Stop();
                timerCamara.Dispose();
            }
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
            historialDistancias.Clear();
            historialEtiquetas.Clear();
            ActualizarEstado("Reintentando escaneo...", Color.Black);
        }
    }
}