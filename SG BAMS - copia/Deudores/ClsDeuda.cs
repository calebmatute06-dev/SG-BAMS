using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SG_BAMS
{
    /// <summary>
    /// Clase para listar deudores usando solo Procedimientos Almacenados.
    /// </summary>
    internal class ClsDeuda : ClsRepositorioBaseDatos
    {
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
                throw new Exception("Error al listar deudores: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
            return tablaDeudores;
        }


        public async Task<bool> CrearDeudaManual(int idFactura, int idCliente, double montoTotal, DateTime fechaVenta)
        {
            ClsRepositorioBaseDatos conexion = new ClsRepositorioBaseDatos();
            try
            {
                conexion.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Deuda_CrearOActualizar", conexion.Conectar))
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
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear/actualizar deuda: " + ex.Message);
                return false;
            }
            finally
            {
                conexion.Cerrar();
            }
        }
    }
}