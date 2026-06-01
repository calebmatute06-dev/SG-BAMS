using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Windows.Forms;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using SG_BAMS.Proveedor;
using SG_BAMS.Reporte;
using Color = System.Drawing.Color;

namespace SG_BAMS.Bitacora
{
    /// <summary>
    /// Formulario de administración de la bitácora del sistema.
    /// Permite visualizar, filtrar y exportar los registros de actividad.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class BitacoraAdmin : Form
    {
        /// <summary>
        /// Instancia de la clase de lógica de bitácora.
        /// </summary>
        ClsBitacora bitacora = new ClsBitacora();

        /// <summary>
        /// Texto del placeholder para el campo de búsqueda
        /// </summary>
        private string placeholderTexto = "Buscar por nombre, acción o módulo...";

        /// <summary>
        /// Bandera para evitar eventos recursivos
        /// </summary>
        private bool isSearching = false;

        /// <summary>
        /// Bandera para evitar bucles en los eventos de fecha
        /// </summary>
        private bool isUpdatingDates = false;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="BitacoraAdmin"/>.
        /// </summary>
        public BitacoraAdmin()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            dtpDesde.MaxDate = DateTime.Today;
            dtpHasta.MaxDate = DateTime.Today;

            txtBuscar.KeyPress += (s, e) => ClsValidaciones.ValidarBusquedaAlfanumerica(e);
            txtBuscar.TextChanged += txtBuscar_TextChanged;
        }

        /// <summary>
        /// Maneja el evento de carga del formulario Bitácora.
        /// Configura el estilo visual del DataGridView y carga los datos iniciales.
        /// </summary>
        /// <param name="sender">El objeto que origina el evento.</param>
        /// <param name="e">Datos del evento <see cref="EventArgs"/>.</param>
        private void Bitacora_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            new PlaceholderTextBox(txtBuscar, placeholderTexto);

            btnBitacora.Enabled = false;
            btnBitacora.BackColor = Color.SkyBlue;
            btnBitacora.ForeColor = Color.White;

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

            
            isUpdatingDates = true;
            dtpDesde.Value = DateTime.Today.AddDays(-30);
            dtpHasta.Value = DateTime.Today;
            isUpdatingDates = false;

            
            bitacora.cargarDatos(dgvBitacora);
            EjecutarBusqueda();
            if (dgvBitacora.Columns.Count >= 4)
            {
                dgvBitacora.Columns["Nombre"].Width = 110;   
                dgvBitacora.Columns["Acción"].Width = 480;   
                dgvBitacora.Columns["Modulo"].Width = 140;
                dgvBitacora.Columns["Fecha"].Width = 100;
            }
            dgvBitacora.ClearSelection();

            this.ActiveControl = null;
        }

        /// <summary>
        /// Ejecuta la búsqueda en la bitácora con los filtros actuales.
        /// Si hay texto de búsqueda, ignora el filtro de fechas y busca en todos los registros.
        /// Si no hay texto, aplica solo el filtro de fechas.
        /// </summary>
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
                {
                    textoBusqueda = "";
                }

                
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
                        {
                            condicionesTexto.Add($"[{col.ColumnName}] LIKE '%{textoSeguro}%'");
                        }
                    }

                    if (condicionesTexto.Count > 0)
                    {
                        condiciones.Add("(" + string.Join(" OR ", condicionesTexto) + ")");
                    }
                }
                else
                {
                    
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
                    {
                        condiciones.Add($"[{columnaFecha}] >= #{fechaDesde}# AND [{columnaFecha}] < #{fechaHasta}#");
                    }
                }

                
                if (condiciones.Count > 0)
                {
                    dataView.RowFilter = string.Join(" AND ", condiciones);
                }
                else
                {
                    dataView.RowFilter = "";
                }

                dgvBitacora.DataSource = dataView;
                dgvBitacora.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al filtrar: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Maneja el evento TextChanged del cuadro de texto de búsqueda.
        /// </summary>
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

        /// <summary>
        /// Maneja el evento ValueChanged del selector de fecha inicial.
        /// </summary>
        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {
            if (isUpdatingDates) return;
            isUpdatingDates = true;

            if (dtpDesde.Value > DateTime.Today)
                dtpDesde.Value = DateTime.Today;
            if (dtpDesde.Value > dtpHasta.Value)
                dtpDesde.Value = dtpHasta.Value;

            isUpdatingDates = false;
            EjecutarBusqueda();
        }

        /// <summary>
        /// Maneja el evento ValueChanged del selector de fecha final.
        /// </summary>
        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {
            if (isUpdatingDates) return;
            isUpdatingDates = true;

            if (dtpHasta.Value > DateTime.Today)
                dtpHasta.Value = DateTime.Today;
            if (dtpHasta.Value < dtpDesde.Value)
                dtpHasta.Value = dtpDesde.Value;

            isUpdatingDates = false;
            EjecutarBusqueda();
        }

        /// <summary>
        /// Maneja el evento Click del botón de actualizar.
        /// Restablece los filtros a los valores por defecto (últimos 30 días).
        /// </summary>
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
            EjecutarBusqueda();
            dgvBitacora.ClearSelection();
        }

        /// <summary>
        /// Maneja el evento Click del botón de exportar.
        /// </summary>
        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvBitacora.Rows.Count == 0 || (dgvBitacora.Rows.Count == 1 && dgvBitacora.Rows[0].IsNewRow))
                {
                    MessageBox.Show("No hay registros disponibles para exportar.", "SG-BAMS",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                var documento = new SG_BAMS.Bitacora.ReporteBitacora(lista);
                documento.GeneratePdf(rutaTemp);

                Process.Start(new ProcessStartInfo { FileName = rutaTemp, UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el reporte PDF: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnNoti_Click(object sender, EventArgs e) => new NotificacionesAdmin().Show();

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

        private void btnReportes_Click(object sender, EventArgs e)
        {
            ReportesAdmin RA = new ReportesAdmin();
            RA.Show();
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
    }
}