using System;
using System.Data;
using Microsoft.Data.SqlClient;
using SG_BAMS.ComprasContratos;
using SG_BAMS.AccesoDatos;

namespace SG_BAMS.ProductoInventario
{
    /// <summary>
    /// Clase para mostrar compras usando solo Procedimientos Almacenados.
    /// </summary>
   public class ClsMostrarCompras : ClsRepositorioBaseDatos, IMostrarComprasRepository
    {
        public DataTable ListarCompras()
        {
            DataTable dt = new DataTable();
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Vista_ListadoCompras", Conectar))
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
                Cerrar();
            }
            return dt;
        }
    }
}