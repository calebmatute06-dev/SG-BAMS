using SG_BAMS.Bitacora;
using SG_BAMS.Proveedor;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Color = System.Drawing.Color;

namespace SG_BAMS.Reporte
{
    public partial class ReportesAdmin : Form
    {
        private readonly IReportesRepository _repositorio;
        private readonly IExportadorReporte _exportadorExcel;
        private readonly IExportadorReporte _exportadorPdf;
        private readonly ServicioFiltroStock _filtroStock;
        private readonly NavegacionService _servicioNavegacion;

        private DataTable _datosActuales;
        private ReporteTipo _tipoActual;

        public ReportesAdmin()
        {
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

            _repositorio = new ClsReportesDatos();
            _exportadorExcel = new ExportadorExcelReporte();
            _exportadorPdf = new ExportadorPdfReporte();
            _filtroStock = new ServicioFiltroStock();
            _servicioNavegacion = new NavegacionService();

            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.dtpDesde.ValueChanged += FiltroFecha_ValueChanged;
            this.dtpHasta.ValueChanged += FiltroFecha_ValueChanged;
        }

        private void FiltroFecha_ValueChanged(object sender, EventArgs e)
        {
            this.dtpDesde.ValueChanged -= FiltroFecha_ValueChanged;
            this.dtpHasta.ValueChanged -= FiltroFecha_ValueChanged;

            if (dtpDesde.Value.Date > dtpHasta.Value.Date)
            {
                if (sender == dtpDesde) dtpDesde.Value = dtpHasta.Value;
                else if (sender == dtpHasta) dtpHasta.Value = dtpDesde.Value;
            }

            this.dtpDesde.ValueChanged += FiltroFecha_ValueChanged;
            this.dtpHasta.ValueChanged += FiltroFecha_ValueChanged;

            if (ReporteConfig.TryDesdeTexto(cmbReporte.SelectedItem?.ToString(), out ReporteTipo tipo)
                && (tipo == ReporteTipo.Ventas || tipo == ReporteTipo.Compras))
            {
                CargarReporte(tipo);
            }
        }

        private void ReportesAdmin_Load(object sender, EventArgs e)
        {
            btnReportes.Enabled = false;
            btnReportes.BackColor = Color.SkyBlue;
            btnReportes.ForeColor = Color.White;
            cmbCant.Visible = false;
            ControlarFiltroStock(false);
            dtpHasta.MaxDate = DateTime.Today;
            dtpDesde.MaxDate = DateTime.Today;
            dtpHasta.Value = DateTime.Today;
            dtpDesde.Value = DateTime.Today.AddDays(-30);
            AdaptadorPantallaCompleta.Habilitar(this);

            this.Min.ValueChanged += FiltroStock_ValueChanged;
            this.Max.ValueChanged += FiltroStock_ValueChanged;

            EstilizarGrilla();

            cmbReporte.SelectedIndex = 0;
            dgvReporte.ClearSelection();
        }

        private void EstilizarGrilla()
        {
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
        }

        private void cmbReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbReporte.SelectedItem == null) return;
            if (!ReporteConfig.TryDesdeTexto(cmbReporte.SelectedItem.ToString(), out ReporteTipo tipo)) return;

