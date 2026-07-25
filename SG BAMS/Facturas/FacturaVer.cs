using Microsoft.Data.SqlClient;
using SG_BAMS.Cliente;
using SG_BAMS.Facturas;
using SG_BAMS.Dominio;
using SG_BAMS.LogicaNegocio;
using SG_BAMS.AccesoDatos;
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
        /// DTO con los datos de la factura que se está visualizando.
        /// Reemplaza los campos sueltos (idFac, idPagoSele, monto_rebaja) que
        /// antes venían por el constructor.
        /// </summary>
        private FacturaDTO facturaDTO;
        private readonly ClsFactura FAC = new ClsFactura();
        private readonly ClsDetalleFactura detalleFactura = new ClsDetalleFactura();

        /// <summary>
        /// The datos cli
        /// </summary>
        DataTable datosCli;

        /// <summary>
        /// Initializes a new instance of the <see cref="FacturaVer"/> class.
        /// </summary>
        /// <param name="dto">Datos de la factura a visualizar.</param>
        public FacturaVer(FacturaDTO dto) : this(dto, new ClsDetalleFactura())
        {
        }

        internal FacturaVer(FacturaDTO dto, ClsDetalleFactura detalleFactura)
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;

            facturaDTO = dto;
            this.detalleFactura = detalleFactura;

            txtCliente.Text = facturaDTO.NombreCliente;
            txtBateriaVieja.Text = facturaDTO.CantidadBateriaVieja.ToString();
            fechaDT.Value = facturaDTO.Fecha;
            lblFactura.Text = "No." + facturaDTO.IdFactura;
            txtVendedor.Text = facturaDTO.Vendedor;
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
            datosCli = await detalleFactura.VerFacturasProducto(facturaDTO.IdFactura);

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
                LlenarDetalleDesdeGrid();
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
            txtSubtotal.Text = $"L. {facturaDTO.Subtotal:N2}";
            txtRebaja.Text = $"L. {facturaDTO.RebajaBateria:N2}";
            txtTotal.Text = $"L. {facturaDTO.Total:N2}";
        }

        /// <summary>
        /// Handles the Load event of the FacturaVer control. 
        /// </summary>
        private async void FacturaVer_Load(object sender, EventArgs e)
        {
            fechaDT.Enabled = false;

            await LlenarComboPago();
            cmbPago.SelectedValue = facturaDTO.IdFormaPago;
            await VerFacturasProductos();
            txtBateriaVieja.ReadOnly = true;
            txtCliente.ReadOnly = true;
            txtTotal.ReadOnly = true;
            txtRebaja.ReadOnly = true;
            txtSubtotal.ReadOnly = true;
            dgvFacturas.ReadOnly = true;
            dgvFacturas.AllowUserToOrderColumns = false;
            dgvFacturas.AllowUserToAddRows = false;

            EstiloDataGridView.Aplicar(dgvFacturas);
        }

        /// <summary>
        /// Llenars the combo pago.
        /// </summary>
        private async Task LlenarComboPago()
        {
            try
            {
                DataTable dt = await FAC.ObtenerFormasPago();
                cmbPago.DisplayMember = "descripcion_forma_pago";
                cmbPago.ValueMember = "id_tipo_forma_pago";
                cmbPago.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al llenar ComboBox: " + ex.Message);
            }
        }

        private void LlenarDetalleDesdeGrid()
        {
            facturaDTO.Detalle.Clear();

            foreach (DataRow fila in datosCli.Rows)
            {
                facturaDTO.Detalle.Add(new DetalleDTO
                {
                    Cantidad = Convert.ToInt32(fila["Cantidad"]),
                    Precio = Convert.ToDouble(fila["Precio"])
                });
            }
        }

        /// <summary>
        /// Handles the Click event of the BtnSalir control.
        /// </summary>
        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}