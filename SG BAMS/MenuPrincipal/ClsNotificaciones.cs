using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SG_BAMS
{
    // Usamos PascalCase para el nombre de la clase
    internal class ClsNotificaciones : ClsConexion
    {
        // El parámetro esAdmin (camelCase) decidirá qué filas traer
        public DataTable ListarNotificaciones(bool esAdmin)
        {
            DataTable tablaDatos = new DataTable();
            try
            {
                AbrirConexion();

                // Si NO es admin, filtramos para que solo vea lo que tiene solo_admin = 0
                string query = "SELECT * FROM Notificaciones";
                if (!esAdmin)
                {
                    query += " WHERE solo_admin = 0";
                }
                query += " ORDER BY fecha DESC"; // Las más recientes arriba

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