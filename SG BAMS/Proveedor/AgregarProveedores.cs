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

        }

        private void AgregarProveedores_Load(object sender, EventArgs e)
        {
            proveedor.CargarComboClasificacion(cmbClasificacion);
            cmbClasificacion.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            ProveedoresAdmin proveedor = new ProveedoresAdmin();
            proveedor.Show();
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

            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono.Text) ||
                string.IsNullOrWhiteSpace(txtDireccion.Text) ||
                string.IsNullOrWhiteSpace(txtRTN.Text) ||
                cmbClasificacion.SelectedValue == null)
            {
                MessageBox.Show("Debe llenar todos los campos obligatorios antes de continuar.",
                                "Campos Vacíos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            else if (!Regex.IsMatch(txtNombre.Text, @"^[a-zA-Z\sñÑ&]+$"))
            {
                MessageBox.Show("El campo de Nombre solo deben contener letras.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            else if (!Regex.IsMatch(txtTelefono.Text, @"^[0-9]+$") || !Regex.IsMatch(txtRTN.Text, @"^[0-9]+$"))
            {
                MessageBox.Show("Los campos de Teléfono o RTN solo deben contener números.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                int idClasificacion = Convert.ToInt32(cmbClasificacion.SelectedValue);

                int idUsuario = new ClsPasarUsuario().IdUsuario();

                proveedor.AgregarProveedor(
                    txtNombre.Text.Trim(),
                    txtTelefono.Text.Trim(),
                    txtDireccion.Text.Trim(),
                    txtRTN.Text.Trim(),
                    idClasificacion,
                    idUsuario
                );

                ProveedoresAdmin proveedorAdmin = new ProveedoresAdmin();
                proveedorAdmin.Show();
                this.Close();
            }
            
        }
    }
}
