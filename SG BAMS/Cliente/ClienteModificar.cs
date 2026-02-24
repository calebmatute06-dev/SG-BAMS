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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SG_BAMS
{
    public partial class ClienteModificar : Form
    {
        ClsConexion objCl = new ClsConexion();
        public ClienteModificar(int idCliente, string nombreCliente, string apellidoCliente, string telefonoCliente, string rtnCliente, int idEstado)
        {
            InitializeComponent();
            txtID.Text = idCliente.ToString();
            txtNombre.Text = nombreCliente;
            txtApellido.Text = apellidoCliente;
            txtTelefono.Text = telefonoCliente;
            txtRTN.Text = rtnCliente;
            cmbEstado.SelectedValue = idEstado;

        }

        public ClienteModificar()
        {
            InitializeComponent();
        }

        

        private void kryptonButton2_Click(object sender, EventArgs e)
        {

        }

        private async void BtnModificar_Click(object sender, EventArgs e)
        {
            ClsModificarCliente objMC = new ClsModificarCliente();
            int filasInsertadas = await objMC.ModificarClientes(Convert.ToInt32(txtID.Text),txtNombre.Text,txtApellido.Text, txtTelefono.Text, txtRTN.Text,Convert.ToInt32(cmbEstado.SelectedValue));

            if (filasInsertadas > 0)
            {
                MessageBox.Show("Cliente actualizado correctamente");


                txtNombre.Clear();
                txtApellido.Clear();
                txtTelefono.Clear();
                txtRTN.Clear();
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo agregar el cliente");
            }
        }



        private async Task LlenarComboEstado()
        {
            try
            {
                objCl.AbrirConexion();

                string query = "SELECT id_estado, descripcion_estado  FROM Estado";

                
                using (SqlCommand cmd = new SqlCommand(query, objCl.Conectar))
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader); 

                    cmbEstado.DisplayMember = "descripcion_estado";
                    cmbEstado.ValueMember = "id_estado";
                    cmbEstado.DataSource = dt;
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

        private async void ClienteModificar_Load(object sender, EventArgs e)
        {
            await LlenarComboEstado();
        }
    }
}
