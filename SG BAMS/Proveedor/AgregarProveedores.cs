using SG_BAMS.Administracion_de_BAMS.Estado;
using SG_BAMS.Login;
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
using static System.Collections.Specialized.BitVector32;

namespace SG_BAMS.Proveedor
{
    public partial class AgregarProveedores : Form
    {
        ClsProveedor proveedor = new ClsProveedor();

        public AgregarProveedores()
        {
            InitializeComponent();
            txtTelefono.MaxLength = 8;
            txtRTN.MaxLength = 14;
            this.txtNombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNombre_KeyPress);
            this.txtDireccion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDireccion_KeyPress);
        }

        private void AgregarProveedores_Load(object sender, EventArgs e)
        {
            proveedor.CargarComboClasificacion(cmbClasificacion);
            cmbClasificacion.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        private bool ValidarFormatoTexto(string texto, string nombreCampo)
        {
            string textoLimpio = texto.Trim();

            if (textoLimpio.Length < 3 || textoLimpio.Length > 200)
            {
                MessageBox.Show($"{nombreCampo} debe tener al menos 3 caracteres.", "Error de Largo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string[] palabras = textoLimpio.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string palabra in palabras)
            {
                if (palabra.Length < 2 && palabra != "&")
                {
                    MessageBox.Show($"{nombreCampo} contiene una palabra muy corta o inválida ('{palabra}').", "Formato Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
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

        private void btnsalir_Click(object sender, EventArgs e)
        {
            ProveedoresAdmin proveedor = new ProveedoresAdmin();
            proveedor.Show();
            this.Close();
        }

        private void cmbClasificacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbClasificacion.SelectedIndex != -1 && cmbClasificacion.SelectedItem is DataRowView)
            {
                DataRowView drv = (DataRowView)cmbClasificacion.SelectedItem;
                int idClasificacion = Convert.ToInt32(drv["id_clasificacion_proveedor"]);
                string nombreClasificacion = drv["clasificacion_proveedor"].ToString();

            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono.Text) ||
                string.IsNullOrWhiteSpace(txtDireccion.Text) ||
                string.IsNullOrWhiteSpace(txtRTN.Text) ||
                cmbClasificacion.SelectedValue == null)
            {
                MessageBox.Show("Todos los campos son obligatorios.", "Campos Vacíos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!ValidarFormatoTexto(txtNombre.Text, "El Nombre") ||
                !ValidarFormatoTexto(txtDireccion.Text, "La Dirección")) return;

            string nombreNuevo = txtNombre.Text.Trim();
            if (proveedor.ExisteNombreProveedor(nombreNuevo))
            {
                MessageBox.Show("El nombre del proveedor ya existe.", "Nombre Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }

            string tel = txtTelefono.Text.Trim();
            if (tel.Length != 8 || Regex.IsMatch(tel, @"(\d)\1{3}"))
            {
                MessageBox.Show("Teléfono inválido. Debe tener 8 dígitos y no más de 3 números iguales consecutivos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string rtn = txtRTN.Text.Trim();
            if (rtn.Length < 14)
            {
                MessageBox.Show("El RTN es obligatorio y debe tener exactamente 14 números.", "RTN Requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int idClasificacion = Convert.ToInt32(cmbClasificacion.SelectedValue);
                int idUsuario = new ClsPasarUsuario().IdUsuario();

                proveedor.AgregarProveedor(
                    txtNombre.Text.Trim(),
                    tel,
                    txtDireccion.Text.Trim(),
                    rtn,
                    idClasificacion,
                    idUsuario
                );

                MessageBox.Show("Proveedor agregado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ProveedoresAdmin proveedorAdmin = new ProveedoresAdmin();
                proveedorAdmin.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
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

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '&')
            {
                e.Handled = true;
            }
        }

        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
