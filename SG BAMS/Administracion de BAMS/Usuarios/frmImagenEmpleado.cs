using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using SG_BAMS.Administracion_de_BAMS.Usuarios;
using System;
using System.Data;
using System.Drawing;
using System.IO;

namespace SG_BAMS
{
    public partial class frmImagenEmpleado : Form
    {
        private VideoCapture camara;
        private bool camaraEnEncendida = false;
        public frmImagenEmpleado()
        {
            InitializeComponent();
            clsSoporte.InicializarDirectorio();
        }
        private void FrameProcess(object sender, EventArgs e)
        {
            if (camara != null && camaraEnEncendida)
            {
                try
                {
                    using (var frame = camara.QueryFrame())
                    {
                        if (frame != null)
                        {
                            pctCamara.Image = frame.ToBitmap();
                        }
                    }
                }
                catch
                {
                    DetenerCamara();
                }
            }
        }

        private void btnCapturar_Click(object sender, EventArgs e)
        {
            if (cmbUsuarios.SelectedIndex == -1)
            {
                MessageBox.Show("Selecciona un usuario primero.");
                return;
            }

            string nombreArchivo = cmbUsuarios.Text;

            using (var frame = camara.QueryFrame().ToImage<Bgr, byte>())
            {
                var rostro = clsSoporte.DetectarRostro(frame);
                if (rostro != null)
                {
                    string nombreFoto = $"{nombreArchivo}_{DateTime.Now.Ticks}.jpg";
                    string path = Path.Combine(clsSoporte.DirectorioRostros, nombreFoto);

                    rostro.Save(path);
                    MessageBox.Show("Rostro guardado. Puedes tomar más fotos cambiando tu expresión o usando lentes para mejorar el reconocimiento.", "Éxito");
                }
                else
                {
                    MessageBox.Show("No se detectó ningún rostro. Intenta de nuevo.");
                }
            }
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

                    cmbUsuarios.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar ComboBox: " + ex.Message);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DetenerCamara();
            this.Close();
        }

        private void btnDetener_Click(object sender, EventArgs e)
        {
            DetenerCamara();
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
            if (cmbUsuarios.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona un usuario.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nombreUsuario = cmbUsuarios.Text;

            try
            {
                var archivos = Directory.GetFiles(clsSoporte.DirectorioRostros, "*.jpg")
                    .Where(f => Path.GetFileNameWithoutExtension(f) == nombreUsuario ||
                                Path.GetFileNameWithoutExtension(f).StartsWith(nombreUsuario + "_"))
                    .ToList();

                if (archivos.Count > 0)
                {
                    DialogResult result = MessageBox.Show($"¿Deseas eliminar las {archivos.Count} fotos biométricas de {nombreUsuario}?",
                                                          "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        foreach (var archivo in archivos)
                        {
                            File.Delete(archivo);
                        }

                        MessageBox.Show($"Se han eliminado los registros faciales de {nombreUsuario}.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        pctCamara.Image = null;
                    }
                }
                else
                {
                    MessageBox.Show("No existen registros faciales para este usuario.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar borrar archivos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}