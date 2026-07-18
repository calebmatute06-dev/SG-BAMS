using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

namespace SG_BAMS.MenuPrincipal
{
    /// <summary>
    /// Implementación de IPerfilService para gestionar el perfil de usuario.
    /// </summary>
    internal class ClsPerfil : IPerfilService
    {
        private readonly ClsRepositorioBaseDatos _repositorio;

        /// <summary>
        /// Constructor sin parámetros para compatibilidad.
        /// </summary>
        public ClsPerfil() : this(new ClsRepositorioBaseDatos())
        {
        }

        /// <summary>
        /// Constructor principal con inyección de dependencias.
        /// 
        /// DIP: Recibe el repositorio por constructor en lugar de heredarlo.
        /// </summary>
        /// <param name="repositorio">Repositorio de base de datos.</param>
        /// <exception cref="ArgumentNullException">Si repositorio es nulo.</exception>
        public ClsPerfil(ClsRepositorioBaseDatos repositorio)
        {
            _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
        }

        /// <inheritdoc/>
        public async Task<DataTable> ObtenerPerfilDesdeVista(string nombreUsuario)
        {
            DataTable tablaUsuario = new DataTable();
            try
            {
                _repositorio.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Usuario_ObtenerPerfil", _repositorio.Conectar))
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
                _repositorio.Cerrar();
            }
            return tablaUsuario;
        }

        /// <inheritdoc/>
        public async Task ActualizarFotoUsuario(string nombreUsuario, byte[] imagenBytes)
        {
            try
            {
                _repositorio.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Usuario_ActualizarFoto", _repositorio.Conectar))
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
                _repositorio.Cerrar();
            }
        }
    }
}