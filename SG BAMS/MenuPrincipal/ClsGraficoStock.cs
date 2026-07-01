using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace SG_BAMS.MenuPrincipal
{
    /// <summary>
    /// Clase para obtener datos del gráfico de stock usando solo Procedimientos Almacenados.
    /// </summary>
    internal class ClsGraficoStock : ClsRepositorioBaseDatos
    {
        public async Task<DataTable> ObtenerDatosGrafico()
        {
            DataTable tablaDatos = new DataTable();
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_vista_stock_productos", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        tablaDatos.Load(reader);
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