using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SG_BAMS.Reporte
{
    /// <summary>
    /// Clase de acceso a datos para reportes del sistema BAMS.
    /// Solo usa Procedimientos Almacenados.
    /// </summary>
    internal class ClsReportesDatos
    {
        private readonly ClsRepositorioBaseDatos db = new ClsRepositorioBaseDatos();

        /// <summary>
        /// Obtiene el reporte de ventas en un rango de fechas.
        /// </summary>
        public DataTable ReporteVentas(DateTime desde, DateTime hasta)
        {
            return EjecutarProcedimiento(
                "sp_Vista_Reporte_Ventas_Final",
                new SqlParameter("@fechaInicio", desde),
                new SqlParameter("@fechaFin", hasta)
            );
        }

        /// <summary>
        /// Obtiene el reporte de compras en un rango de fechas.
        /// </summary>
        public DataTable ReporteCompras(DateTime desde, DateTime hasta)
        {
            return EjecutarProcedimiento(
                "sp_Vista_Reporte_Compras_Final",
                new SqlParameter("@fechaInicio", desde),
                new SqlParameter("@fechaFin", hasta)
            );
        }

        /// <summary>
        /// Obtiene el reporte de deudores.
        /// </summary>
        public DataTable ReporteDeudores()
        {
            return EjecutarProcedimiento("sp_Vista_Reporte_Deudores_Final");
        }

        /// <summary>
        /// Obtiene el reporte de inventario.
        /// </summary>
        public DataTable ReporteInventario()
        {
            return EjecutarProcedimiento("sp_Vista_Reporte_Inventario_Final");
        }

        /// <summary>
        /// Ejecuta un Procedimiento Almacenado y devuelve un DataTable.
        /// </summary>
        private DataTable EjecutarProcedimiento(string nombrePA, params SqlParameter[] parametros)
        {
            DataTable dt = new DataTable();
            try
            {
                db.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand(nombrePA, db.Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (parametros?.Length > 0)
                    {
                        cmd.Parameters.AddRange(parametros);
                    }

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar " + nombrePA + ": " + ex.Message);
            }
            finally
            {
                db.Cerrar();
            }
            return dt;
        }
    }
}