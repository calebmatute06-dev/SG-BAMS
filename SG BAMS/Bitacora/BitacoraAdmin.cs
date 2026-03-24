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

namespace SG_BAMS.Bitacora
{
    public partial class BitacoraAdmin : Form
    {
        ClsBitacora bitacora = new ClsBitacora();

        public BitacoraAdmin()
        {
            InitializeComponent();

           
            dtpDesde.MaxDate = DateTime.Today;
            dtpHasta.MaxDate = DateTime.Today;

            txtBuscar.KeyPress += (s, e) => ClsValidaciones.ValidarBusquedaAlfanumerica(e);
        }

        private void Bitacora_Load(object sender, EventArgs e)
        {
            bitacora.cargarDatos(dgvBitacora);
            EjecutarBusquedaSegura();
        }

        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            
            EjecutarBusquedaSegura();
        }

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

        private void EjecutarBusquedaSegura()
        {
            bitacora.BuscarBitacora(txtBuscar, dtpDesde.Value, dtpHasta.Value, dgvBitacora);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
           
            dtpDesde.MaxDate = DateTime.Today;
            dtpHasta.MaxDate = DateTime.Today;
            dtpDesde.Value = DateTime.Today;
            dtpHasta.Value = DateTime.Today;

            bitacora.cargarDatos(dgvBitacora);
            EjecutarBusquedaSegura();
        }

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

        
        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            new Login.Login().Show();
            this.Close();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            new MenuPrincipalAdm().Show();
            this.Hide();
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            new ProveedoresAdmin().Show();
            this.Hide();
        }

        private void btnFacturas_Click(object sender, EventArgs e)
        {
            new FacturasAdm().Show();
            this.Hide();
        }

        private void btnCompras_Click(object sender, EventArgs e)
        {
            new Compras().Show();
            this.Hide();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            new ClientesAdm().Show();
            this.Hide();
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            new InventarioAdmin().Show();
            this.Hide();
        }

        private void btnDeudores_Click(object sender, EventArgs e)
        {
            new DeudoresAdmin().Show();
            this.Hide();
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            new ReportesAdmin().Show();
            this.Hide();
        }

        private void btnNoti_Click(object sender, EventArgs e) => new NotificacionesAdmin().Show();

        private void btnPerfil_Click(object sender, EventArgs e) => new Perfil().Show();
    }
}