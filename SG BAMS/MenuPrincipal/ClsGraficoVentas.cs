using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace SG_BAMS
{
    
    internal class ClsGraficoVentas : ClsConexion
    {
       
        public async Task<DataTable> ObtenerProductosMasVendidos()
        {
            DataTable tablaVentas = new DataTable();

            try
            {
                
                AbrirConexion();

                string consultaSql = "SELECT * FROM vista_productos_mas_vendidos";

                using (SqlCommand comandoSql = new SqlCommand(consultaSql, Conectar))
                {
                    using (SqlDataReader lectorDatos = await comandoSql.ExecuteReaderAsync())
                    {
                        tablaVentas.Load(lectorDatos);
                    }
                }
            }
            catch (Exception ex)
            {
                
                throw new Exception("Error al obtener ventas para el gráfico: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }

            return tablaVentas;
        }
    }
}
