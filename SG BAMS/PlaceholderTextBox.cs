using Krypton.Toolkit;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SG_BAMS
{
    

    /// <summary>
    /// Contrato mínimo que debe cumplir cualquier control con comportamiento de placeholder.
    /// ISP: define solo los métodos que los consumidores realmente necesitan.
    /// </summary>
    public interface IPlaceholder
    {
        /// <summary>
        /// Activa el placeholder si el campo está vacío.
        /// </summary>
        void Activar();

        /// <summary>
        /// Retorna el valor real del campo, excluyendo el texto del placeholder.
        /// Retorna string.Empty si el placeholder está activo.
        /// </summary>
        string GetRealValue();

        /// <summary>
        /// Indica si el placeholder está activo (el campo está vacío).
        /// </summary>
        bool IsPlaceholderActive { get; }
    }

    /// <summary>
    /// Adaptador que añade comportamiento de placeholder a un KryptonTextBox.
    /// SRP: única responsabilidad — gestionar el estado de placeholder de un campo de texto.
    /// OCP: abierto para extensión mediante IPlaceholder; cerrado para modificación.
    /// DIP: los formularios deben referenciar IPlaceholder, no PlaceholderTextBox directamente.
    /// </summary>
    public class PlaceholderTextBox : IPlaceholder
    {
        private readonly KryptonTextBox _txt;
        private readonly string _placeholder;
        private bool _isPlaceholderActive;
        private bool _isLoading;

        private static readonly Color ColorPlaceholder = Color.Gray;
        private static readonly Color ColorTextoNormal = Color.Navy;

        /// <summary>
        /// Inicializa el adaptador vinculando el KryptonTextBox con su texto de placeholder.
        /// </summary>
        /// <param name="textBox">Control de texto al que se le aplica el placeholder.</param>
        /// <param name="placeholder">Texto orientativo que se muestra cuando el campo está vacío.</param>
        public PlaceholderTextBox(KryptonTextBox textBox, string placeholder)
        {
            _txt = textBox ?? throw new ArgumentNullException(nameof(textBox));
            _placeholder = placeholder ?? throw new ArgumentNullException(nameof(placeholder));

            _txt.GotFocus += Entrar;
            _txt.LostFocus += Salir;

            Activar();
        }

        /// <inheritdoc/>
        public bool IsPlaceholderActive => _isPlaceholderActive;

        /// <inheritdoc/>
        public void Activar()
        {
            if (!string.IsNullOrWhiteSpace(_txt.Text) && _txt.Text != _placeholder)
                return;

            _isLoading = true;
            _txt.Text = _placeholder;
            _txt.StateCommon.Content.Color1 = ColorPlaceholder;
            _isPlaceholderActive = true;
            _isLoading = false;
        }

        /// <inheritdoc/>
        public string GetRealValue()
        {
            return _isPlaceholderActive ? string.Empty : _txt.Text;
        }

        private void Entrar(object sender, EventArgs e)
        {
            if (_isPlaceholderActive)
            {
                _isLoading = true;
                _txt.Text = string.Empty;
                _txt.StateCommon.Content.Color1 = ColorTextoNormal;
                _isPlaceholderActive = false;
                _isLoading = false;
            }
        }

        private void Salir(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_txt.Text))
            {
                Activar();
            }
        }
    }
}