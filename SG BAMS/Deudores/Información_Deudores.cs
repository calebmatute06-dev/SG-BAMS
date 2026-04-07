using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS.Deudores
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class Información_Deudores : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Información_Deudores"/> class.
        /// </summary>
        public Información_Deudores()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Información_Deudores"/> class.
        /// </summary>
        /// <param name="idDeuda">The identifier deuda.</param>
        /// <param name="nombreCliente">The nombre cliente.</param>
        /// <param name="montoInicial">The monto inicial.</param>
        /// <param name="fechaInicio">The fecha inicio.</param>
        public Información_Deudores(int idDeuda, string nombreCliente, string montoInicial, DateTime fechaInicio)
        {
            InitializeComponent();


            lbliddeuda.Text = idDeuda.ToString();
            lblnombre.Text = nombreCliente;
            lblmontoinicial.Text = montoInicial;


            fechainicio.SelectionStart = fechaInicio;
            fechafinal.SelectionStart = fechaInicio.AddDays(30);
        }
        /// <summary>
        /// Handles the Load event of the Información_Deudores control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Información_Deudores_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the btnaceptar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnaceptar_Click(object sender, EventArgs e)
        {
            List<Form> formulariosACerrar = new List<Form>();

            foreach (Form frm in Application.OpenForms)
            {

                if (frm is FacturaAgregarDatos || frm.Name == "FacturasAdm")
                {
                    formulariosACerrar.Add(frm);
                }
            }


            foreach (Form frm in formulariosACerrar)
            {
                frm.Close();
            }


            Form deudoresAbierto = Application.OpenForms["Deudores"];

            if (deudoresAbierto != null)
            {
                deudoresAbierto.BringToFront();
            }
            else
            {

                DeudoresAdmin deudores = new DeudoresAdmin();
                deudores.Show();
            }


            MessageBox.Show("Datos confirmados. Redirigiendo a Deudores.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
