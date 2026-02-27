using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace SG_BAMS.Bitacora
{
    internal class ClsBitacora : ClsConexion
    {
        public void cargarDatos(Krypton.Toolkit.KryptonDataGridView dgvBitacora)
        {
            try
            {
                AbrirConexion(); // Abrir la conexión
                string consulta = "SELECT * FROM vista_bitacora"; // Consulta SQL
                SqlDataAdapter adapter = new SqlDataAdapter(consulta, Conectar);
                DataTable dt = new DataTable();
                adapter.Fill(dt); // Llenar el DataTable
                dgvBitacora.DataSource = dt; // Asignar al DataGridView

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

        public void BuscarBitacora(Krypton.Toolkit.KryptonTextBox txt,
            DateTime desde,
            DateTime hasta,
            Krypton.Toolkit.KryptonDataGridView dgvBitacora)
        {
            try
            {
                AbrirConexion();

                string filtro = txt.Text.Trim();

                string consulta = "SELECT * FROM vista_bitacora " +
                    "WHERE Fecha >= @desde AND Fecha < @hasta " +
                    "AND ( " +
                    "@filtro = '' " +
                    "OR Nombre LIKE @like " +
                    "OR Acción LIKE @like " +
                    "OR Modulo LIKE @like " +
                    ")";

                using (SqlCommand cmd = new SqlCommand(consulta, Conectar))
                {
                    cmd.Parameters.AddWithValue("@desde", desde.Date);
                    cmd.Parameters.AddWithValue("@hasta", hasta.Date.AddDays(1)); // incluye todo el día "hasta"
                    cmd.Parameters.AddWithValue("@filtro", filtro);
                    cmd.Parameters.AddWithValue("@like", "%" + filtro + "%");

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvBitacora.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }
    }
}
