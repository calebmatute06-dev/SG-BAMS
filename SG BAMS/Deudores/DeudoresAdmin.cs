using SG_BAMS.Bitacora;
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
    public partial class DeudoresAdmin : Form
    {
        /// <summary>
        /// The dt deudores
        /// </summary>
        private DataTable dtDeudores;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeudoresAdmin"/> class.
        /// </summary>
        public DeudoresAdmin()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            CargarGridDeudores();

            dgvDeudores.CellDoubleClick += dgvDeudores_CellDoubleClick;

            txtBuscarNombre.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
        }

        /// <summary>
        /// Cargars the grid deudores.
        /// </summary>
        public void CargarGridDeudores()
        {
            ClsDeuda objetoDeuda = new ClsDeuda();
            dtDeudores = objetoDeuda.ListarDeudores();
            dgvDeudores.DataSource = dtDeudores;


            dgvDeudores.ReadOnly = true;
            dgvDeudores.AllowUserToAddRows = false;
            dgvDeudores.AllowUserToDeleteRows = false;


            dgvDeudores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDeudores.MultiSelect = false;

            dgvDeudores.ClearSelection();
        }

        /// <summary>
        /// Filtrars the deudores.
        /// </summary>
        private void FiltrarDeudores()
        {
            if (dtDeudores != null)
            {
                string filtro = txtBuscarNombre.Text
                    .Replace("'", "''")
                    .Replace("[", "[[]")
                    .Replace("]", "[]]")
                    .Trim();

                DataView dv = dtDeudores.DefaultView;
                dv.RowFilter = string.Format("Cliente LIKE '%{0}%'", filtro);
                dgvDeudores.DataSource = dv;
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the txtBuscarNombre control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txtBuscarNombre_TextChanged(object sender, EventArgs e)
        {
            FiltrarDeudores();
        }

        /// <summary>
        /// Handles the Click event of the kryptonButton12 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void kryptonButton12_Click(object sender, EventArgs e)
        {
            FiltrarDeudores();
        }

        /// <summary>
        /// Handles the CellDoubleClick event of the dgvDeudores control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dgvDeudores_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                DataRowView filaSeleccionada = (DataRowView)dgvDeudores.Rows[e.RowIndex].DataBoundItem;

                if (filaSeleccionada != null)
                {
                    int idDeuda = Convert.ToInt32(filaSeleccionada["ID Deuda"]);
                    string nombreCliente = filaSeleccionada["Cliente"].ToString().Trim();
                    string estadoDeuda = filaSeleccionada["Estado Deuda"].ToString().Trim();

                    if (estadoDeuda.Equals("Activo", StringComparison.OrdinalIgnoreCase))
                    {
                        Pago_Deuda pagDe = new Pago_Deuda(nombreCliente, idDeuda);

                        if (pagDe.ShowDialog() == DialogResult.OK)
                        {
                            CargarGridDeudores();
                            txtBuscarNombre.Clear();
                        }
                    }
                    else
                    {
                        MessageBox.Show($"La deuda de {nombreCliente} ya no está activa.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar procesar el pago: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the Click event of the kryptonButton15 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void kryptonButton15_Click(object sender, EventArgs e)
        {
            Pago_Deuda PagDe = new Pago_Deuda("", 0);
            PagDe.ShowDialog();
            CargarGridDeudores();
        }

        /// <summary>
        /// Handles the Shown event of the Deudores control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Deudores_Shown(object sender, EventArgs e)
        {
            Ayudante_UI.AplicarZoomGlobal(this);
        }

        /// <summary>
        /// Handles the KeyPress event of the txtBuscarNombre control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txtBuscarNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.PermitirSoloLetras(e);
        }





        /// <summary>
        /// Handles the Click event of the button12 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button12_Click(object sender, EventArgs e)
        {
            new NotificacionesAdmin().Show();
        }

      

        /// <summary>
        /// Handles the CellContentClick event of the kryptonDataGridView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void kryptonDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        /// <summary>
        /// Handles the DoubleClick event of the dgvDeudores control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void dgvDeudores_DoubleClick(object sender, EventArgs e) { }

        /// <summary>
        /// Handles the Load event of the DeudoresAdmin control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void DeudoresAdmin_Load(object sender, EventArgs e)
        {
            btnDeudores.Enabled = false;
            btnDeudores.BackColor = Color.SkyBlue;
            btnDeudores.ForeColor = Color.White;

            dgvDeudores.ClearSelection();
            dgvDeudores.BorderStyle = BorderStyle.None;
            dgvDeudores.BackgroundColor = Color.White;
            dgvDeudores.RowHeadersVisible = false;
            dgvDeudores.EnableHeadersVisualStyles = false;
            dgvDeudores.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgvDeudores.ColumnHeadersDefaultCellStyle.BackColor = Color.SkyBlue;
            dgvDeudores.ColumnHeadersDefaultCellStyle.ForeColor = Color.Navy;
            dgvDeudores.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvDeudores.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvDeudores.ColumnHeadersHeight = 28;

            dgvDeudores.DefaultCellStyle.BackColor = Color.White;
            dgvDeudores.DefaultCellStyle.ForeColor = Color.Navy;
            dgvDeudores.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvDeudores.DefaultCellStyle.Padding = new Padding(3);
            dgvDeudores.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 245, 255);
            dgvDeudores.AlternatingRowsDefaultCellStyle.ForeColor = Color.Navy;

            dgvDeudores.DefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            dgvDeudores.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvDeudores.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvDeudores.GridColor = Color.LightGray;
            dgvDeudores.RowTemplate.Height = 32;
            dgvDeudores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDeudores.ClearSelection();
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

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            ProveedoresAdmin PA = new ProveedoresAdmin();
            PA.Show();
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