using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace SG_BAMS
{
    // Heredamos de ClsConexion para usar el objeto 'Conectar'
    internal class ClsGraficoVentas : ClsConexion
    {
        // Método en PascalCase y variables internas en camelCase
        public async Task<DataTable> ObtenerProductosMasVendidos()
        {
            DataTable tablaVentas = new DataTable();

            try
            {
                // Usamos los métodos de tu clase base
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
                // Manejo de errores amigable
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
