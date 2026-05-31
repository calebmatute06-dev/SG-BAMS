using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using SG_BAMS.Bitacora;
using SG_BAMS.Proveedor;
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
using Color = System.Drawing.Color;

namespace SG_BAMS.Reporte
{
    public partial class ReportesAdmin : Form
    {
        ClsReportesDatos objReporte = new ClsReportesDatos();

        public ReportesAdmin()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            this.dtpDesde.ValueChanged += new System.EventHandler(this.FiltroFecha_ValueChanged);
            this.dtpHasta.ValueChanged += new System.EventHandler(this.FiltroFecha_ValueChanged);
        }

        private void FiltroFecha_ValueChanged(object sender, EventArgs e)
        {
            this.dtpDesde.ValueChanged -= new System.EventHandler(this.FiltroFecha_ValueChanged);
            this.dtpHasta.ValueChanged -= new System.EventHandler(this.FiltroFecha_ValueChanged);

            if (dtpDesde.Value.Date > dtpHasta.Value.Date)
            {
                if (sender == dtpDesde)
                {
                    dtpDesde.Value = dtpHasta.Value;
                }
                else if (sender == dtpHasta)
                {
                    dtpHasta.Value = dtpDesde.Value;
                }
            }

            this.dtpDesde.ValueChanged += new System.EventHandler(this.FiltroFecha_ValueChanged);
            this.dtpHasta.ValueChanged += new System.EventHandler(this.FiltroFecha_ValueChanged);

            string reporte = cmbReporte.SelectedItem?.ToString();

            if (reporte == "Ventas" || reporte == "Compras")
            {
                cmbReporte_SelectedIndexChanged(null, null);
            }
        }

        private void ReportesAdmin_Load(object sender, EventArgs e)
        {
            btnReportes.Enabled = false;
            btnReportes.BackColor = Color.SkyBlue;
            btnReportes.ForeColor = Color.White;

            ControlarFiltroStock(false);

            dtpHasta.MaxDate = DateTime.Today;
            dtpDesde.MaxDate = DateTime.Today;

            dtpHasta.Value = DateTime.Today;
            dtpDesde.Value = DateTime.Today.AddDays(-30);

            this.Min.ValueChanged += new System.EventHandler(this.FiltroStock_ValueChanged);
            this.Max.ValueChanged += new System.EventHandler(this.FiltroStock_ValueChanged);

            cmbReporte.SelectedIndex = 0;

            dgvReporte.BorderStyle = BorderStyle.None;
            dgvReporte.BackgroundColor = Color.White;
            dgvReporte.RowHeadersVisible = false;
            dgvReporte.EnableHeadersVisualStyles = false;
            dgvReporte.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgvReporte.ColumnHeadersDefaultCellStyle.BackColor = Color.SkyBlue;
            dgvReporte.ColumnHeadersDefaultCellStyle.ForeColor = Color.Navy;
            dgvReporte.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvReporte.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvReporte.ColumnHeadersHeight = 28;

            dgvReporte.DefaultCellStyle.BackColor = Color.White;
            dgvReporte.DefaultCellStyle.ForeColor = Color.Navy;
            dgvReporte.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvReporte.DefaultCellStyle.Padding = new Padding(3);
            dgvReporte.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 245, 255);
            dgvReporte.AlternatingRowsDefaultCellStyle.ForeColor = Color.Navy;

