using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS.Administracion_de_BAMS.ModeloAuto
{
    /// <summary>
    /// Clase encargada de gestionar las operaciones de acceso a datos para los modelos de automóviles.
    /// </summary>
    /// <seealso cref="SG_BAMS.ClsConexion" />
    internal class clsModeloAuto : ClsConexion
    {
        /// <summary>
        /// Recupera todos los modelos de autos registrados a través de una vista detallada de forma asíncrona.
        /// </summary>
        /// <returns>Un DataTable que contiene el listado de modelos.</returns>
        /// <exception cref="System.Exception">Lanzada cuando ocurre un error en la consulta SQL.</exception>
        public async Task<DataTable> LeerModelosAsync()
        {
            DataTable tabla = new DataTable();
            try
            {
                AbrirConexion();
                string query = "SELECT * FROM v_DetalleModelosAuto";

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
                throw new Exception("Error al obtener los modelos de auto: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
            return tabla;
        }

        /// <summary>
        /// Inserta un nuevo modelo de automóvil en la base de datos de forma asíncrona.
        /// </summary>
        /// <param name="nombreModelo">El nombre descriptivo del modelo de auto.</param>
        /// <returns>True si la operación afectó al menos una fila; de lo contrario, False.</returns>
        /// <exception cref="System.Exception">Lanzada cuando ocurre un error durante la ejecución del procedimiento almacenado.</exception>
        public async Task<bool> InsertarModeloAutoAsync(string nombreModelo)
        {
            try
            {
                AbrirConexion();

                using (SqlCommand cmd = new SqlCommand("PA_insertar_modelo_auto", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre_modelo_auto", nombreModelo);

                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el modelo de auto: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }


        /// <summary>
        /// Modifica el nombre de un modelo de automóvil existente de forma asíncrona.
        /// </summary>
        /// <param name="id">El identificador único del modelo de auto.</param>
        /// <param name="nuevoNombre">El nuevo nombre que se asignará al modelo.</param>
        /// <returns>True si la actualización fue exitosa; de lo contrario, False.</returns>
        /// <exception cref="System.Exception">Lanzada cuando ocurre un error en la comunicación con la base de datos.</exception>
        public async Task<bool> ModificarModeloAutoAsync(int id, string nuevoNombre)
        {
            try
            {
                AbrirConexion();

                using (SqlCommand cmd = new SqlCommand("PA_actualizar_modelo_de_auto", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_modelo_auto", id);
                    cmd.Parameters.AddWithValue("@nombre_modelo_auto", nuevoNombre);

                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar el modelo de auto: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }
    }
}