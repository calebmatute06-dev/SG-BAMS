using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace SG_BAMS.MenuPrincipal
{
    internal class ClsUltimasVentas : ClsConexion
    {
        public async Task<DataTable> ObtenerVentasRecientes()
        {
            DataTable tablaVentas = new DataTable();
            try
            {
                AbrirConexion();

                // Llamada a la vista en snake_case
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
                // En caso de error de red con Somee, devolvemos null
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
