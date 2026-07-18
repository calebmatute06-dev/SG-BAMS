using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Clase responsable de las operaciones de recuperación de contraseña contra la BD.
    /// </summary>
    public class ClsRecuperacion
    {
        private readonly ClsRepositorioBaseDatos _conexion;
        private readonly IServicioSeguridad _servicioSeguridad;

        /// <summary>
        /// Constructor sin parámetros para compatibilidad.
        /// </summary>
        public ClsRecuperacion() : this(new ClsRepositorioBaseDatos(), new ServicioSeguridad())
        {
        }

        /// <summary>
        /// Constructor principal con inyección de dependencias.
        /// </summary>
        public ClsRecuperacion(ClsRepositorioBaseDatos conexion, IServicioSeguridad servicioSeguridad)
        {
            _conexion = conexion ?? throw new ArgumentNullException(nameof(conexion));
            _servicioSeguridad = servicioSeguridad ?? throw new ArgumentNullException(nameof(servicioSeguridad));
        }

        /// <summary>
        /// Verifica si un correo electrónico existe en la base de datos.
        /// </summary>
        public bool VerificarCorreo(string correo)
        {
            try
            {
                _conexion.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Recuperacion_VerificarCorreo", _conexion.Conectar))
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
                _conexion.Cerrar();
            }
        }

        /// <summary>
        /// Actualiza la contraseña de un usuario en la base de datos.
        /// La contraseña se hashea antes de enviarla al SP.
        /// </summary>
        public void ActualizarContrasena(string correo, string nuevaContrasena)
        {
            string passHash = _servicioSeguridad.HashSHA256(nuevaContrasena);
            try
            {
                _conexion.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Recuperacion_ActualizarContrasena", _conexion.Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@pass", passHash);
                    cmd.Parameters.AddWithValue("@correo", correo);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar contraseña: " + ex.Message);
            }
            finally
            {
                _conexion.Cerrar();
            }
        }

        /// <summary>
        /// Verifica si la contraseña ingresada es igual a la contraseña actual del usuario.
        /// </summary>
        public bool ContraIgualAntigua(string correo, string contraant)
        {
            string passHash = _servicioSeguridad.HashSHA256(contraant);
            try
            {
                _conexion.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Recuperacion_VerificarContrasena", _conexion.Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@correo", correo);
                    cmd.Parameters.AddWithValue("@pass", passHash);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar contraseña: " + ex.Message);
            }
            finally
            {
                _conexion.Cerrar();
            }
        }
    }
}