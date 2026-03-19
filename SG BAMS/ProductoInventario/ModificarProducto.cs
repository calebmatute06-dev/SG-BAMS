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

        public string marcaActual, tipoActual, modeloActual, estadoActual;

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
                // 1. Validaciones de Formato y Limpieza
                if (!ClsValidacion.ValidarNombre(txtNombre.Text)) return;

                // Limpiamos el precio de símbolos de moneda para que la conversión no falle
                string precioLimpio = txtPrecio.Text.Replace("Lps", "").Replace("$", "").Trim();
                if (!ClsValidacion.ValidarPrecio(precioLimpio)) return;
                if (!ClsValidacion.ValidarCodigoBarra(txtCodigoBarra.Text)) return;

                // Datos necesarios para la validación y actualización
                int idActual = Convert.ToInt32(txtID.Text);
                string nombreNuevo = txtNombre.Text.Trim();
                string codigoNuevo = txtCodigoBarra.Text.Trim();

                // 2. VALIDACIÓN: Nombre repetido en otros registros
                if (ExisteDuplicadoEnOtros(idActual, "nombre_producto", nombreNuevo))
                {
                    MessageBox.Show("No se puede actualizar: El nombre '" + nombreNuevo + "' ya está asignado a otro producto.",
                                    "Nombre Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtNombre.Focus();
                    return;
                }

                // 3. VALIDACIÓN: Código de barras repetido en otros registros
                if (ExisteDuplicadoEnOtros(idActual, "codigo_barra", codigoNuevo))
                {
                    MessageBox.Show("No se puede actualizar: El código de barras '" + codigoNuevo + "' ya está asignado a otro producto.",
                                    "Código Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtCodigoBarra.Focus();
                    return;
                }

                // 4. Validaciones de selección de ComboBoxes
                if (!ClsValidacion.ValidarSeleccion(cmbMarca, "Marca")) return;
                if (!ClsValidacion.ValidarSeleccion(cmbTipo, "Tipo")) return;
                if (!ClsValidacion.ValidarSeleccion(cmbModelo, "Modelo")) return;
                if (!ClsValidacion.ValidarSeleccion(cmbEstado, "Estado")) return;

                // Instancia de la lógica (ClsActualizarProducto ya no requiere 'servicio')
                SG_BAMS.ProductoInventario.ClsActualizarProducto logica = new SG_BAMS.ProductoInventario.ClsActualizarProducto();

                // Ejecución con las variables correctas según la nueva firma
                logica.EjecutarActualizacion(
                    idActual,
                    nombreNuevo,
                    Convert.ToInt32(cmbMarca.SelectedValue),
                    Convert.ToInt32(cmbTipo.SelectedValue),
                    Convert.ToInt32(cmbModelo.SelectedValue),
                    Convert.ToInt32(cmbEstado.SelectedValue),
                    Convert.ToDecimal(precioLimpio),
                    codigoNuevo
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

        private bool ExisteDuplicadoEnOtros(int idActual, string columna, string valor)
        {
            ClsConexion conexion = new ClsConexion();
            int total = 0;
            try
            {
                conexion.AbrirConexion();
                // Consultamos si el valor existe en OTRO ID diferente al que tengo abierto
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

            cmbMarca.SelectedIndex = cmbMarca.FindStringExact(marcaActual);
            cmbTipo.SelectedIndex = cmbTipo.FindStringExact(tipoActual);
            cmbModelo.SelectedIndex = cmbModelo.FindStringExact(modeloActual);
            cmbEstado.SelectedIndex = cmbEstado.FindStringExact(estadoActual);
        }

        public void LlenarCombosModificar()
        {
            ClsLlenarCombo llenar = new ClsLlenarCombo();

            cmbMarca.DataSource = llenar.ObtenerDatosCombo("Marca");
            cmbMarca.DisplayMember = "nombre_marca";
            cmbMarca.ValueMember = "id_marca_producto";

            cmbTipo.DataSource = llenar.ObtenerDatosCombo("Tipo");
            cmbTipo.DisplayMember = "descripcion_forma_pago";
            cmbTipo.ValueMember = "id_tipo_producto";

            cmbModelo.DataSource = llenar.ObtenerDatosCombo("Modelo");
            cmbModelo.DisplayMember = "nombre_modelo_auto";
            cmbModelo.ValueMember = "id_modelo_auto";

            cmbEstado.DataSource = llenar.ObtenerDatosCombo("Estado");
            cmbEstado.DisplayMember = "descripcion_estado";
            cmbEstado.ValueMember = "id_estado";
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
