using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using SG_BAMS.AccesoDatos;

namespace SG_BAMS
{
    /// <summary>
    /// Implementación de acceso a datos para pagos y consultas de deudas, usando solo
    /// Procedimientos Almacenados. Única responsabilidad: ejecutar las operaciones de datos.
    /// No muestra mensajes de interfaz (a diferencia de la antigua ClsDeudas, que mezclaba
    /// acceso a datos con MessageBox — ver auditoría SOLID, hallazgo CDS01/CDS03); los errores
    /// se degradan de forma silenciosa y es responsabilidad del formulario decidir cómo
    /// informarlos al usuario.
    /// </summary>
    internal class DeudasRepository : ClsRepositorioBaseDatos, IDeudasRepository
    {
        /// <inheritdoc />
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

        /// <inheritdoc />
        public DataRow ObtenerSaldoDetalle(int idDeuda)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Deuda_ObtenerSaldo", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@id_deuda", SqlDbType.Int).Value = idDeuda;
                    DataTable dt = new DataTable();
                    using (SqlDataAdapter adaptador = new SqlDataAdapter(cmd))
                    {
                        adaptador.Fill(dt);
                    }
                    return dt.Rows.Count > 0 ? dt.Rows[0] : null;
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally { Cerrar(); }
        }

        /// <inheritdoc />
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
            catch (Exception)
            {
                // Se degrada a una tabla vacía; el formulario decide cómo informarlo.
            }
            finally { Cerrar(); }
            return tablaDeudores;
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
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
            catch (Exception) { }
            finally
            {
                Cerrar();
            }
            return tablaProductos;
        }

        /// <inheritdoc />
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
            catch (Exception) { }
            finally { Cerrar(); }
            return tablaDeudas;
        }

        /// <inheritdoc />
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
            catch (Exception)
            {
                return false;
            }
            finally
            {
                Cerrar();
            }
        }
    }
}
