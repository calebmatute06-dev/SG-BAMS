using Microsoft.Data.SqlClient;
using SG_BAMS.Administracion_de_BAMS.MarcaProd;
using SG_BAMS.Login;
using SG_BAMS.ProductoInventario;
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
    public partial class InventarioAdmin : Form
    {
        ClsVerProducto logica = new ClsVerProducto();

        public InventarioAdmin()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }

        public void CargarInventarioCompleto()
        {
            try
            {
                dgvProductosAdmin.DataSource = logica.MostrarProductosCompleto();
                dgvProductosAdmin.ReadOnly = true;
                dgvProductosAdmin.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvProductosAdmin.AllowUserToAddRows = false;
                dgvProductosAdmin.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                if (dgvProductosAdmin.Columns.Contains("Producto"))
                {
                    dgvProductosAdmin.Columns["Producto"].MinimumWidth = 150;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el inventario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InventarioAdmin_Load(object sender, EventArgs e)
        {
            CargarInventarioCompleto();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarProducto frm = new AgregarProducto();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                CargarInventarioCompleto();
            }
        }

        private void kryptonButton10_Click(object sender, EventArgs e)
        {
            if (dgvProductosAdmin.SelectedRows.Count > 0)
            {
                ModificarProducto frmMod = new ModificarProducto();

                frmMod.txtID.Text = dgvProductosAdmin.CurrentRow.Cells["ID"].Value.ToString();
                frmMod.txtNombre.Text = dgvProductosAdmin.CurrentRow.Cells["Producto"].Value.ToString();
                frmMod.txtPrecio.Text = dgvProductosAdmin.CurrentRow.Cells["Precio Venta"].Value.ToString();
                frmMod.txtCodigoBarra.Text = dgvProductosAdmin.CurrentRow.Cells["Codigo Barra"].Value.ToString();

                if (dgvProductosAdmin.CurrentRow.Cells["Stock Actual"].Value != DBNull.Value)
                {
                    frmMod.txtStock.Value = Convert.ToDecimal(dgvProductosAdmin.CurrentRow.Cells["Stock Actual"].Value);
                }

                frmMod.proveedorActual = dgvProductosAdmin.CurrentRow.Cells["Proveedor"].Value.ToString();
                frmMod.marcaActual = dgvProductosAdmin.CurrentRow.Cells["Marca"].Value.ToString();
                frmMod.tipoActual = dgvProductosAdmin.CurrentRow.Cells["Tipo"].Value.ToString();
                frmMod.modeloActual = dgvProductosAdmin.CurrentRow.Cells["Modelo Auto"].Value.ToString();
                frmMod.estadoActual = dgvProductosAdmin.CurrentRow.Cells["Estado"].Value.ToString();

                if (frmMod.ShowDialog() == DialogResult.OK)
                {
                    CargarInventarioCompleto();
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una fila para modificar.", "BAMS");
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                CargarInventarioCompleto();
                return;
            }

            try
            {
                dgvProductosAdmin.DataSource = logica.BuscarProductos(txtBuscar.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar: " + ex.Message, "BAMS");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void kryptonButton9_Click(object sender, EventArgs e)
        {
            MenuPrincipalAdm menuPrincipalAdm = new MenuPrincipalAdm();
            menuPrincipalAdm.Show();
            this.Hide();
        }

        private void btnFactura_Click(object sender, EventArgs e)
        {
            FacturasAdm facturas = new FacturasAdm();
            facturas.Show();
            this.Hide();
        }

        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            ClientesAdm clientes = new ClientesAdm();
            clientes.Show();
            this.Hide();
        }

        private void btnCompras_Click(object sender, EventArgs e)
        {
            Compras vercompras = new Compras();
            vercompras.Show();
            this.Hide();
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            InventarioAdmin inventario = new InventarioAdmin();
            inventario.Show();
            this.Hide();
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            Proveedor.ProveedoresAdmin proveedores = new Proveedor.ProveedoresAdmin();
            proveedores.Show();
            this.Hide();
        }

        private void btnDeudores_Click(object sender, EventArgs e)
        {
            DeudoresAdmin deudoresAdm = new DeudoresAdmin();
            deudoresAdm.Show();
            this.Hide();
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            ReportesAdmin frmReportes = new ReportesAdmin();
            frmReportes.Show();
            this.Hide();
        }

        private void btnBitacora_Click(object sender, EventArgs e)
        {
            Bitacora.BitacoraAdmin bitacora = new Bitacora.BitacoraAdmin();
            bitacora.Show();
            this.Hide();
        }

        private void btnNoti_Click(object sender, EventArgs e)
        {
            NotificacionesAdmin notificaciones = new NotificacionesAdmin();
            notificaciones.Show();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
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

        private void dgvProductosAdmin_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvProductosAdmin.SelectedRows.Count > 0)
            {
                ModificarProducto frmMod = new ModificarProducto();

                frmMod.txtID.Text = dgvProductosAdmin.CurrentRow.Cells["ID"].Value.ToString();
                frmMod.txtNombre.Text = dgvProductosAdmin.CurrentRow.Cells["Producto"].Value.ToString();
                frmMod.txtPrecio.Text = dgvProductosAdmin.CurrentRow.Cells["Precio Venta"].Value.ToString();
                frmMod.txtCodigoBarra.Text = dgvProductosAdmin.CurrentRow.Cells["Codigo Barra"].Value.ToString();
                if (dgvProductosAdmin.CurrentRow.Cells["Stock Actual"].Value != DBNull.Value)
                {
                    frmMod.txtStock.Value = Convert.ToDecimal(dgvProductosAdmin.CurrentRow.Cells["Stock Actual"].Value);
                }

                frmMod.marcaActual = dgvProductosAdmin.CurrentRow.Cells["Marca"].Value.ToString();
                frmMod.tipoActual = dgvProductosAdmin.CurrentRow.Cells["Tipo"].Value.ToString();
                frmMod.modeloActual = dgvProductosAdmin.CurrentRow.Cells["Modelo Auto"].Value.ToString();
                frmMod.proveedorActual = dgvProductosAdmin.CurrentRow.Cells["Proveedor"].Value.ToString();
                frmMod.estadoActual = dgvProductosAdmin.CurrentRow.Cells["Estado"].Value.ToString();

                if (frmMod.ShowDialog() == DialogResult.OK)
                {
                    CargarInventarioCompleto();
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una fila para modificar.", "BAMS");
            }
        }

        private DateTime ultimaTeclaEscaner = DateTime.Now;

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys key = keyData & Keys.KeyCode;

            if ((key >= Keys.D0 && key <= Keys.Z) || (key >= Keys.NumPad0 && key <= Keys.NumPad9))
            {
                TimeSpan intervalo = DateTime.Now - ultimaTeclaEscaner;
                ultimaTeclaEscaner = DateTime.Now;

                if (intervalo.TotalMilliseconds < 50 || !txtBuscar.Focused)
                {
                    txtBuscar.Text = string.Empty;

                    if (!txtBuscar.Focused) txtBuscar.Focus();

                    char c = (char)key;
                    txtBuscar.AppendText(c.ToString().ToLower());

                    return true;
                }
            }

            if (key == Keys.Enter)
            {
                if (txtBuscar.Focused && !string.IsNullOrWhiteSpace(txtBuscar.Text))
                {
                    dgvProductosAdmin.DataSource = logica.BuscarProductos(txtBuscar.Text.Trim());
                    txtBuscar.SelectAll();

                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}