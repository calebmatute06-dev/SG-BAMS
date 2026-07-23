using SG_BAMS.Bitacora;
using SG_BAMS.Proveedor;
using SG_BAMS.Reporte;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SG_BAMS
{
    public partial class DeudoresAdmin : Form
    {
        private DataTable dtDeudores;
        private bool isFiltering = false;
        private string placeholderTexto = "Buscar por nombre del cliente...";
        private Color placeholderColor = Color.Gray;
        private Color textoColor = Color.Black;

        public DeudoresAdmin()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            CargarGridDeudores();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            dgvDeudores.CellDoubleClick += dgvDeudores_CellDoubleClick;

            txtBuscarNombre.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
            txtBuscarNombre.TextChanged += txtBuscarNombre_TextChanged;

            txtBuscarNombre.Enter += txtBuscarNombre_Enter;
            txtBuscarNombre.Leave += txtBuscarNombre_Leave;
        }

        private void ConfigurarPlaceholder(TextBox textBox, string placeholder)
        {
            placeholderTexto = placeholder;
            textoColor = textBox.ForeColor;
            placeholderColor = Color.Gray;

            textBox.Text = placeholderTexto;
            textBox.ForeColor = placeholderColor;
        }

        private void txtBuscarNombre_Enter(object sender, EventArgs e)
        {
            if (txtBuscarNombre.Text == placeholderTexto)
            {
                txtBuscarNombre.Text = "";
                txtBuscarNombre.ForeColor = textoColor;
            }
        }

        private void txtBuscarNombre_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscarNombre.Text))
            {
                txtBuscarNombre.Text = placeholderTexto;
                txtBuscarNombre.ForeColor = placeholderColor;
            }
        }

        public void CargarGridDeudores()
        {
            ClsDeuda objetoDeuda = new ClsDeuda();
            dtDeudores = objetoDeuda.ListarDeudores();

            dgvDeudores.DataSource = dtDeudores;
            dgvDeudores.ReadOnly = true;
            dgvDeudores.AllowUserToAddRows = false;
            dgvDeudores.AllowUserToDeleteRows = false;
            dgvDeudores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDeudores.MultiSelect = false;

            FiltrarDeudores();
        }

        private void FiltrarDeudores()
        {
            if (dtDeudores == null) return;

            try
            {
                DataView dv = dtDeudores.DefaultView;
                string filtroNombre = txtBuscarNombre.Text?.Trim() ?? "";

                if (filtroNombre == placeholderTexto || txtBuscarNombre.ForeColor == placeholderColor)
                    filtroNombre = "";

                var condiciones = new List<string>();
                condiciones.Add("[Estado Deuda] = 'Activo'");

                if (!string.IsNullOrWhiteSpace(filtroNombre))
                {
                    string nombreBuscar = filtroNombre
                        .Replace("'", "''")
                        .Replace("[", "[[]")
                        .Replace("]", "[]]");

                    condiciones.Add($"Cliente LIKE '%{nombreBuscar}%'");
                }

                string rowFilter = string.Join(" AND ", condiciones);
                dv.RowFilter = rowFilter;
                dgvDeudores.DataSource = dv;
                dgvDeudores.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al filtrar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBuscarNombre_TextChanged(object sender, EventArgs e)
        {
            if (isFiltering) return;
            isFiltering = true;
            FiltrarDeudores();
            isFiltering = false;
        }

        private void kryptonButton12_Click(object sender, EventArgs e) => FiltrarDeudores();

        private void ProcesarPagoDeuda(DataRowView fila)
        {
            if (fila == null) return;

            int idDeuda = Convert.ToInt32(fila["ID Deuda"]);
            string nombreCliente = fila["Cliente"].ToString().Trim();
            string estadoDeuda = fila["Estado Deuda"].ToString().Trim();

            if (estadoDeuda.Equals("Activo", StringComparison.OrdinalIgnoreCase))
            {
                Pago_Deuda pagDe = new Pago_Deuda(nombreCliente, idDeuda);
                if (pagDe.ShowDialog() == DialogResult.OK)
                {
                    CargarGridDeudores();
                    txtBuscarNombre.Clear();
                }
            }
            else
            {
                MessageBox.Show($"La deuda de {nombreCliente} ya no está activa.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvDeudores_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            try
            {
                DataRowView filaSeleccionada = (DataRowView)dgvDeudores.Rows[e.RowIndex].DataBoundItem;
                ProcesarPagoDeuda(filaSeleccionada);
                dgvDeudores.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar procesar el pago: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void kryptonButton15_Click(object sender, EventArgs e)
        {
            if (dgvDeudores.SelectedRows.Count == 0 || dgvDeudores.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccione una fila para pagar la deuda.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                DataRowView filaSeleccionada = dgvDeudores.CurrentRow.DataBoundItem as DataRowView;
                if (filaSeleccionada != null)
                {
                    ProcesarPagoDeuda(filaSeleccionada);
                    dgvDeudores.ClearSelection();
                }
                else
                {
                    MessageBox.Show("No se pudo obtener la información de la fila seleccionada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al procesar el pago: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Deudores_Shown(object sender, EventArgs e) => Ayudante_UI.AplicarZoomGlobal(this);

        private void txtBuscarNombre_KeyPress(object sender, KeyPressEventArgs e) => ClsValidaciones.PermitirSoloLetras(e);

        private void button12_Click(object sender, EventArgs e) => new NotificacionesAdmin().ShowDialog();

        private void kryptonDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void dgvDeudores_DoubleClick(object sender, EventArgs e) { }

        private void DeudoresAdmin_Load(object sender, EventArgs e)
        {
            ConfigurarPlaceholder(txtBuscarNombre, "Buscar por nombre del cliente...");

            btnDeudores.Enabled = false;
            btnDeudores.BackColor = Color.SkyBlue;
            btnDeudores.ForeColor = Color.White;

            dgvDeudores.ClearSelection();
            dgvDeudores.BorderStyle = BorderStyle.None;
            dgvDeudores.BackgroundColor = Color.White;
            dgvDeudores.RowHeadersVisible = false;
            dgvDeudores.EnableHeadersVisualStyles = false;
            dgvDeudores.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgvDeudores.ColumnHeadersDefaultCellStyle.BackColor = Color.SkyBlue;
            dgvDeudores.ColumnHeadersDefaultCellStyle.ForeColor = Color.Navy;
            dgvDeudores.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvDeudores.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvDeudores.ColumnHeadersHeight = 28;

            dgvDeudores.DefaultCellStyle.BackColor = Color.White;
            dgvDeudores.DefaultCellStyle.ForeColor = Color.Navy;
            dgvDeudores.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvDeudores.DefaultCellStyle.Padding = new Padding(3);
            dgvDeudores.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 245, 255);
            dgvDeudores.AlternatingRowsDefaultCellStyle.ForeColor = Color.Navy;

            dgvDeudores.DefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            dgvDeudores.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvDeudores.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvDeudores.GridColor = Color.LightGray;
            dgvDeudores.RowTemplate.Height = 32;
            dgvDeudores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDeudores.ClearSelection();
            ClsMensajeGuia.Activar(txtBuscarNombre);
        }

        private void btnMenu_Click(object sender, EventArgs e) { MenuPrincipalAdm MPA = new MenuPrincipalAdm(); MPA.Show(); this.Close(); }
        private void btnFacturas_Click(object sender, EventArgs e) { FacturasAdm FA = new FacturasAdm(); FA.Show(); this.Close(); }
        private void btnCompra_Click(object sender, EventArgs e) { Compras CF = new Compras(); CF.Show(); this.Close(); }
        private void btnClientes_Click(object sender, EventArgs e) { ClientesAdm CA = new ClientesAdm(); CA.Show(); this.Close(); }
        private void btnInventario_Click(object sender, EventArgs e) { InventarioAdmin IA = new InventarioAdmin(); IA.Show(); this.Close(); }
        private void btnProveedores_Click(object sender, EventArgs e)
        {
            var PA = new ProveedoresAdmin(
                new ProveedorRepository(),
                new EstadoRepository(),
                new ClasificacionRepository());
            PA.Show();
            this.Hide();
        }
        private void btnReportes_Click(object sender, EventArgs e) { ReportesAdmin RA = new ReportesAdmin(); RA.Show(); this.Close(); }
        private void btnBitacora_Click(object sender, EventArgs e) {
            var Bi = new BitacoraAdmin(
               new BitacoraRepository(),
               new FiltroBitacoraService(),
               new ReporteBitacoraPdfExportador());
            Bi.Show();
            this.Hide();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea cerrar sesión?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Login.Login login = new Login.Login();
                login.Show();
                this.Close();
            }
        }

        private void btnPerfil_Click(object sender, EventArgs e) { Perfil perfil = new Perfil(); perfil.ShowDialog(); }
    }
}