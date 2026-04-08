using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS.Login
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class Soporte : Form
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Soporte"/>.
        /// </summary>
        public Soporte()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        /// <summary>
        /// Maneja el evento Click del control btnVerUsuarios.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnVerUsuarios_Click(object sender, EventArgs e)
        {
            MenuPrincipalAdm menuPrincipalAdm = new MenuPrincipalAdm();
            menuPrincipalAdm.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del control kryptonButton1.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            MenuPrincipalEmp menuPrincipalEmp = new MenuPrincipalEmp();
            menuPrincipalEmp.Show();
            this.Hide();
        }
    }
}