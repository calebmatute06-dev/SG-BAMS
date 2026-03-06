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
    public partial class Modificar_Datos__Deudor_ : Form
    {
        public Modificar_Datos__Deudor_()
        {
            InitializeComponent();
        }




       
        public Modificar_Datos__Deudor_(int idDeuda, string nombreCliente, string montoInicial, DateTime fechaInicio)
        {
            InitializeComponent();

            
            lbliddeuda.Text = idDeuda.ToString();
            lblnombre.Text = nombreCliente;
            lblmontoinicial.Text = montoInicial;

            
            fechainicio.SelectionStart = fechaInicio;
            fechafinal.SelectionStart = fechaInicio.AddDays(30);
        }

        private void label3_Click(object sender, EventArgs e)
        {
           
        }

        private void Modificar_Datos__Deudor__Load(object sender, EventArgs e)
        {
            
        }

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
