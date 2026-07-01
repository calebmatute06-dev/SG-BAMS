using Krypton.Toolkit;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;

namespace SG_BAMS.Facturas
{
    /// <summary>
    /// Formulario para registrar baterías viejas y calcular descuentos.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class BateriaVieja : Form
    {
        private double limiteFactura;

        private PlaceholderTextBox phPrecio;
        private PlaceholderTextBox phCantidad;
        private PlaceholderComboBox phBaterias;

        /// <summary>
        /// Obtiene el monto total de descuento por baterías.
        /// </summary>
        public double TotalDineroBateria { get; private set; }

        /// <summary>
        /// Obtiene la cantidad total de baterías.
        /// </summary>
        public string TotalCantidadBateria { get; private set; }

        /// <summary>
        /// Inicializa una nueva instancia del formulario <see cref="BateriaVieja"/>.
        /// </summary>
        /// <param name="montoFactura">Monto total de la factura para validar descuento.</param>
        public BateriaVieja(double montoFactura)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
            this.limiteFactura = montoFactura;
        }

        private void BateriaVieja_Load(object sender, EventArgs e)
        {
            ConfigurarGrid();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;


            cmbBaterias.Items.Clear();
            cmbBaterias.Items.AddRange(new string[] { "Moto", "Carro", "Camión" });
            cmbBaterias.DropDownStyle = ComboBoxStyle.DropDownList;


            phPrecio = new PlaceholderTextBox(txtPrecio, "Precio de la batería");
            phCantidad = new PlaceholderTextBox(txtCantidad, "Cantidad");
            phBaterias = new PlaceholderComboBox(cmbBaterias, "Seleccione tipo");

            txtTotal.ReadOnly = true;
            txtCantidadTotal.ReadOnly = true;


            dgvBateria.BorderStyle = BorderStyle.None;
            dgvBateria.BackgroundColor = Color.White;
            dgvBateria.RowHeadersVisible = false;
            dgvBateria.EnableHeadersVisualStyles = false;
            dgvBateria.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgvBateria.ColumnHeadersDefaultCellStyle.BackColor = Color.SkyBlue;
            dgvBateria.ColumnHeadersDefaultCellStyle.ForeColor = Color.Navy;
            dgvBateria.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvBateria.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvBateria.ColumnHeadersHeight = 28;

            dgvBateria.DefaultCellStyle.BackColor = Color.White;
            dgvBateria.DefaultCellStyle.ForeColor = Color.Navy;
            dgvBateria.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvBateria.DefaultCellStyle.Padding = new Padding(3);
            dgvBateria.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 245, 255);
            dgvBateria.AlternatingRowsDefaultCellStyle.ForeColor = Color.Navy;

