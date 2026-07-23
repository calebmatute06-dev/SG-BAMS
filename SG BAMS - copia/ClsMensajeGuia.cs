using System;
using System.Drawing;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace SG_BAMS
{
    internal class ClsMensajeGuia
    {
        // ===================================
        // TEXTBOX NORMAL
        // ===================================
        public static void Activar(TextBox txt)
        {
            string placeholder = txt.Text;
            Color colorPlaceholder = txt.ForeColor;

            bool esPlaceholder = true;

            txt.Text = placeholder;
            txt.ForeColor = colorPlaceholder;

            txt.Enter += (s, e) =>
            {
                if (esPlaceholder)
                {
                    txt.SelectionStart = 0;
                    txt.SelectionLength = 0;
                }
            };

            txt.KeyPress += (s, e) =>
            {
                if (esPlaceholder &&
                    !char.IsControl(e.KeyChar))
                {
                    txt.Text = "";
                    txt.ForeColor = Color.Black;
                    esPlaceholder = false;
                }
            };

            txt.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txt.Text))
                {
                    txt.Text = placeholder;
                    txt.ForeColor = colorPlaceholder;
                    esPlaceholder = true;
                }
            };

            txt.Click += (s, e) =>
            {
                if (esPlaceholder)
                {
                    txt.SelectionStart = 0;
                    txt.SelectionLength = 0;
                }
            };

            txt.TextChanged += (s, e) =>
            {
                if (!esPlaceholder &&
                    string.IsNullOrWhiteSpace(txt.Text))
                {
                    txt.Text = placeholder;
                    txt.ForeColor = colorPlaceholder;
                    esPlaceholder = true;

                    txt.SelectionStart = 0;
                    txt.SelectionLength = 0;
                }
            };
        }

        // ===================================
        // KRYPTON TEXTBOX
        // ===================================
        public static void ActivarK(KryptonTextBox txt)
        {
            Color colorPlaceholder = Color.Gray;
            Color colorEscritura = Color.Blue;

            // Color del mensaje guía
            txt.CueHint.Color1 = colorPlaceholder;

            // Si está vacío al iniciar
            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                txt.StateCommon.Content.Color1 =
                    colorPlaceholder;
            }

            txt.Enter += (s, e) =>
            {
                txt.SelectionStart = 0;
                txt.SelectionLength = 0;
            };

            txt.TextChanged += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txt.Text))
                {
                    txt.StateCommon.Content.Color1 =
                        colorPlaceholder;
                }
                else
                {
                    txt.StateCommon.Content.Color1 =
                        colorEscritura;
                }
            };

            txt.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txt.Text))
                {
                    txt.StateCommon.Content.Color1 =
                        colorPlaceholder;
                }
            };
        }
    }
}