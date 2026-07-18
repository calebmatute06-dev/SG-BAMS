using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Clase responsable de las operaciones de recuperación de contraseña
    /// contra la base de datos utilizando procedimientos almacenados.
    /// </summary>
    public class ClsRecuperacion
    {
        private readonly ClsRepositorioBaseDatos conexion;
        private readonly IServicioSeguridad servicioSeguridad;

        /// <summary>
        /// Constructor sin parámetros para compatibilidad con código existente.
        /// </summary>
        public ClsRecuperacion() : this(new ClsRepositorioBaseDatos(), new ServicioSeguridad())
        {
        }

        /// <summary>
        /// Constructor principal con inyección de dependencias.
        /// Cumple DIP al recibir las dependencias en lugar de instanciarlas.
        /// </summary>
        /// <param name="conexion">Repositorio de base de datos.</param>
        /// <param name="servicioSeguridad">Servicio de seguridad para hashing.</param>
        /// <exception cref="ArgumentNullException">Si algún parámetro es nulo.</exception>
        public ClsRecuperacion(ClsRepositorioBaseDatos conexion, IServicioSeguridad servicioSeguridad)
        {
            this.conexion = conexion ?? throw new ArgumentNullException(nameof(conexion));
            this.servicioSeguridad = servicioSeguridad ?? throw new ArgumentNullException(nameof(servicioSeguridad));
        }

        /// <summary>
        /// Verifica si un correo electrónico existe en la base de datos.
        /// </summary>
        /// <param name="correo">Correo electrónico a verificar.</param>
        /// <returns>True si el correo está registrado en el sistema.</returns>
        /// <exception cref="Exception">Si ocurre un error de base de datos.</exception>
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

        /// <summary>
        /// Actualiza la contraseña de un usuario en la base de datos.
        /// La contraseña se hashea con SHA256 antes de enviarla al procedimiento almacenado.
        /// Si no se lanza excepción, se asume que la operación fue exitosa.
        /// </summary>
        /// <param name="correo">Correo del usuario.</param>
        /// <param name="nuevaContrasena">Nueva contraseña en texto plano.</param>
        /// <exception cref="Exception">Si ocurre un error de base de datos.</exception>
        public void ActualizarContrasena(string correo, string nuevaContrasena)
        {
            string passHash = servicioSeguridad.HashSHA256(nuevaContrasena);
            try
            {
                conexion.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Recuperacion_ActualizarContrasena", conexion.Conectar))
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
                conexion.Cerrar();
            }
        }

        /// <summary>
        /// Verifica si la contraseña ingresada es igual a la contraseña actual del usuario.
        /// </summary>
        /// <param name="correo">Correo del usuario.</param>
        /// <param name="contrasenaAnterior">Contraseña a comparar en texto plano (se hashea internamente).</param>
        /// <returns>True si la contraseña coincide con la actual.</returns>
        /// <exception cref="Exception">Si ocurre un error de base de datos.</exception>
        public bool ContraIgualAntigua(string correo, string contrasenaAnterior)
        {
            string passHash = servicioSeguridad.HashSHA256(contrasenaAnterior);
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
                throw new Exception("Error al verificar contraseña: " + ex.Message);
            }
            finally
            {
                conexion.Cerrar();
            }
        }
    }
}