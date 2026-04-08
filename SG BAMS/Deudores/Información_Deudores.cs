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
        /// Inicializa una nueva instancia de la clase <see cref="Información_Deudores"/>.
        /// </summary>
        public Información_Deudores()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Información_Deudores"/>.
        /// </summary>
        /// <param name="idDeuda">El identificador de la deuda.</param>
        /// <param name="nombreCliente">El nombre del cliente.</param>
        /// <param name="montoInicial">El monto inicial.</param>
        /// <param name="fechaInicio">La fecha de inicio.</param>
        public Información_Deudores(int idDeuda, string nombreCliente, string montoInicial, DateTime fechaInicio)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            lbliddeuda.Text = idDeuda.ToString();
            lblnombre.Text = nombreCliente;
            lblmontoinicial.Text = montoInicial;


            fechainicio.SelectionStart = fechaInicio;
            fechafinal.SelectionStart = fechaInicio.AddDays(30);
        }
        /// <summary>
        /// Maneja el evento Load del control Información_Deudores.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void Información_Deudores_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Maneja el evento Click del control btnaceptar.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
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