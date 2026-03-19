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
        // Variable global para manejar el filtrado
        private DataTable dtDeudores;

        public Deudores_Emp()
        {
            InitializeComponent();
            CargarGridDeudores();

            this.txtBuscarNombre.KeyPress += (s, e) => ClsValidaciones.ValidarBusquedaAlfanumerica(e);
        }

        public void CargarGridDeudores()
        {
            ClsDeuda objetoDeuda = new ClsDeuda();
            dtDeudores = objetoDeuda.ListarDeudores();
            dgvDeudores.DataSource = dtDeudores;
        }

        // --- LÓGICA DE BÚSQUEDA Y PAGOS ---

        // El botón de la lupa / buscar
        private void txtBuscarNombre_TextChanged(object sender, EventArgs e)
        {
            // Opcional: Convertir a Mayúsculas mientras escribe para estética
            int cursor = txtBuscarNombre.SelectionStart;
            txtBuscarNombre.Text = txtBuscarNombre.Text.ToUpper();
            txtBuscarNombre.SelectionStart = cursor;

            FiltrarDeudores();
        }

        private void FiltrarDeudores()
        {
            if (dtDeudores != null)
            {
                string filtro = txtBuscarNombre.Text.Replace("'", "''").Trim(); // Evita errores con comillas simples

                // Aplicamos el filtro directamente a la vista por defecto
                dtDeudores.DefaultView.RowFilter = $"Cliente LIKE '%{filtro}%'";

                // No es estrictamente necesario reasignar el DataSource si ya estaba vinculado,
                // pero esto asegura que el grid se entere del cambio:
                dgvDeudores.DataSource = dtDeudores.DefaultView;
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
        private void timer1_Tick(object sender, EventArgs e) { }
        private void dgvDeudores_DoubleClick(object sender, EventArgs e) { }

        private void txtBuscarNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.PermitirSoloLetras(e);
        }

        private void btnNoti(object sender, EventArgs e)
        {
            NotificacionesEmp Noti = new NotificacionesEmp();
            Noti.Show();
        }

        private void btnMenuEmp_Click(object sender, EventArgs e)
        {
            MenuPrincipalEmp Menad = new MenuPrincipalEmp();
            Menad.Show();
            this.Hide();
        }

        private void btnFacturasEmp_Click(object sender, EventArgs e)
        {
            FacturasEmp factad = new FacturasEmp();
            factad.Show();
            this.Hide();
        }

        private void btnClientesEmp_Click(object sender, EventArgs e)
        {
            ClientesEmp clientesEmp = new ClientesEmp();
            clientesEmp.Show();
            this.Hide();
        }

        private void btnInventarioEmp_Click(object sender, EventArgs e)
        {
            InventarioEmp inventarioEmp = new InventarioEmp();
            inventarioEmp.Show();
            this.Hide();
        }

        private void btnPerfil_Click(object sender, EventArgs e)
        {
            Perfil Per = new Perfil();
            Per.Show();
        }
        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            SG_BAMS.Login.Login log = new SG_BAMS.Login.Login();
            log.Show();
            this.Hide();
        }

    }
}