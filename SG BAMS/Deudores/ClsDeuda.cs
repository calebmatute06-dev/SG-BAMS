using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SG_BAMS
{

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="SG_BAMS.ClsConexion" />
    internal class ClsDeuda : ClsConexion
    {
        /// <summary>
        /// Listars the deudores.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al listar deudores: " + ex.Message</exception>
        public DataTable ListarDeudores()
        {
            DataTable tablaDeudores = new DataTable();

            try
            {
                
                AbrirConexion();

                
                string query = "SELECT * FROM vista_lista_deudores";

                
                using (SqlCommand comando = new SqlCommand(query, Conectar))
                {
                    using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
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
    }
}