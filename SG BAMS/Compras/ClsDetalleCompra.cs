using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SG_BAMS.ProductoInventario
{
    internal class ClsDetalleCompra
    {
        private ClsConexion conexion = new ClsConexion();

        public DataTable ListarProductosDeCompra(int id)
        {
            DataTable dt = new DataTable();
            try
            {
                conexion.AbrirConexion();
                // Usamos INNER JOIN para traer el nombre del producto desde la tabla Producto
                string query = @"SELECT 
                                    cp.id_producto AS [ID], 
                                    p.nombre_producto AS [Producto], 
                                    cp.cantidad AS [Cantidad], 
                                    cp.precio_costo_unitario AS [Precio],
                                    (cp.cantidad * cp.precio_costo_unitario) AS [Subtotal]
                                 FROM Compra_producto cp
                                 INNER JOIN Producto p ON cp.id_producto = p.id_producto
                                 WHERE cp.id_compra = @id";

                using (SqlCommand cmd = new SqlCommand(query, conexion.Conectar))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar productos: " + ex.Message);
            }
            finally
            {
                conexion.Cerrar();
            }
            return dt;
        }
    }
}