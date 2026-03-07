using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient; 

namespace SG_BAMS.Reporte
{
    internal class ClsReportesDatos
    {
        
        private ClsConexion db = new ClsConexion();

        public DataTable ReporteVentas(DateTime desde, DateTime hasta)
        {
            string query = $"SELECT * FROM Vista_Reporte_Ventas_Final WHERE Fecha BETWEEN '{desde:yyyy-MM-dd}' AND '{hasta:yyyy-MM-dd}'";
            return EjecutarConsulta(query);
        }

        public DataTable ReporteCompras(DateTime desde, DateTime hasta)
        {
            string query = $"SELECT * FROM Vista_Reporte_Compras_Final WHERE Fecha BETWEEN '{desde:yyyy-MM-dd}' AND '{hasta:yyyy-MM-dd}'";
            return EjecutarConsulta(query);
        }

        public DataTable ReporteDeudores()
        {
            // Eliminamos los parámetros y el WHERE para traer la lista completa
            string query = "SELECT * FROM Vista_Reporte_Deudores_Final";
            return EjecutarConsulta(query);
        }

        public DataTable ReporteInventario()
        {
            string query = "SELECT * FROM Vista_Reporte_Inventario_Final";
            return EjecutarConsulta(query);
        }

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
                // Lanza el error para que puedas capturarlo en el Formulario con un MessageBox
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