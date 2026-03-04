using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Krypton.Toolkit;

namespace SG_BAMS.Reportes // Cambié el namespace a Reportes
{
    internal class ClsReporte : ClsConexion
    {
        // Método para cargar todos los reportes inicialmente
        public void CargarDatosReporte(KryptonDataGridView dgvReporte)
        {
            try
            {
                AbrirConexion();

                // Usamos un SELECT con JOIN para traer nombres reales
                string consulta = @"SELECT 
                                    R.id_reporte AS [ID], 
                                    U.nombre_usuario AS [Usuario], 
                                    TR.tipo_reporte_desc AS [Tipo], 
                                    R.desc_reporte AS [Descripción], 
                                    R.fecha_reporte AS [Fecha]
                                   FROM reporte R
                                   INNER JOIN Usuario U ON R.id_usuario = U.id_usuario
                                   INNER JOIN tipo_reporte TR ON R.id_tipo_reporte = TR.id_tipo_reporte
                                   ORDER BY R.fecha_reporte DESC";

                SqlDataAdapter adapter = new SqlDataAdapter(consulta, Conectar);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dgvReporte.DataSource = dt;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error al cargar reportes: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }

        // Método para buscar por rango de fechas y filtro de texto
        public void BuscarReporte(KryptonTextBox txt, DateTime desde, DateTime hasta, KryptonDataGridView dgvReporte)
        {
            try
            {
                AbrirConexion();
                string filtro = "%" + txt.Text.Trim() + "%";

                // Consulta directa con parámetros para manejar el rango de fechas
                string sql = @"SELECT 
                                R.id_reporte AS [ID], 
                                U.nombre_usuario AS [Usuario], 
                                TR.tipo_reporte_desc AS [Tipo], 
                                R.desc_reporte AS [Descripción], 
                                R.fecha_reporte AS [Fecha]
                               FROM reporte R
                               INNER JOIN Usuario U ON R.id_usuario = U.id_usuario
                               INNER JOIN tipo_reporte TR ON R.id_tipo_reporte = TR.id_tipo_reporte
                               WHERE (R.fecha_reporte BETWEEN @desde AND @hasta)
                               AND (R.desc_reporte LIKE @filtro OR U.nombre_usuario LIKE @filtro)
                               ORDER BY R.fecha_reporte DESC";

                using (SqlCommand cmd = new SqlCommand(sql, Conectar))
                {
                    // Aseguramos que 'hasta' incluya todo el día final (23:59:59)
                    cmd.Parameters.Add("@desde", SqlDbType.Date).Value = desde.Date;
                    cmd.Parameters.Add("@hasta", SqlDbType.Date).Value = hasta.Date;
                    cmd.Parameters.Add("@filtro", SqlDbType.NVarChar).Value = filtro;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvReporte.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error al filtrar reportes: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }

        // ESTO VA EN ClsReporte.cs
        public void BuscarReporte(DateTime desde, DateTime hasta, Krypton.Toolkit.KryptonDataGridView dgvReporte)
        {
            try
            {
                AbrirConexion();
                // Consulta SQL que usa solo las fechas
                string sql = @"SELECT 
                        R.id_reporte AS [ID], 
                        U.nombre_usuario AS [Usuario], 
                        TR.tipo_reporte_desc AS [Tipo], 
                        R.desc_reporte AS [Descripción], 
                        R.fecha_reporte AS [Fecha]
                       FROM reporte R
                       INNER JOIN Usuario U ON R.id_usuario = U.id_usuario
                       INNER JOIN tipo_reporte TR ON R.id_tipo_reporte = TR.id_tipo_reporte
                       WHERE R.fecha_reporte BETWEEN @desde AND @hasta
                       ORDER BY R.fecha_reporte DESC";

                using (SqlCommand cmd = new SqlCommand(sql, Conectar))
                {
                    cmd.Parameters.Add("@desde", SqlDbType.Date).Value = desde.Date;
                    cmd.Parameters.Add("@hasta", SqlDbType.Date).Value = hasta.Date;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvReporte.DataSource = dt; // Refresca el Grid con el resultado
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error al filtrar: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }
    }
}