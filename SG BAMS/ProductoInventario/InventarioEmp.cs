using Microsoft.Data.SqlClient;
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
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class InventarioEmp : Form
    {
        /// <summary>
        /// The logica
        /// </summary>
        ClsVerProducto logica = new ClsVerProducto();

        /// <summary>
        /// Initializes a new instance of the <see cref="InventarioEmp"/> class.
        /// </summary>
        public InventarioEmp()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Load event of the InventarioEmp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void InventarioEmp_Load(object sender, EventArgs e)
        {
            CargarInventarioCompleto();
            dgvInventarioEmp.BorderStyle = BorderStyle.None;
            dgvInventarioEmp.BackgroundColor = Color.White;
            dgvInventarioEmp.RowHeadersVisible = false;
            dgvInventarioEmp.EnableHeadersVisualStyles = false;
            dgvInventarioEmp.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgvInventarioEmp.ColumnHeadersDefaultCellStyle.BackColor = Color.SkyBlue;
            dgvInventarioEmp.ColumnHeadersDefaultCellStyle.ForeColor = Color.Navy;
            dgvInventarioEmp.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvInventarioEmp.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvInventarioEmp.ColumnHeadersHeight = 28;

            dgvInventarioEmp.DefaultCellStyle.BackColor = Color.White;
            dgvInventarioEmp.DefaultCellStyle.ForeColor = Color.Navy;
            dgvInventarioEmp.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvInventarioEmp.DefaultCellStyle.Padding = new Padding(3);
            dgvInventarioEmp.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 245, 255);
            dgvInventarioEmp.AlternatingRowsDefaultCellStyle.ForeColor = Color.Navy;

            dgvInventarioEmp.DefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            dgvInventarioEmp.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvInventarioEmp.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvInventarioEmp.GridColor = Color.LightGray;
            dgvInventarioEmp.RowTemplate.Height = 32;
            dgvInventarioEmp.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvInventarioEmp.ClearSelection();

        }

        /// <summary>
        /// Handles the TextChanged event of the txtBuscar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                CargarInventarioCompleto();
                return;
            }

            try
            {
                dgvInventarioEmp.DataSource = logica.BuscarProductos(txtBuscar.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar: " + ex.Message, "BAMS");
            }
        }

        /// <summary>
        /// Cargars the inventario completo.
        /// </summary>
        public void CargarInventarioCompleto()
        {
            try
            {
                dgvInventarioEmp.DataSource = logica.MostrarProductosCompleto();
                dgvInventarioEmp.ReadOnly = true;
                dgvInventarioEmp.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvInventarioEmp.AllowUserToAddRows = false;
                dgvInventarioEmp.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                if (dgvInventarioEmp.Columns.Contains("Producto"))
                {
                    dgvInventarioEmp.Columns["Producto"].MinimumWidth = 150;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el inventario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the Paint event of the panel1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        /// <summary>
        /// Handles the CellDoubleClick event of the dgvInventarioEmp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dgvInventarioEmp_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the btnClientes control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnClientes_Click(object sender, EventArgs e)
        {
            ClientesEmp CE = new ClientesEmp();
            CE.Show();
            this.Close();
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
        /// Handles the Click event of the btnMenu control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnMenu_Click(object sender, EventArgs e)
        {
            MenuPrincipalEmp menu = new MenuPrincipalEmp();
            menu.Show();
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the btnFactu control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnFactu_Click(object sender, EventArgs e)
        {
            FacturasEmp facturasEmp = new FacturasEmp();
            facturasEmp.Show();
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the btnDeudores control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnDeudores_Click(object sender, EventArgs e)
        {
            Deudores_Emp deudoresEmp = new Deudores_Emp();
            deudoresEmp.Show();
            this.Close();
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
        /// Handles the Click event of the btnPerfil control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnPerfil_Click(object sender, EventArgs e)
        {
            Perfil perfil = new Perfil();
            perfil.Show();
        }
    }
}
