using Microsoft.Data.SqlClient;
using SG_BAMS.Administracion_de_BAMS.MarcaProd;
using SG_BAMS.Login;
using SG_BAMS.ProductoInventario;
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

                frmMod.LlenarCombosModificar();

                frmMod.txtID.Text = dgvProductosAdmin.CurrentRow.Cells["ID"].Value.ToString();
                frmMod.txtNombre.Text = dgvProductosAdmin.CurrentRow.Cells["Producto"].Value.ToString();
                frmMod.txtPrecio.Text = dgvProductosAdmin.CurrentRow.Cells["Precio_Venta"].Value.ToString();
                frmMod.txtServicio.Text = dgvProductosAdmin.CurrentRow.Cells["Servicio"].Value.ToString();
                frmMod.txtCodigoBarra.Text = dgvProductosAdmin.CurrentRow.Cells["Codigo_Barra"].Value.ToString();

                frmMod.cmbMarca.Text = dgvProductosAdmin.CurrentRow.Cells["Marca"].Value.ToString();
                frmMod.cmbTipo.Text = dgvProductosAdmin.CurrentRow.Cells["Tipo"].Value.ToString();
                frmMod.cmbModelo.Text = dgvProductosAdmin.CurrentRow.Cells["Modelo_Auto"].Value.ToString();
                frmMod.cmbEstado.Text = dgvProductosAdmin.CurrentRow.Cells["Estado"].Value.ToString();

                if (frmMod.ShowDialog() == DialogResult.OK)
                {
                    CargarInventarioCompleto();
                }

            }
            else
            {
                MessageBox.Show("Por favor, selecciona una fila para modificar.");
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

                string query = @"SELECT 
                            p.id_producto AS ID, 
                            p.nombre_producto AS Producto, 
                            m.nombre_marca AS Marca, 
                            t.descripcion_forma_pago AS Tipo, 
                            mo.nombre_modelo_auto AS Modelo_Auto, 
                            e.descripcion_estado AS Estado,
                            p.precio_venta AS Precio_Venta,
                            p.descripcion_tipo_servicio AS Servicio,
                            p.codigo_barra AS Codigo_Barra
                         FROM Producto p
                         INNER JOIN Marca_producto m ON p.id_marca_producto = m.id_marca_producto
                         INNER JOIN Tipo_producto t ON p.id_tipo_producto = t.id_tipo_producto
                         INNER JOIN Modelo_de_auto mo ON p.id_modelo_auto = mo.id_modelo_auto
                         INNER JOIN Estado e ON p.id_estado = e.id_estado
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
                Console.WriteLine("Error en búsqueda: " + ex.Message);
            }
            finally
            {
                conexion.Cerrar();
            }
        }
    }
}
