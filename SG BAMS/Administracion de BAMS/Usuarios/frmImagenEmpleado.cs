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

        private async void btnCapturar_Click(object sender, EventArgs e)
        {

        }


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
            EncenderCamara(false);
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



        private async void btnCapturar_Click_1(object sender, EventArgs e)
        {
            if (camara == null)
            {
                MessageBox.Show("Primero debes encender la cámara.");
                return;
            }

            if (string.IsNullOrWhiteSpace(cmbUsuarios.Text))
            {
                MessageBox.Show("Selecciona o ingresa un usuario primero.");
                return;
            }

            if (string.IsNullOrWhiteSpace(cmbUsuarios.Text))
            {
                MessageBox.Show("Selecciona o ingresa un usuario primero.");
                return;
            }

            string nombreArchivo = cmbUsuarios.Text;
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
                            using (var gray = frame.Convert<Gray, byte>())
                            {
                                CvInvoke.EqualizeHist(gray, gray);
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
                await Task.Delay(400); 
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

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbUsuarios.Text))
            {
                MessageBox.Show("Selecciona un usuario para buscar sus fotos.", "Aviso");
                return;
            }

            string nombreUsuario = cmbUsuarios.Text;
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
                MessageBox.Show("No se pudieron borrar los archivos. Asegúrate de que no estén abiertos en otro programa: " + ex.Message, "Error de E/S");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado al borrar: " + ex.Message, "Error");
            }
        }

        private void btnEncender_Click(object sender, EventArgs e)
        {
            EncenderCamara(true);
        }

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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DetenerCamara(); this.Close();
        }
    }
}