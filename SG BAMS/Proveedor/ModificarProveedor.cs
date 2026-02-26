using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SG_BAMS.Administracion_de_BAMS.FormaPago;

namespace SG_BAMS.Proveedor
{
    public partial class ModificarProveedor : Form
    {
        ClsProveedor proveedor = new ClsProveedor();

        public ModificarProveedor()
        {
            InitializeComponent();
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
            //proveedor.CargarComboClasificacion(cmbClasificacion);
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
            /*
            if (cmbEstado.SelectedIndex != -1 && cmbEstado.SelectedItem is DataRowView)
            {
                DataRowView drv = (DataRowView)cmbEstado.SelectedItem;
                int idEstado = Convert.ToInt32(drv["id_estado"]);
                string nombreEstado = drv["descripcion_estado"].ToString();
            }
            */
        }
    }
}
