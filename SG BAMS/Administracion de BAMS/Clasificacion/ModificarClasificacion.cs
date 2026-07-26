using SG_BAMS.Administracion_de_BAMS;
using SG_BAMS.Administracion_de_BAMS.Clasificacion;
using System;
using System.Windows.Forms;
using SG_BAMS.AccesoDatos;
using SG_BAMS.Dominio.AdministracionBAMS;


namespace SG_BAMS.Administracion_de_BAMS.Clasificacion
{
    /// <summary>
    /// Formulario para modificar una clasificación de proveedor existente.
    /// DIP: recibe ICatalogoRepository inyectado, no instancia clsClasificacion directamente.
    /// SRP: única responsabilidad — capturar y validar datos para actualizar una clasificación.
    /// </summary>
    public partial class ModificarClasificacion : Form
    {
        private readonly ICatalogoRepository _repositorio;
        private readonly int _idSeleccionado;
        private PlaceholderTextBox _phDescri;

        /// <summary>
        /// Constructor que recibe el repositorio por inyección de dependencias.
        /// </summary>
        public ModificarClasificacion(int id, string descripcionActual, ICatalogoRepository repositorio)
        {
            InitializeComponent();
            _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
            _idSeleccionado = id;

            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            txtDescri.Text = descripcionActual;
            txtDescri.KeyPress += (s, e) => ClsValidaciones.PermitirAlfanumerico(e);

            _phDescri = new PlaceholderTextBox(txtDescri, "Ingrese una clasificación");
        }

        /// <summary>
        /// Constructor sin parámetros para compatibilidad con código existente.
        /// </summary>
        public ModificarClasificacion(int id, string descripcionActual)
            : this(id, descripcionActual, new clsClasificacion()) { }

        private void ModificarClasificacion_Load(object sender, EventArgs e)
        {
            txtDescri.Focus();
            txtDescri.SelectionStart = txtDescri.Text.Length;
        }

        private async void btnModificar_Click(object sender, EventArgs e)
        {
            string descripcionReal = _phDescri.GetRealValue().Trim();

            using (var temp = new TextBox { Text = descripcionReal })
            {
                if (!ClsValidaciones.EsAlfanumericoValido(temp, "Clasificación"))
                    return;
            }

            using (var temp = new TextBox { Text = descripcionReal })
            {
                if (!ClsValidaciones.ValidarNombreUnico(
                        control: temp,
                        tabla: "clasificacion_proveedor",
                        columnaNombre: "clasificacion_proveedor",
                        nombreCampo: "Clasificación",
                        idExcluir: _idSeleccionado,
                        idColumna: "id_clasificacion_proveedor"))
                    return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnModificar.Enabled = false;

                bool exito = await _repositorio.ModificarAsync(_idSeleccionado, descripcionReal);

                if (exito)
                {
                    MessageBox.Show("Clasificación actualizada correctamente.", "SG-BAMS",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar: " + ex.Message,
                    "Error de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btnModificar.Enabled = true;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e) => this.Close();

        private void label8_Click(object sender, EventArgs e) { }
    }
}