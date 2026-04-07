using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SG_BAMS.Bitacora;
using SG_BAMS.Reporte;

namespace SG_BAMS.Proveedor
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class ProveedoresAdmin : Form
    {
        /// <summary>
        /// The proveedor
        /// </summary>
        ClsProveedor proveedor = new ClsProveedor();

        /// <summary>
        /// Initializes a new instance of the <see cref="ProveedoresAdmin"/> class.
        /// </summary>
        public ProveedoresAdmin()
        {
            InitializeComponent();

            txtBuscar.KeyPress += (s, e) =>
            {
                if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) &&
                    !char.IsControl(e.KeyChar) && e.KeyChar != '&')
                {
                    e.Handled = true;
                }
            };

            proveedor.cargarDatos(dgvProveedor);
            dgvProveedor.ClearSelection();


        }

        /// <summary>
        /// Handles the Load event of the ProveedoresAdmin control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ProveedoresAdmin_Load(object sender, EventArgs e)
        {
            proveedor.cargarDatos(dgvProveedor);
            dgvProveedor.Columns["idProveedor"].Visible = false;
            dgvProveedor.Columns["idClasificacion"].Visible = false;
            dgvProveedor.Columns["idEstado"].Visible = false;
            dgvProveedor.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProveedor.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProveedor.AllowUserToAddRows = false;
            dgvProveedor.ReadOnly = true;

            dgvProveedor.BorderStyle = BorderStyle.None;
            dgvProveedor.BackgroundColor = Color.White;
            dgvProveedor.RowHeadersVisible = false;
            dgvProveedor.EnableHeadersVisualStyles = false;
            dgvProveedor.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgvProveedor.ColumnHeadersDefaultCellStyle.BackColor = Color.SkyBlue;
            dgvProveedor.ColumnHeadersDefaultCellStyle.ForeColor = Color.Navy;
            dgvProveedor.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvProveedor.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvProveedor.ColumnHeadersHeight = 28;

            dgvProveedor.DefaultCellStyle.BackColor = Color.White;
            dgvProveedor.DefaultCellStyle.ForeColor = Color.Navy;
            dgvProveedor.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvProveedor.DefaultCellStyle.Padding = new Padding(3);
            dgvProveedor.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 245, 255);
            dgvProveedor.AlternatingRowsDefaultCellStyle.ForeColor = Color.Navy;

            dgvProveedor.DefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            dgvProveedor.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvProveedor.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvProveedor.GridColor = Color.LightGray;
            dgvProveedor.RowTemplate.Height = 32;
            dgvProveedor.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProveedor.ClearSelection();

        }

        /// <summary>
        /// Handles the Click event of the btnAgregar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarProveedores agregar = new AgregarProveedores();
            agregar.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles the Click event of the btnCerrarSesion control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Login.Login login = new Login.Login();
            this.Hide();
            login.Show();
        }

        /// <summary>
        /// Handles the Click event of the btnAjustes control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnAjustes_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the Click event of the btnMenu control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnMenu_Click(object sender, EventArgs e)
        {
            MenuPrincipalAdm menu = new MenuPrincipalAdm();
            menu.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles the Click event of the btnFacturas control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnFacturas_Click(object sender, EventArgs e)
        {
            FacturasAdm facturas = new FacturasAdm();
            facturas.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles the Click event of the btnBitacora control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnBitacora_Click(object sender, EventArgs e)
        {
            BitacoraAdmin bitacora = new BitacoraAdmin();
            bitacora.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles the KeyUp event of the txtBuscar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            proveedor.BuscarProveedor(txtBuscar, dgvProveedor);
        }

        /// <summary>
        /// Handles the 1 event of the btnAgregar_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            AgregarProveedores agregar = new AgregarProveedores();
            agregar.ShowDialog();
            proveedor.cargarDatos(dgvProveedor);
            dgvProveedor.ClearSelection();
        }

        /// <summary>
        /// Handles the Click event of the btnModificar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvProveedor.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un proveedor.");
                return;
            }
            ModificarProveedor(dgvProveedor.CurrentRow);
        }

        /// <summary>
        /// Handles the Click event of the btnRefresh control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            proveedor.cargarDatos(dgvProveedor);
        }

        /// <summary>
        /// Handles the Click event of the btnCompras control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnCompras_Click(object sender, EventArgs e)
        {
            Compras compras = new Compras();
            compras.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles the Click event of the btnClientes control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnClientes_Click(object sender, EventArgs e)
        {
            ClientesAdm clientes = new ClientesAdm();
            clientes.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles the Click event of the btnInventario control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnInventario_Click(object sender, EventArgs e)
        {
            InventarioAdmin inventario = new InventarioAdmin();
            inventario.Show();
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
            ReportesAdmin reportes = new ReportesAdmin();
            reportes.Show();
            this.Close();
        }

        /// <summary>
        /// Handles the CellDoubleClick event of the dgvProveedor control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dgvProveedor_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvProveedor.Rows[e.RowIndex];
                ModificarProveedor(fila);
            }
        }

        /// <summary>
        /// Modificars the proveedor.
        /// </summary>
        /// <param name="fila">The fila.</param>
        private void ModificarProveedor(DataGridViewRow fila)
        {
            if (fila == null)
            {
                MessageBox.Show("Seleccione un proveedor.");
                return;
            }

            int idProveedor = Convert.ToInt32(fila.Cells["idProveedor"].Value);
            string nombre = fila.Cells["Nombre"].Value.ToString();
            string contacto = fila.Cells["Contacto"].Value.ToString();
            string direccion = fila.Cells["Dirección"].Value.ToString();
            string rtn = fila.Cells["RTN"].Value.ToString();
            int idEstado = Convert.ToInt32(fila.Cells["idEstado"].Value);
            int idClasificacion = Convert.ToInt32(fila.Cells["idClasificacion"].Value);

            ModificarProveedor frm = new ModificarProveedor(idProveedor, nombre, contacto, direccion, rtn, idEstado, idClasificacion);
            frm.ShowDialog();

            proveedor.cargarDatos(dgvProveedor);
            dgvProveedor.ClearSelection();
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
        /// Handles the 1 event of the dgvProveedor_CellDoubleClick control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dgvProveedor_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                ModificarProveedor(dgvProveedor.Rows[e.RowIndex]);
            }
        }


    }
}