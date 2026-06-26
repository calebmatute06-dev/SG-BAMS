using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace SG_BAMS.MenuPrincipal
{
    /// <summary>
    /// Clase para obtener contadores de productos usando solo Procedimientos Almacenados.
    /// </summary>
    internal class ClsContadorProducto : ClsConexion
    {
        public async Task<int> ObtenerTotalProductos()
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Producto_TotalActivos", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    object resultado = await cmd.ExecuteScalarAsync();
                    return resultado != null ? Convert.ToInt32(resultado) : 0;
                }
            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                Cerrar();
            }
        }
    }
}