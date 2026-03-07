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
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using SG_BAMS.Proveedor;

namespace SG_BAMS.Bitacora
{
    public partial class BitacoraAdmin : Form
    {
        ClsBitacora bitacora = new ClsBitacora();

        public BitacoraAdmin()
        {
            InitializeComponent();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {

        }

        private void Bitacora_Load(object sender, EventArgs e)
        {
            bitacora.cargarDatos(dgvBitacora);
            bitacora.BuscarBitacora(txtBuscar, dtpDesde.Value, dtpHasta.Value, dgvBitacora);
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            bitacora.BuscarBitacora(txtBuscar, dtpDesde.Value, dtpHasta.Value, dgvBitacora);
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            ProveedoresAdmin proveedores = new ProveedoresAdmin();
            proveedores.Show();
            this.Hide();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            dtpDesde.Value = DateTime.Today;
            dtpHasta.Value = DateTime.Today;
            bitacora.cargarDatos(dgvBitacora);
            bitacora.BuscarBitacora(txtBuscar, dtpDesde.Value, dtpHasta.Value, dgvBitacora);
        }

        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {
            bitacora.BuscarBitacora(txtBuscar, dtpDesde.Value, dtpHasta.Value, dgvBitacora);
        }

        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {
            bitacora.BuscarBitacora(txtBuscar, dtpDesde.Value, dtpHasta.Value, dgvBitacora);
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            MenuPrincipalAdm menu = new MenuPrincipalAdm();
            menu.Show();
            this.Hide();
        }

        private void btnFacturas_Click(object sender, EventArgs e)
        {
            FacturasAdm facturas = new FacturasAdm();
            facturas.Show();
            this.Hide();
        }

        private void btnCompras_Click(object sender, EventArgs e)
        {
            Compras compras = new Compras();
            compras.Show();
            this.Hide();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            ClientesAdm clientes = new ClientesAdm();
            clientes.Show();
            this.Hide();
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            InventarioAdmin inventario = new InventarioAdmin();
            inventario.Show();
            this.Hide();
        }

        private void btnDeudores_Click(object sender, EventArgs e)
        {
            DeudoresAdmin deudores = new DeudoresAdmin();
            deudores.Show();
            this.Hide();
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                QuestPDF.Settings.License = LicenseType.Community;

                // 1) Pasar el DataGridView a lista
                List<BitacoraDTO> lista = new List<BitacoraDTO>();

                foreach (DataGridViewRow row in dgvBitacora.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        lista.Add(new BitacoraDTO
                        {
                            Nombre = row.Cells["Nombre"].Value?.ToString(),
                            Accion = row.Cells["Acción"].Value?.ToString(), // con tilde
                            Modulo = row.Cells["Modulo"].Value?.ToString(),
                            Fecha = Convert.ToDateTime(row.Cells["Fecha"].Value)
                        });
                    }
                }

                // 2) Generar PDF en TEMP (no Documentos)
                string rutaTemp = Path.Combine(
                    Path.GetTempPath(),
                    $"ReporteBitacora_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
                );

                var documento = new SG_BAMS.Bitacora.ReporteBitacora(lista);
                documento.GeneratePdf(rutaTemp);

                // 3) Abrir con el programa predeterminado (normalmente navegador/visor PDF)
                Process.Start(new ProcessStartInfo
                {
                    FileName = rutaTemp,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar/abrir el PDF: " + ex.Message);
            }
        }
    }
}
