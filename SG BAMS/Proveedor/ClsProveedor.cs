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
                AbrirConexion(); // Abrir la conexión
                string consulta = "SELECT * FROM vista_proveedor"; // Consulta SQL
                SqlDataAdapter adapter = new SqlDataAdapter(consulta, Conectar);
                DataTable dt = new DataTable();
                adapter.Fill(dt); // Llenar el DataTable
                dgvProveedor.DataSource = dt; // Asignar al DataGridView

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
            finally
            {
                Cerrar(); // Siempre cerrar la conexión
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

        /*
        public void CargarComboClasificacion()
        {

        }
        */


        public void BuscarProveedor(Krypton.Toolkit.KryptonTextBox txt, Krypton.Toolkit.KryptonDataGridView dgvBitacora)
        {
            try
            {
                string nombre = txt.Text.Trim();
                if (string.IsNullOrWhiteSpace(nombre))
                {
                    cargarDatos(dgvBitacora);
                    Cerrar();
                    return;
                }
                AbrirConexion();

                // Prepara el comando SQL y agrega el parámetro con comodín %
                string consulta = "select * from vista_proveedor where Nombre like @nombre";
                SqlCommand cmd = new SqlCommand(consulta, Conectar);
                cmd.Parameters.AddWithValue("@nombre", nombre);

                // Ejecuta la consulta y llena un DataTable con los resultados
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable resultado = new DataTable();
                adapter.Fill(resultado);

                dgvBitacora.DataSource = resultado;

                Cerrar();
            }
            catch (Exception ex)
            {
                Cerrar();
                MessageBox.Show("Error al buscar: " + ex.Message);
            }
        }
    }
}
