using Microsoft.Data.SqlClient;
using SG_BAMS.Cliente;
using SG_BAMS.Facturas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SG_BAMS
{
    public partial class ClienteAgregar : Form
    {


        public ClienteAgregar()
        {
            InitializeComponent();
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            
            ClsAgregarClientes objAC = new ClsAgregarClientes();
            int idNuevoCliente = await objAC.AgregarClientes(txtNombre.Text, txtApellido.Text, txtTelefono.Text, txtRTN.Text);



            if (idNuevoCliente > 0)
            {
                MessageBox.Show("Cliente agregado correctamente.");

                using (FacturaAgregarDatos frmFA = new FacturaAgregarDatos(txtNombre.Text + " " + txtApellido.Text, idNuevoCliente))
                {
                   
                    if (frmFA.ShowDialog() == DialogResult.OK)
                    {
                        
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    
                }

            }
            else
            {
                MessageBox.Show("No se pudo agregar el cliente.");
            }
            
        }

        private void ClienteAgregar_Load(object sender, EventArgs e)
        {

        }

        private void BtnExistente_Click(object sender, EventArgs e)
        {
           
            using (ClienteExistente frmCE = new ClienteExistente())
            {
                if (frmCE.ShowDialog() == DialogResult.OK)
                {
                    this.DialogResult = DialogResult.OK; 
                    
                }
            }


        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();


        }
    }
}
