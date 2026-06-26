using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace SG_BAMS
{
    /// <summary>
    /// Clase para gestión de deudas usando solo Procedimientos Almacenados.
    /// </summary>
    public class ClsDeudas : ClsConexion
    {
        public async Task<bool> InsertarPago(int idDeuda, decimal montoPago, DateTime fechaPago)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("PA_insertar_pago_deuda", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id_deuda", SqlDbType.Int).Value = idDeuda;
                    cmd.Parameters.Add("@monto_pago", SqlDbType.Money).Value = montoPago;
                    cmd.Parameters.Add("@fecha_pago", SqlDbType.Date).Value = fechaPago;
                    await cmd.ExecuteNonQueryAsync();
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
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Deuda_ObtenerSaldo", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_deuda", idDeuda);
                    object result = await cmd.ExecuteScalarAsync();
                    if (result != null && result != DBNull.Value)
                        saldo = Convert.ToDecimal(result);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en ObtenerSaldo: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
            return saldo;
        }

        public DataTable ObtenerDeudoresActivos()
        {
            DataTable tablaDeudores = new DataTable();
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Deudores_ActivosCombo", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adaptador = new SqlDataAdapter(cmd))
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
                using (SqlCommand cmd = new SqlCommand("sp_vista_ultimas_ventas", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adaptador = new SqlDataAdapter(cmd))
                    {
                        adaptador.Fill(tablaVentas);
                    }
                }
            }
            catch (Exception) { }
            finally { Cerrar(); }
            return tablaVentas;
        }

        public async Task<bool> ClienteTieneDeudaActiva(int idCliente)
        {
            try
            {
                AbrirConexion();
                string query = @"SELECT COUNT(*) 
                         FROM Deuda 
                         WHERE id_cliente = @id_cliente 
                           AND id_estado = 1";
                using (SqlCommand cmd = new SqlCommand(query, Conectar))
                {
                    cmd.Parameters.AddWithValue("@id_cliente", idCliente);
                    object resultado = await cmd.ExecuteScalarAsync();
                    return resultado != null && Convert.ToInt32(resultado) > 0;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en ClienteTieneDeudaActiva: " + ex.Message);
                return false;
            }
            finally
            {
                Cerrar();
            }
        }

    }
}