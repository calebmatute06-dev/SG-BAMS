using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SG_BAMS.ProductoInventario
{
    /// <summary>
    /// Clase para agregar productos usando solo Procedimientos Almacenados.
    /// </summary>
    internal class ClsAgregarProducto : ClsRepositorioBaseDatos
    {
        

        public void EjecutarInsercion(string nombre, int idMarca, int idTipo, int idModelo,
            decimal precio, string codBarra, int idProveedor, int stock)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("PA_insertar_producto", Conectar))
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
            finally { Cerrar(); }
        }

        public bool ExisteProductoMarcaProveedor(string nombre, int idMarca, int idProveedor)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Producto_ExisteEnOtros", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@idMarca", idMarca);
                    cmd.Parameters.AddWithValue("@idProveedor", idProveedor);
                    cmd.Parameters.AddWithValue("@id", 0);
                    int conteo = Convert.ToInt32(cmd.ExecuteScalar());
                    return conteo > 0;
                }
            }
            finally { Cerrar(); }
        }

        public bool ExisteCodigoBarra(string codigo)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Producto_ExisteCodigoEnOtros", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codigo", codigo);
                    cmd.Parameters.AddWithValue("@id", 0);
                    int conteo = Convert.ToInt32(cmd.ExecuteScalar());
                    return conteo > 0;
                }
            }
            finally { Cerrar(); }
        }
    }
}