using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using SG_BAMS.Cliente;
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



namespace SG_BAMS.Bitacora
{
    /// <summary>
    /// Formulario administrativo de bitácora. Construye sus propias dependencias
    /// en el constructor (tipadas por interfaz), delega el estilo visual de la
    /// grilla en EstiloDataGridView y la navegación en NavegacionService.
    /// </summary>
    public partial class BitacoraAdmin : Form
    {
        private readonly IBitacoraRepository _repositorio;
        private readonly IFiltroBitacora _filtroService;
        private readonly IReporteExportador _exportador;
        private readonly NavegacionService navegacion;

        private const string PlaceholderTexto = "Buscar por nombre, acción o módulo...";
        private DataTable? _datosBitacora;

        public BitacoraAdmin()
        {
            _repositorio = new BitacoraRepository();
            _filtroService = new FiltroBitacoraservice();
            _exportador = new ReporteBitacoraPdfExportador();
            navegacion = new NavegacionService();

            InitializeComponent();
            AdaptadorPantallaCompleta.Habilitar(this);
            this.StartPosition = FormStartPosition.CenterScreen;
            dtpDesde.MaxDate = DateTime.Today;
            dtpHasta.MaxDate = DateTime.Today;
            txtBuscar.KeyPress += (s, e) => ClsValidaciones.ValidarBusquedaAlfanumerica(e);
            txtBuscar.TextChanged += txtBuscar_TextChanged;
        }

        private void Bitacora_Load(object? sender, EventArgs e)
        {
            new PlaceholderTextBox(txtBuscar, PlaceholderTexto);

            btnBitacora.Enabled = false;
            btnBitacora.BackColor = Color.SkyBlue;
            btnBitacora.ForeColor = Color.White;

            ConfigurarGrilla();

            dtpDesde.Value = DateTime.Today.AddDays(-30);
            dtpHasta.Value = DateTime.Today;

            CargarDatos();
            FiltrarDatos();

            dgvBitacora.ClearSelection();
            this.ActiveControl = null;
        }

        /// <summary>
        /// Aplica el estilo visual compartido (EstiloDataGridView) y agrega lo
        /// exclusivo de esta pantalla: formato de la columna Fecha y anchos de columna.
        /// </summary>
        private void ConfigurarGrilla()
        {
            EstiloDataGridView.Aplicar(dgvBitacora);

            dgvBitacora.ReadOnly = true;
            dgvBitacora.AllowUserToAddRows = false;
            dgvBitacora.AllowUserToDeleteRows = false;
            dgvBitacora.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvBitacora.CellFormatting += (s, ev) =>
            {
                if (dgvBitacora.Columns[ev.ColumnIndex].Name == "Fecha" && ev.Value is DateTime fecha)
                {
                    ev.Value = fecha.ToString("dd/MM/yyyy HH:mm:ss");
                    ev.FormattingApplied = true;
                }
            };

            if (dgvBitacora.Columns.Count >= 4)
            {
                dgvBitacora.Columns["Nombre"].FillWeight = 10;
                dgvBitacora.Columns["Acción"].FillWeight = 35;
                dgvBitacora.Columns["Modulo"].FillWeight = 15;
                dgvBitacora.Columns["Fecha"].FillWeight = 15;
            }
        }

