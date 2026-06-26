using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace SG_BAMS
{
    /// <summary>
    /// Clase para obtener datos del gráfico de ventas usando solo Procedimientos Almacenados.
    /// </summary>
    internal class ClsGraficoVentas : ClsConexion
    {
        public async Task<DataTable> ObtenerProductosMasVendidos()
        {
            DataTable tablaVentas = new DataTable();
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_vista_productos_mas_vendidos", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        tablaVentas.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener ventas para el gráfico: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
            return tablaVentas;
        }
    }
}