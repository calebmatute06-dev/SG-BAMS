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
    public partial class frmModeloAuto : Form
    {
        public frmModeloAuto()
        {
            InitializeComponent();
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            frmModificarModelos modificarMauto = new frmModificarModelos();
            modificarMauto.Show();
            this.Close();
        }

        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            frmAgregarModeloAuto agregarMauto = new frmAgregarModeloAuto();
            agregarMauto.Show();
            this.Close();
        }
    }
}
