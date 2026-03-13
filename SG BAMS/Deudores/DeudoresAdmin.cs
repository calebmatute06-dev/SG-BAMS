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
        // Variable global para manejar el filtrado
        private DataTable dtDeudores;

        public DeudoresAdmin()
        {
            InitializeComponent();
            CargarGridDeudores();

            this.txtBuscarNombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBuscarNombre_KeyPress);
        }

        public void CargarGridDeudores()
        {
            ClsDeuda objetoDeuda = new ClsDeuda();
            dtDeudores = objetoDeuda.ListarDeudores();
            dgvDeudores.DataSource = dtDeudores;
        }

        // --- NAVEGACIÓN DEL MENÚ (Respetando tus nombres exactos) ---

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

        private void kryptonButton11_Click(object sender, EventArgs e)
        {
            Ajustes Ajus = new Ajustes();
            Ajus.Show();
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
        private void dgvDeudores_DoubleClick(object sender, EventArgs e) { }

        private void txtBuscarNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Solo permite letras, espacios y teclas de control (como Borrar)
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                // "Handled = true" cancela el evento (no escribe el carácter en el cuadro)
                e.Handled = true;

                // Opcional: Avisar al usuario por qué no se escribió el número
                // MessageBox.Show("Solo se permiten letras para el nombre del deudor.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            Perfil Per = new Perfil();
            Per.Show();
        }
    }
}