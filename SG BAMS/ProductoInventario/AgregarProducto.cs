using Microsoft.Data.SqlClient;
using SG_BAMS.Facturas;
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
    public partial class AgregarProducto : Form
    {
        public AgregarProducto()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string nombreLimpio = txtNombre.Text.Trim();
            if (!ClsValidacion.ValidarNombre(nombreLimpio)) return;

            // 1. Validaciones de formato (Capa de Cliente)
            if (!ClsValidacion.ValidarNombre(txtNombre.Text)) return;
            if (!ClsValidacion.ValidarPrecio(txtPrecio.Text)) return;
            if (!ClsValidacion.ValidarSeleccion(cmbMarca, "la Marca")) return;
            if (!ClsValidacion.ValidarSeleccion(cmbTipo, "el Tipo de Producto")) return;
            if (!ClsValidacion.ValidarSeleccion(cmbModelo, "el Modelo de Auto")) return;
            if (!ClsValidacion.ValidarCodigoBarra(txtCodigoBarra.Text)) return;
            if (!ClsValidacion.ValidarServicio(txtServicio.Text)) return;

            try
            {
                // 2. Validación de existencia por NOMBRE
                if (ExisteProductoPorNombre(txtNombre.Text.Trim()))
                {
                    MessageBox.Show("El nombre '" + txtNombre.Text + "' ya está registrado. Use uno diferente.",
                                    "Nombre Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtNombre.Focus();
                    return;
                }

                // 3. Validación de existencia por CÓDIGO DE BARRAS
                if (ExisteProductoPorCodigo(txtCodigoBarra.Text.Trim()))
                {
                    MessageBox.Show("El código de barras '" + txtCodigoBarra.Text + "' ya pertenece a otro producto, Por favor Ingresar otro.",
                                    "Código Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtCodigoBarra.Focus();
                    return;
                }

                // 4. Proceso de inserción (Si pasó todas las pruebas)
                ClsAgregarProducto logicaInsertar = new ClsAgregarProducto();

                string nombre = txtNombre.Text.Trim();
                int idMarca = (int)cmbMarca.SelectedValue;
                int idTipo = (int)cmbTipo.SelectedValue;
                int idModelo = (int)cmbModelo.SelectedValue;
                decimal precio = decimal.Parse(txtPrecio.Text);
                string servicio = txtServicio.Text.Trim();
                string codBarra = txtCodigoBarra.Text.Trim();

                logicaInsertar.EjecutarInsercion(nombre, idMarca, idTipo, idModelo, precio, servicio, codBarra);

                MessageBox.Show("Producto guardado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ExisteProducto(string nombre)
        {
            ClsConexion conexion = new ClsConexion();
            int count = 0;
            try
            {
                conexion.AbrirConexion();
                string sql = "SELECT COUNT(*) FROM Producto WHERE nombre_producto = @nombre";
                using (SqlCommand cmd = new SqlCommand(sql, conexion.Conectar))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    count = (int)cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar duplicados: " + ex.Message);
            }
            finally
            {
                conexion.Cerrar();
            }
            return count > 0;
        }

        private bool ExisteProductoPorNombre(string nombre)
        {
            ClsConexion conexion = new ClsConexion();
            int count = 0;
            try
            {
                conexion.AbrirConexion();
                string sql = "SELECT COUNT(*) FROM Producto WHERE nombre_producto = @nombre";
                using (SqlCommand cmd = new SqlCommand(sql, conexion.Conectar))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    count = (int)cmd.ExecuteScalar();
                }
            }
            catch { throw; }
            finally { conexion.Cerrar(); }
            return count > 0;
        }

        private bool ExisteProductoPorCodigo(string codigo)
        {
            ClsConexion conexion = new ClsConexion();
            int count = 0;
            try
            {
                conexion.AbrirConexion();
                // Buscamos específicamente en la columna codigo_barra
                string sql = "SELECT COUNT(*) FROM Producto WHERE codigo_barra = @codigo";
                using (SqlCommand cmd = new SqlCommand(sql, conexion.Conectar))
                {
                    cmd.Parameters.AddWithValue("@codigo", codigo);
                    count = (int)cmd.ExecuteScalar();
                }
            }
            catch { throw; }
            finally { conexion.Cerrar(); }
            return count > 0;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void AgregarProducto_Load(object sender, EventArgs e)
        {
            LlenarTodosLosCombos();
        }

        private void LlenarTodosLosCombos()
        {
            ClsLlenarCombo llenar = new ClsLlenarCombo();

            try
            {
                cmbMarca.DataSource = llenar.ObtenerDatosCombo("Marca");
                cmbMarca.DisplayMember = "nombre_marca";
                cmbMarca.ValueMember = "id_marca_producto";

                cmbTipo.DataSource = llenar.ObtenerDatosCombo("Tipo");
                cmbTipo.DisplayMember = "descripcion_forma_pago";
                cmbTipo.ValueMember = "id_tipo_producto";

                cmbModelo.DataSource = llenar.ObtenerDatosCombo("Modelo");
                cmbModelo.DisplayMember = "nombre_modelo_auto";
                cmbModelo.ValueMember = "id_modelo_auto";

                cmbMarca.SelectedIndex = -1;
                cmbTipo.SelectedIndex = -1;
                cmbModelo.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
    }
}
