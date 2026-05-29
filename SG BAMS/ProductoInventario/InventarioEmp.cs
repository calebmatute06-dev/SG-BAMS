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
        /// La lógica de negocio
        /// </summary>
        ClsVerProducto logica = new ClsVerProducto();

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="InventarioEmp" />.
        /// </summary>
        public InventarioEmp()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        /// <summary>
        /// Maneja el evento Load del control InventarioEmp.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void InventarioEmp_Load(object sender, EventArgs e)
        {
            btnInventario.Enabled = false;
            btnInventario.BackColor = Color.SkyBlue;
            btnInventario.ForeColor = Color.White;

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
            ClsMensajeGuia.Activar(txtBuscar);


        }

        /// <summary>
        /// Maneja el evento TextChanged del control txtBuscar.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
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
        /// Carga el inventario completo.
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
        /// Maneja el evento Click del control btnNoti.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void btnNoti_Click(object sender, EventArgs e)
        {
            NotificacionesAdmin notificacionesAdmin = new NotificacionesAdmin();
            notificacionesAdmin.Show();
        }

        /// <summary>
        /// Maneja el evento Click del control btnMenu.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnMenu_Click(object sender, EventArgs e)
        {
            MenuPrincipalEmp ME = new MenuPrincipalEmp();
            ME.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del control btnFacturas.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnFacturas_Click(object sender, EventArgs e)
        {
            FacturasEmp FE = new FacturasEmp();
            FE.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del control btnClientes.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnClientes_Click(object sender, EventArgs e)
        {
            ClientesEmp CE = new ClientesEmp();
            CE.Show();
            this.Hide();
        }



        /// <summary>
        /// Maneja el evento Click del control btnDeudores.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnDeudores_Click(object sender, EventArgs e)
        {
            Deudores_Emp DE = new Deudores_Emp();
            DE.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del control btnCerrar.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Login.Login login = new Login.Login();
            login.Show();
            this.Close();
        }

        /// <summary>
        /// Maneja el evento Click del control btnPerfil.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnPerfil_Click(object sender, EventArgs e)
        {
            Perfil perfil = new Perfil();
            perfil.Show();
        }
    }
}