using Microsoft.Data.SqlClient;
using SG_BAMS.ProductoInventario;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS
{
    public partial class ModificarProducto : Form
    {
        public string marcaActual, tipoActual, modeloActual, estadoActual, proveedorActual;

        public ModificarProducto()
        {
            InitializeComponent();
        }

        private void kryptonTextBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private void kryptonButton20_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ClsValidaciones.EsAlfanumericoValido(txtNombre, "Nombre del Producto")) return;
                string precioLimpio = txtPrecio.Text.Replace("Lps", "").Replace("$", "").Trim();
                if (!ClsValidaciones.ValidarPrecio(precioLimpio)) return;
                if (!ClsValidaciones.ValidarCodigoBarra(txtCodigoBarra.Text)) return;

                int idActual = Convert.ToInt32(txtID.Text);
                string nombreNuevo = txtNombre.Text.Trim();
                string codigoNuevo = txtCodigoBarra.Text.Trim();
                int idMarca = Convert.ToInt32(cmbMarca.SelectedValue);
                int idProveedor = Convert.ToInt32(cmbProveedor.SelectedValue);

                ClsActualizarProducto logica = new ClsActualizarProducto();

                if (logica.ExisteProductoEnOtros(idActual, nombreNuevo, idMarca, idProveedor))
                {
                    MessageBox.Show("Este producto con esta marca ya está registrado para el proveedor seleccionado.\n\n" +
                    "Si es un proveedor distinto, sí puede usar el mismo nombre.",
                    "Producto Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtNombre.Focus();
                    return;
                }

                if (logica.ExisteCodigoEnOtros(idActual, codigoNuevo))
                {
                    MessageBox.Show("El código de barras ya está asignado a otro producto.",
                                    "Código Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtCodigoBarra.Focus();
                    return;
                }

                logica.EjecutarActualizacion(
                    idActual,
                    nombreNuevo,
                    idMarca,
                    Convert.ToInt32(cmbTipo.SelectedValue),
                    Convert.ToInt32(cmbModelo.SelectedValue),
                    Convert.ToInt32(cmbEstado.SelectedValue),
                    Convert.ToDecimal(precioLimpio),
                    codigoNuevo,
                    idProveedor
                );

                MessageBox.Show("¡Producto actualizado correctamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar cambios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ModificarProducto_Load(object sender, EventArgs e)
        {
            LlenarCombosModificar();

            cmbMarca.SelectedIndex = cmbMarca.FindStringExact(marcaActual?.Trim());
            cmbTipo.SelectedIndex = cmbTipo.FindStringExact(tipoActual?.Trim());
            cmbModelo.SelectedIndex = cmbModelo.FindStringExact(modeloActual?.Trim());
            cmbEstado.SelectedIndex = cmbEstado.FindStringExact(estadoActual?.Trim());
            cmbProveedor.SelectedIndex = cmbProveedor.FindStringExact(proveedorActual?.Trim());
        }

        public void LlenarCombosModificar()
        {
            ClsLlenarCombo llenar = new ClsLlenarCombo();
            try
            {
                llenar.ConfigurarComboBox(cmbMarca, "Marca");
                llenar.ConfigurarComboBox(cmbTipo, "Tipo");
                llenar.ConfigurarComboBox(cmbModelo, "Modelo");
                llenar.ConfigurarComboBox(cmbEstado, "Estado");
                llenar.ConfigurarComboBox(cmbProveedor, "Proveedor");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtCodigoBarra_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCodigoBarra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            if (txtCodigoBarra.Text.Length >= 20 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;

                int longitud = txtCodigoBarra.Text.Length;

                if (longitud >= 6 && longitud <= 20)
                {
                    cmbProveedor.Focus();
                }
                else
                {
                    MessageBox.Show("Código inválido. Debe tener entre 6 y 20 caracteres (letras o números).\n" +
                                    "Intenta escanear o escribir de nuevo.",
                                    "Código Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCodigoBarra.Clear();
                    txtCodigoBarra.Focus();
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.PermitirNumerosYDecimales(sender, e);
        }
    }
}