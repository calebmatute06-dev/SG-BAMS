using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SG_BAMS.Cliente;

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
            int filasInsertadas = await objAC.AgregarClientes(txtNombre.Text,txtApellido.Text,txtTelefono.Text,txtRTN.Text);

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
