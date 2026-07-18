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
        private int _intentosFallidos;
        private readonly int _maxIntentos;
        private readonly int _segundosBloqueo;
        private int _segundosRestantes;
        private readonly System.Windows.Forms.Timer _timerBloqueo;
        private readonly Control[] _controlesABloquear;
        private readonly Label _lblBloqueo;

        /// <summary>Evento que se dispara cuando el bloqueo finaliza.</summary>
        public event EventHandler BloqueoFinalizado;

        /// <summary>Evento para actualizar el texto del bloqueo en la UI.</summary>
        public event Action<string> EstadoBloqueoCambiado;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="maxIntentos">Número máximo de intentos antes de bloquear.</param>
        /// <param name="segundosBloqueo">Duración del bloqueo en segundos.</param>
        /// <param name="lblBloqueo">Label para mostrar el estado del bloqueo.</param>
        /// <param name="controlesABloquear">Controles a deshabilitar durante el bloqueo.</param>
        public ControlBloqueoIntentos(int maxIntentos, int segundosBloqueo, Label lblBloqueo, params Control[] controlesABloquear)
        {
            _maxIntentos = maxIntentos;
            _segundosBloqueo = segundosBloqueo;
            _lblBloqueo = lblBloqueo;
            _controlesABloquear = controlesABloquear;

            _timerBloqueo = new System.Windows.Forms.Timer { Interval = 1000 };
            _timerBloqueo.Tick += TimerBloqueo_Tick;
        }

        /// <summary>Registra un intento fallido. Retorna true si se activó el bloqueo.</summary>
        public bool RegistrarIntentoFallido()
        {
            _intentosFallidos++;
            int intentosRestantes = _maxIntentos - _intentosFallidos;

            if (_intentosFallidos >= _maxIntentos)
            {
                ActivarBloqueo();
                return true;
            }

            return false;
        }

        /// <summary>Obtiene los intentos restantes antes del bloqueo.</summary>
        public int IntentosRestantes => _maxIntentos - _intentosFallidos;

        /// <summary>Reinicia el contador de intentos fallidos.</summary>
        public void ReiniciarIntentos()
        {
            _intentosFallidos = 0;
        }

        /// <summary>Indica si el bloqueo está activo.</summary>
        public bool EstaBloqueado => _timerBloqueo.Enabled;

        private void ActivarBloqueo()
        {
            _segundosRestantes = _segundosBloqueo;

            foreach (var control in _controlesABloquear)
                control.Enabled = false;

            _lblBloqueo.Visible = true;
            ActualizarTextoBloqueo();
            _timerBloqueo.Start();
        }

        private void TimerBloqueo_Tick(object sender, EventArgs e)
        {
            _segundosRestantes--;
            ActualizarTextoBloqueo();

            if (_segundosRestantes <= 0)
            {
                _timerBloqueo.Stop();
                DesactivarBloqueo();
            }
        }

        private void DesactivarBloqueo()
        {
            foreach (var control in _controlesABloquear)
                control.Enabled = true;

            _lblBloqueo.Visible = false;
            _intentosFallidos = 0;

            BloqueoFinalizado?.Invoke(this, EventArgs.Empty);
        }

        private void ActualizarTextoBloqueo()
        {
            string texto = $"⛔ Cuenta bloqueada. Espere {_segundosRestantes} segundos...";
            _lblBloqueo.Text = texto;
            EstadoBloqueoCambiado?.Invoke(texto);
        }
    }
}