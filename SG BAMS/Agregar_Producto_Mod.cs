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

        // BOTÓN ACEPTAR: Guarda directamente en la BD y cierra
        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            if (cmbProductos.SelectedIndex == -1 || numCantidad.Value <= 0 || string.IsNullOrWhiteSpace(txtPrecio.Text))
            {
                MessageBox.Show("Por favor complete todos los campos.");
                return;
            }

            try
            {
                // Realizamos el INSERT directo para que actue el TRIGGER de inventario
                string sql = "INSERT INTO Compra_producto (id_compra, id_producto, cantidad, precio_costo_unitario) " +
                             "VALUES (@idC, @idP, @cant, @prec)";

                conexion.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand(sql, conexion.Conectar))
                {
                    cmd.Parameters.AddWithValue("@idC", IdCompraActual);
                    cmd.Parameters.AddWithValue("@idP", cmbProductos.SelectedValue);
                    cmd.Parameters.AddWithValue("@cant", (int)numCantidad.Value);
                    cmd.Parameters.AddWithValue("@prec", Convert.ToDecimal(txtPrecio.Text));

                    cmd.ExecuteNonQuery();
                }

                // Guardamos los datos en las propiedades por si el form principal los necesita
                IdSeleccionado = cmbProductos.SelectedValue.ToString();
                NombreSeleccionado = cmbProductos.Text;
                CantidadSeleccionada = (int)numCantidad.Value;
                PrecioSeleccionado = Convert.ToDecimal(txtPrecio.Text);

                MessageBox.Show("Producto agregado y stock actualizado con éxito.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar producto: " + ex.Message);
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