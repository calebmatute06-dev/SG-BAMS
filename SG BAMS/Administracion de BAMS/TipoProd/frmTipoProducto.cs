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
    public partial class frmTipoProducto : Form
    {
        public frmTipoProducto()
        {
            InitializeComponent();
        }

        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            frnAgregarTipoProducto agregarTProducto = new frnAgregarTipoProducto();
            agregarTProducto.Show();
            this.Close();
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            frmModificarTipoProducto ModificarTProducto = new frmModificarTipoProducto();
            ModificarTProducto.Show();
            this.Close();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
