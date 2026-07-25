using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SG_BAMS.Administracion_de_BAMS.Clasificacion
{
    // ============================================================
    // AgregarClasificacion — DIP corrección
    // ============================================================
    // PROBLEMA EN EL ORIGINAL:
    //   clsClasificacion objetoCla = new clsClasificacion(); → viola DIP
    //   El formulario estaba acoplado a la implementación concreta.
    //
    // CORRECCIÓN:
    //   Recibe ICatalogoRepository por constructor.
    //   Constructor sin parámetros para el diseñador de WinForms.
    //
    // NOTA ADICIONAL: el doble registro del PlaceholderTextBox
    //   (constructor + Load) generaba que el placeholder se
    //   inicializara dos veces. Se unifica en el constructor.
    // ============================================================

    /// <summary>
    /// Formulario para agregar una nueva clasificación de proveedor.
    /// DIP: recibe ICatalogoRepository inyectado, no instancia clsClasificacion.
    /// </summary>
    public partial class AgregarClasificacion : Form
    {
        private readonly ICatalogoRepository _repositorio;
        private PlaceholderTextBox _phDescri;

        /// <summary>
        /// Constructor que recibe el repositorio por inyección de dependencias.
        /// </summary>
        public AgregarClasificacion(ICatalogoRepository repositorio)
        {
            InitializeComponent();
            _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));

            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            txtDescri.KeyPress += (s, e) => ClsValidaciones.PermitirAlfanumerico(e);

            // Placeholder registrado una sola vez, aquí en el constructor
            _phDescri = new PlaceholderTextBox(txtDescri, "Ingrese una nueva clasificación");
        }

        /// <summary>
        /// Constructor sin parámetros para compatibilidad con el diseñador de WinForms.
        /// </summary>
        public AgregarClasificacion() : this(new clsClasificacion()) { }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            string descripcion = _phDescri.GetRealValue().Trim();

            // Validar formato con un TextBox temporal (mantiene compatibilidad con ClsValidaciones)
            using (var temp = new TextBox { Text = descripcion })
            {
                if (!ClsValidaciones.EsAlfanumericoValido(temp, "Clasificación"))
                    return;
            }

            // Validar letras aisladas
            if (Regex.IsMatch(descripcion, @"(?i)\b(?![yY]\b)[a-zñáéíóú]\b"))
            {
                MessageBox.Show("No se permiten letras aisladas en el nombre (excepto la 'y').",
                    "Formato Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescri.Focus();
                return;
            }

            // Validar nombre único en base de datos
            using (var temp = new TextBox { Text = descripcion })
            {
                if (!ClsValidaciones.ValidarNombreUnico(
                        control: temp,
                        tabla: "clasificacion_proveedor",
                        columnaNombre: "clasificacion_proveedor",
                        nombreCampo: "Clasificación",
                        idExcluir: 0,
                        idColumna: "id_clasificacion_proveedor"))
                    return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnAgregar.Enabled = false;

                bool exito = await _repositorio.InsertarAsync(descripcion);

                if (exito)
                {
                    MessageBox.Show("Clasificación registrada con éxito.", "SG-BAMS",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error de Sistema",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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