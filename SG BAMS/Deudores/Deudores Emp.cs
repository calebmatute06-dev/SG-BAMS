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
        // Variable global para manejar el filtrado
        private DataTable dtDeudores;

        public void CargarGridDeudores()
        {
            try
            {
                ClsDeuda objetoDeuda = new ClsDeuda();
                dtDeudores = objetoDeuda.ListarDeudores();
                dgvDeudores.DataSource = dtDeudores;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public Deudores_Emp()
        {
            InitializeComponent();
            CargarGridDeudores();

            this.txtBuscarNombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscarNombre_KeyPress);
        }

        private void kryptonButton11_Click(object sender, EventArgs e)
        {
            Ajustes Ajust = new Ajustes();
            Ajust.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            NotificacionesEmp Notiemp = new NotificacionesEmp();
            Notiemp.Show();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Hide();
            SG_BAMS.Login.Login log = new SG_BAMS.Login.Login();
            log.Show();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            Perfil Per = new Perfil();
            Per.Show();
        }

        private void kryptonButton9_Click(object sender, EventArgs e)
        {
            MenuPrincipalEmp Menemp = new MenuPrincipalEmp();
            Menemp.Show();
            this.Hide();
        }

        private void kryptonButton8_Click(object sender, EventArgs e)
        {
            FacturasEmp Factemp = new FacturasEmp();
            Factemp.Show();
            this.Close();
        }

        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            ClientesEmp Clientemp = new ClientesEmp();
            Clientemp.Show();
            this.Close();
        }

        private void kryptonButton15_Click(object sender, EventArgs e)
        {
            Pago_Deuda PagDe = new Pago_Deuda();
            PagDe.ShowDialog();
            CargarGridDeudores();
        }

        // --- LÓGICA DE BÚSQUEDA Y PAGOS ---

        // El botón de la lupa / buscar
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

        // DOBLE CLIC EN EL GRID
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

        private void Deudores_Emp_Shown(object sender, EventArgs e)
        {
            Ayudante_UI.AplicarZoomGlobal(this);
        }

        // Eventos vacíos para evitar errores de referencia si existen en el designer
        private void kryptonDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void timer1_Tick(object sender, EventArgs e) { }
        private void dgvDeudores_DoubleClick(object sender, EventArgs e) { }

        private void btnreporte2_Click(object sender, EventArgs e)
        {

        }

        private void btndeudores2_Click(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void btnproveedores2_Click(object sender, EventArgs e)
        {

        }

        private void btninventario2_Click(object sender, EventArgs e)
        {
            InventarioEmp inventarioEmp = new InventarioEmp();
            inventarioEmp.Show();
            this.Close();
        }

        private void btnclientes2_Click(object sender, EventArgs e)
        {
            ClientesEmp Clien = new ClientesEmp();
            Clien.Show();
            this.Close();
        }

        private void btncompras2_Click(object sender, EventArgs e)
        {

        }

        private void btnfacturas2_Click(object sender, EventArgs e)
        {
            FacturasEmp Fact = new FacturasEmp();
            Fact.Show();
            this.Close();
        }

        private void kryptonButton2_Click_1(object sender, EventArgs e)
        {
            MenuPrincipalEmp Men = new MenuPrincipalEmp();
            Men.Show();
            this.Close();
        }

        private void pictureBox22_Click(object sender, EventArgs e)
        {

        }

        private void txtBuscarNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Solo permite letras, espacios y teclas de control (como Borrar)
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}