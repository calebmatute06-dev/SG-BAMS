using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Data;

namespace SG_BAMS.Facturas
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="SG_BAMS.ClsConexion" />
    internal class ClsVerFactura:ClsConexion
    {

        /// <summary>
        /// Vers the facturas.
        /// </summary>
        /// <returns></returns>
        public async Task<DataTable> VerFacturas()
        {
            DataTable tablaFac = new DataTable();

            try 
            {
                AbrirConexion();

                string sqlQuery = "SELECT * FROM vista_detalle_facturas ";

                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery,Conectar))
                {
                    using (SqlDataReader sqlReader = await sqlCommand.ExecuteReaderAsync())
                    { 
                        tablaFac.Load(sqlReader);
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
