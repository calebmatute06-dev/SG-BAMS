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
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                MessageBox.Show("Error al obtener datos: " + ex.Message);
            }
        }

        private async void ClienteExistente_Load(object sender, EventArgs e)
        {
            await LlenarComboCliente();
            cmbClientes.SelectedIndex = -1;
        }

        private void BtnAsignar_Click(object sender, EventArgs e)
        {
            if (cmbClientes.SelectedValue != null)
            {
                try
                {

                    int idCliente = Convert.ToInt32(cmbClientes.SelectedValue);
                    using (FacturaAgregarDatos frmFA = new FacturaAgregarDatos(cmbClientes.Text, idCliente))
                    {
                        if (frmFA.ShowDialog() == DialogResult.OK)
                        {
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }

                }
                catch
                {

                    MessageBox.Show("El sistema aún está cargando los datos. Por favor, selecciona el cliente de nuevo.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un cliente válido de la lista.");
            }
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
