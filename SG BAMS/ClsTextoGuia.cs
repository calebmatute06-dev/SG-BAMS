
using Krypton.Toolkit;
using System.Drawing;
using System.Windows.Forms;

public class PlaceholderTextBox
{
    private KryptonTextBox txt;
    private string placeholder;

    public PlaceholderTextBox(KryptonTextBox textbox, string textoGuia)
    {
        txt = textbox;
        placeholder = textoGuia;

        // Poner texto inicial
        txt.Text = placeholder;
        txt.StateCommon.Content.Color1 = Color.Gray;

        // Conectar eventos
        txt.Enter += Entrar;
        txt.Leave += Salir;
    }

    private void Entrar(object sender, System.EventArgs e)
    {
        if (txt.Text == placeholder)
        {
            txt.Text = "";
            txt.StateCommon.Content.Color1 = Color.Black;
        }
    }

    private void Salir(object sender, System.EventArgs e)
    {
        if (txt.Text == "")
        {
            txt.Text = placeholder;
            txt.StateCommon.Content.Color1 = Color.Gray;
        }
    }
}