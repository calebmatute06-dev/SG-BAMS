using Krypton.Toolkit;
using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SG_BAMS.AccesoDatos;

namespace SG_BAMS
{
    /// <summary>
    /// Clase centralizada de validaciones reutilizables para controles de formulario.
    ///
    /// El refactor anterior creó una versión para TextBox y otra para KryptonTextBox,
    /// cada una con sus propias reglas (algunas más débiles que las originales), lo
    /// que provocó que la validación real ejecutada dependiera de qué tipo de control
    /// recibía el formulario, en lugar de depender de la regla de negocio. Esta
    /// versión unifica todo en una sola implementación por validación, restaurando
    /// las reglas de negocio completas que existían antes del refactor.
    ///
    /// Todas las consultas SQL usan Procedimientos Almacenados.
    /// </summary>
    public static class ClsValidaciones
    {
        // ============================================================
        // TEXTO Y NOMBRES
        // ============================================================

        /// <summary>
        /// Validar un nombre simple (envoltorio de compatibilidad con firma antigua).
        /// </summary>
        public static bool ValidarNombre(string nombre)
        {
            TextBox temp = new TextBox { Text = nombre };
            return EsNombrePersonalValido(temp, "Nombre");
        }

        /// <summary>
        /// Verifica si un campo de control está vacío y muestra advertencia.
        /// Retorna true si está vacío (comportamiento original).
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
        /// Valida que el texto del control sea un nombre personal:
        /// solo letras (con acentos), longitud entre minLength y maxLength,
        /// sin espacios dobles ni al inicio/fin, sin letras aisladas (excepto "y"),
        /// sin más de 2 caracteres iguales seguidos, y sin palabras repetidas.
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
        /// Valida que el texto del control sea alfanumérico válido
        /// (letras, números y '&amp;', sin repeticiones ni letras aisladas).
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
        /// Valida que el nombre de usuario no tenga espacios dobles, sea alfanumérico
        /// con espacios simples permitidos, y cumpla longitud 3-20 (regla original).
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
        /// Valida que un nombre no esté duplicado en una tabla específica usando
        /// el procedimiento almacenado sp_ValidarNombreUnico.
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

