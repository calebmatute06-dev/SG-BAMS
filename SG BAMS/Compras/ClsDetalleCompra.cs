using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SG_BAMS.ProductoInventario
{
    /// <summary>
    /// Clase para obtener los detalles de los productos de una compra específica.
    /// </summary>
    internal class ClsDetalleCompra
    {
        /// <summary>
        /// Instancia de conexión a la base de datos.
        /// </summary>
        private ClsConexion conexion = new ClsConexion();

        /// <summary>
        /// Obtiene los productos asociados a una compra.
        /// </summary>
        /// <param name="id">Identificador de la compra.</param>
        /// <returns>Un <see cref="DataTable"/> con los productos de la compra, cantidad, precio y subtotal.</returns>
        /// <exception cref="Exception">Si ocurre un error durante la consulta.</exception>
        public DataTable ListarProductosDeCompra(int id)
        {
            DataTable dt = new DataTable();
            try
            {
                conexion.AbrirConexion();

                string query = @"
                    SELECT 
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

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
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