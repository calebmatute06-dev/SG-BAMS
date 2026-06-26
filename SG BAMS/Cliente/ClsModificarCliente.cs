using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

namespace SG_BAMS.Cliente
{
    /// <summary>
    /// Clase para modificar clientes usando solo Procedimientos Almacenados.
    /// </summary>
    internal class ClsModificarCliente : ClsConexion
    {
        public async Task<int> ModificarClientes(int idCliente, string nombre, string apellido, string telefono, string RTN, int idEstado)
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

        public async Task<DataTable> ObtenerEstados()
        {
            DataTable dt = new DataTable();
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Estado_Listar", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        dt.Load(reader);
                    }
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