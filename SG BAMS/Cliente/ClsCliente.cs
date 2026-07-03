using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

namespace SG_BAMS.Cliente
{
    /// <summary>
    /// Clase para la gestión de clientes usando solo Procedimientos Almacenados.
    /// Consolida las operaciones de alta, modificación y consulta.
    /// </summary>
    internal class ClsCliente : ClsRepositorioBaseDatos
    {
        public async Task<int> AgregarClientes(string nombre, string apellido, string telefono, string RTN)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("PA_insertar_cliente", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre_cliente", nombre);
                    cmd.Parameters.AddWithValue("@apellido_cliente", apellido);
                    cmd.Parameters.AddWithValue("@telefono_cliente", telefono);
                    cmd.Parameters.AddWithValue("@rtn_cliente", RTN);
                    object result = await cmd.ExecuteScalarAsync();
                    if (result != null && result != DBNull.Value)
                        return Convert.ToInt32(result);
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

        public async Task<DataTable> VerClienteTabla()
        {
            DataTable tablaC = new DataTable();
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Clientes_ListarTodos", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        tablaC.Load(reader);
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                Cerrar();
            }
            return tablaC;
        }

        public async Task<DataTable> ObtenerClientes()
        {
            DataTable dt = new DataTable();
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Clientes_Nombres", Conectar))
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

        public bool RTNYaExiste(string rtn, int idClienteActual = 0)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Cliente_VerificarRTN", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@rtn", rtn);
                    cmd.Parameters.AddWithValue("@id", idClienteActual);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al verificar RTN: " + ex.Message);
                return false;
            }
            finally
            {
                Cerrar();
            }
        }
    }
}