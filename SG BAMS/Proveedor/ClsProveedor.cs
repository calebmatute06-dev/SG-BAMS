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
    internal class ClsProveedor : ClsConexion
    {
        public void cargarDatos(Krypton.Toolkit.KryptonDataGridView dgvProveedor)
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

        private void SetUsuarioEnSesion(int idUsuario)
        {
            using (SqlCommand ctx = new SqlCommand(
            "EXEC sp_set_session_context @key=N'id_usuario', @value=@id;", Conectar))
            {
                ctx.Parameters.AddWithValue("@id", idUsuario);
                ctx.ExecuteNonQuery();
            }
        }
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
        


        public void BuscarProveedor(Krypton.Toolkit.KryptonTextBox txt, Krypton.Toolkit.KryptonDataGridView dgvProveedor)
        {
            try
            {
                string texto = txt.Text.Trim();
                if (string.IsNullOrWhiteSpace(texto))
                {
                    cargarDatos(dgvProveedor);
                    Cerrar();
                    return;
                }
                AbrirConexion();

                
                string consulta = "select * from vista_proveedor where Nombre like @filtro " +
                    "OR Contacto LIKE @filtro " +
                    "OR RTN LIKE @filtro " +
                    "OR Dirección LIKE @filtro " +
                    "OR Clasificación LIKE @filtro " +
                    "OR Estado LIKE @filtro ";
                SqlCommand cmd = new SqlCommand(consulta, Conectar);
                cmd.Parameters.AddWithValue("@filtro", "%" + texto + "%");

                
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable resultado = new DataTable();
                adapter.Fill(resultado);

                dgvProveedor.DataSource = resultado;

                Cerrar();
            }
            catch (Exception ex)
            {
                Cerrar();
                MessageBox.Show("Error al buscar: " + ex.Message);
            }
        }

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
    }
}
