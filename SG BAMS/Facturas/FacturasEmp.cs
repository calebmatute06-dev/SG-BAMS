using SG_BAMS.Facturas;
using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace SG_BAMS
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FacturasEmp : Form
    {
        /// <summary>
        /// The datos fac
        /// </summary>
        private DataTable datosFac;

        /// <summary>
        /// Initializes a new instance of the <see cref="FacturasEmp"/> class.
        /// </summary>
        public FacturasEmp()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            ConfigurarGrid();
        }

        /// <summary>
        /// Configurars the grid.
        /// </summary>
        private void ConfigurarGrid()
        {
            dgvFacturas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvFacturas.MultiSelect = false;
            dgvFacturas.AllowUserToAddRows = false;
            dgvFacturas.ReadOnly = true;
            dgvFacturas.AllowUserToOrderColumns = false;
            dgvFacturas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            txtBusqueda.KeyPress += (s, e) => ClsValidaciones.ValidarBusquedaAlfanumerica(e);
        }

        /// <summary>
        /// Cargars the factura.
        /// </summary>
        private async Task CargarFactura()
        {
            try
            {
                ClsVerFactura objFac = new ClsVerFactura();
                datosFac = await objFac.VerFacturas();

                if (datosFac != null)
                {
                    dgvFacturas.DataSource = datosFac;

                    dgvFacturas.Columns["Factura"].HeaderText = "N° Factura";
                    dgvFacturas.Columns["ID Método de Pago"].Visible = false;
                    dgvFacturas.Columns["Rebaja"].HeaderText = "Rebaja de Batería Vieja";

                    dgvFacturas.Columns["Rebaja"].DisplayIndex = 8;
                    dgvFacturas.Columns["Batería Vieja"].DisplayIndex = 7;


                    dgvFacturas.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar facturas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the Load event of the FacturasEmp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void FacturasEmp_Load(object sender, EventArgs e)
        {
            btnFacturas.Enabled = false;
            btnFacturas.BackColor = Color.SkyBlue;
            btnFacturas.ForeColor = Color.White;

            await CargarFactura();

            dtpInicio.Value = DateTime.Today;
            dtpFin.Value = DateTime.Today;

            ClsValidaciones.ValidarRangoFechas(dtpInicio, dtpFin);
            FiltrarDatos();

            dtpInicio.ValueChanged += (s, ev) => ValidarYFiltrar();
            dtpFin.ValueChanged += (s, ev) => ValidarYFiltrar();

            dgvFacturas.BorderStyle = BorderStyle.None;
            dgvFacturas.BackgroundColor = Color.White;
            dgvFacturas.RowHeadersVisible = false;
            dgvFacturas.EnableHeadersVisualStyles = false;
            dgvFacturas.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgvFacturas.ColumnHeadersDefaultCellStyle.BackColor = Color.SkyBlue;
            dgvFacturas.ColumnHeadersDefaultCellStyle.ForeColor = Color.Navy;
            dgvFacturas.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvFacturas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvFacturas.ColumnHeadersHeight = 28;

            dgvFacturas.DefaultCellStyle.BackColor = Color.White;
            dgvFacturas.DefaultCellStyle.ForeColor = Color.Navy;
            dgvFacturas.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvFacturas.DefaultCellStyle.Padding = new Padding(3);
            dgvFacturas.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 245, 255);
            dgvFacturas.AlternatingRowsDefaultCellStyle.ForeColor = Color.Navy;

            dgvFacturas.DefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            dgvFacturas.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvFacturas.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvFacturas.GridColor = Color.LightGray;
            dgvFacturas.RowTemplate.Height = 32;
            dgvFacturas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvFacturas.ClearSelection();
            ClsMensajeGuia.Activar(txtBusqueda);

        }

        /// <summary>
        /// Validars the y filtrar.
        /// </summary>
        private void ValidarYFiltrar()
        {
            ClsValidaciones.ValidarRangoFechas(dtpInicio, dtpFin);

            if (dtpFin.Value < dtpInicio.Value)
                dtpFin.Value = dtpInicio.Value;

            FiltrarDatos();
        }

        /// <summary>
        /// Filtrars the datos.
        /// </summary>
        private void FiltrarDatos()
        {
            if (datosFac == null) return;

            DataView dv = datosFac.DefaultView;

            string texto = txtBusqueda.Text.Replace("'", "''").Replace("[", "[[]").Replace("]", "[]]");

            string rowFilter = "";


            if (string.IsNullOrWhiteSpace(texto))
            {
                string fInicio = dtpInicio.Value.Date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                string fFin = dtpFin.Value.Date.AddDays(1).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                rowFilter = $"[Fecha] >= #{fInicio}# AND [Fecha] < #{fFin}#";
            }
            else
            {
                rowFilter = $"Convert([Factura], 'System.String') LIKE '%{texto}%' OR " +
                            $"[Vendedor] LIKE '%{texto}%' OR " +
                            $"[Cliente] LIKE '%{texto}%' OR " +
                            $"[Método de Pago] LIKE '%{texto}%' OR " +
                            $"[RTN Cliente] LIKE '%{texto}%'";
            }

            try
            {
                dv.RowFilter = rowFilter;
                dgvFacturas.DataSource = dv;
                dgvFacturas.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al filtrar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the txtBusqueda control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txtBusqueda_TextChanged(object sender, EventArgs e) => FiltrarDatos();

        /// <summary>
        /// Handles the Click event of the BtnNueva control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void BtnNueva_Click(object sender, EventArgs e)
        {
            using (ClienteAgregar frmCA = new ClienteAgregar())
            {
                if (frmCA.ShowDialog() == DialogResult.OK)
                {
                    await CargarFactura();

                    dtpInicio.Value = DateTime.Today;
                    dtpFin.Value = DateTime.Today;
                    FiltrarDatos();
                    dgvFacturas.ClearSelection();
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the BtnVer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BtnVer_Click(object sender, EventArgs e)
        {
            if (dgvFacturas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar una fila", "Ninguna fila seleccionada",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvFacturas.CurrentRow != null)
                dgvFacturas_CellDoubleClick(null, null);
        }

        /// <summary>
        /// Handles the CellDoubleClick event of the dgvFacturas control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private async void dgvFacturas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e != null && e.RowIndex < 0) return;

            if (dgvFacturas.CurrentRow != null)
            {
                int idFacturas = Convert.ToInt32(dgvFacturas.CurrentRow.Cells["Factura"].Value);
                string nombre_Cliente = dgvFacturas.CurrentRow.Cells["Cliente"].Value.ToString();
                DateTime fecha = Convert.ToDateTime(dgvFacturas.CurrentRow.Cells["Fecha"].Value);
                int idPago = Convert.ToInt32(dgvFacturas.CurrentRow.Cells["ID Método de Pago"].Value);

                int bateriaVieja = 0;
                var valorBateria = dgvFacturas.CurrentRow.Cells["Batería Vieja"].Value?.ToString();
                if (!string.IsNullOrEmpty(valorBateria) && valorBateria != "No dejó")
                {
                    string soloNumero = System.Text.RegularExpressions.Regex.Match(valorBateria, @"\d+").Value;
                    if (!string.IsNullOrEmpty(soloNumero))
                        bateriaVieja = int.Parse(soloNumero);
                }

                string valorCelda = dgvFacturas.CurrentRow.Cells["Rebaja"].Value?.ToString() ?? "0";
                valorCelda = valorCelda.Replace("L.", "").Trim();

                double rebaja = Convert.ToDouble(valorCelda);



                FacturaVer frmFV = new FacturaVer(idFacturas, nombre_Cliente, fecha, bateriaVieja, idPago, rebaja);
                frmFV.ShowDialog();

                await CargarFactura();
                FiltrarDatos();
            }
        }

        /// <summary>
        /// Handles the Click event of the BtnRefrescar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BtnRefrescar_Click(object sender, EventArgs e)
        {
            txtBusqueda.Clear();
            dtpInicio.Value = DateTime.Today;
            dtpFin.Value = DateTime.Today;
            FiltrarDatos();
        }

        /// <summary>
        /// Navegars a.
        /// </summary>
        /// <param name="formulario">The formulario.</param>
        private void NavegarA(Form formulario)
        {
            formulario.Show();
            this.Close();
        }


        /// <summary>
        /// Handles the Click event of the BtnNotificaciones control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BtnNotificaciones_Click(object sender, EventArgs e)
        {
            new NotificacionesAdmin().Show();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            MenuPrincipalEmp ME = new MenuPrincipalEmp();
            ME.Show();
            this.Hide();
        }

       

        private void btnClientes_Click(object sender, EventArgs e)
        {
            ClientesEmp CE = new ClientesEmp();
            CE.Show();
            this.Hide();
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            InventarioEmp IE = new InventarioEmp();
            IE.Show();
            this.Hide();
        }

        private void btnDeudores_Click(object sender, EventArgs e)
        {
            Deudores_Emp DE = new Deudores_Emp();
            DE.Show();
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