using System.Windows.Forms;

namespace SG_BAMS.MenuPrincipal
{
    /// <summary>
    /// Define el contrato para el servicio de cierre de sesión del sistema.
    /// </summary>
    public interface IServicioCerrarSesion
    {
        /// <summary>
        /// Solicita confirmación al usuario y, si acepta, cierra la sesión actual
        /// regresando al formulario de inicio de sesión.
        /// </summary>
        /// <param name="formularioActual">Formulario que será cerrado después de confirmar el cierre de sesión.</param>
        void CerrarSesion(Form formularioActual);
    }
}