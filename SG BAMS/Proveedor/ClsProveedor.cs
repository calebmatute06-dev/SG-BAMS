using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Krypton.Toolkit;

namespace SG_BAMS.Proveedor
{
    /// <summary>
    /// Clase de acceso a datos para Proveedores.
    /// Solo usa Procedimientos Almacenados.
    /// </summary>
    internal class ClsProveedor : ClsConexion
    {
        /// <summary>
        /// Carga los datos en el DataGridView usando PA.
        /// </summary>
        public void cargarDatos(DataGridView dgvProveedor)
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
                        dgvProveedor.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }

        /// <summary>
        /// Establece el usuario en sesión.
        /// </summary>
        private void SetUsuarioEnSesion(int idUsuario)
        {
            using (SqlCommand ctx = new SqlCommand(
                "EXEC sp_set_session_context @key=N'id_usuario', @value=@id;", Conectar))
            {
                ctx.Parameters.AddWithValue("@id", idUsuario);
                ctx.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Carga el combo de estado usando PA.
        /// </summary>
        public void CargarComboEstado(KryptonComboBox cmb)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_vista_estado", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        cmb.DataSource = dt;
                        cmb.DisplayMember = "descripcion_estado";
                        cmb.ValueMember = "id_estado";
                        cmb.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar ComboBox: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }

        /// <summary>
        /// Carga el combo de clasificación usando PA.
        /// </summary>
        public void CargarComboClasificacion(KryptonComboBox cmb)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_vista_clasificacion", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        cmb.DataSource = dt;
                        cmb.DisplayMember = "clasificacion_proveedor";
                        cmb.ValueMember = "id_clasificacion_proveedor";
                        cmb.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar ComboBox: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }

        /// <summary>
        /// Busca proveedores usando PA.
        /// </summary>
        public void BuscarProveedor(KryptonTextBox txt, DataGridView dgvProveedor)
        {
            try
            {
                AbrirConexion();
                string texto = txt.Text.Trim();
                using (SqlCommand cmd = new SqlCommand("sp_proveedor_buscar", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@filtro", SqlDbType.NVarChar, 200).Value = texto;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable resultado = new DataTable();
                        adapter.Fill(resultado);
                        dgvProveedor.DataSource = resultado;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }

        /// <summary>
        /// Agrega un proveedor usando PA.
        /// </summary>
        public void AgregarProveedor(string nombre, string contacto, string direccion, string rtn,
            int idClasificacion, int idUsuario)
        {
            try
            {
                AbrirConexion();
                SetUsuarioEnSesion(idUsuario);
                using (SqlCommand cmd = new SqlCommand("sp_proveedor_insertar", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre_proveedor", nombre);
                    cmd.Parameters.AddWithValue("@contacto_proveedor", contacto);
                    cmd.Parameters.AddWithValue("@direccion_proveedor", direccion);
                    cmd.Parameters.AddWithValue("@rtn_proveedor", rtn);
                    cmd.Parameters.AddWithValue("@id_clasificacion_proveedor", idClasificacion);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar proveedor: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }

        /// <summary>
        /// Verifica si existe un proveedor con el nombre dado usando PA.
        /// </summary>
        public bool ExisteNombreProveedor(string nombre)
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
                MessageBox.Show("Error al verificar duplicados: " + ex.Message);
                return false;
            }
            finally
            {
                Cerrar();
            }
        }

        /// <summary>
        /// Modifica un proveedor usando PA.
        /// </summary>
        public void ModificarProveedor(int idProveedor, string nombre, string contacto, string direccion,
            string rtn, int idEstado, int idClasificacion, int idUsuario)
        {
            try
            {
                AbrirConexion();
                SetUsuarioEnSesion(idUsuario);
                using (SqlCommand cmd = new SqlCommand("sp_proveedor_actualizar", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_proveedor", idProveedor);
                    cmd.Parameters.AddWithValue("@nombre_proveedor", nombre);
                    cmd.Parameters.AddWithValue("@contacto_proveedor", contacto);
                    cmd.Parameters.AddWithValue("@direccion_proveedor", direccion);
                    cmd.Parameters.AddWithValue("@rtn_proveedor", rtn);
                    cmd.Parameters.AddWithValue("@id_estado", idEstado);
                    cmd.Parameters.AddWithValue("@id_clasificacion_proveedor", idClasificacion);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar proveedor: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }

        /// <summary>
        /// Verifica si existe un proveedor con el RTN dado usando PA.
        /// </summary>
        public bool ExisteRtnProveedor(string rtn)
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
                MessageBox.Show("Error al validar el RTN: " + ex.Message);
                return false;
            }
            finally
            {
                Cerrar();
            }
        }

        /// <summary>
        /// Verifica si existe un RTN excluyendo el proveedor actual usando PA.
        /// </summary>
        public bool ExisteRtnProveedorModificar(string rtn, int idProveedorActual)
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
                MessageBox.Show("Error al validar duplicado de RTN: " + ex.Message);
                return false;
            }
            finally
            {
                Cerrar();
            }
        }
    }
}