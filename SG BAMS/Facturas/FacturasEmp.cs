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
    public partial class FacturasEmp : Form
    {
        DataTable datosFac;
        public FacturasEmp()
        {
            InitializeComponent();
            dgvFacturas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvFacturas.MultiSelect = false;
            dgvFacturas.AllowUserToAddRows = false;
        }

        private void dgvFacturas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e != null && e.RowIndex < 0) return;

            int idFacturas, idPago, bateriaVieja;
            string nombre_Cliente;
           
            DateTime fecha;

            if (dgvFacturas.CurrentRow != null)
            {

                idFacturas = Convert.ToInt32(dgvFacturas.CurrentRow.Cells[0].Value);
                nombre_Cliente = dgvFacturas.CurrentRow.Cells[2].Value.ToString();
                fecha = Convert.ToDateTime(dgvFacturas.CurrentRow.Cells[6].Value);
                bateriaVieja = Convert.ToInt32(dgvFacturas.CurrentRow.Cells[7].Value);
                idPago = Convert.ToInt32(dgvFacturas.CurrentRow.Cells[10].Value);
                double rebaja = Convert.ToDouble(dgvFacturas.CurrentRow.Cells["Rebaja"].Value);


                FacturaVer frmFV = new FacturaVer(idFacturas, nombre_Cliente, fecha, bateriaVieja, idPago, rebaja);
                frmFV.ShowDialog();



                CargarFactura();
            }
        }

        private async void BtnNueva_Click(object sender, EventArgs e)
        {
            using (ClienteAgregar frmCA = new ClienteAgregar())
            {
                if (frmCA.ShowDialog() == DialogResult.OK)
                {
                    string nombre = frmCA.NombreDelCliente;
                    int id = frmCA.IdClienteGenerado;

                    FacturaAgregarDatos factura = new FacturaAgregarDatos(nombre, id);
                    factura.Show(this);

                    await CargarFactura();
                }
            }
        }

        private void BtnVer_Click(object sender, EventArgs e)
        {
            if (dgvFacturas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar una fila",
                                "Ninguna fila seleccionada",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }
            if (dgvFacturas.CurrentRow != null)
            {
                dgvFacturas_CellDoubleClick(null, null);
            }

        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            if (datosFac != null)
            {
                DataView dv = datosFac.DefaultView;

                if (string.IsNullOrWhiteSpace(txtBusqueda.Text))
                {
                    dv.RowFilter = string.Empty;
                }
                else
                {
                    string textoSeguro = txtBusqueda.Text
                        .Replace("'", "''")
                        .Replace("[", "[[]")
                        .Replace("]", "[]]")
                        .Replace("*", "[*]")
                        .Replace("%", "[%]");

                    try
                    {
                        dv.RowFilter = string.Format(
                            "Convert([Factura], 'System.String') LIKE '%{0}%' OR " +
                            "[Vendedor] LIKE '%{0}%' OR " +
                            "[Cliente] LIKE '%{0}%' OR " +
                            "[Método de Pago] LIKE '%{0}%'",
                            textoSeguro);
                    }
                    catch (Exception)
                    {
                        dv.RowFilter = string.Empty;
                    }
                }

                dgvFacturas.DataSource = dv;
                dgvFacturas.ClearSelection();
            }
        }

        private async Task CargarFactura()
        {
            ClsVerFactura objFac = new ClsVerFactura();
            datosFac = await objFac.VerFacturas();

            if (datosFac != null)
            {
                dgvFacturas.DataSource = datosFac;
                dgvFacturas.Columns["Rebaja"].DisplayIndex = 8;
                dgvFacturas.Columns["Batería Vieja"].DisplayIndex = 7;
                dgvFacturas.Columns["Total Unidades"].DisplayIndex = 9;


                dgvFacturas.Columns["Factura"].HeaderText = "N° Factura";
                dgvFacturas.Columns["Vendedor"].HeaderText = "Vendedor";
                dgvFacturas.Columns["Cliente"].HeaderText = "Cliente";
                dgvFacturas.Columns["RTN Cliente"].HeaderText = "RTN Cliente";
                dgvFacturas.Columns["Método de Pago"].HeaderText = "Metodo de pago";
                dgvFacturas.Columns["ID Método de Pago"].Visible = false;
                dgvFacturas.Columns["Fecha"].HeaderText = "Fecha";
                dgvFacturas.Columns["Detalle Venta"].HeaderText = "Detalle Venta";
                dgvFacturas.Columns["Batería Vieja"].HeaderText = "Batería Vieja";
                dgvFacturas.Columns["Rebaja"].HeaderText = "Rebaja de Batería Vieja";
                dgvFacturas.Columns["Total Unidades"].HeaderText = "Total Unidades";


            }
        }

        private void BtnRefrescar_Click(object sender, EventArgs e)
        {
            dtpInicio.Value = DateTime.Today;
            dtpFin.Value = DateTime.Today;

            FiltrarPorFecha();

            txtBusqueda.Text = "";
            dgvFacturas.ClearSelection();
        
        }

        private async void FacturasEmp_Load(object sender, EventArgs e)
        {
            await CargarFactura();

            dtpInicio.Value = DateTime.Today;
            dtpFin.Value = DateTime.Today;

            FiltrarPorFecha(); 

            dtpFin.ValueChanged += dtpInicio_ValueChanged;
            dgvFacturas.ClearSelection();
            dgvFacturas.ReadOnly = true;
            dgvFacturas.AllowUserToOrderColumns = false;
        }

        private void FiltrarPorFecha()
        {
            if (datosFac != null)
            {
                DataView dv = datosFac.DefaultView;

                DateTime fechaInicio = dtpInicio.Value.Date;
                DateTime fechaFin = dtpFin.Value.Date;

                dv.RowFilter = string.Format(
                    "[Fecha] >= #{0}# AND [Fecha] <= #{1}#",
                    fechaInicio.ToString("MM/dd/yyyy"),
                    fechaFin.ToString("MM/dd/yyyy")
                );

                dgvFacturas.DataSource = dv;
            }
        }

        private void dtpInicio_ValueChanged(object sender, EventArgs e)
        {

            FiltrarPorFecha();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Login.Login login = new Login.Login();
            login.Show();
            this.Close();
        }

        private void BtnMenu_Click(object sender, EventArgs e)
        {
            MenuPrincipalEmp MPE = new MenuPrincipalEmp();
            MPE.Show();
            this.Close();
        }

        private void BtnFacturas_Click(object sender, EventArgs e)
        {

        }

        private void BtnClientes_Click(object sender, EventArgs e)
        {
            ClientesEmp CE = new ClientesEmp();
            CE.Show();
            this.Close();
        }

        private void BtnInventario_Click(object sender, EventArgs e)
        {
            InventarioEmp IE = new InventarioEmp();
            IE.Show();
            this.Close();
        }

        private void BtnDeudores_Click(object sender, EventArgs e)
        {
            Deudores_Emp DE = new Deudores_Emp();
            DE.Show();
            this.Close();
        }

       

        private void BtnNotificaciones_Click(object sender, EventArgs e)
        {
            NotificacionesAdmin NA = new NotificacionesAdmin();
            NA.Show();
            this.Hide();
        }
    }
}
