using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using SG_BAMS.AccesoDatos;

namespace SG_BAMS
{
    /// <summary>
    /// Implementación de acceso a datos para el listado de deudores y la creación/actualización
    /// automática de deudas, usando solo Procedimientos Almacenados.
    /// Reemplaza a la antigua clase ClsDeuda (ver auditoría SOLID).
    /// </summary>
    public class DeudaRepository : ClsRepositorioBaseDatos, IDeudaRepository
    {
        /// <inheritdoc />
        public DataTable ListarDeudores()
        {
            DataTable tablaDeudores = new DataTable();
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_vista_lista_deudores", Conectar))
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
                throw new ApplicationException("Error al listar deudores.", ex);
            }
            finally
            {
                Cerrar();
            }
            return tablaDeudores;
        }

        /// <inheritdoc />
        public async Task<bool> CrearDeudaManual(int idFactura, int idCliente, double montoTotal, DateTime fechaVenta)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Deuda_CrearOActualizar", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idFactura", idFactura);
                    cmd.Parameters.AddWithValue("@idCliente", idCliente);
                    cmd.Parameters.AddWithValue("@monto", montoTotal);
                    cmd.Parameters.AddWithValue("@fechaVenta", fechaVenta);
                    await cmd.ExecuteNonQueryAsync();
                    return true;
                }
            }
            catch
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
