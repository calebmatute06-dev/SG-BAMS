using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS
{
    internal class NavegacionService
    {
        public void IrA(Form origen, Form destino)
        {
            destino.Show();
            origen.Hide();
        }
    }
}
