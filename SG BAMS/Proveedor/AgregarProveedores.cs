using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SG_BAMS.Administracion_de_BAMS.Estado;
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
            int idClasificacion = Convert.ToInt32(cmbClasificacion.SelectedValue);

            int idUsuario = Sesion.IdUsuario;

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
