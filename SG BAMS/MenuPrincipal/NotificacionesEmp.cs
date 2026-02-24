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
    public partial class NotificacionesEmp : Form
    {
        public NotificacionesEmp()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void NotificacionesEmp_Load(object sender, EventArgs e)
        {
            Ayudante_UI.AplicarZoomGlobal(this);
        }
    }
}
