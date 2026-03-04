using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient; // Usa solo esta para evitar conflictos con System.Data.SqlClient

namespace SG_BAMS
{
    public class ClsVerCompra1
    {
        // Instanciamos tu clase de conexión
        private ClsConexion conexionBase = new ClsConexion();

        public void LlenarDetalleCompra(DataGridView dgv, int idCompra, Label lblTotal)
        {
            try
            {
                // 1. Abrimos la conexión usando tu método
                conexionBase.AbrirConexion();

                // 2. Definimos la consulta basada en tu tabla Compra_producto
                string query = @"SELECT 
                                    P.nombre_producto AS [Producto], 
                                    CP.cantidad AS [Cantidad], 
                                    CP.precio_costo_unitario AS [Precio Costo],
                                    (CP.cantidad * CP.precio_costo_unitario) AS [Subtotal]
                                 FROM Compra_producto CP
                                 INNER JOIN Producto P ON CP.id_producto = P.id_producto
                                 WHERE CP.id_compra = @id_compra";

                // 3. Importante: Usamos conexionBase.Conectar que ya fue configurado en AbrirConexion()
                using (SqlCommand cmd = new SqlCommand(query, conexionBase.Conectar))
                {
                    cmd.Parameters.AddWithValue("@id_compra", idCompra);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // 4. Asignamos los datos al DataGridView
                    dgv.DataSource = dt;

                    // 5. Calculamos el total recorriendo el DataTable
                    decimal total = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        total += Convert.ToDecimal(row["Subtotal"]);
                    }
                    lblTotal.Text = "L " + total.ToString("N2");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message);
            }
            finally
            {
                // 6. Cerramos la conexión siempre
                conexionBase.Cerrar();
            }
        }
    }
}