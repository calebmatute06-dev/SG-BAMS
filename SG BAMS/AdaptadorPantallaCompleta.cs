using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Convierte cualquier formulario "padre" (diseñado con tamaño fijo) en uno
    /// que se maximiza y reescala proporcionalmente todos sus controles hijos
    /// (incluidos los anidados en paneles) según el tamaño real de la pantalla.
    /// No requiere reorganizar el diseño a mano.
    /// </summary>
    public static class AdaptadorPantallaCompleta
    {
        private sealed class InfoOriginalControl
        {
            public Rectangle Bounds;
            public float TamanoFuente;
        }

        private static readonly Dictionary<Form, Size> _tamanoOriginalForm = new Dictionary<Form, Size>();
        private static readonly Dictionary<Control, InfoOriginalControl> _estadoOriginalControles =
            new Dictionary<Control, InfoOriginalControl>();

        /// <summary>
        /// Llamar UNA vez, justo después de InitializeComponent(), en el constructor
        /// (o en el Load) del formulario padre.
        /// </summary>
        public static void Habilitar(Form formulario)
        {
            if (formulario == null) throw new ArgumentNullException(nameof(formulario));

            // Reduce parpadeo y artefactos visuales ("fantasmas") al redimensionar rápido.
            typeof(Control).InvokeMember(
                "DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty
                    | System.Reflection.BindingFlags.Instance
                    | System.Reflection.BindingFlags.NonPublic,
                null, formulario, new object[] { true });

            _tamanoOriginalForm[formulario] = formulario.ClientSize;
            GuardarEstadoOriginal(formulario);

            formulario.FormBorderStyle = FormBorderStyle.Sizable;
            formulario.MaximizeBox = true;
            formulario.StartPosition = FormStartPosition.CenterScreen;
            formulario.WindowState = FormWindowState.Maximized;

            formulario.Resize += (s, e) => Reescalar(formulario);
            formulario.FormClosed += (s, e) => Limpiar(formulario);

            Reescalar(formulario);
        }

        private static void GuardarEstadoOriginal(Control contenedor)
        {
            foreach (Control control in contenedor.Controls)
            {
                if (!_estadoOriginalControles.ContainsKey(control))
                {
                    _estadoOriginalControles[control] = new InfoOriginalControl
                    {
                        Bounds = control.Bounds,
                        TamanoFuente = control.Font.Size
                    };
                }
                if (control.Controls.Count > 0) GuardarEstadoOriginal(control);
            }
        }

        private static void Reescalar(Form formulario)
        {
            if (!_tamanoOriginalForm.TryGetValue(formulario, out Size tamanoOriginal)) return;
            if (tamanoOriginal.Width <= 0 || tamanoOriginal.Height <= 0) return;
            if (formulario.ClientSize.Width <= 0 || formulario.ClientSize.Height <= 0) return;

            float escalaX = (float)formulario.ClientSize.Width / tamanoOriginal.Width;
            float escalaY = (float)formulario.ClientSize.Height / tamanoOriginal.Height;

            formulario.SuspendLayout();
            try
            {
                ReescalarControles(formulario, escalaX, escalaY);
            }
            finally
            {
                formulario.ResumeLayout(true);
                formulario.Invalidate(true); // fuerza a repintar TODO el formulario y sus hijos
                formulario.Update();         // aplica el repintado de inmediato, sin esperar
            }
        }

        private static void ReescalarControles(Control contenedor, float escalaX, float escalaY)
        {
            foreach (Control control in contenedor.Controls)
            {
                if (_estadoOriginalControles.TryGetValue(control, out InfoOriginalControl info))
                {
                    control.Bounds = new Rectangle(
                        (int)Math.Round(info.Bounds.X * escalaX),
                        (int)Math.Round(info.Bounds.Y * escalaY),
                        (int)Math.Round(info.Bounds.Width * escalaX),
                        (int)Math.Round(info.Bounds.Height * escalaY));

                    float escalaFuente = Math.Min(escalaX, escalaY);
                    float nuevoTamanoFuente = Math.Max(1f, info.TamanoFuente * escalaFuente);
                    if (Math.Abs(control.Font.Size - nuevoTamanoFuente) > 0.1f)
                        control.Font = new Font(control.Font.FontFamily, nuevoTamanoFuente, control.Font.Style);
                }
                if (control.Controls.Count > 0) ReescalarControles(control, escalaX, escalaY);
            }
        }

        private static void Limpiar(Form formulario)
        {
            _tamanoOriginalForm.Remove(formulario);
            QuitarEstadoOriginal(formulario);
        }

        private static void QuitarEstadoOriginal(Control contenedor)
        {
            foreach (Control control in contenedor.Controls)
            {
                _estadoOriginalControles.Remove(control);
                if (control.Controls.Count > 0) QuitarEstadoOriginal(control);
            }
        }
    }
}