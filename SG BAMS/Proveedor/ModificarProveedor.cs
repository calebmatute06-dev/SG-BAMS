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
    public partial class ModificarProveedor : Form
    {
        ClsProveedor proveedor = new ClsProveedor();

        private int _idEstado;
        private int _idClasificacion;
        private string _nombreOriginal;

        public ModificarProveedor(int idProveedor, string nombre, string contacto,
            string direccion, string rtn, int idEstado, int idClasificacion)
        {
            InitializeComponent();

            txtID.Text = idProveedor.ToString();
            txtNombre.Text = nombre;
            txtTelefono.Text = contacto;
            txtDireccion.Text = direccion;
            txtRTN.Text = rtn;

            _idEstado = idEstado;
            _idClasificacion = idClasificacion;
            _nombreOriginal = nombre; // IMPORTANTE: Guardar el nombre original para la validación de duplicados

            txtTelefono.MaxLength = 8;
            txtRTN.MaxLength = 14;

            // Asignación de eventos KeyPress
            this.txtNombre.KeyPress += new KeyPressEventHandler(this.txtNombre_KeyPress);
            this.txtDireccion.KeyPress += new KeyPressEventHandler(this.txtDireccion_KeyPress);
            this.txtTelefono.KeyPress += new KeyPressEventHandler(this.txtTelefono_KeyPress);
            this.txtRTN.KeyPress += new KeyPressEventHandler(this.txtRTN_KeyPress);
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            ProveedoresAdmin admin = new ProveedoresAdmin();
            admin.Show();
            this.Close();
        }

        private void ModificarProveedor_Load(object sender, EventArgs e)
        {
            proveedor.CargarComboEstado(cmbEstado);
            proveedor.CargarComboClasificacion(cmbClasificacion);

            cmbEstado.SelectedValue = _idEstado;
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbClasificacion.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbClasificacion.SelectedValue = _idClasificacion;

            // Validaciones en tiempo real (KeyPress dinámicos)
            txtNombre.KeyPress += (s, ev) => ClsValidaciones.PermitirSoloLetras(ev);
            txtDireccion.KeyPress += (s, ev) => ClsValidaciones.ValidarBusquedaAlfanumerica(ev);
            txtTelefono.KeyPress += (s, ev) => ClsValidaciones.ValidarSoloNumeros(ev);
            txtRTN.KeyPress += (s, ev) => ClsValidaciones.ValidarSoloNumeros(ev);
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // 1. Validaciones de Texto
            if (!ClsValidaciones.EsNombrePersonalValido(txtNombre, "Nombre del Proveedor")) return;
            if (ClsValidaciones.CampoVacio(txtDireccion, "Dirección")) return;

            // 2. Validación de Teléfono
            if (!ClsValidaciones.EsTelefonoHondurasValido(txtTelefono)) return;

            // 3. Validación de RTN (Centralizada)
            if (!ClsValidaciones.EsRTNValido(txtRTN)) return;

            // 4. Validación de Combos
            if (cmbEstado.SelectedValue == null || cmbClasificacion.SelectedValue == null)
            {
                MessageBox.Show("Asegúrese de seleccionar el Estado y la Clasificación.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 5. Validación de Duplicados (Solo si el nombre cambió)
            string nombreNuevo = txtNombre.Text.Trim();
            if (nombreNuevo != _nombreOriginal && proveedor.ExisteNombreProveedor(nombreNuevo))
            {
                MessageBox.Show("El nuevo nombre ya pertenece a otro proveedor.", "Nombre Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            try
            {
                int idProveedor = Convert.ToInt32(txtID.Text);
                int idEstado = Convert.ToInt32(cmbEstado.SelectedValue);
                int idClasificacion = Convert.ToInt32(cmbClasificacion.SelectedValue);
                int idUsuario = new ClsPasarUsuario().IdUsuario();

                proveedor.ModificarProveedor(
                    idProveedor,
                    nombreNuevo,
                    txtTelefono.Text.Trim(),
                    txtDireccion.Text.Trim(),
                    txtRTN.Text.Trim(),
                    idEstado,
                    idClasificacion,
                    idUsuario
                );

                MessageBox.Show("Proveedor modificado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Navegación de regreso
                ProveedoresAdmin admin = new ProveedoresAdmin();
                admin.Show();
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- Eventos KeyPress manuales (Manteniendo compatibilidad) ---

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '&')
            {
                e.Handled = true;
            }
        }

        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
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
                {
                    e.Handled = true;
                }
            }
        }

        private void txtRTN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void cmbEstado_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmbClasificacion_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}