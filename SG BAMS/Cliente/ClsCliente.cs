using Microsoft.Data.SqlClient;
using SG_BAMS.Cliente.DTO;
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
        /// <summary>
        /// Registra un cliente nuevo a partir de los datos del DTO.
        /// </summary>
        /// <param name="dto">Datos del cliente a registrar.</param>
        public async Task<int> AgregarClientes(ClienteDTO dto)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("PA_insertar_cliente", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre_cliente", dto.Nombre);
                    cmd.Parameters.AddWithValue("@apellido_cliente", dto.Apellido);
                    cmd.Parameters.AddWithValue("@telefono_cliente", dto.Telefono);
                    cmd.Parameters.AddWithValue("@rtn_cliente", dto.RTN);
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

        /// <summary>
        /// Actualiza un cliente existente a partir de los datos del DTO.
        /// </summary>
        /// <param name="dto">Datos actualizados del cliente, incluyendo su IdCliente.</param>
        public async Task<int> ModificarClientes(ClienteDTO dto)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("PA_actualizar_cliente", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_cliente", dto.IdCliente);
                    cmd.Parameters.AddWithValue("@nombre_cliente", dto.Nombre);
                    cmd.Parameters.AddWithValue("@apellido_cliente", dto.Apellido);
                    cmd.Parameters.AddWithValue("@telefono_cliente", dto.Telefono);
                    cmd.Parameters.AddWithValue("@rtn_cliente", dto.RTN);
                    cmd.Parameters.AddWithValue("@id_estado", dto.IdEstado);
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