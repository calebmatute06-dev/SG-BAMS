using Krypton.Toolkit;
using System.Drawing;
using System.Windows.Forms;



namespace SG_BAMS
{
    public class PlaceholderTextBox
    {
        private KryptonTextBox txt;
        private string placeholder;



        /// <summary>
        /// Inicializa una nueva instancia del placeholder.
        /// </summary>
        /// <param name="textbox">Control al que se aplica el placeholder.</param>
        /// <param name="textoGuia">Texto de ayuda que se muestra cuando está vacío.</param>

        public PlaceholderTextBox(KryptonTextBox textbox, string textoGuia)
        {
            txt = textbox;
            placeholder = textoGuia;


            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                txt.Text = placeholder;
                txt.StateCommon.Content.Color1 = Color.Gray;
            }
            else
            {

                txt.StateCommon.Content.Color1 = Color.Black;
            }

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

            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                txt.Text = placeholder;
                txt.StateCommon.Content.Color1 = Color.Gray;
            }
            else
            {

                txt.StateCommon.Content.Color1 = Color.Black;
            }
        }

        /// <summary>
        /// Obtiene el valor real ingresado por el usuario.
        /// Si el texto actual es igual al placeholder, retorna cadena vacía.
        /// </summary>
        public string GetRealValue()
        {
            return txt.Text == placeholder ? string.Empty : txt.Text;
        }
    }
}
   