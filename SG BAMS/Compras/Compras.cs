using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using SG_BAMS.Bitacora;
using SG_BAMS.ComprasContratos;
using SG_BAMS.ProductoInventario;
using SG_BAMS.Proveedor;
using SG_BAMS.Reporte;

namespace SG_BAMS
{
    public partial class Compras : Form
    {
        private readonly IModificarComprasRepository _logic;
        private readonly IMostrarComprasRepository _consultaLogic;
        private readonly NavegacionService _navegacion;
        private DataTable _dtCompras = new DataTable();
        public Compras() : this(new ClsModificarCompras(), new ClsMostrarCompras(), new NavegacionService()) { }

        public Compras(IModificarComprasRepository logic, IMostrarComprasRepository consultaLogic, NavegacionService navegacion)
        {
            _logic = logic;
            _consultaLogic = consultaLogic;
            _navegacion = navegacion;

            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            ConfigurarLimitesFechas();
            SuscribirEventosControles();
        }

        private void Compras_Load(object sender, EventArgs e)
        {
            new PlaceholderTextBox(txtBuscarCompra, "Ingrese un Nombre de Comprador, Forma de pago, N.Compra");
            ConfigurarBotonMenuActivo();
            EstiloDataGridView.Aplicar(dgvComprasAdmin);

            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            CargarCompras();
            FiltrarCompras();

            ClsMensajeGuia.ActivarK(txtBuscarCompra);
            this.ActiveControl = null;
        }

        private void ConfigurarLimitesFechas()
        {
            dtpDesde.MaxDate = DateTime.Today;
            dtpHasta.MaxDate = DateTime.Today;
            dtpDesde.Value = DateTime.Today.AddMonths(-1);
            dtpHasta.Value = DateTime.Today;
        }

        private void SuscribirEventosControles()
        {
            txtBuscarCompra.TextChanged += (s, e) => FiltrarCompras();
            txtBuscarCompra.KeyPress += (s, e) => ClsValidaciones.ValidarBusquedaAlfanumerica(e);
            dtpDesde.ValueChanged += dtpDesde_ValueChanged;
            dtpHasta.ValueChanged += dtpHasta_ValueChanged;
        }

        private void ConfigurarBotonMenuActivo()
        {
            btnComprasMenu.Enabled = false;
            btnComprasMenu.BackColor = Color.SkyBlue;
            btnComprasMenu.ForeColor = Color.White;
        }

