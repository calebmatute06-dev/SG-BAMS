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

namespace SG_BAMS.Bitacora
{
    public partial class BitacoraAdmin : Form
    {
        ClsBitacora bitacora = new ClsBitacora();
        private string placeholderTexto = "Buscar por nombre, acción o módulo...";
        private bool isSearching = false;
        private bool isUpdatingDates = false;

        public BitacoraAdmin()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            dtpDesde.MaxDate = DateTime.Today;
            dtpHasta.MaxDate = DateTime.Today;
            txtBuscar.KeyPress += (s, e) => ClsValidaciones.ValidarBusquedaAlfanumerica(e);
            txtBuscar.TextChanged += txtBuscar_TextChanged;
        }

        private void Bitacora_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            new PlaceholderTextBox(txtBuscar, placeholderTexto);

            btnBitacora.Enabled = false;
            btnBitacora.BackColor = Color.SkyBlue;
            btnBitacora.ForeColor = Color.White;

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

            // Formatear columna Fecha con hora
            dgvBitacora.CellFormatting += (s, ev) =>
            {
                if (dgvBitacora.Columns[ev.ColumnIndex].Name == "Fecha" && ev.Value != null)
                {
                    if (ev.Value is DateTime fecha)
                    {
                        ev.Value = fecha.ToString("dd/MM/yyyy HH:mm:ss");
                        ev.FormattingApplied = true;
                    }
                }
            };

            isUpdatingDates = true;
            dtpDesde.Value = DateTime.Today.AddDays(-30);
            dtpHasta.Value = DateTime.Today;
            isUpdatingDates = false;

            bitacora.cargarDatos(dgvBitacora);

            // Cambiar header de Fecha a Fecha y Hora
            if (dgvBitacora.Columns.Contains("Fecha"))
                dgvBitacora.Columns["Fecha"].HeaderText = "Fecha y Hora";

            EjecutarBusqueda();

