using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace SG_BAMS.Proveedor
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="SG_BAMS.ClsConexion" />
    internal class ClsProveedor : ClsConexion
    {
        /// <summary>
        /// Carga los datos en el DataGridView.
        /// </summary>
        /// <param name="dgvProveedor">El DataGridView de proveedores.</param>
        public void cargarDatos(DataGridView dgvProveedor)
        {
            try
            {
                AbrirConexion();
                string consulta = "SELECT * FROM vista_proveedor";
                SqlDataAdapter adapter = new SqlDataAdapter(consulta, Conectar);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvProveedor.DataSource = dt;
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
        /// <param name="idUsuario">El identificador del usuario.</param>
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
        /// Carga el combo de estado.
        /// </summary>
        /// <param name="cmb">El ComboBox.</param>
        public void CargarComboEstado(Krypton.Toolkit.KryptonComboBox cmb)
        {
            try
            {
                AbrirConexion();
                string consulta = "SELECT * FROM vista_estado";
                SqlDataAdapter adapter = new SqlDataAdapter(consulta, Conectar);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                cmb.DataSource = dt;
                cmb.DisplayMember = "descripcion_estado";
                cmb.ValueMember = "id_estado";
                cmb.SelectedIndex = -1;
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
        /// Carga el combo de clasificación.
        /// </summary>
        /// <param name="cmb">El ComboBox.</param>
        public void CargarComboClasificacion(Krypton.Toolkit.KryptonComboBox cmb)
        {
            try
            {
                AbrirConexion();
                string consulta = "SELECT * FROM vista_clasificacion";
                SqlDataAdapter adapter = new SqlDataAdapter(consulta, Conectar);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                cmb.DataSource = dt;
                cmb.DisplayMember = "clasificacion_proveedor";
                cmb.ValueMember = "id_clasificacion_proveedor";
                cmb.SelectedIndex = -1;
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
        /// Busca proveedores.
        /// </summary>
        /// <param name="txt">El cuadro de texto de búsqueda.</param>
        /// <param name="dgvProveedor">El DataGridView de proveedores.</param>
        public void BuscarProveedor(Krypton.Toolkit.KryptonTextBox txt, DataGridView dgvProveedor)
        {
            try
            {
                AbrirConexion();

                string texto = txt.Text.Trim();

                using (SqlCommand cmd = new SqlCommand("sp_proveedor_buscar", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@filtro", SqlDbType.NVarChar, 200).Value = texto;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable resultado = new DataTable();
                    adapter.Fill(resultado);

                    dgvProveedor.DataSource = resultado;
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
        /// Agrega un proveedor.
        /// </summary>
        /// <param name="nombre">El nombre.</param>
        /// <param name="contacto">El contacto.</param>
        /// <param name="direccion">La dirección.</param>
        /// <param name="rtn">El RTN.</param>
        /// <param name="idClasificacion">El identificador de la clasificación.</param>
        /// <param name="idUsuario">El identificador del usuario.</param>
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

                MessageBox.Show("Proveedor agregado correctamente.");
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
        /// Verifica si existe un proveedor con el nombre dado.
        /// </summary>
        /// <param name="nombre">El nombre.</param>
        /// <returns></returns>
        public bool ExisteNombreProveedor(string nombre)
        {
            bool existe = false;
            try
            {
                AbrirConexion();
                string consulta = "SELECT COUNT(*) FROM Proveedor WHERE nombre_proveedor = @nombre";

                using (SqlCommand cmd = new SqlCommand(consulta, Conectar))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (count > 0)
                    {
                        existe = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al verificar duplicados: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
            return existe;
        }

        /// <summary>
        /// Modifica un proveedor.
        /// </summary>
        /// <param name="idProveedor">El identificador del proveedor.</param>
        /// <param name="nombre">El nombre.</param>
        /// <param name="contacto">El contacto.</param>
        /// <param name="direccion">La dirección.</param>
        /// <param name="rtn">El RTN.</param>
        /// <param name="idEstado">El identificador del estado.</param>
        /// <param name="idClasificacion">El identificador de la clasificación.</param>
        /// <param name="idUsuario">El identificador del usuario.</param>
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

                MessageBox.Show("Proveedor modificado correctamente.");
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
        /// Verifica si existe un proveedor con el RTN dado.
        /// </summary>
        /// <param name="rtn">El RTN.</param>
        /// <returns></returns>
        public bool ExisteRtnProveedor(string rtn)
        {
            bool existe = false;
            try
            {
                AbrirConexion();

                string consulta = "SELECT COUNT(*) FROM Proveedor WHERE rtn_proveedor = @rtn";

                using (SqlCommand cmd = new SqlCommand(consulta, Conectar))
                {
                    cmd.Parameters.AddWithValue("@rtn", rtn);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    existe = count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al validar el RTN: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
            return existe;
        }

        /// <summary>
        /// Verifica si existe un proveedor con el RTN dado, excluyendo el proveedor actual.
        /// </summary>
        /// <param name="rtn">El RTN.</param>
        /// <param name="idProveedorActual">El identificador del proveedor actual.</param>
        /// <returns></returns>
        public bool ExisteRtnProveedorModificar(string rtn, int idProveedorActual)
        {
            bool existe = false;
            try
            {
                AbrirConexion();
                string consulta = "SELECT COUNT(*) FROM Proveedor WHERE rtn_proveedor = @rtn AND id_proveedor <> @idActual";

                using (SqlCommand cmd = new SqlCommand(consulta, Conectar))
                {
                    cmd.Parameters.AddWithValue("@rtn", rtn);
                    cmd.Parameters.AddWithValue("@idActual", idProveedorActual);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    existe = count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al validar duplicado de RTN: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
            return existe;
        }
    }
}