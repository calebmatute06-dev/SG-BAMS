using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SG_BAMS.Bitacora
{
    /// <summary>
    /// Clase para operaciones de bitácora usando solo Procedimientos Almacenados.
    /// </summary>
    internal class ClsBitacora : ClsRepositorioBaseDatos
    {
        public void cargarDatos(System.Windows.Forms.DataGridView dgvBitacora)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Bitacora_Listar", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvBitacora.DataSource = dt;
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
                    cmd.Parameters.Add("@hasta", SqlDbType.Date).Value = hasta.Date.AddDays(1);
                    cmd.Parameters.Add("@filtro", SqlDbType.NVarChar, 200).Value = filtro;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvBitacora.DataSource = dt;
                    }
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