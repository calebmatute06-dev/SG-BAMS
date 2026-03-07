using Microsoft.Data.SqlClient;
using SG_BAMS.Cliente;
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
    public partial class ClienteModificar : Form
    {
        ClsConexion objCl = new ClsConexion();
        int idEstadoSelec;
        public ClienteModificar(int idCliente, string nombreCliente, string apellidoCliente, string telefonoCliente, string rtnCliente, int idEstado)
        {
            InitializeComponent();
            txtID.Text = idCliente.ToString();
            txtNombre.Text = nombreCliente;
            txtApellido.Text = apellidoCliente;
            txtTelefono.Text = telefonoCliente;
            txtRTN.Text = rtnCliente;
            idEstadoSelec = idEstado;

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
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono.Text) ||
                cmbEstado.SelectedValue == null) 
            {
                MessageBox.Show("Debe llenar todos los campos obligatorios antes de continuar.",
                                "Campos Vacíos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            if (!Regex.IsMatch(txtNombre.Text, @"^[a-zA-Z\sñÑáéíóúÁÉÍÓÚ]+$") ||
                !Regex.IsMatch(txtApellido.Text, @"^[a-zA-Z\sñÑáéíóúÁÉÍÓÚ]+$"))
            {
                MessageBox.Show("Nombre y Apellido solo deben contener letras.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            if (!Regex.IsMatch(txtTelefono.Text, @"^[0-9]+$"))
            {
                MessageBox.Show("El teléfono solo permite números.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

             if (!string.IsNullOrWhiteSpace(txtRTN.Text))
            {
                if (!Regex.IsMatch(txtRTN.Text, @"^([0-9]+|Sin RTN)$"))
                {
                    MessageBox.Show("El RTN solo debe contener números", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            try
            {
                ClsModificarCliente objMC = new ClsModificarCliente();

                
                int filasInsertadas = await objMC.ModificarClientes(
                    Convert.ToInt32(txtID.Text),
                    txtNombre.Text.Trim(),
                    txtApellido.Text.Trim(),
                    txtTelefono.Text.Trim(),
                    txtRTN.Text.Trim(),
                    Convert.ToInt32(cmbEstado.SelectedValue)
                );

                if (filasInsertadas > 0)
                {
                    MessageBox.Show("Cliente actualizado correctamente");
                    this.DialogResult = DialogResult.OK; 
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se realizaron cambios en el cliente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al procesar la modificación: " + ex.Message);
            }


        }



        private async Task LlenarComboEstado()
        {
            ClsModificarCliente MC = new ClsModificarCliente();
           
            try
            {
                
                DataTable dt = await MC.ObtenerEstados();

                cmbEstado.DisplayMember = "descripcion_estado";
                cmbEstado.ValueMember = "id_estado";
                cmbEstado.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al llenar Estados: " + ex.Message);
            }
        }

        private async void ClienteModificar_Load(object sender, EventArgs e)
        {
            await LlenarComboEstado();
            cmbEstado.SelectedValue = idEstadoSelec;
            txtID.ReadOnly = true;
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
