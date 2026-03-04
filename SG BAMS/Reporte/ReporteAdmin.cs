using SG_BAMS.Reportes;
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
    public partial class ReporteAdmin : Form
    {

        ClsReporte objReporte = new ClsReporte();

        public ReporteAdmin()
        {
            InitializeComponent();
        }

        private void kryptonButton12_Click(object sender, EventArgs e)
        {
            
        }

        private void kryptonGroup3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ReporteAdmin_Load(object sender, EventArgs e)
        {
            CargarGridPrincipal();
        }

        private void CargarGridPrincipal()
        {
            objReporte.CargarDatosReporte(dgvReporte);
        }
    }
}
