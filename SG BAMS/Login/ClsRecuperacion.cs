using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace SG_BAMS.Login
{
    public class ClsRecuperacion
    {
        ClsRepositorioBaseDatos conexion = new ClsRepositorioBaseDatos();

        public bool VerificarCorreo(string correo)
        {
            try
            {
                conexion.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Recuperacion_VerificarCorreo", conexion.Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@correo", correo);
                    int resultado = (int)cmd.ExecuteScalar();
                    return resultado > 0;
                }
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
            string passHash = ClsSeguridad.HashSHA256(nuevaContrasena);
            try
            {
                conexion.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Recuperacion_ActualizarContrasena", conexion.Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pass", passHash);
                    cmd.Parameters.AddWithValue("@correo", correo);
                    return cmd.ExecuteNonQuery() > 0;
                }
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

        public bool ContraIgualAntigua(string correo, string contraant)
        {
            string passHash = ClsSeguridad.HashSHA256(contraant);
            try
            {
                conexion.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Recuperacion_VerificarContrasena", conexion.Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@correo", correo);
                    cmd.Parameters.AddWithValue("@pass", passHash);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al verificar contraseña: " + ex.Message);
                return false;
            }
            finally
            {
                conexion.Cerrar();
            }
        }
    }
}