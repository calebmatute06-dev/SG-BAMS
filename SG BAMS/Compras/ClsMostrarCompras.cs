using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SG_BAMS.ProductoInventario
{
    /// <summary>
    /// Clase para mostrar compras usando solo Procedimientos Almacenados.
    /// </summary>
    internal class ClsMostrarCompras
    {
        private readonly ClsRepositorioBaseDatos conexion = new ClsRepositorioBaseDatos();

        public DataTable ListarCompras()
        {
            DataTable dt = new DataTable();
            try
            {
                conexion.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Vista_ListadoCompras", conexion.Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las compras desde la base de datos: " + ex.Message);
            }
            finally
            {
                conexion.Cerrar();
            }
            return dt;
        }
    }
}