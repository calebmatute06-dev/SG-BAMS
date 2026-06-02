using Krypton.Toolkit;
using System.Drawing;
using System.Windows.Forms;

/// <summary>
/// Implementa un comportamiento de placeholder para un KryptonComboBox.
/// </summary>
public class PlaceholderComboBox
{
    private KryptonComboBox cmb;
    private string placeholder;
    private bool isPlaceholderActive;

    /// <summary>
    /// Inicializa una nueva instancia del placeholder para ComboBox.
    /// </summary>
    /// <param name="comboBox">Control al que se aplica el placeholder.</param>
    /// <param name="textoGuia">Texto de ayuda que se muestra cuando está vacío y sin selección.</param>
    public PlaceholderComboBox(KryptonComboBox comboBox, string textoGuia)
    {
        cmb = comboBox;
        placeholder = textoGuia;

        
        if (string.IsNullOrWhiteSpace(cmb.Text) && cmb.SelectedIndex == -1)
        {
            cmb.Text = placeholder;
            cmb.StateCommon.ComboBox.Content.Color1 = Color.Gray;
            isPlaceholderActive = true;
        }
        else
        {
            cmb.StateCommon.ComboBox.Content.Color1 = Color.Black;
            isPlaceholderActive = false;
        }

        
        cmb.Enter += Entrar;
        cmb.Leave += Salir;
        cmb.SelectedIndexChanged += OnSelectedIndexChanged;
        cmb.TextUpdate += OnTextUpdate;
        cmb.DropDown += OnDropDown;
    }

    private void Entrar(object sender, System.EventArgs e)
    {
        if (isPlaceholderActive && cmb.Text == placeholder)
        {
            cmb.Text = "";
            cmb.StateCommon.ComboBox.Content.Color1 = Color.Black;
            isPlaceholderActive = false;
        }
    }

    private void Salir(object sender, System.EventArgs e)
    {
        if (cmb.SelectedIndex == -1 && string.IsNullOrWhiteSpace(cmb.Text))
        {
            cmb.Text = placeholder;
            cmb.StateCommon.ComboBox.Content.Color1 = Color.Gray;
            isPlaceholderActive = true;
        }
        else
        {
            cmb.StateCommon.ComboBox.Content.Color1 = Color.Black;
            isPlaceholderActive = false;
        }
    }

    private void OnSelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (cmb.SelectedIndex != -1)
        {
          
            if (isPlaceholderActive)
            {
                cmb.StateCommon.ComboBox.Content.Color1 = Color.Black;
                isPlaceholderActive = false;
            }
        }
        else
        {
            
            if (string.IsNullOrWhiteSpace(cmb.Text))
            {
                cmb.Text = placeholder;
                cmb.StateCommon.ComboBox.Content.Color1 = Color.Gray;
                isPlaceholderActive = true;
            }
        }
    }

    private void OnTextUpdate(object sender, System.EventArgs e)
    {
        if (isPlaceholderActive && cmb.Text != placeholder)
        {
            isPlaceholderActive = false;
            cmb.StateCommon.ComboBox.Content.Color1 = Color.Black;
        }
    }

    private void OnDropDown(object sender, System.EventArgs e)
    {
      
        if (isPlaceholderActive && cmb.Text == placeholder)
        {
            cmb.Text = "";
            cmb.StateCommon.ComboBox.Content.Color1 = Color.Black;
            isPlaceholderActive = false;
        }
    }

    /// <summary>
    /// Devuelve el valor real del ComboBox (sin placeholder).
    /// </summary>
    public string GetRealValue()
    {
        return isPlaceholderActive ? string.Empty : cmb.Text;
    }

    /// <summary>
    /// Indica si el placeholder está activo.
    /// </summary>
    public bool IsPlaceholderActive => isPlaceholderActive;
}