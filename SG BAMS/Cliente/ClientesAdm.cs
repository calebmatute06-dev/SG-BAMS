using Microsoft.Data.SqlClient;
using SG_BAMS.Bitacora;
using SG_BAMS.Cliente;
using SG_BAMS.Proveedor;
using SG_BAMS.Reporte;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS
{
    public partial class ClientesAdm : Form
    {
        DataTable datosCli;

        public ClientesAdm()
        {
            InitializeComponent();
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClientes.MultiSelect = false;


            txtBusqueda.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetrasYNumeros(e);
        }

        private async Task TablaClientes()
        {
            ClsVerCliente objC = new ClsVerCliente();
            datosCli = await objC.VerClienteTabla();

            if (datosCli != null)
            {
                dgvClientes.DataSource = datosCli;

                dgvClientes.Columns["ID"].HeaderText = "ID Cliente";
                dgvClientes.Columns["Nombre"].HeaderText = "Nombre";
                dgvClientes.Columns["Apellido"].HeaderText = "Apellido";
                dgvClientes.Columns["Teléfono"].HeaderText = "Teléfono";
                dgvClientes.Columns["RTN"].HeaderText = "RTN";
                dgvClientes.Columns["Estado"].HeaderText = "Estado";

                if (dgvClientes.Columns.Contains("ID Estado"))
                    dgvClientes.Columns["ID Estado"].Visible = false;

                AplicarFiltro();

                dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvClientes.AllowUserToAddRows = false;
                dgvClientes.ReadOnly = true;
                dgvClientes.ClearSelection();
            }
        }

        private void AplicarFiltro()
        {
            if (datosCli == null) return;

            DataView dv = datosCli.DefaultView;
            string filtroEstado = chkActivo.Checked ? "Estado <> 'Activo'" : "Estado = 'Activo'";

            if (string.IsNullOrWhiteSpace(txtBusqueda.Text))
            {
                dv.RowFilter = filtroEstado;
            }
            else
            {

                string textoSeguro = txtBusqueda.Text
                    .Replace("'", "''")
                    .Replace("[", "[[]")
                    .Replace("]", "[]]")
                    .Replace("*", "[*]")
                    .Replace("%", "[%]");


                dv.RowFilter = string.Format(
                    "({0}) AND (Nombre LIKE '%{1}%' OR Apellido LIKE '%{1}%' OR RTN LIKE '%{1}%' OR Teléfono LIKE '%{1}%')",
                    filtroEstado, textoSeguro);
            }

            dgvClientes.DataSource = dv;
            dgvClientes.ClearSelection();
        }

        private async void ClientesAdm_Load(object sender, EventArgs e)
        {
            await TablaClientes();
            dgvClientes.BorderStyle = BorderStyle.None;
            dgvClientes.BackgroundColor = Color.White;
            dgvClientes.RowHeadersVisible = false;
            dgvClientes.EnableHeadersVisualStyles = false;
            dgvClientes.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgvClientes.ColumnHeadersDefaultCellStyle.BackColor = Color.SkyBlue;
            dgvClientes.ColumnHeadersDefaultCellStyle.ForeColor = Color.Navy;
            dgvClientes.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvClientes.ColumnHeadersHeight = 28;

            dgvClientes.DefaultCellStyle.BackColor = Color.White;
            dgvClientes.DefaultCellStyle.ForeColor = Color.Navy;
            dgvClientes.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvClientes.DefaultCellStyle.Padding = new Padding(3);
            dgvClientes.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 245, 255);
            dgvClientes.AlternatingRowsDefaultCellStyle.ForeColor = Color.Navy;

            dgvClientes.DefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            dgvClientes.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvClientes.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvClientes.GridColor = Color.LightGray;
            dgvClientes.RowTemplate.Height = 32;
            dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvClientes.ClearSelection();
        }

        private async void dgvClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e != null && e.RowIndex < 0) return;

            if (dgvClientes.CurrentRow != null)
            {
                try
                {
                    int idCliente = Convert.ToInt32(dgvClientes.CurrentRow.Cells[0].Value);
                    string nombreCliente = dgvClientes.CurrentRow.Cells[1].Value?.ToString() ?? "";
                    string apellidoCliente = dgvClientes.CurrentRow.Cells[2].Value?.ToString() ?? "";
                    string telefonoCliente = dgvClientes.CurrentRow.Cells[3].Value?.ToString() ?? "";
                    string rtnCliente = dgvClientes.CurrentRow.Cells[4].Value?.ToString() ?? "";
                    int idEstado = Convert.ToInt32(dgvClientes.CurrentRow.Cells[5].Value);

                    ClienteModificar frmMo = new ClienteModificar(idCliente, nombreCliente, apellidoCliente, telefonoCliente, rtnCliente, idEstado);

                    if (frmMo.ShowDialog() == DialogResult.OK || frmMo.DialogResult == DialogResult.Cancel)
                    {
                        await TablaClientes();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al procesar la selección: " + ex.Message);
                }
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar una fila", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dgvClientes_CellDoubleClick(null, null);
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
        }

        private void chkActivo_CheckedChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
        }



        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            new Login.Login().Show();
            this.Close();
        }

        private void BtnPerfil_Click(object sender, EventArgs e) => new Perfil().Show();

        private void BtnMenu_Click(object sender, EventArgs e)
        {
            new MenuPrincipalAdm().Show();
            this.Close();
        }

        private void BtnFacturas_Click(object sender, EventArgs e)
        {
            new FacturasAdm().Show();
            this.Close();
        }

        private void BtnCompras_Click(object sender, EventArgs e)
        {
            new Compras().Show();
            this.Close();
        }

        private void BtnInventario_Click(object sender, EventArgs e)
        {
            new InventarioAdmin().Show();
            this.Close();
        }

        private void BtnProveedores_Click(object sender, EventArgs e)
        {
            new ProveedoresAdmin().Show();
            this.Close();
        }

        private void BtnDeudores_Click(object sender, EventArgs e)
        {
            new DeudoresAdmin().Show();
            this.Close();
        }

        private void BtnBitacora_Click(object sender, EventArgs e)
        {
            new BitacoraAdmin().Show();
            this.Close();
        }

       

        private void btnReportes_Click(object sender, EventArgs e)
        {
            new ReportesAdmin().Show();
            this.Close();
        }

        private void btnNoti_Click(object sender, EventArgs e) => new NotificacionesAdmin().Show();

        private void label4_Click(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void BtnReporte_Click(object sender, EventArgs e) { }
        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}