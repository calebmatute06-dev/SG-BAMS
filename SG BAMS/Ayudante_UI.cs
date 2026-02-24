using System.Drawing;
using System.Windows.Forms;

namespace SG_BAMS
{
    public static class Ayudante_UI
    {
        public static void AplicarZoomGlobal(Form formulario)
        {
            float factor = Config_Sistema.FactorZoom;

            // Si es 100%, no procesamos nada para ahorrar recursos
            if (factor == 1.0f) return;

            // 1. Escalamos el formulario y sus controles de forma nativa
            formulario.Scale(new SizeF(factor, factor));

            // 2. Ajustamos las fuentes (Scale no siempre escala bien el texto)
            EscalarFuentesRecurrente(formulario, factor);
        }

        private static void EscalarFuentesRecurrente(Control contenedor, float factor)
        {
            foreach (Control c in contenedor.Controls)
            {
                // Ajustamos el tamaño de la fuente basándonos en el factor
                c.Font = new Font(c.Font.FontFamily, c.Font.SizeInPoints * factor, c.Font.Style);

                // Si el control tiene otros controles dentro (como un Panel o GroupBox)
                if (c.HasChildren)
                {
                    EscalarFuentesRecurrente(c, factor);
                }
            }
        }
    }
}
