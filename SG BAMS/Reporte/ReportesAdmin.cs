using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS.Reporte
{
    public partial class ReportesAdmin : Form
    {

        ClsReportesDatos objReporte = new ClsReportesDatos();


        public ReportesAdmin()
        {
            InitializeComponent();
        }

        private void ReportesAdmin_Load(object sender, EventArgs e)
        {

        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            try
            {
                // Llamamos al método de ventas pasando las fechas de los controles DateTimePicker
                // Asumo que tus controles se llaman dtpDesde y dtpHasta
                DataTable datos = objReporte.ReporteVentas(dtpDesde.Value, dtpHasta.Value);

                // Asignamos el resultado al DataGridView
                dgvReporte.DataSource = datos;

                // Opcional: Ajustar el ancho de las columnas automáticamente al contenido
                dgvReporte.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el reporte de ventas: " + ex.Message, "Error BAMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCompras_Click(object sender, EventArgs e)
        {
            try
            {
                // Llamamos al método de compras pasando los valores de tus DateTimePicker
                DataTable datos = objReporte.ReporteCompras(dtpDesde.Value, dtpHasta.Value);

                dgvReporte.DataSource = datos;
                dgvReporte.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el reporte de compras: " + ex.Message, "Error BAMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeudores_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable datos = objReporte.ReporteDeudores();
                dgvReporte.DataSource = datos;

                // 2. Quitamos los guiones bajos de las etiquetas (HeaderText)
                if (dgvReporte.Columns.Contains("Fecha_Inicio"))
                    dgvReporte.Columns["Fecha_Inicio"].HeaderText = "Fecha de Inicio";

                if (dgvReporte.Columns.Contains("Monto_Credito"))
                    dgvReporte.Columns["Monto_Credito"].HeaderText = "Monto Crédito";

                if (dgvReporte.Columns.Contains("Saldo_Pendiente"))
                    dgvReporte.Columns["Saldo_Pendiente"].HeaderText = "Saldo a Cobrar";

                // 3. Ordenamos automáticamente de mayor a menor deuda
                if (dgvReporte.Columns.Contains("Saldo_Pendiente"))
                {
                    dgvReporte.Sort(dgvReporte.Columns["Saldo_Pendiente"], System.ComponentModel.ListSortDirection.Descending);
                }

                dgvReporte.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar deudores: " + ex.Message, "BAMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            try
            {
                dgvReporte.DataSource = objReporte.ReporteInventario();

                if (dgvReporte.Columns.Contains("Stock_Actual"))
                {
                    dgvReporte.Columns["Stock_Actual"].HeaderText = "Stock Actual";
                }
                if (dgvReporte.Columns.Contains("Precio_Unitario"))
                {
                    dgvReporte.Columns["Precio_Unitario"].HeaderText = "Precio Venta";
                }

                dgvReporte.Sort(dgvReporte.Columns["Stock_Actual"], System.ComponentModel.ListSortDirection.Descending);
                dgvReporte.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void dgvReporte_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvReporte_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvReporte.Columns[e.ColumnIndex].Name == "Stock_Actual" && e.Value != null)
            {
                // Convertimos el valor de la celda a número entero
                if (int.TryParse(e.Value.ToString(), out int stock))
                {
                    if (stock == 0) // ROJO: Sin existencias
                    {
                        e.CellStyle.BackColor = Color.FromArgb(255, 192, 192);
                        e.CellStyle.ForeColor = Color.DarkRed;
                    }
                    else if (stock <= 10) // NARANJA: Stock bajo
                    {
                        e.CellStyle.BackColor = Color.FromArgb(255, 224, 192);
                        e.CellStyle.ForeColor = Color.Brown;
                    }
                    else // VERDE: Stock suficiente
                    {
                        e.CellStyle.BackColor = Color.FromArgb(192, 255, 192);
                        e.CellStyle.ForeColor = Color.DarkGreen;
                    }
                }
            }
        }
    }
}