            if (dgvBitacora.Columns.Count >= 4)
            {
                dgvBitacora.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvBitacora.Columns["Nombre"].FillWeight = 10;
                dgvBitacora.Columns["Acción"].FillWeight = 35;
                dgvBitacora.Columns["Modulo"].FillWeight = 15;
                dgvBitacora.Columns["Fecha"].FillWeight = 15;
            }
            dgvBitacora.ClearSelection();
            this.ActiveControl = null;
        }

        private void EjecutarBusqueda()
        {
            try
            {
                bitacora.cargarDatos(dgvBitacora);

                DataTable dt = null;
                if (dgvBitacora.DataSource is DataTable)
                    dt = (DataTable)dgvBitacora.DataSource;
                else if (dgvBitacora.DataSource is DataView dv)
                    dt = dv.Table;

                if (dt == null) return;

                DataView dataView = dt.DefaultView;
                var condiciones = new List<string>();
                string textoBusqueda = txtBuscar.Text?.Trim() ?? "";

                if (textoBusqueda == placeholderTexto)
                    textoBusqueda = "";

                if (!string.IsNullOrWhiteSpace(textoBusqueda))
                {
                    string textoSeguro = textoBusqueda
                        .Replace("'", "''")
                        .Replace("[", "[[]")
                        .Replace("]", "[]]")
                        .Replace("*", "[*]")
                        .Replace("%", "[%]");

                    var condicionesTexto = new List<string>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        if (col.DataType == typeof(string))
                            condicionesTexto.Add($"[{col.ColumnName}] LIKE '{textoSeguro}%'");
                    }
                    if (condicionesTexto.Count > 0)
                        condiciones.Add("(" + string.Join(" OR ", condicionesTexto) + ")");
                }

                string fechaDesde = dtpDesde.Value.Date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                string fechaHasta = dtpHasta.Value.Date.AddDays(1).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                string columnaFecha = null;
                foreach (DataColumn col in dt.Columns)
                {
                    if (col.DataType == typeof(DateTime))
                    {
                        columnaFecha = col.ColumnName;
                        break;
                    }
                }
                if (columnaFecha != null)
                    condiciones.Add($"[{columnaFecha}] >= #{fechaDesde}# AND [{columnaFecha}] < #{fechaHasta}#");

                dataView.RowFilter = condiciones.Count > 0 ? string.Join(" AND ", condiciones) : "";
                dgvBitacora.DataSource = dataView;

                if (dgvBitacora.Columns.Contains("Fecha"))
                    dgvBitacora.Columns["Fecha"].HeaderText = "Fecha y Hora";

                dgvBitacora.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al filtrar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscar.Text == placeholderTexto) return;
            if (isSearching) return;
            isSearching = true;
            int cursorPosition = txtBuscar.SelectionStart;
            EjecutarBusqueda();
            txtBuscar.Focus();
            txtBuscar.SelectionStart = cursorPosition;
            isSearching = false;
        }

        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {
            if (isUpdatingDates) return;
            isUpdatingDates = true;
            if (dtpDesde.Value > DateTime.Today) dtpDesde.Value = DateTime.Today;
            if (dtpDesde.Value > dtpHasta.Value) dtpDesde.Value = dtpHasta.Value;
            isUpdatingDates = false;
            EjecutarBusqueda();
        }

        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {
            if (isUpdatingDates) return;
            isUpdatingDates = true;
            if (dtpHasta.Value > DateTime.Today) dtpHasta.Value = DateTime.Today;
            if (dtpHasta.Value < dtpDesde.Value) dtpHasta.Value = dtpDesde.Value;
            isUpdatingDates = false;
            EjecutarBusqueda();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            isUpdatingDates = true;
            dtpDesde.MaxDate = DateTime.Today;
            dtpHasta.MaxDate = DateTime.Today;
            dtpDesde.Value = DateTime.Today.AddDays(-30);
            dtpHasta.Value = DateTime.Today;
            isUpdatingDates = false;
            bitacora.cargarDatos(dgvBitacora);
            if (dgvBitacora.Columns.Contains("Fecha"))
                dgvBitacora.Columns["Fecha"].HeaderText = "Fecha y Hora";
            EjecutarBusqueda();
            dgvBitacora.ClearSelection();
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvBitacora.Rows.Count == 0 || (dgvBitacora.Rows.Count == 1 && dgvBitacora.Rows[0].IsNewRow))
                {
                    MessageBox.Show("No hay registros disponibles para exportar.", "SG-BAMS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                QuestPDF.Settings.License = LicenseType.Community;
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
                var documento = new ReporteBitacora(lista);
                documento.GeneratePdf(rutaTemp);
                Process.Start(new ProcessStartInfo { FileName = rutaTemp, UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el reporte PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNoti_Click(object sender, EventArgs e) => new NotificacionesAdmin().Show();

        private void btnMenu_Click(object sender, EventArgs e) { MenuPrincipalAdm MPA = new MenuPrincipalAdm(); MPA.Show(); this.Hide(); }
        private void btnFacturas_Click(object sender, EventArgs e) { FacturasAdm FA = new FacturasAdm(); FA.Show(); this.Hide(); }
        private void btnCompra_Click(object sender, EventArgs e) { Compras CF = new Compras(); CF.Show(); this.Hide(); }
        private void btnClientes_Click(object sender, EventArgs e) { ClientesAdm CA = new ClientesAdm(); CA.Show(); this.Hide(); }
        private void btnInventario_Click(object sender, EventArgs e) { InventarioAdmin IA = new InventarioAdmin(); IA.Show(); this.Hide(); }
        private void btnProveedores_Click(object sender, EventArgs e) { ProveedoresAdmin PA = new ProveedoresAdmin(); PA.Show(); this.Hide(); }
        private void btnDeudores_Click(object sender, EventArgs e) { DeudoresAdmin DA = new DeudoresAdmin(); DA.Show(); this.Hide(); }
        private void btnReportes_Click(object sender, EventArgs e) { ReportesAdmin RA = new ReportesAdmin(); RA.Show(); this.Hide(); }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea cerrar sesión?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Login.Login login = new Login.Login();
                login.Show();
                this.Close();
            }
        }

        private void btnPerfil_Click(object sender, EventArgs e) { Perfil perfil = new Perfil(); perfil.Show(); }
    }
}