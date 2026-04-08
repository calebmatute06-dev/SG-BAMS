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
    internal class ClsUltimasVentas : ClsConexion
    {
        /// <summary>
        /// Obtiene las ventas recientes.
        /// </summary>
        /// <returns></returns>
        public async Task<DataTable> ObtenerVentasRecientes()
        {
            DataTable tablaVentas = new DataTable();
            try
            {
                AbrirConexion();


                string sqlQuery = "SELECT * FROM vista_ultimas_ventas";

                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, Conectar))
                {
                    using (SqlDataReader sqlReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        tablaVentas.Load(sqlReader);
                    }
                }
            }
            catch (Exception)
            {

                return null;
            }
            finally
            {
                Cerrar();
            }
            return tablaVentas;
        }
    }
}