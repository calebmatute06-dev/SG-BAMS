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
    /// 
    /// </summary>
    /// <seealso cref="SG_BAMS.ClsConexion" />
    internal class clsMarca : ClsConexion
    {
        /// <summary>
        /// Leers the marcas asynchronous.
        /// </summary>
        /// <returns></returns>
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
        /// Insertars the marca asynchronous.
        /// </summary>
        /// <param name="nombreMarca">The nombre marca.</param>
        /// <returns></returns>
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
        /// Modificars the marca asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="nuevoNombre">The nuevo nombre.</param>
        /// <returns></returns>
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
