using SG_BAMS.Administracion_de_BAMS;
using SG_BAMS.Administracion_de_BAMS.Rol;
using System;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Representa la interfaz de usuario para el registro de nuevos roles de usuario en el sistema.
    /// DIP: recibe ICatalogoRepository inyectado, no instancia clsRol directamente.
    /// </summary>
    public partial class frmAgregarRol : Form
    {
        private readonly ICatalogoRepository _repositorio;
        private PlaceholderTextBox phDescri;

        public frmAgregarRol(ICatalogoRepository repositorio)
        {
            InitializeComponent();
            _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));

            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            txtDescri.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
            phDescri = new PlaceholderTextBox(txtDescri, "Escriba el nombre del rol");
        }

        /// <summary>
        /// Constructor sin parámetros para compatibilidad con el diseñador de WinForms.
        /// </summary>
        public frmAgregarRol() : this(new clsRol()) { }

        private void pictureBox16_Click(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }

        private async void btmAgregar_Click(object sender, EventArgs e)
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
                        idExcluir: 0,
                        idColumna: "id_rol_usuario"))
                    return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btmAgregar.Enabled = false;

                bool exito = await _repositorio.InsertarAsync(nombreReal);

                if (exito)
                {
                    MessageBox.Show("Rol registrado correctamente.", "SG-BAMS",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btmAgregar.Enabled = true;
            }
        }

        private void btmSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
