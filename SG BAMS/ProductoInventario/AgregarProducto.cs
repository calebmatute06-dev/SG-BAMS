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
            if (!ClsValidacion.ValidarNombre(txtNombre.Text)) return;
            if (!ClsValidacion.ValidarPrecio(txtPrecio.Text)) return;

            // Validaciones de Selección (Combos)
            if (!ClsValidacion.ValidarSeleccion(cmbMarca, "la Marca")) return;
            if (!ClsValidacion.ValidarSeleccion(cmbTipo, "el Tipo de Producto")) return;
            if (!ClsValidacion.ValidarSeleccion(cmbModelo, "el Modelo de Auto")) return;
            if (!ClsValidacion.ValidarSeleccion(cmbProveedor, "el Proveedor")) return; // OBLIGATORIO

            if (!ClsValidacion.ValidarCodigoBarra(txtCodigoBarra.Text)) return;

            try
            {
                // 2. Captura de variables necesarias para la validación de regla de negocio
                string nombre = txtNombre.Text.Trim();
                int idMarca = (int)cmbMarca.SelectedValue;
                int idProveedor = (int)cmbProveedor.SelectedValue;
                string codBarra = txtCodigoBarra.Text.Trim();

                // 3. NUEVA REGLA: Validar combinación Nombre + Marca + Proveedor
                // Pasamos '0' porque al ser un producto nuevo no necesitamos excluir ningún ID
                if (!ClsValidacion.ValidarExistencia(0, nombre, idMarca, idProveedor))
                {
                    txtNombre.Focus();
                    return;
                }

                // 4. VALIDACIÓN DE CÓDIGO DE BARRAS (Sigue siendo universalmente único)
                if (ExisteProductoPorCodigo(codBarra))
                {
                    MessageBox.Show("El código de barras ya pertenece a otro producto en el sistema.",
                                    "Código Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtCodigoBarra.Focus();
                    return;
                }

                // 5. Instancia de la lógica e Inserción
                ClsAgregarProducto logicaInsertar = new ClsAgregarProducto();

                int idTipo = (int)cmbTipo.SelectedValue;
                int idModelo = (int)cmbModelo.SelectedValue;
                decimal precio = decimal.Parse(txtPrecio.Text);

                // Ejecución con los 7 parámetros
                logicaInsertar.EjecutarInsercion(nombre, idMarca, idTipo, idModelo, precio, codBarra, idProveedor);

                MessageBox.Show("¡Producto guardado exitosamente con su proveedor!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                // Carga de Marcas
                cmbMarca.DataSource = llenar.ObtenerDatosCombo("Marca");
                cmbMarca.DisplayMember = "nombre_marca";
                cmbMarca.ValueMember = "id_marca_producto";

                // Carga de Tipos
                cmbTipo.DataSource = llenar.ObtenerDatosCombo("Tipo");
                cmbTipo.DisplayMember = "descripcion_forma_pago";
                cmbTipo.ValueMember = "id_tipo_producto";

                // Carga de Modelos
                cmbModelo.DataSource = llenar.ObtenerDatosCombo("Modelo");
                cmbModelo.DisplayMember = "nombre_modelo_auto";
                cmbModelo.ValueMember = "id_modelo_auto";

                // --- ADAPTACIÓN: Carga de Proveedores ---
                cmbProveedor.DataSource = llenar.ObtenerDatosCombo("Proveedor");
                cmbProveedor.DisplayMember = "nombre_proveedor"; // Lo que el usuario lee
                cmbProveedor.ValueMember = "id_proveedor";     // El ID para la tabla intermedia

                // Reinicio de índices para que aparezcan vacíos al inicio
                cmbMarca.SelectedIndex = -1;
                cmbTipo.SelectedIndex = -1;
                cmbModelo.SelectedIndex = -1;
                cmbProveedor.SelectedIndex = -1; // Nuevo reinicio
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
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
