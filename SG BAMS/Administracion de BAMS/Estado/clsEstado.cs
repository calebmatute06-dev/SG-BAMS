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
    /// Clase que gestiona las operaciones de base de datos para los estados.
    /// </summary>
    /// <seealso cref="SG_BAMS.ClsConexion" />
    internal class clsEstado : ClsConexion
    {
        /// <summary>
        /// Lee los estados de forma asíncrona desde la base de datos.
        /// </summary>
        /// <returns>Un DataTable con los registros de los estados.</returns>
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
        /// Inserta un nuevo estado de forma asíncrona.
        /// </summary>
        /// <param name="descripcion">La descripción del estado.</param>
        /// <returns>Verdadero si la inserción fue exitosa; de lo contrario, falso.</returns>
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
        /// Modifica un estado existente de forma asíncrona.
        /// </summary>
        /// <param name="id">El identificador del estado.</param>
        /// <param name="nuevaDescripcion">La nueva descripción para el estado.</param>
        /// <returns>Verdadero si la modificación fue exitosa; de lo contrario, falso.</returns>
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