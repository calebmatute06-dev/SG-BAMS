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
            InitializeComponent();
            this.limiteFactura = montoFactura;
        }

        private void BateriaVieja_Load(object sender, EventArgs e)
        {
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
            cmbBaterias.DropDownStyle = ComboBoxStyle.DropDownList;
            txtTotal.ReadOnly = true;
            txtCantidadTotal.ReadOnly = true;



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


            if (!double.TryParse(txtPrecio.Text, out double precio) || precio <= 0)
            {
                MessageBox.Show("El precio debe ser un número mayor a 0", "Valor inválido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPrecio.Focus();
                return;
            }

            if (!int.TryParse(txtCantidad.Text, out int cant) || cant <= 0)
            {
                MessageBox.Show("La cantidad debe ser un número entero mayor a 0", "Valor inválido", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCantidad.Focus();
                return;
            }


            dgvBateria.Rows.Add(cmbBaterias.Text, precio, cant, (precio * cant));

            CalcularTotales();

            dgvBateria.Columns["nombre"].ReadOnly = true;

            dgvBateria.Columns["precio"].ReadOnly = false;
            dgvBateria.Columns["cantidad"].ReadOnly = false;



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
                row.Cells["subtotal"].Value = precio * cantidad;

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
            string name = dgvBateria.Columns[dgvBateria.CurrentCell.ColumnIndex].Name;

            if (char.IsControl(e.KeyChar)) return;
            if (char.IsDigit(e.KeyChar)) return;

            if (name == "precio" && e.KeyChar == ',')
            {

                if (tb.Text.Contains(","))
                {
                    e.Handled = true;
                }
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
                MessageBox.Show($"La rebaja por baterías (L. {totalBateria:N2}) supera el total de la factura (L. {limiteFactura:N2}).\n\nNo se puede aplicar una rebaja mayor a la compra.",
                                "Monto Excedido", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    totalDinero += Convert.ToDouble(row.Cells["subtotal"].Value);
                }


                if (row.Cells["cantidad"].Value != null)
                {
                    totalProductos += Convert.ToInt32(row.Cells["cantidad"].Value);
                }
            }


            txtTotal.Text = totalDinero.ToString("N2");
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
    }
}
