using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SG_BAMS.MenuPrincipal
{
    /// <summary>
    /// Servicio de gestión de temas visuales de la aplicación.
    /// Permite alternar entre modo claro y modo oscuro, guardando la preferencia
    /// del usuario en un archivo de configuración.
    /// </summary>
    public static class ClsTemas
    {
        /// <summary>
        /// Indica si el modo oscuro está habilitado.
        /// </summary>
        public static bool modoOscuroEnabled = false;

        private static string rutaArchivo = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "config_tema_krypton_fix.txt");

        private static Color colorFondoNegro = Color.FromArgb(25, 25, 25);
        private static Color colorFondoClaro = Color.White;
        private static Color colorTextoBlanco = Color.White;

        /// <summary>
        /// Guarda la preferencia de tema del usuario en el archivo de configuración.
        /// </summary>
        /// <param name="estado">True para activar modo oscuro, false para modo claro.</param>
        public static void GuardarPreferencia(bool estado)
        {
            try
            {
                modoOscuroEnabled = estado;
                File.WriteAllText(rutaArchivo, estado.ToString());
            }
            catch { }
        }

        /// <summary>
        /// Carga la preferencia de tema desde el archivo de configuración.
        /// Si el archivo no existe, se mantiene el valor por defecto.
        /// </summary>
        public static void CargarPreferencia()
        {
            try
            {
                if (File.Exists(rutaArchivo))
                    bool.TryParse(File.ReadAllText(rutaArchivo), out modoOscuroEnabled);
            }
            catch { }
        }

        /// <summary>
        /// Aplica el tema actual a un formulario y todos sus controles hijos de forma recursiva.
        /// </summary>
        /// <param name="formulario">Formulario al que se aplicará el tema.</param>
        public static void AplicarTema(Form formulario)
        {
            // En modo claro se usa blanco, no SystemColors.Control (que es gris
            // y pisaba el fondo blanco que se definió en el Designer).
            formulario.BackColor = modoOscuroEnabled ? colorFondoNegro : colorFondoClaro;

            foreach (Control objetoControl in formulario.Controls)
            {
                ProcesarEstiloCapa(objetoControl, modoOscuroEnabled);
            }
        }

        /// <summary>
        /// Aplica el estilo correspondiente a un control y todos sus hijos según el estado del modo oscuro.
        /// </summary>
        /// <param name="objetoControl">Control a procesar.</param>
        /// <param name="esOscuro">True si el modo oscuro está activado.</param>
        private static void ProcesarEstiloCapa(Control objetoControl, bool esOscuro)
        {
            if (objetoControl.Name == "kryptonButton11" || objetoControl is Button)
            {
                AplicarEstiloBoton(objetoControl, esOscuro);
            }
            else if (objetoControl is Label || objetoControl is CheckBox || objetoControl is RadioButton)
            {
                // Solo se fuerza el color en modo oscuro. En modo claro se respeta
                // el color original definido en el Designer (ej. navy de los KPI),
                // en vez de pisarlo con SystemColors.ControlText (negro).
                if (esOscuro)
                    objetoControl.ForeColor = colorTextoBlanco;
            }
            else if (objetoControl is TextBox || objetoControl is ComboBox)
            {
                objetoControl.BackColor = esOscuro ? Color.FromArgb(45, 45, 48) : Color.White;
                objetoControl.ForeColor = esOscuro ? colorTextoBlanco : Color.Black;
            }
            else if (objetoControl is Panel || objetoControl is GroupBox)
            {
                if (objetoControl.BackColor == SystemColors.Control)
                {
                    // Mismo fix que el formulario: blanco en vez de gris de sistema.
                    objetoControl.BackColor = esOscuro ? colorFondoNegro : colorFondoClaro;
                }
            }

            foreach (Control hijo in objetoControl.Controls)
            {
                ProcesarEstiloCapa(hijo, esOscuro);
            }
        }

        /// <summary>
        /// Aplica el estilo de tema a un control de tipo botón, incluyendo soporte para KryptonButton.
        /// </summary>
        /// <param name="objetoControl">Control botón a estilizar.</param>
        /// <param name="esOscuro">True si el modo oscuro está activado.</param>
        private static void AplicarEstiloBoton(Control objetoControl, bool esOscuro)
        {
            if (esOscuro)
            {
                objetoControl.BackColor = Color.White;
                objetoControl.ForeColor = Color.Black;

                if (objetoControl.GetType().Name.Contains("KryptonButton"))
                {
                    try
                    {
                        dynamic kBtn = objetoControl;
                        kBtn.StateCommon.Back.Color1 = Color.White;
                        kBtn.StateCommon.Back.Color2 = Color.White;
                        kBtn.StateCommon.Content.ShortText.Color1 = Color.Black;
                        kBtn.StateTracking.Back.Color1 = Color.White;
                        kBtn.StateTracking.Back.Color2 = Color.White;
                        kBtn.StateTracking.Content.ShortText.Color1 = Color.Black;
                        kBtn.StatePressed.Back.Color1 = Color.White;
                        kBtn.StatePressed.Back.Color2 = Color.White;
                    }
                    catch { }
                }
            }
            else
            {
                objetoControl.BackColor = SystemColors.ControlLight;
                objetoControl.ForeColor = SystemColors.ControlText;

                if (objetoControl.GetType().Name.Contains("KryptonButton"))
                {
                    try
                    {
                        dynamic kBtn = objetoControl;
                        kBtn.StateCommon.Back.Color1 = Color.Empty;
                        kBtn.StateTracking.Back.Color1 = Color.Empty;
                        kBtn.StateCommon.Content.ShortText.Color1 = Color.Empty;
                    }
                    catch { }
                }
            }
        }
    }
}