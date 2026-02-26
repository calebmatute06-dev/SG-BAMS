using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS.Bitacora
{
    public partial class Bitacora : Form
    {
        ClsBitacora bitacora = new ClsBitacora();

        public Bitacora()
        {
            InitializeComponent();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {

        }

        private void btnExportar_Click(object sender, EventArgs e)
        {

        }

        private void Bitacora_Load(object sender, EventArgs e)
        {
            bitacora.cargarDatos(dgvBitacora);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
