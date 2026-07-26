using SG_BAMS.Administracion_de_BAMS;
using System;
using System.Windows.Forms;
using SG_BAMS.AccesoDatos;
using SG_BAMS.Dominio.AdministracionBAMS;

namespace SG_BAMS
{
    /// <summary>
    /// Formulario para agregar un nuevo estado al sistema.
    /// DIP: recibe ICatalogoRepository inyectado, no instancia clsEstado directamente.
    /// SRP: única responsabilidad — capturar y validar datos para crear un estado.
    /// </summary>
    public partial class frmAgregarEstado : Form
    {
        private readonly ICatalogoRepository _repositorio;
        private PlaceholderTextBox _phDescri;

        /// <summary>
        /// Constructor que recibe el repositorio por inyección de dependencias.
        /// </summary>
        public frmAgregarEstado(ICatalogoRepository repositorio)
        {
            InitializeComponent();
            _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));

            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            this.txtDescri.KeyPress += txtDescri_KeyPress;

            // Placeholder registrado una sola vez aquí — evita el doble registro del original
            _phDescri = new PlaceholderTextBox(txtDescri, "Ingrese la descripción del estado");
        }

        /// <summary>
        /// Constructor sin parámetros para compatibilidad con código existente.
        /// </summary>
        public frmAgregarEstado() : this(new clsEstado()) { }

        private void txtDescri_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.PermitirSoloLetras(e);
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e) => this.Close();

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            string descripcionReal = _phDescri.GetRealValue().Trim();

            using (var temp = new TextBox { Text = descripcionReal })
            {
                if (!ClsValidaciones.EsNombrePersonalValido(temp, "Descripción del Estado"))
                    return;
            }

            using (var temp = new TextBox { Text = descripcionReal })
            {
                if (!ClsValidaciones.ValidarNombreUnico(
                        control: temp,
                        tabla: "Estado",
                        columnaNombre: "descripcion_estado",
                        nombreCampo: "Tipo de Estado",
                        idExcluir: 0,
                        idColumna: "id_estado"))
                    return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnAgregar.Enabled = false;

                bool exito = await _repositorio.InsertarAsync(descripcionReal);

                if (exito)
                {
                    MessageBox.Show("Estado registrado correctamente.", "SG-BAMS",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btnAgregar.Enabled = true;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e) => this.Close();
    }
}