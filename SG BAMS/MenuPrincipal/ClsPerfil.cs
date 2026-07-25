using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading.Tasks;
using SG_BAMS.AccesoDatos;

namespace SG_BAMS.MenuPrincipal
{
    /// <summary>
    /// Implementación del servicio de perfil de usuario.
    /// Gestiona la obtención de datos del perfil y la actualización de la foto
    /// mediante procedimientos almacenados.
    /// </summary>
    internal class ClsPerfil : IPerfilService
    {
        private readonly ClsRepositorioBaseDatos repositorio;

        /// <summary>
        /// Constructor sin parámetros para compatibilidad con código existente.
        /// </summary>
        public ClsPerfil() : this(new ClsRepositorioBaseDatos())
        {
        }

        /// <summary>
        /// Constructor principal que recibe el repositorio de base de datos.
        /// </summary>
        /// <param name="repositorio">Repositorio de base de datos para ejecutar las consultas.</param>
        /// <exception cref="ArgumentNullException">Si repositorio es nulo.</exception>
        public ClsPerfil(ClsRepositorioBaseDatos repositorio)
        {
            this.repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
        }

        /// <inheritdoc/>
        public async Task<DataTable> ObtenerPerfilDesdeVista(string nombreUsuario)
        {
            DataTable tablaUsuario = new DataTable();
            try
            {
                repositorio.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Usuario_ObtenerPerfil", repositorio.Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@usuario", nombreUsuario);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        tablaUsuario.Load(reader);
                    }
                }
            }
            finally
            {
                repositorio.Cerrar();
            }
            return tablaUsuario;
        }

        /// <inheritdoc/>
        public async Task ActualizarFotoUsuario(string nombreUsuario, byte[] imagenBytes)
        {
            try
            {
                repositorio.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Usuario_ActualizarFoto", repositorio.Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@usuario", nombreUsuario);
                    cmd.Parameters.Add("@foto", SqlDbType.VarBinary).Value = imagenBytes;
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al subir imagen: " + ex.Message);
            }
            finally
            {
                repositorio.Cerrar();
            }
        }
    }
}