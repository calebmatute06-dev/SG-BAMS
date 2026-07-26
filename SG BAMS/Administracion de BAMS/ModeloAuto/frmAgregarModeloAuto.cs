using SG_BAMS.Administracion_de_BAMS;
using System;
using System.Windows.Forms;
using SG_BAMS.AccesoDatos;
using SG_BAMS.Dominio.AdministracionBAMS;

namespace SG_BAMS
{
    /// <summary>
    /// Representa la interfaz de usuario para registrar un nuevo modelo de automóvil en el sistema.
    /// DIP: recibe ICatalogoRepository inyectado, no instancia clsModeloAuto directamente.
    /// </summary>
    public partial class frmAgregarModeloAuto : Form
    {
        private readonly ICatalogoRepository _repositorio;
        private PlaceholderTextBox phDescri;

        public frmAgregarModeloAuto(ICatalogoRepository repositorio)
        {
            InitializeComponent();
            _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));

            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            txtDescri.KeyPress += (s, e) => ClsValidaciones.PermitirAlfanumerico(e);
            phDescri = new PlaceholderTextBox(txtDescri, "Ingrese el nombre del modelo de auto");
        }

        /// <summary>
        /// Constructor sin parámetros para compatibilidad con el diseñador de WinForms.
        /// </summary>
        public frmAgregarModeloAuto() : this(new clsModeloAuto()) { }

        private async void btnAgregar_Click_1(object sender, EventArgs e)
        {
            string nombreReal = phDescri.GetRealValue().Trim();

            using (var temp = new TextBox { Text = nombreReal })
            {
                if (!ClsValidaciones.EsAlfanumericoValido(temp, "Nombre del Modelo de Auto"))
                    return;
            }

            using (var temp = new TextBox { Text = nombreReal })
            {
                if (!ClsValidaciones.ValidarNombreUnico(
                        control: temp,
                        tabla: "Modelo_de_auto",
                        columnaNombre: "nombre_modelo_auto",
                        nombreCampo: "Tipo de Modelo de Auto",
                        idExcluir: 0,
                        idColumna: "id_modelo_auto"))
                    return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (btnAgregar != null) btnAgregar.Enabled = false;

                bool exito = await _repositorio.InsertarAsync(nombreReal);

                if (exito)
                {
                    MessageBox.Show("Modelo de auto agregado con éxito.", "SG-BAMS",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de sistema: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                if (btnAgregar != null) btnAgregar.Enabled = true;
            }
        }

        private void btnSalir_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
