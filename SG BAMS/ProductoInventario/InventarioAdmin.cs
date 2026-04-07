using Microsoft.Data.SqlClient;
using SG_BAMS.Administracion_de_BAMS.MarcaProd;
using SG_BAMS.Bitacora;
using SG_BAMS.Login;
using SG_BAMS.ProductoInventario;
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
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class InventarioAdmin : Form
    {
        /// <summary>
        /// The logica
        /// </summary>
        ClsVerProducto logica = new ClsVerProducto();

        /// <summary>
        /// Initializes a new instance of the <see cref="InventarioAdmin"/> class.
        /// </summary>
        public InventarioAdmin()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.KeyPreview = true;
        }

        /// <summary>
        /// Cargars the inventario completo.
        /// </summary>
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

        /// <summary>
        /// Handles the Load event of the InventarioAdmin control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void InventarioAdmin_Load(object sender, EventArgs e)
        {
            btnInventario.Enabled = false;
            btnInventario.BackColor = Color.SkyBlue;
            btnInventario.ForeColor = Color.White;

            CargarInventarioCompleto();

            dgvProductosAdmin.BorderStyle = BorderStyle.None;
            dgvProductosAdmin.BackgroundColor = Color.White;
            dgvProductosAdmin.RowHeadersVisible = false;
            dgvProductosAdmin.EnableHeadersVisualStyles = false;
            dgvProductosAdmin.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgvProductosAdmin.ColumnHeadersDefaultCellStyle.BackColor = Color.SkyBlue;
            dgvProductosAdmin.ColumnHeadersDefaultCellStyle.ForeColor = Color.Navy;
            dgvProductosAdmin.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvProductosAdmin.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvProductosAdmin.ColumnHeadersHeight = 28;

            dgvProductosAdmin.DefaultCellStyle.BackColor = Color.White;
            dgvProductosAdmin.DefaultCellStyle.ForeColor = Color.Navy;
            dgvProductosAdmin.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvProductosAdmin.DefaultCellStyle.Padding = new Padding(3);
            dgvProductosAdmin.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 245, 255);
            dgvProductosAdmin.AlternatingRowsDefaultCellStyle.ForeColor = Color.Navy;

            dgvProductosAdmin.DefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            dgvProductosAdmin.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvProductosAdmin.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvProductosAdmin.GridColor = Color.LightGray;
            dgvProductosAdmin.RowTemplate.Height = 32;
            dgvProductosAdmin.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProductosAdmin.ClearSelection();
        }

        /// <summary>
        /// Handles the Click event of the btnAgregar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarProducto frm = new AgregarProducto();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                CargarInventarioCompleto();
            }
        }

        /// <summary>
        /// Handles the Click event of the kryptonButton10 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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
                dgvProductosAdmin.DataSource = logica.BuscarProductos(txtBuscar.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar: " + ex.Message, "BAMS");
            }
        }






        /// <summary>
        /// Handles the Click event of the btnNoti control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnNoti_Click(object sender, EventArgs e)
        {
            NotificacionesAdmin notificaciones = new NotificacionesAdmin();
            notificaciones.Show();
        }





        /// <summary>
        /// Handles the 1 event of the dgvProductosAdmin_CellDoubleClick control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// The ultima tecla escaner
        /// </summary>
        private DateTime ultimaTeclaEscaner = DateTime.Now;

        /// <summary>
        /// Processes a command key.
        /// </summary>
        /// <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the Win32 message to process.</param>
        /// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
        /// <returns>
        ///   <see langword="true" /> if the keystroke was processed and consumed by the control; otherwise, <see langword="false" /> to allow further processing.
        /// </returns>
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

        private void btnMenu_Click(object sender, EventArgs e)
        {
            MenuPrincipalAdm MPA = new MenuPrincipalAdm();
            MPA.Show();
            this.Hide();
        }

        private void btnFacturas_Click(object sender, EventArgs e)
        {
            FacturasAdm FA = new FacturasAdm();
            FA.Show();
            this.Hide();
        }

        private void btnCompra_Click(object sender, EventArgs e)
        {
            Compras CF = new Compras();
            CF.Show();
            this.Hide();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            ClientesAdm CA = new ClientesAdm();
            CA.Show();
            this.Hide();
        }

       
        private void btnProveedores_Click(object sender, EventArgs e)
        {
            ProveedoresAdmin PA = new ProveedoresAdmin();
            PA.Show();
            this.Hide();
        }

        private void btnDeudores_Click(object sender, EventArgs e)
        {
            DeudoresAdmin DA = new DeudoresAdmin();
            DA.Show();
            this.Hide();
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            ReportesAdmin RA = new ReportesAdmin();
            RA.Show();
            this.Hide();
        }

        private void btnBitacora_Click(object sender, EventArgs e)
        {
            BitacoraAdmin BA = new BitacoraAdmin();
            BA.Show();
            this.Hide();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
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