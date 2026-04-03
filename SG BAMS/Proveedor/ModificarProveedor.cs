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
            _nombreOriginal = nombre;

            txtTelefono.MaxLength = 8;
            txtRTN.MaxLength = 14;

            this.txtNombre.KeyPress += new KeyPressEventHandler(this.txtNombre_KeyPress);
            this.txtDireccion.KeyPress += new KeyPressEventHandler(this.txtDireccion_KeyPress);
            this.txtTelefono.KeyPress += new KeyPressEventHandler(this.txtTelefono_KeyPress);
            this.txtRTN.KeyPress += new KeyPressEventHandler(this.txtRTN_KeyPress);
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
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

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string nombreNuevo = txtNombre.Text.Trim();

           
            if (!ClsValidaciones.EsAlfanumericoValido(txtNombre, "Nombre del Proveedor")) return;

            if (ClsValidaciones.CampoVacio(txtDireccion, "Dirección")) return;
            
            if (!ClsValidaciones.EsAlfanumericoValido(txtDireccion, "Dirección")) return;

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

                ProveedoresAdmin admin = new ProveedoresAdmin();
                admin.Show();
                this.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.PermitirAlfanumerico(e);
        }

        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.ValidarBusquedaAlfanumerica(e);
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            ClsValidaciones.ValidarSoloNumeros(e);
        }

        private void txtRTN_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.ValidarSoloNumeros(e);
        }

        private void cmbEstado_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmbClasificacion_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}