using Krypton.Toolkit;
using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Clase centralizada de validaciones reutilizables para controles de formulario.
    /// Todas las consultas SQL usan Procedimientos Almacenados.
    /// </summary>
    public static class ClsValidaciones
    {
        /// <summary>
        /// Validar un nombre simple (envoltorio).
        /// </summary>
        public static bool ValidarNombre(string nombre)
        {
            TextBox temp = new TextBox { Text = nombre };
            return EsNombrePersonalValido(temp, "Nombre");
        }

        /// <summary>
        /// Validar un precio simple (envoltorio).
        /// </summary>
        public static bool ValidarPrecio(string precio)
        {
            TextBox temp = new TextBox { Text = precio };
            decimal salida;
            return EsNumeroDecimalValido(temp, "Precio", out salida);
        }

        /// <summary>
        /// Verifica si un campo de control está vacío y muestra advertencia.
        /// </summary>
        public static bool CampoVacio(Control control, string nombreCampo)
        {
            if (string.IsNullOrWhiteSpace(control.Text))
            {
                MessageBox.Show($"El campo '{nombreCampo}' no puede estar vacío.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Valida que el texto del control sea un número decimal mayor a cero.
        /// </summary>
        public static bool EsNumeroDecimalValido(Control control, string nombreCampo, out decimal valorResultado)
        {
            valorResultado = 0;
            string texto = control.Text.Trim();

            if (string.IsNullOrWhiteSpace(texto))
            {
                MessageBox.Show($"El campo '{nombreCampo}' no puede estar vacío.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            CultureInfo ci = CultureInfo.CurrentCulture;
            string decimalSep = ci.NumberFormat.NumberDecimalSeparator;
            string thousandSep = ci.NumberFormat.NumberGroupSeparator;

            if (!Regex.IsMatch(texto, @"^[\d\.,]+$"))
            {
                MessageBox.Show($"{nombreCampo} solo puede contener números, comas y puntos.",
                    "Formato Incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            int lastComa = texto.LastIndexOf(',');
            int lastDot = texto.LastIndexOf('.');
            char decimalChar;

            if (lastComa == -1 && lastDot == -1)
                decimalChar = decimalSep[0];
            else if (lastComa > lastDot)
                decimalChar = ',';
            else
                decimalChar = '.';

            int countDecimal = texto.Count(c => c == decimalChar);
            if (countDecimal > 1)
            {
                MessageBox.Show($"{nombreCampo} tiene múltiples separadores decimales.",
                    "Formato Incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            char thousandChar = (decimalChar == ',') ? '.' : ',';
            string normalized = texto.Replace(thousandChar.ToString(), "");

            if (decimalChar != decimalSep[0])
                normalized = normalized.Replace(decimalChar.ToString(), decimalSep);

            bool esValido = decimal.TryParse(normalized,
                NumberStyles.Number | NumberStyles.AllowDecimalPoint,
                ci, out valorResultado);

            if (!esValido || valorResultado <= 0)
            {
                MessageBox.Show($"{nombreCampo} debe ser un valor numérico válido y mayor a cero.",
                    "Formato Incorrecto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Permite solo dígitos y un separador decimal (punto o coma) en el KeyPress.
        /// </summary>
        public static void ValidarDecimales(Control control, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
                e.Handled = true;
        }

        /// <summary>
        /// Permite solo dígitos en el KeyPress.
        /// </summary>
        public static void ValidarSoloNumeros(KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        /// <summary>
        /// Permite solo letras y espacios en el KeyPress.
        /// </summary>
        public static void PermitirSoloLetras(KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }

        /// <summary>
        /// Valida que el texto del control sea un nombre personal (solo letras, sin caracteres raros,
        /// sin letras aisladas, sin repeticiones excesivas ni palabras duplicadas).
        /// </summary>
        public static bool EsNombrePersonalValido(Control control, string nombreCampo,
            int minLength = 3, int maxLength = 50)
        {
            string texto = control.Text;

            if (string.IsNullOrWhiteSpace(texto))
            {
                MessageBox.Show($"El campo '{nombreCampo}' es obligatorio.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            string textoTrim = texto.Trim();

            if (textoTrim.Length < minLength || textoTrim.Length > maxLength)
            {
                MessageBox.Show($"{nombreCampo} debe tener entre {minLength} y {maxLength} caracteres.",
                    "Longitud", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            if (Regex.IsMatch(texto, @"\s{2,}") || texto.StartsWith(" ") || texto.EndsWith(" "))
            {
                MessageBox.Show($"El campo '{nombreCampo}' tiene un espaciado incorrecto.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            if (Regex.IsMatch(textoTrim, @"(?i)\b(?![yY]\b)[a-zñáéíóú]\b"))
            {
                MessageBox.Show($"No se permiten letras aisladas en {nombreCampo} (excepto la 'y').",
                    "Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            if (Regex.IsMatch(textoTrim, @"(.)\1{2,}")
                || Regex.IsMatch(textoTrim.Replace(" ", ""), @"(.{2,})\1{2,}"))
            {
                MessageBox.Show(
                    $"{nombreCampo} contiene caracteres o patrones repetitivos inválidos (máximo 2 iguales seguidos).",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            if (!Regex.IsMatch(textoTrim, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]+$"))
            {
                MessageBox.Show($"{nombreCampo} solo debe contener letras.",
                    "Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            if (Regex.IsMatch(textoTrim, @"\b(\w+)\b\s+\1\b", RegexOptions.IgnoreCase))
            {
                MessageBox.Show($"{nombreCampo} contiene palabras repetidas inválidas.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Permite alfanumérico y espacio en el KeyPress.
        /// </summary>
        public static void PermitirAlfanumerico(KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }

        /// <summary>
        /// Valida que el texto del control sea alfanumérico válido (letras, números, '&', sin repeticiones).
        /// </summary>
        public static bool EsAlfanumericoValido(Control control, string nombreCampo,
            int minLength = 3, int maxLength = 50)
        {
            string texto = control.Text;

            if (string.IsNullOrWhiteSpace(texto))
            {
                MessageBox.Show($"El campo '{nombreCampo}' es obligatorio.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            string textoTrim = texto.Trim();

            if (textoTrim.Length < minLength || textoTrim.Length > maxLength)
            {
                MessageBox.Show($"{nombreCampo} debe tener entre {minLength} y {maxLength} caracteres.",
                    "Longitud", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            if (Regex.IsMatch(textoTrim, @"(.)\1{2,}"))
            {
                MessageBox.Show(
                    $"{nombreCampo} contiene demasiados caracteres repetidos seguidos (máximo 2).",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            if (Regex.IsMatch(textoTrim, @"(?i)\b(?![yY]\b)[a-zñáéíóú]\b"))
            {
                MessageBox.Show($"No se permiten letras aisladas en {nombreCampo}.",
                    "Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            if (Regex.IsMatch(texto, @"\s{2,}") || texto.StartsWith(" ") || texto.EndsWith(" ")
                || Regex.IsMatch(textoTrim.Replace(" ", ""), @"(.{2,})\1{2,}"))
            {
                MessageBox.Show($"{nombreCampo} tiene un formato o repetición inválida.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            if (!Regex.IsMatch(textoTrim, @"^[a-zA-Z0-9ñÑáéíóúÁÉÍÓÚ\s&]+$"))
            {
                MessageBox.Show($"{nombreCampo} solo debe contener letras, números y '&'.",
                    "Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            if (Regex.IsMatch(texto, @"\b(\w+)\b\s+\1\b", RegexOptions.IgnoreCase))
            {
                MessageBox.Show($"{nombreCampo} contiene palabras repetidas inválidas.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Permite solo caracteres alfanuméricos (sin espacios) en una búsqueda, en el KeyPress.
        /// </summary>
        public static void ValidarBusquedaAlfanumerica(KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsWhiteSpace(e.KeyChar)) return;
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"^[a-zA-Z0-9ñÑáéíóúÁÉÍÓÚüÜ&\s]+$"))
                e.Handled = true;
        }

        /// <summary>
        /// Valida que el teléfono tenga 8 dígitos y comience con prefijo válido de Honduras (2,3,7,8,9).
        /// </summary>
        public static bool EsTelefonoHondurasValido(Control control)
        {
            string tel = control.Text.Trim();

            if (!Regex.IsMatch(tel, @"^[23789]\d{7}$"))
            {
                MessageBox.Show(
                    "El teléfono debe tener 8 dígitos y comenzar con un prefijo válido de Honduras (2, 3, 7, 8 o 9).",
                    "Teléfono Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Valida que la contraseña no esté vacía, no tenga espacios, tenga longitud mínima
        /// y no contenga más de 2 caracteres repetidos consecutivos.
        /// </summary>
        public static bool EsPasswordValido(Control control, string nombreCampo, int minLength = 6)
        {
            string pass = control.Text;

            if (string.IsNullOrWhiteSpace(pass) || pass.Contains(" ") || pass.Length < minLength)
            {
                MessageBox.Show(
                    $"{nombreCampo} debe tener al menos {minLength} caracteres sin espacios.",
                    "Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            if (Regex.IsMatch(pass, @"[^\x20-\x7E]"))
            {
                MessageBox.Show($"{nombreCampo} solo puede contener letras, números y caracteres estándar.",
                    "Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            if (Regex.IsMatch(pass, @"(.)\1{2,}"))
            {
                MessageBox.Show(
                    $"{nombreCampo} es muy débil (demasiados caracteres repetidos seguidos).",
                    "Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Valida que un KryptonComboBox tenga una opción seleccionada.
        /// </summary>
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

        /// <summary>
        /// Valida que un código de barras sea alfanumérico y tenga entre 6 y 20 caracteres.
        /// </summary>
        public static bool ValidarCodigoBarra(string codigo)
        {
            if (!Regex.IsMatch(codigo, @"^[a-zA-Z0-9]{6,20}$"))
            {
                MessageBox.Show(
                    "El código de barras debe ser alfanumérico y tener entre 6 y 20 caracteres.",
                    "Error de Código de Barras", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        public static void ForzarCodigoBarraKeyPress(KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.KeyChar = char.ToUpper(e.KeyChar);
            }
            else if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Restringe el KeyPress de un campo de teléfono: solo dígitos y primer dígito con prefijo hondureño válido.
        /// </summary>
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
                    e.Handled = true;
            }
        }

        /// <summary>
        /// Permite dígitos y un solo separador decimal (punto o coma) en el KeyPress.
        /// </summary>
        public static void PermitirNumerosYDecimales(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ','
                && !char.IsControl(e.KeyChar))
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

                if (e.KeyChar == '.' && texto.Contains("."))
                    e.Handled = true;

                if (e.KeyChar == ',' && texto.Contains("."))
                    e.Handled = true;
            }
        }

        /// <summary>
        /// Ajusta el rango máximo de dos DateTimePicker y evita que la fecha de inicio
        /// sea posterior a la de fin.
        /// </summary>
        public static void ValidarRangoFechas(DateTimePicker dtpInicio, DateTimePicker dtpFin)
        {
            dtpInicio.MaxDate = DateTime.Today;
            dtpFin.MaxDate = DateTime.Today;

            if (dtpInicio.Value.Date > dtpFin.Value.Date)
                dtpInicio.Value = dtpFin.Value;
        }

        private static readonly int[] Municipios = { 0, 28, 12, 11, 9, 8, 28, 16, 19, 23, 16, 16, 28, 11, 4, 8, 12, 11, 6 };

        /// <summary>
        /// Valida el formato del RTN hondureño (14 dígitos, código de departamento, tipo y año).
        /// </summary>
        public static bool EsRTNValido(Control control)
        {
            string rtn = control.Text.Trim();

            if (!Regex.IsMatch(rtn, @"^\d{14}$"))
            {
                MessageBox.Show("El RTN debe tener exactamente 14 dígitos.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            int depto = int.Parse(rtn.Substring(0, 2));
            if (depto < 1 || depto > 18)
            {
                MessageBox.Show("El código de departamento es inválido.",
                    "Ubicación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            int municipio = int.Parse(rtn.Substring(2, 2));
            if (municipio < 1 || municipio > Municipios[depto])
            {
                MessageBox.Show("El código de municipio es inválido.",
                    "Municipio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            int tipo = int.Parse(rtn.Substring(4, 1));
            if (tipo != 1 && tipo != 2 && tipo != 3 && tipo != 9)
            {
                MessageBox.Show("El formato del RTN (dígito de tipo) es incorrecto.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            int anioActual = DateTime.Now.Year;

            if (tipo == 9)
            {
                int digitosAnio = int.Parse(rtn.Substring(5, 2));
                int anioActualCorto = anioActual % 100;

                int anioConstitucion = (digitosAnio > anioActualCorto) ? 1900 + digitosAnio : 2000 + digitosAnio;

                if (anioConstitucion < 1950)
                {
                    anioConstitucion += 100;
                }

                if (anioConstitucion > anioActual)
                {
                    MessageBox.Show($"El año de constitución de la empresa ({anioConstitucion}) no puede ser mayor al año actual.",
                        "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    control.Focus();
                    return false;
                }
            }
            else
            {
                int anioCompleto = int.Parse(rtn.Substring(4, 4));
                int edad = anioActual - anioCompleto;

                if (anioCompleto > anioActual)
                {
                    MessageBox.Show("El año de nacimiento en el RTN no puede ser mayor al año actual.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    control.Focus();
                    return false;
                }

                if (edad < 18)
                {
                    MessageBox.Show($"La persona debe ser mayor de edad para ser registrada (Edad calculada: {edad} años).",
                        "Validación de Edad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    control.Focus();
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Verifica que el RTN no esté duplicado usando PA.
        /// </summary>
        public static bool ValidarRTNUnico(Control control, string tablaOrigen, int idExcluir = 0)
        {
            string rtnBusqueda = control.Text.Trim();

            if (string.IsNullOrWhiteSpace(rtnBusqueda))
            {
                MessageBox.Show("El RTN no puede estar vacío.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            ClsConexion conexion = new ClsConexion();
            try
            {
                conexion.AbrirConexion();
                using (var cmd = new Microsoft.Data.SqlClient.SqlCommand("sp_ValidarRTNUnico", conexion.Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@rtn", rtnBusqueda);
                    cmd.Parameters.AddWithValue("@tablaOrigen", tablaOrigen.ToLower());
                    cmd.Parameters.AddWithValue("@idExcluir", idExcluir);
                    int conteo = Convert.ToInt32(cmd.ExecuteScalar());
                    if (conteo > 0)
                    {
                        MessageBox.Show($"El RTN '{rtnBusqueda}' ya se encuentra registrado.", "RTN Duplicado",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        control.Focus();
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al validar duplicidad: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        /// <summary>
        /// Permite solo letras, dígitos y espacios en el KeyPress.
        /// </summary>
        public static void PermitirSoloLetrasYNumeros(KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }

        /// <summary>
        /// Valida que el nombre de usuario no tenga espacios, solo alfanumérico,
        /// y cumpla con la longitud mínima/máxima.
        /// </summary>
        public static bool EsNombreUsuarioValido(Control control, string nombreCampo,
            int minLength = 3, int maxLength = 20)
        {
            string texto = control.Text;

            if (string.IsNullOrWhiteSpace(texto))
            {
                MessageBox.Show($"El campo '{nombreCampo}' es obligatorio.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            string textoTrim = texto.Trim();

            if (textoTrim.Length < minLength || textoTrim.Length > maxLength)
            {
                MessageBox.Show($"{nombreCampo} debe tener entre {minLength} y {maxLength} caracteres.",
                    "Longitud", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            if (Regex.IsMatch(textoTrim, @"\s{2,}"))
            {
                MessageBox.Show($"El campo '{nombreCampo}' no puede contener dobles espacios.",
                    "Formato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            if (!Regex.IsMatch(textoTrim, @"^[a-zA-Z0-9\s]+$"))
            {
                MessageBox.Show($"{nombreCampo} solo puede contener letras, números y espacios simples.",
                    "Formato inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            if (Regex.IsMatch(textoTrim, @"(.)\1{2,}"))
            {
                MessageBox.Show($"{nombreCampo} contiene demasiados caracteres repetidos seguidos.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Valida que un nombre no esté duplicado en una tabla específica usando PA.
        /// </summary>
        public static bool ValidarNombreUnico(Control control, string tabla, string columnaNombre,
            string nombreCampo, int idExcluir = 0, string idColumna = "id")
        {
            string nombre = control.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show($"El campo '{nombreCampo}' no puede estar vacío.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            ClsConexion conexion = new ClsConexion();
            try
            {
                conexion.AbrirConexion();
                using (var cmd = new Microsoft.Data.SqlClient.SqlCommand("sp_ValidarNombreUnico", conexion.Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@tabla", tabla);
                    cmd.Parameters.AddWithValue("@columnaNombre", columnaNombre);
                    cmd.Parameters.AddWithValue("@idExcluir", idExcluir);
                    cmd.Parameters.AddWithValue("@idColumna", idColumna);
                    int conteo = Convert.ToInt32(cmd.ExecuteScalar());
                    if (conteo > 0)
                    {
                        MessageBox.Show($"El {nombreCampo.ToLower()} '{nombre}' ya se encuentra registrado.",
                            "Registro Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        control.Focus();
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al validar nombre único: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        /// <summary>
        /// Valida que el texto del control sea un correo electrónico válido.
        /// </summary>
        public static bool ValidacionCorreo(Control control, string nombreCampo = "Correo electrónico")
        {
            string texto = control.Text;

            if (string.IsNullOrWhiteSpace(texto))
            {
                MessageBox.Show($"El campo '{nombreCampo}' es obligatorio.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            string correo = texto.Trim();

            if (correo.Contains(" "))
            {
                MessageBox.Show($"El campo '{nombreCampo}' no puede contener espacios.",
                    "Formato Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            if (!Regex.IsMatch(correo, @"^[a-zA-Z0-9]+([._][a-zA-Z0-9]+)*@[a-zA-Z0-9]+([.\-][a-zA-Z0-9]+)*\.[a-zA-Z]{2,}$"))
            {
                MessageBox.Show($"El formato de '{nombreCampo}' no es válido (ej: usuario@dominio.com).",
                    "Formato Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            if (correo.Count(c => c == '@') != 1)
            {
                MessageBox.Show($"'{nombreCampo}' debe contener exactamente un símbolo '@'.",
                    "Formato Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            string[] partes = correo.Split('@');
            string usuario = partes[0];
            string dominio = partes[1];

            if (usuario.Length < 6 || usuario.Length > 30)
            {
                MessageBox.Show($"El usuario del '{nombreCampo}' debe tener entre 6 y 30 caracteres.",
                    "Longitud", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            if (Regex.IsMatch(usuario, @"^[.\-_]|[.\-_]$"))
            {
                MessageBox.Show($"El usuario del '{nombreCampo}' no puede iniciar ni terminar con '.', '-' o '_'.",
                    "Formato Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            if (Regex.IsMatch(dominio, @"^[.\-]|[.\-]$"))
            {
                MessageBox.Show($"El dominio del '{nombreCampo}' no puede iniciar ni terminar con '.' o '-'.",
                    "Formato Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            string extension = dominio.Contains(".")
                ? dominio.Substring(dominio.LastIndexOf('.') + 1).ToLower()
                : "";

            if (extension.Length < 2)
            {
                MessageBox.Show($"La extensión del dominio en '{nombreCampo}' debe tener al menos 2 letras (ej: .com, .hn).",
                    "Dominio Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            string[] extensionesValidas =
            {
                "com", "net", "org", "edu", "gov", "mil",
                "io", "co", "app", "ai", "info", "biz",
                "hn", "mx", "gt", "sv", "ni", "cr", "pa",
                "ve", "ec", "pe", "cl", "ar", "br",
                "us", "ca", "es", "fr", "de", "uk", "eu"
            };

            if (!extensionesValidas.Contains(extension))
            {
                DialogResult respuesta = MessageBox.Show(
                    $"La extensión '.{extension}' no es común.\n\n¿Está seguro que el correo es correcto?",
                    "Extensión Inusual",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (respuesta == DialogResult.No)
                {
                    control.Focus();
                    return false;
                }
            }

            string[] partesDominio = dominio.Split('.');
            string nombreDominio = partesDominio[partesDominio.Length - 2].ToLower();

            string[] dominiosConocidos =
            {
                "gmail", "yahoo", "outlook", "hotmail", "icloud",
                "live", "msn", "aol", "protonmail", "zoho"
            };

            if (!dominiosConocidos.Contains(nombreDominio))
            {
                DialogResult respuesta = MessageBox.Show(
                    $"El proveedor '{nombreDominio}.{extension}' no es un servicio de correo reconocido.\n\n" +
                    "¿Está seguro que el correo es correcto?",
                    "Proveedor Inusual",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (respuesta == DialogResult.No)
                {
                    control.Focus();
                    return false;
                }
            }

            return true;
        }
    }
}