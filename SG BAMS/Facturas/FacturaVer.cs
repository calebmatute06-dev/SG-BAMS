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
    public partial class FacturaVer : Form
    {
        int idFac;
        DataTable datosCli;
        int idPagoSele;
        double monto_rebaja;

        public FacturaVer(int idF, string nomFac, DateTime fec, int bateriaVij, int idPago, double reb)
        {
            InitializeComponent();

            txtCliente.Text = nomFac;
            txtBateriaVieja.Text = bateriaVij.ToString();
            idPagoSele = idPago;
            idFac = idF;
            fechaDT.SelectionStart = fec;
            lblFactura.Text = "No." + idF.ToString();
            monto_rebaja = reb;

        }
        public FacturaVer()
        {
            InitializeComponent();
        }

        private async Task VerFacturasProductos()
        {

            ClsVerFacturaProducto objVFP = new ClsVerFacturaProducto();
            datosCli = await objVFP.VerFacturasProducto(idFac);

            if (datosCli != null)
            {
                dgvFacturas.DataSource = datosCli;
                CalcularTotal();

                dgvFacturas.Columns["ID_Factura"].Visible = false;
                dgvFacturas.Columns["ID"].HeaderText = "ID Producto";
                dgvFacturas.Columns["Nombre"].HeaderText = "Nombre";
                dgvFacturas.Columns["Cantidad"].HeaderText = "Cantidad";
                dgvFacturas.Columns["Precio"].HeaderText = "Precio";
                dgvFacturas.Columns["Subtotal"].HeaderText = "Subtotal";

            }
        }

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

            txtSubtotal.Text = acumulador.ToString();
            txtRebaja.Text = rebaja.ToString(); 
            txtTotal.Text = total.ToString();
        }

        private async void FacturaVer_Load(object sender, EventArgs e)
        {
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



        }

        private async Task LlenarComboPago()
        {
            ClsConexion objCl = new ClsConexion();
            try
            {
                objCl.AbrirConexion();

                string query = "SELECT *  FROM Tipo_Forma_de_pago";


                using (SqlCommand cmd = new SqlCommand(query, objCl.Conectar))
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);


                    cmbPago.DisplayMember = "descripcion_forma_pago";
                    cmbPago.ValueMember = "id_tipo_forma_pago";
                    cmbPago.DataSource = dt;
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

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
