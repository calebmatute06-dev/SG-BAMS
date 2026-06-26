using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

namespace SG_BAMS.Facturas
{
    /// <summary>
    /// Clase para visualizar productos de factura usando solo PA.
    /// </summary>
    internal class ClsVerFacturaProducto : ClsConexion
    {
        public async Task<DataTable> VerFacturasProducto(int idfacturas)
        {
            DataTable tablaFacProd = new DataTable();
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Factura_ProductosPorFactura", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idfactura", idfacturas);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        tablaFacProd.Load(reader);
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