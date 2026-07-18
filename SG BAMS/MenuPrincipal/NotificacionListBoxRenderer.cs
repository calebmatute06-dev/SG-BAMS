using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Renderizador personalizado para el ListBox de notificaciones.
    /// Extraído del formulario NotificacionesAdmin para cumplir SRP.
    /// </summary>
    public class NotificacionListBoxRenderer
    {
        private readonly HashSet<int> _notificacionesLeidas;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="notificacionesLeidas">Conjunto de IDs de notificaciones ya leídas.</param>
        public NotificacionListBoxRenderer(HashSet<int> notificacionesLeidas)
        {
            _notificacionesLeidas = notificacionesLeidas ?? new HashSet<int>();
        }

        /// <summary>
        /// Maneja el evento DrawItem del ListBox.
        /// Colorea el fondo según el tipo de notificación y el estado de lectura.
        /// </summary>
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

            FontStyle estilo = _notificacionesLeidas.Contains(idActual)
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
        /// Determina el color de fondo según el tipo de notificación y estado.
        /// </summary>
        private Color ObtenerColorFondo(string titulo, int idNotificacion, DrawItemState state)
        {
 
            if (titulo.Contains("acabado") || titulo.Contains("agotado"))
                return Color.FromArgb(255, 210, 210);

            if (titulo.Contains("critico") || titulo.Contains("bajo"))
                return Color.FromArgb(255, 255, 210); 

            if (_notificacionesLeidas.Contains(idNotificacion))
                return Color.FromArgb(245, 245, 245); 

            if ((state & DrawItemState.Selected) == DrawItemState.Selected)
                return SystemColors.Highlight;

            return Color.White; 
        }
    }
}