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
    public partial class Deudores : Form
    {

        // Variable global para manejar el filtrado (PascalCase por ser campo de clase)
        private DataTable dtDeudores;

        public void CargarGridDeudores()
        {
            ClsDeuda objetoDeuda = new ClsDeuda();
            // Es vital que se asigne a la variable global para que el buscador trabaje con datos frescos
            dtDeudores = objetoDeuda.ListarDeudores();
            dgvDeudores.DataSource = dtDeudores;
        }

        public Deudores()
        {
            InitializeComponent();
            CargarGridDeudores();


        }




        private void kryptonButton9_Click(object sender, EventArgs e)
        {
            MenuPrincipalAdm Menad = new MenuPrincipalAdm();
            Menad.Show();
            this.Close();

        }

        private void kryptonButton8_Click(object sender, EventArgs e)
        {
            FacturasAdm factad = new FacturasAdm();
            factad.Show();
            this.Close();
        }

        private void kryptonButton7_Click(object sender, EventArgs e)
        {
            Compras Comp = new Compras();
            Comp.Show();
            this.Close();
        }

        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            ClientesAdm Clientad = new ClientesAdm();
            Clientad.Show();
            this.Close();
        }

        private void kryptonButton5_Click(object sender, EventArgs e)
        {
            InventarioAdmin Invad = new InventarioAdmin();
            Invad.Show();
            this.Close();
        }

        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            ProveedoresAdmin Proad = new ProveedoresAdmin();
            Proad.Show();
            this.Close();
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            this.Show();

        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            ReporteAdmin Repoad = new ReporteAdmin();
            Repoad.Show();
            this.Close();
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            Bitacora Bit = new Bitacora();
            Bit.Show();
            this.Close();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            SG_BAMS.Login.Login log = new SG_BAMS.Login.Login();
            log.Show();
            this.Close();


        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            Perfil Per = new Perfil();
            Per.Show();

        }

        private void button12_Click(object sender, EventArgs e)
        {
            NotificacionesAdmin Notad = new NotificacionesAdmin();
            Notad.Show();
            this.Close();
        }

        private void kryptonButton11_Click(object sender, EventArgs e)
        {
            Ajustes Ajus = new Ajustes();
            Ajus.Show();


        }

        private void Deudores_Shown(object sender, EventArgs e)
        {
            Ayudante_UI.AplicarZoomGlobal(this);
        }

        private void kryptonDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void kryptonButton15_Click(object sender, EventArgs e)
        {
            Pago_Deuda PagDe = new Pago_Deuda();
            PagDe.ShowDialog();
            CargarGridDeudores();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void kryptonButton12_Click(object sender, EventArgs e)
        {
            if (dtDeudores != null)
            {
                string filtro = txtBuscarNombre.Text.Trim();
                DataView dv = dtDeudores.DefaultView;

                // Filtramos por la columna "Cliente" (Asegúrate que se llame así en tu Vista SQL)
                dv.RowFilter = string.Format("Cliente LIKE '%{0}%'", filtro);
                dgvDeudores.DataSource = dv;
            }
        }

        private void dgvDeudores_DoubleClick(object sender, EventArgs e)
        {

        }

        private void dgvDeudores_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // 1. Evitar clics en encabezados y evitar disparos múltiples rápidos
            if (e.RowIndex < 0) return;

            try
            {
                // 2. Obtener el objeto de datos real vinculado a esa fila específica
                // Esto evita que se confunda de deudor si la lista está filtrada
                DataRowView filaSeleccionada = (DataRowView)dgvDeudores.Rows[e.RowIndex].DataBoundItem;

                if (filaSeleccionada != null)
                {
                    // Usamos los nombres de las columnas de tu DataTable/Vista
                    string nombreCliente = filaSeleccionada["Cliente"].ToString().Trim();
                    string estadoDeuda = filaSeleccionada["Estado Deuda"].ToString().Trim();

                    // 3. Validación de estado
                    if (estadoDeuda.Equals("Activo", StringComparison.OrdinalIgnoreCase))
                    {
                        Pago_Deuda pagDe = new Pago_Deuda(nombreCliente);

                        if (pagDe.ShowDialog() == DialogResult.OK)
                        {
                            // Al regresar, refrescamos los datos de la DB
                            CargarGridDeudores();

                            // Limpiamos el buscador para evitar que el índice se pierda
                            txtBuscarNombre.Clear();
                        }
                    }
                    else
                    {
                        // Mensaje informativo si no está activo
                        MessageBox.Show($"El cliente {nombreCliente} no tiene deudas pendientes (Estado: {estadoDeuda}).",
                                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                // Esto nos dirá si el problema es de conversión o de nombres de columna
                Console.WriteLine("Error en Doble Clic: " + ex.Message);
            }
        }

        private void txtBuscarNombre_TextChanged(object sender, EventArgs e)
        {
           
            if (dtDeudores != null)
            {
                string filtro = txtBuscarNombre.Text.Trim();
                DataView dv = dtDeudores.DefaultView;

                
                dv.RowFilter = string.Format("Cliente LIKE '%{0}%'", filtro);

               
                dgvDeudores.DataSource = dv;
            }
        }
    }
}
