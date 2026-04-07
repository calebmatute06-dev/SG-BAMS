using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS.Facturas
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="SG_BAMS.ClsConexion" />
    internal class ClsVerFacturaProducto:ClsConexion
    {
        /// <summary>
        /// Vers the facturas producto.
        /// </summary>
        /// <param name="idfacturas">The idfacturas.</param>
        /// <returns></returns>
        public async Task<DataTable> VerFacturasProducto(int idfacturas)
        {
            DataTable tablaFacProd = new DataTable();

            try
            {
                AbrirConexion();

                string sqlQuery = "SELECT * FROM vista_productos_factura WHERE ID_Factura = @idfacturas";

                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, Conectar))
                {
                    sqlCommand.Parameters.AddWithValue("@idfacturas", idfacturas);

                    using (SqlDataReader sqlReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        tablaFacProd.Load(sqlReader);
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
            return tablaFacProd;
        }

       



    }
}