            dgvReporte.DefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            dgvReporte.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvReporte.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvReporte.GridColor = Color.LightGray;
            dgvReporte.RowTemplate.Height = 32;
            dgvReporte.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReporte.ClearSelection();
        }

        private void FiltroStock_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvReporte.DataSource != null && dgvReporte.DataSource is DataTable dt)
                {
                    this.Min.ValueChanged -= new System.EventHandler(this.FiltroStock_ValueChanged);
                    this.Max.ValueChanged -= new System.EventHandler(this.FiltroStock_ValueChanged);

                    int valorMin = (int)Min.Value;
                    int valorMax = (int)Max.Value;

                    Max.Minimum = valorMin;

                    if (sender == Min && valorMin > valorMax)
                    {
                        Max.Value = valorMin;
                        valorMax = valorMin;
                    }

                    dt.DefaultView.RowFilter = string.Format("Stock_Actual >= {0} AND Stock_Actual <= {1}", valorMin, valorMax);

                    this.Min.ValueChanged += new System.EventHandler(this.FiltroStock_ValueChanged);
                    this.Max.ValueChanged += new System.EventHandler(this.FiltroStock_ValueChanged);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error al filtrar en tiempo real: " + ex.Message);
            }
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

        private void ControlarFiltroStock(bool estado)
        {
            Min.Enabled = estado;
            Max.Enabled = estado;

            if (estado)
            {
                Min.BackColor = System.Drawing.Color.White;
                Max.BackColor = System.Drawing.Color.White;
            }
            else
            {
                Min.Value = 0;
                Max.Value = 0;
                Max.Minimum = 0;
                Min.BackColor = System.Drawing.Color.LightGray;
                Max.BackColor = System.Drawing.Color.LightGray;
            }
        }

        private void cmbReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbReporte.SelectedItem == null) return;
            string reporteSeleccionado = cmbReporte.SelectedItem.ToString();

            bool usaFechas = (reporteSeleccionado == "Ventas" || reporteSeleccionado == "Compras");
            dtpDesde.Visible = usaFechas;
            dtpHasta.Visible = usaFechas;

            if (this.Controls.Find("label1", true).FirstOrDefault() is Label lbl1) lbl1.Visible = usaFechas;
            if (this.Controls.Find("label3", true).FirstOrDefault() is Label lbl3) lbl3.Visible = usaFechas;
            if (this.Controls.Find("label4", true).FirstOrDefault() is Label lbl4) lbl4.Visible = usaFechas;

            bool mostrarFiltroStock = (reporteSeleccionado == "Inventario");
            Min.Visible = mostrarFiltroStock;
            Max.Visible = mostrarFiltroStock;

            if (this.Controls.Find("label5", true).FirstOrDefault() is Label lbl5) lbl5.Visible = mostrarFiltroStock;
            if (this.Controls.Find("label6", true).FirstOrDefault() is Label lbl6) lbl6.Visible = mostrarFiltroStock;
            if (this.Controls.Find("label7", true).FirstOrDefault() is Label lbl7) lbl7.Visible = mostrarFiltroStock;
            if (this.Controls.Find("label12", true).FirstOrDefault() is Label lbl12) lbl12.Visible = mostrarFiltroStock;

            ControlarFiltroStock(mostrarFiltroStock);

            try
            {
                DataTable datos = new DataTable();

                switch (reporteSeleccionado)
                {
                    case "Ventas":
                        datos = objReporte.ReporteVentas(dtpDesde.Value, dtpHasta.Value);
                        dgvReporte.DataSource = datos;
                        if (dgvReporte.Columns.Contains("Telefono")) dgvReporte.Columns["Telefono"].HeaderText = "Teléfono";
                        if (dgvReporte.Columns.Contains("Metodo_Pago")) dgvReporte.Columns["Metodo_Pago"].HeaderText = "Método de Pago";
                        if (dgvReporte.Columns.Contains("Total_Venta")) dgvReporte.Columns["Total_Venta"].HeaderText = "Total";
                        if (dgvReporte.Columns.Contains("Recibio_Chatarra")) dgvReporte.Columns["Recibio_Chatarra"].HeaderText = "Bateria Vieja";
                        break;

                    case "Compras":
                        datos = objReporte.ReporteCompras(dtpDesde.Value, dtpHasta.Value);
                        dgvReporte.DataSource = datos;
                        if (dgvReporte.Columns.Contains("Inversion_Total")) dgvReporte.Columns["Inversion_Total"].HeaderText = "Total";
                        if (dgvReporte.Columns.Contains("RTN_Proveedor")) dgvReporte.Columns["RTN_Proveedor"].HeaderText = "RTN";
                        if (dgvReporte.Columns.Contains("Telefono_Proveedor")) dgvReporte.Columns["Telefono_Proveedor"].HeaderText = "Teléfono";
                        break;

                    case "Deudores":
                        datos = objReporte.ReporteDeudores();
                        dgvReporte.DataSource = datos;
                        if (dgvReporte.Columns.Contains("Fecha_Inicio")) dgvReporte.Columns["Fecha_Inicio"].HeaderText = "Fecha de Inicio";
                        if (dgvReporte.Columns.Contains("Monto_Credito")) dgvReporte.Columns["Monto_Credito"].HeaderText = "Monto Deuda";
                        if (dgvReporte.Columns.Contains("Saldo_Pendiente"))
                        {
                            dgvReporte.Columns["Saldo_Pendiente"].HeaderText = "Saldo a Cobrar";
                            dgvReporte.Sort(dgvReporte.Columns["Saldo_Pendiente"], System.ComponentModel.ListSortDirection.Descending);
                        }
                        break;

                    case "Inventario":
                        datos = objReporte.ReporteInventario();

                        if (!datos.Columns.Contains("Total_Venta_Esperada"))
                        {
                            DataColumn colTotal = new DataColumn("Total_Venta_Esperada", typeof(decimal));
                            colTotal.Expression = "Stock_Actual * Precio_Unitario";
                            datos.Columns.Add(colTotal);
                        }

                        dgvReporte.DataSource = datos;

                        if (dgvReporte.Columns.Contains("Stock_Actual"))
                        {
                            dgvReporte.Columns["Stock_Actual"].HeaderText = "Stock Actual";
                            dgvReporte.Sort(dgvReporte.Columns["Stock_Actual"], System.ComponentModel.ListSortDirection.Descending);
                        }

                        if (dgvReporte.Columns.Contains("Precio_Unitario"))
                            dgvReporte.Columns["Precio_Unitario"].HeaderText = "Precio Venta";

                        if (dgvReporte.Columns.Contains("Total_Venta_Esperada"))
                        {
                            dgvReporte.Columns["Total_Venta_Esperada"].HeaderText = "Capital";
                            dgvReporte.Columns["Total_Venta_Esperada"].DefaultCellStyle.Format = "N2";
                        }
                        break;
                }

                dgvReporte.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el reporte: " + ex.Message, "Error BAMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNoti_Click(object sender, EventArgs e)
        {
            NotificacionesAdmin notificaciones = new NotificacionesAdmin();
            notificaciones.Show();
        }

        private void btnExportaar_Click(object sender, EventArgs e)
        {
            try
            {
                QuestPDF.Settings.License = LicenseType.Community;
                if (dgvReporte.Rows.Count == 0)
                {
                    MessageBox.Show("No hay datos.", "BAMS"); return;
                }

                string seleccion = cmbReporte.SelectedItem?.ToString() ?? "REPORTE";
                string rutaTemp = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"Reporte_{seleccion}_{DateTime.Now:yyyyMMdd}.pdf");

                var documento = new DocumentoDinamico(dgvReporte, $"REPORTE DE {seleccion}", dtpDesde.Value, dtpHasta.Value);
                documento.GeneratePdf(rutaTemp);

                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo { FileName = rutaTemp, UseShellExecute = true });
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        private void btnExportarEx_Click_1(object sender, EventArgs e)
        {
            if (dgvReporte.Rows.Count == 0) return;

            string seleccion = cmbReporte.SelectedItem?.ToString() ?? "GENERAL";

            ClsExportarExcel exportador = new ClsExportarExcel();
            exportador.ExportarDataGridView(dgvReporte, seleccion, dtpDesde.Value, dtpHasta.Value);
        }

        private void btnLimpiar_Click_1(object sender, EventArgs e)
        {
            dtpHasta.Value = DateTime.Today;
            dtpDesde.Value = DateTime.Today.AddDays(-30);

            Min.Value = 0;
            Max.Value = 0;
            ControlarFiltroStock(false);

            cmbReporte_SelectedIndexChanged(null, null);
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            MenuPrincipalAdm MPA = new MenuPrincipalAdm();
            MPA.Show();
            this.Hide();
        }

        private void btnFacturas_Click(object sender, EventArgs e)
        {
            FacturasAdm FA = new FacturasAdm();
            FA.Show();
            this.Hide();
        }

        private void btnCompra_Click(object sender, EventArgs e)
        {
            Compras CF = new Compras();
            CF.Show();
            this.Hide();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            ClientesAdm CA = new ClientesAdm();
            CA.Show();
            this.Hide();
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            InventarioAdmin IA = new InventarioAdmin();
            IA.Show();
            this.Hide();
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            ProveedoresAdmin PA = new ProveedoresAdmin();
            PA.Show();
            this.Hide();
        }

        private void btnDeudores_Click(object sender, EventArgs e)
        {
            DeudoresAdmin DA = new DeudoresAdmin();
            DA.Show();
            this.Hide();
        }

        private void btnBitacora_Click(object sender, EventArgs e)
        {
            BitacoraAdmin BA = new BitacoraAdmin();
            BA.Show();
            this.Hide();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Login.Login login = new Login.Login();
            login.Show();
            this.Close();
        }

        private void btnPerfil_Click(object sender, EventArgs e)
        {
            Perfil perfil = new Perfil();
            perfil.Show();
        }

        private void dgvReporte_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvReporte.Columns[e.ColumnIndex].Name == "Stock_Actual" && e.Value != null)
            {
                if (int.TryParse(e.Value.ToString(), out int stock))
                {
                    if (stock < 1)
                    {
                        e.CellStyle.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
                        e.CellStyle.ForeColor = System.Drawing.Color.DarkRed;
                    }
                    else if (stock < 10)
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

            string[] columnasDinero = { "Total_Venta", "Inversion_Total", "Monto_Credito",
                                 "Saldo_Pendiente", "Precio_Unitario", "Total_Venta_Esperada", "Abonado", "Abono" };

            if (e.Value != null && columnasDinero.Contains(dgvReporte.Columns[e.ColumnIndex].Name))
            {
                if (decimal.TryParse(e.Value.ToString(), out decimal monto))
                {
                    e.Value = $"L. {monto:N2}";
                    e.FormattingApplied = true;
                }
            }
        }
    }
}