using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS.Login
{
    public class ClsRecuperacion
    {
        ClsConexion conexion = new ClsConexion();

        public bool VerificarCorreo(string correo)
        {
            try
            {
                conexion.AbrirConexion();
                string query = "SELECT COUNT(*) FROM credenciales_usuario WHERE Correo = @correo";
                SqlCommand cmd = new SqlCommand(query, conexion.Conectar);
                cmd.Parameters.AddWithValue("@correo", correo);
                int resultado = (int)cmd.ExecuteScalar();
                return resultado > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar correo: " + ex.Message);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        public bool ActualizarContrasena(string correo, string nuevaContrasena)
        {
            try
            {
                conexion.AbrirConexion();
                string query = "UPDATE credenciales_usuario SET Contraseña = @pass WHERE Correo = @correo";
                SqlCommand cmd = new SqlCommand(query, conexion.Conectar);
                cmd.Parameters.AddWithValue("@pass", nuevaContrasena);
                cmd.Parameters.AddWithValue("@correo", correo);
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar contraseña: " + ex.Message);
            }
            finally
            {
                conexion.Cerrar();
            }
        }
    }
}
