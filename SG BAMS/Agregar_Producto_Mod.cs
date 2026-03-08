using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace SG_BAMS
{
    public partial class Agregar_Producto_Mod : Form
    {
        // Esta propiedad recibirá el ID de la compra desde el formulario Modificar_datos_Compra_
        public string IdCompraActual { get; set; }

        public string IdSeleccionado { get; set; }
        public string NombreSeleccionado { get; set; }
        public int CantidadSeleccionada { get; set; }
        public decimal PrecioSeleccionado { get; set; }

        private ClsConexion conexion = new ClsConexion();

        public Agregar_Producto_Mod()
        {
            InitializeComponent();
        }

        private void Agregar_Producto_Mod_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dtProductos = ObtenerDatosCombo("Producto");
                cmbProductos.DataSource = dtProductos;
                cmbProductos.DisplayMember = "nombre_producto";
                cmbProductos.ValueMember = "id_producto";

                cmbProductos.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbProductos.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmbProductos.DropDownStyle = ComboBoxStyle.DropDown;
                cmbProductos.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public DataTable ObtenerDatosCombo(string tabla)
        {
            DataTable dt = new DataTable();
            string query = "";
            switch (tabla)
            {
                case "Marca": query = "SELECT id_marca_producto, nombre_marca FROM Marca_producto"; break;
                case "Tipo": query = "SELECT id_tipo_producto, descripcion_forma_pago FROM Tipo_producto"; break;
                case "Modelo": query = "SELECT id_modelo_auto, nombre_modelo_auto FROM Modelo_de_auto"; break;
                case "Estado": query = "SELECT id_estado, descripcion_estado FROM Estado"; break;
                case "Producto": query = "SELECT id_producto, nombre_producto FROM Producto WHERE id_estado = 1"; break;
            }

            try
            {
                conexion.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand(query, conexion.Conectar))
                {
                    using (SqlDataReader leer = cmd.ExecuteReader()) { dt.Load(leer); }
                }
            }
            catch (Exception ex) { throw new Exception("Error al llenar combo " + tabla + ": " + ex.Message); }
            finally { conexion.Cerrar(); }
            return dt;
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            if (cmbProductos.SelectedValue == null || cmbProductos.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, seleccione un producto válido de la lista.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (numCantidad.Value <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor a cero.", "Cantidad Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numCantidad.Focus();
                return;
            }

            string precioTexto = txtPrecio.Text.Trim().Replace(",", ".");

            if (!double.TryParse(precioTexto, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out _) || string.IsNullOrWhiteSpace(precioTexto))
            {
                MessageBox.Show("El precio debe ser un valor numérico válido (ejemplo: 15.50).", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecio.Focus();
                return;
            }

            try
            {
                string sql = "INSERT INTO Compra_producto (id_compra, id_producto, cantidad, precio_costo_unitario) " +
                             "VALUES (@idC, @idP, @cant, @prec)";

                // Convertimos el string a decimal usando la misma cultura invariable
                decimal precioFinal = decimal.Parse(precioTexto, System.Globalization.CultureInfo.InvariantCulture);

                conexion.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand(sql, conexion.Conectar))
                {
                    cmd.Parameters.AddWithValue("@idC", IdCompraActual);
                    cmd.Parameters.AddWithValue("@idP", cmbProductos.SelectedValue);
                    cmd.Parameters.AddWithValue("@cant", (int)numCantidad.Value);
                    cmd.Parameters.AddWithValue("@prec", precioFinal);

                    cmd.ExecuteNonQuery();
                }

                IdSeleccionado = cmbProductos.SelectedValue.ToString();
                NombreSeleccionado = cmbProductos.Text;
                CantidadSeleccionada = (int)numCantidad.Value;
                PrecioSeleccionado = precioFinal;

                MessageBox.Show("Producto agregado y stock actualizado con éxito.", "BAMS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar producto: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { conexion.Cerrar(); }
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}