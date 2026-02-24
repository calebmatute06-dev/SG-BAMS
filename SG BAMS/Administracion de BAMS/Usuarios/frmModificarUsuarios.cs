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
    public partial class frmModificarUsuarios : Form
    {
        public frmModificarUsuarios()
        {
            InitializeComponent();
            kryptonComboBox2.AutoSize = false;
        }

        private void fmrModificarUsuarios_Load(object sender, EventArgs e)
        {

        }

        private void kryptonComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            this.Close();
            frmUsuarios verUsuario = new frmUsuarios();
            verUsuario.Show();

        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            frmImagenEmpleado agregarImagen = new frmImagenEmpleado();
            agregarImagen.Show();
        }
    }
}
