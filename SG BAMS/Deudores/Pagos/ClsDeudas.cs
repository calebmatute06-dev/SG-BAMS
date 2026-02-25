using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace SG_BAMS
{
    public class ClsDeudas : ClsConexion
    {
        // Método para registrar el pago usando el PA_insertar_pago_deuda
        public async Task<bool> InsertarPago(int idDeuda, decimal montoPago, DateTime fechaPago)
        {
            try
            {
                AbrirConexion();

                using (SqlCommand comando = new SqlCommand("PA_insertar_pago_deuda", Conectar))
                {
                    comando.CommandType = CommandType.StoredProcedure;

                    // Definición de parámetros con tipos de datos explícitos de SQL
                    comando.Parameters.Add("@id_deuda", SqlDbType.Int).Value = idDeuda;
                    comando.Parameters.Add("@monto_pago", SqlDbType.Money).Value = montoPago;
                    comando.Parameters.Add("@fecha_pago", SqlDbType.Date).Value = fechaPago;

                    await comando.ExecuteNonQueryAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // El error puede ser capturado aquí para depuración
                return false;
            }
            finally
            {
                Cerrar();
            }
        }

        // Método para obtener los deudores desde la vista y llenar el ComboBox
        public DataTable ObtenerDeudoresActivos()
        {
            DataTable tablaDeudores = new DataTable();
            try
            {
                AbrirConexion();

                // Consultamos la vista que creaste previamente
                string consultaSql = "SELECT ID, [Nombre completo] FROM vista_nombres_clientes";

                using (SqlCommand comando = new SqlCommand(consultaSql, Conectar))
                {
                    using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                    {
                        adaptador.Fill(tablaDeudores);
                    }
                }
            }
            catch (Exception ex)
            {
                // Retornar tabla vacía en caso de error
            }
            finally
            {
                Cerrar();
            }
            return tablaDeudores;
        }
    }
}