using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace SG_BAMS
{
    public class ClsDeudas : ClsConexion
    {
        // 1. Método para registrar el pago (Llama a tu PA)
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

        // 2. Método para llenar el ComboBox (Usa tu nueva vista_lista_deudores o similar)
        public DataTable ObtenerDeudoresActivos()
        {
            DataTable tablaDeudores = new DataTable();
            try
            {
                AbrirConexion();
                // Seleccionamos solo los que tienen estado 'Activo' o similar según tu vista
                string consultaSql = "SELECT [ID Deuda] AS ID, Cliente FROM vista_lista_deudores WHERE [Estado Deuda] = 'Activo'";

                using (SqlCommand comando = new SqlCommand(consultaSql, Conectar))
                {
                    using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                    {
                        adaptador.Fill(tablaDeudores);
                    }
                }
            }
            catch (Exception) { /* Manejo de error */ }
            finally { Cerrar(); }
            return tablaDeudores;
        }

        // 3. Método para el DataGridView de últimas ventas (Menu Principal)
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