        public void CargarCompras()
        {
            try
            {
                _dtCompras = _consultaLogic.ListarCompras();
                dgvComprasAdmin.DataSource = _dtCompras.DefaultView;
                dgvComprasAdmin.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar compras: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FiltrarCompras()
        {
            if (_dtCompras == null || _dtCompras.Rows.Count == 0) return;

            string texto = txtBuscarCompra.Text?.Trim() ?? string.Empty;
            if (texto == "Ingrese un Nombre de Vendedor, Cliente, N.Factura, RTN" || texto == "Ingrese un Nombre de Comprador, Forma de pago, N.Compra")
            {
                texto = string.Empty;
            }
            else
            {
                texto = texto.Replace("'", "''").Replace("[", "[[]").Replace("]", "[]]").Trim();
            }

            var condiciones = new List<string>();

            if (!string.IsNullOrWhiteSpace(texto))
            {
                var condicionesTexto = new List<string>();
                foreach (DataColumn col in _dtCompras.Columns)
                {
                    if (col.DataType == typeof(string))
                    {
                        condicionesTexto.Add($"[{col.ColumnName}] LIKE '%{texto}%'");
                    }
                    else if (col.DataType == typeof(int) || col.DataType == typeof(decimal) || col.DataType == typeof(double) || col.DataType == typeof(long))
                    {
                        if (decimal.TryParse(texto, out _))
                            condicionesTexto.Add($"CONVERT([{col.ColumnName}], System.String) LIKE '%{texto}%'");
                    }
                }

                if (condicionesTexto.Count > 0) condiciones.Add("(" + string.Join(" OR ", condicionesTexto) + ")");
            }

            string fDesde = dtpDesde.Value.Date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            string fHasta = dtpHasta.Value.Date.AddDays(1).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

            var condicionesFecha = new List<string>();
            foreach (DataColumn col in _dtCompras.Columns)
            {
                if (col.DataType == typeof(DateTime))
                {
                    condicionesFecha.Add($"[{col.ColumnName}] >= #{fDesde}# AND [{col.ColumnName}] < #{fHasta}#");
                }
            }

            if (condicionesFecha.Count > 0) condiciones.Add("(" + string.Join(" OR ", condicionesFecha) + ")");

            try
            {
                _dtCompras.DefaultView.RowFilter = condiciones.Count > 0 ? string.Join(" AND ", condiciones) : string.Empty;
                dgvComprasAdmin.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {
            if (dtpDesde.Value > DateTime.Today) dtpDesde.Value = DateTime.Today;
            if (dtpDesde.Value > dtpHasta.Value) dtpDesde.Value = dtpHasta.Value;
            FiltrarCompras();
        }

        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {
            if (dtpHasta.Value > DateTime.Today) dtpHasta.Value = DateTime.Today;
            if (dtpHasta.Value < dtpDesde.Value) dtpHasta.Value = dtpDesde.Value;
            FiltrarCompras();
        }

        private void btnRefresh_Click_1(object sender, EventArgs e)
        {
            txtBuscarCompra.Clear();

            dtpDesde.ValueChanged -= dtpDesde_ValueChanged;
            dtpHasta.ValueChanged -= dtpHasta_ValueChanged;

            ConfigurarLimitesFechas();

            dtpDesde.ValueChanged += dtpDesde_ValueChanged;
            dtpHasta.ValueChanged += dtpHasta_ValueChanged;

            CargarCompras();
            FiltrarCompras();
        }

        private void btnNoti_Click(object sender, EventArgs e)
        {
            using (NotificacionesAdmin notificacionesAdmin = new NotificacionesAdmin())
            {
                notificacionesAdmin.ShowDialog();
            }
        }

        private void dgvComprasAdmin_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            EjecutarModificacionCompra();
        }

        private void btnModificarC_Click(object sender, EventArgs e) => EjecutarModificacionCompra();

        private void EjecutarModificacionCompra()
        {
            if (dgvComprasAdmin.SelectedRows.Count > 0 && dgvComprasAdmin.CurrentRow != null)
            {
                int idSeleccionado = Convert.ToInt32(dgvComprasAdmin.CurrentRow.Cells["ID"].Value);
                using (Modificar_datos__Compra_ frmModificar = new Modificar_datos__Compra_(idSeleccionado))
                {
                    frmModificar.ShowDialog();
                }
                CargarCompras();
                FiltrarCompras();
                txtBuscarCompra.Clear();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una compra de la lista.", "BAMS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnCompra_Click_1(object sender, EventArgs e)
        {
            using (Ingresar_datos__Compra_ frmNuevaCompra = new Ingresar_datos__Compra_())
            {
                frmNuevaCompra.ShowDialog();
            }
            CargarCompras();
            FiltrarCompras();
            txtBuscarCompra.Clear();
        }

        private void btnEliminarC_Click(object sender, EventArgs e)
        {
            if (dgvComprasAdmin.SelectedRows.Count > 0)
            {
                int idCompra = Convert.ToInt32(dgvComprasAdmin.SelectedRows[0].Cells["ID"].Value);
                DialogResult resultado = MessageBox.Show("¿Está seguro de que desea eliminar permanentemente esta compra?", "Confirmar Eliminación - BAMS", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (resultado == DialogResult.Yes)
                {
                    try
                    {
                        if (_logic.EliminarCompraCompleta(idCompra))
                        {
                            MessageBox.Show("Compra eliminada correctamente.", "BAMS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarCompras();
                            FiltrarCompras();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No se pudo eliminar la compra: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una compra de la lista.", "BAMS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        // Implementación del servicio unificado de navegación siguiendo el formato enviado por captura
        private void btnMenu_Click(object sender, EventArgs e) => _navegacion.IrA(this, new MenuPrincipalAdm());
        private void btnFacturas_Click(object sender, EventArgs e) => _navegacion.IrA(this, new FacturasAdm());
        private void btnClientes_Click(object sender, EventArgs e) => _navegacion.IrA(this, new ClientesAdm());
        private void btnInventario_Click(object sender, EventArgs e)
        {
            InventarioAdmin IA = new InventarioAdmin(new ProductoInventario.ProductoRepository(), new ProductoInventario.ComboRepository());
            IA.Show();
            this.Hide();
        }

        private void btnProveedores_Click(object sender, EventArgs e) =>
            _navegacion.IrA(this, new ProveedoresAdmin(new ProveedorRepository(), new EstadoRepository(), new ClasificacionRepository()));

        private void btnDeudores_Click(object sender, EventArgs e)
        {
            DeudoresAdmin DA = new DeudoresAdmin(new DeudaRepository());
            DA.Show();
            this.Hide();
        }

        private void btnReportes_Click(object sender, EventArgs e) => _navegacion.IrA(this, new ReportesAdmin());

        private void btnBitacora_Click(object sender, EventArgs e) =>
            _navegacion.IrA(this, new BitacoraAdmin(new BitacoraRepository(), new FiltroBitacoraService(), new ReporteBitacoraPdfExportador()));

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("¿Está seguro que desea cerrar sesión?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resultado == DialogResult.Yes)
            {
                _navegacion.IrA(this, new Login.Login());
            }
        }

        private void btnPerfil_Click(object sender, EventArgs e)
        {
            using (Perfil perfil = new Perfil())
            {
                perfil.ShowDialog();
            }
        }
    }
}