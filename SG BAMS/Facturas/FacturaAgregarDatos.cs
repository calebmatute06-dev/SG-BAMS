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

namespace SG_BAMS
{
    public partial class FacturaAgregarDatos : Form
    {
        public FacturaAgregarDatos(string cliente, int idCliente)
        {
            InitializeComponent();
            TxtCliente.Text = cliente;

        }

        public FacturaAgregarDatos()
        {
            InitializeComponent();


        }


        private async Task LlenarComboPago()
        {
            ClsConexion objCl = new ClsConexion();
            try
            {
                objCl.AbrirConexion();

                string query = "SELECT *  FROM Tipo_Forma_de_pago";


                using (SqlCommand cmd = new SqlCommand(query, objCl.Conectar))
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);


                    cmbPago.DisplayMember = "descripcion_forma_pago";
                    cmbPago.ValueMember = "id_tipo_forma_pago";
                    cmbPago.DataSource = dt;
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

        private async void FacturaAgregarDatos_Load(object sender, EventArgs e)
        {
            await LlenarComboPago();
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            ClsAgregarFactura objAF = new ClsAgregarFactura();
            int filasInsertadas = await objAF.AgregarFacturas(txtNombre.Text, txtApellido.Text, txtTelefono.Text, txtRTN.Text);

            if (filasInsertadas > 0)
            {
                MessageBox.Show("Cliente agregado correctamente.");


                txtNombre.Clear();
                txtApellido.Clear();
                txtTelefono.Clear();
                txtRTN.Clear();
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo agregar el cliente.");
            }
        }
    }
}
