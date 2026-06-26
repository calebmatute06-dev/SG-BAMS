using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace SG_BAMS.Facturas
{
    /// <summary>
    /// Clase para visualizar facturas usando solo Procedimientos Almacenados.
    /// </summary>
    internal class ClsVerFactura : ClsConexion
    {
        public async Task<DataTable> VerFacturas()
        {
            DataTable tablaFac = new DataTable();
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_vista_detalle_facturas", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        tablaFac.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en la base de datos: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
            return tablaFac;
        }
    }
}