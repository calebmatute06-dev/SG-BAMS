using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS.Administracion_de_BAMS.FormaPago
{
    internal class clsFormaPago : ClsConexion
    {
        public async Task<DataTable> LeerFormasPagoAsync()
        {
            DataTable tabla = new DataTable();
            try
            {
                // Usamos el nombre exacto de tu método en ClsConexion
                AbrirConexion();

                string query = "SELECT * FROM v_DetalleFormasPago";

                using (SqlCommand cmd = new SqlCommand(query, Conectar))
                {
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        tabla.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener formas de pago: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
            return tabla;
        }

        public async Task<bool> InsertarFormaPagoAsync(string descripcion)
        {
            try
            {
                AbrirConexion(); // Usando tu método de ClsConexion
                string query = "INSERT INTO Tipo_Forma_de_pago (descripcion_forma_pago) VALUES (@desc)";

                using (SqlCommand cmd = new SqlCommand(query, Conectar))
                {
                    cmd.Parameters.AddWithValue("@desc", descripcion);

                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar la forma de pago: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }

    }
}

