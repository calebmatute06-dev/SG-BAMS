using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Clase encargada de la validación de credenciales de usuario.
    /// 
    /// PRINCIPIOS SOLID APLICADOS:
    /// - SRP: Validar credenciales contra la BD.
    /// - DIP: Recibe IServicioSeguridad por constructor. No llama a ClsSeguridad estático.
    /// </summary>
    internal class ClsLogin : ClsRepositorioBaseDatos
    {
        private readonly IServicioSeguridad _servicioSeguridad;

        public static int idusuario;
        public static int RolUsuario;
        public string NombreUsuario { get; set; }
        public string NombreCompleto { get; set; }
        public int IdUsuarioInstancia => idusuario;

        /// <summary>
        /// Constructor sin parámetros para compatibilidad.
        /// </summary>
        public ClsLogin() : this(new ServicioSeguridad())
        {
        }

        /// <summary>
        /// Constructor principal con inyección de dependencias.
        /// </summary>
        /// <param name="servicioSeguridad">Servicio de seguridad para hashing.</param>
        public ClsLogin(IServicioSeguridad servicioSeguridad)
        {
            _servicioSeguridad = servicioSeguridad ?? throw new ArgumentNullException(nameof(servicioSeguridad));
        }

        public int ValidarUsuario(string usuario_correo, string contra)
        {
            int rol = 0;
            try
            {
                string contraHasheada = _servicioSeguridad.HashSHA256(contra);
                AbrirConexion();

                using (SqlCommand cmd = new SqlCommand("sp_Login_ValidarUsuario", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@usuario_correo", usuario_correo);
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

                                string nombreCompleto = reader["NombreUsuario"].ToString();
                                NombreCompleto = nombreCompleto;

                                if (!string.IsNullOrEmpty(nombreCompleto) && nombreCompleto.Contains('_'))
                                {
                                    NombreUsuario = nombreCompleto.Substring(0, nombreCompleto.IndexOf('_'));
                                }
                                else
                                {
                                    NombreUsuario = nombreCompleto;
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