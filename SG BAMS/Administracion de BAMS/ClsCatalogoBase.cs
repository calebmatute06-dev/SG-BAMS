using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading.Tasks;
using SG_BAMS.AccesoDatos;

namespace SG_BAMS.Administracion_de_BAMS
{
    

    /// <summary>
    /// Clase base para catálogos simples (Leer, Insertar, Modificar).
    /// SRP: única responsabilidad — CRUD genérico parametrizado por SPs.
    /// OCP: abierto para extensión (subclases definen los SPs);
    ///      cerrado para modificación (la lógica de conexión no cambia).
    /// DIP: implementa ICatalogoRepository para que los formularios
    ///      dependan de la abstracción, no de esta clase concreta.
    /// </summary>
    internal abstract class ClsCatalogoBase : ClsRepositorioBaseDatos, ICatalogoRepository
    {
        // ---- Propiedades que cada subclase debe definir ----
        protected abstract string SpLeer { get; }
        protected abstract string SpInsertar { get; }
        protected abstract string SpModificar { get; }
        protected abstract string ParamDescripcion { get; }
        protected abstract string ParamId { get; }

        /// <summary>Nombre legible del catálogo para mensajes de error.</summary>
        protected abstract string NombreCatalogo { get; }

        // ---- Implementación de ICatalogoRepository ----

        /// <inheritdoc/>
        public async Task<DataTable> LeerAsync()
        {
            DataTable tabla = new DataTable();
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand(SpLeer, Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        tabla.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener {NombreCatalogo}: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
            return tabla;
        }

        /// <inheritdoc/>
        public async Task<bool> InsertarAsync(string descripcion)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand(SpInsertar, Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(ParamDescripcion, descripcion);
                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al insertar {NombreCatalogo}: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }

        /// <inheritdoc/>
        public async Task<bool> ModificarAsync(int id, string nuevaDescripcion)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand(SpModificar, Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(ParamId, id);
                    cmd.Parameters.AddWithValue(ParamDescripcion, nuevaDescripcion);
                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al modificar {NombreCatalogo}: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }
    }
}