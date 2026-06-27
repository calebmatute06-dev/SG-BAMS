using SG_BAMS.Administracion_de_BAMS.Estado;
using SG_BAMS.Login;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SG_BAMS.Proveedor
{
    /// <summary>
    /// Formulario para agregar un nuevo proveedor al sistema.
    /// </summary>
    public partial class AgregarProveedores : Form
    {
        private ClsProveedor proveedor = new ClsProveedor();

        private PlaceholderTextBox phNombre;
        private PlaceholderTextBox phDireccion;
        private PlaceholderTextBox phTelefono;
        private PlaceholderTextBox phRTN;
        private PlaceholderComboBox phClasificacion;

        public AgregarProveedores()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            txtTelefono.MaxLength = 8;
            txtRTN.MaxLength = 14;
            this.txtNombre.KeyPress += new KeyPressEventHandler(this.txtNombre_KeyPress);
            this.txtDireccion.KeyPress += new KeyPressEventHandler(this.txtDireccion_KeyPress);
        }

        private void AgregarProveedores_Load(object sender, EventArgs e)
        {
            proveedor.CargarComboClasificacion(cmbClasificacion);
            
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

        private void cmbClasificacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbClasificacion.SelectedIndex != -1 && cmbClasificacion.SelectedItem is DataRowView drv)
            {
                int idClasificacion = Convert.ToInt32(drv["id_clasificacion_proveedor"]);
                string nombreClasificacion = drv["clasificacion_proveedor"].ToString();
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string nombreReal = phNombre.GetRealValue().Trim();
            string direccionReal = phDireccion.GetRealValue().Trim();
            string telefonoReal = phTelefono.GetRealValue().Trim();
            string rtnReal = phRTN.GetRealValue().Trim();

            if (!ClsValidaciones.EsNombrePersonalValido(new TextBox { Text = nombreReal }, "Nombre del proveedor"))
                return;
            if (ClsValidaciones.CampoVacio(new TextBox { Text = direccionReal }, "Dirección"))
                return;
            if (!ClsValidaciones.EsTelefonoHondurasValido(new TextBox { Text = telefonoReal }))
                return;
            if (!ClsValidaciones.EsRTNValido(new TextBox { Text = rtnReal }))
                return;
            if (phClasificacion.IsPlaceholderActive || cmbClasificacion.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar una clasificación.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (proveedor.ExisteNombreProveedor(nombreReal))
            {
                MessageBox.Show("El nombre del proveedor ya existe.", "Nombre Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }
            if (proveedor.ExisteRtnProveedor(rtnReal))
            {
                MessageBox.Show("El RTN ingresado ya pertenece a otro proveedor.", "RTN Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRTN.Focus();
                return;
            }

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

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            if (!char.IsDigit(e.KeyChar)) { e.Handled = true; return; }
            if (txtTelefono.SelectionStart == 0)
            {
                char[] validos = { '2', '3', '8', '9' };
                if (!validos.Contains(e.KeyChar)) e.Handled = true;
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

        private void label7_Click(object sender, EventArgs e) { }
    }
}