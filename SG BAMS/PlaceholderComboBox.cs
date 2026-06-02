using Krypton.Toolkit;
using System.Drawing;
using System.Windows.Forms;

/// <summary>
/// Agrega comportamiento de placeholder a un KryptonComboBox.
/// </summary>
public class PlaceholderComboBox
{
    private KryptonComboBox cmb;
    private string placeholder;
    private bool isPlaceholderActive;
    private bool isLoading = false;

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
        cmb.DataSourceChanged += OnDataSourceChanged;
    }

    private void Entrar(object sender, System.EventArgs e)
    {
        if (isPlaceholderActive && cmb.Text == placeholder)
        {
            isLoading = true;
            cmb.Text = "";
            cmb.StateCommon.ComboBox.Content.Color1 = Color.Black;
            isPlaceholderActive = false;
            isLoading = false;
        }
    }

    private void Salir(object sender, System.EventArgs e)
    {
        if (cmb.SelectedIndex == -1 && string.IsNullOrWhiteSpace(cmb.Text))
        {
            isLoading = true;
            cmb.Text = placeholder;
            cmb.StateCommon.ComboBox.Content.Color1 = Color.Gray;
            isPlaceholderActive = true;
            isLoading = false;
        }
        else
        {
            cmb.StateCommon.ComboBox.Content.Color1 = Color.Black;
            isPlaceholderActive = false;
        }
    }

    private void OnSelectedIndexChanged(object sender, System.EventArgs e)
    {
        if (isLoading) return;
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
                isLoading = true;
                cmb.Text = placeholder;
                cmb.StateCommon.ComboBox.Content.Color1 = Color.Gray;
                isPlaceholderActive = true;
                isLoading = false;
            }
        }
    }

    private void OnTextUpdate(object sender, System.EventArgs e)
    {
        if (isLoading) return;
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
            isLoading = true;
            cmb.Text = "";
            cmb.StateCommon.ComboBox.Content.Color1 = Color.Black;
            isPlaceholderActive = false;
            isLoading = false;
        }
    }

    private void OnDataSourceChanged(object sender, System.EventArgs e)
    {
        if (cmb.DataSource != null && cmb.Items.Count > 0)
        {
            if (cmb.SelectedIndex == -1 && string.IsNullOrWhiteSpace(cmb.Text))
            {
                isLoading = true;
                cmb.Text = placeholder;
                cmb.StateCommon.ComboBox.Content.Color1 = Color.Gray;
                isPlaceholderActive = true;
                isLoading = false;
            }
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