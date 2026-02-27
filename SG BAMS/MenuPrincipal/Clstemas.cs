using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

public static class ClsTemas
{
    public static bool modoOscuroEnabled = false;
    private static string rutaArchivo = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "config_tema_krypton_fix.txt");

    private static Color colorFondoNegro = Color.FromArgb(25, 25, 25);
    private static Color colorTextoBlanco = Color.White;

    public static void GuardarPreferencia(bool estado)
    {
        try { modoOscuroEnabled = estado; File.WriteAllText(rutaArchivo, estado.ToString()); } catch { }
    }

    public static void CargarPreferencia()
    {
        try { if (File.Exists(rutaArchivo)) bool.TryParse(File.ReadAllText(rutaArchivo), out modoOscuroEnabled); } catch { }
    }

    public static void AplicarTema(Form formulario)
    {
        formulario.BackColor = modoOscuroEnabled ? colorFondoNegro : SystemColors.Control;

        foreach (Control objetoControl in formulario.Controls)
        {
            ProcesarEstiloCapa(objetoControl, modoOscuroEnabled);
        }
    }

    private static void ProcesarEstiloCapa(Control objetoControl, bool esOscuro)
    {
        // --- 1. BOTONES (ESTÁNDAR Y KRYPTONBUTTON11) ---
        if (objetoControl.Name == "kryptonButton11" || objetoControl is Button)
        {
            if (esOscuro)
            {
                objetoControl.BackColor = Color.White;
                objetoControl.ForeColor = Color.Black;

                // Si es el botón de Krypton, forzamos blanco en TODOS sus estados
                if (objetoControl.GetType().Name.Contains("KryptonButton"))
                {
                    try
                    {
                        dynamic kBtn = objetoControl;
                        // Color Normal
                        kBtn.StateCommon.Back.Color1 = Color.White;
                        kBtn.StateCommon.Back.Color2 = Color.White;
                        kBtn.StateCommon.Content.ShortText.Color1 = Color.Black;

                        // Color al pasar el mouse (Tracking) - Forzamos Blanco también
                        kBtn.StateTracking.Back.Color1 = Color.White;
                        kBtn.StateTracking.Back.Color2 = Color.White;
                        kBtn.StateTracking.Content.ShortText.Color1 = Color.Black;

                        // Color al hacer clic (Pressed)
                        kBtn.StatePressed.Back.Color1 = Color.White;
                        kBtn.StatePressed.Back.Color2 = Color.White;
                    }
                    catch { }
                }
            }
            else
            {
                // Modo Claro: Devolvemos a la normalidad o colores estándar
                objetoControl.BackColor = SystemColors.ControlLight;
                objetoControl.ForeColor = SystemColors.ControlText;

                if (objetoControl.GetType().Name.Contains("KryptonButton"))
                {
                    try
                    {
                        dynamic kBtn = objetoControl;
                        // Al poner Color.Empty, Krypton vuelve a usar su paleta original
                        kBtn.StateCommon.Back.Color1 = Color.Empty;
                        kBtn.StateTracking.Back.Color1 = Color.Empty;
                        kBtn.StateCommon.Content.ShortText.Color1 = Color.Empty;
                    }
                    catch { }
                }
            }
        }

        // --- 2. TEXTOS (Labels, CheckBox, Radio) ---
        else if (objetoControl is Label || objetoControl is CheckBox || objetoControl is RadioButton)
        {
            objetoControl.ForeColor = esOscuro ? colorTextoBlanco : SystemColors.ControlText;
        }

        // --- 3. CAJAS DE TEXTO Y COMBOS ---
        else if (objetoControl is TextBox || objetoControl is ComboBox)
        {
            objetoControl.BackColor = esOscuro ? Color.FromArgb(45, 45, 48) : Color.White;
            objetoControl.ForeColor = esOscuro ? colorTextoBlanco : Color.Black;
        }

        // --- 4. PANELES ---
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