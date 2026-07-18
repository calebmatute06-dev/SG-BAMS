using SG_BAMS.Administracion_de_BAMS.FormaPago;
using SG_BAMS.Login;
using SG_BAMS.Proveedor.DTO;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SG_BAMS.Proveedor
{
    public partial class ModificarProveedor : Form
    {
        private readonly IProveedorRepository _repositorio;
        private readonly IEstadoRepository _estadoRepositorio;
        private readonly IClasificacionRepository _clasificacionRepositorio;

        private ProveedorDTO proveedorDTO;

        private string _nombreOriginal;

        private PlaceholderTextBox phNombre;
        private PlaceholderTextBox phTelefono;
        private PlaceholderTextBox phDireccion;
        private PlaceholderTextBox phRTN;
        private PlaceholderComboBox phEstado;
        private PlaceholderComboBox phClasificacion;

        public ModificarProveedor(ProveedorDTO dto,
                                   IProveedorRepository repositorio,
                                   IEstadoRepository estadoRepositorio,
                                   IClasificacionRepository clasificacionRepositorio)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            _repositorio = repositorio;
            _estadoRepositorio = estadoRepositorio;
            _clasificacionRepositorio = clasificacionRepositorio;

            proveedorDTO = dto;

            txtID.Text = dto.IdProveedor.ToString();
            txtNombre.Text = dto.Nombre;
            txtTelefono.Text = dto.Contacto;
            txtDireccion.Text = dto.Direccion;
            txtRTN.Text = dto.Rtn;
            _nombreOriginal = dto.Nombre;

            txtTelefono.MaxLength = 8;
            txtRTN.MaxLength = 14;

            this.txtNombre.KeyPress += new KeyPressEventHandler(this.txtNombre_KeyPress);
            this.txtDireccion.KeyPress += new KeyPressEventHandler(this.txtDireccion_KeyPress);
            this.txtTelefono.KeyPress += new KeyPressEventHandler(this.txtTelefono_KeyPress);
            this.txtRTN.KeyPress += new KeyPressEventHandler(this.txtRTN_KeyPress);

            txtNombre.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
            txtNombre.KeyPress += (s, e) => ClsValidaciones.PermitirAlfanumerico(e);
            txtTelefono.KeyPress += (s, e) => ClsValidaciones.ValidarSoloNumeros(e);
            txtRTN.KeyPress += (s, e) => ClsValidaciones.ValidarSoloNumeros(e);
            txtTelefono.KeyPress += (s, e) => ClsValidaciones.ValidarTelefonoKeyPress(e);
        }

        private void btnCancelar_Click(object sender, EventArgs e) => this.Close();

        private void ModificarProveedor_Load(object sender, EventArgs e)
        {
            DataTable estados = _estadoRepositorio.ObtenerEstados();
            cmbEstado.DataSource = estados;
            cmbEstado.DisplayMember = "descripcion_estado";
            cmbEstado.ValueMember = "id_estado";

            DataTable clasificaciones = _clasificacionRepositorio.ObtenerClasificaciones();
            cmbClasificacion.DataSource = clasificaciones;
            cmbClasificacion.DisplayMember = "clasificacion_proveedor";
            cmbClasificacion.ValueMember = "id_clasificacion_proveedor";

            cmbEstado.DropDownStyle = ComboBoxStyle.DropDown;
            cmbClasificacion.DropDownStyle = ComboBoxStyle.DropDown;
            cmbEstado.SelectedValue = proveedorDTO.IdEstado;
            cmbClasificacion.SelectedValue = proveedorDTO.IdClasificacion;

            phNombre = new PlaceholderTextBox(txtNombre, "Ingrese el Nombre del proveedor");
            phTelefono = new PlaceholderTextBox(txtTelefono, "Número que empiece con 9,8,3,2");
            phDireccion = new PlaceholderTextBox(txtDireccion, "Colonia, Barrio, Pueblo");
            phRTN = new PlaceholderTextBox(txtRTN, "Ingrese el RTN");
            phEstado = new PlaceholderComboBox(cmbEstado, "Seleccione un estado");
            phClasificacion = new PlaceholderComboBox(cmbClasificacion, "Seleccione una clasificación");
        }

        private void btnAceptar_Click_1(object sender, EventArgs e)
        {
            string nombreReal = phNombre.GetRealValue().Trim();
            string telefonoReal = phTelefono.GetRealValue().Trim();
            string direccionReal = phDireccion.GetRealValue().Trim();
            string rtnReal = phRTN.GetRealValue().Trim();
            int idProveedor = Convert.ToInt32(txtID.Text);

            if (!ValidarFormato(nombreReal, direccionReal, telefonoReal, rtnReal))
                return;

            try
            {
                if (nombreReal != _nombreOriginal && _repositorio.ExisteNombre(nombreReal))
                {
                    MessageBox.Show("El nuevo nombre ya pertenece a otro proveedor.", "Nombre Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return;
                }

                if (_repositorio.ExisteRtnExcluyendo(rtnReal, idProveedor))
                {
                    MessageBox.Show("El RTN ingresado ya pertenece a otro proveedor registrado.", "RTN Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRTN.Focus();
                    return;
                }

                proveedorDTO.Nombre = nombreReal;
                proveedorDTO.Contacto = telefonoReal;
                proveedorDTO.Direccion = direccionReal;
                proveedorDTO.Rtn = rtnReal;
                proveedorDTO.IdEstado = Convert.ToInt32(cmbEstado.SelectedValue);
                proveedorDTO.IdClasificacion = Convert.ToInt32(cmbClasificacion.SelectedValue);
                proveedorDTO.IdUsuario = ClsLogin.idusuario;

                _repositorio.Modificar(proveedorDTO);

                MessageBox.Show("Proveedor modificado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarFormato(string nombre, string direccion, string telefono, string rtn)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("El nombre del proveedor no puede estar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (!Regex.IsMatch(nombre, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ\s&]+$"))
            {
                MessageBox.Show("El nombre solo puede contener letras y el carácter '&'.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (nombre.Contains("  "))
            {
                MessageBox.Show("El nombre no puede contener espacios dobles.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (Regex.IsMatch(nombre, @"(.)\1{2,}", RegexOptions.IgnoreCase))
            {
                MessageBox.Show("El nombre no puede tener más de dos letras repetidas consecutivamente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (Regex.IsMatch(nombre, @"([a-zA-ZñÑáéíóúÁÉÍÓÚ])\s\1", RegexOptions.IgnoreCase))
            {
                MessageBox.Show("El nombre contiene una secuencia de letras repetidas no válida (ejemplo: 'a a').", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (ClsValidaciones.CampoVacio(new TextBox { Text = direccion }, "Dirección")) return false;

            if (direccion.Contains("  "))
            {
                MessageBox.Show("La dirección no puede contener espacios dobles.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDireccion.Focus();
                return false;
            }

            if (!ClsValidaciones.EsTelefonoHondurasValido(new TextBox { Text = telefono })) return false;
            if (!ClsValidaciones.EsRTNValido(new TextBox { Text = rtn })) return false;

            if (phEstado.IsPlaceholderActive || cmbEstado.SelectedValue == null ||
                phClasificacion.IsPlaceholderActive || cmbClasificacion.SelectedValue == null)
            {
                MessageBox.Show("Asegúrese de seleccionar el Estado y la Clasificación.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
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

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            if (!char.IsDigit(e.KeyChar)) { e.Handled = true; return; }

            if (txtTelefono.SelectionStart == 0)
            {
                char[] validos = { '2', '3', '8', '9' };
                if (!validos.Contains(e.KeyChar)) { e.Handled = true; return; }
            }

            if (txtTelefono.Text.Length >= 3)
            {
                int pos = txtTelefono.SelectionStart;
                string t = txtTelefono.Text;
                if (pos >= 3 && t[pos - 1] == e.KeyChar && t[pos - 2] == e.KeyChar && t[pos - 3] == e.KeyChar)
                    e.Handled = true;
            }
        }

        private void txtRTN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void cmbEstado_SelectedIndexChanged(object sender, EventArgs e) { }

        private void cmbClasificacion_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}