using SG_BAMS.Cliente;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Krypton.Toolkit;

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


            txtTelefono.MaxLength = 8;
            txtRTN.MaxLength = 14;
            txtID.ReadOnly = true;


            txtNombre.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
            txtApellido.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
            txtTelefono.KeyPress += (s, e) => ClsValidaciones.ValidarSoloNumeros(e);
            txtRTN.KeyPress += (s, e) => ClsValidaciones.ValidarSoloNumeros(e);
            txtTelefono.KeyPress += (s, e) => 
                ClsValidaciones.ValidarTelefonoKeyPress(txtTelefono, e);
        }

        public ClienteModificar()
        {
            InitializeComponent();
        }

        private async void BtnModificar_Click(object sender, EventArgs e)
        {

            if (!ClsValidaciones.EsNombrePersonalValido(txtNombre, "Nombre") ||
                !ClsValidaciones.EsNombrePersonalValido(txtApellido, "Apellido"))
            {
                return;
            }


            if (!ClsValidaciones.EsTelefonoHondurasValido(txtTelefono))
            {
                return;
            }


            if (!ClsValidaciones.ValidarSeleccion(cmbEstado, "el estado del cliente"))
            {
                return;
            }


            string rtn = txtRTN.Text.Trim();
            if (!string.IsNullOrWhiteSpace(rtn) && rtn.ToUpper() != "SIN RTN")
            {
                if (!ClsValidaciones.EsRTNValido(txtRTN))
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
                ClsModificarCliente objMC = new ClsModificarCliente();

                int filasActualizadas = await objMC.ModificarClientes(
                    Convert.ToInt32(txtID.Text),
                    txtNombre.Text.Trim(),
                    txtApellido.Text.Trim(),
                    txtTelefono.Text.Trim(),
                    rtn,
                    Convert.ToInt32(cmbEstado.SelectedValue)
                );

                if (filasActualizadas > 0)
                {
                    MessageBox.Show("Cliente actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se detectaron cambios para actualizar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private async Task LlenarComboEstado()
        {
            ClsModificarCliente MC = new ClsModificarCliente();
            try
            {
                DataTable dt = await MC.ObtenerEstados();
                cmbEstado.DataSource = dt;
                cmbEstado.DisplayMember = "descripcion_estado";
                cmbEstado.ValueMember = "id_estado";
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
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void BtnSalir_Click(object sender, EventArgs e) => this.Close();

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.ValidarTelefonoKeyPress(txtTelefono, e);
        }
    }
}