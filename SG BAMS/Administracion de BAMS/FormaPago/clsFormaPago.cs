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
    /// 
    /// </summary>
    /// <seealso cref="SG_BAMS.ClsConexion" />
    internal class clsFormaPago : ClsConexion
    {
        /// <summary>
        /// Leers the formas pago asynchronous.
        /// </summary>
        /// <returns></returns>
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
        /// Insertars the forma pago asynchronous.
        /// </summary>
        /// <param name="descripcion">The descripcion.</param>
        /// <returns></returns>
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
        /// Modificars the forma pago asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="nuevaDescripcion">The nueva descripcion.</param>
        /// <returns></returns>
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

