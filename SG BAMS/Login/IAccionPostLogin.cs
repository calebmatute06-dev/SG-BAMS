using System.Windows.Forms;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Define el contrato para la acción a ejecutar después de un inicio de sesión exitoso.
    /// Cada implementación representa el comportamiento específico para un rol de usuario.
    /// </summary>
    public interface IAccionPostLogin
    {
        /// <summary>
        /// Ejecuta la acción correspondiente al rol después del inicio de sesión exitoso.
        /// </summary>
        /// <param name="formularioActual">Formulario de login actual, que será ocultado.</param>
        /// <param name="nombreUsuario">Nombre del usuario que inició sesión.</param>
        /// <param name="rol">Identificador del rol del usuario.</param>
        void Ejecutar(Form formularioActual, string nombreUsuario, int rol);
    }
}