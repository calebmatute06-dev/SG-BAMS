using Krypton.Toolkit;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System;

namespace SG_BAMS
{
    public static class ClsValidaciones
    {
        public static bool ValidarNombre(string nombre)
        {
            TextBox temp = new TextBox { Text = nombre };
            return EsNombrePersonalValido(temp, "Nombre");
        }

        public static bool ValidarPrecio(string precio)
        {
            TextBox temp = new TextBox { Text = precio };
            decimal salida;
            return EsNumeroDecimalValido(temp, "Precio", out salida);
        }

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

        public static void ValidarSoloNumeros(KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
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

        public static bool EsNombrePersonalValido(Control control, string nombreCampo, int minLength = 3, int maxLength = 50)
        {
            string texto = control.Text;

            if (string.IsNullOrWhiteSpace(texto))
            {
                MessageBox.Show($"El campo '{nombreCampo}' es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            string textoTrim = texto.Trim();

            if (textoTrim.Length < minLength || textoTrim.Length > maxLength)
            {
                MessageBox.Show($"{nombreCampo} debe tener entre {minLength} y {maxLength} caracteres.", "Longitud", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            if (Regex.IsMatch(texto, @"\s{2,}") || texto.StartsWith(" ") || texto.EndsWith(" "))
            {
                MessageBox.Show($"El campo '{nombreCampo}' tiene un espaciado incorrecto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            if (Regex.IsMatch(textoTrim, @"(?i)\b(?![yY]\b)[a-zñáéíóú]\b"))
            {
                MessageBox.Show($"No se permiten letras aisladas en {nombreCampo} (excepto la 'y').", "Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

           
            if (Regex.IsMatch(textoTrim, @"(.)\1{3,}") || Regex.IsMatch(textoTrim.Replace(" ", ""), @"(.{2,})\1{2,}"))
            {
                MessageBox.Show($"{nombreCampo} contiene caracteres o patrones repetitivos inválidos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            if (!Regex.IsMatch(textoTrim, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]+$"))
            {
                MessageBox.Show($"{nombreCampo} solo debe contener letras.", "Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            return true;
        }

        public static void PermitirAlfanumerico(KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public static bool EsAlfanumericoValido(Control control, string nombreCampo, int minLength = 3, int maxLength = 50)
        {
            string texto = control.Text;

            if (string.IsNullOrWhiteSpace(texto))
            {
                MessageBox.Show($"El campo '{nombreCampo}' es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            string textoTrim = texto.Trim();

            if (textoTrim.Length < minLength || textoTrim.Length > maxLength)
            {
                MessageBox.Show($"{nombreCampo} debe tener entre {minLength} y {maxLength} caracteres.", "Longitud", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            
            if (Regex.IsMatch(textoTrim, @"(.)\1{3,}"))
            {
                MessageBox.Show($"{nombreCampo} contiene demasiados caracteres repetidos seguidos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            if (Regex.IsMatch(textoTrim, @"(?i)\b(?![yY]\b)[a-zñáéíóú]\b"))
            {
                MessageBox.Show($"No se permiten letras aisladas en {nombreCampo}.", "Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            if (Regex.IsMatch(texto, @"\s{2,}") || texto.StartsWith(" ") || texto.EndsWith(" ") ||
                Regex.IsMatch(textoTrim.Replace(" ", ""), @"(.{2,})\1{2,}"))
            {
                MessageBox.Show($"{nombreCampo} tiene un formato o repetición inválida.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            if (!Regex.IsMatch(textoTrim, @"^[a-zA-Z0-9ñÑáéíóúÁÉÍÓÚ\s]+$"))
            {
                MessageBox.Show($"{nombreCampo} solo debe contener letras y números.", "Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            return true;
        }

        public static void ValidarBusquedaAlfanumerica(KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsWhiteSpace(e.KeyChar)) return;
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"^[a-zA-Z0-9ñÑáéíóúÁÉÍÓÚ]+$")) e.Handled = true;
        }

        public static bool EsTelefonoHondurasValido(Control control)
        {
            string tel = control.Text.Trim();

           
            if (!Regex.IsMatch(tel, @"^[23789]\d{7}$"))
            {
                MessageBox.Show("El teléfono debe tener 8 dígitos y comenzar con un prefijo válido de Honduras (2, 3, 7, 8 o 9).",
                                "Teléfono Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            
            if (Regex.IsMatch(tel, @"(.)\1{3,}"))
            {
                MessageBox.Show("El teléfono no puede tener más de 3 números repetidos seguidos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            
            if (Regex.IsMatch(pass, @"(.)\1{3,}"))
            {
                MessageBox.Show($"{nombreCampo} es muy débil (demasiados caracteres repetidos).", "Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            return true;
        }

        public static bool ValidarSeleccion(KryptonComboBox cb, string nombreCampo)
        {
            if (cb.SelectedIndex == -1 || string.IsNullOrWhiteSpace(cb.Text))
            {
                MessageBox.Show("Debe seleccionar una opción en " + nombreCampo + ".", "Campo Requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        public static bool ValidarCodigoBarra(string codigo)
        {
            if (!Regex.IsMatch(codigo, @"^\d{6,20}$"))
            {
                MessageBox.Show("El código de barras debe contener entre 6 y 20 dígitos. Por favor, verifíquelo.",
                                "Error de Código de Barras", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (Regex.IsMatch(codigo, @"(.)\1{4,}"))
            {
                MessageBox.Show("El código de barras tiene un patrón de repetición inválido.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
        public static void ValidarTelefonoKeyPress(KryptonTextBox txt, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            if (!char.IsControl(e.KeyChar))
            {
                int pos = txt.SelectionStart;
                if (pos >= 3)
                {
                    if (txt.Text[pos - 1] == e.KeyChar &&
                        txt.Text[pos - 2] == e.KeyChar &&
                        txt.Text[pos - 3] == e.KeyChar)
                    {
                        e.Handled = true;
                        return;
                    }
                }
            }

            if (!e.Handled && txt.SelectionStart == 0 && !char.IsControl(e.KeyChar))
            {
                char[] prefijosHonduras = { '2', '3', '7', '8', '9' };
                if (!prefijosHonduras.Contains(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        public static void PermitirNumerosYDecimales(object sender, KeyPressEventArgs e)
        {
           
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            string textoActual = "";
            if (sender is Control control) textoActual = control.Text;

           
            if (e.KeyChar == '.')
            {
                if (textoActual.Contains("."))
                {
                    e.Handled = true;
                }
            }

            
            if (e.KeyChar == ',')
            {
                
                if (textoActual.Contains("."))
                {
                    
                    e.Handled = true; 
                }
            }
        }

        public static void ValidarRangoFechas(DateTimePicker dtpInicio, DateTimePicker dtpFin)
        {
            
            dtpInicio.MaxDate = DateTime.Today;
            dtpFin.MaxDate = DateTime.Today;

           
            if (dtpInicio.Value.Date > dtpFin.Value.Date)
            {
                dtpInicio.Value = dtpFin.Value;
            }
        }

        public static bool EsRTNValido(Control control)
        {
            string rtn = control.Text.Trim();

            
            if (!Regex.IsMatch(rtn, @"^\d{14}$"))
            {
                MessageBox.Show("El RTN debe tener exactamente 14 dígitos numéricos.",
                                "RTN Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

           
            if (Regex.IsMatch(rtn, @"^(.)\1{13}$"))
            {
                MessageBox.Show("El RTN contiene un patrón de repetición inválido.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            return true;
        }
    }
}