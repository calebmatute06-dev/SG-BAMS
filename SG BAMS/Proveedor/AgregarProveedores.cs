using SG_BAMS.Administracion_de_BAMS.Estado;
using SG_BAMS.Login;
using System;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SG_BAMS.Proveedor
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class AgregarProveedores : Form
    {
        /// <summary>
        /// El proveedor
        /// </summary>
        ClsProveedor proveedor = new ClsProveedor();

        private PlaceholderTextBox phNombre;
        private PlaceholderTextBox phDireccion;
        private PlaceholderTextBox phTelefono;
        private PlaceholderTextBox phRTN;
        private PlaceholderComboBox phClasificacion;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="AgregarProveedores"/>.
        /// </summary>
        public AgregarProveedores()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            txtTelefono.MaxLength = 8;
            txtRTN.MaxLength = 14;
            this.txtNombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombre_KeyPress);
            this.txtDireccion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDireccion_KeyPress);

            txtNombre.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
            txtDireccion.KeyPress += (s, e) => ClsValidaciones.PermitirAlfanumerico(e);
            txtTelefono.KeyPress += (s, e) => ClsValidaciones.ValidarSoloNumeros(e);
            txtRTN.KeyPress += (s, e) => ClsValidaciones.ValidarSoloNumeros(e);
            txtTelefono.KeyPress += (s, e) =>
                ClsValidaciones.ValidarTelefonoKeyPress(txtTelefono, e);
        }

        /// <summary>
        /// Maneja el evento Load del control AgregarProveedores.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void AgregarProveedores_Load(object sender, EventArgs e)
        {
            proveedor.CargarComboClasificacion(cmbClasificacion);
            cmbClasificacion.DropDownStyle = ComboBoxStyle.DropDownList;

            txtNombre.KeyPress += (s, ev) =>
            {
                if (!char.IsLetter(ev.KeyChar) && !char.IsWhiteSpace(ev.KeyChar) && !char.IsControl(ev.KeyChar) && ev.KeyChar != '&')
                {
                    ev.Handled = true;
                }
            };
            txtDireccion.KeyPress += (s, ev) => ClsValidaciones.ValidarBusquedaAlfanumerica(ev);
            txtTelefono.KeyPress += (s, ev) => ClsValidaciones.ValidarSoloNumeros(ev);
            txtRTN.KeyPress += (s, ev) => ClsValidaciones.ValidarSoloNumeros(ev);

            // Inicializar placeholders después de cargar el combo
            phNombre = new PlaceholderTextBox(txtNombre, "Nombre del proveedor");
            phDireccion = new PlaceholderTextBox(txtDireccion, "Colonia, Barrio, Pueblo");
            phTelefono = new PlaceholderTextBox(txtTelefono, "Número que empiece con 9,8,3,2");
            phRTN = new PlaceholderTextBox(txtRTN, "Ingrese el RTN");
            phClasificacion = new PlaceholderComboBox(cmbClasificacion, "Seleccione una clasificación");
        }

        /// <summary>
        /// Maneja el evento Click del control btnsalir.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Maneja el evento SelectedIndexChanged del control cmbClasificacion.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void cmbClasificacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbClasificacion.SelectedIndex != -1 && cmbClasificacion.SelectedItem is DataRowView)
            {
                DataRowView drv = (DataRowView)cmbClasificacion.SelectedItem;
                int idClasificacion = Convert.ToInt32(drv["id_clasificacion_proveedor"]);
                string nombreClasificacion = drv["clasificacion_proveedor"].ToString();
            }
        }

        /// <summary>
        /// Maneja el evento Click del control btnAceptar.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Obtener valores reales (sin placeholder)
            string nombreReal = phNombre.GetRealValue().Trim();
            string direccionReal = phDireccion.GetRealValue().Trim();
            string telefonoReal = phTelefono.GetRealValue().Trim();
            string rtnReal = phRTN.GetRealValue().Trim();

            // Guardar textos originales
            string originalNombre = txtNombre.Text;
            string originalDireccion = txtDireccion.Text;
            string originalTelefono = txtTelefono.Text;
            string originalRTN = txtRTN.Text;

            // Asignar valores reales temporalmente para que las validaciones funcionen
            txtNombre.Text = nombreReal;
            txtDireccion.Text = direccionReal;
            txtTelefono.Text = telefonoReal;
            txtRTN.Text = rtnReal;

            bool valido = true;

            // Validaciones manuales del nombre
            if (string.IsNullOrWhiteSpace(nombreReal))
            {
                MessageBox.Show("El nombre del proveedor no puede estar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                valido = false;
            }
            else if (!Regex.IsMatch(nombreReal, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ\s&]+$"))
            {
                MessageBox.Show("El nombre solo puede contener letras y el carácter '&'.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                valido = false;
            }
            else if (nombreReal.Contains("  "))
            {
                MessageBox.Show("El nombre no puede contener espacios dobles.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                valido = false;
            }
            else if (Regex.IsMatch(nombreReal, @"(.)\1{2,}", RegexOptions.IgnoreCase))
            {
                MessageBox.Show("El nombre no puede tener más de dos letras repetidas consecutivamente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                valido = false;
            }
            else if (Regex.IsMatch(nombreReal, @"([a-zA-ZñÑáéíóúÁÉÍÓÚ])\s\1", RegexOptions.IgnoreCase))
            {
                MessageBox.Show("El nombre contiene una secuencia de letras repetidas no válida (ejemplo: 'a a').", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                valido = false;
            }
            else if (nombreReal.Length >= 2 && nombreReal[0] == nombreReal[1])
            {
                MessageBox.Show("El nombre no puede iniciar con dos letras iguales.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                valido = false;
            }

            // Validaciones usando ClsValidaciones
            if (valido && ClsValidaciones.CampoVacio(txtDireccion, "Dirección"))
                valido = false;
            else if (valido && !ClsValidaciones.EsTelefonoHondurasValido(txtTelefono))
                valido = false;
            else if (valido && !ClsValidaciones.EsRTNValido(txtRTN))
                valido = false;
            else if (valido && (phClasificacion.IsPlaceholderActive || cmbClasificacion.SelectedValue == null))
            {
                MessageBox.Show("Debe seleccionar una clasificación.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                valido = false;
            }

            // Validaciones de duplicados
            if (valido && proveedor.ExisteNombreProveedor(nombreReal))
            {
                MessageBox.Show("El nombre del proveedor ya existe.", "Nombre Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                valido = false;
            }

            if (valido && proveedor.ExisteRtnProveedor(rtnReal))
            {
                MessageBox.Show("El RTN ingresado ya pertenece a otro proveedor.", "RTN Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRTN.Focus();
                valido = false;
            }

            // Restaurar textos originales
            txtNombre.Text = originalNombre;
            txtDireccion.Text = originalDireccion;
            txtTelefono.Text = originalTelefono;
            txtRTN.Text = originalRTN;

            if (!valido) return;

            try
            {
                int idClasificacion = Convert.ToInt32(cmbClasificacion.SelectedValue);
                int idUsuario = new ClsPasarUsuario().IdUsuario();

                proveedor.AgregarProveedor(
                    nombreReal,
                    telefonoReal,
                    direccionReal,
                    rtnReal,
                    idClasificacion,
                    idUsuario
                );

                MessageBox.Show("Proveedor agregado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ProveedoresAdmin admin = new ProveedoresAdmin();
                admin.Show();
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Maneja el evento KeyPress del control txtTelefono.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="KeyPressEventArgs"/> que contiene los datos del evento.</param>
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
                {
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// Maneja el evento KeyPress del control txtRTN.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="KeyPressEventArgs"/> que contiene los datos del evento.</param>
        private void txtRTN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Maneja el evento KeyPress del control txtNombre.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="KeyPressEventArgs"/> que contiene los datos del evento.</param>
        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '&')
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Maneja el evento KeyPress del control txtDireccion.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="KeyPressEventArgs"/> que contiene los datos del evento.</param>
        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Maneja el evento Click del control label7.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void label7_Click(object sender, EventArgs e)
        {
        }
    }
}