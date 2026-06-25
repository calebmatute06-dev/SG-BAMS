using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace SG_BAMS.Reporte
{
    /// <summary>
    /// Clase de acceso a datos para reportes del sistema BAMS.
    /// </summary>
    internal class ClsReportesDatos
    {
        private readonly ClsConexion db = new ClsConexion();

        /// <summary>
        /// Obtiene el reporte de ventas en un rango de fechas.
        /// </summary>
        /// <param name="desde">Fecha inicial del período.</param>
        /// <param name="hasta">Fecha final del período.</param>
        /// <returns>DataTable con los resultados de ventas.</returns>
        public DataTable ReporteVentas(DateTime desde, DateTime hasta)
        {
            string query = "SELECT * FROM Vista_Reporte_Ventas_Final WHERE Fecha BETWEEN @fechaInicio AND @fechaFin";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@fechaInicio", desde),
                new SqlParameter("@fechaFin", hasta)
            };

            return EjecutarConsultaParametrizada(query, parametros);
        }

        /// <summary>
        /// Obtiene el reporte de compras en un rango de fechas.
        /// </summary>
        /// <param name="desde">Fecha inicial del período.</param>
        /// <param name="hasta">Fecha final del período.</param>
        /// <returns>DataTable con los resultados de compras.</returns>
        public DataTable ReporteCompras(DateTime desde, DateTime hasta)
        {
            string query = "SELECT * FROM Vista_Reporte_Compras_Final WHERE Fecha BETWEEN @fechaInicio AND @fechaFin";

            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@fechaInicio", desde),
                new SqlParameter("@fechaFin", hasta)
            };

            return EjecutarConsultaParametrizada(query, parametros);
        }

        /// <summary>
        /// Obtiene el reporte de deudores.
        /// </summary>
        /// <returns>DataTable con los resultados de deudores.</returns>
        public DataTable ReporteDeudores()
        {
            string query = "SELECT * FROM Vista_Reporte_Deudores_Final";
            return EjecutarConsulta(query);
        }

        /// <summary>
        /// Obtiene el reporte de inventario.
        /// </summary>
        /// <returns>DataTable con los resultados de inventario.</returns>
        public DataTable ReporteInventario()
        {
            string query = "SELECT * FROM Vista_Reporte_Inventario_Final";
            return EjecutarConsulta(query);
        }

        /// <summary>
        /// Ejecuta una consulta SQL simple sin parámetros.
        /// </summary>
        /// <param name="query">La consulta SQL a ejecutar.</param>
        /// <returns>DataTable con los resultados de la consulta.</returns>
        private DataTable EjecutarConsulta(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                db.AbrirConexion();
                using (SqlDataAdapter da = new SqlDataAdapter(query, db.Conectar))
                {
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar la base de datos: " + ex.Message);
            }
            finally
            {
                db.Cerrar();
            }
            return dt;
        }

        /// <summary>
        /// Ejecuta una consulta SQL con parámetros para prevenir inyección SQL.
        /// </summary>
        /// <param name="query">La consulta SQL con parámetros (@parametro).</param>
        /// <param name="parametros">Array de parámetros SQL.</param>
        /// <returns>DataTable con los resultados de la consulta.</returns>
        private DataTable EjecutarConsultaParametrizada(string query, SqlParameter[] parametros)
        {
            DataTable dt = new DataTable();
            try
            {
                db.AbrirConexion();
                using (SqlCommand comando = new SqlCommand(query, db.Conectar))
                {
                    if (parametros?.Length > 0)
                    {
                        comando.Parameters.AddRange(parametros);
                    }

                    using (SqlDataAdapter da = new SqlDataAdapter(comando))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar la base de datos: " + ex.Message);
            }
            finally
            {
                db.Cerrar();
            }
            return dt;
        }
    }
}
