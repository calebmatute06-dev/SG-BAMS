using System;
using System.Drawing;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// 
    /// </summary>
    public static class Ayudante_UI
    {
        /// <summary>
        /// Aplica el zoom global al formulario.
        /// </summary>
        /// <param name="formulario">El formulario.</param>
        public static void AplicarZoomGlobal(Form formulario)
        {
            float factor = Config_Sistema.FactorZoom;
            if (factor <= 1.0f || formulario == null) return;

            formulario.AutoScroll = true;

            try
            {
                formulario.Scale(new SizeF(factor, factor));
                EscalarFuentesRecurrente(formulario, factor);

                Rectangle areaTrabajo = Screen.FromControl(formulario).WorkingArea;
                formulario.Left = Math.Max(0, (areaTrabajo.Width - formulario.Width) / 2);
                formulario.Top = Math.Max(0, (areaTrabajo.Height - formulario.Height) / 2);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error de zoom: " + ex.Message);
            }
        }

        /// <summary>
        /// Escala las fuentes de forma recursiva.
        /// </summary>
        /// <param name="contenedor">El contenedor.</param>
        /// <param name="factor">El factor de escala.</param>
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