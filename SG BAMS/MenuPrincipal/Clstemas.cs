using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;



namespace SG_BAMS.MenuPrincipal
{
    public static class ClsTemas
    {
        /// <summary>
        /// The modo oscuro enabled
        /// </summary>
        public static bool modoOscuroEnabled = false;
        /// <summary>
        /// The ruta archivo
        /// </summary>
        private static string rutaArchivo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "config_tema_krypton_fix.txt");

        /// <summary>
        /// The color fondo negro
        /// </summary>
        private static Color colorFondoNegro = Color.FromArgb(25, 25, 25);
        /// <summary>
        /// The color texto blanco
        /// </summary>
        private static Color colorTextoBlanco = Color.White;

        /// <summary>
        /// Guardars the preferencia.
        /// </summary>
        /// <param name="estado">if set to <c>true</c> [estado].</param>
        public static void GuardarPreferencia(bool estado)
        {
            try { modoOscuroEnabled = estado; File.WriteAllText(rutaArchivo, estado.ToString()); } catch { }
        }

        /// <summary>
        /// Cargars the preferencia.
        /// </summary>
        public static void CargarPreferencia()
        {
            try { if (File.Exists(rutaArchivo)) bool.TryParse(File.ReadAllText(rutaArchivo), out modoOscuroEnabled); } catch { }
        }

        /// <summary>
        /// Aplicars the tema.
        /// </summary>
        /// <param name="formulario">The formulario.</param>
        public static void AplicarTema(Form formulario)
        {
            formulario.BackColor = modoOscuroEnabled ? colorFondoNegro : SystemColors.Control;

            foreach (Control objetoControl in formulario.Controls)
            {
                ProcesarEstiloCapa(objetoControl, modoOscuroEnabled);
            }
        }

        /// <summary>
        /// Procesars the estilo capa.
        /// </summary>
        /// <param name="objetoControl">The objeto control.</param>
        /// <param name="esOscuro">if set to <c>true</c> [es oscuro].</param>
        private static void ProcesarEstiloCapa(Control objetoControl, bool esOscuro)
        {

            if (objetoControl.Name == "kryptonButton11" || objetoControl is Button)
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


            else if (objetoControl is Label || objetoControl is CheckBox || objetoControl is RadioButton)
            {
                objetoControl.ForeColor = esOscuro ? colorTextoBlanco : SystemColors.ControlText;
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
                    objetoControl.BackColor = esOscuro ? colorFondoNegro : SystemColors.Control;
                }
            }

            foreach (Control hijo in objetoControl.Controls)
            {
                ProcesarEstiloCapa(hijo, esOscuro);
            }
        }
    }
}
