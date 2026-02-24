using Microsoft.Data.SqlClient;
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
            ClsConexion objCl = new ClsConexion();
            try
            {
                objCl.AbrirConexion();

                string query = "SELECT *  FROM vista_nombres_clientes";


                using (SqlCommand cmd = new SqlCommand(query, objCl.Conectar))
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);


                    cmbClientes.DisplayMember = "Nombre Completo";
                    cmbClientes.ValueMember = "id_cliente";
                    cmbClientes.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al llenar ComboBox: " + ex.Message);
            }
            finally
            {
                objCl.Cerrar();
            }
        }

        private async void ClienteExistente_Load(object sender, EventArgs e)
        {
            await LlenarComboCliente();
        }

        private void BtnAsignar_Click(object sender, EventArgs e)
        {
            FacturaAgregarDatos frmFA = new FacturaAgregarDatos(cmbClientes.Text, Convert.ToInt32(cmbClientes.SelectedValue));

        }
    }
}
