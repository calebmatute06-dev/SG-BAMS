using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace SG_BAMS
{
    public partial class Agregar_Producto_Mod : Form
    {

        public string IdCompraActual { get; set; }

        public string IdSeleccionado { get; set; }
        public string NombreSeleccionado { get; set; }
        public int CantidadSeleccionada { get; set; }
        public decimal PrecioSeleccionado { get; set; }

        private ClsConexion conexion = new ClsConexion();

        private int _idProveedor;

        public Agregar_Producto_Mod(int idProv)
        {
            InitializeComponent();
            this._idProveedor = idProv;
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
            numCantidad.DecimalPlaces = 0;
            numCantidad.ThousandsSeparator = true;
        }

        public DataTable ObtenerDatosCombo(string tabla)
        {
            DataTable dt = new DataTable();
            string query = "";
            switch (tabla)
            {
                case "Producto":
                    // Filtramos por el proveedor que recibimos en el constructor
                    query = @"SELECT p.id_producto, p.nombre_producto 
                      FROM Producto p
                      INNER JOIN Proveedor_Producto pp ON p.id_producto = pp.id_producto
                      WHERE p.id_estado = 1 AND pp.id_proveedor = @idProv
                      ORDER BY p.nombre_producto ASC";
                    break;

                case "Marca": query = "SELECT id_marca_producto, nombre_marca FROM Marca_producto"; break;
                    // ... los demás casos se quedan igual
            }

            try
            {
                conexion.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand(query, conexion.Conectar))
                {
                    // ¡IMPORTANTE! Agregamos el parámetro para el filtro
                    if (tabla == "Producto")
                    {
                        cmd.Parameters.AddWithValue("@idProv", _idProveedor);
                    }

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
                MessageBox.Show("Por favor, seleccione un producto válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (numCantidad.Value <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor a cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numCantidad.Focus();
                return;
            }


            if (!ClsValidaciones.EsNumeroDecimalValido(txtPrecio, "El precio", out decimal precioFinal))
            {
                return;
            }

            try
            {
                conexion.AbrirConexion();


                string sqlCheck = "SELECT COUNT(*) FROM Compra_producto WHERE id_compra = @idC AND id_producto = @idP";
                using (SqlCommand cmdCheck = new SqlCommand(sqlCheck, conexion.Conectar))
                {
                    cmdCheck.Parameters.AddWithValue("@idC", IdCompraActual);
                    cmdCheck.Parameters.AddWithValue("@idP", cmbProductos.SelectedValue);

                    int existe = (int)cmdCheck.ExecuteScalar();
                    if (existe > 0)
                    {
                        MessageBox.Show("Este producto ya está incluido en la compra.\nModifique la cantidad en la pantalla anterior\n(dando doble click sobre la celda precio o cantidad).",
                                        "Producto Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }


                string sql = "INSERT INTO Compra_producto (id_compra, id_producto, cantidad, precio_costo_unitario) " +
                             "VALUES (@idC, @idP, @cant, @prec)";

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

                MessageBox.Show("Producto añadido correctamente a la compra.", "SG-BAMS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { conexion.Cerrar(); }
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnProductoNuevo_Click(object sender, EventArgs e)
        {
            AgregarProducto agregarProducto = new AgregarProducto();
            agregarProducto.Show();
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.PermitirNumerosYDecimales(sender, e);
        }

        private void txtPrecio_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtPrecio.Text))
            {
                if (decimal.TryParse(txtPrecio.Text, out decimal valor))
                {
                    txtPrecio.Text = valor.ToString("N2", CultureInfo.InvariantCulture);
                }
            }
        }

        private void numCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.ValidarSoloNumeros(e);
        }
    }
}