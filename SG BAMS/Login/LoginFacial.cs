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
using SG_BAMS.LogicaNegocio.AdministracionBAMS;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Formulario de validación de identidad mediante reconocimiento facial.
    /// Utiliza EmguCV y el algoritmo LBPH para comparar el rostro capturado
    /// por la cámara contra las imágenes de entrenamiento del usuario.
    /// </summary>
    public partial class LoginFacial : Form
    {
        /// <summary>
        /// Nombre del usuario que se debe validar facialmente.
        /// Debe coincidir con el nombre base de los archivos de imagen en el directorio de rostros.
        /// </summary>
        public string UsuarioAValidar { get; set; }

        /// <summary>
        /// Rol asignado al usuario. 1 = Administrador, 2 = Empleado.
        /// </summary>
        public int RolAsignado { get; set; }

        private VideoCapture camara;
        private LBPHFaceRecognizer recognizer = new LBPHFaceRecognizer(1, 8, 8, 8, 100);
        private CascadeClassifier faceDetector = new CascadeClassifier("haarcascade_frontalface_default.xml");

        private int contadorExito = 0;
        private const int VotosParaValidar = 8;
        private const double UmbralDistanciaBase = 95;
        private const double UmbralPocaLuz = 110;
        private const double UmbralLuzIntensa = 100;

        private Dictionary<int, string> etiquetaANombre = new Dictionary<int, string>();
        private int etiquetaUsuarioValido = -1;
        private int totalUsuariosEnModelo = 0;

        private CancellationTokenSource cts;
        private volatile bool procesando = false;
        private System.Windows.Forms.Timer timerCamara;
        private List<double> historialDistancias = new List<double>();
        private List<int> historialEtiquetas = new List<int>();
        private const int HistorialSize = 8;

        /// <summary>
        /// Constructor del formulario de reconocimiento facial.
        /// </summary>
        public LoginFacial()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        /// <summary>
        /// Evento Load del formulario. Carga el modelo facial, entrena el reconocedor
        /// e inicia la captura de video desde la cámara.
        /// </summary>
        private void LoginFacial_Load(object sender, EventArgs e)
        {
            DepurarDirectorioRostros();

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
        /// Método de diagnóstico que imprime en la consola de salida los usuarios
        /// encontrados en el directorio de rostros. Útil para verificar coincidencias de nombres.
        /// </summary>
        private void DepurarDirectorioRostros()
        {
            try
            {
                if (!Directory.Exists(DetectorRostroService.DirectorioRostros))
                {
                    System.Diagnostics.Debug.WriteLine($"[ERROR] El directorio no existe: {DetectorRostroService.DirectorioRostros}");
                    return;
                }

                var archivos = Directory.GetFiles(DetectorRostroService.DirectorioRostros, "*.jpg");
                System.Diagnostics.Debug.WriteLine($"[DEBUG] ========================================");
                System.Diagnostics.Debug.WriteLine($"[DEBUG] Directorio: {DetectorRostroService.DirectorioRostros}");
                System.Diagnostics.Debug.WriteLine($"[DEBUG] Total archivos: {archivos.Length}");
                System.Diagnostics.Debug.WriteLine($"[DEBUG] Usuario a validar: '{UsuarioAValidar}'");

                var usuarios = archivos
                    .Select(f =>
                    {
                        string nombre = Path.GetFileNameWithoutExtension(f);
                        int idx = nombre.IndexOf('_');
                        return idx >= 0 ? nombre.Substring(0, idx) : nombre;
                    })
                    .Distinct()
                    .OrderBy(u => u);

                System.Diagnostics.Debug.WriteLine($"[DEBUG] Usuarios encontrados:");
                foreach (var usuario in usuarios)
                {
                    bool coincide = string.Equals(usuario, UsuarioAValidar, StringComparison.OrdinalIgnoreCase);
                    System.Diagnostics.Debug.WriteLine($"[DEBUG]   - '{usuario}' {(coincide ? "← COINCIDE" : "")}");
                }
                System.Diagnostics.Debug.WriteLine($"[DEBUG] ========================================");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ERROR] DepurarDirectorioRostros: {ex.Message}");
            }
        }

        /// <summary>
        /// Regresa al formulario principal de inicio de sesión.
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
        /// Entrena el modelo LBPH con todos los usuarios registrados en el sistema.
        /// Asigna una etiqueta única a cada usuario para su identificación.
        /// </summary>
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

                if (string.Equals(nombreUsuario, UsuarioAValidar, StringComparison.OrdinalIgnoreCase))
                    etiquetaUsuarioValido = etiqueta;

                foreach (var archivo in grupo)
                {
                    var img = new Image<Gray, byte>(archivo).Resize(100, 100, Inter.Linear);
                    AplicarPreprocesamiento(img);
                    AgregarConVariantes(img, rostros, etiquetas, etiqueta);
                }
            }

            if (etiquetaUsuarioValido == -1)
            {
                string usuariosEncontrados = string.Join(", ", etiquetaANombre.Values);
                throw new Exception(
                    $"No se encontró la etiqueta para el usuario a validar.\n\n" +
                    $"Usuario buscado: '{UsuarioAValidar}'\n" +
                    $"Usuarios encontrados en el modelo: {usuariosEncontrados}\n" +
                    $"Total de usuarios en el modelo: {totalUsuariosEnModelo}\n\n" +
                    $"Verifica que:\n" +
                    $"1. El nombre de usuario coincida exactamente con el nombre base de los archivos de imagen\n" +
                    $"2. Las imágenes estén en: {DetectorRostroService.DirectorioRostros}\n" +
                    $"3. El formato del archivo sea: NOMBREUSUARIO_numero.jpg (ej: JuanPerez_1.jpg)");
            }

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
        /// Agrega variantes de una imagen (original, flip horizontal, brillo alto, brillo bajo)
        /// para mejorar el entrenamiento del modelo.
        /// </summary>
        private void AgregarConVariantes(Image<Gray, byte> imagenBase,
            List<Image<Gray, byte>> lista, List<int> etiquetas, int etiqueta)
        {
            void Add(Image<Gray, byte> img) { lista.Add(img); etiquetas.Add(etiqueta); }

            Add(imagenBase);
            Add(imagenBase.Flip(FlipType.Horizontal));

            var brilloAlto = imagenBase.Clone(); brilloAlto._Mul(1.4); Add(brilloAlto);
            var brilloBajo = imagenBase.Clone(); brilloBajo._Mul(0.7); Add(brilloBajo);
        }

        /// <summary>
        /// Inicia la captura de video desde la cámara predeterminada del sistema.
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
        /// Evento Tick del temporizador de la cámara. Captura un frame y lo procesa.
        /// </summary>
        private void TimerCamara_Tick(object sender, EventArgs e)
        {
            if (camara == null || procesando) return;

            Mat m = new Mat();
            camara.Read(m);
            if (m.IsEmpty) { m.Dispose(); return; }

            using (var frameUI = m.ToImage<Bgr, byte>())
            {
                if (picValidar.Image != null) picValidar.Image.Dispose();
                picValidar.Image = frameUI.ToBitmap();
            }

            procesando = true;
            var token = cts.Token;
            Task.Run(() =>
            {
                try { if (!token.IsCancellationRequested) ProcesarFrame(m); }
                finally { m.Dispose(); procesando = false; }
            }, token);
        }

        /// <summary>
        /// Calcula la distancia promedio del historial de predicciones.
        /// </summary>
        private double CalcularDistanciaPromedio()
        {
            if (historialDistancias.Count == 0) return 999;
            return historialDistancias.Average();
        }

        /// <summary>
        /// Obtiene la etiqueta más frecuente del historial de predicciones.
        /// </summary>
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
        /// Procesa un frame de la cámara: detecta rostros, los compara con el modelo
        /// entrenado y determina si coincide con el usuario a validar.
        /// </summary>
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
                        CvInvoke.CLAHE(rostroProcesado, 2.0, new Size(8, 8), 256, rostroProcesado);

                        var resultado = recognizer.Predict(rostroProcesado);
                        double distancia = resultado.Distance;

                        historialDistancias.Add(distancia);
                        historialEtiquetas.Add(resultado.Label);
                        if (historialDistancias.Count > HistorialSize)
                            historialDistancias.RemoveAt(0);
                        if (historialEtiquetas.Count > HistorialSize)
                            historialEtiquetas.RemoveAt(0);

                        double distanciaPromedio = CalcularDistanciaPromedio();
                        int etiquetaFrecuente = ObtenerEtiquetaMasFrecuente();

                        MCvScalar media = CvInvoke.Mean(rostroProcesado.Mat);
                        double brillo = media.V0;
                        double umbralActual = UmbralDistanciaBase;
                        if (brillo < 70) umbralActual = UmbralPocaLuz;
                        else if (brillo > 180) umbralActual = UmbralLuzIntensa;

                        bool coincide = etiquetaFrecuente == etiquetaUsuarioValido &&
                                        distanciaPromedio < umbralActual &&
                                        historialEtiquetas.Count >= 4;

                        System.Diagnostics.Debug.WriteLine($"[DEBUG] Usuario: {UsuarioAValidar}, Label: {resultado.Label}, Frec: {etiquetaFrecuente}, Dist: {distancia:F1}, Prom: {distanciaPromedio:F1}, Umbral: {umbralActual}, Brillo: {brillo:F0}");

                        if (coincide)
                        {
                            contadorExito++;
                            ActualizarEstado($"Verificando... ({contadorExito}/{VotosParaValidar})", Color.DodgerBlue);
                            if (contadorExito >= VotosParaValidar)
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
        /// Aplica preprocesamiento a una imagen: ecualización de histograma, CLAHE y desenfoque gaussiano.
        /// </summary>
        private void AplicarPreprocesamiento(Image<Gray, byte> imagen)
        {
            using (Mat m = imagen.Mat)
            {
                CvInvoke.EqualizeHist(m, m);
                CvInvoke.CLAHE(m, 2.0, new Size(8, 8), 256, m);
                CvInvoke.GaussianBlur(m, m, new Size(3, 3), 0.5);
            }
        }

        /// <summary>
        /// Actualiza el texto y color de la etiqueta de estado en la interfaz de usuario.
        /// </summary>
        private void ActualizarEstado(string texto, Color color)
        {
            if (lblEstado.InvokeRequired)
                lblEstado.Invoke(new Action(() => { lblEstado.Text = texto; lblEstado.ForeColor = color; }));
            else
                lblEstado.Text = texto; lblEstado.ForeColor = color;
        }

        /// <summary>
        /// Finaliza el proceso de reconocimiento facial y navega al menú correspondiente.
        /// </summary>
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
        /// Libera los recursos de la cámara al cerrar el formulario.
        /// </summary>
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
        /// Evento Click del botón Cancelar. Regresa al formulario de inicio de sesión.
        /// </summary>
        private void btnCancelar1_Click(object sender, EventArgs e) => RegresarAlLogin();

        /// <summary>
        /// Evento Click del botón Reintentar. Reinicia el contador de éxito y el historial.
        /// </summary>
        private void btnReintentar1_Click(object sender, EventArgs e)
        {
            contadorExito = 0;
            historialDistancias.Clear();
            historialEtiquetas.Clear();
            ActualizarEstado("Reintentando escaneo...", Color.Black);
        }
    }
}