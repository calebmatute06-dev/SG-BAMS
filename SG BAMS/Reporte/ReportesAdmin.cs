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
            ControlarFiltroStock(false);

            dtpHasta.MaxDate = DateTime.Now;
            dtpDesde.MaxDate = DateTime.Now;

            dtpHasta.Value = DateTime.Now;
            dtpDesde.Value = DateTime.Now.AddDays(-30);

            cmbReporte.SelectedIndex = 0;
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
            btnFiltro.Enabled = estado;

            if (estado)
            {
                Min.BackColor = System.Drawing.Color.White;
                Max.BackColor = System.Drawing.Color.White;
            }
            else
            {
                Min.Value = 0;
                Max.Value = 0;
                Min.BackColor = System.Drawing.Color.LightGray;
                Max.BackColor = System.Drawing.Color.LightGray;
            }
        }

        private void dgvReporte_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvReporte.Columns[e.ColumnIndex].Name == "Stock_Actual" && e.Value != null)
            {
                if (int.TryParse(e.Value.ToString(), out int stock))
                {
                    if (stock == 0)
                    {
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

        private void btnExportarEx_Click(object sender, EventArgs e)
        {

        }

        private void btnMenuPrincipal_Click(object sender, EventArgs e)
        {
            MenuPrincipalAdm menuPrincipalAdm = new MenuPrincipalAdm();
            menuPrincipalAdm.Show();
            this.Hide();
        }

        private void btnFactura_Click(object sender, EventArgs e)
        {
            FacturasAdm facturas = new FacturasAdm();
            facturas.Show();
            this.Hide();
        }

        private void btnCompra_Click(object sender, EventArgs e)
        {
            Compras vercompras = new Compras();
            vercompras.Show();
            this.Hide();
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            ClientesAdm clientes = new ClientesAdm();
            clientes.Show();
            this.Hide();
        }

        private void btnInventarioAdmin_Click(object sender, EventArgs e)
        {
            InventarioAdmin inventario = new InventarioAdmin();
            inventario.Show();
            this.Hide();
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            Proveedor.ProveedoresAdmin proveedores = new Proveedor.ProveedoresAdmin();
            proveedores.Show();
            this.Hide();
        }

        private void btnDeudoresAdmin_Click(object sender, EventArgs e)
        {
            DeudoresAdmin deudoresAdm = new DeudoresAdmin();
            deudoresAdm.Show();
            this.Hide();
        }

        private void btnBitacora_Click(object sender, EventArgs e)
        {
            Bitacora.BitacoraAdmin bitacora = new Bitacora.BitacoraAdmin();
            bitacora.Show();
            this.Hide();
        }

        private void cmbReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbReporte.SelectedItem == null) return;
            string reporteSeleccionado = cmbReporte.SelectedItem.ToString();

            bool usaFechas = (reporteSeleccionado == "Ventas" || reporteSeleccionado == "Compras");
            dtpDesde.Enabled = usaFechas;
            dtpHasta.Enabled = usaFechas;

            ControlarFiltroStock(reporteSeleccionado == "Inventario");

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

        private void btnPerfil_Click(object sender, EventArgs e)
        {
            Perfil perfil = new Perfil();
            perfil.Show();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Login.Login login = new Login.Login();
            login.Show();
            this.Close();
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

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            dtpHasta.Value = DateTime.Now;
            dtpDesde.Value = DateTime.Now.AddDays(-30);

            Min.Value = 0;
            Max.Value = 0;
            ControlarFiltroStock(false);

            CargarReporteVentas();
        }

        private void btnAplicar_Click(object sender, EventArgs e)
        {
            try
            {

                if (dgvReporte.DataSource != null && dgvReporte.DataSource is DataTable dt)
                {
                    int valorMin = (int)Min.Value;
                    int valorMax = (int)Max.Value;

                    if (valorMin > valorMax)
                    {
                        MessageBox.Show("El valor mínimo no puede ser mayor al máximo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    dt.DefaultView.RowFilter = string.Format("Stock_Actual >= {0} AND Stock_Actual <= {1}", valorMin, valorMax);

                    if (dgvReporte.Rows.Count == 0)
                    {
                        MessageBox.Show("No hay productos con ese rango de stock.", "Sin resultados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Primero debe cargar el Inventario para aplicar un filtro de stock.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar: " + ex.Message);
            }
        }
    }
}
