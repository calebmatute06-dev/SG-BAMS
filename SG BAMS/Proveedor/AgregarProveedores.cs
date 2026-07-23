using SG_BAMS.Administracion_de_BAMS.Estado;
using SG_BAMS.Login;
using SG_BAMS.Proveedor.DTO;
using System;
using System.Data;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace SG_BAMS.Proveedor
{
    /// <summary>
    /// Formulario para agregar un nuevo proveedor al sistema.
    /// </summary>
    public partial class AgregarProveedores : Form
    {
        private readonly IProveedorRepository repositorio;
        private readonly IClasificacionRepository clasificacionRepositorio;

        private PlaceholderTextBox phNombre;
        private PlaceholderTextBox phDireccion;
        private PlaceholderTextBox phTelefono;
        private PlaceholderTextBox phRTN;
        private PlaceholderComboBox phClasificacion;

        public AgregarProveedores(IProveedorRepository repositorio, IClasificacionRepository clasificacionRepositorio)
        {
            InitializeComponent();
            this.repositorio = repositorio;
            this.clasificacionRepositorio = clasificacionRepositorio;

            this.StartPosition = FormStartPosition.CenterScreen;
            txtTelefono.MaxLength = 8;
            txtRTN.MaxLength = 14;
            this.txtNombre.KeyPress += new KeyPressEventHandler(this.txtNombre_KeyPress);
            this.txtDireccion.KeyPress += new KeyPressEventHandler(this.txtDireccion_KeyPress);
        }

        private void AgregarProveedores_Load(object sender, EventArgs e)
        {
            DataTable clasificaciones = clasificacionRepositorio.ObtenerClasificaciones();
            cmbClasificacion.DataSource = clasificaciones;
            cmbClasificacion.DisplayMember = "clasificacion_proveedor";
            cmbClasificacion.ValueMember = "id_clasificacion_proveedor";
            cmbClasificacion.DropDownStyle = ComboBoxStyle.DropDown;
            cmbClasificacion.SelectedIndex = -1;

            phNombre = new PlaceholderTextBox(txtNombre, "Ingrese el Nombre del proveedor");
            phDireccion = new PlaceholderTextBox(txtDireccion, "Colonia, Barrio, Pueblo");
            phTelefono = new PlaceholderTextBox(txtTelefono, "Número que empiece con 9,8,3,2");
            phRTN = new PlaceholderTextBox(txtRTN, "Ingrese el RTN");
            phClasificacion = new PlaceholderComboBox(cmbClasificacion, "Seleccione una clasificación");

            txtNombre.KeyPress += (s, ev) =>
            {
                if (!char.IsLetter(ev.KeyChar) && !char.IsWhiteSpace(ev.KeyChar) && !char.IsControl(ev.KeyChar) && ev.KeyChar != '&')
                    ev.Handled = true;
            };
            txtDireccion.KeyPress += (s, ev) => ClsValidaciones.ValidarBusquedaAlfanumerica(ev);
            txtTelefono.KeyPress += (s, ev) => ClsValidaciones.ValidarSoloNumeros(ev);
            txtRTN.KeyPress += (s, ev) => ClsValidaciones.ValidarSoloNumeros(ev);
        }

        private void btnsalir_Click(object sender, EventArgs e) => this.Close();
        private void btnCancelar_Click(object sender, EventArgs e) => this.Close();
        private void cmbClasificacion_SelectedIndexChanged(object sender, EventArgs e) { }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string nombreReal = phNombre.GetRealValue().Trim();
            string direccionReal = phDireccion.GetRealValue().Trim();
            string telefonoReal = phTelefono.GetRealValue().Trim();
            string rtnReal = phRTN.GetRealValue().Trim();

            if (!ValidarFormulario())
                return;

            try
            {
                if (repositorio.ExisteNombre(nombreReal))
                {
                    MessageBox.Show("El nombre del proveedor ya existe.", "Nombre Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return;
                }

                if (repositorio.ExisteRtn(rtnReal))
                {
                    MessageBox.Show("El RTN ingresado ya pertenece a otro proveedor.", "RTN Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRTN.Focus();
                    return;
                }

                ProveedorDTO proveedorDTO = new ProveedorDTO
                {
                    Nombre = nombreReal,
                    Contacto = telefonoReal,
                    Direccion = direccionReal,
                    Rtn = rtnReal,
                    IdClasificacion = Convert.ToInt32(cmbClasificacion.SelectedValue),
                    IdUsuario = SesionUsuarioService.Instancia.IdUsuario
                };

                repositorio.Agregar(proveedorDTO);

                MessageBox.Show("Proveedor agregado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Valida todos los campos del formulario delegando en ClsValidaciones.
        /// </summary>
        private bool ValidarFormulario()
        {

            string nombreReal = phNombre.GetRealValue().Trim();
            string direccionReal = phDireccion.GetRealValue().Trim();
            string telefonoReal = phTelefono.GetRealValue().Trim();
            string rtnReal = phRTN.GetRealValue().Trim();

            bool valido = true;

            using (var tempNombre = new KryptonTextBox())
            using (var tempDireccion = new KryptonTextBox())
            using (var tempTelefono = new KryptonTextBox())
            using (var tempRTN = new KryptonTextBox())
            {
                tempNombre.Text = nombreReal;
                tempDireccion.Text = direccionReal;
                tempTelefono.Text = telefonoReal;
                tempRTN.Text = rtnReal;

                if (!ClsValidaciones.EsNombrePersonalValido(tempNombre, "Nombre del proveedor"))
                    valido = false;
                else if (!ClsValidaciones.EsAlfanumericoValido(tempDireccion, "Dirección"))
                    valido = false;
                else if (!ClsValidaciones.EsTelefonoHondurasValido(tempTelefono))
                    valido = false;
                else if (!ClsValidaciones.EsRTNValido(tempRTN))
                    valido = false;
            }

            if (!valido) return false;

            if (phClasificacion.IsPlaceholderActive || cmbClasificacion.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar una clasificación.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            if (!char.IsDigit(e.KeyChar)) { e.Handled = true; return; }

            if (txtTelefono.SelectionStart == 0)
            {
                char[] validos = { '2', '3', '8', '9' };
                if (!validos.Contains(e.KeyChar)) e.Handled = true;
            }
        }

        private void txtRTN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '&')
                e.Handled = true;
        }

        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }


    }
}