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
    public partial class frnAgregarTipoProducto : Form
    {
        public frnAgregarTipoProducto()
        {
            InitializeComponent();
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            frmTipoProducto verTproductos = new frmTipoProducto();
            verTproductos.Show();
            this.Close();
        }

        private void kryptonButton6_Click(object sender, EventArgs e)
        {

        }
    }
}
