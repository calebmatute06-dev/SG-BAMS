using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SG_BAMS.Cliente
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="SG_BAMS.ClsConexion" />
    internal class ClsModificarCliente:ClsConexion
    {

        /// <summary>
        /// Modificars the clientes.
        /// </summary>
        /// <param name="idCliente">The identifier cliente.</param>
        /// <param name="nombre">The nombre.</param>
        /// <param name="apellido">The apellido.</param>
        /// <param name="telefono">The telefono.</param>
        /// <param name="RTN">The RTN.</param>
        /// <param name="idEstado">The identifier estado.</param>
        /// <returns></returns>
        public async Task<int> ModificarClientes(int idCliente,string nombre, string apellido, string telefono, string RTN, int idEstado)
        {
            
            try 
            {

                AbrirConexion();

                using (SqlCommand cmd = new SqlCommand("PA_actualizar_cliente", Conectar))
                {

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id_cliente", idCliente);
                    cmd.Parameters.AddWithValue("@nombre_cliente", nombre);
                    cmd.Parameters.AddWithValue("@apellido_cliente", apellido);
                    cmd.Parameters.AddWithValue("@telefono_cliente", telefono);
                    cmd.Parameters.AddWithValue("@rtn_cliente", RTN);
                    cmd.Parameters.AddWithValue("@id_estado", idEstado);

                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    return filasAfectadas;

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
            finally
            {
                Cerrar();

            }
        
        }

        /// <summary>
        /// Obteners the estados.
        /// </summary>
        /// <returns></returns>
        public async Task<DataTable> ObtenerEstados()
        {
            
            DataTable dt = new DataTable();

            try
            {
                AbrirConexion();
                string query = "SELECT id_estado, descripcion_estado FROM Estado";

                using (SqlCommand cmd = new SqlCommand(query, Conectar))
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    dt.Load(reader);
                }
                return dt;
            }
            catch (Exception)
            {
                throw; 
            }
            finally
            {
                Cerrar();
            }
        }
    }
}
