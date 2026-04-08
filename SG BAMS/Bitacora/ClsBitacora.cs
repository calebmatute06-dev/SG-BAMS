using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace SG_BAMS.Bitacora
{
    /// <summary>
    /// Maneja las operaciones de acceso a datos para el módulo de bitácora.
    /// </summary>
    /// <seealso cref="SG_BAMS.ClsConexion" />
    internal class ClsBitacora : ClsConexion
    {
        /// <summary>
        /// Carga todos los registros de la bitácora en el DataGridView especificado.
        /// </summary>
        /// <param name="dgvBitacora">El control DataGridView donde se mostrarán los datos de la bitácora.</param>
        public void cargarDatos(System.Windows.Forms.DataGridView dgvBitacora)
        {
            try
            {
                AbrirConexion();
                string consulta = "SELECT * FROM vista_bitacora";
                SqlDataAdapter adapter = new SqlDataAdapter(consulta, Conectar);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvBitacora.DataSource = dt;
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
        /// Busca registros en la bitácora usando un filtro de texto y un rango de fechas,
        /// luego muestra los resultados en el DataGridView especificado.
        /// </summary>
        /// <param name="txt">El cuadro de texto que contiene la palabra o frase para filtrar los resultados.</param>
        /// <param name="desde">La fecha de inicio del rango de búsqueda (inclusiva).</param>
        /// <param name="hasta">La fecha de fin del rango de búsqueda (inclusiva).</param>
        /// <param name="dgvBitacora">El control DataGridView donde se mostrarán los resultados filtrados.</param>
        public void BuscarBitacora(Krypton.Toolkit.KryptonTextBox txt,
            DateTime desde,
            DateTime hasta,
            System.Windows.Forms.DataGridView dgvBitacora)
        {
            try
            {
                AbrirConexion();
                string filtro = txt.Text.Trim();
                using (SqlCommand cmd = new SqlCommand("sp_bitacora_buscar", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@desde", SqlDbType.Date).Value = desde.Date;
                    cmd.Parameters.Add("@hasta", SqlDbType.Date).Value = hasta.Date.AddDays(1); // límite superior exclusivo
                    cmd.Parameters.Add("@filtro", SqlDbType.NVarChar, 200).Value = filtro;
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