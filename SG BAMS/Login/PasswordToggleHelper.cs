using System;
using System.Drawing;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Componente reutilizable para mostrar y ocultar la contraseña mediante un ícono de ojo.
    /// Compatible con controles TextBox estándar y KryptonTextBox.
    /// </summary>
    public class PasswordToggleHelper
    {
        private readonly Control txtPassword;
        private readonly Label lblOjo;
        private bool passwordVisible;
        private bool esPlaceholder;
        private const string TextoPlaceholder = "Ingrese su contraseña";

        // Guardar los colores originales del KryptonTextBox
        private Color colorOriginalCommon;
        private Color colorOriginalActive;
        private Color colorOriginalNormal;

        /// <summary>
        /// Constructor del componente. Agrega un ícono de ojo junto al campo de contraseña
        /// que permite alternar la visibilidad del texto.
        /// </summary>
        /// <param name="txtPassword">Control de contraseña (TextBox o KryptonTextBox).</param>
        /// <param name="contenedor">Control contenedor donde se agregará el ícono del ojo.</param>
        /// <exception cref="ArgumentNullException">Si txtPassword es nulo.</exception>
        public PasswordToggleHelper(Control txtPassword, Control contenedor)
        {
            this.txtPassword = txtPassword ?? throw new ArgumentNullException(nameof(txtPassword));

            // Guardar los colores originales del tema Krypton
            if (txtPassword is KryptonTextBox ktb)
            {
                colorOriginalCommon = ktb.StateCommon.Content.Color1;
                colorOriginalActive = ktb.StateActive.Content.Color1;
                colorOriginalNormal = ktb.StateNormal.Content.Color1;
            }

            lblOjo = new Label
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

            lblOjo.Click += (s, ev) =>
            {
                if (esPlaceholder) return;
                ToggleVisibilidad();
            };

            contenedor.Controls.Add(lblOjo);
            lblOjo.BringToFront();

            ConfigurarPlaceholder();
        }

        /// <summary>
        /// Configura el comportamiento de placeholder en el campo de contraseña.
        /// Muestra un texto guía cuando el campo está vacío.
        /// </summary>
        private void ConfigurarPlaceholder()
        {
            esPlaceholder = true;
            SetText(TextoPlaceholder);
            SetUseSystemPasswordChar(false);

            // Aplicar color gris al placeholder
            AplicarColorPlaceholder();

            this.txtPassword.Enter += (s, ev) =>
            {
                if (esPlaceholder)
                {
                    SetText("");
                    SetUseSystemPasswordChar(!passwordVisible);
                    RestaurarColoresOriginales();
                    esPlaceholder = false;
                }
            };

            this.txtPassword.Leave += (s, ev) =>
            {
                if (string.IsNullOrWhiteSpace(GetText()))
                {
                    SetText(TextoPlaceholder);
                    SetUseSystemPasswordChar(false);
                    AplicarColorPlaceholder();
                    esPlaceholder = true;
                }
            };
        }

        /// <summary>
        /// Alterna la visibilidad de la contraseña entre texto plano y oculto.
        /// </summary>
        private void ToggleVisibilidad()
        {
            passwordVisible = !passwordVisible;
            SetUseSystemPasswordChar(!passwordVisible);
            lblOjo.Text = passwordVisible ? "🙈" : "👁";
        }

        /// <summary>
        /// Indica si el campo de contraseña está mostrando el texto guía (placeholder).
        /// </summary>
        public bool EsPlaceholder => esPlaceholder;

        /// <summary>
        /// Obtiene el texto actual del campo de contraseña.
        /// </summary>
        private string GetText() => txtPassword.Text;

        /// <summary>
        /// Establece el texto del campo de contraseña.
        /// </summary>
        private void SetText(string text) => txtPassword.Text = text;

        /// <summary>
        /// Configura si el campo debe mostrar la contraseña como caracteres ocultos.
        /// Soporta tanto TextBox estándar como KryptonTextBox.
        /// </summary>
        private void SetUseSystemPasswordChar(bool usePasswordChar)
        {
            if (txtPassword is KryptonTextBox ktb)
                ktb.UseSystemPasswordChar = usePasswordChar;
            else if (txtPassword is TextBox tb)
                tb.UseSystemPasswordChar = usePasswordChar;
        }

        /// <summary>
        /// Aplica color gris al texto (modo placeholder).
        /// </summary>
        private void AplicarColorPlaceholder()
        {
            if (txtPassword is KryptonTextBox ktb)
            {
                ktb.StateCommon.Content.Color1 = Color.Gray;
                ktb.StateActive.Content.Color1 = Color.Gray;
                ktb.StateNormal.Content.Color1 = Color.Gray;
            }
            else
            {
                txtPassword.ForeColor = Color.Gray;
            }
        }

        /// <summary>
        /// Restaura los colores originales del tema Krypton.
        /// </summary>
        private void RestaurarColoresOriginales()
        {
            if (txtPassword is KryptonTextBox ktb)
            {
                ktb.StateCommon.Content.Color1 = colorOriginalCommon;
                ktb.StateActive.Content.Color1 = colorOriginalActive;
                ktb.StateNormal.Content.Color1 = colorOriginalNormal;
            }
            else
            {
                txtPassword.ForeColor = Color.Black;
            }
        }
    }
}