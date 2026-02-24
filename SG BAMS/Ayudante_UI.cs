using System;
using System.Drawing;
using System.Windows.Forms;

namespace SG_BAMS
{
    public static class Ayudante_UI
    {
        public static void AplicarZoomGlobal(Form formulario)
        {
            float factor = Config_Sistema.FactorZoom;
            if (factor <= 1.0f || formulario == null) return;

            // ACTIVAR SCROLL: Esto permite que aparezcan barras si el contenido es muy grande
            formulario.AutoScroll = true;

            try
            {
                // 1. Aplicamos el escalado de controles y fuentes
                formulario.Scale(new SizeF(factor, factor));
                EscalarFuentesRecurrente(formulario, factor);

                // 2. Centrado básico
                // Nota: No limitamos el tamaño del Form con la pantalla para que el 
                // scroll pueda funcionar correctamente sobre el tamaño real escalado.
                Rectangle areaTrabajo = Screen.FromControl(formulario).WorkingArea;
                formulario.Left = Math.Max(0, (areaTrabajo.Width - formulario.Width) / 2);
                formulario.Top = Math.Max(0, (areaTrabajo.Height - formulario.Height) / 2);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error de zoom: " + ex.Message);
            }
        }

        private static void EscalarFuentesRecurrente(Control contenedor, float factor)
        {
            foreach (Control c in contenedor.Controls)
            {
                if (c?.Font != null)
                {
                    c.Font = new Font(c.Font.FontFamily, c.Font.SizeInPoints * factor, c.Font.Style);
                }
                if (c != null && c.HasChildren)
                {
                    EscalarFuentesRecurrente(c, factor);
                }
            }
        }
    }
}