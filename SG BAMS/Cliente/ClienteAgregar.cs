using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SG_BAMS.Cliente;
using SG_BAMS.Facturas;

namespace SG_BAMS
{
    public partial class ClienteAgregar : Form
    {
        public int IdClienteGenerado { get; private set; }
        public string NombreDelCliente { get; private set; }

        public ClienteAgregar()
        {
            InitializeComponent();

           
            txtTelefono.MaxLength = 8;
            txtRTN.MaxLength = 14;

            
            txtNombre.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
            txtApellido.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
            txtRTN.KeyPress += (s, e) => ClsValidaciones.ValidarSoloNumeros(e);
        }

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

            string rtn = txtRTN.Text.Trim();

            
            if (!string.IsNullOrWhiteSpace(rtn))
            {
                
                if (!ClsValidaciones.EsAlfanumericoValido(txtRTN.TextBox, "RTN", 14, 14))
                {
                    return;
                }
            }
            else
            {
                rtn = "Sin RTN";
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                ClsAgregarClientes objAC = new ClsAgregarClientes();

                int id = await objAC.AgregarClientes(
                    txtNombre.Text.Trim(),
                    txtApellido.Text.Trim(),
                    txtTelefono.Text.Trim(),
                    rtn
                );

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
                MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {

            ClsValidaciones.ValidarTelefonoKeyPress(txtTelefono, e);
        }

        private void BtnExistente_Click(object sender, EventArgs e)
        {
            using (ClienteExistente frmCE = new ClienteExistente())
            {
                if (frmCE.ShowDialog() == DialogResult.OK)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private void BtnSalir_Click(object sender, EventArgs e) => this.Close();

        private void ClienteAgregar_Load(object sender, EventArgs e) { }
    }
}