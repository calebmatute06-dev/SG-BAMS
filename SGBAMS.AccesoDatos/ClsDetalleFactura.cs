using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS.Facturas
{
    public class ClsDetalleFactura : ClsRepositorioBaseDatos
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
