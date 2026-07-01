using Microsoft.Data.SqlClient;
using SG_BAMS.Cliente;
using SG_BAMS.Facturas;
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
    public partial class FacturaVer : Form
    {
        /// <summary>
        /// The identifier fac
        /// </summary>
        int idFac;
        /// <summary>
        /// The datos cli
        /// </summary>
        DataTable datosCli;
        /// <summary>
        /// The identifier pago sele
        /// </summary>
        int idPagoSele;
        /// <summary>
        /// The monto rebaja
        /// </summary>
        double monto_rebaja;

        /// <summary>
        /// Initializes a new instance of the <see cref="FacturaVer"/> class.
        /// </summary>
        /// <param name="idF">The identifier f.</param>
        /// <param name="nomFac">The nom fac.</param>
        /// <param name="fec">The fec.</param>
        /// <param name="bateriaVij">The bateria vij.</param>
        /// <param name="idPago">The identifier pago.</param>
        /// <param name="reb">The reb.</param>
        public FacturaVer(int idF, string nomFac, DateTime fec, int bateriaVij, int idPago, double reb, string ven)
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            txtCliente.Text = nomFac;
            txtBateriaVieja.Text = bateriaVij.ToString();
            idPagoSele = idPago;
            idFac = idF;
            fechaDT.Value = fec;
            lblFactura.Text = "No." + idF.ToString();
            monto_rebaja = reb;
            txtVendedor.Text = ven;
           
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="FacturaVer"/> class.
        /// </summary>
        public FacturaVer()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

        }

        /// <summary>
        /// Vers the facturas productos.
        /// </summary>
        private async Task VerFacturasProductos()
        {

            ClsDetalleFactura objVFP = new ClsDetalleFactura();
            datosCli = await objVFP.VerFacturasProducto(idFac);

            if (datosCli != null)
            {
                dgvFacturas.DataSource = datosCli;
                dgvFacturas.CellFormatting += (s, ev) =>
                {
                    if (ev.RowIndex < 0 || ev.Value == null) return;
                    string col = dgvFacturas.Columns[ev.ColumnIndex].Name;
                    if ((col == "Precio" || col == "Subtotal") &&
                        decimal.TryParse(ev.Value.ToString(), out decimal monto))
                    {
                        ev.Value = $"L. {monto:N2}";
                        ev.FormattingApplied = true;
                    }
                };
                CalcularTotal();

                dgvFacturas.Columns["ID_Factura"].Visible = false;
                dgvFacturas.Columns["ID"].HeaderText = "ID Producto";
                dgvFacturas.Columns["Nombre Completo"].Width = 200;
                dgvFacturas.Columns["Cantidad"].HeaderText = "Cantidad";
                dgvFacturas.Columns["Precio"].HeaderText = "Precio";
                dgvFacturas.Columns["Subtotal"].HeaderText = "Subtotal";

            }
        }

        /// <summary>
        /// Calculars the total.
        /// </summary>
        private void CalcularTotal()
        {
            double acumulador = 0;

            
            for (int i = 0; i < dgvFacturas.Rows.Count; i++)
            {
                if (dgvFacturas.Rows[i].Cells["Subtotal"].Value != null)
                {
                    acumulador += Convert.ToDouble(dgvFacturas.Rows[i].Cells["Subtotal"].Value);
                }
            }

           
            double rebaja = monto_rebaja;

            double total = acumulador - rebaja;

            txtSubtotal.Text = $"L. {acumulador:N2}";
            txtRebaja.Text = $"L. {rebaja:N2}";
            txtTotal.Text = $"L. {(total < 0 ? 0 : total):N2}";
        }

        /// <summary>
        /// Handles the Load event of the FacturaVer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void FacturaVer_Load(object sender, EventArgs e)
        {
            fechaDT.Enabled = false;

            await LlenarComboPago();
            cmbPago.SelectedValue = idPagoSele;
            await VerFacturasProductos();
            txtBateriaVieja.ReadOnly = true;
            txtCliente.ReadOnly = true;
            txtTotal.ReadOnly = true;
            txtRebaja.ReadOnly = true;
            txtSubtotal.ReadOnly = true;
            dgvFacturas.ReadOnly = true;
            dgvFacturas.AllowUserToOrderColumns = false;
            dgvFacturas.AllowUserToAddRows = false;


            dgvFacturas.BorderStyle = BorderStyle.None;
            dgvFacturas.BackgroundColor = Color.White;
            dgvFacturas.RowHeadersVisible = false;
            dgvFacturas.EnableHeadersVisualStyles = false;
            dgvFacturas.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgvFacturas.ColumnHeadersDefaultCellStyle.BackColor = Color.SkyBlue;
            dgvFacturas.ColumnHeadersDefaultCellStyle.ForeColor = Color.Navy;
            dgvFacturas.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvFacturas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvFacturas.ColumnHeadersHeight = 28;

            dgvFacturas.DefaultCellStyle.BackColor = Color.White;
            dgvFacturas.DefaultCellStyle.ForeColor = Color.Navy;
            dgvFacturas.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvFacturas.DefaultCellStyle.Padding = new Padding(3);
            dgvFacturas.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 245, 255);
            dgvFacturas.AlternatingRowsDefaultCellStyle.ForeColor = Color.Navy;

            dgvFacturas.DefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            dgvFacturas.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvFacturas.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvFacturas.GridColor = Color.LightGray;
            dgvFacturas.RowTemplate.Height = 32;
            dgvFacturas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvFacturas.ClearSelection();



        }

        /// <summary>
        /// Llenars the combo pago.
        /// </summary>
        private async Task LlenarComboPago()
        {
            ClsRepositorioBaseDatos objCl = new ClsRepositorioBaseDatos();
            try
            {
                objCl.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_FormasPago_Listar", objCl.Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        cmbPago.DisplayMember = "descripcion_forma_pago";
                        cmbPago.ValueMember = "id_tipo_forma_pago";
                        cmbPago.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al llenar ComboBox: " + ex.Message);
            }
            finally
            {
                objCl.Cerrar();
            }
        }

        /// <summary>
        /// Handles the Click event of the BtnSalir control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
