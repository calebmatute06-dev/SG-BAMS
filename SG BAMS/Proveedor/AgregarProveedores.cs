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

        }

        private void AgregarProveedores_Load(object sender, EventArgs e)
        {
            proveedor.CargarComboClasificacion(cmbClasificacion);
            cmbClasificacion.DropDownStyle = ComboBoxStyle.DropDownList;

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
            string nombreLimpio = txtNombre.Text.Trim();
            string direLimpio = txtDireccion.Text.Trim();
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono.Text) ||
                string.IsNullOrWhiteSpace(txtDireccion.Text) ||
                string.IsNullOrWhiteSpace(txtRTN.Text) ||
                cmbClasificacion.SelectedValue == null)
            {
                MessageBox.Show("Debe llenar todos los campos obligatorios antes de continuar.",
                                "Campos Vacíos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (direLimpio.Length < 3)
            {
                MessageBox.Show("La direccion debe tener mas de 3 caracteres.", "Error de Longitud", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nombreLimpio.Contains("  "))
            {
                MessageBox.Show("El nombre no puede contener dos espacios seguidos.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtTelefono.Text.Contains(" ") || txtRTN.Text.Contains(" "))
            {
                MessageBox.Show("El Numero o RTN no puede contener espacios.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (nombreLimpio.Length < 3)
            {
                MessageBox.Show("El nombre debe tener tener mas 3 caracteres.", "Error de Longitud", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Regex.IsMatch(nombreLimpio, @"(\w)\1{2,}"))
            {
                MessageBox.Show("No se permite repetir la misma letra más de dos veces seguidas.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Regex.IsMatch(nombreLimpio, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ&]{3,}(\s[a-zA-ZñÑáéíóúÁÉÍÓÚ&]{3,})*$"))
            {
                MessageBox.Show("El campos de Nombre solo deben contener caracteres validos.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (!Regex.IsMatch(txtTelefono.Text, @"^([0-9]{8})$") || !Regex.IsMatch(txtRTN.Text, @"^([0-9]{14})$"))
            {
                MessageBox.Show("Los campos de Telefono o RTN solo deben contener números o El numero esta incompleto.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            else
            {
                int idClasificacion = Convert.ToInt32(cmbClasificacion.SelectedValue);

                int idUsuario = new ClsPasarUsuario().IdUsuario();

                proveedor.AgregarProveedor(
                    txtNombre.Text.Trim(),
                    txtTelefono.Text.Trim(),
                    txtDireccion.Text.Trim(),
                    txtRTN.Text.Trim(),
                    idClasificacion,
                    idUsuario
                );

                ProveedoresAdmin proveedorAdmin = new ProveedoresAdmin();
                proveedorAdmin.Show();
                this.Close();
            }
            
        }
    }
}
