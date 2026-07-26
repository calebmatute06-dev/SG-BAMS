using System.Windows.Forms;
using System.Data;
using System;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Define el contrato para la navegación entre formularios del sistema.
    /// Centraliza las operaciones de apertura y cierre de formularios
    /// para desacoplar la navegación de la lógica de cada pantalla.
    /// </summary>
    public interface INavegacionFormsService
    {
        /// <summary>
        /// Navega al formulario de establecimiento de nueva contraseña
        /// después de validar el token de recuperación.
        /// </summary>
        /// <param name="correo">Correo del usuario que solicita el cambio.</param>
        /// <param name="recuperacionService">Servicio de recuperación de contraseña.</param>
        void NavegarANuevaContrasena(string correo, IRecuperacionService recuperacionService);

        /// <summary>
        /// Regresa al formulario principal de inicio de sesión.
        /// </summary>
        void NavegarAlLogin();

        /// <summary>
        /// Cierra el formulario especificado.
        /// </summary>
        /// <param name="formularioActual">Formulario que se desea cerrar.</param>
        void CerrarFormulario(Form formularioActual);

        /// <summary>
        /// Abre un formulario del tipo especificado utilizando su constructor sin parámetros.
        /// </summary>
        /// <typeparam name="T">Tipo de formulario a abrir. Debe heredar de Form y tener constructor sin parámetros.</typeparam>
        void IrA<T>() where T : Form, new();

        /// <summary>
        /// Muestra un formulario que ya ha sido instanciado previamente.
        /// </summary>
        /// <param name="formulario">Formulario a mostrar.</param>
        void MostrarFormulario(Form formulario);
    }
}