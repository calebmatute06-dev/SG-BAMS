using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SG_BAMS.Proveedor
{
    /// <summary>
    /// Implementación de acceso a datos para el catálogo de estados de proveedor.
    /// </summary>
    internal class EstadoRepository : ClsRepositorioBaseDatos, IEstadoRepository
    {
        /// <inheritdoc />
        public DataTable ObtenerEstados()
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_vista_estado", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al cargar el catálogo de estados.", ex);
            }
            finally
            {
                Cerrar();
            }
        }
    }
}
