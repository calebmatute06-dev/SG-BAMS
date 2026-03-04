using Microsoft.Data.SqlClient;
using SG_BAMS.Administracion_de_BAMS.Estado;
using SG_BAMS.Cliente;
using SG_BAMS.Facturas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                MessageBox.Show("Debe llenar todos los campos obligatorios antes de continuar.",
                                "Campos Vacíos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            else if (!Regex.IsMatch(txtNombre.Text, @"^[a-zA-Z\s]+$") || !Regex.IsMatch(txtApellido.Text, @"^[a-zA-Z\s]+$"))
            {
                MessageBox.Show("Los campos 'Nombre' y 'Apellido' solo deben contener letras.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            else if (!Regex.IsMatch(txtTelefono.Text, @"^[0-9]+$")  /*|| !Regex.IsMatch(txtRTN.Text, @"^[0-9]+$")*/)
            {
                MessageBox.Show("Los campos 'Teléfono' solo deben contener números.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            

            else
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
