using System.Data;

namespace SG_BAMS
{
    /// <summary>
    /// Define el contrato para el servicio de notificaciones toast.
    /// </summary>
    public interface IToastService
    {
        /// <summary>
        /// Muestra un toast con título y mensaje.
        /// </summary>
        /// <param name="titulo">Título del toast.</param>
        /// <param name="mensaje">Mensaje del toast.</param>
        /// <param name="segundos">Duración en segundos.</param>
        void Mostrar(string titulo, string mensaje, int segundos = 5);

        /// <summary>
        /// Muestra un toast con el resumen de notificaciones.
        /// </summary>
        /// <param name="notificaciones">DataTable de notificaciones.</param>
        void MostrarResumen(DataTable notificaciones);
    }
}