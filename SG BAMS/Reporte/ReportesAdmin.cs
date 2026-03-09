using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
            dtpHasta.Value = DateTime.Now;
            dtpDesde.Value = DateTime.Now.AddDays(-30);

            CargarReporteVentas();
        }

        private void CargarReporteVentas()
        {
            try
            {
                DataTable datos = objReporte.ReporteVentas(dtpDesde.Value, dtpHasta.Value);
                dgvReporte.DataSource = datos;

                if (dgvReporte.Columns.Contains("Telefono"))
                {
                    dgvReporte.Columns["Telefono"].HeaderText = "Teléfono";
                }

                if (dgvReporte.Columns.Contains("Metodo_Pago"))
                {
                    dgvReporte.Columns["Metodo_Pago"].HeaderText = "Método de Pago";
                }

                if (dgvReporte.Columns.Contains("Total_Venta"))
                {
                    dgvReporte.Columns["Total_Venta"].HeaderText = "Total";
                }

                if (dgvReporte.Columns.Contains("Recibio_Chatarra"))
                {
                    dgvReporte.Columns["Recibio_Chatarra"].HeaderText = "Bateria Vieja";
                }

                dgvReporte.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el reporte de ventas: " + ex.Message, "Error BAMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable datos = objReporte.ReporteVentas(dtpDesde.Value, dtpHasta.Value);
                dgvReporte.DataSource = datos;

                if (dgvReporte.Columns.Contains("Telefono"))
                {
                    dgvReporte.Columns["Telefono"].HeaderText = "Teléfono";
                }

                if (dgvReporte.Columns.Contains("Metodo_Pago"))
                {
                    dgvReporte.Columns["Metodo_Pago"].HeaderText = "Método de Pago";
                }

                if (dgvReporte.Columns.Contains("Total_Venta"))
                {
                    dgvReporte.Columns["Total_Venta"].HeaderText = "Total";
                }

                if (dgvReporte.Columns.Contains("Recibio_Chatarra"))
                {
                    dgvReporte.Columns["Recibio_Chatarra"].HeaderText = "Bateria Vieja";
                }

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
                DataTable datos = objReporte.ReporteCompras(dtpDesde.Value, dtpHasta.Value);
                dgvReporte.DataSource = datos;

                if (dgvReporte.Columns.Contains("Inversion_Total"))
                {
                    dgvReporte.Columns["Inversion_Total"].HeaderText = "Total";
                }

                if (dgvReporte.Columns.Contains("RTN_Proveedor"))
                {
                    dgvReporte.Columns["RTN_Proveedor"].HeaderText = "RTN";
                }

                if (dgvReporte.Columns.Contains("Telefono_Proveedor"))
                {
                    dgvReporte.Columns["Telefono_Proveedor"].HeaderText = "Teléfono";
                }

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

                if (dgvReporte.Columns.Contains("Fecha_Inicio"))
                    dgvReporte.Columns["Fecha_Inicio"].HeaderText = "Fecha de Inicio";

                if (dgvReporte.Columns.Contains("Monto_Credito"))
                    dgvReporte.Columns["Monto_Credito"].HeaderText = "Monto Deuda";

                if (dgvReporte.Columns.Contains("Saldo_Pendiente"))
                    dgvReporte.Columns["Saldo_Pendiente"].HeaderText = "Saldo a Cobrar";

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
                if (int.TryParse(e.Value.ToString(), out int stock))
                {
                    if (stock == 0)
                    {
                        // Especificamos la ruta completa del color
                        e.CellStyle.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
                        e.CellStyle.ForeColor = System.Drawing.Color.DarkRed;
                    }
                    else if (stock <= 10)
                    {
                        e.CellStyle.BackColor = System.Drawing.Color.FromArgb(255, 224, 192);
                        e.CellStyle.ForeColor = System.Drawing.Color.Brown;
                    }
                    else
                    {
                        e.CellStyle.BackColor = System.Drawing.Color.FromArgb(192, 255, 192);
                        e.CellStyle.ForeColor = System.Drawing.Color.DarkGreen;
                    }
                }
            }
        }

        private void btnExportaar_Click(object sender, EventArgs e)
        {
            try
            {
                QuestPDF.Settings.License = LicenseType.Community;

                if (dgvReporte.Rows.Count == 0)
                {
                    MessageBox.Show("No hay datos para exportar.", "BAMS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string nombreArchivo = $"Reporte_BAMS_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                string rutaTemp = Path.Combine(Path.GetTempPath(), nombreArchivo);

                var documento = new DocumentoDinamico(dgvReporte);
                documento.GeneratePdf(rutaTemp);

                Process.Start(new ProcessStartInfo
                {
                    FileName = rutaTemp,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el reporte PDF: " + ex.Message, "Error BAMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            dtpHasta.Value = DateTime.Now;
            dtpDesde.Value = DateTime.Now.AddDays(-30);
        }
    }
}
