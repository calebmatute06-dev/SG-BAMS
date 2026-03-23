
using Krypton.Toolkit;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SG_BAMS
{
    public static class ClsValidaciones
    {

        public static bool CampoVacio(Control control, string nombreCampo)
        {
            if (string.IsNullOrWhiteSpace(control.Text))
            {
                MessageBox.Show($"El campo '{nombreCampo}' no puede estar vacío.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return true;
            }
            return false;
        }

        public static bool EsNumeroDecimalValido(Control control, string nombreCampo, out decimal valorResultado)
        {
            valorResultado = 0;
            string textoLimpio = control.Text.Trim().Replace(",", ".");

            bool esValido = decimal.TryParse(textoLimpio, NumberStyles.Any, CultureInfo.InvariantCulture, out valorResultado);

            if (!esValido || valorResultado <= 0)
            {
                MessageBox.Show($"{nombreCampo} debe ser un valor numérico válido y mayor a cero.", "Formato Incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }
            return true;
        }


        public static void ValidarDecimales(Control control, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != ','))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.' || e.KeyChar == ',') && (control.Text.Contains(".") || control.Text.Contains(",")))
            {
                e.Handled = true;
            }
        }

        public static void PermitirSoloLetras(KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public static void ValidarSoloNumeros(KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public static void ValidarBusquedaAlfanumerica(KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsWhiteSpace(e.KeyChar))
            {
                return;
            }

            if (!Regex.IsMatch(e.KeyChar.ToString(), @"^[a-zA-Z0-9ñÑáéíóúÁÉÍÓÚ]+$"))
            {
                e.Handled = true;
            }
        }


        public static bool EsNombrePersonalValido(Control control, string nombreCampo)
        {
            string texto = control.Text;

            if (string.IsNullOrWhiteSpace(texto))
            {
                MessageBox.Show($"El campo '{nombreCampo}' es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            if (Regex.IsMatch(texto, @"\s{2,}"))
            {
                MessageBox.Show($"El campo '{nombreCampo}' no puede contener dobles espacios.", "Validación de Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            if (texto.StartsWith(" "))
            {
                MessageBox.Show($"El campo '{nombreCampo}' no puede iniciar con un espacio.", "Validación de Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            if (!Regex.IsMatch(texto, @"^[a-zA-Z\sñÑáéíóúÁÉÍÓÚ]+$"))
            {
                MessageBox.Show($"{nombreCampo} solo debe contener letras.", "Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            return !ContieneCaracteresRepetidos(control, nombreCampo);
        }

        public static bool ContieneCaracteresRepetidos(Control control, string nombreCampo)
        {
            string texto = control.Text.Trim();
            if (Regex.IsMatch(texto, @"(.)\1{2,}"))
            {
                MessageBox.Show($"El campo '{nombreCampo}' contiene demasiados caracteres repetidos seguidos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return true;
            }
            return false;
        }

        public static bool EsTelefonoHondurasValido(Control control)
        {
            string tel = control.Text.Trim();
            if (tel.Length != 8 || !tel.All(char.IsDigit))
            {
                MessageBox.Show("El teléfono debe tener 8 dígitos numéricos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }
            return true;
        }

        public static bool EsPasswordValido(Control control, string nombreCampo, int minLength = 6)
        {
            string pass = control.Text;
            if (string.IsNullOrWhiteSpace(pass) || pass.Contains(" ") || pass.Length < minLength)
            {
                MessageBox.Show($"{nombreCampo} debe tener al menos {minLength} caracteres sin espacios.", "Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }
            return true;
        }

        //Producto
        public static bool ValidarNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("El nombre no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string nombreLimpio = nombre.Trim();

            if (!System.Text.RegularExpressions.Regex.IsMatch(nombreLimpio, @"^[a-zA-ZñÑ]{3}"))
            {
                MessageBox.Show("Los primeros tres caracteres del nombre deben ser letras (sin espacios ni números).",
                                "Formato de Inicio Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            bool formatoBasico = System.Text.RegularExpressions.Regex.IsMatch(nombreLimpio, @"^[a-zA-Z0-9\s&ñÑ@,.;:<>]+$");
            if (!formatoBasico)
            {
                MessageBox.Show("El nombre contiene caracteres no permitidos.",
                                "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (nombreLimpio.Length < 3 || nombreLimpio.Length > 40)
            {
                MessageBox.Show("El nombre del producto debe tener entre 3 y 40 caracteres.",
                                "Longitud Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (System.Text.RegularExpressions.Regex.IsMatch(nombreLimpio, @"([a-zA-ZñÑ])\1{2,}"))
            {
                MessageBox.Show("El nombre no permite que una letra se repita más de 2 veces consecutivamente.",
                                "Error de Escritura", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (nombreLimpio.Contains("  "))
            {
                MessageBox.Show("El nombre no puede contener dos o más espacios consecutivos.",
                                "Error de Espaciado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (System.Text.RegularExpressions.Regex.IsMatch(nombreLimpio, @"(^| )\w( |$)"))
            {
                MessageBox.Show("No se permiten letras aisladas. Cada palabra o sigla debe tener al menos 2 caracteres.",
                                "Formato Incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        public static bool ValidarPrecio(string precio)
        {
            if (string.IsNullOrWhiteSpace(precio) || !double.TryParse(precio, out double valor))
            {
                MessageBox.Show("El precio solo puede contener números y decimales válidos.",
                                "Error de Precio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (valor <= 0)
            {
                MessageBox.Show("El precio debe ser un valor mayor a cero.",
                                "Precio Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        public static bool ValidarSeleccion(KryptonComboBox cb, string nombreCampo)
        {
            if (cb.SelectedIndex == -1 || string.IsNullOrWhiteSpace(cb.Text))
            {
                MessageBox.Show("Debe seleccionar una opción en " + nombreCampo + ".",
                                "Campo Requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        public static bool ValidarCodigoBarra(string codigo)
        {
            bool esValido = Regex.IsMatch(codigo, @"^\d{13}$");

            if (!esValido)
            {
                MessageBox.Show("El código de barra debe contener exactamente 13 números. Por favor, verifíquelo.",
                                "Error de Código de Barras", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }
}