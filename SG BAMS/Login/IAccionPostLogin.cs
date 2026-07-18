using System.Windows.Forms;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Define el contrato para la acción a ejecutar después de un login exitoso.
    /// Permite polimorfismo por rol en lugar de un switch gigante.
    /// </summary>
    public interface IAccionPostLogin
    {
        /// <summary>
        /// Ejecuta la acción correspondiente al rol después del login exitoso.
        /// </summary>
        /// <param name="formularioActual">Formulario de login actual (para ocultarlo).</param>
        /// <param name="nombreUsuario">Nombre del usuario autenticado.</param>
        /// <param name="rol">Rol del usuario.</param>
        void Ejecutar(Form formularioActual, string nombreUsuario, int rol);
    }
}