using Microsoft.Data.SqlClient;
using SG_BAMS.Bitacora;
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
    public partial class Compras : Form
    {
        private ClsModificarCompras logic = new ClsModificarCompras();
        private ClsMostrarCompras consultaLogic = new ClsMostrarCompras();

        public Compras()
        {
            InitializeComponent();
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
            DeudoresAdmin deudores = new DeudoresAdmin();
            deudores.Show();
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
            BitacoraAdmin bitacoraAdmin = new BitacoraAdmin();
            bitacoraAdmin.Show();
            this.Hide();
        }

        private void Compras_Load(object sender, EventArgs e)
        {
            CargarCompras();
            dgvComprasAdmin.ClearSelection();
        }

        public void CargarCompras()
        {
            try
            {
                dgvComprasAdmin.DataSource = consultaLogic.ListarCompras();
                dgvComprasAdmin.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvComprasAdmin.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar compras: " + ex.Message);
            }
        }

        private void btnNoti_Click(object sender, EventArgs e)
        {
            NotificacionesAdmin notificacionesAdmin = new NotificacionesAdmin();
            notificacionesAdmin.Show();
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

        private void dgvComprasAdmin_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
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
        }

        private void btnModificarC_Click(object sender, EventArgs e)
        {
            if (dgvComprasAdmin.SelectedRows.Count > 0)
            {
                int idSeleccionado = Convert.ToInt32(dgvComprasAdmin.CurrentRow.Cells["ID"].Value);
                Modificar_datos__Compra_ frmModificar = new Modificar_datos__Compra_(idSeleccionado);
                frmModificar.ShowDialog();
                CargarCompras();
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
                            MessageBox.Show("Compra eliminada correctamente.", "BAMS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarCompras();
                            dgvComprasAdmin.ClearSelection();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No se pudo eliminar la compra: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una compra de la lista.",
                                "BAMS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}