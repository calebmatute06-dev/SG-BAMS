using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Renderizador personalizado para el ListBox de notificaciones.
    /// Aplica colores de fondo y estilos de fuente según el tipo de notificación
    /// y su estado de lectura.
    /// </summary>
    public class NotificacionListBoxRenderer
    {
        private readonly HashSet<int> notificacionesLeidas;

        /// <summary>
        /// Constructor del renderizador de notificaciones.
        /// </summary>
        /// <param name="notificacionesLeidas">Conjunto de identificadores de notificaciones ya leídas.</param>
        public NotificacionListBoxRenderer(HashSet<int> notificacionesLeidas)
        {
            this.notificacionesLeidas = notificacionesLeidas ?? new HashSet<int>();
        }

        /// <summary>
        /// Maneja el evento DrawItem del ListBox para dibujar cada notificación
        /// con el color de fondo y estilo de fuente correspondiente.
        /// </summary>
        /// <param name="sender">Control ListBox que contiene las notificaciones.</param>
        /// <param name="e">Argumentos del evento DrawItem.</param>
        public void DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            var listBox = sender as ListBox;
            if (listBox == null) return;

            DataRowView fila = (DataRowView)listBox.Items[e.Index];
            string tituloTexto = fila["titulo"].ToString().ToLower();
            int idActual = Convert.ToInt32(fila["id_notificacion"]);

            Color colorFondo = ObtenerColorFondo(tituloTexto, idActual, e.State);

            using (SolidBrush pincelFondo = new SolidBrush(colorFondo))
            {
                e.Graphics.FillRectangle(pincelFondo, e.Bounds);
            }

            Color colorTexto = (e.State & DrawItemState.Selected) == DrawItemState.Selected
                ? Color.White
                : Color.Black;

            FontStyle estilo = notificacionesLeidas.Contains(idActual)
                ? FontStyle.Regular
                : FontStyle.Bold;

            using (Font fuentePersonalizada = new Font(e.Font, estilo))
            {
                TextRenderer.DrawText(
                    e.Graphics,
                    fila["titulo"].ToString(),
                    fuentePersonalizada,
                    e.Bounds,
                    colorTexto,
                    TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
            }

            e.DrawFocusRectangle();
        }

        /// <summary>
        /// Determina el color de fondo según el tipo de notificación y su estado de lectura.
        /// </summary>
        /// <param name="titulo">Título de la notificación.</param>
        /// <param name="idNotificacion">Identificador de la notificación.</param>
        /// <param name="state">Estado del dibujo del elemento.</param>
        /// <returns>Color de fondo correspondiente.</returns>
        private Color ObtenerColorFondo(string titulo, int idNotificacion, DrawItemState state)
        {
            if (titulo.Contains("acabado") || titulo.Contains("agotado"))
                return Color.FromArgb(255, 210, 210);

            if (titulo.Contains("critico") || titulo.Contains("bajo"))
                return Color.FromArgb(255, 255, 210);

            if (notificacionesLeidas.Contains(idNotificacion))
                return Color.FromArgb(245, 245, 245);

            if ((state & DrawItemState.Selected) == DrawItemState.Selected)
                return SystemColors.Highlight;

            return Color.White;
        }
    }
}