using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using SG_BAMS.Proveedor;
using SG_BAMS.Reporte;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using Color = System.Drawing.Color;

using System.Data;
using System.Diagnostics;

#nullable enable
using System.Data;
using System.Diagnostics;

namespace SG_BAMS.Bitacora;

/// <summary>
/// Formulario administrativo de bitácora. Única responsabilidad: orquestar
/// la interfaz gráfica y delegar en <see cref="IBitacoraRepository"/>,
/// <see cref="IFiltroBitacora"/> e <see cref="IReporteExportado"/>
/// toda la lógica de datos, filtrado y exportación.
/// </summary>
public partial class BitacoraAdmin : Form
{
    private readonly IBitacoraRepository _repositorio;
    private readonly IFiltroBitacora _filtroService;
    private readonly IReporteExportado _exportador;
    private readonly SemaphoreSlim _busquedaLock = new(1, 1); 

    private const string PlaceholderTexto = "Buscar por nombre, acción o módulo...";
    private bool _isSearching;
    private bool _isUpdatingDates;

    /// <summary>
    /// Crea el formulario recibiendo sus dependencias por inyección
    /// (repositorio de datos, servicio de filtrado y exportador de reportes).
    /// </summary>
    public BitacoraAdmin(IBitacoraRepository repositorio,
                          IFiltroBitacora filtroService,
                          IReporteExportado exportador)
    {
        InitializeComponent();
        _repositorio = repositorio;
        _filtroService = filtroService;
        _exportador = exportador;

        StartPosition = FormStartPosition.CenterScreen;
        dtpDesde.MaxDate = DateTime.Today;
        dtpHasta.MaxDate = DateTime.Today;
        txtBuscar.KeyPress += (s, e) => ClsValidaciones.ValidarBusquedaAlfanumerica(e);
        txtBuscar.TextChanged += txtBuscar_TextChanged;
    }

    private async void Bitacora_Load(object sender, EventArgs e)
    {
        AdaptadorPantallaCompleta.Habilitar(this);
        new PlaceholderTextBox(txtBuscar, PlaceholderTexto);

        btnBitacora.Enabled = false;
        btnBitacora.BackColor = Color.SkyBlue;
        btnBitacora.ForeColor = Color.White;

        ConfigurarGrilla();

        _isUpdatingDates = true;
        dtpDesde.Value = DateTime.Today.AddDays(-30);
        dtpHasta.Value = DateTime.Today;
        _isUpdatingDates = false;

        await EjecutarBusquedaAsync();

        if (dgvBitacora.Columns.Count >= 4)
        {
            dgvBitacora.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBitacora.Columns["Nombre"].FillWeight = 10;
            dgvBitacora.Columns["Acción"].FillWeight = 35;
            dgvBitacora.Columns["Modulo"].FillWeight = 15;
            dgvBitacora.Columns["Fecha"].FillWeight = 15;
        }
        dgvBitacora.ClearSelection();
        ActiveControl = null;
    }

    /// <summary>
    /// Aplica el estilo visual de la grilla (colores, bordes, fuentes, formato de fecha).
    /// </summary>
    private void ConfigurarGrilla()
    {
        dgvBitacora.ReadOnly = true;
        dgvBitacora.AllowUserToAddRows = false;
        dgvBitacora.AllowUserToDeleteRows = false;
        dgvBitacora.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvBitacora.BorderStyle = BorderStyle.None;
        dgvBitacora.BackgroundColor = Color.White;
        dgvBitacora.RowHeadersVisible = false;
        dgvBitacora.EnableHeadersVisualStyles = false;
        dgvBitacora.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

        dgvBitacora.ColumnHeadersDefaultCellStyle.BackColor = Color.SkyBlue;
        dgvBitacora.ColumnHeadersDefaultCellStyle.ForeColor = Color.Navy;
        dgvBitacora.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        dgvBitacora.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        dgvBitacora.ColumnHeadersHeight = 28;

        dgvBitacora.DefaultCellStyle.BackColor = Color.White;
        dgvBitacora.DefaultCellStyle.ForeColor = Color.Navy;
        dgvBitacora.DefaultCellStyle.Font = new Font("Segoe UI", 10);
        dgvBitacora.DefaultCellStyle.Padding = new Padding(3);
        dgvBitacora.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 245, 255);
        dgvBitacora.AlternatingRowsDefaultCellStyle.ForeColor = Color.Navy;
        dgvBitacora.DefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
        dgvBitacora.DefaultCellStyle.SelectionForeColor = Color.White;

        dgvBitacora.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        dgvBitacora.GridColor = Color.LightGray;
        dgvBitacora.RowTemplate.Height = 32;
        dgvBitacora.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        dgvBitacora.TabStop = false;

