using System;
using System.Data;
using Microsoft.Data.SqlClient;
using SG_BAMS.ComprasContratos;
using SG_BAMS.AccesoDatos;

namespace SG_BAMS.ProductoInventario
{
    /// <summary>
    /// Clase para obtener detalles de compra usando solo PA.
    /// Es la única fuente de esta consulta: ClsModificarCompras delega
    /// aquí en lugar de reimplementar la misma consulta (antes ambas
    /// clases llamaban a sp_Compra_ListarProductos por separado).
    /// </summary>
    internal class ClsDetalleCompra : ClsRepositorioBaseDatos, IDetalleCompraRepository
    {
        public DataTable ListarProductosDeCompra(int idCompra)
        {
            DataTable dt = new DataTable();
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Compra_ListarProductos", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", idCompra);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar productos: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
            return dt;
        }
    }
}
