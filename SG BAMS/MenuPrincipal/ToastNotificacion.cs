using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Implementación de IToastService como formulario toast.
    /// Muestra notificaciones temporales en pantalla.
    /// </summary>
    public class ToastNotificacion : Form, IToastService
    {
        private System.Windows.Forms.Timer timer;

        /// <summary>
        /// Constructor privado para crear un toast individual.
        /// </summary>
        private ToastNotificacion(string titulo, string mensaje, int segundos = 5)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.StartPosition = FormStartPosition.Manual;
            this.BackColor = Color.FromArgb(30, 30, 40);
            this.Size = new Size(400, 70);

            var area = Screen.PrimaryScreen.WorkingArea;
            this.Location = new Point((area.Width - this.Width) / 2, 40);

            var panelColor = new Panel
            {
                Size = new Size(5, this.Height),
                Location = new Point(0, 0),
                BackColor = Color.DodgerBlue
            };

            var lblTitulo = new Label
            {
                Text = titulo,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 8),
                AutoSize = true
            };

            var lblMensaje = new Label
            {
                Text = mensaje,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.LightGray,
                Location = new Point(20, 32),
                AutoSize = true,
                MaximumSize = new Size(360, 30)
            };

            this.Controls.Add(panelColor);
            this.Controls.Add(lblTitulo);
            this.Controls.Add(lblMensaje);

            timer = new System.Windows.Forms.Timer { Interval = segundos * 1000 };
            timer.Tick += (s, ev) => { timer.Stop(); this.Close(); };
            timer.Start();
        }

        /// <summary>
        /// Constructor público sin parámetros para inyección y compatibilidad.
        /// </summary>
        public ToastNotificacion()
        {
        }

        /// <inheritdoc/>
        public void Mostrar(string titulo, string mensaje, int segundos = 5)
        {
            var toast = new ToastNotificacion(titulo, mensaje, segundos);
            toast.Show();
        }

        /// <inheritdoc/>
        public void MostrarResumen(DataTable notificaciones)
        {
            if (notificaciones == null || notificaciones.Rows.Count == 0) return;

            string mensaje = ResumenNotificacionesBuilder.Construir(notificaciones);

            if (!string.IsNullOrEmpty(mensaje))
            {
                Mostrar("Notificaciones", mensaje, 8);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                timer?.Dispose();
            base.Dispose(disposing);
        }
    }
}