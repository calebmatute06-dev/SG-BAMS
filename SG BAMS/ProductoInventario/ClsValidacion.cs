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
            bool esValido = Regex.IsMatch(nombre, @"^[a-zA-Z0-9\s]+$");

            if (!esValido || string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("El nombre del producto solo puede contener letras y números, y no puede estar vacío.",
                                "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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