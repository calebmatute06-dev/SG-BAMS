using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace SG_BAMS.Facturas
{
    public partial class BateriaVieja : Form
    {
        public BateriaVieja()
        {
            InitializeComponent();
        }

        public double TotalDineroBateria { get; private set; }
        public string TotalCantidadBateria { get; private set; }

        private double limiteFactura;

        public BateriaVieja(double montoFactura)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            InitializeComponent();
            this.limiteFactura = montoFactura;
        }

        private void BateriaVieja_Load(object sender, EventArgs e)
        {
            txtPrecio.KeyPress += txtPrecio_KeyPress;
            txtCantidad.KeyPress += txtCantidad_KeyPress;
            dgvBateria.Columns.Clear();

            dgvBateria.Columns.Add("nombre", "Batería");
            dgvBateria.Columns.Add("precio", "Precio");
            dgvBateria.Columns.Add("cantidad", "Cantidad");
            dgvBateria.Columns.Add("subtotal", "Subtotal");

            cmbBaterias.Items.Add("Moto");
            cmbBaterias.Items.Add("Carro");
            cmbBaterias.Items.Add("Camión");

            dgvBateria.Columns["nombre"].ReadOnly = true;
            dgvBateria.Columns["subtotal"].ReadOnly = true;
            dgvBateria.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            cmbBaterias.DropDownStyle = ComboBoxStyle.DropDownList;
            txtTotal.ReadOnly = true;
            txtCantidadTotal.ReadOnly = true;
            dgvBateria.AllowUserToAddRows = false;



        }

        private void Agregar_Click(object sender, EventArgs e)
        {

            if (cmbBaterias.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, selecciona un tipo de batería.", "Dato faltante", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbBaterias.Focus();
                return;
            }


            if (string.IsNullOrWhiteSpace(txtPrecio.Text) || string.IsNullOrWhiteSpace(txtCantidad.Text))
            {
                MessageBox.Show("Por favor, completa los campos de Precio y Cantidad.", "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string precioTexto = txtPrecio.Text.Replace(",", "");

            if (!double.TryParse(txtPrecio.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out double precio) || precio <= 0)
            {
               
                if (!double.TryParse(txtPrecio.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out precio) || precio <= 0)
                {
                    MessageBox.Show("El precio debe ser un número mayor a 0", "Valor inválido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPrecio.Focus();
                    return;
                }
            }

            if (!int.TryParse(txtCantidad.Text, out int cant) || cant <= 0)
            {
                MessageBox.Show("La cantidad debe ser un número entero mayor a 0", "Valor inválido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCantidad.Focus();
                return;
            }

            double subtotal = Math.Round(precio * cant, 2);

            dgvBateria.Rows.Add( cmbBaterias.Text, precio.ToString("N2", CultureInfo.InvariantCulture), cant, subtotal.ToString("N2", CultureInfo.InvariantCulture) );

            CalcularTotales();

            cmbBaterias.SelectedIndex = -1;
            txtPrecio.Clear();
            txtCantidad.Clear();
            txtPrecio.Focus();
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

            string columnName = dgvBateria.Columns[e.ColumnIndex].Name;

            if (columnName == "precio" || columnName == "cantidad")
            {
                var row = dgvBateria.Rows[e.RowIndex];

                double precio = Convert.ToDouble(row.Cells["precio"].Value ?? 0);
                double cantidad = Convert.ToDouble(row.Cells["cantidad"].Value ?? 0);
                double subtotal = Math.Round(precio * cantidad, 2);
                row.Cells["subtotal"].Value = subtotal;

                CalcularTotales();
            }

        }

        private void dgvBateria_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgvBateria.Rows[e.RowIndex].IsNewRow) return;

            string columnName = dgvBateria.Columns[e.ColumnIndex].Name;


            if (columnName == "precio" || columnName == "cantidad")
            {
                string input = e.FormattedValue.ToString();


                if (!double.TryParse(input, out double valor) || valor <= 0)
                {
                    MessageBox.Show("Debe ingresar un valor numérico mayor a 0 en la tabla.",
                                    "Dato Erróneo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    e.Cancel = true;
                }
            }
        }

        private void dgvBateria_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {


            string name = dgvBateria.Columns[dgvBateria.CurrentCell.ColumnIndex].Name;

            if ((name == "precio" || name == "cantidad") && e.Control is TextBox txt)
            {
                txt.ShortcutsEnabled = false;
                txt.KeyPress -= SoloNumeros_Handler;

                txt.KeyPress += SoloNumeros_Handler;
            }
        }

        private void SoloNumeros_Handler(object sender, KeyPressEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb == null) return;

            string name = dgvBateria.Columns[dgvBateria.CurrentCell.ColumnIndex].Name;

            if (char.IsControl(e.KeyChar)) return;

            if (char.IsDigit(e.KeyChar)) return;

            if (name == "precio" && e.KeyChar == '.')
            {
                if (tb.Text.Contains("."))
                {
                    e.Handled = true;
                }
                return;
            }

            if (name == "cantidad")
            {
                e.Handled = true;
                return;
            }

            e.Handled = true;
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            if (dgvBateria.Rows.Count == 0 || (dgvBateria.Rows.Count == 1 && dgvBateria.Rows[0].IsNewRow))
            {
                MessageBox.Show("No hay baterías registradas en la lista.", "Lista vacía", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double totalBateria = 0;
            if (!double.TryParse(txtTotal.Text, out totalBateria)) totalBateria = 0;

            if (totalBateria > limiteFactura)
            {
                MessageBox.Show($"El descuento por baterías (L. {totalBateria:N2}) supera el total de la factura (L. {limiteFactura:N2}).\n\nNo se puede aplicar un descuento mayor a la compra.",
                                "Monto Excedido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (totalBateria == limiteFactura)
            {
                MessageBox.Show($"El descuento por baterías (L. {totalBateria:N2}) es igual al total de la factura (L. {limiteFactura:N2}).\n\nNo se puede aplicar el descuento",
                                "Monto Igual", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            TotalDineroBateria = totalBateria;
            TotalCantidadBateria = txtCantidadTotal.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CalcularTotales()
        {
            double totalDinero = 0;
            int totalProductos = 0;

            foreach (DataGridViewRow row in dgvBateria.Rows)
            {
                if (row.Cells["subtotal"].Value != null)
                {
                    
                    totalDinero += Convert.ToDouble(row.Cells["subtotal"].Value, CultureInfo.InvariantCulture);
                }

                if (row.Cells["cantidad"].Value != null)
                {
                    totalProductos += Convert.ToInt32(row.Cells["cantidad"].Value);
                }
            }

          
            txtTotal.Text = totalDinero.ToString("N2", CultureInfo.InvariantCulture);
            txtCantidadTotal.Text = totalProductos.ToString();
        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvBateria.CurrentRow != null && !dgvBateria.CurrentRow.IsNewRow)
            {

                dgvBateria.Rows.RemoveAt(dgvBateria.CurrentRow.Index);


                CalcularTotales();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila válida para eliminar.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb == null) return;

            if (char.IsControl(e.KeyChar)) return;

            if (char.IsDigit(e.KeyChar)) return;

            if (e.KeyChar == '.')
            {
                if (tb.Text.Contains("."))
                {
                    e.Handled = true;
                }
                return;
            }

            e.Handled = true;
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;

            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
