using Microsoft.Data.SqlClient;
using SG_BAMS.Bitacora;
using SG_BAMS.ComprasContratos;
using SG_BAMS.ProductoInventario;
using SG_BAMS.Proveedor;
using SG_BAMS.Reporte;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS
{
    public partial class Compras : Form
    {
        private readonly IModificarComprasRepository logic;
        private readonly IMostrarComprasRepository consultaLogic;
        private DataTable dtCompras = new DataTable();

        public Compras() : this(new ClsModificarCompras(), new ClsMostrarCompras()) { }

        public Compras(IModificarComprasRepository logic, IMostrarComprasRepository consultaLogic)
        {
            this.logic = logic;
            this.consultaLogic = consultaLogic;

            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            dtpDesde.MaxDate = DateTime.Today;
            dtpHasta.MaxDate = DateTime.Today;

            dtpDesde.Value = DateTime.Today.AddMonths(-1);
            dtpHasta.Value = DateTime.Today;

            CargarCompras();

            txtBuscarCompra.TextChanged += (s, e) => FiltrarCompras();
            txtBuscarCompra.KeyPress += (s, e) => ClsValidaciones.ValidarBusquedaAlfanumerica(e);

            dtpDesde.ValueChanged += dtpDesde_ValueChanged;
            dtpHasta.ValueChanged += dtpHasta_ValueChanged;
        }

        private void Compras_Load(object sender, EventArgs e)
        {
            new PlaceholderTextBox(txtBuscarCompra, "Ingrese un Nombre de Comprador,  Forma de pago, N.Compra");
            btnComprasMenu.Enabled = false;
            btnComprasMenu.BackColor = Color.SkyBlue;
            btnComprasMenu.ForeColor = Color.White;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            dgvComprasAdmin.ClearSelection();
            dgvComprasAdmin.BorderStyle = BorderStyle.None;
            dgvComprasAdmin.BackgroundColor = Color.White;
            dgvComprasAdmin.RowHeadersVisible = false;
            dgvComprasAdmin.EnableHeadersVisualStyles = false;
            dgvComprasAdmin.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgvComprasAdmin.ColumnHeadersDefaultCellStyle.BackColor = Color.SkyBlue;
            dgvComprasAdmin.ColumnHeadersDefaultCellStyle.ForeColor = Color.Navy;
            dgvComprasAdmin.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvComprasAdmin.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvComprasAdmin.ColumnHeadersHeight = 28;

            dgvComprasAdmin.DefaultCellStyle.BackColor = Color.White;
            dgvComprasAdmin.DefaultCellStyle.ForeColor = Color.Navy;
            dgvComprasAdmin.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvComprasAdmin.DefaultCellStyle.Padding = new Padding(3);
            dgvComprasAdmin.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 245, 255);
            dgvComprasAdmin.AlternatingRowsDefaultCellStyle.ForeColor = Color.Navy;

            dgvComprasAdmin.DefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            dgvComprasAdmin.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvComprasAdmin.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvComprasAdmin.GridColor = Color.LightGray;
            dgvComprasAdmin.RowTemplate.Height = 32;
            dgvComprasAdmin.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvComprasAdmin.ClearSelection();

            FiltrarCompras();

            ClsMensajeGuia.ActivarK(txtBuscarCompra);
            this.ActiveControl = null;
        }

        public void CargarCompras()
        {
            try
            {
                dtCompras = consultaLogic.ListarCompras();
                // Enlazamos directamente a la vista por defecto para mantener la reactividad activa
                dgvComprasAdmin.DataSource = dtCompras.DefaultView;
                dgvComprasAdmin.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvComprasAdmin.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar compras: " + ex.Message);
            }
        }

        private void FiltrarCompras()
        {
            if (dtCompras == null || dtCompras.Rows.Count == 0) return;

            string texto = txtBuscarCompra.Text?.Trim() ?? "";

            if (texto == "Ingrese un Nombre de Vendedor, Cliente, N.Factura, RTN" || texto == "Ingrese un Nombre de Comprador,  Forma de pago, N.Compra")
            {
                texto = "";
            }
            else
            {
                texto = texto
                    .Replace("'", "''")
                    .Replace("[", "[[]")
                    .Replace("]", "[]]")
                    .Trim();
            }

            var condiciones = new List<string>();

            if (!string.IsNullOrWhiteSpace(texto))
            {
                var condicionesTexto = new List<string>();
                foreach (DataColumn col in dtCompras.Columns)
                {
                    if (col.DataType == typeof(string))
                    {
                        condicionesTexto.Add($"[{col.ColumnName}] LIKE '%{texto}%'");
                    }
                    else if (col.DataType == typeof(int) || col.DataType == typeof(decimal) ||
                             col.DataType == typeof(double) || col.DataType == typeof(long))
                    {
                        if (decimal.TryParse(texto, out _))
                            condicionesTexto.Add($"CONVERT([{col.ColumnName}], System.String) LIKE '%{texto}%'");
                    }
                }

                if (condicionesTexto.Count > 0)
                {
                    condiciones.Add("(" + string.Join(" OR ", condicionesTexto) + ")");
                }
            }

            string fDesde = dtpDesde.Value.Date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            string fHasta = dtpHasta.Value.Date.AddDays(1).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

            var condicionesFecha = new List<string>();
            foreach (DataColumn col in dtCompras.Columns)
            {
                if (col.DataType == typeof(DateTime))
                {
                    condicionesFecha.Add(
                        $"[{col.ColumnName}] >= #{fDesde}# AND [{col.ColumnName}] < #{fHasta}#"
                    );
                }
            }

            if (condicionesFecha.Count > 0)
            {
                condiciones.Add("(" + string.Join(" OR ", condicionesFecha) + ")");
            }

            string rowFilter = condiciones.Count > 0
                ? string.Join(" AND ", condiciones)
                : string.Empty;

            try
            {
                // Se altera directamente la propiedad RowFilter del origen enlazado
                dtCompras.DefaultView.RowFilter = rowFilter;
                dgvComprasAdmin.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBuscarCompra_TextChanged(object sender, EventArgs e)
        {
            FiltrarCompras();
        }

        private void txtBuscarCompra_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.ValidarBusquedaAlfanumerica(e);
        }

        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {
            if (dtpDesde.Value > DateTime.Today)
                dtpDesde.Value = DateTime.Today;

            if (dtpDesde.Value > dtpHasta.Value)
                dtpDesde.Value = dtpHasta.Value;

            FiltrarCompras();
        }

        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {
            if (dtpHasta.Value > DateTime.Today)
                dtpHasta.Value = DateTime.Today;

            if (dtpHasta.Value < dtpDesde.Value)
                dtpHasta.Value = dtpDesde.Value;

            FiltrarCompras();
        }

        private void btnRefresh_Click_1(object sender, EventArgs e)
        {
            txtBuscarCompra.Clear();

            dtpDesde.ValueChanged -= dtpDesde_ValueChanged;
            dtpHasta.ValueChanged -= dtpHasta_ValueChanged;

            dtpDesde.MaxDate = DateTime.Today;
            dtpHasta.MaxDate = DateTime.Today;
            dtpDesde.Value = DateTime.Today.AddMonths(-1);
            dtpHasta.Value = DateTime.Today;

            dtpDesde.ValueChanged += dtpDesde_ValueChanged;
            dtpHasta.ValueChanged += dtpHasta_ValueChanged;

            CargarCompras();
            FiltrarCompras();
            dgvComprasAdmin.ClearSelection();
        }

        private void btnNoti_Click(object sender, EventArgs e)
        {
            NotificacionesAdmin notificacionesAdmin = new NotificacionesAdmin();
            notificacionesAdmin.ShowDialog();
        }

        private void dgvComprasAdmin_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvComprasAdmin_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvComprasAdmin.SelectedRows.Count > 0)
            {
                int idSeleccionado = Convert.ToInt32(dgvComprasAdmin.CurrentRow.Cells["ID"].Value);
                Modificar_datos__Compra_ frmModificar = new Modificar_datos__Compra_(idSeleccionado);
                frmModificar.ShowDialog();
                CargarCompras();
                FiltrarCompras();
                txtBuscarCompra.Clear();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una compra de la lista.");
            }
        }

        private void btnCompra_Click_1(object sender, EventArgs e)
        {
            Ingresar_datos__Compra_ frmNuevaCompra = new Ingresar_datos__Compra_();
            frmNuevaCompra.ShowDialog();
            CargarCompras();
            FiltrarCompras();
            txtBuscarCompra.Clear();
        }

        private void btnModificarC_Click(object sender, EventArgs e)
        {
            if (dgvComprasAdmin.SelectedRows.Count > 0)
            {
                int idSeleccionado = Convert.ToInt32(dgvComprasAdmin.CurrentRow.Cells["ID"].Value);
                Modificar_datos__Compra_ frmModificar = new Modificar_datos__Compra_(idSeleccionado);
                frmModificar.ShowDialog();
                CargarCompras();
                FiltrarCompras();
                txtBuscarCompra.Clear();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una compra de la lista.",
                                "BAMS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnEliminarC_Click(object sender, EventArgs e)
        {
            if (dgvComprasAdmin.SelectedRows.Count > 0)
            {
                int idCompra = Convert.ToInt32(dgvComprasAdmin.SelectedRows[0].Cells["ID"].Value);

                DialogResult resultado = MessageBox.Show("¿Está seguro de que desea eliminar permanentemente esta compra?",
                                                        "Confirmar Eliminación - BAMS",
                                                        MessageBoxButtons.YesNo,
                                                        MessageBoxIcon.Warning);

                if (resultado == DialogResult.Yes)
                {
                    try
                    {
                        if (logic.EliminarCompraCompleta(idCompra))
                        {
                            MessageBox.Show("Compra eliminada correctamente.", "BAMS",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Limpieza e invocación directa secuencial sin romper el puntero DataSource
                            CargarCompras();
                            FiltrarCompras();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No se pudo eliminar la compra: " + ex.Message,
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una compra de la lista.",
                                "BAMS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

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
            var PA = new ProveedoresAdmin(
                new ProveedorRepository(),
                new EstadoRepository(),
                new ClasificacionRepository());
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

        private void btnBitacora_Click(object sender, EventArgs e)
        {
            var Bi = new BitacoraAdmin(
                new BitacoraRepository(),
                new FiltroBitacoraService(),
                new ReporteBitacoraPdfExportador());
            Bi.Show();
            this.Hide();
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
    }
}