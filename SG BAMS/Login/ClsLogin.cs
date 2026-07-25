using Microsoft.Data.SqlClient;
using System;
using System.Data;
using SG_BAMS.AccesoDatos;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Clase encargada de la validación de credenciales de usuario
    /// utilizando el procedimiento almacenado sp_Login_ValidarUsuario.
    /// Hereda de ClsRepositorioBaseDatos para reutilizar la conexión a BD.
    /// </summary>
    internal class ClsLogin : ClsRepositorioBaseDatos
    {
        private readonly IServicioSeguridad servicioSeguridad;

        /// <summary>ID del usuario autenticado. Se mantiene estático por compatibilidad.</summary>
        public static int idusuario;

        /// <summary>Rol del usuario autenticado. Se mantiene estático por compatibilidad.</summary>
        public static int RolUsuario;

        /// <summary>
        /// Nombre recortado del usuario (primer nombre).
        /// Se usa para reconocimiento facial donde los archivos tienen formato "Nombre_numero.jpg".
        /// </summary>
        public string NombreUsuario { get; set; }

        /// <summary>
        /// Nombre completo del usuario tal como viene de la base de datos.
        /// Se usa para consultas que requieren el nombre exacto, como el perfil.
        /// </summary>
        public string NombreCompleto { get; set; }

        /// <summary>Expone el ID del usuario como propiedad de instancia.</summary>
        public int IdUsuarioInstancia => idusuario;

        /// <summary>
        /// Constructor sin parámetros para compatibilidad con código existente.
        /// </summary>
        public ClsLogin() : this(new ServicioSeguridad())
        {
        }

        /// <summary>
        /// Constructor principal con inyección de dependencias.
        /// Cumple DIP al recibir IServicioSeguridad en lugar de instanciarlo.
        /// </summary>
        /// <param name="servicioSeguridad">Servicio de seguridad para hashing de contraseñas.</param>
        /// <exception cref="ArgumentNullException">Si servicioSeguridad es nulo.</exception>
        public ClsLogin(IServicioSeguridad servicioSeguridad)
        {
            this.servicioSeguridad = servicioSeguridad ?? throw new ArgumentNullException(nameof(servicioSeguridad));
        }

        /// <summary>
        /// Valida las credenciales del usuario contra la base de datos.
        /// Hashea la contraseña internamente y ejecuta el procedimiento almacenado.
        /// </summary>
        /// <param name="usuarioOCorreo">Nombre de usuario o correo electrónico.</param>
        /// <param name="contrasena">Contraseña en texto plano.</param>
        /// <returns>
        /// ID del rol si las credenciales son válidas: 1=Admin, 2=Empleado, 3=Soporte.
        /// Retorna -1 si el usuario está inactivo, 0 si las credenciales son inválidas.
        /// </returns>
        /// <exception cref="Exception">Si ocurre un error de conexión o consulta.</exception>
        public int ValidarUsuario(string usuarioOCorreo, string contrasena)
        {
            int rol = 0;
            try
            {
                string contraHasheada = servicioSeguridad.HashSHA256(contrasena);
                AbrirConexion();

                using (SqlCommand cmd = new SqlCommand("sp_Login_ValidarUsuario", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@usuario_correo", usuarioOCorreo);
                    cmd.Parameters.AddWithValue("@contra", contraHasheada);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int estado = Convert.ToInt32(reader["ID_Estado"]);
                            if (estado == 1)
                            {
                                rol = Convert.ToInt32(reader["ID_Rol_Usuario"]);
                                idusuario = Convert.ToInt32(reader["ID_Usuario"]);

                                string nombreCompletoBD = reader["NombreUsuario"].ToString();
                                NombreCompleto = nombreCompletoBD;

                                if (!string.IsNullOrEmpty(nombreCompletoBD) && nombreCompletoBD.Contains('_'))
                                {
                                    NombreUsuario = nombreCompletoBD.Substring(0, nombreCompletoBD.IndexOf('_'));
                                }
                                else
                                {
                                    NombreUsuario = nombreCompletoBD;
                                }

                                RolUsuario = rol;
                            }
                            else
                            {
                                rol = -1;
                            }
                        }
                        else
                        {
                            rol = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar el usuario: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
            return rol;
        }
    }
}