using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS.Cliente
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="SG_BAMS.ClsConexion" />
    internal class ClsVerCliente:ClsConexion
    {
        /// <summary>
        /// Vers the cliente tabla.
        /// </summary>
        /// <returns></returns>
        public async Task<DataTable> VerClienteTabla()
        {
            DataTable tablaC = new DataTable();

            try
            {
                AbrirConexion();

                string sqlQuery = "SELECT * FROM vista_lista_clientes ";

                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, Conectar))
                {
                    using (SqlDataReader sqlReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        tablaC.Load(sqlReader);
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
            return tablaC;
        }



    }
    
}
