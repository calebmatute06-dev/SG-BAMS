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

        // Mantenido para evitar errores en el Designer
        private void kryptonTextBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private void kryptonButton20_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Validaciones de Formato y Limpieza
                if (!ClsValidacion.ValidarNombre(txtNombre.Text)) return;

                string precioLimpio = txtPrecio.Text.Replace("Lps", "").Replace("$", "").Trim();
                if (!ClsValidacion.ValidarPrecio(precioLimpio)) return;
                if (!ClsValidacion.ValidarCodigoBarra(txtCodigoBarra.Text)) return;

                // 2. Validaciones de selección de ComboBoxes
                if (!ClsValidacion.ValidarSeleccion(cmbMarca, "Marca")) return;
                if (!ClsValidacion.ValidarSeleccion(cmbTipo, "Tipo")) return;
                if (!ClsValidacion.ValidarSeleccion(cmbModelo, "Modelo")) return;
                if (!ClsValidacion.ValidarSeleccion(cmbEstado, "Estado")) return;
                if (!ClsValidacion.ValidarSeleccion(cmbProveedor, "Proveedor")) return;

                int idActual = Convert.ToInt32(txtID.Text);
                string nombreNuevo = txtNombre.Text.Trim();
                string codigoNuevo = txtCodigoBarra.Text.Trim();
                int idMarca = Convert.ToInt32(cmbMarca.SelectedValue);
                int idProveedor = Convert.ToInt32(cmbProveedor.SelectedValue);

                // --- NUEVA VALIDACIÓN ADAPTADA ---
                // Reemplazamos la validación vieja de nombre por la del "Trío" (Nombre+Marca+Proveedor)
                // Se asume que ClsValidacion.ValidarExistencia ya fue actualizada para recibir estos 4 parámetros
                if (!ClsValidacion.ValidarExistencia(idActual, nombreNuevo, idMarca, idProveedor))
                {
                    txtNombre.Focus();
                    return;
                }

                // Instancia de la lógica
                SG_BAMS.ProductoInventario.ClsActualizarProducto logica = new SG_BAMS.ProductoInventario.ClsActualizarProducto();

                // Validación de duplicados (Código) usando el nuevo método de la clase lógica
                if (logica.ExisteCodigoEnOtros(idActual, codigoNuevo))
                {
                    MessageBox.Show("El código de barras ya está asignado a otro producto.", "Código Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtCodigoBarra.Focus();
                    return;
                }

                // Ejecución con los 9 parámetros requeridos
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

        // Se mantiene el método aunque la lógica ahora se llame desde ClsActualizarProducto para no romper referencias si existieran
        private bool ExisteDuplicadoEnOtros(int idActual, string columna, string valor)
        {
            ClsConexion conexion = new ClsConexion();
            int total = 0;
            try
            {
                conexion.AbrirConexion();
                string sql = $"SELECT COUNT(*) FROM Producto WHERE {columna} = @valor AND id_producto <> @id";

                using (SqlCommand cmd = new SqlCommand(sql, conexion.Conectar))
                {
                    cmd.Parameters.AddWithValue("@valor", valor);
                    cmd.Parameters.AddWithValue("@id", idActual);
                    total = (int)cmd.ExecuteScalar();
                }
            }
            catch { throw; }
            finally { conexion.Cerrar(); }

            return total > 0;
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
                // Usando el nuevo método optimizado que configuramos previamente
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
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            if (txtCodigoBarra.Text.Length >= 13 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}