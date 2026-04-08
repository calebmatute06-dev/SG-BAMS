using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS.Administracion_de_BAMS.TipoProd
{
    /// <summary>
    /// Provee los métodos de acceso a datos para la gestión de las categorías o tipos de productos en el sistema.
    /// </summary>
    /// <seealso cref="SG_BAMS.ClsConexion" />
    internal class clsTipoProducto : ClsConexion
    {
        /// <summary>
        /// Obtiene el listado de todos los tipos de productos mediante una vista detallada de forma asíncrona.
        /// </summary>
        /// <returns>Un objeto DataTable con los registros de tipos de producto.</returns>
        /// <exception cref="System.Exception">Lanzada si ocurre un error en la consulta SQL.</exception>
        public async Task<DataTable> LeerTiposProductoAsync()
        {
            DataTable tabla = new DataTable();
            try
            {
                AbrirConexion();

                string query = "SELECT * FROM v_DetalleTipoProducto";

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
                throw new Exception("Error al obtener los tipos de producto: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
            return tabla;
        }

        /// <summary>
        /// Inserta un nuevo tipo de producto en la base de datos utilizando un procedimiento almacenado.
        /// </summary>
        /// <param name="descripcion">El nombre o descripción de la nueva categoría de producto.</param>
        /// <returns>True si la operación afectó al menos una fila; de lo contrario, False.</returns>
        /// <exception cref="System.Exception">Lanzada si el procedimiento almacenado falla.</exception>
        public async Task<bool> InsertarTipoProductoAsync(string descripcion)
        {
            try
            {
                AbrirConexion();

                using (SqlCommand cmd = new SqlCommand("PA_insertar_tipo_producto", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@descripcion_producto", descripcion);

                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar tipo de producto: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }

        /// <summary>
        /// Actualiza la descripción de un tipo de producto existente de forma asíncrona.
        /// </summary>
        /// <param name="id">El identificador único del tipo de producto.</param>
        /// <param name="nuevaDescripcion">La nueva descripción que se desea asignar.</param>
        /// <returns>True si la actualización fue exitosa; de lo contrario, False.</returns>
        /// <exception cref="System.Exception">Lanzada si ocurre un error durante la actualización.</exception>
        public async Task<bool> ModificarTipoProductoAsync(int id, string nuevaDescripcion)
        {
            try
            {
                AbrirConexion();

                using (SqlCommand cmd = new SqlCommand("PA_actualizar_tipo_producto", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id_tipo_producto", id);
                    cmd.Parameters.AddWithValue("@descripcion_producto", nuevaDescripcion);

                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el tipo de producto: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }
    }
}