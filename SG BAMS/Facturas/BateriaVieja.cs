using Krypton.Toolkit;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;

namespace SG_BAMS.Facturas
{
    public partial class BateriaVieja : Form
    {
       
        public double TotalDineroBateria { get; private set; }
        public string TotalCantidadBateria { get; private set; }

        private double limiteFactura;

        public BateriaVieja(double montoFactura)
        {
            
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            InitializeComponent();
            this.limiteFactura = montoFactura;
        }

        private void BateriaVieja_Load(object sender, EventArgs e)
        {
            ConfigurarGrid();

            
            cmbBaterias.Items.Clear();
            cmbBaterias.Items.AddRange(new string[] { "Moto", "Carro", "Camión" });
            cmbBaterias.DropDownStyle = ComboBoxStyle.DropDownList;

            
            txtTotal.ReadOnly = true;
            txtCantidadTotal.ReadOnly = true;
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
            dgvBateria.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBateria.AllowUserToAddRows = false;
        }

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
                totalDinero += Convert.ToDouble(row.Cells["subtotal"].Value);
                totalProductos += Convert.ToInt32(row.Cells["cantidad"].Value);
            }

            txtTotal.Text = totalDinero.ToString("N2");
            txtCantidadTotal.Text = totalProductos.ToString();
        }

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

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvBateria.CurrentRow != null)
            {
                dgvBateria.Rows.RemoveAt(dgvBateria.CurrentRow.Index);
                CalcularTotales();
            }
        }


        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.ValidarSoloNumeros(e);
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) e.Handled = true;
        }

        private void BtnSalir_Click(object sender, EventArgs e) => this.Close();
    }
}