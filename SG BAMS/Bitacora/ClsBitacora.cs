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
    }
}
