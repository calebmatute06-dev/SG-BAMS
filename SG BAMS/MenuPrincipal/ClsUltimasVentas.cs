using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace SG_BAMS.MenuPrincipal
{
    /// <summary>
    /// Clase para obtener últimas ventas usando solo Procedimientos Almacenados.
    /// </summary>
    internal class ClsUltimasVentas : ClsRepositorioBaseDatos
    {
        public async Task<DataTable> ObtenerVentasRecientes()
        {
            DataTable tablaVentas = new DataTable();
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_vista_ultimas_ventas", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        tablaVentas.Load(reader);
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