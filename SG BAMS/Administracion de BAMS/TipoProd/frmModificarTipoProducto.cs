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
    public partial class frmModificarTipoProducto : Form
    {
        public frmModificarTipoProducto()
        {
            InitializeComponent();
        }

        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            frmTipoProducto verproducto = new frmTipoProducto();
            verproducto.Show();
            this.Close();
        }

        private void frmModificarTipoProducto_Load(object sender, EventArgs e)
        {

        }
    }
}
