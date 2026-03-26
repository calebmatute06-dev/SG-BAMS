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
    public partial class DeudoresAdmin : Form
    {
        private DataTable dtDeudores;

        public DeudoresAdmin()
        {
            InitializeComponent();
            CargarGridDeudores();

            dgvDeudores.CellDoubleClick += dgvDeudores_CellDoubleClick;

            txtBuscarNombre.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
        }

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

        private void txtBuscarNombre_TextChanged(object sender, EventArgs e)
        {
            FiltrarDeudores();
        }

        private void kryptonButton12_Click(object sender, EventArgs e)
        {
            FiltrarDeudores();
        }

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

        private void kryptonButton15_Click(object sender, EventArgs e)
        {
            Pago_Deuda PagDe = new Pago_Deuda("", 0);
            PagDe.ShowDialog();
            CargarGridDeudores();
        }

        private void Deudores_Shown(object sender, EventArgs e)
        {
            Ayudante_UI.AplicarZoomGlobal(this);
        }

        private void txtBuscarNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.PermitirSoloLetras(e);
        }

        

        private void btnMenuAdmin_Click(object sender, EventArgs e)
        {
            new MenuPrincipalAdm().Show();
            this.Close();
        }

        private void btnfacturas_Click(object sender, EventArgs e)
        {
            new FacturasAdm().Show();
            this.Close();
        }

        private void btncompras2_Click(object sender, EventArgs e)
        {
            new Compras().Show();
            this.Close();
        }

        private void btnclientes2_Click(object sender, EventArgs e)
        {
            new ClientesAdm().Show();
            this.Close();
        }

        private void btninventario2_Click(object sender, EventArgs e)
        {
            new InventarioAdmin().Show();
            this.Close();
        }

        private void btnproveedores2_Click(object sender, EventArgs e)
        {
            new ProveedoresAdmin().Show();
            this.Close();
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            new ReportesAdmin().Show();
            this.Close();
        }

        private void btnreporte2_Click(object sender, EventArgs e)
        {
            new BitacoraAdmin().Show();
            this.Close();
        }

        private void btndeudores2_Click(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            new SG_BAMS.Login.Login().Show();
            this.Close();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            new NotificacionesAdmin().Show();
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            new Perfil().Show();
        }

        private void kryptonDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void dgvDeudores_DoubleClick(object sender, EventArgs e) { }

        private void DeudoresAdmin_Load(object sender, EventArgs e)
        {
            dgvDeudores.ClearSelection();
        }
    }
}