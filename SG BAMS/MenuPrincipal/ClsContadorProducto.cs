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

                // Contamos los productos que están marcados como activos (id_estado = 1)
                string sqlQuery = "SELECT COUNT(*) FROM Producto WHERE id_estado = 1";

                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, Conectar))
                {
                    // Ejecución asíncrona para la base de datos en Somee
                    object resultadoConsulta = await sqlCommand.ExecuteScalarAsync();

                    return resultadoConsulta != null ? Convert.ToInt32(resultadoConsulta) : 0;
                }
            }
            catch (Exception)
            {
                // En caso de error de red con el servidor, retornamos -1
                return -1;
            }
            finally
            {
                Cerrar();
            }
        }
    }
}
