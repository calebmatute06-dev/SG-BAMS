using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace SG_BAMS
{
    public class ClsDeudas : ClsConexion
    {
        
        public async Task<bool> InsertarPago(int idDeuda, decimal montoPago, DateTime fechaPago)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand comando = new SqlCommand("PA_insertar_pago_deuda", Conectar))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@id_deuda", SqlDbType.Int).Value = idDeuda;
                    comando.Parameters.Add("@monto_pago", SqlDbType.Money).Value = montoPago;
                    comando.Parameters.Add("@fecha_pago", SqlDbType.Date).Value = fechaPago;

                    await comando.ExecuteNonQueryAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                Cerrar();
            }
        }

        public async Task<decimal> ObtenerSaldo(int idDeuda)
        {
            decimal saldo = 0;
            string query = @"
            SELECT (d.monto_inicial - ISNULL((SELECT SUM(pd.monto_pago) 
                                             FROM Pago_deuda pd 
                                             WHERE pd.id_deuda = d.id_deuda), 0)) AS SaldoPendiente
            FROM Deuda d
            WHERE d.id_deuda = @id_deuda";

            try
            {
                AbrirConexion(); // Usamos los métodos de ClsConexion
                using (SqlCommand cmd = new SqlCommand(query, Conectar)) // Usamos la propiedad Conectar de la base
                {
                    cmd.Parameters.AddWithValue("@id_deuda", idDeuda);

                    object result = await cmd.ExecuteScalarAsync();
                    if (result != null && result != DBNull.Value)
                    {
                        saldo = Convert.ToDecimal(result);
                    }
                }
            }
            catch (Exception ex)
            {
                // Puedes registrar el error aquí si lo deseas
                System.Diagnostics.Debug.WriteLine("Error en ObtenerSaldo: " + ex.Message);
            }
            finally
            {
                Cerrar(); // Cerramos siempre la conexión
            }

            return saldo;
        }
        public DataTable ObtenerDeudoresActivos()
        {
            DataTable tablaDeudores = new DataTable();
            try
            {
                AbrirConexion();
                // Usamos la vista especializada que creaste y asignamos alias simples (ID, ClienteDetalle)
                string consultaSql = @"SELECT 
                                ID, 
                                [Nombre completo] + ' (Saldo: L.' + CAST([Saldo Real] AS VARCHAR) + ')' AS ClienteDetalle 
                               FROM vista_deudores_pendientes";

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
                System.Windows.Forms.MessageBox.Show("Error al cargar deudores: " + ex.Message);
            }
            finally { Cerrar(); }
            return tablaDeudores;
        }

        
        public DataTable ObtenerUltimasVentas()
        {
            DataTable tablaVentas = new DataTable();
            try
            {
                AbrirConexion();
                string consultaSql = "SELECT * FROM vista_ultimas_ventas";
                using (SqlCommand comando = new SqlCommand(consultaSql, Conectar))
                {
                    using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                    {
                        adaptador.Fill(tablaVentas);
                    }
                }
            }
            catch (Exception) { }
            finally { Cerrar(); }
            return tablaVentas;
        }
    }
}