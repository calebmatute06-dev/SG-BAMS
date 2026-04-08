using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS.Administracion_de_BAMS.MarcaProd
{
    /// <summary>
    /// Clase encargada de gestionar las operaciones de base de datos para las marcas de productos.
    /// </summary>
    /// <seealso cref="SG_BAMS.ClsConexion" />
    internal class clsMarca : ClsConexion
    {
        /// <summary>
        /// Obtiene el listado de marcas desde la vista detallada de forma asíncrona.
        /// </summary>
        /// <returns>Un DataTable con la información de las marcas.</returns>
        /// <exception cref="System.Exception">Error al obtener las marcas: " + ex.Message</exception>
        public async Task<DataTable> LeerMarcasAsync()
        {
            DataTable tabla = new DataTable();
            try
            {
                AbrirConexion();
                string query = "SELECT * FROM v_DetalleMarcas";

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
                throw new Exception("Error al obtener las marcas: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
            return tabla;
        }

        /// <summary>
        /// Inserta una nueva marca de producto en la base de datos mediante un procedimiento almacenado.
        /// </summary>
        /// <param name="nombreMarca">El nombre de la marca a registrar.</param>
        /// <returns>Verdadero si se insertó el registro; de lo contrario, falso.</returns>
        /// <exception cref="System.Exception">Error al insertar la marca: " + ex.Message</exception>
        public async Task<bool> InsertarMarcaAsync(string nombreMarca)
        {
            try
            {
                AbrirConexion();

                using (SqlCommand cmd = new SqlCommand("PA_insertar_marca_producto", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre_marca", nombreMarca);

                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar la marca: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }

        /// <summary>
        /// Actualiza el nombre de una marca existente de forma asíncrona.
        /// </summary>
        /// <param name="id">El identificador único de la marca.</param>
        /// <param name="nuevoNombre">El nuevo nombre que se asignará a la marca.</param>
        /// <returns>Verdadero si la actualización fue exitosa; de lo contrario, falso.</returns>
        /// <exception cref="System.Exception">Error al modificar la marca: " + ex.Message</exception>
        public async Task<bool> ModificarMarcaAsync(int id, string nuevoNombre)
        {
            try
            {
                AbrirConexion();

                using (SqlCommand cmd = new SqlCommand("PA_actualizar_marca_producto", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_marca_producto", id);
                    cmd.Parameters.AddWithValue("@nombre_marca", nuevoNombre);

                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar la marca: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }
    }
}