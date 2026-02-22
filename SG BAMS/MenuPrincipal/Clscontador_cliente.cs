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
                // Usamos el método de tu clase base para abrir la conexión
                AbrirConexion();

                string query = "SELECT COUNT(*) FROM Cliente WHERE id_estado = 1";

                // Usamos la variable 'Conectar' que es protected en tu ClsConexion
                using (SqlCommand cmd = new SqlCommand(query, Conectar))
                {
                    // Ejecutamos de forma asíncrona para no congelar el Menú
                    object resultado = await cmd.ExecuteScalarAsync();
                    return resultado != null ? Convert.ToInt32(resultado) : 0;
                }
            }
            catch (Exception ex)
            {
                // Opcional: registrar el error
                return -1;
            }
            finally
            {
                // Siempre cerramos la conexión después de usarla
                Cerrar();
            }
        }
    }
}
