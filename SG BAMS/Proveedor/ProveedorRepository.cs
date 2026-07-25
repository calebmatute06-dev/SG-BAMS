using System;
using System.Data;
using Microsoft.Data.SqlClient;
using SG_BAMS.Proveedor.DTO;
using SG_BAMS.AccesoDatos;

namespace SG_BAMS.Proveedor
{
    /// <summary>
    /// Implementación del repositorio de proveedores sobre SQL Server.
    /// Ejecuta las operaciones de datos sin conocer detalles de la interfaz gráfica.
    /// </summary>
    internal class ProveedorRepository : ClsRepositorioBaseDatos, IProveedorRepository
    {
        /// <inheritdoc />
        public DataTable ObtenerProveedores()
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_vista_proveedor", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al cargar los datos de proveedores.", ex);
            }
            finally
            {
                Cerrar();
            }
        }

        /// <inheritdoc />
        public DataTable Buscar(string filtro)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_proveedor_buscar", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@filtro", SqlDbType.NVarChar, 200).Value = filtro;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al buscar proveedores.", ex);
            }
            finally
            {
                Cerrar();
            }
        }

        /// <inheritdoc />
        public bool ExisteNombre(string nombre)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Proveedor_ExisteNombre", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al verificar el nombre del proveedor.", ex);
            }
            finally
            {
                Cerrar();
            }
        }

        /// <inheritdoc />
        public bool ExisteRtn(string rtn)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Proveedor_ExisteRTN", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@rtn", rtn);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al validar el RTN del proveedor.", ex);
            }
            finally
            {
                Cerrar();
            }
        }

        /// <inheritdoc />
        public bool ExisteRtnExcluyendo(string rtn, int idProveedorActual)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Proveedor_ExisteRTNModificar", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@rtn", rtn);
                    cmd.Parameters.AddWithValue("@idActual", idProveedorActual);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al validar el duplicado de RTN.", ex);
            }
            finally
            {
                Cerrar();
            }
        }

        /// <inheritdoc />
        public void Agregar(ProveedorDTO proveedor)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_proveedor_insertar", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre_proveedor", proveedor.Nombre);
                    cmd.Parameters.AddWithValue("@contacto_proveedor", proveedor.Contacto);
                    cmd.Parameters.AddWithValue("@direccion_proveedor", proveedor.Direccion);
                    cmd.Parameters.AddWithValue("@rtn_proveedor", proveedor.Rtn);
                    cmd.Parameters.AddWithValue("@id_clasificacion_proveedor", proveedor.IdClasificacion);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al insertar el proveedor.", ex);
            }
            finally
            {
                Cerrar();
            }
        }

        /// <inheritdoc />
        public void Modificar(ProveedorDTO proveedor)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_proveedor_actualizar", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_proveedor", proveedor.IdProveedor);
                    cmd.Parameters.AddWithValue("@nombre_proveedor", proveedor.Nombre);
                    cmd.Parameters.AddWithValue("@contacto_proveedor", proveedor.Contacto);
                    cmd.Parameters.AddWithValue("@direccion_proveedor", proveedor.Direccion);
                    cmd.Parameters.AddWithValue("@rtn_proveedor", proveedor.Rtn);
                    cmd.Parameters.AddWithValue("@id_estado", proveedor.IdEstado);
                    cmd.Parameters.AddWithValue("@id_clasificacion_proveedor", proveedor.IdClasificacion);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al modificar el proveedor.", ex);
            }
            finally
            {
                Cerrar();
            }
        }
    }
}