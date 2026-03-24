using Microsoft.Data.SqlClient;
using SG_BAMS.Cliente;
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
    public partial class ClienteExistente : Form
    {
        public ClienteExistente()
        {
            InitializeComponent();
        }

        private async Task LlenarComboCliente()
        {
            ClsAgregarClientes objAC = new ClsAgregarClientes();
            try
            {
                
                DataTable dt = await objAC.ObtenerClientes();

                cmbClientes.DisplayMember = "Nombre Completo";
                cmbClientes.ValueMember = "ID";
                cmbClientes.DataSource = dt;

                
                cmbClientes.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbClientes.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener datos: " + ex.Message, "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void ClienteExistente_Load(object sender, EventArgs e)
        {
            await LlenarComboCliente();
            
            cmbClientes.SelectedIndex = -1;
        }

        private void BtnAsignar_Click(object sender, EventArgs e)
        {
            
            if (!ClsValidaciones.ValidarSeleccion(cmbClientes, "la lista de clientes"))
            {
                return;
            }

            try
            {
                
                if (cmbClientes.SelectedValue != null && int.TryParse(cmbClientes.SelectedValue.ToString(), out int idCliente))
                {
                    using (FacturaAgregarDatos frmFA = new FacturaAgregarDatos(cmbClientes.Text, idCliente))
                    {
                        this.Hide(); 
                        if (frmFA.ShowDialog() == DialogResult.OK)
                        {
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            this.Show();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione un cliente válido de la lista desplegable.", "Selección Requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al asignar el cliente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}