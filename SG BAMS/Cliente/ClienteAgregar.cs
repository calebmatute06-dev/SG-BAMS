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
                ClsAgregarClientes objAC = new ClsAgregarClientes();

                int filasInsertadas = await objAC.AgregarClientes(
                    txtNombre.Text.Trim(),
                    txtApellido.Text.Trim(),
                    txtTelefono.Text.Trim(),
                    txtRTN.Text.Trim()
                );

                if (filasInsertadas > 0)
                {
                    MessageBox.Show("Cliente agregado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar cliente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
