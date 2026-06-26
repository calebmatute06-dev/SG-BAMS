using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SG_BAMS.ProductoInventario
{
    /// <summary>
    /// Clase para visualizar productos usando solo Procedimientos Almacenados.
    /// </summary>
    internal class ClsVerProducto
    {
        private readonly ClsConexion conexion = new ClsConexion();

        /// <summary>
        /// Muestra el listado completo de productos usando PA.
        /// </summary>
        public DataTable MostrarProductosCompleto()
        {
            DataTable tabla = new DataTable();
            try
            {
                conexion.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Vista_Productos_Detallada", conexion.Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader leer = cmd.ExecuteReader())
                    {
                        tabla.Load(leer);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista: " + ex.Message);
            }
            finally
            {
                conexion.Cerrar();
            }
            return tabla;
        }

        /// <summary>
        /// Busca productos por filtro usando PA.
        /// </summary>
        public DataTable BuscarProductos(string filtro)
        {
            ClsConexion conexion = new ClsConexion();
            DataTable dt = new DataTable();
            try
            {
                conexion.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_BuscarProductos", conexion.Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@filtro", filtro);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar procedimiento: " + ex.Message);
            }
            finally
            {
                conexion.Cerrar();
            }
            return dt;
        }
    }
}