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
    internal class ClsVerFactura:ClsConexion
    {

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
            catch (Exception)
            {
                return null;
            }
            finally
            {
                Cerrar();
            }
            return tablaFac;
        }


    }
}
