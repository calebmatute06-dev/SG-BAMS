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
            txtTelefono.MaxLength = 8;
            txtRTN.MaxLength = 14;

            txtNombre.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
            txtApellido.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
        }

        
        public int IdClienteGenerado { get; private set; }
        public string NombreDelCliente { get; private set; }
        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            
            if (!ClsValidaciones.EsNombrePersonalValido(txtNombre.TextBox, "El Nombre") ||
                !ClsValidaciones.EsNombrePersonalValido(txtApellido.TextBox, "El Apellido"))
            {
                return;
            }

            
            if (!ClsValidaciones.EsTelefonoHondurasValido(txtTelefono.TextBox))
            {
                return;
            }

            string tel = txtTelefono.Text.Trim();
            string rtn = txtRTN.Text.Trim();

            
            if (!string.IsNullOrWhiteSpace(rtn) && rtn.Length < 14)
            {
                MessageBox.Show("El RTN debe tener exactamente 14 números o dejarse vacío.",
                                "RTN Incompleto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(rtn))
            {
                rtn = "Sin RTN";
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                ClsAgregarClientes objAC = new ClsAgregarClientes();

                int id = await objAC.AgregarClientes(txtNombre.Text.Trim(), txtApellido.Text.Trim(), tel, rtn);

                if (id > 0)
                {
                    this.IdClienteGenerado = id;
                    this.NombreDelCliente = $"{txtNombre.Text.Trim()} {txtApellido.Text.Trim()}";

                    using (FacturaAgregarDatos frmFact = new FacturaAgregarDatos(this.NombreDelCliente, this.IdClienteGenerado))
                    {
                        this.Hide();
                        frmFact.ShowDialog();
                    }

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
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

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            ClsValidaciones.ValidarSoloNumeros(e);

            
            if (!e.Handled && txtTelefono.SelectionStart == 0 && !char.IsControl(e.KeyChar))
            {
                char[] validos = { '2', '3', '8', '9' };
                if (!validos.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtRTN_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
