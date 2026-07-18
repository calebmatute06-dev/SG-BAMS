using System.Data;

namespace SG_BAMS
{
    /// <summary>
    /// Define el contrato para el servicio de notificaciones tipo toast en pantalla.
    /// </summary>
    public interface IToastService
    {
        /// <summary>
        /// Muestra una notificación temporal en pantalla con un título y mensaje personalizados.
        /// </summary>
        /// <param name="titulo">Título de la notificación.</param>
        /// <param name="mensaje">Contenido del mensaje a mostrar.</param>
        /// <param name="segundos">Tiempo de duración en pantalla. Valor predeterminado: 5 segundos.</param>
        void Mostrar(string titulo, string mensaje, int segundos = 5);

        /// <summary>
        /// Muestra una notificación con el resumen de notificaciones pendientes.
        /// </summary>
        /// <param name="notificaciones">DataTable con las notificaciones a resumir.</param>
        void MostrarResumen(DataTable notificaciones);
    }
}