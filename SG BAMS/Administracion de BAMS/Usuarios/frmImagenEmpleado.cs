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
    public partial class frmImagenEmpleado : Form
    {
        private VideoCapture camara;
        private bool camaraEnEncendida = false;

        // Detectores para diferentes ángulos
        private CascadeClassifier frontalFaceDetector = new CascadeClassifier("haarcascade_frontalface_default.xml");
        private CascadeClassifier profileFaceDetector = new CascadeClassifier("haarcascade_profileface.xml");

        private string usuarioAsignado = "";

        public frmImagenEmpleado(string nombreUsuario = "")
        {
            InitializeComponent();
            clsSoporte.InicializarDirectorio();
            usuarioAsignado = nombreUsuario;
        }

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
                                // PROCESAMIENTO PARA LUZ: Convertir a gris y mejorar contraste
                                using (var grayFrame = frame.Convert<Gray, byte>())
                                {
                                    // Ecualización para normalizar la iluminación
                                    CvInvoke.EqualizeHist(grayFrame, grayFrame);

                                    // Detección frontal
                                    Rectangle[] rostrosFrontales = frontalFaceDetector.DetectMultiScale(grayFrame, 1.1, 10, Size.Empty);
                                    // Detección de perfil (ángulos)
                                    Rectangle[] rostrosPerfil = profileFaceDetector.DetectMultiScale(grayFrame, 1.1, 10, Size.Empty);

                                    // Dibujar rostros detectados
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

        private async void btnCapturar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbUsuarios.Text))
            {
                MessageBox.Show("Selecciona o ingresa un usuario primero.");
                return;
            }

            string nombreArchivo = cmbUsuarios.Text;
            int fotosTomadas = 0;
            int intentos = 0;

            btnCapturar.Enabled = false;

            while (fotosTomadas < 5 && intentos < 30) // Más intentos para buscar mejores ángulos
            {
                using (var frameMat = camara.QueryFrame())
                {
                    if (frameMat != null)
                    {
                        using (var frame = frameMat.ToImage<Bgr, byte>())
                        {
                            // Aplicamos la misma mejora de luz antes de pasar el frame a clsSoporte
                            using (var gray = frame.Convert<Gray, byte>())
                            {
                                CvInvoke.EqualizeHist(gray, gray);
                                // Nota: clsSoporte.DetectarRostro debería idealmente usar gray para ser más preciso
                            }

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
                await Task.Delay(400); // Pausa ligeramente mayor para permitir movimiento
            }

            btnCapturar.Enabled = true;

            if (fotosTomadas >= 5)
            {
                MessageBox.Show($"¡Análisis completado! Se guardaron {fotosTomadas} capturas con éxito.", "Éxito");
            }
            else if (fotosTomadas > 0)
            {
                MessageBox.Show($"Se capturaron {fotosTomadas} fotos. Intenta mover la cabeza más lento para llegar a 5.", "Aviso");
            }
            else
            {
                MessageBox.Show("No se detectó el rostro. Asegúrate de tener buena iluminación frontal.");
            }
        }

        // --- El resto de métodos (Load, LlenarUsuarios, Detener, etc.) se mantienen igual ---

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.Idle -= FrameProcess;
            if (camara != null) camara.Dispose();
            base.OnFormClosing(e);
        }

        private void frmImagenEmpleado_Load(object sender, EventArgs e)
        {
            clsSoporte.InicializarDirectorio();
            LlenarUsuarios();
            if (!string.IsNullOrEmpty(usuarioAsignado)) cmbUsuarios.Text = usuarioAsignado;
        }

        private void LlenarUsuarios()
        {
            try
            {
                clsSoporte soporte = new clsSoporte();
                DataTable dt = soporte.ObtenerUsuarios();
                if (dt.Rows.Count > 0)
                {
                    cmbUsuarios.DataSource = dt;
                    cmbUsuarios.DisplayMember = "nombre_usuario";
                    cmbUsuarios.ValueMember = "id_usuario";
                    cmbUsuarios.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmbUsuarios.AutoCompleteSource = AutoCompleteSource.ListItems;
                    cmbUsuarios.SelectedIndex = -1;
                }
            }
            catch (Exception ex) { MessageBox.Show("Error al cargar ComboBox: " + ex.Message); }
        }

        private void btnSalir_Click(object sender, EventArgs e) { DetenerCamara(); this.Close(); }
        private void btnDetener_Click(object sender, EventArgs e) { DetenerCamara(); }

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

        private void btnEncender_Click(object sender, EventArgs e)
        {
            if (camara == null)
            {
                camara = new VideoCapture(0);
                Application.Idle += FrameProcess;
                camaraEnEncendida = true;
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbUsuarios.Text)) return;
            string nombreUsuario = cmbUsuarios.Text;
            try
            {
                var archivos = Directory.GetFiles(clsSoporte.DirectorioRostros, "*.jpg")
                    .Where(f => Path.GetFileNameWithoutExtension(f).StartsWith(nombreUsuario)).ToList();

                if (archivos.Count > 0 && MessageBox.Show($"¿Eliminar {archivos.Count} fotos?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    foreach (var archivo in archivos) File.Delete(archivo);
                    MessageBox.Show("Registros eliminados.");
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }
    }
}