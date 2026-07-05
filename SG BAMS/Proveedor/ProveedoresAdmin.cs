using SG_BAMS.Bitacora;
using SG_BAMS.Proveedor.DTO;
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

namespace SG_BAMS.Proveedor
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class ProveedoresAdmin : Form
    {
        /// <summary>
        /// El proveedor
        /// </summary>
        ClsProveedor proveedor = new ClsProveedor();

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ProveedoresAdmin"/>.
        /// </summary>
        public ProveedoresAdmin()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
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
        /// Maneja el evento Load del control ProveedoresAdmin.
        /// </summary>
        private void ProveedoresAdmin_Load(object sender, EventArgs e)
        {
            new PlaceholderTextBox(txtBuscar, "Ingrese un Nombre de Vendedor, Cliente, N.Factura, RTN");
            btnProveedores.Enabled = false;
            btnProveedores.BackColor = Color.SkyBlue;
            btnProveedores.ForeColor = Color.White;
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

            ClsMensajeGuia.ActivarK(txtBuscar);
        }

        /// <summary>
        /// Maneja el evento KeyUp del control txtBuscar.
        /// </summary>
        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            proveedor.BuscarProveedor(txtBuscar, dgvProveedor);
        }

        /// <summary>
        /// Maneja el evento Click del control btnAgregar.
        /// </summary>
        private void btnAgregar1_Click(object sender, EventArgs e)
        {
            AgregarProveedores agregar = new AgregarProveedores();
            agregar.ShowDialog();
            proveedor.cargarDatos(dgvProveedor);
            dgvProveedor.ClearSelection();
        }

        /// <summary>
        /// Maneja el evento Click del control btnModificar.
        /// </summary>
        private void btnModificar_Click_1(object sender, EventArgs e)
        {
            if (dgvProveedor.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un proveedor.");
                return;
            }

            AbrirModificarProveedor(dgvProveedor.CurrentRow);
            dgvProveedor.ClearSelection();
        }

        /// <summary>
        /// Maneja el evento Click del control btnRefresh.
        /// </summary>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            proveedor.cargarDatos(dgvProveedor);
        }

        /// <summary>
        /// Maneja el evento CellDoubleClick del control dgvProveedor.
        /// </summary>
        private void dgvProveedor_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvProveedor.Rows[e.RowIndex];
                AbrirModificarProveedor(fila);
            }
        }

        /// <summary>
        /// Arma el ProveedorDTO a partir de la fila seleccionada y abre la
        /// pantalla de modificación. Antes se pasaban 7 parámetros sueltos
        /// al constructor de ModificarProveedor; ahora viaja un solo objeto.
        /// </summary>
        /// <param name="fila">La fila seleccionada del grid.</param>
        private void AbrirModificarProveedor(DataGridViewRow fila)
        {
            if (fila == null)
            {
                MessageBox.Show("Seleccione un proveedor.");
                return;
            }

            ProveedorDTO proveedorDTO = new ProveedorDTO
            {
                IdProveedor = Convert.ToInt32(fila.Cells["idProveedor"].Value),
                Nombre = fila.Cells["Nombre"].Value.ToString(),
                Contacto = fila.Cells["Contacto"].Value.ToString(),
                Direccion = fila.Cells["Dirección"].Value.ToString(),
                Rtn = fila.Cells["RTN"].Value.ToString(),
                IdEstado = Convert.ToInt32(fila.Cells["idEstado"].Value),
                IdClasificacion = Convert.ToInt32(fila.Cells["idClasificacion"].Value)
            };

            ModificarProveedor frm = new ModificarProveedor(proveedorDTO);
            frm.ShowDialog();
            proveedor.cargarDatos(dgvProveedor);
            dgvProveedor.ClearSelection();
        }

        /// <summary>
        /// Maneja el evento Click del control btnNoti.
        /// </summary>
        private void btnNoti_Click(object sender, EventArgs e)
        {
            NotificacionesAdmin notificaciones = new NotificacionesAdmin();
            notificaciones.Show();
        }

        /// <summary>
        /// Maneja el evento CellDoubleClick del control dgvProveedor.
        /// </summary>
        private void dgvProveedor_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                AbrirModificarProveedor(dgvProveedor.Rows[e.RowIndex]);
            }
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

        private void btnInventario_Click(object sender, EventArgs e)
        {
            InventarioAdmin IA = new InventarioAdmin();
            IA.Show();
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
            DialogResult resultado = MessageBox.Show(
                "¿Está seguro que desea cerrar sesión?",
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                Login.Login login = new Login.Login();
                login.Show();
                this.Close();
            }
        }

        private void btnPerfil_Click(object sender, EventArgs e)
        {
            Perfil perfil = new Perfil();
            perfil.Show();
        }
    }
}
