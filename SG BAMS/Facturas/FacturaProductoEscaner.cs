using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS.Facturas
{
    public partial class FacturaProductoEscaner : Form
    {
        public FacturaProductoEscaner()
        {
            InitializeComponent();
        }

        private void BtnNombre_Click(object sender, EventArgs e)
        {
            FacturaProducto FP = new FacturaProducto();
            FP.Show();
            this.Close();
        }
    }
}
