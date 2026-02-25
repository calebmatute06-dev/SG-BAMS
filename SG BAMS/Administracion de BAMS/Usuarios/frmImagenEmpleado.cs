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
        // Propiedad pública para recibir el nombre del usuario sin usar el constructor
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
            // Validamos que se haya seleccionado un usuario del ComboBox
            if (cmbUsuarios2.SelectedIndex == -1)
            {
                MessageBox.Show("Selecciona un usuario primero.");
                return;
            }

            // Obtenemos el texto visible (el nombre)
            string nombreArchivo = cmbUsuarios2.Text;

            using (var frame = camara.QueryFrame().ToImage<Bgr, byte>())
            {
                var rostro = clsSoporte.DetectarRostro(frame);
                if (rostro != null)
                {
                    string path = Path.Combine(clsSoporte.DirectorioRostros, nombreArchivo + ".jpg");
                    rostro.Save(path);
                    MessageBox.Show("Rostro guardado para " + nombreArchivo);
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
            // Inicializamos la carpeta y la cámara al cargar el formulario
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
                    cmbUsuarios2.DataSource = dt;
                    // 'nombre_usuario' es el nombre exacto en tu tabla/SP
                    cmbUsuarios2.DisplayMember = "nombre_usuario";
                    // 'id_usuario' es la llave primaria de tu tabla
                    cmbUsuarios2.ValueMember = "id_usuario";

                    cmbUsuarios2.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                // Esto atrapará el error de ConnectionString si aún persiste
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
                // 1. Primero dejamos de procesar frames
                Application.Idle -= FrameProcess;
                camaraEnEncendida = false;

                // 2. Liberamos el hardware
                camara.Dispose();
                camara = null;

                // 3. Limpiamos la interfaz
                pctCamara.Image = null;
                pctCamara.Invalidate();
            }
        }

        private void btnEncender_Click(object sender, EventArgs e)
        {
            if (camara == null)
            {
                camara = new VideoCapture(0);
                // Suscribimos el proceso de dibujo
                Application.Idle += FrameProcess;
                camaraEnEncendida = true;
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            // 1. Validamos que haya un usuario seleccionado en el ComboBox
            if (cmbUsuarios2.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona un usuario de la lista para borrar su registro facial.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nombreUsuario = cmbUsuarios2.Text;

            // 2. Construimos la ruta del archivo usando tu clase clsSoporte
            string path = Path.Combine(clsSoporte.DirectorioRostros, nombreUsuario + ".jpg");

            try
            {
                // 3. Verificamos si el archivo existe antes de intentar borrarlo
                if (File.Exists(path))
                {
                    // Preguntar confirmación al usuario
                    DialogResult result = MessageBox.Show($"¿Estás seguro de que deseas eliminar el registro biométrico de {nombreUsuario}?",
                                                          "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // Importante: Si la cámara está encendida y mostrando esa imagen, 
                        // a veces el archivo puede estar "bloqueado". Por seguridad, liberamos recursos.
                        File.Delete(path);

                        MessageBox.Show($"El registro facial de {nombreUsuario} ha sido eliminado.",
                                        "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Opcional: Limpiar el PictureBox si se estaba mostrando la cara borrada
                        pctCamara.Image = null;
                    }
                }
                else
                {
                    MessageBox.Show("No existe un registro facial guardado para este usuario.",
                                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar borrar el archivo: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //--------------------------------------------------------------Final

    }
}