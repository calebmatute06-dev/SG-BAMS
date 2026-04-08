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
        private int fotosRequeridas = 15;
        private int fotosAnguloRequeridas = 5;

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
                                    CvInvoke.GaussianBlur(grayFrame, grayFrame, new Size(3, 3), 0);

                                    Rectangle[] rostrosFrontales = frontalFaceDetector.DetectMultiScale(grayFrame, 1.05, 8, Size.Empty);
                                    Rectangle[] rostrosPerfil = profileFaceDetector.DetectMultiScale(grayFrame, 1.05, 8, Size.Empty);

                                    foreach (Rectangle rostro in rostrosFrontales.Concat(rostrosPerfil))
                                    {

                                        frame.Draw(rostro, new Bgr(Color.LimeGreen), 3);
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
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            var archivos = Directory.GetFiles(clsSoporte.DirectorioRostros, "*.jpg")
                    .Where(f => Path.GetFileNameWithoutExtension(f) == usuarioAsignado ||
                    Path.GetFileNameWithoutExtension(f).StartsWith(usuarioAsignado + "_"))
                    .ToList();

            if (archivos.Count < fotosRequeridas && !string.IsNullOrWhiteSpace(usuarioAsignado))
            {
                DialogResult respuesta = MessageBox.Show(
                    $"Solo se han registrado {archivos.Count} rostros de {fotosRequeridas} requeridos.\n\n" +
                    "El usuario necesitará al menos 15 fotos para un reconocimiento facial confiable.\n\n" +
                    "¿Estás seguro de que deseas salir sin completar el registro?",
                    "Registro facial incompleto",
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
        private void EncenderCamara(bool mostrarMensajeExito)
        {
            try
            {
                if (camara == null)
                {
                    camara = new VideoCapture(0);


                    try
                    {
                        camara.Set(CapProp.FrameWidth, 640);
                        camara.Set(CapProp.FrameHeight, 480);
                        camara.Set(CapProp.AutoExposure, 0.25);
                    }
                    catch { }

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
                        MessageBox.Show("No se logró encender la cámara. Intente de nuevo.", "Error de Cámara", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show("No se logró encender la cámara. Intente de nuevo.", "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// Verifica la calidad del rostro detectado.
        /// </summary>
        /// <summary>
        /// Verifica la calidad del rostro detectado.
        /// </summary>
        private bool VerificarCalidadRostro(Image<Gray, byte> rostro)
        {

            if (rostro.Width < 100 || rostro.Height < 100)
                return false;


            using (Mat laplacian = new Mat())
            {
                CvInvoke.Laplacian(rostro.Mat, laplacian, DepthType.Cv32F, 3);


                MCvScalar media = new MCvScalar();
                MCvScalar desviacion = new MCvScalar();


                CvInvoke.MeanStdDev(laplacian, ref media, ref desviacion);

                double varianza = desviacion.V0 * desviacion.V0;


                return varianza > 50;
            }
        }

        /// <summary>
        /// Aplica preprocesamiento a la imagen capturada para mejorar la calidad.
        /// </summary>
        private Image<Gray, byte> PreprocesarRostro(Image<Gray, byte> rostro)
        {

            var redimensionado = rostro.Resize(100, 100, Inter.Linear);


            CvInvoke.EqualizeHist(redimensionado, redimensionado);
            CvInvoke.CLAHE(redimensionado, 2.5, new Size(8, 8), redimensionado);

            return redimensionado;
        }

        /// <summary>
        /// Realiza captura automática de múltiples fotos con diferentes ángulos y condiciones.
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


            var archivosExistentes = Directory.GetFiles(clsSoporte.DirectorioRostros, "*.jpg")
                .Where(f => Path.GetFileNameWithoutExtension(f).StartsWith(nombreArchivo))
                .ToList();

            int fotosActuales = archivosExistentes.Count;
            int fotosNecesarias = fotosRequeridas - fotosActuales;

            if (fotosNecesarias <= 0)
            {
                MessageBox.Show($"Ya tienes {fotosActuales} fotos registradas. No necesitas más.\n" +
                    "Si deseas agregar más, primero borra las existentes.",
                    "Captura completa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int fotosTomadas = 0;
            int intentos = 0;
            int maxIntentos = fotosNecesarias * 10;

            btnCapturar.Enabled = false;


            MessageBox.Show($"Se capturarán {fotosNecesarias} fotos.\n\n" +
                "Instrucciones:\n" +
                "1. Mantén tu rostro centrado\n" +
                "2. Mueve ligeramente la cabeza entre fotos\n" +
                "3. Cambia tu expresión (sonríe, serio, etc.)\n" +
                "4. Asegura buena iluminación\n\n" +
                "El proceso es automático. Presiona OK para comenzar.",
                "Captura facial", MessageBoxButtons.OK, MessageBoxIcon.Information);

            while (fotosTomadas < fotosNecesarias && intentos < maxIntentos)
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
                                CvInvoke.GaussianBlur(grayFrame, grayFrame, new Size(3, 3), 0);

                                var rostrosFrontales = frontalFaceDetector.DetectMultiScale(grayFrame, 1.05, 8, new Size(80, 80));
                                var rostro = rostrosFrontales.FirstOrDefault();

                                if (rostro != null && rostro.Width >= 100 && rostro.Height >= 100)
                                {
                                    using (var rostroRecortado = grayFrame.GetSubRect(rostro).Clone())
                                    {

                                        if (VerificarCalidadRostro(rostroRecortado))
                                        {

                                            var rostroProcesado = PreprocesarRostro(rostroRecortado);


                                            string angulo = (fotosTomadas % fotosAnguloRequeridas + 1).ToString();
                                            string nombreFoto = $"{nombreArchivo}_{fotosActuales + fotosTomadas + 1}_a{angulo}_{DateTime.Now.Ticks}.jpg";
                                            string path = Path.Combine(clsSoporte.DirectorioRostros, nombreFoto);

                                            rostroProcesado.Save(path);
                                            fotosTomadas++;


                                            await Task.Delay(800);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                intentos++;
                await Task.Delay(100);
            }

            btnCapturar.Enabled = true;

            if (fotosTomadas >= fotosNecesarias)
            {
                MessageBox.Show($"¡Éxito! Se capturaron {fotosTomadas} fotos con calidad óptima.\n\n" +
                    $"Total de fotos para {nombreArchivo}: {fotosActuales + fotosTomadas}\n" +
                    "El reconocimiento facial funcionará correctamente.",
                    "Captura completada", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (fotosTomadas > 0)
            {
                MessageBox.Show($"Se capturaron {fotosTomadas} de {fotosNecesarias} fotos requeridas.\n\n" +
                    "Sugerencias:\n" +
                    "- Acércate más a la cámara\n" +
                    "- Mejora la iluminación frontal\n" +
                    "- Limpia el lente de la cámara\n\n" +
                    "Presiona 'Capturar' nuevamente para completar el registro.",
                    "Captura parcial", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("No se detectó ningún rostro válido.\n\n" +
                    "Verifica:\n" +
                    "- Buena iluminación frontal\n" +
                    "- Rostro bien centrado\n" +
                    "- Cámara funcionando correctamente\n\n" +
                    "Presiona 'Encender Cámara' y vuelve a intentar.",
                    "Error de detección", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

                DialogResult resultado = MessageBox.Show(
                    $"Se encontraron {archivos.Count} fotos.\n\n" +
                    "¿Estás seguro de eliminarlas?\n" +
                    "El usuario deberá volver a registrar su rostro.",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    foreach (var archivo in archivos)
                    {
                        File.Delete(archivo);
                    }
                    MessageBox.Show($"{archivos.Count} fotos eliminadas con éxito.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
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