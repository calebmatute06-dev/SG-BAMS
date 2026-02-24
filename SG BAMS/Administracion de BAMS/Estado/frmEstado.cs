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
    public partial class frmEstado : Form
    {
        public frmEstado()
        {
            InitializeComponent();
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            frmAgregarEstado agregarEstado = new frmAgregarEstado();
            agregarEstado.Show();
            this.Close();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            frmModificarEstado modificarEstado = new frmModificarEstado();
            modificarEstado.Show();
            this.Close();
        }
    }
}