            dgvBateria.DefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            dgvBateria.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvBateria.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvBateria.GridColor = Color.LightGray;
            dgvBateria.RowTemplate.Height = 32;
            dgvBateria.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBateria.ClearSelection();
        }

        private void ConfigurarGrid()
        {
            dgvBateria.Columns.Clear();
            dgvBateria.Columns.Add("nombre", "Batería");
            dgvBateria.Columns.Add("precio", "Precio");
            dgvBateria.Columns.Add("cantidad", "Cantidad");
            dgvBateria.Columns.Add("subtotal", "Subtotal");

            dgvBateria.Columns["nombre"].ReadOnly = true;
            dgvBateria.Columns["subtotal"].ReadOnly = true;

            dgvBateria.CellValueChanged += dgvBateria_CellValueChanged;
            dgvBateria.CurrentCellDirtyStateChanged += dgvBateria_CurrentCellDirtyStateChanged;


            dgvBateria.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBateria.AllowUserToAddRows = false;

            dgvBateria.EditingControlShowing += dgvBateria_EditingControlShowing;

            dgvBateria.CellFormatting += (s, ev) =>
            {
                if (ev.RowIndex < 0 || ev.Value == null) return;
                string col = dgvBateria.Columns[ev.ColumnIndex].Name;
                if ((col == "precio" || col == "subtotal"))
                {

                    string puro = ev.Value.ToString().Replace("L.", "").Replace(",", "").Trim();
                    if (decimal.TryParse(puro, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal monto))
                    {
                        ev.Value = $"L. {monto:N2}";
                        ev.FormattingApplied = true;
                    }
                }
            };


        }

        private void CantidadGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.ValidarSoloNumeros(e);
        }

        private void PrecioGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.PermitirNumerosYDecimales(sender, e);
        }

        private void Agregar_Click(object sender, EventArgs e)
        {

            if (phBaterias.IsPlaceholderActive || cmbBaterias.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un tipo de batería.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbBaterias.Focus();
                return;
            }


            string precioReal = phPrecio.GetRealValue().Trim();
            string cantidadReal = phCantidad.GetRealValue().Trim();



            bool precioValido;
            double precio = 0;
            using (var tempPrecio = new KryptonTextBox())
            {
                tempPrecio.Text = precioReal;
                precioValido = !ClsValidaciones.CampoVacio(tempPrecio, "Precio");

                if (precioValido)
                {
                    precio = ClsValidaciones.ParsearMontoInteligente(tempPrecio.Text.Trim());

                    if (precio <= 0)
                    {
                        MessageBox.Show("Precio inválido. Debe ser un número mayor a cero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        precioValido = false;
                    }
                }
            }



            if (!precioValido) return;


            bool cantidadValida;
            int cantidad = 0;
            using (var tempCantidad = new KryptonTextBox())
            {
                tempCantidad.Text = cantidadReal;
                cantidadValida = !ClsValidaciones.CampoVacio(tempCantidad, "Cantidad");
                if (cantidadValida && (!int.TryParse(tempCantidad.Text, out cantidad) || cantidad <= 0))
                {
                    MessageBox.Show("Cantidad inválida. Debe ser un número entero mayor a cero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cantidadValida = false;
                }
            }

            if (!cantidadValida) return;

            double subtotal = Math.Round(precio * cantidad, 2);
            dgvBateria.Rows.Add(cmbBaterias.Text, precio.ToString("N2"), cantidad, subtotal.ToString("N2"));
            CalcularTotales();
            LimpiarCamposEntrada();
        }

        private void LimpiarCamposEntrada()
        {
            cmbBaterias.SelectedIndex = -1;
            txtPrecio.Clear();
            txtCantidad.Clear();
            txtPrecio.Focus();
        }

        private void CalcularTotales()
        {
            double totalDinero = 0;
            int totalProductos = 0;

            foreach (DataGridViewRow row in dgvBateria.Rows)
            {
                if (row.Cells["subtotal"].Value != null)
                {
                    string subtotalStr = row.Cells["subtotal"].Value.ToString().Replace("L.", "").Replace(",", "").Trim();
                    if (double.TryParse(subtotalStr, NumberStyles.Any, CultureInfo.InvariantCulture, out double subtotalVal))
                    {
                        totalDinero += subtotalVal;
                    }
                }

                if (row.Cells["cantidad"].Value != null)
                {
                    int.TryParse(row.Cells["cantidad"].Value.ToString(), out int cantVal);
                    totalProductos += cantVal;
                }
            }

            txtTotal.Text = $"L. {totalDinero:N2}";
            txtCantidadTotal.Text = totalProductos.ToString();
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            if (dgvBateria.Rows.Count == 0)
            {
                MessageBox.Show("No hay baterías en la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string limpio = txtTotal.Text.Replace("L.", "").Replace(",", "").Trim();
            double totalBateria = double.Parse(limpio, CultureInfo.InvariantCulture);

            if (totalBateria >= limiteFactura)
            {
                MessageBox.Show($"El descuento (L. {totalBateria:N2}) no puede ser igual o mayor al total de los productos (L. {limiteFactura:N2}).",
                                "Monto Excedido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            TotalDineroBateria = totalBateria;
            TotalCantidadBateria = txtCantidadTotal.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvBateria.CurrentRow != null)
            {
                dgvBateria.Rows.RemoveAt(dgvBateria.CurrentRow.Index);
                CalcularTotales();
            }
        }

        private void dgvBateria_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvBateria.IsCurrentCellDirty)
            {
                dgvBateria.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvBateria_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvBateria.Columns[e.ColumnIndex].Name == "precio" || dgvBateria.Columns[e.ColumnIndex].Name == "cantidad")
            {
                try
                {
                    var row = dgvBateria.Rows[e.RowIndex];


                    string precioRaw = row.Cells["precio"].Value?.ToString() ?? "0";
                    double precio = ClsValidaciones.ParsearMontoInteligente(precioRaw);

                    string cantidadRaw = row.Cells["cantidad"].Value?.ToString() ?? "0";
                    if (!int.TryParse(cantidadRaw, out int cantidad))
                    {
                        cantidad = 0;
                    }

                    double subtotal = Math.Round(precio * cantidad, 2);

                 
                    dgvBateria.CellValueChanged -= dgvBateria_CellValueChanged;

                    row.Cells["precio"].Value = precio.ToString("N2");
                    row.Cells["subtotal"].Value = subtotal.ToString("N2");

                    dgvBateria.CellValueChanged += dgvBateria_CellValueChanged;

                    CalcularTotales();
                }
                catch
                {
                    dgvBateria.Rows[e.RowIndex].Cells["subtotal"].Value = "0.00";
                    CalcularTotales();
                }
            }
        }
        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.PermitirNumerosYDecimales(sender, e);
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.ValidarSoloNumeros(e);
        }

        private void BtnSalir_Click(object sender, EventArgs e) => this.Close();

        private void dgvBateria_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox txtControl = e.Control as TextBox;

            if (dgvBateria.CurrentCell?.OwningColumn.Name == "cantidad")
            {
                if (txtControl != null)
                {
                    txtControl.KeyPress -= CantidadGrid_KeyPress;
                    txtControl.KeyPress += CantidadGrid_KeyPress;
                }
            }
            else if (dgvBateria.CurrentCell?.OwningColumn.Name == "precio")
            {
                if (txtControl != null)
                {
                    // Quitamos el formato visual para que edites el número limpio
                    txtControl.Text = txtControl.Text.Replace("L.", "").Replace(",", "").Trim();

                    txtControl.KeyPress -= PrecioGrid_KeyPress;
                    txtControl.KeyPress += PrecioGrid_KeyPress;
                }
            }
        }
    }
}