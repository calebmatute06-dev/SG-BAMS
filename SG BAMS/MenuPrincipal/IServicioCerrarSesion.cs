using System.Windows.Forms;

namespace SG_BAMS.MenuPrincipal
{
    /// <summary>
    /// Define el contrato para el servicio de cierre de sesión.
    /// Centraliza la lógica duplicada de confirmación de cierre de sesión.
    /// </summary>
    public interface IServicioCerrarSesion
    {
        /// <summary>
        /// Muestra confirmación y cierra sesión si el usuario acepta.
        /// </summary>
        /// <param name="formularioActual">Formulario a cerrar tras confirmar.</param>
        void CerrarSesion(Form formularioActual);
    }
}