using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS.Proveedor
{
    public partial class ProveedoresAdmin : Form
    {
        public ProveedoresAdmin()
        {
            InitializeComponent();
        }

        private void ProveedoresAdmin_Load(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarProveedores agregar = new AgregarProveedores();
            agregar.Show();
            this.Close();
        }
    }
}
