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

        public void BuscarBitacora(Krypton.Toolkit.KryptonTextBox txt,
            DateTime desde,
            DateTime hasta,
            Krypton.Toolkit.KryptonDataGridView dgvBitacora)
        {
            try
            {
                AbrirConexion();

                string filtro = txt.Text.Trim();

                using (SqlCommand cmd = new SqlCommand("sp_bitacora_buscar", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@desde", SqlDbType.Date).Value = desde.Date;
                    cmd.Parameters.Add("@hasta", SqlDbType.Date).Value = hasta.Date.AddDays(1); // hasta exclusivo
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