            ConfigurarVisibilidadControles(tipo);
            CargarReporte(tipo);
        }

        private void ConfigurarVisibilidadControles(ReporteTipo tipo)
        {
            bool usaFechas = tipo == ReporteTipo.Ventas || tipo == ReporteTipo.Compras;
            dtpDesde.Visible = usaFechas;
            dtpHasta.Visible = usaFechas;
            MostrarControl("label1", usaFechas);
            MostrarControl("label3", usaFechas);
            MostrarControl("label4", usaFechas);

            bool mostrarFiltroStock = tipo == ReporteTipo.Inventario;
            cmbCant.Visible = mostrarFiltroStock;
            MostrarControl("label12", mostrarFiltroStock);
            Min.Visible = mostrarFiltroStock;
            Max.Visible = mostrarFiltroStock;
            MostrarControl("label5", mostrarFiltroStock);
            MostrarControl("label6", mostrarFiltroStock);
            MostrarControl("label7", mostrarFiltroStock);
            MostrarControl("label10", mostrarFiltroStock);

            ControlarFiltroStock(mostrarFiltroStock);
        }

        private void MostrarControl(string nombre, bool visible)
        {
            if (this.Controls.Find(nombre, true).FirstOrDefault() is Label lbl) lbl.Visible = visible;
        }

        private void CargarReporte(ReporteTipo tipo)
        {
            try
            {
                DataTable datos = ObtenerDatos(tipo);

                if (tipo == ReporteTipo.Inventario) AgregarColumnaCapital(datos);

                _datosActuales = datos;
                _tipoActual = tipo;

                dgvReporte.DataSource = datos;
                AplicarRenombresYOrden(tipo, datos);

                if (tipo == ReporteTipo.Inventario) cmbCant_SelectedIndexChanged(null, null);

                dgvReporte.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el reporte: " + ex.Message, "Error BAMS",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable ObtenerDatos(ReporteTipo tipo)
        {
            switch (tipo)
            {
                case ReporteTipo.Ventas: return _repositorio.ReporteVentas(dtpDesde.Value, dtpHasta.Value);
                case ReporteTipo.Compras: return _repositorio.ReporteCompras(dtpDesde.Value, dtpHasta.Value);
                case ReporteTipo.Deudores: return _repositorio.ReporteDeudores();
                case ReporteTipo.Inventario: return _repositorio.ReporteInventario();
                default: throw new ArgumentOutOfRangeException(nameof(tipo), tipo, null);
            }
        }

        private void AgregarColumnaCapital(DataTable datos)
        {
            if (!datos.Columns.Contains("Total_Venta_Esperada"))
            {
                DataColumn colTotal = new DataColumn("Total_Venta_Esperada", typeof(decimal))
                {
                    Expression = "Stock_Actual * Precio_Unitario"
                };
                datos.Columns.Add(colTotal);
            }
        }

        private void AplicarRenombresYOrden(ReporteTipo tipo, DataTable datos)
        {
            var config = ReporteConfig.Obtener(tipo);

            foreach (var renombre in config.RenombresColumnas)
            {
                if (dgvReporte.Columns.Contains(renombre.Key))
                    dgvReporte.Columns[renombre.Key].HeaderText = renombre.Value;
            }

            if (!string.IsNullOrEmpty(config.ColumnaOrden) && dgvReporte.Columns.Contains(config.ColumnaOrden))
            {
                dgvReporte.Sort(
                    dgvReporte.Columns[config.ColumnaOrden],
                    config.OrdenDescendente ? ListSortDirection.Descending : ListSortDirection.Ascending);
            }
        }

        private void dgvReporte_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value == null) return;

            var config = ReporteConfig.Obtener(_tipoActual);
            string nombreColumna = dgvReporte.Columns[e.ColumnIndex].Name;

            if (nombreColumna == config.ColumnaStock && int.TryParse(e.Value.ToString(), out int stock))
            {
                var nivel = ServicioColorStock.Evaluar(stock);
                var (fondo, letra) = ServicioColorStock.ColoresWinForms(nivel);
                e.CellStyle.BackColor = fondo;
                e.CellStyle.ForeColor = letra;
            }
            else if (config.ColumnasMonetarias.Contains(nombreColumna) && decimal.TryParse(e.Value.ToString(), out decimal monto))
            {
                e.Value = $"L. {monto:N2}";
                e.FormattingApplied = true;
            }
        }

        private void ControlarFiltroStock(bool estado)
        {
            Min.Enabled = estado;
            Max.Enabled = estado;
            if (estado)
            {
                Min.BackColor = Color.White;
                Max.BackColor = Color.White;
            }
            else
            {
                Min.Value = 0;
                Max.Value = 0;
                Max.Minimum = 0;
                Min.BackColor = Color.LightGray;
                Max.BackColor = Color.LightGray;
            }
        }

        private void FiltroStock_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!(dgvReporte.DataSource is DataTable dt)) return;

                this.Min.ValueChanged -= FiltroStock_ValueChanged;
                this.Max.ValueChanged -= FiltroStock_ValueChanged;

                int valorMin = (int)Min.Value;
                int valorMax = (int)Max.Value;
                Max.Minimum = valorMin;
                if (sender == Min && valorMin > valorMax)
                {
                    Max.Value = valorMin;
                    valorMax = valorMin;
                }

                cmbCant.SelectedIndexChanged -= cmbCant_SelectedIndexChanged;
                cmbCant.SelectedIndex = -1;
                cmbCant.SelectedIndexChanged += cmbCant_SelectedIndexChanged;

                dt.DefaultView.RowFilter = _filtroStock.PorRango(valorMin, valorMax);

                this.Min.ValueChanged += FiltroStock_ValueChanged;
                this.Max.ValueChanged += FiltroStock_ValueChanged;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error al filtrar en tiempo real: " + ex.Message);
            }
        }

        private void cmbCant_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(dgvReporte.DataSource is DataTable dt)) return;

            if (cmbCant.SelectedItem == null)
            {
                dt.DefaultView.RowFilter = string.Empty;
                return;
            }

            CategoriaStock categoria = MapearCategoria(cmbCant.SelectedItem.ToString());
            dt.DefaultView.RowFilter = _filtroStock.PorCategoria(categoria);
        }

        private CategoriaStock MapearCategoria(string opcion)
        {
            switch (opcion)
            {
                case "Sin Stock": return CategoriaStock.SinStock;
                case "Bajo Stock": return CategoriaStock.BajoStock;
                case "Buen Stock": return CategoriaStock.BuenStock;
                default: return CategoriaStock.Todos;
            }
        }

        private void btnExportaar_Click(object sender, EventArgs e)
        {
            ExportarYAbrir(_exportadorPdf, "PDF");
        }

        private void btnExportarEx_Click_1(object sender, EventArgs e)
        {
            ExportarYAbrir(_exportadorExcel, "Excel");
        }

        private void ExportarYAbrir(IExportadorReporte exportador, string formato)
        {
            DataTable datosVisibles = ObtenerDatosVisibles();
            if (datosVisibles == null || datosVisibles.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos.", "BAMS");
                return;
            }

            try
            {
                string ruta = exportador.Exportar(datosVisibles, _tipoActual, dtpDesde.Value, dtpHasta.Value);
                AbrirArchivo(ruta);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar a {formato}: " + ex.Message, "BAMS");
            }
        }

        private DataTable ObtenerDatosVisibles()
        {
            if (dgvReporte.DataSource is DataTable dt) return dt.DefaultView.ToTable();
            return _datosActuales;
        }

        private void AbrirArchivo(string ruta)
        {
            if (string.IsNullOrEmpty(ruta)) return;
            Process.Start(new ProcessStartInfo { FileName = ruta, UseShellExecute = true });
        }

        private void btnNoti_Click(object sender, EventArgs e)
        {
            NotificacionesAdmin notificaciones = new NotificacionesAdmin();
            notificaciones.ShowDialog();
        }

        // =========================================================================
        // SECCIÓN DE NAVEGACIÓN ADAPTADA EXACTAMENTE A LA OTRA INTERFAZ
        // =========================================================================

        private void btnMenu_Click(object sender, EventArgs e) => _servicioNavegacion.IrA(this, new MenuPrincipalAdm());

        private void btnFacturas_Click(object sender, EventArgs e) => _servicioNavegacion.IrA(this, new FacturasAdm());

        private void btnCompra_Click(object sender, EventArgs e) => _servicioNavegacion.IrA(this, new Compras());

        private void btnClientes_Click(object sender, EventArgs e) => _servicioNavegacion.IrA(this, new ClientesAdm(new Cliente.ClienteRepository()));

        private void btnInventario_Click(object sender, EventArgs e)
        {
            InventarioAdmin IA = new InventarioAdmin(new ProductoInventario.ProductoRepository(), new ProductoInventario.ComboRepository());
            IA.Show();
            this.Hide();
        }

        private void btnDeudores_Click(object sender, EventArgs e)
        {
            DeudoresAdmin DA = new DeudoresAdmin(new DeudaRepository());
            DA.Show();
            this.Hide();
        }


        private void btnProveedores_Click(object sender, EventArgs e)
        {
            var PA = new ProveedoresAdmin(
                new ProveedorRepository(),
                new EstadoRepository(),
                new ClasificacionRepository());
            _servicioNavegacion.IrA(this, PA);
        }

        private void btnBitacora_Click(object sender, EventArgs e)
        {
            var Bi = new BitacoraAdmin(
                new BitacoraRepository(),
                new FiltroBitacoraService(),
                new ReporteBitacoraPdfExportador());
            _servicioNavegacion.IrA(this, Bi);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "¿Está seguro que desea cerrar sesión?",
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                Login.Login login = new Login.Login();
                login.Show();
                this.Close();
            }
        }

        private void btnPerfil_Click(object sender, EventArgs e)
        {
            Perfil perfil = new Perfil();
            perfil.ShowDialog();
        }

        private void btnLimpiar_Click_1(object sender, EventArgs e)
        {
            dtpHasta.Value = DateTime.Today;
            dtpDesde.Value = DateTime.Today.AddDays(-30);
            Min.Value = 0;
            Max.Value = 0;
            ControlarFiltroStock(false);
            cmbCant.SelectedIndex = -1;

            if (dgvReporte.DataSource is DataTable dt)
            {
                dt.DefaultView.RowFilter = string.Empty;
            }

            cmbReporte_SelectedIndexChanged(null, null);
        }
    }
}