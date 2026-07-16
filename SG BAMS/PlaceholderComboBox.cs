using Krypton.Toolkit;
using System.Drawing;
using System.Windows.Forms;

namespace SG_BAMS
{
    // ============================================================
    // PlaceholderComboBox — ISP + OCP (corrección de la versión anterior)
    // ============================================================
    // PROBLEMA DETECTADO EN LA REVISIÓN:
    //   PlaceholderComboBox no implementaba IPlaceholder.
    //   Si un formulario declaraba:
    //       private IPlaceholder _phEstado;
    //   no podía asignarle un PlaceholderComboBox, solo un PlaceholderTextBox.
    //   Esto rompía el polimorfismo esperado del ISP.
    //
    // CORRECCIÓN: PlaceholderComboBox ahora implementa IPlaceholder,
    //   igual que PlaceholderTextBox, de modo que los formularios pueden
    //   tratar ambos controles a través de la misma abstracción.
    // ============================================================

    /// <summary>
    /// Adaptador que añade comportamiento de placeholder a un KryptonComboBox.
    /// SRP: única responsabilidad — gestionar el estado de placeholder de un combo.
    /// ISP: implementa IPlaceholder, el mismo contrato que PlaceholderTextBox.
    /// OCP: abierto para extensión; si se necesita soporte para otro combo
    ///      (p.ej. ComboBox nativo), se crea una nueva clase que implemente IPlaceholder.
    /// </summary>
    public class PlaceholderComboBox : IPlaceholder
    {
        private readonly KryptonComboBox _cmb;
        private readonly string _placeholder;
        private bool _isPlaceholderActive;
        private bool _isLoading;

        private static readonly Color ColorPlaceholder = Color.Gray;
        private static readonly Color ColorTextoNormal = Color.Black;

        /// <summary>
        /// Inicializa el adaptador vinculando el KryptonComboBox con su texto de placeholder.
        /// </summary>
        /// <param name="comboBox">Control combo al que se le aplica el placeholder.</param>
        /// <param name="textoGuia">Texto orientativo que se muestra cuando no hay selección.</param>
        public PlaceholderComboBox(KryptonComboBox comboBox, string textoGuia)
        {
            _cmb = comboBox;
            _placeholder = textoGuia;

            _cmb.Enter += Entrar;
            _cmb.Click += Entrar;
            _cmb.KeyDown += OnKeyDown;
            _cmb.Leave += Salir;
            _cmb.SelectedIndexChanged += OnSelectedIndexChanged;
            _cmb.TextUpdate += OnTextUpdate;
            _cmb.DropDown += OnDropDown;
            _cmb.DataSourceChanged += OnDataSourceChanged;

            Activar();
        }

        // ---- IPlaceholder ----

        /// <inheritdoc/>
        public bool IsPlaceholderActive => _isPlaceholderActive;

        /// <inheritdoc/>
        public void Activar()
        {
            _isLoading = true;
            _cmb.SelectedIndex = -1;
            _cmb.Text = _placeholder;
            _cmb.StateCommon.ComboBox.Content.Color1 = ColorPlaceholder;
            _isPlaceholderActive = true;
            _isLoading = false;
        }

        /// <inheritdoc/>
        public string GetRealValue()
        {
            return _isPlaceholderActive ? string.Empty : _cmb.Text;
        }

        // ---- Manejadores de eventos ----

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (_isPlaceholderActive && _cmb.Text == _placeholder)
                LimpiarPlaceholder();
        }

        private void Entrar(object sender, System.EventArgs e)
        {
            if (_isPlaceholderActive && _cmb.Text == _placeholder)
                LimpiarPlaceholder();
        }

        private void Salir(object sender, System.EventArgs e)
        {
            if (_cmb.SelectedIndex == -1 && string.IsNullOrWhiteSpace(_cmb.Text))
                Activar();
            else
                PonerTextoNormal();
        }

        private void OnSelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (_isLoading) return;

            if (_cmb.SelectedIndex != -1)
                PonerTextoNormal();
            else if (string.IsNullOrWhiteSpace(_cmb.Text))
                Activar();
        }

        private void OnTextUpdate(object sender, System.EventArgs e)
        {
            if (_isLoading) return;
            if (_isPlaceholderActive && _cmb.Text != _placeholder)
                PonerTextoNormal();
        }

        private void OnDropDown(object sender, System.EventArgs e)
        {
            if (_isPlaceholderActive && _cmb.Text == _placeholder)
                LimpiarPlaceholder();
        }

        private void OnDataSourceChanged(object sender, System.EventArgs e)
        {
            if (_cmb.DataSource != null && _cmb.Items.Count > 0)
            {
                if (_cmb.SelectedIndex == -1 && string.IsNullOrWhiteSpace(_cmb.Text))
                    Activar();
            }
        }

        // ---- Helpers privados ----

        private void LimpiarPlaceholder()
        {
            _isLoading = true;
            _cmb.Text = string.Empty;
            _cmb.SelectionStart = 0;
            PonerTextoNormal();
            _isPlaceholderActive = false;
            _isLoading = false;
        }

        private void PonerTextoNormal()
        {
            _cmb.StateCommon.ComboBox.Content.Color1 = ColorTextoNormal;
            _isPlaceholderActive = false;
        }
    }
}