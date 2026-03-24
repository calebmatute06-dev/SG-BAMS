using SG_BAMS.Cliente;
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
    public partial class ClientesEmp : Form
    {
        DataTable datosCli;

        public ClientesEmp()
        {
            InitializeComponent();
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClientes.MultiSelect = false;

            
            txtBusqueda.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
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

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
        }

        private void chkActivo_CheckedChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
        }

        private async void dgvClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e != null && e.RowIndex < 0) return;

            if (dgvClientes.CurrentRow != null)
            {
                try
                {
                    int idCliente = Convert.ToInt32(dgvClientes.CurrentRow.Cells[0].Value);
                    string nombre = dgvClientes.CurrentRow.Cells[1].Value?.ToString() ?? "";
                    string apellido = dgvClientes.CurrentRow.Cells[2].Value?.ToString() ?? "";
                    string telefono = dgvClientes.CurrentRow.Cells[3].Value?.ToString() ?? "";
                    string rtn = dgvClientes.CurrentRow.Cells[4].Value?.ToString() ?? "";
                    int idEstado = Convert.ToInt32(dgvClientes.CurrentRow.Cells[5].Value);

                    ClienteModificar frmMo = new ClienteModificar(idCliente, nombre, apellido, telefono, rtn, idEstado);
                    frmMo.ShowDialog();

                    await TablaClientes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar datos del cliente: " + ex.Message);
                }
            }
        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar una fila", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dgvClientes_CellDoubleClick(null, null);
        }

        private async void ClientesEmp_Load(object sender, EventArgs e)
        {
            await TablaClientes();
        }

        

        private void BtnMenu_Click(object sender, EventArgs e)
        {
            new MenuPrincipalEmp().Show();
            this.Close();
        }

        private void BtnFacturas_Click(object sender, EventArgs e)
        {
            new FacturasEmp().Show();
            this.Close();
        }

        private void BtnDeudores_Click(object sender, EventArgs e)
        {
            new Deudores_Emp().Show();
            this.Close();
        }

        private void BtnInventario_Click(object sender, EventArgs e)
        {
            new InventarioEmp().Show();
            this.Close();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            new Login.Login().Show();
            this.Close();
        }

        private void btnAjustes_Click(object sender, EventArgs e)
        {
            new Ajustes().Show();
        }

        private void btnNoti_Click(object sender, EventArgs e)
        {
            new NotificacionesAdmin().Show();
        }

        private void btnEmp_Click(object sender, EventArgs e)
        {
            new Perfil().Show();
        }

        
        private void panel6_Paint(object sender, PaintEventArgs e) { }
        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
    }
}