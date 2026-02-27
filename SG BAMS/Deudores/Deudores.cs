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
        // Variable global para manejar el filtrado
        private DataTable dtDeudores;

        public Deudores()
        {
            InitializeComponent();
            CargarGridDeudores();
        }

        public void CargarGridDeudores()
        {
            ClsDeuda objetoDeuda = new ClsDeuda();
            dtDeudores = objetoDeuda.ListarDeudores();
            dgvDeudores.DataSource = dtDeudores;
        }

        // --- NAVEGACIÓN DEL MENÚ (Respetando tus nombres exactos) ---

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

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            this.Show();
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

        // BOTÓN PAGAR (kryptonButton15): Aquí pasamos los dos argumentos
        private void kryptonButton15_Click(object sender, EventArgs e)
        {
            // Usamos "" y 0 para indicar que no hay selección previa desde el grid
            Pago_Deuda PagDe = new Pago_Deuda("", 0);
            PagDe.ShowDialog();
            CargarGridDeudores();
        }

        // DOBLE CLIC EN EL GRID: Aquí es donde forzamos la exactitud por ID
        private void dgvDeudores_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Evitar clics en el encabezado
            if (e.RowIndex < 0) return;

            try
            {
                // Obtenemos la fila vinculada
                DataRowView filaSeleccionada = (DataRowView)dgvDeudores.Rows[e.RowIndex].DataBoundItem;

                if (filaSeleccionada != null)
                {
                    // --- CAMBIO IMPORTANTE AQUÍ ---
                    // Si te da error, verifica si es "ID Deuda", "ID_Deuda" o "id"
                    int idDeuda = Convert.ToInt32(filaSeleccionada["ID Deuda"]);

                    string nombreCliente = filaSeleccionada["Cliente"].ToString().Trim();
                    string estadoDeuda = filaSeleccionada["Estado Deuda"].ToString().Trim();

                    if (estadoDeuda.Equals("Activo", StringComparison.OrdinalIgnoreCase))
                    {
                        // Ahora pasamos los dos argumentos correctamente
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
                // Este mensaje te dirá exactamente cómo se llaman tus columnas si fallas de nuevo
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

        // Eventos vacíos para evitar errores de referencia si existen en el designer
        private void kryptonDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void timer1_Tick(object sender, EventArgs e) { }
        private void dgvDeudores_DoubleClick(object sender, EventArgs e) { }
    }
}