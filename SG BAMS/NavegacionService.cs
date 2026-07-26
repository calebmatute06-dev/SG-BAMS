using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;            
using System.Windows.Forms;

namespace SG_BAMS
{
    public class NavegacionService
    {
        public void IrA(Form origen, Form destino)
        {
            destino.Show();
            origen.Hide();
        }
        public void CerrarFormulariosDe<T>() where T : Form
        {
            foreach (Form frm in Application.OpenForms.OfType<T>().ToList())
            {
                frm.Close();
            }
        }

        /// <summary>
        /// Cierra todas las ventanas abiertas cuyo Name coincida con <paramref name="nombreFormulario"/>.
        /// </summary>
        public void CerrarFormularioPorNombre(string nombreFormulario)
        {
            foreach (Form frm in Application.OpenForms.Cast<Form>()
                         .Where(f => f.Name == nombreFormulario).ToList())
            {
                frm.Close();
            }
        }

        /// <summary>
        /// Si ya existe una ventana abierta con el Name indicado, la trae al frente;
        /// si no existe, la crea con <paramref name="factory"/> y la muestra.
        /// </summary>
        public void MostrarOTraerAlFrente(string nombreFormulario, Func<Form> factory)
        {
            Form abierto = Application.OpenForms[nombreFormulario];

            if (abierto != null)
            {
                abierto.BringToFront();
            }
            else
            {
                factory().Show();
            }
        }
    }
}
