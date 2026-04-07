using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data.SqlClient;
using System.Data;

namespace SG_BAMS.Cliente
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="SG_BAMS.ClsConexion" />
    internal class ClsAgregarClientes:ClsConexion
    {

        /// <summary>
        /// Agregars the clientes.
        /// </summary>
        /// <param name="nombre">The nombre.</param>
        /// <param name="apellido">The apellido.</param>
        /// <param name="telefono">The telefono.</param>
        /// <param name="RTN">The RTN.</param>
        /// <returns></returns>
        public async Task<int> AgregarClientes(string nombre, string apellido, string telefono, string RTN)
        {
            try
            {
                AbrirConexion();

                using (SqlCommand cmd = new SqlCommand("PA_insertar_cliente",Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@nombre_cliente", nombre);
                    cmd.Parameters.AddWithValue("@apellido_cliente", apellido);
                    cmd.Parameters.AddWithValue("@telefono_cliente", telefono);
                    cmd.Parameters.AddWithValue("@rtn_cliente", RTN);

                    object result = await cmd.ExecuteScalarAsync();

                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }

                    return 0;

                }
            
            
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar cliente: " + ex.Message);
                return 0;
            }
            finally
            {
                Cerrar();
            }
        
        
        
        }

        /// <summary>
        /// Obteners the clientes.
        /// </summary>
        /// <returns></returns>
        public async Task<DataTable> ObtenerClientes()
        {
            
            DataTable dt = new DataTable();

            try
            {
                AbrirConexion();
                string query = "SELECT * FROM vista_nombres_clientes";

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
