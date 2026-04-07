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
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class BateriaVieja : Form
    {
        /// <summary>
        /// Gets the total dinero bateria.
        /// </summary>
        /// <value>
        /// The total dinero bateria.
        /// </value>
        public double TotalDineroBateria { get; private set; }
        /// <summary>
        /// Gets the total cantidad bateria.
        /// </summary>
        /// <value>
        /// The total cantidad bateria.
        /// </value>
        public string TotalCantidadBateria { get; private set; }

        /// <summary>
        /// The limite factura
        /// </summary>
        private double limiteFactura;

        /// <summary>
        /// Initializes a new instance of the <see cref="BateriaVieja"/> class.
        /// </summary>
        /// <param name="montoFactura">The monto factura.</param>
        public BateriaVieja(double montoFactura)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            InitializeComponent();
            this.limiteFactura = montoFactura;
        }

        /// <summary>
        /// Handles the Load event of the BateriaVieja control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BateriaVieja_Load(object sender, EventArgs e)
        {
            ConfigurarGrid();

            cmbBaterias.Items.Clear();
            cmbBaterias.Items.AddRange(new string[] { "Moto", "Carro", "Camión" });
            cmbBaterias.DropDownStyle = ComboBoxStyle.DropDownList;

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

        /// <summary>
        /// Configurars the grid.
        /// </summary>
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
        }

        /// <summary>
        /// Handles the Click event of the Agregar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Agregar_Click(object sender, EventArgs e)
        {
            if (cmbBaterias.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un tipo de batería.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (ClsValidaciones.CampoVacio(txtPrecio, "Precio") || ClsValidaciones.CampoVacio(txtCantidad, "Cantidad")) return;

            if (!double.TryParse(txtPrecio.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out double precio) || precio <= 0)
            {
                MessageBox.Show("Precio inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(txtCantidad.Text, out int cant) || cant <= 0)
            {
                MessageBox.Show("Cantidad inválida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            double subtotal = Math.Round(precio * cant, 2);
            dgvBateria.Rows.Add(cmbBaterias.Text, precio.ToString("N2"), cant, subtotal.ToString("N2"));

            CalcularTotales();
            LimpiarCamposEntrada();
        }

        /// <summary>
        /// Limpiars the campos entrada.
        /// </summary>
        private void LimpiarCamposEntrada()
        {
            cmbBaterias.SelectedIndex = -1;
            txtPrecio.Clear();
            txtCantidad.Clear();
            txtPrecio.Focus();
        }

        /// <summary>
        /// Calculars the totales.
        /// </summary>
        private void CalcularTotales()
        {
            double totalDinero = 0;
            int totalProductos = 0;

            foreach (DataGridViewRow row in dgvBateria.Rows)
            {
                if (row.Cells["subtotal"].Value != null)
                    totalDinero += Convert.ToDouble(row.Cells["subtotal"].Value);

                if (row.Cells["cantidad"].Value != null)
                    totalProductos += Convert.ToInt32(row.Cells["cantidad"].Value);
            }

            txtTotal.Text = totalDinero.ToString("N2");
            txtCantidadTotal.Text = totalProductos.ToString();
        }

        /// <summary>
        /// Handles the Click event of the BtnAceptar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            if (dgvBateria.Rows.Count == 0)
            {
                MessageBox.Show("No hay baterías en la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double totalBateria = double.Parse(txtTotal.Text);

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

        /// <summary>
        /// Handles the Click event of the BtnEliminar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvBateria.CurrentRow != null)
            {
                dgvBateria.Rows.RemoveAt(dgvBateria.CurrentRow.Index);
                CalcularTotales();
            }
        }



        /// <summary>
        /// Handles the CurrentCellDirtyStateChanged event of the dgvBateria control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void dgvBateria_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {

            if (dgvBateria.IsCurrentCellDirty)
            {
                dgvBateria.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        /// <summary>
        /// Handles the CellValueChanged event of the dgvBateria control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dgvBateria_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;


            if (dgvBateria.Columns[e.ColumnIndex].Name == "precio" || dgvBateria.Columns[e.ColumnIndex].Name == "cantidad")
            {
                try
                {
                    var row = dgvBateria.Rows[e.RowIndex];
                    double precio = Convert.ToDouble(row.Cells["precio"].Value ?? 0);
                    int cantidad = Convert.ToInt32(row.Cells["cantidad"].Value ?? 0);

                    double subtotal = Math.Round(precio * cantidad, 2);
                    row.Cells["subtotal"].Value = subtotal.ToString("N2");

                    CalcularTotales();
                }
                catch
                {
                    dgvBateria.Rows[e.RowIndex].Cells["subtotal"].Value = "0.00";
                    CalcularTotales();
                }
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the txtPrecio control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.PermitirNumerosYDecimales(sender, e);
        }

        /// <summary>
        /// Handles the KeyPress event of the txtCantidad control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.ValidarSoloNumeros(e);
        }

        /// <summary>
        /// Handles the Click event of the BtnSalir control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BtnSalir_Click(object sender, EventArgs e) => this.Close();
    }
}