using System.Data;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Define el contrato para el repositorio de acceso a base de datos
    /// relacionado con la autenticación de usuarios.
    /// </summary>
    public interface IClsRepositorioBaseDatos
    {
        /// <summary>Verifica si un correo electrónico existe en la base de datos.</summary>
        bool ExisteCorreo(string correo);

        /// <summary>Obtiene los datos del usuario por su correo electrónico.</summary>
        DataTable ObtenerUsuarioPorCorreo(string correo);

        /// <summary>Valida un token de recuperación para un correo específico.</summary>
        bool ValidarTokenRecuperacion(string correo, string token);

        /// <summary>Actualiza la contraseña hasheada de un usuario.</summary>
        bool ActualizarContrasena(string correo, string hashContrasena);
    }
}