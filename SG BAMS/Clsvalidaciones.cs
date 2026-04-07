using Krypton.Toolkit;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// 
    /// </summary>
    public static class ClsValidaciones
    {
        /// <summary>
        /// Validars the nombre.
        /// </summary>
        /// <param name="nombre">The nombre.</param>
        /// <returns></returns>
        public static bool ValidarNombre(string nombre)
        {
            TextBox temp = new TextBox { Text = nombre };
            return EsNombrePersonalValido(temp, "Nombre");
        }

        /// <summary>
        /// Validars the precio.
        /// </summary>
        /// <param name="precio">The precio.</param>
        /// <returns></returns>
        public static bool ValidarPrecio(string precio)
        {
            TextBox temp = new TextBox { Text = precio };
            decimal salida;
            return EsNumeroDecimalValido(temp, "Precio", out salida);
        }

        /// <summary>
        /// Campoes the vacio.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="nombreCampo">The nombre campo.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Eses the numero decimal valido.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="nombreCampo">The nombre campo.</param>
        /// <param name="valorResultado">The valor resultado.</param>
        /// <returns></returns>
        public static bool EsNumeroDecimalValido(Control control, string nombreCampo, out decimal valorResultado)
        {
            valorResultado = 0;
            string texto = control.Text.Trim();
            bool esValido = decimal.TryParse(texto, NumberStyles.Number | NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out valorResultado);

            if (!esValido)
            {
                esValido = decimal.TryParse(texto, NumberStyles.Currency, CultureInfo.CurrentCulture, out valorResultado);
            }

            if (!esValido || valorResultado <= 0)
            {
                MessageBox.Show($"{nombreCampo} debe ser un valor numérico válido y mayor a cero.", "Formato Incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Validars the decimales.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Validars the solo numeros.
        /// </summary>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        public static void ValidarSoloNumeros(KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Permitirs the solo letras.
        /// </summary>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        public static void PermitirSoloLetras(KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Eses the nombre personal valido.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="nombreCampo">The nombre campo.</param>
        /// <param name="minLength">The minimum length.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <returns></returns>
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

           
            if (Regex.IsMatch(textoTrim, @"(.)\1{2,}") || Regex.IsMatch(textoTrim.Replace(" ", ""), @"(.{2,})\1{2,}"))
            {
                MessageBox.Show($"{nombreCampo} contiene caracteres o patrones repetitivos inválidos (máximo 2 iguales seguidos).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            if (!Regex.IsMatch(textoTrim, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]+$"))
            {
                MessageBox.Show($"{nombreCampo} solo debe contener letras.", "Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            if (Regex.IsMatch(textoTrim, @"(.)\1{2,}") ||
                Regex.IsMatch(textoTrim, @"\b(\w+)\b\s+\1\b", RegexOptions.IgnoreCase))
            {
                MessageBox.Show($"{nombreCampo} contiene caracteres o palabras repetidas inválidas.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Permitirs the alfanumerico.
        /// </summary>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        public static void PermitirAlfanumerico(KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Eses the alfanumerico valido.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="nombreCampo">The nombre campo.</param>
        /// <param name="minLength">The minimum length.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <returns></returns>
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

            
            if (Regex.IsMatch(textoTrim, @"(.)\1{2,}"))
            {
                MessageBox.Show($"{nombreCampo} contiene demasiados caracteres repetidos seguidos (máximo 2).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            if (!Regex.IsMatch(textoTrim, @"^[a-zA-Z0-9ñÑáéíóúÁÉÍÓÚ\s&]+$"))
            {
                MessageBox.Show($"{nombreCampo} solo debe contener letras, números y '&'.", "Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            if (Regex.IsMatch(texto, @"(.)\1{2,}") || Regex.IsMatch(texto, @"\b(\w+)\b\s+\1\b", RegexOptions.IgnoreCase))
            {
                MessageBox.Show($"{nombreCampo} contiene caracteres o palabras repetidas inválidas.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Validars the busqueda alfanumerica.
        /// </summary>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        public static void ValidarBusquedaAlfanumerica(KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsWhiteSpace(e.KeyChar)) return;
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"^[a-zA-Z0-9ñÑáéíóúÁÉÍÓÚ]+$")) e.Handled = true;
        }

        /// <summary>
        /// Eses the telefono honduras valido.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns></returns>
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

            return true;
        }

        /// <summary>
        /// Eses the password valido.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="nombreCampo">The nombre campo.</param>
        /// <param name="minLength">The minimum length.</param>
        /// <returns></returns>
        public static bool EsPasswordValido(Control control, string nombreCampo, int minLength = 6)
        {
            string pass = control.Text;
            if (string.IsNullOrWhiteSpace(pass) || pass.Contains(" ") || pass.Length < minLength)
            {
                MessageBox.Show($"{nombreCampo} debe tener al menos {minLength} caracteres sin espacios.", "Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            
            if (Regex.IsMatch(pass, @"(.)\1{2,}"))
            {
                MessageBox.Show($"{nombreCampo} es muy débil (demasiados caracteres repetidos seguidos).", "Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validars the seleccion.
        /// </summary>
        /// <param name="cb">The cb.</param>
        /// <param name="nombreCampo">The nombre campo.</param>
        /// <returns></returns>
        public static bool ValidarSeleccion(KryptonComboBox cb, string nombreCampo)
        {
            if (cb.SelectedIndex == -1 || string.IsNullOrWhiteSpace(cb.Text))
            {
                MessageBox.Show("Debe seleccionar una opción en " + nombreCampo + ".", "Campo Requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Validars the codigo barra.
        /// </summary>
        /// <param name="codigo">The codigo.</param>
        /// <returns></returns>
        public static bool ValidarCodigoBarra(string codigo)
        {
            if(!Regex.IsMatch(codigo, @"^[a-zA-Z0-9]{6,20}$"))
    {
                MessageBox.Show("El código de barras debe ser alfanumérico y tener entre 6 y 20 caracteres.",
                                "Error de Código de Barras", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validars the telefono key press.
        /// </summary>
        /// <param name="txt">The text.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        public static void ValidarTelefonoKeyPress(KryptonTextBox txt, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                return;
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

        /// <summary>
        /// Permitirs the numeros y decimales.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        public static void PermitirNumerosYDecimales(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            if (sender is Control control)
            {
                string texto = control.Text;

                if ((e.KeyChar == '.' || e.KeyChar == ',') && string.IsNullOrEmpty(texto))
                {
                    e.Handled = true;
                    return;
                }

                if (e.KeyChar == '.')
                {
                    if (texto.Contains("."))
                    {
                        e.Handled = true;
                    }
                }

                if (e.KeyChar == ',')
                {
                    if (texto.Contains("."))
                    {
                        e.Handled = true;
                    }
                }
            }
        }

        /// <summary>
        /// Validars the rango fechas.
        /// </summary>
        /// <param name="dtpInicio">The DTP inicio.</param>
        /// <param name="dtpFin">The DTP fin.</param>
        public static void ValidarRangoFechas(DateTimePicker dtpInicio, DateTimePicker dtpFin)
        {
            dtpInicio.MaxDate = DateTime.Today;
            dtpFin.MaxDate = DateTime.Today;

            if (dtpInicio.Value.Date > dtpFin.Value.Date)
            {
                dtpInicio.Value = dtpFin.Value;
            }
        }

        /// <summary>
        /// Eses the RTN valido.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <returns></returns>
        public static bool EsRTNValido(Control control)
        {
            string rtn = control.Text.Trim();

            
            if (!Regex.IsMatch(rtn, @"^\d{14}$"))
            {
                MessageBox.Show("El RTN debe tener exactamente 14 dígitos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            
            int depto = int.Parse(rtn.Substring(0, 2));
            if (depto < 1 || depto > 18)
            {
                MessageBox.Show("El código de departamento es inválido.", "Ubicación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            
            int tipo = int.Parse(rtn.Substring(4, 1));
           
            if (tipo != 1 && tipo != 2 && tipo != 3 && tipo != 9)
            {
                MessageBox.Show("El formato del RTN (dígito de tipo) es incorrecto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

           
            int anioCompleto;
            string bloqueAnio = rtn.Substring(4, 4);

            if (tipo == 9) 
            {
                
                int correlativoEmpresa = int.Parse(rtn.Substring(4, 4));
                if (correlativoEmpresa < 1000) 
                {
                    MessageBox.Show("El código de registro de empresa es inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            else 
            {
                int prefix = (tipo == 1) ? 1800 : (tipo == 2) ? 1900 : 2000;
                anioCompleto = prefix + int.Parse(rtn.Substring(5, 3));

                if (anioCompleto > DateTime.Now.Year)
                {
                    MessageBox.Show("El año de nacimiento en el RTN es mayor al año actual.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }



        /// <summary>
        /// Permitirs the solo letras y numeros.
        /// </summary>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        public static void PermitirSoloLetrasYNumeros(KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true; 
            }
        }
    }
}