        dgvBitacora.CellFormatting += (s, ev) =>
        {
            if (dgvBitacora.Columns[ev.ColumnIndex].Name == "Fecha" && ev.Value is DateTime fecha)
            {
                ev.Value = fecha.ToString("dd/MM/yyyy HH:mm:ss");
                ev.FormattingApplied = true;
            }
        };
    }

    /// <summary>
    /// Obtiene los registros y aplica el filtro actual de búsqueda y fechas.
    /// Serializado con <see cref="_busquedaLock"/> para evitar que dos búsquedas
    /// intenten usar la misma conexión al mismo tiempo.
    /// </summary>
    private async Task EjecutarBusquedaAsync()
    {
        await _busquedaLock.WaitAsync();
        try
        {
            var dt = await _repositorio.ObtenerRegistrosAsync();
            var vista = dt.DefaultView;

            var textoBusqueda = txtBuscar.Text?.Trim() ?? string.Empty;
            if (textoBusqueda == PlaceholderTexto)
                textoBusqueda = string.Empty;

            vista.RowFilter = _filtroService.ConstruirFiltro(dt, textoBusqueda, dtpDesde.Value, dtpHasta.Value);
            dgvBitacora.DataSource = vista;

            if (dgvBitacora.Columns.Contains("Fecha"))
                dgvBitacora.Columns["Fecha"].HeaderText = "Fecha y Hora";

            dgvBitacora.ClearSelection();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al filtrar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            _busquedaLock.Release();
        }
    }

    private async void txtBuscar_TextChanged(object sender, EventArgs e)
    {
        if (txtBuscar.Text == PlaceholderTexto || _isSearching) return;

        _isSearching = true;
        var cursorPosition = txtBuscar.SelectionStart;
        await EjecutarBusquedaAsync();
        txtBuscar.Focus();
        txtBuscar.SelectionStart = cursorPosition;
        _isSearching = false;
    }

    private async void dtpDesde_ValueChanged(object sender, EventArgs e)
    {
        if (_isUpdatingDates) return;
        _isUpdatingDates = true;
        if (dtpDesde.Value > DateTime.Today) dtpDesde.Value = DateTime.Today;
        if (dtpDesde.Value > dtpHasta.Value) dtpDesde.Value = dtpHasta.Value;
        _isUpdatingDates = false;
        await EjecutarBusquedaAsync();
    }

    private async void dtpHasta_ValueChanged(object sender, EventArgs e)
    {
        if (_isUpdatingDates) return;
        _isUpdatingDates = true;
        if (dtpHasta.Value > DateTime.Today) dtpHasta.Value = DateTime.Today;
        if (dtpHasta.Value < dtpDesde.Value) dtpHasta.Value = dtpDesde.Value;
        _isUpdatingDates = false;
        await EjecutarBusquedaAsync();
    }

    private async void btnRefresh_Click(object sender, EventArgs e)
    {
        txtBuscar.Text = string.Empty;
        _isUpdatingDates = true;
        dtpDesde.MaxDate = DateTime.Today;
        dtpHasta.MaxDate = DateTime.Today;
        dtpDesde.Value = DateTime.Today.AddDays(-30);
        dtpHasta.Value = DateTime.Today;
        _isUpdatingDates = false;
        await EjecutarBusquedaAsync();
        dgvBitacora.ClearSelection();
    }

    private async void btnExportar_Click(object sender, EventArgs e)
    {
        try
        {
            if (dgvBitacora.Rows.Count == 0 || (dgvBitacora.Rows.Count == 1 && dgvBitacora.Rows[0].IsNewRow))
            {
                MessageBox.Show("No hay registros disponibles para exportar.", "SG-BAMS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var lista = dgvBitacora.Rows
                .Cast<DataGridViewRow>()
                .Where(row => !row.IsNewRow)
                .Select(row => new BitacoraDTO(
                    row.Cells["Nombre"].Value?.ToString() ?? "N/A",
                    row.Cells["Acción"].Value?.ToString() ?? "N/A",
                    row.Cells["Modulo"].Value?.ToString() ?? "N/A",
                    row.Cells["Fecha"].Value is not null ? Convert.ToDateTime(row.Cells["Fecha"].Value) : DateTime.Now))
                .ToList();

            var rutaTemp = Path.Combine(Path.GetTempPath(), $"ReporteBitacora_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");
            await _exportador.ExportarAsync(lista, rutaTemp);
            Process.Start(new ProcessStartInfo { FileName = rutaTemp, UseShellExecute = true });
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error al generar el reporte PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnNoti_Click(object sender, EventArgs e) => new NotificacionesAdmin().ShowDialog();
    private void btnMenu_Click(object sender, EventArgs e) { new MenuPrincipalAdm().Show(); Hide(); }
    private void btnFacturas_Click(object sender, EventArgs e) { new FacturasAdm().Show(); Hide(); }
    private void btnCompra_Click(object sender, EventArgs e) { new Compras().Show(); Hide(); }
    private void btnClientes_Click(object sender, EventArgs e) { new ClientesAdm(new Cliente.ClienteRepository()).Show(); Hide(); }
    private void btnInventario_Click(object sender, EventArgs e) { new InventarioAdmin(new ProductoInventario.ProductoRepository(), new ProductoInventario.ComboRepository()).Show(); Hide(); }
    private void btnProveedores_Click(object sender, EventArgs e)
    {
        var PA = new ProveedoresAdmin(
            new ProveedorRepository(),
            new EstadoRepository(),
            new ClasificacionRepository());
        PA.Show();
        this.Hide();
    }
    private void btnDeudores_Click(object sender, EventArgs e) { new DeudoresAdmin(new DeudaRepository()).Show(); Hide(); }
    private void btnReportes_Click(object sender, EventArgs e) { new ReportesAdmin().Show(); Hide(); }

    private void btnCerrar_Click(object sender, EventArgs e)
    {
        if (MessageBox.Show("¿Está seguro que desea cerrar sesión?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
            new Login.Login().Show();
            Close();
        }
    }

    private void btnPerfil_Click(object sender, EventArgs e) => new Perfil().ShowDialog();
}