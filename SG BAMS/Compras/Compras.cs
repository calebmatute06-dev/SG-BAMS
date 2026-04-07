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
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class Compras : Form
    {
        /// <summary>
        /// The logic
        /// </summary>
        private ClsModificarCompras logic = new ClsModificarCompras();
        /// <summary>
        /// The consulta logic
        /// </summary>
        private ClsMostrarCompras consultaLogic = new ClsMostrarCompras();

        /// <summary>
        /// Initializes a new instance of the <see cref="Compras"/> class.
        /// </summary>
        public Compras()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the btnMenu control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnMenu_Click(object sender, EventArgs e)
        {
            MenuPrincipalAdm menuPrincipalAdm = new MenuPrincipalAdm();
            menuPrincipalAdm.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles the Click event of the btnFactura control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnFactura_Click(object sender, EventArgs e)
        {
            FacturasAdm facturasAdm = new FacturasAdm();
            facturasAdm.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles the Click event of the btnClientes control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnClientes_Click(object sender, EventArgs e)
        {
            ClientesAdm clientesAdm = new ClientesAdm();
            clientesAdm.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles the Click event of the btnCompra control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnCompra_Click(object sender, EventArgs e)
        {
            Compras compras = new Compras();
            compras.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles the Click event of the btnInve control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnInve_Click(object sender, EventArgs e)
        {
            InventarioAdmin inventarioAdmin = new InventarioAdmin();
            inventarioAdmin.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles the Click event of the btnProvee control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnProvee_Click(object sender, EventArgs e)
        {
            Proveedor.ProveedoresAdmin proveedoresAdmin = new Proveedor.ProveedoresAdmin();
            proveedoresAdmin.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles the Click event of the btnDeudores control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnDeudores_Click(object sender, EventArgs e)
        {
            DeudoresAdmin deudores = new DeudoresAdmin();
            deudores.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles the Click event of the btnReporte control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnReporte_Click(object sender, EventArgs e)
        {
            ReportesAdmin frmReportes = new ReportesAdmin();
            frmReportes.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles the Click event of the btnBitacora control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnBitacora_Click(object sender, EventArgs e)
        {
            BitacoraAdmin bitacoraAdmin = new BitacoraAdmin();
            bitacoraAdmin.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles the Load event of the Compras control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Compras_Load(object sender, EventArgs e)
        {
            CargarCompras();
            dgvComprasAdmin.ClearSelection();
            dgvComprasAdmin.BorderStyle = BorderStyle.None;
            dgvComprasAdmin.BackgroundColor = Color.White;
            dgvComprasAdmin.RowHeadersVisible = false;
            dgvComprasAdmin.EnableHeadersVisualStyles = false;
            dgvComprasAdmin.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgvComprasAdmin.ColumnHeadersDefaultCellStyle.BackColor = Color.SkyBlue;
            dgvComprasAdmin.ColumnHeadersDefaultCellStyle.ForeColor = Color.Navy;
            dgvComprasAdmin.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvComprasAdmin.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvComprasAdmin.ColumnHeadersHeight = 28;

            dgvComprasAdmin.DefaultCellStyle.BackColor = Color.White;
            dgvComprasAdmin.DefaultCellStyle.ForeColor = Color.Navy;
            dgvComprasAdmin.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvComprasAdmin.DefaultCellStyle.Padding = new Padding(3);
            dgvComprasAdmin.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 245, 255);
            dgvComprasAdmin.AlternatingRowsDefaultCellStyle.ForeColor = Color.Navy;

            dgvComprasAdmin.DefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            dgvComprasAdmin.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvComprasAdmin.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvComprasAdmin.GridColor = Color.LightGray;
            dgvComprasAdmin.RowTemplate.Height = 32;
            dgvComprasAdmin.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvComprasAdmin.ClearSelection();

        }

        /// <summary>
        /// Cargars the compras.
        /// </summary>
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

        /// <summary>
        /// Handles the Click event of the btnNoti control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnNoti_Click(object sender, EventArgs e)
        {
            NotificacionesAdmin notificacionesAdmin = new NotificacionesAdmin();
            notificacionesAdmin.Show();
        }

        /// <summary>
        /// Handles the Click event of the btnCerrarSesion control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Login.Login login = new Login.Login();
            login.Show();
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the btnPerfil control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnPerfil_Click(object sender, EventArgs e)
        {
            Perfil perfil = new Perfil();
            perfil.Show();
        }

        /// <summary>
        /// Handles the CellContentDoubleClick event of the dgvComprasAdmin control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dgvComprasAdmin_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        /// <summary>
        /// Handles the 1 event of the dgvComprasAdmin_CellDoubleClick control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Handles the 1 event of the btnCompra_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnCompra_Click_1(object sender, EventArgs e)
        {
            Ingresar_datos__Compra_ frmNuevaCompra = new Ingresar_datos__Compra_();
            frmNuevaCompra.ShowDialog();
            CargarCompras();
        }

        /// <summary>
        /// Handles the Click event of the btnModificarC control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Handles the Click event of the btnEliminarC control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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