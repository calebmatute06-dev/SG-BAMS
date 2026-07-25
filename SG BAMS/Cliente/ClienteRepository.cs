using Microsoft.Data.SqlClient;
using SG_BAMS.Cliente.DTO;
using System;
using System.Data;
using System.Threading.Tasks;
using SG_BAMS.AccesoDatos;

namespace SG_BAMS.Cliente
{
    /// <summary>
    /// Implementación de acceso a datos del catálogo de clientes sobre SQL Server,
    /// usando solo Procedimientos Almacenados. Única responsabilidad: ejecutar las
    /// operaciones de datos de cliente. No muestra mensajes de interfaz — todos los
    /// métodos propagan la excepción de forma consistente y es la capa de presentación
    /// la que decide cómo informarla (ver auditoría SOLID, hallazgos CLI02/CLI03).
    /// Reemplaza a la antigua clase ClsCliente.
    /// </summary>
    internal class ClienteRepository : ClsRepositorioBaseDatos, IClienteRepository
    {
        /// <inheritdoc />
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
                    return (result != null && result != DBNull.Value) ? Convert.ToInt32(result) : 0;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al insertar cliente.", ex);
            }
            finally
            {
                Cerrar();
            }
        }

        /// <inheritdoc />
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
                throw new ApplicationException("Error al modificar cliente.", ex);
            }
            finally
            {
                Cerrar();
            }
        }

        /// <inheritdoc />
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
            catch (Exception ex)
            {
                throw new ApplicationException("Error al listar clientes.", ex);
            }
            finally
            {
                Cerrar();
            }
            return tablaC;
        }

        /// <inheritdoc />
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
            catch (Exception ex)
            {
                throw new ApplicationException("Error al obtener clientes.", ex);
            }
            finally
            {
                Cerrar();
            }
        }

        /// <inheritdoc />
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
            catch (Exception ex)
            {
                throw new ApplicationException("Error al obtener estados.", ex);
            }
            finally
            {
                Cerrar();
            }
        }

        /// <inheritdoc />
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
                throw new ApplicationException("Error al verificar RTN.", ex);
            }
            finally
            {
                Cerrar();
            }
        }
    }
}
