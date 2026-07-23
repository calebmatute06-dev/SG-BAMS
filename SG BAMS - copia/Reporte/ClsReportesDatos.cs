using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SG_BAMS.Reporte
{
    /// <summary>
    /// Clase de acceso a datos para reportes del sistema BAMS.
    /// Solo usa Procedimientos Almacenados.
    /// Ahora implementa IReportesRepository (RD01) para que quien la
    /// consuma (ReportesAdmin) dependa de la abstracción y no de esta
    /// clase concreta.
    /// </summary>
    internal class ClsReportesDatos : ClsRepositorioBaseDatos, IReportesRepository
    {
        public DataTable ReporteVentas(DateTime desde, DateTime hasta)
        {
            return EjecutarProcedimiento(
                "sp_Vista_Reporte_Ventas_Final",
                new SqlParameter("@fechaInicio", desde),
                new SqlParameter("@fechaFin", hasta)
            );
        }

        public DataTable ReporteCompras(DateTime desde, DateTime hasta)
        {
            return EjecutarProcedimiento(
                "sp_Vista_Reporte_Compras_Final",
                new SqlParameter("@fechaInicio", desde),
                new SqlParameter("@fechaFin", hasta)
            );
        }

        public DataTable ReporteDeudores()
        {
            return EjecutarProcedimiento("sp_Vista_Reporte_Deudores_Final");
        }

        public DataTable ReporteInventario()
        {
            return EjecutarProcedimiento("sp_Vista_Reporte_Inventario_Final");
        }

        /// <summary>
        /// Ejecuta un Procedimiento Almacenado y devuelve un DataTable.
        /// Único criterio de manejo de errores del repositorio (RD03):
        /// siempre relanza envolviendo el nombre del procedimiento; es
        /// responsabilidad de quien la invoca decidir cómo mostrarlo.
        /// </summary>
        private DataTable EjecutarProcedimiento(string nombrePA, params SqlParameter[] parametros)
        {
            DataTable dt = new DataTable();
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand(nombrePA, Conectar))
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
                throw new Exception("Error al ejecutar " + nombrePA + ": " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }
            return dt;
        }
    }
}
