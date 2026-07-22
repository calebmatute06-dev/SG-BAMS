using SG_BAMS.Administracion_de_BAMS;
using SG_BAMS.Administracion_de_BAMS.Rol;
using System;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Representa la interfaz de usuario para la modificación de un rol de usuario existente.
    /// DIP: recibe ICatalogoRepository inyectado, no instancia clsRol directamente.
    /// </summary>
    public partial class frmModificarRol : Form
    {
        private readonly ICatalogoRepository _repositorio;
        private readonly int idRolSeleccionado;
        private PlaceholderTextBox phDescri;

        public frmModificarRol(int id, string nombreActual, ICatalogoRepository repositorio)
        {
            InitializeComponent();
            _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
            this.StartPosition = FormStartPosition.CenterScreen;
            this.idRolSeleccionado = id;
            txtDescri.Text = nombreActual;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            txtDescri.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
            phDescri = new PlaceholderTextBox(txtDescri, "Escriba el nombre del rol");
        }

        /// <summary>
        /// Constructor de compatibilidad sin repositorio explícito (usa clsRol por defecto).
        /// </summary>
        public frmModificarRol(int id, string nombreActual)
            : this(id, nombreActual, new clsRol()) { }

        private void frmModificarRol_Load(object sender, EventArgs e)
        {
            txtDescri.Focus();
            txtDescri.SelectionStart = txtDescri.Text.Length;
        }

        private async void btmModificar_Click(object sender, EventArgs e)
        {
            string nombreReal = phDescri.GetRealValue().Trim();

            using (var temp = new TextBox { Text = nombreReal })
            {
                if (!ClsValidaciones.EsNombrePersonalValido(temp, "Nombre del Rol"))
                    return;
            }

            using (var temp = new TextBox { Text = nombreReal })
            {
                if (!ClsValidaciones.ValidarNombreUnico(
                        control: temp,
                        tabla: "Rol",
                        columnaNombre: "descripcion_rol",
                        nombreCampo: "Tipo de Rol",
                        idExcluir: idRolSeleccionado,
                        idColumna: "id_rol_usuario"))
                    return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btmModificar.Enabled = false;

                bool exito = await _repositorio.ModificarAsync(idRolSeleccionado, nombreReal);

                if (exito)
                {
                    MessageBox.Show("Rol actualizado con éxito.", "SG-BAMS",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar: " + ex.Message, "Error de Sistema",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btmModificar.Enabled = true;
            }
        }

        private void btmSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
