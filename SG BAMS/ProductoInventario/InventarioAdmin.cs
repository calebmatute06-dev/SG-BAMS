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

            ClsConexion conexion = new ClsConexion();
            DataTable dt = new DataTable();

            try
            {
                conexion.AbrirConexion();

                // Usamos exactamente "Proveedor_Producto" como dice tu imagen de CREATE TABLE
                string query = @"SELECT 
                            p.id_producto AS ID, 
                            p.nombre_producto AS Producto, 
                            m.nombre_marca AS Marca, 
                            t.descripcion_forma_pago AS Tipo, 
                            mo.nombre_modelo_auto AS [Modelo Auto], 
                            prov.nombre_proveedor AS Proveedor,
                            p.precio_venta AS [Precio Venta],
                            p.codigo_barra AS [Codigo Barra],
                            e.descripcion_estado AS Estado
                         FROM Producto p
                         INNER JOIN Marca_producto m ON p.id_marca_producto = m.id_marca_producto
                         INNER JOIN Tipo_producto t ON p.id_tipo_producto = t.id_tipo_producto
                         INNER JOIN Modelo_de_auto mo ON p.id_modelo_auto = mo.id_modelo_auto
                         INNER JOIN Estado e ON p.id_estado = e.id_estado
                         LEFT JOIN Proveedor_Producto pp ON p.id_producto = pp.id_producto
                         LEFT JOIN Proveedor prov ON pp.id_proveedor = prov.id_proveedor
                         WHERE p.nombre_producto LIKE '%' + @filtro + '%' 
                         OR p.codigo_barra LIKE '%' + @filtro + '%'";

                using (SqlCommand cmd = new SqlCommand(query, conexion.Conectar))
                {
                    cmd.Parameters.AddWithValue("@filtro", txtBuscar.Text.Trim());
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }

                dgvProductosAdmin.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en búsqueda: " + ex.Message);
            }
            finally { conexion.Cerrar(); }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvProductosAdmin_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return; // Salimos del método sin hacer nada
            }

            if (dgvProductosAdmin.SelectedRows.Count > 0)
            {
                ModificarProducto frmMod = new ModificarProducto();

                frmMod.txtID.Text = dgvProductosAdmin.CurrentRow.Cells["ID"].Value.ToString();
                frmMod.txtNombre.Text = dgvProductosAdmin.CurrentRow.Cells["Producto"].Value.ToString();
                frmMod.txtPrecio.Text = dgvProductosAdmin.CurrentRow.Cells["Precio Venta"].Value.ToString();
                frmMod.txtCodigoBarra.Text = dgvProductosAdmin.CurrentRow.Cells["Codigo Barra"].Value.ToString();
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
    }
}
