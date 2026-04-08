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
    internal class ClsGraficoStock : ClsConexion
    {
        /// <summary>
        /// Obtiene los datos para el gráfico.
        /// </summary>
        /// <returns></returns>
        public async Task<DataTable> ObtenerDatosGrafico()
        {
            DataTable tablaDatos = new DataTable();
            try
            {
                AbrirConexion();
                string sqlQuery = "SELECT * FROM vista_stock_productos";

                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, Conectar))
                {
                    using (SqlDataReader sqlReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        tablaDatos.Load(sqlReader);
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
            return tablaDatos;
        }
    }
}