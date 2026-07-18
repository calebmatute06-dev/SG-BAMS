using SG_BAMS.Login;
using System;
using System.Windows.Forms;

namespace SG_BAMS.MenuPrincipal
{
    /// <summary>
    /// Implementación de IServicioCerrarSesion.
    /// </summary>
    public class ServicioCerrarSesion : IServicioCerrarSesion
    {
        private readonly INavegacionFormsService _navegacionForms;

        public ServicioCerrarSesion(INavegacionFormsService navegacionForms)
        {
            _navegacionForms = navegacionForms ?? throw new ArgumentNullException(nameof(navegacionForms));
        }

        /// <inheritdoc/>
        public void CerrarSesion(Form formularioActual)
        {
            if (MessageBox.Show("¿Está seguro que desea regresar al inicio de sesión?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _navegacionForms.NavegarAlLogin();
                formularioActual.Close();
            }
        }
    }
}