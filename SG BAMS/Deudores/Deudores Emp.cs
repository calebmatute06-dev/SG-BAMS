using SG_BAMS.Bitacora;
using SG_BAMS.Proveedor;
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
    public partial class Deudores_Emp : Form
    {
        private DataTable dtDeudores;

        public Deudores_Emp()
        {
            InitializeComponent();
            CargarGridDeudores();

            
            this.txtBuscarNombre.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
        }

        public void CargarGridDeudores()
        {
            ClsDeuda objetoDeuda = new ClsDeuda();
            dtDeudores = objetoDeuda.ListarDeudores();
            dgvDeudores.DataSource = dtDeudores;
        }

        private void txtBuscarNombre_TextChanged(object sender, EventArgs e)
        {
           
            int cursor = txtBuscarNombre.SelectionStart;
            txtBuscarNombre.Text = txtBuscarNombre.Text.ToUpper();
            txtBuscarNombre.SelectionStart = cursor;

            FiltrarDeudores();
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

                dtDeudores.DefaultView.RowFilter = string.Format("Cliente LIKE '%{0}%'", filtro);
                dgvDeudores.DataSource = dtDeudores.DefaultView;
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
                        MessageBox.Show($"La deuda de {nombreCliente} ya no está activa.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al procesar el pago: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Deudores_Shown(object sender, EventArgs e)
        {
            Ayudante_UI.AplicarZoomGlobal(this);
        }

        
        private void txtBuscarNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            ClsValidaciones.PermitirSoloLetras(e);
        }

        

        private void btnNoti(object sender, EventArgs e)
        {
            new NotificacionesEmp().Show();
        }

        private void btnMenuEmp_Click(object sender, EventArgs e)
        {
            new MenuPrincipalEmp().Show();
            this.Close(); 
        }

        private void btnFacturasEmp_Click(object sender, EventArgs e)
        {
            new FacturasEmp().Show();
            this.Close();
        }

        private void btnClientesEmp_Click(object sender, EventArgs e)
        {
            new ClientesEmp().Show();
            this.Close();
        }

        private void btnInventarioEmp_Click(object sender, EventArgs e)
        {
            new InventarioEmp().Show();
            this.Close();
        }

        private void btnPerfil_Click(object sender, EventArgs e)
        {
            new Perfil().Show();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            new SG_BAMS.Login.Login().Show();
            this.Close();
        }

       
        private void timer1_Tick(object sender, EventArgs e) { }
        private void dgvDeudores_DoubleClick(object sender, EventArgs e) { }
    }
}