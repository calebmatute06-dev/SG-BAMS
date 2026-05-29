using SG_BAMS.Administracion_de_BAMS.FormaPago;
using SG_BAMS.Login;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS.Proveedor
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class ModificarProveedor : Form
    {
        /// <summary>
        /// El proveedor
        /// </summary>
        ClsProveedor proveedor = new ClsProveedor();

        /// <summary>
        /// El identificador del estado
        /// </summary>
        private int _idEstado;
        /// <summary>
        /// El identificador de la clasificación
        /// </summary>
        private int _idClasificacion;
        /// <summary>
        /// El nombre original
        /// </summary>
        private string _nombreOriginal;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ModificarProveedor" />.
        /// </summary>
        /// <param name="idProveedor">El identificador del proveedor.</param>
        /// <param name="nombre">El nombre.</param>
        /// <param name="contacto">El contacto.</param>
        /// <param name="direccion">La dirección.</param>
        /// <param name="rtn">El RTN.</param>
        /// <param name="idEstado">El identificador del estado.</param>
        /// <param name="idClasificacion">El identificador de la clasificación.</param>
        public ModificarProveedor(int idProveedor, string nombre, string contacto,
            string direccion, string rtn, int idEstado, int idClasificacion)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            txtID.Text = idProveedor.ToString();
            txtNombre.Text = nombre;
            txtTelefono.Text = contacto;
            txtDireccion.Text = direccion;
            txtRTN.Text = rtn;

            _idEstado = idEstado;
            _idClasificacion = idClasificacion;
            _nombreOriginal = nombre;

            txtTelefono.MaxLength = 8;
            txtRTN.MaxLength = 14;

            this.txtNombre.KeyPress += new KeyPressEventHandler(this.txtNombre_KeyPress);
            this.txtDireccion.KeyPress += new KeyPressEventHandler(this.txtDireccion_KeyPress);
            this.txtTelefono.KeyPress += new KeyPressEventHandler(this.txtTelefono_KeyPress);
            this.txtRTN.KeyPress += new KeyPressEventHandler(this.txtRTN_KeyPress);
        }

        /// <summary>
        /// Maneja el evento Click del control btnsalir.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void btnsalir_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        /// <summary>
        /// Maneja el evento Load del control ModificarProveedor.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void ModificarProveedor_Load(object sender, EventArgs e)
        {
            proveedor.CargarComboEstado(cmbEstado);
            proveedor.CargarComboClasificacion(cmbClasificacion);

            cmbEstado.SelectedValue = _idEstado;
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbClasificacion.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbClasificacion.SelectedValue = _idClasificacion;

            ClsMensajeGuia.Activar(txtNombre);
            ClsMensajeGuia.Activar(txtTelefono);
            ClsMensajeGuia.Activar(txtDireccion);
            ClsMensajeGuia.Activar(txtRTN);
        }

        /// <summary>
        /// Maneja el evento Click del control btnAceptar.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string nombreNuevo = txtNombre.Text.Trim();
            string rtnNuevo = txtRTN.Text.Trim();
            int idProveedor = Convert.ToInt32(txtID.Text);

            if (string.IsNullOrWhiteSpace(nombreNuevo))
            {
                MessageBox.Show("El nombre del proveedor no puede estar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            if (!Regex.IsMatch(nombreNuevo, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ\s&]+$"))
            {
                MessageBox.Show("El nombre solo puede contener letras y el carácter '&'.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }


            if (nombreNuevo.Contains("  "))
            {
                MessageBox.Show("El nombre no puede contener espacios dobles.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }


            if (Regex.IsMatch(nombreNuevo, @"(.)\1{2,}", RegexOptions.IgnoreCase))
            {
                MessageBox.Show("El nombre no puede tener más de dos letras repetidas consecutivamente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }


            if (Regex.IsMatch(nombreNuevo, @"([a-zA-ZñÑáéíóúÁÉÍÓÚ])\s\1", RegexOptions.IgnoreCase))
            {
                MessageBox.Show("El nombre contiene una secuencia de letras repetidas no válida (ejemplo: 'a a').", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }


            if (ClsValidaciones.CampoVacio(txtDireccion, "Dirección")) return;


            if (txtDireccion.Text.Contains("  "))
            {
                MessageBox.Show("La dirección no puede contener espacios dobles.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDireccion.Focus();
                return;
            }

            if (!ClsValidaciones.EsTelefonoHondurasValido(txtTelefono)) return;
            if (!ClsValidaciones.EsRTNValido(txtRTN)) return;

            if (cmbEstado.SelectedValue == null || cmbClasificacion.SelectedValue == null)
            {
                MessageBox.Show("Asegúrese de seleccionar el Estado y la Clasificación.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (nombreNuevo != _nombreOriginal && proveedor.ExisteNombreProveedor(nombreNuevo))
            {
                MessageBox.Show("El nuevo nombre ya pertenece a otro proveedor.", "Nombre Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }


            if (proveedor.ExisteRtnProveedorModificar(rtnNuevo, idProveedor))
            {
                MessageBox.Show("El RTN ingresado ya pertenece a otro proveedor registrado.", "RTN Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRTN.Focus();
                return;
            }

            try
            {
                int idEstado = Convert.ToInt32(cmbEstado.SelectedValue);
                int idClasificacion = Convert.ToInt32(cmbClasificacion.SelectedValue);
                int idUsuario = new ClsPasarUsuario().IdUsuario();

                proveedor.ModificarProveedor(
                    idProveedor,
                    nombreNuevo,
                    txtTelefono.Text.Trim(),
                    txtDireccion.Text.Trim(),
                    rtnNuevo,
                    idEstado,
                    idClasificacion,
                    idUsuario
                );

                MessageBox.Show("Proveedor modificado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ProveedoresAdmin admin = new ProveedoresAdmin();
                admin.Show();
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Maneja el evento KeyPress del control txtNombre.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="KeyPressEventArgs" /> que contiene los datos del evento.</param>
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
        /// <param name="e">La instancia <see cref="KeyPressEventArgs" /> que contiene los datos del evento.</param>
        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Maneja el evento KeyPress del control txtTelefono.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="KeyPressEventArgs" /> que contiene los datos del evento.</param>
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
        /// <param name="e">La instancia <see cref="KeyPressEventArgs" /> que contiene los datos del evento.</param>
        private void txtRTN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Maneja el evento SelectedIndexChanged del control cmbEstado.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void cmbEstado_SelectedIndexChanged(object sender, EventArgs e) { }
        /// <summary>
        /// Maneja el evento SelectedIndexChanged del control cmbClasificacion.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void cmbClasificacion_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}