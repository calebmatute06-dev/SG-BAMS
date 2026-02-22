using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace SG_BAMS.MenuPrincipal
{
    internal class ClsContadorProducto : ClsConexion
    {
        public async Task<int> ObtenerTotalProductos()
        {
            try
            {
                AbrirConexion();

                
                string sqlQuery = "SELECT COUNT(*) FROM Producto WHERE id_estado = 1";

                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, Conectar))
                {
                   
                    object resultadoConsulta = await sqlCommand.ExecuteScalarAsync();

                    return resultadoConsulta != null ? Convert.ToInt32(resultadoConsulta) : 0;
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
