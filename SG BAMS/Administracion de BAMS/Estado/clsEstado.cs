using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS.Administracion_de_BAMS.Estado
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="SG_BAMS.ClsConexion" />
    internal class clsEstado : ClsConexion
    {
        /// <summary>
        /// Leers the estados asynchronous.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al cargar los estados: " + ex.Message</exception>
        public async Task<DataTable> LeerEstadosAsync()
        {
            DataTable tabla = new DataTable();
            try
            {
                AbrirConexion();

                string query = "SELECT * FROM v_DetalleEstados";

                using (SqlCommand cmd = new SqlCommand(query, Conectar))
                {
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        tabla.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar los estados: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
            return tabla;
        }

        /// <summary>
        /// Insertars the estado asynchronous.
        /// </summary>
        /// <param name="descripcion">The descripcion.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al insertar el estado: " + ex.Message</exception>
        public async Task<bool> InsertarEstadoAsync(string descripcion)
        {
            try
            {

                AbrirConexion();

                using (SqlCommand cmd = new SqlCommand("PA_insertar_estado", Conectar))
                {

                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@descripcion_estado", descripcion);

                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el estado: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }

        /// <summary>
        /// Modificars the estado asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="nuevaDescripcion">The nueva descripcion.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al modificar el estado: " + ex.Message</exception>
        public async Task<bool> ModificarEstadoAsync(int id, string nuevaDescripcion)
        {
            try
            {

                AbrirConexion();

                using (SqlCommand cmd = new SqlCommand("PA_actualizar_estado", Conectar))
                {

                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@id_estado", id);
                    cmd.Parameters.AddWithValue("@descripcion_estado", nuevaDescripcion);

                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar el estado: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }

    }
}
