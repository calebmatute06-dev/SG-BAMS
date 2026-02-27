using Krypton.Toolkit;
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
    public partial class MenuPrincipalAdm : Form
    {
        public MenuPrincipalAdm()
        {
            InitializeComponent();
        }


        private void MenuPrincipalAdm_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }



        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void kryptonButton8_Click(object sender, EventArgs e)
        {

        }

        private void BtnFacturas_Click(object sender, EventArgs e)
        {
            FacturasAdm frmFA  = new FacturasAdm();
            frmFA.Show();
        }
    }
}
