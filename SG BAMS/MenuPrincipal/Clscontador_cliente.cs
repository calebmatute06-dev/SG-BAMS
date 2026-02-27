using System;
using System.Data;
using Microsoft.Data.SqlClient; 
using System.Threading.Tasks;

namespace SG_BAMS.MenuPrincipal
{
   
    internal class Clscontador_cliente : ClsConexion
    {
        public async Task<int> ObtenerTotalClientes()
        {
            try
            {
                
                AbrirConexion();

                string query = "SELECT COUNT(*) FROM Cliente WHERE id_estado = 1";

                
                using (SqlCommand cmd = new SqlCommand(query, Conectar))
                {
                    
                    object resultado = await cmd.ExecuteScalarAsync();
                    return resultado != null ? Convert.ToInt32(resultado) : 0;
                }
            }
            catch (Exception ex)
            {
                
                return -1;
            }
            finally
            {
                
                Cerrar();
            }
        }
    }
}
