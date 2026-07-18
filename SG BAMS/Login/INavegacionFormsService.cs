using System.Windows.Forms;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Define el contrato para la navegación entre formularios del sistema.
    /// </summary>
    public interface INavegacionFormsService
    {
        /// <summary>
        /// Navega al formulario de nueva contraseña.
        /// </summary>
        /// <param name="correo">Correo del usuario.</param>
        /// <param name="recuperacionService">Servicio de recuperación.</param>
        void NavegarANuevaContrasena(string correo, IRecuperacionService recuperacionService);

        /// <summary>
        /// Regresa al formulario de login principal.
        /// </summary>
        void NavegarAlLogin();

        /// <summary>
        /// Cierra el formulario actual.
        /// </summary>
        /// <param name="formularioActual">Formulario a cerrar.</param>
        void CerrarFormulario(Form formularioActual);

        /// <summary>
        /// Navega a un formulario del tipo especificado.
        /// El formulario debe tener un constructor sin parámetros.
        /// 
        /// OCP: Permite agregar nuevas navegaciones sin modificar esta interfaz
        /// ni los formularios que la usan.
        /// </summary>
        /// <typeparam name="T">Tipo de formulario a abrir (debe heredar de Form y tener constructor sin parámetros).</typeparam>
        void IrA<T>() where T : Form, new();

        /// <summary>
        /// Muestra el formulario especificado.
        /// </summary>
        /// <param name="formulario">Formulario a mostrar.</param>
        void MostrarFormulario(Form formulario);
    }
}