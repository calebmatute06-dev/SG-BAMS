using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS.Administracion_de_BAMS.FormaPago
{
    /// <summary>
    /// Clase encargada de gestionar las operaciones de base de datos relacionadas con las formas de pago.
    /// </summary>
    /// <seealso cref="SG_BAMS.ClsConexion" />
    internal class clsFormaPago : ClsConexion
    {
        /// <summary>
        /// Obtiene todas las formas de pago registradas de manera asíncrona.
        /// </summary>
        /// <returns>Un objeto DataTable con los registros de las formas de pago.</returns>
        /// <exception cref="System.Exception">Error al obtener formas de pago: " + ex.Message</exception>
        public async Task<DataTable> LeerFormasPagoAsync()
        {
            DataTable tabla = new DataTable();
            try
            {

                AbrirConexion();

                string query = "SELECT * FROM v_DetalleFormasPago";

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
                throw new Exception("Error al obtener formas de pago: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
            return tabla;
        }

        /// <summary>
        /// Inserta una nueva forma de pago de manera asíncrona.
        /// </summary>
        /// <param name="descripcion">La descripción de la forma de pago.</param>
        /// <returns>Verdadero si la operación fue exitosa; de lo contrario, falso.</returns>
        /// <exception cref="System.Exception">Error al insertar la forma de pago: " + ex.Message</exception>
        public async Task<bool> InsertarFormaPagoAsync(string descripcion)
        {
            try
            {
                AbrirConexion();

                using (SqlCommand cmd = new SqlCommand("PA_insertar_tipo_forma_pago", Conectar))
                {

                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@descripcion_forma_pago", descripcion);

                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar la forma de pago: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }


        }

        /// <summary>
        /// Modifica una forma de pago existente de manera asíncrona.
        /// </summary>
        /// <param name="id">El identificador único de la forma de pago.</param>
        /// <param name="nuevaDescripcion">La nueva descripción que se asignará.</param>
        /// <returns>Verdadero si la actualización fue exitosa; de lo contrario, falso.</returns>
        /// <exception cref="System.Exception">Error en la base de datos: " + ex.Message</exception>
        public async Task<bool> ModificarFormaPagoAsync(int id, string nuevaDescripcion)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("PA_actualizar_tipo_forma_pago", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_tipo_forma_pago", id);
                    cmd.Parameters.AddWithValue("@descripcion_forma_pago", nuevaDescripcion);

                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
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
        }

    }
}