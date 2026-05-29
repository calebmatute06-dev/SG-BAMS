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
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class Deudores_Emp : Form
    {
        /// <summary>
        /// La tabla de datos de deudores
        /// </summary>
        private DataTable dtDeudores;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Deudores_Emp" />.
        /// </summary>
        public Deudores_Emp()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            
            dgvDeudores.CellDoubleClick += dgvDeudores_CellDoubleClick;

            CargarGridDeudores();

            this.txtBuscarNombre.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
        }

        /// <summary>
        /// Carga el grid de deudores mostrando solo los activos.
        /// </summary>
        public void CargarGridDeudores()
        {
            ClsDeuda objetoDeuda = new ClsDeuda();
            dtDeudores = objetoDeuda.ListarDeudores();

            
            DataView dv = new DataView(dtDeudores);
            dv.RowFilter = "[Estado Deuda] = 'Activo'";
            dgvDeudores.DataSource = dv;

            dgvDeudores.ReadOnly = true;
            dgvDeudores.AllowUserToAddRows = false;
            dgvDeudores.AllowUserToDeleteRows = false;

            dgvDeudores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDeudores.MultiSelect = false;

            dgvDeudores.ClearSelection();
        }

        /// <summary>
        /// Maneja el evento TextChanged del control txtBuscarNombre.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void txtBuscarNombre_TextChanged(object sender, EventArgs e)
        {
            int cursor = txtBuscarNombre.SelectionStart;
            txtBuscarNombre.Text = txtBuscarNombre.Text.ToUpper();
            txtBuscarNombre.SelectionStart = cursor;

            FiltrarDeudores();
        }

        /// <summary>
        /// Filtra los deudores por nombre manteniendo el filtro de estado activo.
        /// </summary>
        private void FiltrarDeudores()
        {
            if (dtDeudores != null)
            {
                string filtroNombre = txtBuscarNombre.Text
                    .Replace("'", "''")
                    .Replace("[", "[[]")
                    .Replace("]", "[]]")
                    .Trim();

                
                string filtroCompleto = "[Estado Deuda] = 'Activo'";
                if (!string.IsNullOrEmpty(filtroNombre))
                {
                    filtroCompleto += string.Format(" AND Cliente LIKE '%{0}%'", filtroNombre);
                }

                DataView dv = dtDeudores.DefaultView;
                dv.RowFilter = filtroCompleto;
                dgvDeudores.DataSource = dv;
            }
        }

        /// <summary>
        /// Procesa el pago de una deuda a partir de la fila seleccionada.
        /// </summary>
        /// <param name="fila">La fila del deudor seleccionado.</param>
        private void ProcesarPagoDeuda(DataRowView fila)
        {
            if (fila == null) return;

            int idDeuda = Convert.ToInt32(fila["ID Deuda"]);
            string nombreCliente = fila["Cliente"].ToString().Trim();
            string estadoDeuda = fila["Estado Deuda"].ToString().Trim();

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

        /// <summary>
        /// Maneja el evento Click del control kryptonButton15.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void kryptonButton15_Click(object sender, EventArgs e)
        {
            if (dgvDeudores.CurrentRow == null || dgvDeudores.CurrentRow.Index < 0)
            {
                MessageBox.Show("Por favor, seleccione una fila para pagar la deuda.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                DataRowView filaSeleccionada = dgvDeudores.CurrentRow.DataBoundItem as DataRowView;
                if (filaSeleccionada != null)
                {
                    ProcesarPagoDeuda(filaSeleccionada);
                }
                else
                {
                    MessageBox.Show("No se pudo obtener la información de la fila seleccionada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al procesar el pago: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Maneja el evento CellDoubleClick del control dgvDeudores.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="DataGridViewCellEventArgs" /> que contiene los datos del evento.</param>
        private void dgvDeudores_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                DataRowView filaSeleccionada = null;

                if (dgvDeudores.DataSource is DataView dv)
                {
                    if (e.RowIndex < dv.Count)
                        filaSeleccionada = dv[e.RowIndex];
                }
                else if (dgvDeudores.DataSource is DataTable dt)
                {
                    if (e.RowIndex < dt.Rows.Count)
                        filaSeleccionada = dt.DefaultView[e.RowIndex];
                }
                else if (dgvDeudores.Rows[e.RowIndex].DataBoundItem is DataRowView drv)
                {
                    filaSeleccionada = drv;
                }

                if (filaSeleccionada != null)
                {
                    ProcesarPagoDeuda(filaSeleccionada);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al procesar el pago: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Maneja el evento Shown del control Deudores.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void Deudores_Shown(object sender, EventArgs e)
        {
            Ayudante_UI.AplicarZoomGlobal(this);
        }

        /// <summary>
        /// Maneja el evento KeyPress del control txtBuscarNombre.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="KeyPressEventArgs" /> que contiene los datos del evento.</param>
        private void txtBuscarNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.PermitirSoloLetras(e);
        }

        /// <summary>
        /// Botón de notificaciones.
        /// </summary>
        /// <param name="sender">El remitente.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void btnNoti(object sender, EventArgs e)
        {
            new NotificacionesAdmin().Show();
        }

        /// <summary>
        /// Maneja el evento Tick del control timer1.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void timer1_Tick(object sender, EventArgs e) { }

        /// <summary>
        /// Maneja el evento Load del control Deudores_Emp.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void Deudores_Emp_Load(object sender, EventArgs e)
        {
            btnDeudores.Enabled = false;
            btnDeudores.BackColor = Color.SkyBlue;
            btnDeudores.ForeColor = Color.White;

            dgvDeudores.ClearSelection();
            dgvDeudores.BorderStyle = BorderStyle.None;
            dgvDeudores.BackgroundColor = Color.White;
            dgvDeudores.RowHeadersVisible = false;
            dgvDeudores.EnableHeadersVisualStyles = false;
            dgvDeudores.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgvDeudores.ColumnHeadersDefaultCellStyle.BackColor = Color.SkyBlue;
            dgvDeudores.ColumnHeadersDefaultCellStyle.ForeColor = Color.Navy;
            dgvDeudores.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvDeudores.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvDeudores.ColumnHeadersHeight = 28;

            dgvDeudores.DefaultCellStyle.BackColor = Color.White;
            dgvDeudores.DefaultCellStyle.ForeColor = Color.Navy;
            dgvDeudores.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvDeudores.DefaultCellStyle.Padding = new Padding(3);
            dgvDeudores.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 245, 255);
            dgvDeudores.AlternatingRowsDefaultCellStyle.ForeColor = Color.Navy;

            dgvDeudores.DefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            dgvDeudores.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvDeudores.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvDeudores.GridColor = Color.LightGray;
            dgvDeudores.RowTemplate.Height = 32;
            dgvDeudores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDeudores.ClearSelection();
            ClsMensajeGuia.Activar(txtBuscarNombre);
          
        }

        /// <summary>
        /// Maneja el evento Click del control btnMenu.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnMenu_Click(object sender, EventArgs e)
        {
            MenuPrincipalEmp ME = new MenuPrincipalEmp();
            ME.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del control btnFacturas.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnFacturas_Click(object sender, EventArgs e)
        {
            FacturasEmp FE = new FacturasEmp();
            FE.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del control btnClientes.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnClientes_Click(object sender, EventArgs e)
        {
            ClientesEmp CE = new ClientesEmp();
            CE.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del control btnInventario.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnInventario_Click(object sender, EventArgs e)
        {
            InventarioEmp IE = new InventarioEmp();
            IE.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del control btnCerrar.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Login.Login login = new Login.Login();
            login.Show();
            this.Close();
        }

        /// <summary>
        /// Maneja el evento Click del control btnPerfil.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnPerfil_Click(object sender, EventArgs e)
        {
            Perfil perfil = new Perfil();
            perfil.Show();
        }
    }
}