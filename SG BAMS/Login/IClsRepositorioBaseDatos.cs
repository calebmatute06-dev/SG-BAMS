using System.Data;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Define el contrato para el repositorio de acceso a base de datos
    /// relacionado con la autenticación de usuarios.
    /// </summary>
    public interface IClsRepositorioBaseDatos
    {
        /// <summary>
        /// Verifica si un correo electrónico existe en la base de datos.
        /// </summary>
        /// <param name="correo">Correo electrónico a verificar.</param>
        /// <returns>True si el correo está registrado en el sistema.</returns>
        bool ExisteCorreo(string correo);

        /// <summary>
        /// Obtiene los datos del usuario asociado a un correo electrónico.
        /// </summary>
        /// <param name="correo">Correo electrónico del usuario.</param>
        /// <returns>DataTable con los datos del usuario.</returns>
        DataTable ObtenerUsuarioPorCorreo(string correo);

        /// <summary>
        /// Valida un token de recuperación de contraseña para un correo específico.
        /// </summary>
        /// <param name="correo">Correo del usuario que solicitó la recuperación.</param>
        /// <param name="token">Token de recuperación a validar.</param>
        /// <returns>True si el token es válido para ese correo.</returns>
        bool ValidarTokenRecuperacion(string correo, string token);

        /// <summary>
        /// Actualiza la contraseña de un usuario con el hash proporcionado.
        /// </summary>
        /// <param name="correo">Correo del usuario.</param>
        /// <param name="hashContrasena">Nuevo hash de contraseña SHA256.</param>
        /// <returns>True si la actualización fue exitosa.</returns>
        bool ActualizarContrasena(string correo, string hashContrasena);
    }
}