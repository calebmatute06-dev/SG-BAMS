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
    public partial class frmMarcaProductos : Form
    {
        public frmMarcaProductos()
        {
            InitializeComponent();
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            frmModificarMarcaProducto modificarMproducto = new frmModificarMarcaProducto();
            modificarMproducto.Show();
            this.Close();
        }

        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            frmIngresarMarcaProducto agregarMproducto = new frmIngresarMarcaProducto();
            agregarMproducto.Show();
            this.Close();
        }
    }
}
