using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace SG_BAMS.MenuPrincipal
{

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="SG_BAMS.ClsConexion" />
    internal class ClsContadorDeuda : ClsConexion
    {


        /// <summary>
        /// Obteners the total deudores.
        /// </summary>
        /// <returns></returns>
        public async Task<int> ObtenerTotalDeudores()
        {
            try
            {
                AbrirConexion();

                
                string sqlQuery = "SELECT COUNT(DISTINCT id_cliente) FROM Deuda WHERE id_estado = 1";

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