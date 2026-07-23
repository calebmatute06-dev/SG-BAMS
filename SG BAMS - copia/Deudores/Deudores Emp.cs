using SG_BAMS.Bitacora;
using SG_BAMS.Proveedor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SG_BAMS
{
    public partial class Deudores_Emp : Form
    {
        private DataTable dtDeudores;
        private bool isFiltering = false;
        private string placeholderTexto = "Buscar por nombre del cliente...";
        private Color placeholderColor = Color.Gray;
        private Color textoColor = Color.Black;

        public Deudores_Emp()
        {
            InitializeComponent();
            CargarGridDeudores();
            this.StartPosition = FormStartPosition.CenterScreen;

            dgvDeudores.CellDoubleClick += dgvDeudores_CellDoubleClick;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            this.txtBuscarNombre.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
            this.txtBuscarNombre.TextChanged += txtBuscarNombre_TextChanged;

            this.txtBuscarNombre.Enter += txtBuscarNombre_Enter;
            this.txtBuscarNombre.Leave += txtBuscarNombre_Leave;

            dgvDeudores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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
            dgvDeudores.MultiSelect = false;

            FiltrarDeudores();
        }

        private void txtBuscarNombre_TextChanged(object sender, EventArgs e)
        {
            if (isFiltering) return;
            isFiltering = true;
            FiltrarDeudores();
            isFiltering = false;
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
                dgvDeudores.CurrentCell = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al filtrar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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

        private void kryptonButton15_Click(object sender, EventArgs e)
        {
            if (dgvDeudores.SelectedRows.Count == 0 || dgvDeudores.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccione una fila.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void dgvDeudores_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                DataRowView filaSeleccionada = null;

                if (dgvDeudores.DataSource is DataView dv)
                {
                    if (e.RowIndex < dv.Count)
                        filaSeleccionada = dv[e.RowIndex];
                }
                else if (dgvDeudores.DataSource is DataTable dt)
                {
                    if (e.RowIndex < dt.Rows.Count)
                        filaSeleccionada = dt.DefaultView[e.RowIndex];
                }
                else if (dgvDeudores.Rows[e.RowIndex].DataBoundItem is DataRowView drv)
                {
                    filaSeleccionada = drv;
                }

                if (filaSeleccionada != null)
                {
                    ProcesarPagoDeuda(filaSeleccionada);
                    dgvDeudores.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al procesar el pago: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Deudores_Shown(object sender, EventArgs e)
        {
            Ayudante_UI.AplicarZoomGlobal(this);
            CargarGridDeudores();
            dgvDeudores.ClearSelection();
            dgvDeudores.CurrentCell = null;
        }

        private void txtBuscarNombre_KeyPress(object sender, KeyPressEventArgs e) => ClsValidaciones.PermitirSoloLetras(e);

        private void btnNoti(object sender, EventArgs e) => new NotificacionesAdmin().ShowDialog();

        private void timer1_Tick(object sender, EventArgs e) { }

        private void Deudores_Emp_Load(object sender, EventArgs e)
        {
            dgvDeudores.ClearSelection();
            ConfigurarPlaceholder(txtBuscarNombre, "Buscar por nombre del cliente...");

            btnDeudores.Enabled = false;
            btnDeudores.BackColor = Color.SkyBlue;
            btnDeudores.ForeColor = Color.White;

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

            ClsMensajeGuia.Activar(txtBuscarNombre);
        }

        private void btnMenu_Click(object sender, EventArgs e) { MenuPrincipalEmp ME = new MenuPrincipalEmp(); ME.Show(); this.Close(); }
        private void btnFacturas_Click(object sender, EventArgs e) { FacturasEmp FE = new FacturasEmp(); FE.Show(); this.Close(); }
        private void btnClientes_Click(object sender, EventArgs e) { ClientesEmp CE = new ClientesEmp(); CE.Show(); this.Close(); }
        private void btnInventario_Click(object sender, EventArgs e) { InventarioEmp IE = new InventarioEmp(); IE.Show(); this.Close(); }

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