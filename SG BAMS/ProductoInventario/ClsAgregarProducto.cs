using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SG_BAMS.ProductoInventario
{
    internal class ClsAgregarProducto
    {
        private ClsConexion conexion = new ClsConexion();

        public void EjecutarInsercion(string nombre, int idMarca, int idTipo, int idModelo, decimal precio, string codBarra, int idProveedor, int stock)
        {
            try
            {
                conexion.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("PA_insertar_producto", conexion.Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@nombre_producto", nombre);
                    cmd.Parameters.AddWithValue("@id_marca_producto", idMarca);
                    cmd.Parameters.AddWithValue("@id_tipo_producto", idTipo);
                    cmd.Parameters.AddWithValue("@id_modelo_auto", idModelo);
                    cmd.Parameters.Add("@precio_venta", SqlDbType.Money).Value = precio;
                    cmd.Parameters.AddWithValue("@codigo_barra", codBarra);
                    cmd.Parameters.AddWithValue("@id_proveedor", idProveedor);
                    cmd.Parameters.AddWithValue("@stock", stock);

                    cmd.ExecuteNonQuery();
                }
            }
            finally { conexion.Cerrar(); }
        }

        public bool ExisteProductoMarcaProveedor(string nombre, int idMarca, int idProveedor)
        {
            int conteo = 0;
            string sql = @"SELECT COUNT(*) 
                           FROM Producto p
                           INNER JOIN Proveedor_Producto pp ON p.id_producto = pp.id_producto
                           WHERE p.nombre_producto = @nombre 
                           AND p.id_marca_producto = @idMarca 
                           AND pp.id_proveedor = @idProv";

            try
            {
                conexion.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand(sql, conexion.Conectar))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@idMarca", idMarca);
                    cmd.Parameters.AddWithValue("@idProv", idProveedor);
                    conteo = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            finally { conexion.Cerrar(); }

            return conteo > 0;
        }

        public bool ExisteCodigoBarra(string codigo)
        {
            int conteo = 0;
            string sql = "SELECT COUNT(*) FROM Producto WHERE codigo_barra = @codigo";

            try
            {
                conexion.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand(sql, conexion.Conectar))
                {
                    cmd.Parameters.AddWithValue("@codigo", codigo);
                    conteo = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            finally { conexion.Cerrar(); }

            return conteo > 0;
        }
    }
}