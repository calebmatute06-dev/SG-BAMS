using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using SG_BAMS.Administracion_de_BAMS.Usuarios;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Proporciona una interfaz para la captura y gestión de imágenes biométricas de empleados, 
    /// utilizando Emgu CV para la detección de rostros frontales y de perfil.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class frmImagenEmpleado : Form
    {
        private VideoCapture camara;
        private bool camaraEnEncendida = false;

        private CascadeClassifier frontalFaceDetector = new CascadeClassifier("haarcascade_frontalface_default.xml");
        private CascadeClassifier profileFaceDetector = new CascadeClassifier("haarcascade_profileface.xml");

        private string usuarioAsignado = "";

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="frmImagenEmpleado"/>.
        /// </summary>
        /// <param name="nombreUsuario">Nombre del usuario al que se le asociarán las capturas.</param>
        public frmImagenEmpleado(string nombreUsuario = "")
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            clsSoporte.InicializarDirectorio();
            usuarioAsignado = nombreUsuario;
        }

        /// <summary>
        /// Procesa cada cuadro capturado por la cámara en tiempo real para detectar y resaltar rostros.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void FrameProcess(object sender, EventArgs e)
        {
            if (camara != null && camaraEnEncendida)
            {
                try
                {
                    using (var frameMat = camara.QueryFrame())
                    {
                        if (frameMat != null)
                        {
                            using (var frame = frameMat.ToImage<Bgr, byte>())
                            {
                                using (var grayFrame = frame.Convert<Gray, byte>())
                                {
                                    CvInvoke.EqualizeHist(grayFrame, grayFrame);

                                    Rectangle[] rostrosFrontales = frontalFaceDetector.DetectMultiScale(grayFrame, 1.1, 10, Size.Empty);
                                    Rectangle[] rostrosPerfil = profileFaceDetector.DetectMultiScale(grayFrame, 1.1, 10, Size.Empty);

                                    foreach (Rectangle rostro in rostrosFrontales.Concat(rostrosPerfil))
                                    {
                                        frame.Draw(rostro, new Bgr(Color.LimeGreen), 2);
                                    }
                                }

                                if (pctCamara.Image != null) pctCamara.Image.Dispose();
                                pctCamara.Image = frame.ToBitmap();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    DetenerCamara();
                    MessageBox.Show("Error en la cámara: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Gestiona el cierre del formulario, verificando que se hayan realizado las capturas mínimas requeridas.
        /// </summary>
        /// <param name="e">Datos del evento de cierre.</param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            var archivos = Directory.GetFiles(clsSoporte.DirectorioRostros, "*.jpg")
                    .Where(f => Path.GetFileNameWithoutExtension(f) == usuarioAsignado ||
                    Path.GetFileNameWithoutExtension(f).StartsWith(usuarioAsignado + "_"))
                    .ToList();

            if (archivos.Count == 0 && !string.IsNullOrWhiteSpace(usuarioAsignado))
            {
                DialogResult respuesta = MessageBox.Show(
                    "No se ha registrado ningún rostro para este usuario.\n\n" +
                    "El usuario NO podrá iniciar sesión sin registro facial.\n\n" +
                    "¿Estás seguro de que deseas salir sin registrar?",
                    "Registro facial requerido",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (respuesta == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }
            Application.Idle -= FrameProcess;
            if (camara != null) camara.Dispose();
            base.OnFormClosing(e);
        }

        /// <summary>
        /// Configura el estado inicial del formulario al cargarse.
        /// </summary>
        private void frmImagenEmpleado_Load(object sender, EventArgs e)
        {
            clsSoporte.InicializarDirectorio();
            if (!string.IsNullOrEmpty(usuarioAsignado))
                lblUsuario.Text = usuarioAsignado;
            EncenderCamara(false);
        }

        /// <summary>
        /// Inicializa el dispositivo de captura de video y activa el procesamiento de cuadros.
        /// </summary>
        /// <param name="mostrarMensajeExito">Indica si se debe notificar al usuario cuando la cámara se encienda.</param>
        private void EncenderCamara(bool mostrarMensajeExito)
        {
            try
            {
                if (camara == null)
                {
                    camara = new VideoCapture(0);

                    if (camara.IsOpened)
                    {
                        Application.Idle += FrameProcess;
                        camaraEnEncendida = true;
                        btnCapturar.Enabled = true;

                        if (mostrarMensajeExito)
                        {
                            MessageBox.Show("Cámara encendida con éxito.", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        camara.Dispose();
                        camara = null;
                        MessageBox.Show("No se logro encender la camara intente de nuevo", "Error de Cámara", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    if (mostrarMensajeExito)
                    {
                        MessageBox.Show("La cámara ya está encendida.", "Aviso");
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No se logro encender la camara intente de nuevo", "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Desactiva el dispositivo de captura y libera los recursos asociados.
        /// </summary>
        private void DetenerCamara()
        {
            if (camara != null)
            {
                Application.Idle -= FrameProcess;
                camaraEnEncendida = false;
                camara.Dispose();
                camara = null;
                pctCamara.Image = null;
                pctCamara.Invalidate();
            }
        }

        /// <summary>
        /// Realiza una ráfaga de capturas automáticas buscando detectar el rostro para el entrenamiento o registro.
        /// </summary>
        private async void btnCapturar_Click_1(object sender, EventArgs e)
        {
            if (camara == null)
            {
                MessageBox.Show("Primero debes encender la cámara.");
                return;
            }

            if (string.IsNullOrWhiteSpace(lblUsuario.Text))
            {
                MessageBox.Show("No hay un usuario asignado.");
                return;
            }

            string nombreArchivo = lblUsuario.Text;
            int fotosTomadas = 0;
            int intentos = 0;

            btnCapturar.Enabled = false;

            while (fotosTomadas < 5 && intentos < 30)
            {
                using (var frameMat = camara.QueryFrame())
                {
                    if (frameMat != null)
                    {
                        using (var frame = frameMat.ToImage<Bgr, byte>())
                        {
                            var rostro = clsSoporte.DetectarRostro(frame);
                            if (rostro != null)
                            {
                                string nombreFoto = $"{nombreArchivo}_{fotosTomadas + 1}_{DateTime.Now.Ticks}.jpg";
                                string path = Path.Combine(clsSoporte.DirectorioRostros, nombreFoto);

                                rostro.Save(path);
                                fotosTomadas++;
                            }
                        }
                    }
                }
                intentos++;
                await Task.Delay(400);
            }

            btnCapturar.Enabled = true;

            if (fotosTomadas >= 5)
                MessageBox.Show($"¡Análisis completado! Se guardaron {fotosTomadas} capturas con éxito.", "Éxito");
            else if (fotosTomadas > 0)
                MessageBox.Show($"Se capturaron {fotosTomadas} fotos. Intenta mover la cabeza más lento para llegar a 5.", "Aviso");
            else
                MessageBox.Show("No se detectó el rostro. Asegúrate de tener buena iluminación frontal.");
        }

        /// <summary>
        /// Elimina todas las capturas registradas en el disco asociadas al usuario actual.
        /// </summary>
        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(lblUsuario.Text))
            {
                MessageBox.Show("No hay un usuario asignado.", "Aviso");
                return;
            }

            string nombreUsuario = lblUsuario.Text;
            try
            {
                var archivos = Directory.GetFiles(clsSoporte.DirectorioRostros, "*.jpg")
                    .Where(f => Path.GetFileNameWithoutExtension(f).StartsWith(nombreUsuario)).ToList();

                if (archivos.Count == 0)
                {
                    MessageBox.Show($"No se encontraron fotos registradas para '{nombreUsuario}'.", "Sin archivos", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (MessageBox.Show($"Se encontraron {archivos.Count} fotos. ¿Estás seguro de eliminarlas?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    foreach (var archivo in archivos)
                    {
                        File.Delete(archivo);
                    }
                    MessageBox.Show("Fotos eliminadas con éxito.", "Éxito");
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("No se pudieron borrar los archivos: " + ex.Message, "Error de E/S");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado al borrar: " + ex.Message, "Error");
            }
        }

        /// <summary>
        /// Maneja la acción manual de encendido de la cámara.
        /// </summary>
        private void btnEncender_Click(object sender, EventArgs e)
        {
            EncenderCamara(true);
        }

        /// <summary>
        /// Maneja la acción manual de apagado de la cámara.
        /// </summary>
        private void btnDetener_Click(object sender, EventArgs e)
        {
            if (camara != null)
            {
                DetenerCamara();
                btnCapturar.Enabled = false;
                MessageBox.Show("Cámara desconectada correctamente.", "Sistema");
            }
            else
            {
                MessageBox.Show("La cámara ya se encuentra apagada.", "Aviso");
            }
        }

        /// <summary>
        /// Detiene la cámara y cierra el formulario.
        /// </summary>
        private void btnSalir_Click(object sender, EventArgs e)
        {
            DetenerCamara();
            this.Close();
        }
    }
}