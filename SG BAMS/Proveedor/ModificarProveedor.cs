using SG_BAMS.Administracion_de_BAMS.FormaPago;
using SG_BAMS.Login;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            txtTelefono.MaxLength = 8;
            txtRTN.MaxLength = 14;


           

            this.txtNombre.KeyPress += new KeyPressEventHandler(this.txtNombre_KeyPress);
            this.txtDireccion.KeyPress += new KeyPressEventHandler(this.txtDireccion_KeyPress);
            this.txtTelefono.KeyPress += new KeyPressEventHandler(this.txtTelefono_KeyPress);
            this.txtRTN.KeyPress += new KeyPressEventHandler(this.txtRTN_KeyPress);

            txtNombre.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
            txtDireccion.KeyPress += (s, e) => ClsValidaciones.ValidarBusquedaAlfanumerica(e);
            txtTelefono.KeyPress += (s, e) => ClsValidaciones.ValidarSoloNumeros(e);
            txtRTN.KeyPress += (s, e) => ClsValidaciones.ValidarSoloNumeros(e);
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            ProveedoresAdmin proveedor = new ProveedoresAdmin();
            proveedor.Show();
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
        }

        private void cmbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEstado.SelectedIndex != -1 && cmbEstado.SelectedItem is DataRowView)
            {
                DataRowView drv = (DataRowView)cmbEstado.SelectedItem;
                int idEstado = Convert.ToInt32(drv["id_estado"]);
                string nombreEstado = drv["descripcion_estado"].ToString();
            }

        }

        private void cmbClasificacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEstado.SelectedIndex != -1 && cmbEstado.SelectedItem is DataRowView)
            {
                DataRowView drv = (DataRowView)cmbEstado.SelectedItem;
                int idEstado = Convert.ToInt32(drv["id_estado"]);
                string nombreEstado = drv["descripcion_estado"].ToString();
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            
            if (!ClsValidaciones.EsNombrePersonalValido(txtNombre, "Nombre del Proveedor")) return;
            if (ClsValidaciones.CampoVacio(txtDireccion, "Dirección")) return;

            
            if (!ClsValidaciones.EsTelefonoHondurasValido(txtTelefono)) return;

            
            if (txtRTN.Text.Trim().Length != 14)
            {
                MessageBox.Show("El RTN debe tener exactamente 14 dígitos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRTN.Focus();
                return;
            }

            
            if (cmbEstado.SelectedValue == null || cmbClasificacion.SelectedValue == null)
            {
                MessageBox.Show("Asegúrese de seleccionar el Estado y la Clasificación.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 5. VALIDACIÓN DE DUPLICADOS (Solo si el nombre cambió)
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
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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
    }
}
