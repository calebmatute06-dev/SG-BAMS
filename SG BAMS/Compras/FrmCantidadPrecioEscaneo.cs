using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Ventana emergente que se muestra automáticamente al escanear un
    /// código de barra reconocido desde las pantallas de Ingresar/Modificar
    /// datos de compra, para capturar la cantidad y el precio unitario de
    /// ese producto antes de agregarlo a la lista.
    /// </summary>
    public class FrmCantidadPrecioEscaneo : Form
    {
        public int CantidadResultado { get; private set; }
        public decimal PrecioResultado { get; private set; }

        private readonly NumericUpDown numCantidad;
        private readonly TextBox txtPrecio;
        private readonly Button btnAceptar;
        private readonly Button btnCancelar;

        public FrmCantidadPrecioEscaneo(string nombreProducto)
        {
            Text = "Producto escaneado";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            ClientSize = new Size(340, 215);

            var lblProducto = new Label
            {
                Text = nombreProducto,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(10, 10),
                Size = new Size(320, 40)
            };

            var lblCantidad = new Label { Text = "Cantidad:", Location = new Point(20, 65), AutoSize = true };
            numCantidad = new NumericUpDown
            {
                Location = new Point(140, 62),
                Size = new Size(170, 23),
                Minimum = 1,
                Maximum = 999999,
                Value = 1,
                DecimalPlaces = 0,
                ThousandsSeparator = true
            };

            var lblPrecio = new Label { Text = "Precio unitario:", Location = new Point(20, 102), AutoSize = true };
            txtPrecio = new TextBox
            {
                Location = new Point(140, 99),
                Size = new Size(170, 23),
                TextAlign = HorizontalAlignment.Right
            };
            txtPrecio.KeyPress += TxtPrecio_KeyPress;

            btnAceptar = new Button
            {
                Text = "Aceptar",
                Location = new Point(130, 150),
                Size = new Size(90, 32)
            };
            btnAceptar.Click += BtnAceptar_Click;

            btnCancelar = new Button
            {
                Text = "Cancelar",
                Location = new Point(230, 150),
                Size = new Size(90, 32),
                DialogResult = DialogResult.Cancel
            };

            Controls.Add(lblProducto);
            Controls.Add(lblCantidad);
            Controls.Add(numCantidad);
            Controls.Add(lblPrecio);
            Controls.Add(txtPrecio);
            Controls.Add(btnAceptar);
            Controls.Add(btnCancelar);

            AcceptButton = btnAceptar;
            CancelButton = btnCancelar;

            Shown += (s, e) => txtPrecio.Focus();
        }

        private void TxtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.PermitirNumerosYDecimales(sender, e);
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            if (numCantidad.Value <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor a cero.", "Cantidad inválida",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numCantidad.Focus();
                return;
            }

            string precioTexto = txtPrecio.Text.Trim();
            if (string.IsNullOrWhiteSpace(precioTexto) ||
                !decimal.TryParse(precioTexto, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal precio) ||
                precio <= 0)
            {
                MessageBox.Show("El precio debe ser un valor numérico válido mayor a cero.", "Precio inválido",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecio.Focus();
                return;
            }

            CantidadResultado = (int)numCantidad.Value;
            PrecioResultado = precio;

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}