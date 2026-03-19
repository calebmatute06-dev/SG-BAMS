
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Linq;

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
    }
}