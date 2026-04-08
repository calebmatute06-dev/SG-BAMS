using Microsoft.Data.SqlClient;
using SG_BAMS.Bitacora;
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
    /// <summary>
    /// Formulario para la gestión y visualización de compras.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class Compras : Form
    {
        /// <summary>
        /// Lógica para la modificación de compras.
        /// </summary>
        private ClsModificarCompras logic = new ClsModificarCompras();

        /// <summary>
        /// Lógica para la consulta de compras.
        /// </summary>
        private ClsMostrarCompras consultaLogic = new ClsMostrarCompras();

        /// <summary>
        /// Tabla de datos que contiene las compras.
        /// </summary>
        private DataTable dtCompras;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Compras" />.
        /// </summary>
        public Compras()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            CargarCompras();

            dtpDesde.MaxDate = DateTime.Today;
            dtpHasta.MaxDate = DateTime.Today;

            // Rango predeterminado: última semana
            dtpDesde.Value = DateTime.Today.AddDays(-7);
            dtpHasta.Value = DateTime.Today;

            // Eventos de búsqueda y filtrado
            txtBuscarCompra.TextChanged += (s, e) => FiltrarCompras();
            txtBuscarCompra.KeyPress += (s, e) => ClsValidaciones.ValidarBusquedaAlfanumerica(e);

            dtpDesde.ValueChanged += dtpDesde_ValueChanged;
            dtpHasta.ValueChanged += dtpHasta_ValueChanged;
        }

        /// <summary>
        /// Maneja el evento de carga (Load) del control Compras.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void Compras_Load(object sender, EventArgs e)
        {
            btnComprasMenu.Enabled = false;
            btnComprasMenu.BackColor = Color.SkyBlue;
            btnComprasMenu.ForeColor = Color.White;

            // Configuración visual del DataGridView
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
        }

        /// <summary>
        /// Carga los datos de las compras desde la lógica de consulta.
        /// </summary>
        public void CargarCompras()
        {
            try
            {
                dtCompras = consultaLogic.ListarCompras();
                dgvComprasAdmin.DataSource = dtCompras;
                dgvComprasAdmin.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvComprasAdmin.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar compras: " + ex.Message);
            }
        }

        /// <summary>
        /// Filtra las compras basándose en el texto de búsqueda o el rango de fechas.
        /// </summary>
        private void FiltrarCompras()
        {
            if (dtCompras == null) return;

            DataView dv = dtCompras.DefaultView;

            string texto = txtBuscarCompra.Text
                .Replace("'", "''")
                .Replace("[", "[[]")
                .Replace("]", "[]]")
                .Trim();

            string rowFilter;

            if (!string.IsNullOrWhiteSpace(texto))
            {
                // Si hay texto, busca en todas las columnas de texto e identificadores numéricos
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

                rowFilter = condicionesTexto.Count > 0
                    ? string.Join(" OR ", condicionesTexto)
                    : string.Empty;
            }
            else
            {
                // Si no hay texto, filtra únicamente por el rango de fechas de los DateTimePickers
                string fDesde = dtpDesde.Value.Date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                string fHasta = dtpHasta.Value.Date.AddDays(1).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

                var condicionesFecha = new List<string>();
                foreach (DataColumn col in dtCompras.Columns)
                {
                    if (col.DataType == typeof(DateTime))
                        condicionesFecha.Add(
                            $"[{col.ColumnName}] >= #{fDesde}# AND [{col.ColumnName}] < #{fHasta}#"
                        );
                }

                rowFilter = condicionesFecha.Count > 0
                    ? string.Join(" AND ", condicionesFecha)
                    : string.Empty;
            }

            try
            {
                dv.RowFilter = rowFilter;
                dgvComprasAdmin.DataSource = dv;
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

        /// <summary>
        /// Reinicia los filtros y vuelve a cargar los datos.
        /// </summary>
        private void btnRefresh_Click_1(object sender, EventArgs e)
        {
            txtBuscarCompra.Clear();

            dtpDesde.ValueChanged -= dtpDesde_ValueChanged;
            dtpHasta.ValueChanged -= dtpHasta_ValueChanged;

            dtpDesde.MaxDate = DateTime.Today;
            dtpHasta.MaxDate = DateTime.Today;
            dtpDesde.Value = DateTime.Today.AddDays(-7);
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
            notificacionesAdmin.Show();
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
                            CargarCompras();
                            FiltrarCompras();
                            txtBuscarCompra.Clear();
                            dgvComprasAdmin.ClearSelection();
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

        // --- Navegación del Menú ---

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

        private void btnBitacora_Click(object sender, EventArgs e)
        {
            BitacoraAdmin BA = new BitacoraAdmin();
            BA.Show();
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