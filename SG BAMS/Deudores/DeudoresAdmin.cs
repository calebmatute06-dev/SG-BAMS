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

            txtBuscarNombre.KeyPress += (s, e) => ClsValidaciones.ValidarBusquedaAlfanumerica(e);
        }

        public void CargarGridDeudores()
        {
            ClsDeuda objetoDeuda = new ClsDeuda();
            dtDeudores = objetoDeuda.ListarDeudores();
            dgvDeudores.DataSource = dtDeudores;
        }

       

        private void btnMenuAdmin_Click(object sender, EventArgs e)
        {
            MenuPrincipalAdm Men = new MenuPrincipalAdm();
            Men.Show();
            this.Close();
        }

        private void btnfacturas_Click(object sender, EventArgs e)
        {
            FacturasAdm Fact = new FacturasAdm();
            Fact.Show();
            this.Close();
        }

        private void btncompras2_Click(object sender, EventArgs e)
        {
            Compras Comp = new Compras();
            Comp.Show();
            this.Close();
        }

        private void btnclientes2_Click(object sender, EventArgs e)
        {
            ClientesAdm Clien = new ClientesAdm();
            Clien.Show();
            this.Close();
        }

        private void btninventario2_Click(object sender, EventArgs e)
        {
            InventarioAdmin inventarioAdmin = new InventarioAdmin();
            inventarioAdmin.Show();
            this.Close();
        }
        private void btnproveedores2_Click(object sender, EventArgs e)
        {
            ProveedoresAdmin Pro = new ProveedoresAdmin();
            Pro.Show();
            this.Close();
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            ReportesAdmin reportesAdmin = new ReportesAdmin();
            reportesAdmin.Show();
            this.Close();
        }

        private void btnreporte2_Click(object sender, EventArgs e)
        {
            BitacoraAdmin Bit = new BitacoraAdmin();
            Bit.Show();
            this.Close();
        }
        private void btndeudores2_Click(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            SG_BAMS.Login.Login log = new SG_BAMS.Login.Login();
            log.Show();
            this.Close();
        }


        private void button12_Click(object sender, EventArgs e)
        {
            NotificacionesAdmin Notad = new NotificacionesAdmin();
            Notad.Show();
        }


       
        private void kryptonButton12_Click(object sender, EventArgs e)
        {
            FiltrarDeudores();
        }

        private void txtBuscarNombre_TextChanged(object sender, EventArgs e)
        {
            FiltrarDeudores();
        }

        private void FiltrarDeudores()
        {
            if (dtDeudores != null)
            {
                string filtro = txtBuscarNombre.Text.Trim();
                DataView dv = dtDeudores.DefaultView;
                dv.RowFilter = string.Format("Cliente LIKE '%{0}%'", filtro);
                dgvDeudores.DataSource = dv;
            }
        }

        
        private void kryptonButton15_Click(object sender, EventArgs e)
        {
            
            Pago_Deuda PagDe = new Pago_Deuda("", 0);
            PagDe.ShowDialog();
            CargarGridDeudores();
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
                        MessageBox.Show($"La deuda de {nombreCliente} ya no está activa.", "Información");
                    }
                }
            }
            catch (ArgumentException ex)
            {
                
                MessageBox.Show("Error: No se encuentra la columna. Verifica si el nombre es 'ID Deuda'. \nDetalle: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir el pago: " + ex.Message);
            }
        }

        private void Deudores_Shown(object sender, EventArgs e)
        {
            Ayudante_UI.AplicarZoomGlobal(this);
        }

        
        private void kryptonDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void dgvDeudores_DoubleClick(object sender, EventArgs e) { }

        private void txtBuscarNombre_KeyPress(object sender, KeyPressEventArgs e)
        {

            ClsValidaciones.PermitirSoloLetras(e);
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            Perfil Per = new Perfil();
            Per.Show();
        }
    }
}