using System;
using System.Data;
using Microsoft.Data.SqlClient;
using SG_BAMS.AccesoDatos;

namespace SG_BAMS.Bitacora
{
    /// <summary>
    /// Implementación de acceso a datos de la bitácora sobre SQL Server.
    /// Única responsabilidad: obtener los registros desde la base de datos,
    /// sin conocer nada sobre controles de interfaz gráfica.
    /// </summary>
    public class BitacoraRepository : ClsRepositorioBaseDatos, IBitacoraRepository
    {
        public DataTable ObtenerRegistros()
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Bitacora_Listar", Conectar))
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
                throw new ApplicationException("Error al cargar los datos de la bitácora.", ex);
            }
            finally
            {
                Cerrar();
            }
        }
    }
}