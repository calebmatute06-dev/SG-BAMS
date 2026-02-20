using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS
{
    public partial class AgregarProducto : Form
    {
        public ClsProducto InfoProducto { get; set; }
        public AgregarProducto()
        {
            InitializeComponent();
        }

        private void kryptonButton20_Click(object sender, EventArgs e)
        {
            InfoProducto = new ClsProducto();

            InfoProducto.ID = txtID.Text;
            InfoProducto.Nombre = txtNombre.Text;
            InfoProducto.Precio = txtPrecio.Text;
            InfoProducto.Marca = cmbMarca.Text;
            InfoProducto.Tipo = cmbTipo.Text;
            InfoProducto.ModeloAuto = cmbModelo.Text;
            InfoProducto.Estado = cmbEstado.Text;
            InfoProducto.Stock = txtStock.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
