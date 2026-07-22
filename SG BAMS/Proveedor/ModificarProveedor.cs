using SG_BAMS.Administracion_de_BAMS.FormaPago;
using SG_BAMS.Login;
using SG_BAMS.Proveedor.DTO;
using System;
using System.Data;
using System.Windows.Forms;

namespace SG_BAMS.Proveedor
{
    /// <summary>
    /// Formulario para modificar los datos de un proveedor existente.
    /// </summary>
    public partial class ModificarProveedor : Form
    {
        private readonly IProveedorRepository repositorio;
        private readonly IEstadoRepository estadoRepositorio;
        private readonly IClasificacionRepository clasificacionRepositorio;

        private ProveedorDTO proveedorDTO;
        private string nombreOriginal;

        private PlaceholderTextBox phNombre;
        private PlaceholderTextBox phTelefono;
        private PlaceholderTextBox phDireccion;
        private PlaceholderTextBox phRTN;
        private PlaceholderComboBox phEstado;
        private PlaceholderComboBox phClasificacion;

        /// <summary>
        /// Constructor del formulario de modificación de proveedor.
        /// </summary>
        /// <param name="dto">DTO con los datos del proveedor a modificar.</param>
        /// <param name="repositorio">Repositorio de proveedores.</param>
        /// <param name="estadoRepositorio">Repositorio de estados.</param>
        /// <param name="clasificacionRepositorio">Repositorio de clasificaciones.</param>
        public ModificarProveedor(ProveedorDTO dto,
                                   IProveedorRepository repositorio,
                                   IEstadoRepository estadoRepositorio,
                                   IClasificacionRepository clasificacionRepositorio)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            this.repositorio = repositorio;
            this.estadoRepositorio = estadoRepositorio;
            this.clasificacionRepositorio = clasificacionRepositorio;

            proveedorDTO = dto;

            txtID.Text = dto.IdProveedor.ToString();
            txtNombre.Text = dto.Nombre;
            txtTelefono.Text = dto.Contacto;
            txtDireccion.Text = dto.Direccion;
            txtRTN.Text = dto.Rtn;
            nombreOriginal = dto.Nombre;

            txtTelefono.MaxLength = 8;
            txtRTN.MaxLength = 14;

            this.txtNombre.KeyPress += new KeyPressEventHandler(this.txtNombre_KeyPress);
            this.txtDireccion.KeyPress += new KeyPressEventHandler(this.txtDireccion_KeyPress);
            this.txtTelefono.KeyPress += new KeyPressEventHandler(this.txtTelefono_KeyPress);
            this.txtRTN.KeyPress += new KeyPressEventHandler(this.txtRTN_KeyPress);

            txtNombre.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
            txtTelefono.KeyPress += (s, e) => ClsValidaciones.ValidarSoloNumeros(e);
            txtRTN.KeyPress += (s, e) => ClsValidaciones.ValidarSoloNumeros(e);
            txtTelefono.KeyPress += (s, e) => ClsValidaciones.ValidarTelefonoKeyPress(e);
        }

        private void btnCancelar_Click(object sender, EventArgs e) => this.Close();

        /// <summary>
        /// Evento Load del formulario. Carga los catálogos de estados y clasificaciones.
        /// </summary>
        private void ModificarProveedor_Load(object sender, EventArgs e)
        {
            DataTable estados = estadoRepositorio.ObtenerEstados();
            cmbEstado.DataSource = estados;
            cmbEstado.DisplayMember = "descripcion_estado";
            cmbEstado.ValueMember = "id_estado";

            DataTable clasificaciones = clasificacionRepositorio.ObtenerClasificaciones();
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

        /// <summary>
        /// Evento Click del botón Aceptar. Valida y guarda los cambios del proveedor.
        /// </summary>
        private void btnAceptar_Click_1(object sender, EventArgs e)
        {
            string nombreReal = phNombre.GetRealValue().Trim();
            string telefonoReal = phTelefono.GetRealValue().Trim();
            string direccionReal = phDireccion.GetRealValue().Trim();
            string rtnReal = phRTN.GetRealValue().Trim();
            int idProveedor = Convert.ToInt32(txtID.Text);

            if (!ValidarFormulario())
                return;

            try
            {
                if (nombreReal != nombreOriginal && repositorio.ExisteNombre(nombreReal))
                {
                    MessageBox.Show("El nuevo nombre ya pertenece a otro proveedor.", "Nombre Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return;
                }

                if (repositorio.ExisteRtnExcluyendo(rtnReal, idProveedor))
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
                proveedorDTO.IdUsuario = SesionUsuarioService.Instancia.IdUsuario;

                repositorio.Modificar(proveedorDTO);

                MessageBox.Show("Proveedor modificado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Valida todos los campos del formulario delegando en ClsValidaciones.
        /// </summary>
        private bool ValidarFormulario()
        {
            if (ClsValidaciones.CampoVacio(txtNombre, "Nombre del proveedor"))
                return false;

            if (ClsValidaciones.CampoVacio(txtDireccion, "Dirección"))
                return false;

            if (ClsValidaciones.CampoVacio(txtTelefono, "Teléfono"))
                return false;

            if (ClsValidaciones.CampoVacio(txtRTN, "RTN"))
                return false;

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
        }

        private void txtRTN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }


    }
}