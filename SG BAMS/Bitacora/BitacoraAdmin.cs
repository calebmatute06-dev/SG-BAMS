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
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class BitacoraAdmin : Form
    {
        /// <summary>
        /// The bitacora
        /// </summary>
        ClsBitacora bitacora = new ClsBitacora();

        /// <summary>
        /// Initializes a new instance of the <see cref="BitacoraAdmin"/> class.
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
        /// Handles the Load event of the Bitacora control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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
        }

        /// <summary>
        /// Handles the KeyUp event of the txtBuscar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {

            EjecutarBusquedaSegura();
        }

        /// <summary>
        /// Handles the ValueChanged event of the dtpDesde control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {

            if (dtpDesde.Value > DateTime.Today)
            {
                dtpDesde.Value = DateTime.Today;
            }


            if (dtpDesde.Value > dtpHasta.Value)
            {
                dtpDesde.Value = dtpHasta.Value;
            }
            EjecutarBusquedaSegura();
        }

        /// <summary>
        /// Handles the ValueChanged event of the dtpHasta control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {

            if (dtpHasta.Value > DateTime.Today)
            {
                dtpHasta.Value = DateTime.Today;
            }


            if (dtpHasta.Value < dtpDesde.Value)
            {
                dtpHasta.Value = dtpDesde.Value;
            }
            EjecutarBusquedaSegura();
        }

        /// <summary>
        /// Ejecutars the busqueda segura.
        /// </summary>
        private void EjecutarBusquedaSegura()
        {
            bitacora.BuscarBitacora(txtBuscar, dtpDesde.Value, dtpHasta.Value, dgvBitacora);
        }

        /// <summary>
        /// Handles the Click event of the btnRefresh control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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
        /// Handles the Click event of the btnExportar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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
        /// Handles the Click event of the btnNoti control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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