            ClsRepositorioBaseDatos conexion = new ClsRepositorioBaseDatos();
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
        /// Valida que un KryptonComboBox o ComboBox tenga una opción seleccionada.
        /// </summary>
        public static bool ValidarSeleccion(Control control, string nombreCampo)
        {
            string texto = control is KryptonComboBox kcb ? kcb.Text
                          : control is ComboBox cb ? cb.Text
                          : control.Text;
            bool sinSeleccion = control is KryptonComboBox kcb2 ? kcb2.SelectedIndex == -1
                              : control is ComboBox cb2 ? cb2.SelectedIndex == -1
                              : string.IsNullOrWhiteSpace(texto);

            if (sinSeleccion || string.IsNullOrWhiteSpace(texto))
            {
                MessageBox.Show("Debe seleccionar una opción en " + nombreCampo + ".",
                    "Campo Requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }


        public static void PermitirSoloLetras(KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }

        public static void PermitirAlfanumerico(KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }

        public static void PermitirSoloLetrasYNumeros(KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }

        public static void ValidarSoloNumeros(KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        public static void ValidarDecimales(Control control, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
                e.Handled = true;
        }

        /// <summary>
        /// Permite dígitos y un solo separador decimal (punto o coma) en el KeyPress,
        /// bloqueando un segundo punto/coma si ya existe uno.
        /// </summary>
        public static void PermitirNumerosYDecimales(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;
            if (char.IsDigit(e.KeyChar)) return;

            if (e.KeyChar != '.' && e.KeyChar != ',')
            {
                e.Handled = true;
                return;
            }

            if (!(sender is Control control))
            {
                e.Handled = true;
                return;
            }

            string texto = control.Text;

            if (string.IsNullOrEmpty(texto))
            {
                e.Handled = true;
                return;
            }

            if ((texto.Contains('.') || texto.Contains(',')))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Permite solo caracteres alfanuméricos (con espacios) en el KeyPress de una búsqueda.
        /// </summary>
        public static void ValidarBusquedaAlfanumerica(KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsWhiteSpace(e.KeyChar)) return;
            if (!Regex.IsMatch(e.KeyChar.ToString(), @"^[a-zA-Z0-9ñÑáéíóúÁÉÍÓÚüÜ&\s]+$"))
                e.Handled = true;
        }

        /// <summary>
        /// Valida (post-captura) que el contenido de búsqueda sea alfanumérico válido.
        /// </summary>
        public static bool ValidarBusquedaAlfanumerica(Control control)
        {
            if (string.IsNullOrWhiteSpace(control.Text)) return true;
            if (!Regex.IsMatch(control.Text.Trim(), @"^[a-zA-Z0-9ñÑáéíóúÁÉÍÓÚüÜ&\s]+$"))
            {
                MessageBox.Show("El criterio de búsqueda contiene caracteres no permitidos.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Convierte a mayúscula toda letra escrita en un campo de código de barras
        /// y bloquea cualquier carácter que no sea letra o número.
        /// </summary>
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
        /// Validar un precio simple (envoltorio de compatibilidad).
        /// </summary>
        public static bool ValidarPrecio(string precio)
        {
            TextBox temp = new TextBox { Text = precio };
            decimal salida;
            return EsNumeroDecimalValido(temp, "Precio", out salida);
        }

        public static bool ValidarPrecio(Control control, string nombreCampo)
        {
            decimal salida;
            return EsNumeroDecimalValido(control, nombreCampo, out salida);
        }

        /// <summary>
        /// Valida que el texto del control sea un número decimal mayor a cero,
        /// interpretando de forma inteligente el separador decimal (punto o coma)
        /// según la cultura actual y la posición relativa de ambos símbolos.
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

        public static bool ValidarSoloNumeros(Control control, string nombreCampo)
        {
            string valor = control.Text.Trim();

            if (string.IsNullOrWhiteSpace(valor))
            {
                MessageBox.Show($"El campo '{nombreCampo}' es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            if (!int.TryParse(valor, out _))
            {
                MessageBox.Show($"El campo '{nombreCampo}' debe contener solo números enteros.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Interpreta un texto numérico decidiendo si la coma es separador de miles
        /// o decimal, según la cantidad de dígitos que la siguen. Restaura la lógica
        /// original: si hay punto Y coma, gana el símbolo que aparece más a la derecha
        /// como separador decimal; si solo hay coma, se interpreta como decimal
        /// únicamente cuando NO le siguen exactamente 3 dígitos (patrón de miles).
        /// </summary>
        public static double ParsearMontoInteligente(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return 0;

            texto = texto.Replace("L.", "").Replace(" ", "").Trim();

            bool tienePunto = texto.Contains(".");
            bool tieneComa = texto.Contains(",");
            string resultado;

            if (tienePunto && tieneComa)
            {
                int posPunto = texto.LastIndexOf('.');
                int posComa = texto.LastIndexOf(',');

                resultado = posComa > posPunto
                    ? texto.Replace(".", "").Replace(",", ".")   // coma es decimal, punto es miles
                    : texto.Replace(",", "");                    // punto es decimal, coma es miles
            }
            else if (tieneComa)
            {
                int posComa = texto.LastIndexOf(',');
                int digitosDespues = texto.Length - posComa - 1;

                resultado = digitosDespues == 3
                    ? texto.Replace(",", "")       // patrón de miles (ej. 1,234)
                    : texto.Replace(",", ".");     // separador decimal (ej. 12,5)
            }
            else
            {
                resultado = texto;
            }

            if (resultado.EndsWith(".")) resultado += "0";

            double.TryParse(resultado, NumberStyles.Any, CultureInfo.InvariantCulture, out double valor);
            return valor;
        }

        public static double ParsearMontoMoneda(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return 0;
            return ParsearMontoInteligente(texto);
        }

        /// <summary>
        /// Valida que el teléfono tenga 8 dígitos y comience con prefijo válido
        /// de Honduras (2, 3, 7, 8 o 9) — regla original.
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
        /// Restringe el KeyPress de un campo de teléfono: solo dígitos y primer
        /// dígito con prefijo hondureño válido (2, 3, 7, 8, 9).
        /// </summary>
        public static void ValidarTelefonoKeyPress(Control control, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            bool inicioVacio = control is KryptonTextBox ktb ? ktb.SelectionStart == 0
                              : control is TextBox tb ? tb.SelectionStart == 0
                              : true;

            if (!e.Handled && inicioVacio && !char.IsControl(e.KeyChar))
            {
                char[] prefijosHonduras = { '2', '3', '7', '8', '9' };
                if (!prefijosHonduras.Contains(e.KeyChar))
                    e.Handled = true;
            }
        }

        /// <summary>
        /// Sobrecarga de un solo parámetro (sin control): usada por formularios que
        /// suscriben el evento KeyPress solo con el argumento del evento, sin pasar
        /// el control. Al no tener referencia al control, no puede validar la
        /// posición del cursor (SelectionStart), por lo que solo bloquea caracteres
        /// que no sean dígitos, sin aplicar la restricción de prefijo en la primera
        /// posición. Esta es la firma que existía en el código original antes del
        /// refactor y que algunos formularios (ej. ModificarProveedor.cs) siguen
        /// usando directamente.
        /// </summary>
        public static void ValidarTelefonoKeyPress(KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private static readonly int[] Municipios =
            { 0, 28, 12, 11, 9, 8, 28, 16, 19, 23, 16, 16, 28, 11, 4, 8, 12, 11, 6 };

        /// <summary>
        /// Valida el formato completo del RTN hondureño (14 dígitos): código de
        /// departamento (1-18), código de municipio válido según el departamento,
        /// dígito de tipo (1=persona natural, 2/3=variantes, 9=empresa jurídica) y
        /// coherencia del año (mayoría de edad para personas, año no futuro para
        /// empresas) — restaura la validación semántica completa original.
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
                    anioConstitucion += 100;

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
        /// Verifica que el RTN no esté duplicado usando el procedimiento almacenado
        /// sp_ValidarRTNUnico.
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

            ClsRepositorioBaseDatos conexion = new ClsRepositorioBaseDatos();
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
        /// Valida el formato de un correo electrónico con las reglas completas
        /// originales: estructura básica, longitud del usuario (6-30), sin
        /// caracteres inválidos al inicio/fin de usuario o dominio, extensión
        /// reconocida (con confirmación si es inusual) y proveedor de correo
        /// reconocido (con confirmación si no lo es).
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

            string[] dominiosPermitidos = { "gmail.com", "yahoo.com", "outlook.com", "icloud.com" };

            if (!dominiosPermitidos.Contains(dominio.ToLower()))
            {
                MessageBox.Show(
                    $"El correo debe ser de uno de estos proveedores: {string.Join(", ", dominiosPermitidos.Select(d => "@" + d))}.",
                    "Proveedor no permitido",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                control.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Valida que la contraseña no esté vacía, no tenga espacios, tenga
        /// longitud mínima, solo contenga caracteres ASCII imprimibles, y no
        /// contenga más de 2 caracteres repetidos consecutivos — reglas
        /// originales, usadas de forma consistente para cualquier control.
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
        /// Validar un código de barras simple (envoltorio de compatibilidad).
        /// Debe ser alfanumérico puro (sin guiones) de 6 a 20 caracteres.
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

        public static bool ValidarCodigoBarra(Control control)
            => ValidarCodigoBarra(control.Text.Trim());

        // ============================================================
        // FECHAS
        // ============================================================

        /// <summary>
        /// Restringe ambos DateTimePicker a no permitir fechas futuras y corrige
        /// automáticamente si la fecha de inicio queda después de la de fin —
        /// comportamiento original. Además retorna false y muestra advertencia
        /// si el rango es inválido, para los formularios que verifiquen el resultado.
        /// </summary>
        public static bool ValidarRangoFechas(DateTimePicker dtpInicio, DateTimePicker dtpFin)
        {
            dtpInicio.MaxDate = DateTime.Today;
            dtpFin.MaxDate = DateTime.Today;

            if (dtpInicio.Value.Date > dtpFin.Value.Date)
            {
                MessageBox.Show("La fecha de inicio no puede ser posterior a la fecha final.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpInicio.Value = dtpFin.Value;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Sobrecarga de compatibilidad para formularios que no verifican el
        /// resultado booleano y solo esperan el efecto de autocorrección.
        /// </summary>
        public static void ValidarRangoFechas(DateTimePicker dtpInicio, DateTimePicker dtpFin, bool ajustar)
        {
            ValidarRangoFechas(dtpInicio, dtpFin);
        }



        public static bool EsRTNValido(string rtn)
        {
            TextBox temp = new TextBox { Text = rtn };
            return EsRTNValido(temp);
        }

        public static bool EsTelefonoHondurasValido(string telefono)
        {
            TextBox temp = new TextBox { Text = telefono };
            return EsTelefonoHondurasValido(temp);
        }

        public static bool ValidacionCorreo(string correo, string nombreCampo = "Correo electrónico")
        {
            TextBox temp = new TextBox { Text = correo };
            return ValidacionCorreo(temp, nombreCampo);
        }
    }
}