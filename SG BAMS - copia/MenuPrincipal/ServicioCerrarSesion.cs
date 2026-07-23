using SG_BAMS.Login;
using System;
using System.Windows.Forms;

namespace SG_BAMS.MenuPrincipal
{
    /// <summary>
    /// Implementación del servicio de cierre de sesión del sistema.
    /// Solicita confirmación al usuario y, si acepta, cierra la sesión actual
    /// regresando al formulario de inicio de sesión.
    /// </summary>
    public class ServicioCerrarSesion : IServicioCerrarSesion
    {
        private readonly INavegacionFormsService navegacionForms;

        /// <summary>
        /// Constructor del servicio de cierre de sesión.
        /// </summary>
        /// <param name="navegacionForms">Servicio de navegación entre formularios.</param>
        /// <exception cref="ArgumentNullException">Si navegacionForms es nulo.</exception>
        public ServicioCerrarSesion(INavegacionFormsService navegacionForms)
        {
            this.navegacionForms = navegacionForms ?? throw new ArgumentNullException(nameof(navegacionForms));
        }

        /// <inheritdoc/>
        public void CerrarSesion(Form formularioActual)
        {
            if (MessageBox.Show("¿Está seguro que desea regresar al inicio de sesión?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                navegacionForms.NavegarAlLogin();
                formularioActual.Close();
            }
        }
    }
}