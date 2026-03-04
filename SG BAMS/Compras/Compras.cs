using Microsoft.Data.SqlClient;
using SG_BAMS.Bitacora;
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
    public partial class Compras : Form
    {
        public Compras()
        {
            InitializeComponent();
        }

        private void kryptonGroup3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void kryptonButton10_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox19_Click(object sender, EventArgs e)
        {


        }

        private void kryptonButton13_Click(object sender, EventArgs e)
        {

        }

        private void kryptonGroup4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void kryptonButton15_Click(object sender, EventArgs e)
        {

        }

        private void kryptonButton13_Click_1(object sender, EventArgs e)
        {

        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            MenuPrincipalAdm menuPrincipalAdm = new MenuPrincipalAdm();
            menuPrincipalAdm.Show();
            this.Hide();

        }

        private void btnFactura_Click(object sender, EventArgs e)
        {
            FacturasAdm facturasAdm = new FacturasAdm();
            facturasAdm.Show();
            this.Hide();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            ClientesAdm clientesAdm = new ClientesAdm();
            clientesAdm.Show();
            this.Hide();
        }

        private void btnCompra_Click(object sender, EventArgs e)
        {
            Compras compras = new Compras();
            compras.Show();
            this.Hide();
        }

        private void btnInve_Click(object sender, EventArgs e)
        {
            InventarioAdmin inventarioAdmin = new InventarioAdmin();
            inventarioAdmin.Show();
            this.Hide();
        }

        private void btnProvee_Click(object sender, EventArgs e)
        {
            Proveedor.ProveedoresAdmin proveedoresAdmin = new Proveedor.ProveedoresAdmin();
            proveedoresAdmin.Show();
            this.Hide();
        }

        private void btnDeudores_Click(object sender, EventArgs e)
        {
            Deudores deudores = new Deudores();
            deudores.Show();
            this.Hide();
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            ReporteAdmin reporteAdmin = new ReporteAdmin();
            reporteAdmin.Show();
            this.Hide();
        }

        private void btnBitacora_Click(object sender, EventArgs e)
        {
            BitacoraAdmin bitacoraAdmin = new BitacoraAdmin();
            bitacoraAdmin.Show();
            this.Hide();
        }

        private void Compras_Load(object sender, EventArgs e)
        {
            CargarCompras();
        }

        public void CargarCompras()
        {
            ClsConexion conexion = new ClsConexion();
            DataTable dt = new DataTable();

            try
            {
                conexion.AbrirConexion();

                // Esta consulta usa los nombres EXACTOS de tu script SQL
                string query = @"SELECT 
                            C.id_compra AS [ID],
                            U.nombre_usuario AS [Usuario],
                            C.fecha_pedido AS [Fecha],
                            FP.descripcion_forma_pago AS [Forma Pago],
                            P.nombre_proveedor AS [Proveedor],
                            C.desc_compra AS [Descripción]
                         FROM Compra C
                         INNER JOIN Usuario U ON C.id_usuario = U.id_usuario
                         INNER JOIN Tipo_Forma_de_pago FP ON C.id_tipo_forma_pago = FP.id_tipo_forma_pago
                         INNER JOIN Proveedor P ON C.id_proveedor = P.id_proveedor";

                using (SqlCommand cmd = new SqlCommand(query, conexion.Conectar))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }

                // Asignamos los datos al DataGridView
                dgvComprasAdmin.DataSource = dt;

                // Ajuste visual para que se vea profesional
                dgvComprasAdmin.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                // Si sale error ahora, es porque falta algún dato en las tablas (como un ID que no existe)
                MessageBox.Show("Error al cargar compras: " + ex.Message);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        private void btnComprar_Click(object sender, EventArgs e)
        {
            Ingresar_datos__Compra_ frmNuevaCompra = new Ingresar_datos__Compra_();

            frmNuevaCompra.ShowDialog();

            CargarCompras();
        }
    }
}
