using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SG_BAMS
{
    
    internal class ClsNotificaciones : ClsConexion
    {
        
        public DataTable ListarNotificaciones(bool esAdmin)
        {
            DataTable tablaDatos = new DataTable();
            try
            {
                AbrirConexion();

               
                string query = "SELECT * FROM Notificaciones";
                if (!esAdmin)
                {
                    query += " WHERE solo_admin = 0";
                }
                query += " ORDER BY fecha DESC"; 

                using (SqlDataAdapter adaptador = new SqlDataAdapter(query, Conectar))
                {
                    adaptador.Fill(tablaDatos);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar notificaciones: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
            return tablaDatos;
        }
    }
}