using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions; 
using System.Threading.Tasks;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace SG_BAMS.ProductoInventario
{
    internal class ClsValidacion
    {
        public static bool ValidarNombre(string nombre)
        {
            // 1. Limpieza inicial y validación de nulidad
            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("El nombre no puede estar vacío.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string nombreLimpio = nombre.Trim();

            // 2. NUEVA VALIDACIÓN: Los primeros 3 caracteres DEBEN ser letras
            // Explicación: ^[a-zA-ZñÑ]{3} obliga a que empiece con 3 letras exactamente
            if (!Regex.IsMatch(nombreLimpio, @"^[a-zA-ZñÑ]{3}"))
            {
                MessageBox.Show("Los primeros tres caracteres del nombre deben ser letras (sin espacios ni números).",
                                "Formato de Inicio Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // 3. Validación de caracteres permitidos generales
            bool formatoBasico = Regex.IsMatch(nombreLimpio, @"^[a-zA-Z0-9\s&ñÑ@,.;:<>]+$");
            if (!formatoBasico)
            {
                MessageBox.Show("El nombre contiene caracteres no permitidos.",
                                "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // 4. Validación de longitud
            if (nombreLimpio.Length < 3 || nombreLimpio.Length > 40)
            {
                MessageBox.Show("El nombre del producto debe tener entre 3 y 40 caracteres.",
                                "Longitud Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // 5. No más de 2 letras repetidas consecutivamente
            if (Regex.IsMatch(nombreLimpio, @"([a-zA-ZñÑ])\1{2,}"))
            {
                MessageBox.Show("El nombre no permite que una letra se repita más de 2 veces consecutivamente.",
                                "Error de Escritura", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // 6. No espacios consecutivos
            if (nombreLimpio.Contains("  "))
            {
                MessageBox.Show("El nombre no puede contener dos o más espacios consecutivos.",
                                "Error de Espaciado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        public static bool ValidarPrecio(string precio)
        {
            bool esValido = double.TryParse(precio, out _) && !string.IsNullOrWhiteSpace(precio);

            if (!esValido)
            {
                MessageBox.Show("El precio solo puede tener números y decimales. Por favor, corríjalo.",
                                "Error de Precio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        public static bool ValidarServicio(string servicio)
        {
            bool esValido = !string.IsNullOrWhiteSpace(servicio) && Regex.IsMatch(servicio, @"^[a-zA-Z\s]{1,12}$");

            if (!esValido)
            {
                MessageBox.Show("El tipo de servicio es obligatorio. Solo se permiten letras y espacios (máximo 12 caracteres).",
                                "Campo Requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

    }
}