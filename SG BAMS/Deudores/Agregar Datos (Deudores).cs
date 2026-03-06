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
    public partial class Agregar_Datos__Deudores_ : Form
    {
        public Agregar_Datos__Deudores_()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void kryptonMonthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void kryptonMonthCalendar1_MouseDown(object sender, MouseEventArgs e)
        {
            ((HandledMouseEventArgs)e).Handled = true;
        }
    }
}
