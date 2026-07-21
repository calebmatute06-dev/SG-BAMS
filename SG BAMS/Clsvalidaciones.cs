using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SG_BAMS
{
    

    /// <summary>
    /// Validaciones de campos de texto y nombres.
    /// SRP: única responsabilidad — validar entradas de tipo texto.
    /// </summary>
    public static class ValidacionesTexto
    {
        /// <summary>
        /// Valida que el nombre personal contenga solo letras, espacios y caracteres acentuados.
        /// Longitud mínima 2, máxima 100.
        /// </summary>
        public static bool EsNombrePersonalValido(TextBox txt, string nombreCampo)
        {
            string valor = txt.Text.Trim();

            if (string.IsNullOrWhiteSpace(valor))
            {
                MessageBox.Show($"El campo '{nombreCampo}' es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }

            if (valor.Length < 2)
            {
                MessageBox.Show($"El campo '{nombreCampo}' debe tener al menos 2 caracteres.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }

            if (valor.Length > 100)
            {
                MessageBox.Show($"El campo '{nombreCampo}' no puede exceder 100 caracteres.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }

            if (!Regex.IsMatch(valor, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s'-]+$"))
            {
                MessageBox.Show($"El campo '{nombreCampo}' solo puede contener letras, espacios y guiones.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }

            if (Regex.IsMatch(valor, @"\b[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ]\b(?<!\bde\b|\bla\b|\bel\b|\blos\b|\blas\b|\bdel\b)"))
            {
                MessageBox.Show($"El campo '{nombreCampo}' no puede contener letras aisladas (excepto artículos).", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Valida que el campo sea alfanumérico. Permite letras, números y espacios.
        /// </summary>
        public static bool EsAlfanumericoValido(TextBox txt, string nombreCampo)
        {
            string valor = txt.Text.Trim();

            if (string.IsNullOrWhiteSpace(valor))
            {
                MessageBox.Show($"El campo '{nombreCampo}' es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }

            if (!Regex.IsMatch(valor, @"^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑüÜ\s]+$"))
            {
                MessageBox.Show($"El campo '{nombreCampo}' solo puede contener letras y números.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Valida que el nombre de usuario cumpla el formato requerido:
        /// solo letras, números, puntos y guiones bajos. Longitud 3-30.
        /// </summary>
        public static bool EsNombreUsuarioValido(TextBox txt, string nombreCampo)
        {
            string valor = txt.Text.Trim();

            if (string.IsNullOrWhiteSpace(valor))
            {
                MessageBox.Show($"El campo '{nombreCampo}' es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }

            if (valor.Length < 3 || valor.Length > 30)
            {
                MessageBox.Show($"El campo '{nombreCampo}' debe tener entre 3 y 30 caracteres.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }

            if (!Regex.IsMatch(valor, @"^[a-zA-Z0-9._]+$"))
            {
                MessageBox.Show($"El campo '{nombreCampo}' solo puede contener letras, números, puntos y guiones bajos.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Valida que el campo no esté vacío.
        /// </summary>
        public static bool CampoVacio(TextBox txt, string nombreCampo)
        {
            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                MessageBox.Show($"El campo '{nombreCampo}' es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Valida que una selección de combo no esté vacía.
        /// </summary>
        public static bool ValidarSeleccion(ComboBox cmb, string nombreCampo)
        {
            if (cmb.SelectedIndex < 0 || cmb.SelectedItem == null)
            {
                MessageBox.Show($"Debe seleccionar un valor para '{nombreCampo}'.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmb.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Filtra el evento KeyPress para permitir solo letras y espacios.
        /// </summary>
        public static void PermitirSoloLetras(KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        /// <summary>
        /// Filtra el evento KeyPress para permitir solo letras, números y espacios.
        /// </summary>
        public static void PermitirAlfanumerico(KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        /// <summary>
        /// Filtra el evento KeyPress para permitir solo letras y números sin espacios.
        /// </summary>
        public static void PermitirSoloLetrasNumerosSinEspacios(KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        /// <summary>
        /// Valida que el valor sea alfanumérico para búsquedas.
        /// </summary>
        public static bool ValidarBusquedaAlfanumerica(TextBox txt)
        {
            if (string.IsNullOrWhiteSpace(txt.Text)) return true;
            if (!Regex.IsMatch(txt.Text.Trim(), @"^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s]+$"))
            {
                MessageBox.Show("El criterio de búsqueda contiene caracteres no permitidos.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Valida que el código de barra solo contenga caracteres válidos.
        /// </summary>
        public static bool ValidarCodigoBarra(TextBox txt)
        {
            string valor = txt.Text.Trim();
            if (string.IsNullOrWhiteSpace(valor))
            {
                MessageBox.Show("El código de barras es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }

            if (!Regex.IsMatch(valor, @"^[a-zA-Z0-9\-]+$"))
            {
                MessageBox.Show("El código de barras solo puede contener letras, números y guiones.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Valida nombre de proveedor: permite letras, números, espacios y caracteres especiales de empresa.
        /// </summary>
        public static bool EsNombreManualValido(TextBox txt, string nombreCampo)
        {
            string valor = txt.Text.Trim();

            if (string.IsNullOrWhiteSpace(valor))
            {
                MessageBox.Show($"El campo '{nombreCampo}' es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }

            if (!Regex.IsMatch(valor, @"^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑüÜ\s&.,'-]+$"))
            {
                MessageBox.Show($"El campo '{nombreCampo}' contiene caracteres no permitidos.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Filtra KeyPress para permitir solo letras, números sin espacios.
        /// </summary>
        public static void PermitirSoloLetrasYNumeros(KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        /// <summary>
        /// Valida que el nombre no sea válido si tiene solo un carácter.
        /// </summary>
        public static bool ValidarNombre(TextBox txt, string nombreCampo)
        {
            return EsNombrePersonalValido(txt, nombreCampo);
        }
    }

    /// <summary>
    /// Validaciones de campos numéricos y precios.
    /// SRP: única responsabilidad — validar entradas de tipo numérico.
    /// </summary>
    public static class ValidacionesNumericas
    {
        /// <summary>
        /// Valida que el campo contenga un número decimal válido y positivo.
        /// </summary>
        public static bool EsNumeroDecimalValido(TextBox txt, string nombreCampo)
        {
            string valor = txt.Text.Trim();

            if (string.IsNullOrWhiteSpace(valor))
            {
                MessageBox.Show($"El campo '{nombreCampo}' es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }

            if (!decimal.TryParse(valor, out decimal numero))
            {
                MessageBox.Show($"El campo '{nombreCampo}' debe ser un número válido.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }

            if (numero <= 0)
            {
                MessageBox.Show($"El campo '{nombreCampo}' debe ser mayor a cero.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Valida el precio: debe ser un decimal positivo con hasta 2 decimales.
        /// </summary>
        public static bool ValidarPrecio(TextBox txt, string nombreCampo)
        {
            string valor = txt.Text.Trim();

            if (string.IsNullOrWhiteSpace(valor))
            {
                MessageBox.Show($"El campo '{nombreCampo}' es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }

            if (!decimal.TryParse(valor, out decimal precio) || precio <= 0)
            {
                MessageBox.Show($"El campo '{nombreCampo}' debe ser un precio válido mayor a cero.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Valida decimales con soporte para separador de miles y punto decimal.
        /// </summary>
        public static bool ValidarDecimales(TextBox txt, string nombreCampo)
        {
            return EsNumeroDecimalValido(txt, nombreCampo);
        }

        /// <summary>
        /// Valida que el campo contenga solo números enteros.
        /// </summary>
        public static bool ValidarSoloNumeros(TextBox txt, string nombreCampo)
        {
            string valor = txt.Text.Trim();

            if (string.IsNullOrWhiteSpace(valor))
            {
                MessageBox.Show($"El campo '{nombreCampo}' es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }

            if (!int.TryParse(valor, out _))
            {
                MessageBox.Show($"El campo '{nombreCampo}' debe contener solo números enteros.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Filtra el evento KeyPress para permitir solo números y un punto decimal.
        /// </summary>
        public static void PermitirNumerosYDecimales(KeyPressEventArgs e, TextBox txt)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                if (e.KeyChar == '.' && !txt.Text.Contains('.'))
                    return;
                e.Handled = true;
            }
        }

        /// <summary>
        /// Intenta parsear un monto ingresado aceptando comas como separadores de miles.
        /// </summary>
        public static bool ParsearMontoInteligente(TextBox txt, out decimal monto)
        {
            monto = 0;
            string valor = txt.Text.Trim().Replace(",", "");
            return decimal.TryParse(valor, out monto) && monto >= 0;
        }
    }

    /// <summary>
    /// Validaciones de datos de contacto: teléfono, correo y RTN.
    /// SRP: única responsabilidad — validar información de contacto e identificación fiscal.
    /// </summary>
    public static class ValidacionesContacto
    {
        /// <summary>
        /// Valida que el teléfono cumpla el formato hondureño: 8 dígitos.
        /// </summary>
        public static bool EsTelefonoHondurasValido(TextBox txt)
        {
            string valor = txt.Text.Trim();

            if (string.IsNullOrWhiteSpace(valor))
            {
                MessageBox.Show("El campo Teléfono es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }

            if (!Regex.IsMatch(valor, @"^\d{8}$"))
            {
                MessageBox.Show("El teléfono debe tener exactamente 8 dígitos numéricos.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }

            string prefijo = valor.Substring(0, 2);
            string[] prefijosValidos = { "32", "33", "34", "35", "36", "37", "38", "39",
                                         "96", "97", "98", "99", "20", "21", "22", "23",
                                         "24", "25", "26", "27", "28", "29", "55" };
            bool prefijoValido = false;
            foreach (string p in prefijosValidos)
            {
                if (prefijo == p) { prefijoValido = true; break; }
            }

            if (!prefijoValido)
            {
                MessageBox.Show("El teléfono no corresponde a un prefijo hondureño válido.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }

            return true;

        }

        /// <summary>
        /// Filtra KeyPress para permitir solo dígitos en el campo de teléfono.
        /// </summary>
        public static void ValidarTelefonoKeyPress(KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        /// <summary>
        /// Valida el formato del correo electrónico.
        /// </summary>
        public static bool ValidacionCorreo(TextBox txt)
        {
            string valor = txt.Text.Trim();

            if (string.IsNullOrWhiteSpace(valor))
            {
                MessageBox.Show("El campo Correo es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }

            if (!Regex.IsMatch(valor, @"^[^@\s]+@[^@\s]+\.[^@\s]{2,}$"))
            {
                MessageBox.Show("El formato del correo no es válido. Ejemplo: usuario@dominio.com", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }

            if (valor.Length > 100)
            {
                MessageBox.Show("El correo no puede exceder los 100 caracteres.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Valida el Registro Tributario Nacional (RTN) hondureño.
        /// Formato: 14 dígitos numéricos.
        /// </summary>
        public static bool EsRTNValido(TextBox txt)
        {
            string valor = txt.Text.Trim();

            if (string.IsNullOrWhiteSpace(valor))
            {
                MessageBox.Show("El campo RTN es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }

            if (!Regex.IsMatch(valor, @"^\d{14}$"))
            {
                MessageBox.Show("El RTN debe contener exactamente 14 dígitos numéricos.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }

            string municipio = valor.Substring(0, 4);
            if (!int.TryParse(municipio, out int codigoMunicipio) ||
                codigoMunicipio < 101 || codigoMunicipio > 1818)
            {
                MessageBox.Show("Los primeros 4 dígitos del RTN no corresponden a un municipio válido.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Valida que el RTN sea único (no duplicado) consultando externamente.
        /// Este método recibe el resultado de la consulta ya ejecutada para mantener SRP.
        /// </summary>
        public static bool ValidarRTNUnico(bool rtnYaExiste, TextBox txt)
        {
            if (rtnYaExiste)
            {
                MessageBox.Show("El RTN ingresado ya está registrado en el sistema.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }
            return true;
        }
    }

    /// <summary>
    /// Validaciones de contraseñas y seguridad.
    /// SRP: única responsabilidad — validar credenciales de acceso.
    /// </summary>
    public static class ValidacionesSeguridad
    {
        /// <summary>
        /// Valida que la contraseña cumpla los requisitos mínimos de seguridad:
        /// al menos 8 caracteres, una mayúscula, una minúscula y un número.
        /// </summary>
        public static bool EsPasswordValido(TextBox txt, string nombreCampo)
        {
            string valor = txt.Text;

            if (string.IsNullOrWhiteSpace(valor))
            {
                MessageBox.Show($"El campo '{nombreCampo}' es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }

            if (valor.Length < 8)
            {
                MessageBox.Show($"La contraseña debe tener al menos 8 caracteres.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }

            if (!Regex.IsMatch(valor, @"[A-Z]"))
            {
                MessageBox.Show("La contraseña debe contener al menos una letra mayúscula.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }

            if (!Regex.IsMatch(valor, @"[a-z]"))
            {
                MessageBox.Show("La contraseña debe contener al menos una letra minúscula.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }

            if (!Regex.IsMatch(valor, @"[0-9]"))
            {
                MessageBox.Show("La contraseña debe contener al menos un número.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }

            return true;
        }
    }

    /// <summary>
    /// Validaciones de rangos de fecha.
    /// SRP: única responsabilidad — validar entradas relacionadas con fechas.
    /// </summary>
    public static class ValidacionesFechas
    {
        /// <summary>
        /// Valida que la fecha inicial no sea posterior a la fecha final.
        /// </summary>
        public static bool ValidarRangoFechas(DateTimePicker dtpDesde, DateTimePicker dtpHasta)
        {
            if (dtpDesde.Value.Date > dtpHasta.Value.Date)
            {
                MessageBox.Show("La fecha de inicio no puede ser posterior a la fecha final.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpDesde.Focus();
                return false;
            }
            return true;
        }
    }

    /// <summary>
    /// Fachada estática que mantiene compatibilidad con el código existente.
    /// OCP: el código existente no necesita modificarse — ClsValidaciones sigue siendo válido
    /// pero ahora delega a las clases especializadas en lugar de contener toda la lógica.
    /// </summary>
    public static partial class ClsValidaciones
    {
        // --- Texto ---
        public static bool EsNombrePersonalValido(TextBox txt, string campo) =>
            ValidacionesTexto.EsNombrePersonalValido(txt, campo);

        public static bool EsAlfanumericoValido(TextBox txt, string campo) =>
            ValidacionesTexto.EsAlfanumericoValido(txt, campo);

        public static bool EsNombreUsuarioValido(TextBox txt, string campo) =>
            ValidacionesTexto.EsNombreUsuarioValido(txt, campo);

        public static bool CampoVacio(TextBox txt, string campo) =>
            ValidacionesTexto.CampoVacio(txt, campo);

        public static bool ValidarSeleccion(ComboBox cmb, string campo) =>
            ValidacionesTexto.ValidarSeleccion(cmb, campo);

        public static void PermitirSoloLetras(KeyPressEventArgs e) =>
            ValidacionesTexto.PermitirSoloLetras(e);

        public static void PermitirAlfanumerico(KeyPressEventArgs e) =>
            ValidacionesTexto.PermitirAlfanumerico(e);

        public static void PermitirSoloLetrasNumerosSinEspacios(KeyPressEventArgs e) =>
            ValidacionesTexto.PermitirSoloLetrasNumerosSinEspacios(e);

        public static bool ValidarBusquedaAlfanumerica(TextBox txt) =>
            ValidacionesTexto.ValidarBusquedaAlfanumerica(txt);

        public static bool ValidarCodigoBarra(TextBox txt) =>
            ValidacionesTexto.ValidarCodigoBarra(txt);

        public static bool EsNombreManualValido(TextBox txt, string campo) =>
            ValidacionesTexto.EsNombreManualValido(txt, campo);

        public static void PermitirSoloLetrasYNumeros(KeyPressEventArgs e) =>
            ValidacionesTexto.PermitirSoloLetrasYNumeros(e);

        public static bool ValidarNombre(TextBox txt, string campo) =>
            ValidacionesTexto.ValidarNombre(txt, campo);

        // --- Numéricas ---
        public static bool EsNumeroDecimalValido(TextBox txt, string campo) =>
            ValidacionesNumericas.EsNumeroDecimalValido(txt, campo);

        public static bool ValidarPrecio(TextBox txt, string campo) =>
            ValidacionesNumericas.ValidarPrecio(txt, campo);

        public static bool ValidarDecimales(TextBox txt, string campo) =>
            ValidacionesNumericas.ValidarDecimales(txt, campo);

        public static bool ValidarSoloNumeros(TextBox txt, string campo) =>
            ValidacionesNumericas.ValidarSoloNumeros(txt, campo);

        public static void PermitirNumerosYDecimales(KeyPressEventArgs e, TextBox txt) =>
            ValidacionesNumericas.PermitirNumerosYDecimales(e, txt);

        public static bool ParsearMontoInteligente(TextBox txt, out decimal monto) =>
            ValidacionesNumericas.ParsearMontoInteligente(txt, out monto);

        // --- Contacto ---
        public static bool EsTelefonoHondurasValido(TextBox txt) =>
            ValidacionesContacto.EsTelefonoHondurasValido(txt);

        public static void ValidarTelefonoKeyPress(KeyPressEventArgs e) =>
            ValidacionesContacto.ValidarTelefonoKeyPress(e);

        public static bool ValidacionCorreo(TextBox txt) =>
            ValidacionesContacto.ValidacionCorreo(txt);

        public static bool EsRTNValido(TextBox txt) =>
            ValidacionesContacto.EsRTNValido(txt);

        public static bool ValidarRTNUnico(bool rtnYaExiste, TextBox txt) =>
            ValidacionesContacto.ValidarRTNUnico(rtnYaExiste, txt);

        // --- Seguridad ---
        public static bool EsPasswordValido(TextBox txt, string campo) =>
            ValidacionesSeguridad.EsPasswordValido(txt, campo);

        // --- Fechas ---
        public static bool ValidarRangoFechas(DateTimePicker desde, DateTimePicker hasta) =>
            ValidacionesFechas.ValidarRangoFechas(desde, hasta);

        // ============================================================
        // SOBRECARGAS DE COMPATIBILIDAD CON EL CÓDIGO EXISTENTE
        // Permiten que los formularios que usan las firmas originales
        // sigan compilando sin modificaciones.
        // ============================================================

        /// <summary>
        /// Compatibilidad: ValidarPrecio con string (firma original de Clsvalidaciones.cs).
        /// Usado en AgregarProducto.cs y ModificarProducto.cs.
        /// </summary>
        public static bool ValidarPrecio(string precio)
        {
            if (string.IsNullOrWhiteSpace(precio))
            {
                MessageBox.Show("El campo Precio es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!decimal.TryParse(precio.Trim(), out decimal val) || val <= 0)
            {
                MessageBox.Show("El Precio debe ser un valor válido mayor a cero.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Compatibilidad: ValidarCodigoBarra con string (firma original de Clsvalidaciones.cs).
        /// Usado en AgregarProducto.cs y ModificarProducto.cs.
        /// </summary>
        public static bool ValidarCodigoBarra(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo) ||
                !System.Text.RegularExpressions.Regex.IsMatch(codigo.Trim(), @"^[a-zA-Z0-9\-]{6,20}$"))
            {
                MessageBox.Show("El código de barras debe ser alfanumérico y tener entre 6 y 20 caracteres.",
                    "Código de Barras", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Compatibilidad: PermitirNumerosYDecimales con (object sender, KeyPressEventArgs e)
        /// — firma original usada en Compras, BateriaVieja, AgregarProducto, ModificarProducto, Pago Deuda.
        /// </summary>
        public static void PermitirNumerosYDecimales(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar)) return;
            if (e.KeyChar != '.' && e.KeyChar != ',') { e.Handled = true; return; }
            if (sender is Control ctrl)
            {
                string t = ctrl.Text;
                if (e.KeyChar == '.' && t.Contains('.')) e.Handled = true;
                else if (e.KeyChar == ',' && t.Contains('.')) e.Handled = true;
            }
            else { e.Handled = true; }
        }

        /// <summary>
        /// Compatibilidad: ValidarBusquedaAlfanumerica con KeyPressEventArgs
        /// — firma original usada en FacturasAdm, FacturasEmp, Compras, AgregarProveedores.
        /// </summary>
        public static void ValidarBusquedaAlfanumerica(KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar) || char.IsWhiteSpace(e.KeyChar)) return;
            if (!System.Text.RegularExpressions.Regex.IsMatch(
                    e.KeyChar.ToString(), @"^[a-zA-Z0-9ñÑáéíóúÁÉÍÓÚüÜ&\s]+$"))
                e.Handled = true;
        }

        /// <summary>
        /// Compatibilidad: ValidarRangoFechas sin retorno (void) — firma original
        /// usada en FacturasAdm y FacturasEmp que no verifican el bool de retorno.
        /// </summary>
        public static void ValidarRangoFechas(DateTimePicker dtpInicio, DateTimePicker dtpFin, bool ajustar)
        {
            dtpInicio.MaxDate = DateTime.Today;
            dtpFin.MaxDate = DateTime.Today;
            if (dtpInicio.Value.Date > dtpFin.Value.Date)
                dtpInicio.Value = dtpFin.Value;
        }

        /// <summary>
        /// Compatibilidad: ValidarSoloNumeros con KeyPressEventArgs solo
        /// — firma original usada en AgregarProveedores.
        /// </summary>
        public static void ValidarSoloNumeros(KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        /// <summary>
        /// Compatibilidad: EsNumeroDecimalValido con out decimal — firma original.
        /// </summary>
        public static bool EsNumeroDecimalValido(Control control, string nombreCampo, out decimal valorResultado)
        {
            valorResultado = 0;
            string texto = control.Text.Trim();
            if (string.IsNullOrWhiteSpace(texto))
            {
                MessageBox.Show($"El campo '{nombreCampo}' no puede estar vacío.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus(); return false;
            }
            if (!decimal.TryParse(texto.Replace(",", ""), out valorResultado) || valorResultado <= 0)
            {
                MessageBox.Show($"{nombreCampo} debe ser un valor numérico válido y mayor a cero.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus(); return false;
            }
            return true;
        }

        /// <summary>
        /// Compatibilidad: ParsearMontoInteligente con string — firma original.
        /// </summary>
        public static double ParsearMontoInteligente(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return 0;
            texto = texto.Replace(",", "").Trim();
            return double.TryParse(texto, System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture, out double r) ? r : 0;
        }

        public static double ParsearMontoMoneda(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return 0;

            texto = texto.Replace("L.", "")
                         .Replace(",", "")
                         .Trim();

            return double.TryParse(
                texto,
                NumberStyles.Any,
                CultureInfo.InvariantCulture,
                out double resultado)
                    ? resultado
                    : 0;
        }

        /// <summary>
        /// Compatibilidad: ValidarCodigoBarra con Control (firma del documento Cambios).
        /// </summary>
        public static bool ValidarCodigoBarra(Control txt)
            => ValidarCodigoBarra(txt.Text.Trim());

        /// <summary>
        /// Compatibilidad: ValidarPrecio con Control y string (firma del documento Cambios).
        /// </summary>
        public static bool ValidarPrecio(Control txt, string campo)
            => ValidarPrecio(txt.Text.Trim());

        /// <summary>
        /// Compatibilidad: ForzarCodigoBarraKeyPress sin retorno — firma original de Clsvalidaciones.cs.
        /// </summary>
        public static void ForzarCodigoBarraKeyPress(KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar)) e.KeyChar = char.ToUpper(e.KeyChar);
            else if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) e.Handled = true;
        }

        /// <summary>
        /// Compatibilidad: ValidarTelefonoKeyPress con KryptonTextBox — firma original.
        /// </summary>
        public static void ValidarTelefonoKeyPress(Krypton.Toolkit.KryptonTextBox txt, KeyPressEventArgs e)
            => ValidacionesContacto.ValidarTelefonoKeyPress(e);

        /// <summary>
        /// Compatibilidad: ValidarTelefonoKeyPress con Control genérico.
        /// </summary>
        public static void ValidarTelefonoKeyPress(Control control, KeyPressEventArgs e)
            => ValidacionesContacto.ValidarTelefonoKeyPress(e);

        /// <summary>
        /// Compatibilidad: ValidarSeleccion con KryptonComboBox — firma original.
        /// </summary>
        public static bool ValidarSeleccion(Krypton.Toolkit.KryptonComboBox cmb, string nombreCampo)
        {
            if (cmb.SelectedIndex < 0 || cmb.SelectedItem == null)
            {
                MessageBox.Show($"Debe seleccionar un valor para '{nombreCampo}'.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmb.Focus(); return false;
            }
            return true;
        }

        /// <summary>
        /// Compatibilidad: ValidarNombreUnico con tabla/columna — firma original de Clsvalidaciones.cs.
        /// Delega al SP sp_ValidarNombreUnico igual que la versión original.
        /// </summary>
        public static bool ValidarNombreUnico(Control control, string tabla, string columnaNombre,
            string nombreCampo, int idExcluir = 0, string idColumna = "id")
        {
            string nombre = control.Text.Trim();
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show($"El campo '{nombreCampo}' no puede estar vacío.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus(); return false;
            }
            var conexion = new ClsRepositorioBaseDatos();
            try
            {
                conexion.AbrirConexion();
                using var cmd = new Microsoft.Data.SqlClient.SqlCommand("sp_ValidarNombreUnico", conexion.Conectar);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
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
                    control.Focus(); return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al validar nombre único: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally { conexion.Cerrar(); }
        }

        /// <summary>
        /// Compatibilidad: ValidarRTNUnico con Control y tabla — firma original de Clsvalidaciones.cs.
        /// </summary>
        public static bool ValidarRTNUnico(Control control, string tablaOrigen, int idExcluir = 0)
        {
            string rtn = control.Text.Trim();
            if (string.IsNullOrWhiteSpace(rtn))
            {
                MessageBox.Show("El RTN no puede estar vacío.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus(); return false;
            }
            var conexion = new ClsRepositorioBaseDatos();
            try
            {
                conexion.AbrirConexion();
                using var cmd = new Microsoft.Data.SqlClient.SqlCommand("sp_ValidarRTNUnico", conexion.Conectar);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@rtn", rtn);
                cmd.Parameters.AddWithValue("@tablaOrigen", tablaOrigen.ToLower());
                cmd.Parameters.AddWithValue("@idExcluir", idExcluir);
                int conteo = Convert.ToInt32(cmd.ExecuteScalar());
                if (conteo > 0)
                {
                    MessageBox.Show($"El RTN '{rtn}' ya se encuentra registrado.", "RTN Duplicado",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    control.Focus(); return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al validar RTN: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally { conexion.Cerrar(); }
        }

        /// <summary>
        /// Compatibilidad: ValidacionCorreo con Control y nombreCampo opcional — firma original.
        /// </summary>
        public static bool ValidacionCorreo(Control control, string nombreCampo = "Correo electrónico")
        {
            string correo = control.Text.Trim();
            if (string.IsNullOrWhiteSpace(correo))
            {
                MessageBox.Show($"El campo '{nombreCampo}' es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus(); return false;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(correo,
                    @"^[a-zA-Z0-9]+([._][a-zA-Z0-9]+)*@[a-zA-Z0-9]+([.\-][a-zA-Z0-9]+)*\.[a-zA-Z]{2,}$"))
            {
                MessageBox.Show($"El formato de '{nombreCampo}' no es válido (ej: usuario@dominio.com).",
                    "Formato Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus(); return false;
            }
            return true;
        }

        /// <summary>
        /// Compatibilidad: EsPasswordValido con Control y minLength opcional — firma original.
        /// </summary>
        public static bool EsPasswordValido(Control control, string nombreCampo, int minLength = 6)
        {
            string pass = control.Text;
            if (string.IsNullOrWhiteSpace(pass) || pass.Contains(" ") || pass.Length < minLength)
            {
                MessageBox.Show($"{nombreCampo} debe tener al menos {minLength} caracteres sin espacios.",
                    "Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus(); return false;
            }
            return true;
        }

        /// <summary>
        /// Compatibilidad: EsTelefonoHondurasValido con Control genérico — firma original.
        /// </summary>
        public static bool EsTelefonoHondurasValido(Control control)
        {
            string tel = control.Text.Trim();
            if (!System.Text.RegularExpressions.Regex.IsMatch(tel, @"^[23789]\d{7}$"))
            {
                MessageBox.Show("El teléfono debe tener 8 dígitos y comenzar con prefijo válido (2,3,7,8,9).",
                    "Teléfono Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus(); return false;
            }
            return true;
        }

        /// <summary>
        /// Compatibilidad: EsRTNValido con Control genérico — firma original.
        /// </summary>
        public static bool EsRTNValido(Control control)
        {
            string rtn = control.Text.Trim();
            if (!System.Text.RegularExpressions.Regex.IsMatch(rtn, @"^\d{14}$"))
            {
                MessageBox.Show("El RTN debe tener exactamente 14 dígitos.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus(); return false;
            }
            return true;
        }

        /// <summary>
        /// Compatibilidad: EsNombrePersonalValido con Control genérico y parámetros opcionales.
        /// </summary>
        public static bool EsNombrePersonalValido(Control control, string nombreCampo,
            int minLength = 3, int maxLength = 50)
        {
            string texto = control.Text.Trim();
            if (string.IsNullOrWhiteSpace(texto))
            {
                MessageBox.Show($"El campo '{nombreCampo}' es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus(); return false;
            }
            if (texto.Length < minLength || texto.Length > maxLength)
            {
                MessageBox.Show($"{nombreCampo} debe tener entre {minLength} y {maxLength} caracteres.",
                    "Longitud", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus(); return false;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(texto, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]+$"))
            {
                MessageBox.Show($"{nombreCampo} solo debe contener letras.", "Formato",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus(); return false;
            }
            return true;
        }

        /// <summary>
        /// Compatibilidad: EsAlfanumericoValido con Control genérico y parámetros opcionales.
        /// </summary>
        public static bool EsAlfanumericoValido(Control control, string nombreCampo,
            int minLength = 3, int maxLength = 50)
        {
            string texto = control.Text.Trim();
            if (string.IsNullOrWhiteSpace(texto))
            {
                MessageBox.Show($"El campo '{nombreCampo}' es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus(); return false;
            }
            if (texto.Length < minLength || texto.Length > maxLength)
            {
                MessageBox.Show($"{nombreCampo} debe tener entre {minLength} y {maxLength} caracteres.",
                    "Longitud", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus(); return false;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(texto, @"^[a-zA-Z0-9ñÑáéíóúÁÉÍÓÚ\s&]+$"))
            {
                MessageBox.Show($"{nombreCampo} solo debe contener letras, números y '&'.", "Formato",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus(); return false;
            }
            return true;
        }

        /// <summary>
        /// Compatibilidad: EsNombreUsuarioValido con Control genérico y parámetros opcionales.
        /// </summary>
        public static bool EsNombreUsuarioValido(Control control, string nombreCampo,
            int minLength = 3, int maxLength = 20)
        {
            string texto = control.Text.Trim();
            if (string.IsNullOrWhiteSpace(texto))
            {
                MessageBox.Show($"El campo '{nombreCampo}' es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus(); return false;
            }
            if (texto.Length < minLength || texto.Length > maxLength)
            {
                MessageBox.Show($"{nombreCampo} debe tener entre {minLength} y {maxLength} caracteres.",
                    "Longitud", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus(); return false;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(texto, @"^[a-zA-Z0-9\s]+$"))
            {
                MessageBox.Show($"{nombreCampo} solo puede contener letras, números y espacios.", "Formato",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus(); return false;
            }
            return true;
        }

        /// <summary>
        /// Compatibilidad: CampoVacio que retorna true si vacío (firma original — invertida respecto al nuevo).
        /// La versión original retornaba true=vacío; la nueva retorna false=inválido.
        /// Este alias mantiene la semántica original.
        /// </summary>
        public static bool CampoVacioLegacy(Control control, string nombreCampo)
        {
            if (string.IsNullOrWhiteSpace(control.Text))
            {
                MessageBox.Show($"El campo '{nombreCampo}' no puede estar vacío.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                control.Focus();
                return true; // vacío = true (comportamiento original)
            }
            return false;
        }

        /// <summary>
        /// Compatibilidad: ValidarBusquedaAlfanumerica con Control genérico — firma del proyecto.
        /// </summary>
        public static bool ValidarBusquedaAlfanumerica(Control txt)
        {
            if (string.IsNullOrWhiteSpace(txt.Text)) return true;
            if (!System.Text.RegularExpressions.Regex.IsMatch(txt.Text.Trim(),
                    @"^[a-zA-Z0-9ñÑáéíóúÁÉÍÓÚ\s]+$"))
            {
                MessageBox.Show("El criterio de búsqueda contiene caracteres no permitidos.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus(); return false;
            }
            return true;
        }

        // ============================================================
        // SOBRECARGAS PARA KryptonTextBox
        // KryptonTextBox NO hereda de System.Windows.Forms.TextBox,
        // por eso necesita sus propias sobrecargas explícitas.
        // Todos los métodos delegan a la sobrecarga Control para no
        // duplicar lógica (OCP / DRY).
        // ============================================================

        /// <summary>
        /// Sobrecarga KryptonTextBox: CampoVacio.
        /// Usado en BateriaVieja.cs, FacturaProducto.cs y Login.cs.
        /// </summary>
        public static bool CampoVacio(Krypton.Toolkit.KryptonTextBox txt, string nombreCampo)
        {
            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                MessageBox.Show($"El campo '{nombreCampo}' no puede estar vacío.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return true;   // true = vacío, igual que la firma original
            }
            return false;
        }

        /// <summary>
        /// Sobrecarga KryptonTextBox: EsPasswordValido.
        /// Usado en Login.cs línea 114–115.
        /// </summary>
        public static bool EsPasswordValido(Krypton.Toolkit.KryptonTextBox txt, string nombreCampo,
            int minLength = 6)
        {
            string pass = txt.Text;
            if (string.IsNullOrWhiteSpace(pass) || pass.Contains(" ") || pass.Length < minLength)
            {
                MessageBox.Show($"{nombreCampo} debe tener al menos {minLength} caracteres sin espacios.",
                    "Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }
            if (System.Text.RegularExpressions.Regex.IsMatch(pass, @"[^\x20-\x7E]"))
            {
                MessageBox.Show($"{nombreCampo} solo puede contener letras, números y caracteres estándar.",
                    "Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }
            if (System.Text.RegularExpressions.Regex.IsMatch(pass, @"(.)\1{2,}"))
            {
                MessageBox.Show($"{nombreCampo} es muy débil (demasiados caracteres repetidos seguidos).",
                    "Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Sobrecarga KryptonTextBox: EsNombrePersonalValido.
        /// Por si algún formulario pasa un KryptonTextBox directamente.
        /// </summary>
        public static bool EsNombrePersonalValido(Krypton.Toolkit.KryptonTextBox txt, string nombreCampo,
            int minLength = 3, int maxLength = 50)
        {
            string texto = txt.Text.Trim();
            if (string.IsNullOrWhiteSpace(texto))
            {
                MessageBox.Show($"El campo '{nombreCampo}' es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus(); return false;
            }
            if (texto.Length < minLength || texto.Length > maxLength)
            {
                MessageBox.Show($"{nombreCampo} debe tener entre {minLength} y {maxLength} caracteres.",
                    "Longitud", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus(); return false;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(texto, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]+$"))
            {
                MessageBox.Show($"{nombreCampo} solo debe contener letras.", "Formato",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus(); return false;
            }
            return true;
        }

        /// <summary>
        /// Sobrecarga KryptonTextBox: EsNombreUsuarioValido.
        /// </summary>
        public static bool EsNombreUsuarioValido(Krypton.Toolkit.KryptonTextBox txt, string nombreCampo,
            int minLength = 3, int maxLength = 20)
        {
            string texto = txt.Text.Trim();
            if (string.IsNullOrWhiteSpace(texto))
            {
                MessageBox.Show($"El campo '{nombreCampo}' es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus(); return false;
            }
            if (texto.Length < minLength || texto.Length > maxLength)
            {
                MessageBox.Show($"{nombreCampo} debe tener entre {minLength} y {maxLength} caracteres.",
                    "Longitud", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus(); return false;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(texto, @"^[a-zA-Z0-9\s]+$"))
            {
                MessageBox.Show($"{nombreCampo} solo puede contener letras, números y espacios.", "Formato",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus(); return false;
            }
            return true;
        }

        /// <summary>
        /// Sobrecarga KryptonTextBox: EsRTNValido.
        /// </summary>
        public static bool EsRTNValido(Krypton.Toolkit.KryptonTextBox txt)
        {
            string rtn = txt.Text.Trim();
            if (!System.Text.RegularExpressions.Regex.IsMatch(rtn, @"^\d{14}$"))
            {
                MessageBox.Show("El RTN debe tener exactamente 14 dígitos.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus(); return false;
            }
            return true;
        }

        /// <summary>
        /// Sobrecarga KryptonTextBox: EsTelefonoHondurasValido.
        /// </summary>
        public static bool EsTelefonoHondurasValido(Krypton.Toolkit.KryptonTextBox txt)
        {
            string tel = txt.Text.Trim();
            if (!System.Text.RegularExpressions.Regex.IsMatch(tel, @"^[23789]\d{7}$"))
            {
                MessageBox.Show("El teléfono debe tener 8 dígitos y comenzar con prefijo válido (2,3,7,8,9).",
                    "Teléfono Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus(); return false;
            }
            return true;
        }

        /// <summary>
        /// Sobrecarga KryptonTextBox: ValidacionCorreo.
        /// </summary>
        public static bool ValidacionCorreo(Krypton.Toolkit.KryptonTextBox txt,
            string nombreCampo = "Correo electrónico")
        {
            string correo = txt.Text.Trim();
            if (string.IsNullOrWhiteSpace(correo))
            {
                MessageBox.Show($"El campo '{nombreCampo}' es obligatorio.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus(); return false;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(correo,
                    @"^[a-zA-Z0-9]+([._][a-zA-Z0-9]+)*@[a-zA-Z0-9]+([.\-][a-zA-Z0-9]+)*\.[a-zA-Z]{2,}$"))
            {
                MessageBox.Show($"El formato de '{nombreCampo}' no es válido (ej: usuario@dominio.com).",
                    "Formato Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus(); return false;
            }
            return true;
        }

        /// <summary>
        /// Sobrecarga KryptonTextBox: ValidarBusquedaAlfanumerica.
        /// </summary>
        public static bool ValidarBusquedaAlfanumerica(Krypton.Toolkit.KryptonTextBox txt)
        {
            if (string.IsNullOrWhiteSpace(txt.Text)) return true;
            if (!System.Text.RegularExpressions.Regex.IsMatch(txt.Text.Trim(),
                    @"^[a-zA-Z0-9ñÑáéíóúÁÉÍÓÚ\s]+$"))
            {
                MessageBox.Show("El criterio de búsqueda contiene caracteres no permitidos.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus(); return false;
            }
            return true;
        }

        /// <summary>
        /// Sobrecarga KryptonTextBox: EsNumeroDecimalValido con out decimal.
        /// </summary>
        public static bool EsNumeroDecimalValido(Krypton.Toolkit.KryptonTextBox txt,
            string nombreCampo, out decimal valorResultado)
        {
            valorResultado = 0;
            string texto = txt.Text.Trim();
            if (string.IsNullOrWhiteSpace(texto))
            {
                MessageBox.Show($"El campo '{nombreCampo}' no puede estar vacío.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus(); return false;
            }
            if (!decimal.TryParse(texto.Replace(",", ""), out valorResultado) || valorResultado <= 0)
            {
                MessageBox.Show($"{nombreCampo} debe ser un valor numérico válido y mayor a cero.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus(); return false;
            }
            return true;
        }

        /// <summary>
        /// Sobrecarga KryptonTextBox: ValidarRTNUnico.
        /// </summary>
        public static bool ValidarRTNUnico(Krypton.Toolkit.KryptonTextBox txt,
            string tablaOrigen, int idExcluir = 0)
        {
            string rtn = txt.Text.Trim();
            if (string.IsNullOrWhiteSpace(rtn))
            {
                MessageBox.Show("El RTN no puede estar vacío.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus(); return false;
            }
            var conexion = new ClsRepositorioBaseDatos();
            try
            {
                conexion.AbrirConexion();
                using var cmd = new Microsoft.Data.SqlClient.SqlCommand("sp_ValidarRTNUnico", conexion.Conectar);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@rtn", rtn);
                cmd.Parameters.AddWithValue("@tablaOrigen", tablaOrigen.ToLower());
                cmd.Parameters.AddWithValue("@idExcluir", idExcluir);
                int conteo = Convert.ToInt32(cmd.ExecuteScalar());
                if (conteo > 0)
                {
                    MessageBox.Show($"El RTN '{rtn}' ya se encuentra registrado.", "RTN Duplicado",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txt.Focus(); return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al validar RTN: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally { conexion.Cerrar(); }
        }

        /// <summary>
        /// Sobrecarga KryptonTextBox: ValidarNombreUnico.
        /// </summary>
        public static bool ValidarNombreUnico(Krypton.Toolkit.KryptonTextBox txt,
            string tabla, string columnaNombre, string nombreCampo,
            int idExcluir = 0, string idColumna = "id")
        {
            string nombre = txt.Text.Trim();
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show($"El campo '{nombreCampo}' no puede estar vacío.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt.Focus(); return false;
            }
            var conexion = new ClsRepositorioBaseDatos();
            try
            {
                conexion.AbrirConexion();
                using var cmd = new Microsoft.Data.SqlClient.SqlCommand("sp_ValidarNombreUnico", conexion.Conectar);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
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
                    txt.Focus(); return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al validar nombre único: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally { conexion.Cerrar(); }
        }
    }
}