        /// <summary>
        /// Trae los registros del repositorio y los guarda en memoria.
        /// Solo se llama al abrir la pantalla o al refrescar.
        /// </summary>
        private void CargarDatos()
        {
            try
            {
                _datosBitacora = _repositorio.ObtenerRegistros();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Filtra los datos que ya están en memoria; no consulta la base de datos.
        /// </summary>
        private void FiltrarDatos()
        {
            if (_datosBitacora == null) return;

            try
            {
                string textoBusqueda = txtBuscar.Text?.Trim() ?? "";
                if (textoBusqueda == PlaceholderTexto)
                    textoBusqueda = "";

                DataView vista = _datosBitacora.DefaultView;
                vista.RowFilter = _filtroService.ConstruirFiltro(_datosBitacora, textoBusqueda, dtpDesde.Value, dtpHasta.Value);

                dgvBitacora.DataSource = vista;

                if (dgvBitacora.Columns.Contains("Fecha"))
                    dgvBitacora.Columns["Fecha"].HeaderText = "Fecha y Hora";

                dgvBitacora.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al filtrar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBuscar_TextChanged(object? sender, EventArgs e)
        {
            if (txtBuscar.Text == PlaceholderTexto) return;
            FiltrarDatos();
        }

        private void dtpDesde_ValueChanged(object? sender, EventArgs e)
        {
            if (dtpDesde.Value > DateTime.Today) dtpDesde.Value = DateTime.Today;
            if (dtpDesde.Value > dtpHasta.Value) dtpDesde.Value = dtpHasta.Value;
            FiltrarDatos();
        }

        private void dtpHasta_ValueChanged(object? sender, EventArgs e)
        {
            if (dtpHasta.Value > DateTime.Today) dtpHasta.Value = DateTime.Today;
            if (dtpHasta.Value < dtpDesde.Value) dtpHasta.Value = dtpDesde.Value;
            FiltrarDatos();
        }

        private void btnRefresh_Click(object? sender, EventArgs e)
        {
            txtBuscar.Text = "";
            dtpDesde.Value = DateTime.Today.AddDays(-30);
            dtpHasta.Value = DateTime.Today;
            CargarDatos();
            FiltrarDatos();
        }

        private void btnExportar_Click(object? sender, EventArgs e)
        {
            try
            {
                if (dgvBitacora.Rows.Count == 0 || (dgvBitacora.Rows.Count == 1 && dgvBitacora.Rows[0].IsNewRow))
                {
                    MessageBox.Show("No hay registros disponibles para exportar.", "SG-BAMS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                List<BitacoraDTO> lista = new List<BitacoraDTO>();
                foreach (DataGridViewRow row in dgvBitacora.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        lista.Add(new BitacoraDTO
                        {
                            Nombre = row.Cells["Nombre"].Value?.ToString() ?? "N/A",
                            Accion = row.Cells["Acción"].Value?.ToString() ?? "N/A",
                            Modulo = row.Cells["Modulo"].Value?.ToString() ?? "N/A",
                            Fecha = row.Cells["Fecha"].Value != null ? Convert.ToDateTime(row.Cells["Fecha"].Value) : DateTime.Now
                        });
                    }
                }

                string rutaTemp = Path.Combine(Path.GetTempPath(), $"ReporteBitacora_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");
                _exportador.Exportar(lista, rutaTemp);
                Process.Start(new ProcessStartInfo { FileName = rutaTemp, UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el reporte PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNoti_Click(object? sender, EventArgs e) => new NotificacionesAdmin().ShowDialog();

        private void btnMenu_Click(object? sender, EventArgs e) => navegacion.IrA(this, new MenuPrincipalAdm());
        private void btnFacturas_Click(object? sender, EventArgs e) => navegacion.IrA(this, new FacturasAdm());
        private void btnCompra_Click(object? sender, EventArgs e) => navegacion.IrA(this, new Compras());
        private void btnReportes_Click(object? sender, EventArgs e) => navegacion.IrA(this, new ReportesAdmin());

        private void btnClientes_Click(object? sender, EventArgs e)
        {
            var CA = new ClientesAdm(new ClienteRepository());
            navegacion.IrA(this, CA);
        }

        private void btnInventario_Click(object? sender, EventArgs e)
        {
            var IA = new InventarioAdmin(
                new ProductoInventario.ProductoRepository(),
                new ProductoInventario.ComboRepository());
            navegacion.IrA(this, IA);
        }

        private void btnDeudores_Click(object? sender, EventArgs e)
        {
            var DA = new DeudoresAdmin(new DeudaRepository());
            navegacion.IrA(this, DA);
        }

        private void btnProveedores_Click(object? sender, EventArgs e)
        {
            var PA = new ProveedoresAdmin(
                new ProveedorRepository(),
                new EstadoRepository(),
                new ClasificacionRepository());
            navegacion.IrA(this, PA);
        }

        private void btnCerrar_Click(object? sender, EventArgs e)
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

        private void btnPerfil_Click(object? sender, EventArgs e) => new Perfil().ShowDialog();
    }
}

