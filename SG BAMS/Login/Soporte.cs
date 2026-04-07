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
        /// Initializes a new instance of the <see cref="Soporte"/> class.
        /// </summary>
        public Soporte()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the btnVerUsuarios control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnVerUsuarios_Click(object sender, EventArgs e)
        {
            MenuPrincipalAdm menuPrincipalAdm = new MenuPrincipalAdm();
            menuPrincipalAdm.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles the Click event of the kryptonButton1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            MenuPrincipalEmp menuPrincipalEmp = new MenuPrincipalEmp();
            menuPrincipalEmp.Show();
            this.Hide();
        }
    }
}
