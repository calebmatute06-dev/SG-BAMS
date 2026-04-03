using SG_BAMS.Administracion_de_BAMS.Estado;
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
using static System.Collections.Specialized.BitVector32;

namespace SG_BAMS.Proveedor
{
    public partial class AgregarProveedores : Form
    {
        ClsProveedor proveedor = new ClsProveedor();

        public AgregarProveedores()
        {
            InitializeComponent();
            txtTelefono.MaxLength = 8;
            txtRTN.MaxLength = 14;
            this.txtNombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombre_KeyPress);
            this.txtDireccion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDireccion_KeyPress);
        }

        private void AgregarProveedores_Load(object sender, EventArgs e)
        {
            proveedor.CargarComboClasificacion(cmbClasificacion);
            cmbClasificacion.DropDownStyle = ComboBoxStyle.DropDownList;

            
            txtNombre.KeyPress += (s, ev) => ClsValidaciones.PermitirAlfanumerico(ev);
            txtDireccion.KeyPress += (s, ev) => ClsValidaciones.ValidarBusquedaAlfanumerica(ev);
            txtTelefono.KeyPress += (s, ev) => ClsValidaciones.ValidarSoloNumeros(ev);
            txtRTN.KeyPress += (s, ev) => ClsValidaciones.ValidarSoloNumeros(ev);
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbClasificacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbClasificacion.SelectedIndex != -1 && cmbClasificacion.SelectedItem is DataRowView)
            {
                DataRowView drv = (DataRowView)cmbClasificacion.SelectedItem;
                int idClasificacion = Convert.ToInt32(drv["id_clasificacion_proveedor"]);
                string nombreClasificacion = drv["clasificacion_proveedor"].ToString();
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();

            
            if (!ClsValidaciones.EsAlfanumericoValido(txtNombre, "Nombre del Proveedor")) return;

            if (ClsValidaciones.CampoVacio(txtDireccion, "Dirección")) return;
            if (!ClsValidaciones.EsTelefonoHondurasValido(txtTelefono)) return;
            if (!ClsValidaciones.EsRTNValido(txtRTN)) return;

            if (cmbClasificacion.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar una clasificación.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (proveedor.ExisteNombreProveedor(nombre))
            {
                MessageBox.Show("El nombre del proveedor ya existe.", "Nombre Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            try
            {
                int idClasificacion = Convert.ToInt32(cmbClasificacion.SelectedValue);
                int idUsuario = new ClsPasarUsuario().IdUsuario();

                proveedor.AgregarProveedor(
                    nombre,
                    txtTelefono.Text.Trim(),
                    txtDireccion.Text.Trim(),
                    txtRTN.Text.Trim(),
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
            ClsValidaciones.ValidarSoloNumeros(e);
        }

        private void txtRTN_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.ValidarSoloNumeros(e);
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.PermitirAlfanumerico(e);
        }

        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.ValidarBusquedaAlfanumerica(e);
        }

        private void label7_Click(object sender, EventArgs e)
        {
           
        }
    }
}