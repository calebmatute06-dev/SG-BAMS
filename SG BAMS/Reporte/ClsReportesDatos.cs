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
    /// 
    /// </summary>
    internal class ClsReportesDatos
    {

        /// <summary>
        /// The database
        /// </summary>
        private ClsConexion db = new ClsConexion();

        /// <summary>
        /// Reportes the ventas.
        /// </summary>
        /// <param name="desde">The desde.</param>
        /// <param name="hasta">The hasta.</param>
        /// <returns></returns>
        public DataTable ReporteVentas(DateTime desde, DateTime hasta)
        {
            string query = $"SELECT * FROM Vista_Reporte_Ventas_Final WHERE Fecha BETWEEN '{desde:yyyy-MM-dd}' AND '{hasta:yyyy-MM-dd}'";
            return EjecutarConsulta(query);
        }

        /// <summary>
        /// Reportes the compras.
        /// </summary>
        /// <param name="desde">The desde.</param>
        /// <param name="hasta">The hasta.</param>
        /// <returns></returns>
        public DataTable ReporteCompras(DateTime desde, DateTime hasta)
        {
            string query = $"SELECT * FROM Vista_Reporte_Compras_Final WHERE Fecha BETWEEN '{desde:yyyy-MM-dd}' AND '{hasta:yyyy-MM-dd}'";
            return EjecutarConsulta(query);
        }

        /// <summary>
        /// Reportes the deudores.
        /// </summary>
        /// <returns></returns>
        public DataTable ReporteDeudores()
        {
            string query = "SELECT * FROM Vista_Reporte_Deudores_Final";
            return EjecutarConsulta(query);
        }

        /// <summary>
        /// Reportes the inventario.
        /// </summary>
        /// <returns></returns>
        public DataTable ReporteInventario()
        {
            string query = "SELECT * FROM Vista_Reporte_Inventario_Final";
            return EjecutarConsulta(query);
        }

        /// <summary>
        /// Ejecutars the consulta.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al consultar la base de datos: " + ex.Message</exception>
        private DataTable EjecutarConsulta(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                db.AbrirConexion();
                SqlDataAdapter da = new SqlDataAdapter(query, db.Conectar);
                da.Fill(dt);
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