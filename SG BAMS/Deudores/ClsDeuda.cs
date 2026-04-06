using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SG_BAMS
{
   
    internal class ClsDeuda : ClsConexion
    {
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