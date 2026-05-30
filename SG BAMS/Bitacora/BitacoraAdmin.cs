using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
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
        /// Inicializa una nueva instancia de la clase <see cref="BitacoraAdmin"/>.
        /// </summary>
        public BitacoraAdmin()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            dtpDesde.MaxDate = DateTime.Today;
            dtpHasta.MaxDate = DateTime.Today;

            txtBuscar.KeyPress += (s, e) => ClsValidaciones.ValidarBusquedaAlfanumerica(e);

            
        }

        /// <summary>
        /// Maneja el evento de carga del formulario Bitácora.
        /// Configura el estilo visual del DataGridView y carga los datos iniciales.
        /// </summary>
        /// <param name="sender">El objeto que origina el evento.</param>
        /// <param name="e">Datos del evento <see cref="EventArgs"/>.</param>
        private void Bitacora_Load(object sender, EventArgs e)
        {
            

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
            dgvBitacora.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            

            bitacora.cargarDatos(dgvBitacora);
            EjecutarBusquedaSegura();
            dgvBitacora.ClearSelection();

            ClsMensajeGuia.ActivarK(txtBuscar);
            this.ActiveControl = null;

        }

        /// <summary>
        /// Maneja el evento KeyUp del cuadro de texto de búsqueda.
        /// Ejecuta la búsqueda cada vez que el usuario escribe.
        /// </summary>
        /// <param name="sender">El objeto que origina el evento.</param>
        /// <param name="e">Datos del evento <see cref="KeyEventArgs"/>.</param>
        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            EjecutarBusquedaSegura();
        }

        /// <summary>
        /// Maneja el evento ValueChanged del selector de fecha inicial.
        /// Valida que la fecha no supere el día actual ni sea posterior a la fecha final.
        /// </summary>
        /// <param name="sender">El objeto que origina el evento.</param>
        /// <param name="e">Datos del evento <see cref="EventArgs"/>.</param>
        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {
            if (dtpDesde.Value > DateTime.Today)
                dtpDesde.Value = DateTime.Today;

            if (dtpDesde.Value > dtpHasta.Value)
                dtpDesde.Value = dtpHasta.Value;

            EjecutarBusquedaSegura();
        }

        /// <summary>
        /// Maneja el evento ValueChanged del selector de fecha final.
        /// Valida que la fecha no supere el día actual ni sea anterior a la fecha inicial.
        /// </summary>
        /// <param name="sender">El objeto que origina el evento.</param>
        /// <param name="e">Datos del evento <see cref="EventArgs"/>.</param>
        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {
            if (dtpHasta.Value > DateTime.Today)
                dtpHasta.Value = DateTime.Today;

            if (dtpHasta.Value < dtpDesde.Value)
                dtpHasta.Value = dtpDesde.Value;

            EjecutarBusquedaSegura();
        }

        /// <summary>
        /// Ejecuta la búsqueda en la bitácora aplicando el texto ingresado y el rango de fechas seleccionado.
        /// </summary>
        private void EjecutarBusquedaSegura()
        {
            bitacora.BuscarBitacora(txtBuscar, dtpDesde.Value, dtpHasta.Value, dgvBitacora);
        }

        /// <summary>
        /// Maneja el evento Click del botón de actualizar.
        /// Limpia los filtros y recarga todos los registros de la bitácora.
        /// </summary>
        /// <param name="sender">El objeto que origina el evento.</param>
        /// <param name="e">Datos del evento <see cref="EventArgs"/>.</param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();

            dtpDesde.MaxDate = DateTime.Today;
            dtpHasta.MaxDate = DateTime.Today;
            dtpDesde.Value = DateTime.Today;
            dtpHasta.Value = DateTime.Today;

            bitacora.cargarDatos(dgvBitacora);
            EjecutarBusquedaSegura();
            dgvBitacora.ClearSelection();
        }

        /// <summary>
        /// Maneja el evento Click del botón de exportar.
        /// Genera un reporte PDF con los registros visibles en la bitácora y lo abre automáticamente.
        /// </summary>
        /// <param name="sender">El objeto que origina el evento.</param>
        /// <param name="e">Datos del evento <see cref="EventArgs"/>.</param>
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

        /// <summary>
        /// Maneja el evento Click del botón de notificaciones.
        /// Abre el formulario de notificaciones del administrador.
        /// </summary>
        /// <param name="sender">El objeto que origina el evento.</param>
        /// <param name="e">Datos del evento <see cref="EventArgs"/>.</param>
        private void btnNoti_Click(object sender, EventArgs e) => new NotificacionesAdmin().Show();

        /// <summary>
        /// Navega al menú principal del administrador.
        /// </summary>
        private void btnMenu_Click(object sender, EventArgs e)
        {
            MenuPrincipalAdm MPA = new MenuPrincipalAdm();
            MPA.Show();
            this.Hide();
        }

        /// <summary>
        /// Navega al formulario de facturas del administrador.
        /// </summary>
        private void btnFacturas_Click(object sender, EventArgs e)
        {
            FacturasAdm FA = new FacturasAdm();
            FA.Show();
            this.Hide();
        }

        /// <summary>
        /// Navega al formulario de compras.
        /// </summary>
        private void btnCompra_Click(object sender, EventArgs e)
        {
            Compras CF = new Compras();
            CF.Show();
            this.Hide();
        }

        /// <summary>
        /// Navega al formulario de clientes del administrador.
        /// </summary>
        private void btnClientes_Click(object sender, EventArgs e)
        {
            ClientesAdm CA = new ClientesAdm();
            CA.Show();
            this.Hide();
        }

        /// <summary>
        /// Navega al formulario de inventario del administrador.
        /// </summary>
        private void btnInventario_Click(object sender, EventArgs e)
        {
            InventarioAdmin IA = new InventarioAdmin();
            IA.Show();
            this.Hide();
        }

        /// <summary>
        /// Navega al formulario de proveedores del administrador.
        /// </summary>
        private void btnProveedores_Click(object sender, EventArgs e)
        {
            ProveedoresAdmin PA = new ProveedoresAdmin();
            PA.Show();
            this.Hide();
        }

        /// <summary>
        /// Navega al formulario de deudores del administrador.
        /// </summary>
        private void btnDeudores_Click(object sender, EventArgs e)
        {
            DeudoresAdmin DA = new DeudoresAdmin();
            DA.Show();
            this.Hide();
        }

        /// <summary>
        /// Navega al formulario de reportes del administrador.
        /// </summary>
        private void btnReportes_Click(object sender, EventArgs e)
        {
            ReportesAdmin RA = new ReportesAdmin();
            RA.Show();
            this.Hide();
        }

        /// <summary>
        /// Cierra la sesión actual y regresa al formulario de inicio de sesión.
        /// </summary>
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Login.Login login = new Login.Login();
            login.Show();
            this.Close();
        }

        /// <summary>
        /// Abre el formulario de perfil del usuario actual.
        /// </summary>
        private void btnPerfil_Click(object sender, EventArgs e)
        {
            Perfil perfil = new Perfil();
            perfil.Show();
        }
    }
}