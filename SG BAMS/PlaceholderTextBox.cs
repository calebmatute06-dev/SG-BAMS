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
        void Activar();
        string GetRealValue();
        bool IsPlaceholderActive { get; }
    }

    /// <summary>
    /// Adaptador que añade comportamiento de placeholder a un KryptonTextBox.
    /// </summary>
    public class PlaceholderTextBox : IPlaceholder
    {
        private readonly KryptonTextBox _txt;
        private readonly string _placeholder;
        private bool _isPlaceholderActive;
        private bool _isLoading;

        private static readonly Color ColorPlaceholder = Color.Gray;
        private static readonly Color ColorTextoNormal = Color.Navy;

        public PlaceholderTextBox(KryptonTextBox textBox, string placeholder)
        {
            _txt = textBox ?? throw new ArgumentNullException(nameof(textBox));
            _placeholder = placeholder ?? throw new ArgumentNullException(nameof(placeholder));

            // Foco/click SOLO reposicionan el cursor, no tocan el texto.
            _txt.Enter += PosicionarCursor;
            _txt.Click += PosicionarCursor;

            // El placeholder se borra únicamente cuando el usuario escribe de verdad,
            // sin importar si el control ya tenía el foco puesto de antes.
            _txt.KeyPress += AlEscribir;

            _txt.LostFocus += Salir;
            _txt.TextChanged += OnTextChanged;

            if (string.IsNullOrWhiteSpace(_txt.Text) || _txt.Text == _placeholder)
            {
                Activar();
            }
            else
            {
                ForzarColor(ColorTextoNormal);
                _isPlaceholderActive = false;
            }
        }

        public bool IsPlaceholderActive => _isPlaceholderActive;

        public void Activar()
        {
            _isLoading = true;
            _txt.Text = _placeholder;
            ForzarColor(ColorPlaceholder);
            _isPlaceholderActive = true;
            _isLoading = false;
        }

        public string GetRealValue()
        {
            return _isPlaceholderActive ? string.Empty : _txt.Text;
        }

        private void PosicionarCursor(object sender, EventArgs e)
        {
            if (_isPlaceholderActive)
            {
                _txt.SelectionStart = 0;
                _txt.SelectionLength = 0;
            }
        }

        private void AlEscribir(object sender, KeyPressEventArgs e)
        {
            if (_isPlaceholderActive && !char.IsControl(e.KeyChar))
            {
                _isLoading = true;
                _txt.Text = string.Empty;
                ForzarColor(ColorTextoNormal);
                _isPlaceholderActive = false;
                _isLoading = false;
                // El propio KeyPress inserta el carácter después de este handler.
            }
        }

        private void Salir(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_txt.Text))
            {
                Activar();
            }
        }

        // Cubre pegar texto (Ctrl+V) o drag&drop, que no disparan KeyPress.
        private void OnTextChanged(object sender, EventArgs e)
        {
            if (_isLoading) return;

            if (_isPlaceholderActive)
            {
                _isPlaceholderActive = false;
                ForzarColor(ColorTextoNormal);
                return;
            }

            if (string.IsNullOrWhiteSpace(_txt.Text))
            {
                Activar();
            }
            else
            {
                ForzarColor(ColorTextoNormal);
            }
        }

        private void ForzarColor(Color color)
        {
            _txt.StateCommon.Content.Color1 = color;
            _txt.StateNormal.Content.Color1 = color;
            _txt.StateActive.Content.Color1 = color;
        }
    }
}