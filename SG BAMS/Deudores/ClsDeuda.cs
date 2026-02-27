using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SG_BAMS
{
    // Heredamos de ClsConexion para acceder a 'Conectar', 'AbrirConexion' y 'Cerrar'
    internal class ClsDeuda : ClsConexion
    {
        public DataTable ListarDeudores()
        {
            DataTable tablaDeudores = new DataTable();

            try
            {
                // 1. Usamos tu método de la clase padre
                AbrirConexion();

                // 2. Ejecutamos la consulta sobre la vista
                string query = "SELECT * FROM vista_lista_deudores";

                // Usamos el objeto 'Conectar' que es protected en ClsConexion
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
                // 3. Siempre cerramos la conexión pase lo que pase
                Cerrar();
            }

            return tablaDeudores;
        }
    }
}