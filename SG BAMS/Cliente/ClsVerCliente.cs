using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

namespace SG_BAMS.Cliente
{
    /// <summary>
    /// Clase para visualizar clientes usando solo Procedimientos Almacenados.
    /// </summary>
    internal class ClsVerCliente : ClsConexion
    {
        public async Task<DataTable> VerClienteTabla()
        {
            DataTable tablaC = new DataTable();
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Clientes_ListarTodos", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        tablaC.Load(reader);
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                Cerrar();
            }
            return tablaC;
        }

        public bool RTNYaExiste(string rtn, int idClienteActual = 0)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Cliente_VerificarRTN", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@rtn", rtn);
                    cmd.Parameters.AddWithValue("@id", idClienteActual);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al verificar RTN: " + ex.Message);
                return false;
            }
            finally
            {
                Cerrar();
            }
        }
    }
}