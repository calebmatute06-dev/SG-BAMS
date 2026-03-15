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
            txtTelefono.MaxLength = 8;
            txtRTN.MaxLength = 14;
        }

        public ClienteModificar()
        {
            InitializeComponent();
        }

        private bool ValidarFormatoTexto(string texto, string nombreCampo)
        {
            string textoLimpio = texto.Trim();

            if (textoLimpio.Length < 3 || textoLimpio.Length > 70)
            {
                MessageBox.Show($"{nombreCampo} debe tener entre 3 y 70 caracteres.", "Error de Largo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string[] palabras = textoLimpio.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string palabra in palabras)
            {
                if (palabra.Length < 2)
                {
                    MessageBox.Show($"{nombreCampo} contiene una palabra demasiado corta ('{palabra}').", "Formato Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            if (!Regex.IsMatch(textoLimpio, @"^[a-zA-Z\sñÑáéíóúÁÉÍÓÚ]+$"))
            {
                MessageBox.Show($"{nombreCampo} solo debe contener letras.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (Regex.IsMatch(textoLimpio, @"\s{2,}"))
            {
                MessageBox.Show($"{nombreCampo} no puede contener dobles espacios.", "Error de Espacios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (Regex.IsMatch(textoLimpio, @"([a-zA-ZñÑáéíóúÁÉÍÓÚ])\1{2,}", RegexOptions.IgnoreCase))
            {
                MessageBox.Show($"{nombreCampo} tiene demasiadas letras repetidas seguidas.", "Error de Escritura", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
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
                MessageBox.Show("Debe llenar todos los campos obligatorios.", "Campos Vacíos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!ValidarFormatoTexto(txtNombre.Text, "El Nombre") || !ValidarFormatoTexto(txtApellido.Text, "El Apellido")) return;

            string tel = txtTelefono.Text.Trim();
            if (tel.Length != 8 || Regex.IsMatch(tel, @"(\d)\1{3}"))
            {
                MessageBox.Show("El teléfono debe tener 8 dígitos y no más de 3 números iguales consecutivos.", "Error de Teléfono", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string rtn = txtRTN.Text.Trim();

            if (!string.IsNullOrWhiteSpace(rtn) && rtn.Length < 14 && rtn.ToUpper() != "SIN RTN")
            {
                MessageBox.Show("Debe completar los 14 números del RTN o dejar el campo vacío.",
                                "RTN Incompleto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }
            if (string.IsNullOrWhiteSpace(rtn))
            {
                rtn = "Sin RTN";
            }

            try
            {
                ClsModificarCliente objMC = new ClsModificarCliente();

                int filasActualizadas = await objMC.ModificarClientes(
                    Convert.ToInt32(txtID.Text),
                    txtNombre.Text.Trim(),
                    txtApellido.Text.Trim(),
                    tel,
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
                    MessageBox.Show("No se realizaron cambios en la base de datos.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            this.Close();
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            if (!char.IsDigit(e.KeyChar)) { e.Handled = true; return; }

            if (txtTelefono.SelectionStart == 0)
            {
                char[] validos = { '2', '3', '8', '9' };
                if (!validos.Contains(e.KeyChar)) { e.Handled = true; return; }
            }

            if (txtTelefono.Text.Length >= 3)
            {
                int pos = txtTelefono.SelectionStart;
                string t = txtTelefono.Text;
                if (pos >= 3 && t[pos - 1] == e.KeyChar && t[pos - 2] == e.KeyChar && t[pos - 3] == e.KeyChar)
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
