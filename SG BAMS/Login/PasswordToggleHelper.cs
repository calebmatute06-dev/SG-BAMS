using System;
using System.Drawing;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Componente reutilizable para mostrar/ocultar contraseña con ícono de ojo.
    /// Soporta tanto TextBox estándar como KryptonTextBox.
    /// 
    /// PRINCIPIOS SOLID APLICADOS:
    /// - SRP: Única responsabilidad de gestionar la visibilidad de la contraseña.
    /// - OCP: Se puede reutilizar en cualquier formulario sin modificar.
    /// </summary>
    public class PasswordToggleHelper
    {
        private readonly Control _txtPassword;
        private readonly Label _lblOjo;
        private bool _passwordVisible;
        private bool _esPlaceholder;
        private const string TEXTO_PLACEHOLDER = "Ingrese su contraseña";

        /// <summary>
        /// Constructor que acepta cualquier Control (TextBox o KryptonTextBox).
        /// </summary>
        /// <param name="txtPassword">Control de contraseña (TextBox o KryptonTextBox).</param>
        /// <param name="contenedor">Control contenedor donde se agregará el label del ojo.</param>
        public PasswordToggleHelper(Control txtPassword, Control contenedor)
        {
            _txtPassword = txtPassword ?? throw new ArgumentNullException(nameof(txtPassword));

            _lblOjo = new Label
            {
                Text = "👁",
                Font = new Font("Arial", 13),
                AutoSize = false,
                Size = new Size(32, 32),
                TextAlign = ContentAlignment.MiddleCenter,
                Cursor = Cursors.Hand,
                BackColor = Color.Transparent,
                Location = new Point(
                    txtPassword.Right + 5,
                    txtPassword.Top + (txtPassword.Height - 32) / 2)
            };

            _lblOjo.Click += (s, ev) =>
            {
                if (_esPlaceholder) return;
                ToggleVisibilidad();
            };

            contenedor.Controls.Add(_lblOjo);
            _lblOjo.BringToFront();

            ConfigurarPlaceholder();
        }

        private void ConfigurarPlaceholder()
        {
            _esPlaceholder = true;
            SetText(TEXTO_PLACEHOLDER);
            SetForeColor(Color.Gray);
            SetUseSystemPasswordChar(false);

            _txtPassword.Enter += (s, ev) =>
            {
                if (_esPlaceholder)
                {
                    SetText("");
                    SetForeColor(Color.Black);
                    SetUseSystemPasswordChar(!_passwordVisible);
                    _esPlaceholder = false;
                }
            };

            _txtPassword.Leave += (s, ev) =>
            {
                if (string.IsNullOrWhiteSpace(GetText()))
                {
                    SetText(TEXTO_PLACEHOLDER);
                    SetForeColor(Color.Gray);
                    SetUseSystemPasswordChar(false);
                    _esPlaceholder = true;
                }
            };
        }

        private void ToggleVisibilidad()
        {
            _passwordVisible = !_passwordVisible;
            SetUseSystemPasswordChar(!_passwordVisible);
            _lblOjo.Text = _passwordVisible ? "🙈" : "👁";
        }

        public bool EsPlaceholder => _esPlaceholder;

        private string GetText() => _txtPassword.Text;
        private void SetText(string text) => _txtPassword.Text = text;
        private void SetForeColor(Color color) => _txtPassword.ForeColor = color;

        private void SetUseSystemPasswordChar(bool usePasswordChar)
        {
            if (_txtPassword is KryptonTextBox ktb)
                ktb.UseSystemPasswordChar = usePasswordChar;
            else if (_txtPassword is TextBox tb)
                tb.UseSystemPasswordChar = usePasswordChar;
        }
    }
}