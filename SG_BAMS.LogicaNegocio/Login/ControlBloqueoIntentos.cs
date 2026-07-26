using System;
using System.Windows.Forms;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Controla la lógica de bloqueo por intentos fallidos de inicio de sesión.
    /// Extraído del formulario Login para cumplir SRP.
    /// </summary>
    public class ControlBloqueoIntentos
    {
        private int intentosFallidos;
        private readonly int maxIntentos;
        private readonly int segundosBloqueo;
        private int segundosRestantes;
        private readonly System.Windows.Forms.Timer timerBloqueo;
        private readonly Control[] controlesABloquear;
        private readonly Label lblBloqueo;

        /// <summary>Evento que se dispara cuando el bloqueo finaliza.</summary>
        public event EventHandler BloqueoFinalizado;

        /// <summary>Evento para actualizar el texto del bloqueo en la UI.</summary>
        public event Action<string> EstadoBloqueoCambiado;

        /// <summary>
        /// Constructor que inicializa el control de bloqueo.
        /// </summary>
        /// <param name="maxIntentos">Número máximo de intentos fallidos antes de bloquear.</param>
        /// <param name="segundosBloqueo">Duración del bloqueo en segundos.</param>
        /// <param name="lblBloqueo">Label donde se muestra el estado del bloqueo.</param>
        /// <param name="controlesABloquear">Controles que se deshabilitarán durante el bloqueo.</param>
        public ControlBloqueoIntentos(int maxIntentos, int segundosBloqueo, Label lblBloqueo, params Control[] controlesABloquear)
        {
            this.maxIntentos = maxIntentos;
            this.segundosBloqueo = segundosBloqueo;
            this.lblBloqueo = lblBloqueo;
            this.controlesABloquear = controlesABloquear;

            timerBloqueo = new System.Windows.Forms.Timer { Interval = 1000 };
            timerBloqueo.Tick += TimerBloqueoTick;
        }

        /// <summary>
        /// Registra un intento fallido de inicio de sesión.
        /// Si se alcanza el máximo de intentos, activa el bloqueo.
        /// </summary>
        /// <returns>True si se activó el bloqueo, false en caso contrario.</returns>
        public bool RegistrarIntentoFallido()
        {
            intentosFallidos++;

            if (intentosFallidos >= maxIntentos)
            {
                ActivarBloqueo();
                return true;
            }

            return false;
        }

        /// <summary>Obtiene el número de intentos restantes antes del bloqueo.</summary>
        public int IntentosRestantes => maxIntentos - intentosFallidos;

        /// <summary>Reinicia el contador de intentos fallidos a cero.</summary>
        public void ReiniciarIntentos()
        {
            intentosFallidos = 0;
        }

        /// <summary>Indica si el bloqueo está actualmente activo.</summary>
        public bool EstaBloqueado => timerBloqueo.Enabled;

        /// <summary>
        /// Activa el bloqueo: deshabilita los controles, muestra el label
        /// de bloqueo e inicia el temporizador.
        /// </summary>
        private void ActivarBloqueo()
        {
            segundosRestantes = segundosBloqueo;

            foreach (var control in controlesABloquear)
                control.Enabled = false;

            lblBloqueo.Visible = true;
            ActualizarTextoBloqueo();
            timerBloqueo.Start();
        }

        /// <summary>
        /// Evento Tick del temporizador. Descuenta un segundo y verifica
        /// si el bloqueo debe finalizar.
        /// </summary>
        private void TimerBloqueoTick(object sender, EventArgs e)
        {
            segundosRestantes--;
            ActualizarTextoBloqueo();

            if (segundosRestantes <= 0)
            {
                timerBloqueo.Stop();
                DesactivarBloqueo();
            }
        }

        /// <summary>
        /// Desactiva el bloqueo: restaura los controles, oculta el label
        /// y reinicia el contador de intentos.
        /// </summary>
        private void DesactivarBloqueo()
        {
            foreach (var control in controlesABloquear)
                control.Enabled = true;

            lblBloqueo.Visible = false;
            intentosFallidos = 0;

            BloqueoFinalizado?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Actualiza el texto del label de bloqueo con los segundos restantes.
        /// </summary>
        private void ActualizarTextoBloqueo()
        {
            string texto = $"⛔ Cuenta bloqueada. Espere {segundosRestantes} segundos...";
            lblBloqueo.Text = texto;
            EstadoBloqueoCambiado?.Invoke(texto);
        }
    }
}