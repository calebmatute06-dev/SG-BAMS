
using Microsoft.Data.SqlClient;
using SG_BAMS.Cliente.DTO;
using System;
using System.Data;
using System.Threading.Tasks;

namespace SG_BAMS.Cliente
{
    /// <summary>
    /// Acceso a datos de clientes usando exclusivamente Procedimientos Almacenados.
    /// Implementa dos interfaces pequeñas y específicas (ISP) en lugar de una sola
    /// interfaz "gorda": IClienteRepository para el CRUD de clientes, e
    /// IEstadoClienteRepository para el catálogo de Estados. Ninguna de las dos
    /// conoce MessageBox ni ningún elemento de la capa de presentación —
    /// las excepciones se relanzan para que el formulario decida cómo mostrarlas.
    /// </summary>
    internal class ClienteRepository : ClsRepositorioBaseDatos, IClienteRepository, IEstadoClienteRepository
    {
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
                throw new Exception("Error al insertar cliente: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }
        }

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
                    return await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar cliente: " + ex.Message, ex);
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
                return tablaC;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el listado de clientes: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }
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
            catch
            {
                throw;
            }
            finally
            {
                Cerrar();
            }
        }

        public async Task<bool> RTNYaExiste(string rtn, int idClienteActual = 0)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Cliente_VerificarRTN", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@rtn", rtn);
                    cmd.Parameters.AddWithValue("@id", idClienteActual);
                    object resultado = await cmd.ExecuteScalarAsync();
                    return Convert.ToInt32(resultado) > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar RTN: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }
        }

        // ---- IEstadoClienteRepository ----

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
            catch
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