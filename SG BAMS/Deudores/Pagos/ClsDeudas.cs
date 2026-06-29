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
        public DataTable ObtenerProductosPorDeuda(int idDeuda)
        {
            DataTable tablaProductos = new DataTable();
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerProductosPorDeuda", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@IDDeuda", SqlDbType.Int).Value = idDeuda;
                    using (SqlDataAdapter adaptador = new SqlDataAdapter(cmd))
                    {
                        adaptador.Fill(tablaProductos);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en ObtenerProductosPorDeuda: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
            return tablaProductos;
        }
        public DataTable ObtenerDeudasPorCliente(int idCliente)
        {
            DataTable tablaDeudas = new DataTable();
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerDeudasPorCliente", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id_cliente", SqlDbType.Int).Value = idCliente;
                    using (SqlDataAdapter adaptador = new SqlDataAdapter(cmd))
                    {
                        adaptador.Fill(tablaDeudas);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en ObtenerDeudasPorCliente: " + ex.Message);
            }
            finally { Cerrar(); }
            return tablaDeudas;
        }
        public async Task<bool> ClienteTieneDeudaActiva(int idCliente)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_ClienteTieneDeudaActiva", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id_cliente", SqlDbType.Int).Value = idCliente;
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