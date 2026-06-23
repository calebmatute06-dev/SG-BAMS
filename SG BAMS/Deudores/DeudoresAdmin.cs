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
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class DeudoresAdmin : Form
    {
        /// <summary>
        /// La tabla de datos de deudores
        /// </summary>
        private DataTable dtDeudores;

        /// <summary>
        /// Bandera para evitar el evento recursivo
        /// </summary>
        private bool isFiltering = false;

        /// <summary>
        /// Texto del placeholder para el campo de búsqueda
        /// </summary>
        private string placeholderTexto = "Buscar por nombre del cliente...";

        /// <summary>
        /// Color del texto placeholder
        /// </summary>
        private Color placeholderColor = Color.Gray;

        /// <summary>
        /// Color del texto normal
        /// </summary>
        private Color textoColor = Color.Black;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="DeudoresAdmin" />.
        /// </summary>
        public DeudoresAdmin()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            CargarGridDeudores();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            dgvDeudores.CellDoubleClick += dgvDeudores_CellDoubleClick;

            txtBuscarNombre.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
            txtBuscarNombre.TextChanged += txtBuscarNombre_TextChanged;

            
            txtBuscarNombre.Enter += txtBuscarNombre_Enter;
            txtBuscarNombre.Leave += txtBuscarNombre_Leave;
        }

        /// <summary>
        /// Configura el placeholder en un TextBox estándar.
        /// </summary>
        /// <param name="textBox">El TextBox a configurar.</param>
        /// <param name="placeholder">El texto del placeholder.</param>
        private void ConfigurarPlaceholder(TextBox textBox, string placeholder)
        {
            placeholderTexto = placeholder;
            textoColor = textBox.ForeColor;
            placeholderColor = Color.Gray;

            textBox.Text = placeholderTexto;
            textBox.ForeColor = placeholderColor;
        }

        /// <summary>
        /// Maneja el evento Enter del TextBox de búsqueda.
        /// </summary>
        private void txtBuscarNombre_Enter(object sender, EventArgs e)
        {
            if (txtBuscarNombre.Text == placeholderTexto)
            {
                txtBuscarNombre.Text = "";
                txtBuscarNombre.ForeColor = textoColor;
            }
        }

        /// <summary>
        /// Maneja el evento Leave del TextBox de búsqueda.
        /// </summary>
        private void txtBuscarNombre_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscarNombre.Text))
            {
                txtBuscarNombre.Text = placeholderTexto;
                txtBuscarNombre.ForeColor = placeholderColor;
            }
        }

        /// <summary>
        /// Carga el grid de deudores mostrando solo los activos.
        /// </summary>
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

            // Aplicar filtro inicial
            FiltrarDeudores();
        }

        /// <summary>
        /// Filtra los deudores por estado activo y por nombre si hay texto de búsqueda.
        /// Ambos filtros se aplican simultáneamente.
        /// </summary>
        private void FiltrarDeudores()
        {
            if (dtDeudores == null) return;

            try
            {
                DataView dv = dtDeudores.DefaultView;
                string filtroNombre = txtBuscarNombre.Text?.Trim() ?? "";

               
                if (filtroNombre == placeholderTexto || txtBuscarNombre.ForeColor == placeholderColor)
                {
                    filtroNombre = "";
                }

                var condiciones = new List<string>();

                
                condiciones.Add("[Estado Deuda] = 'Activo'");

                
                if (!string.IsNullOrWhiteSpace(filtroNombre))
                {
                    
                    string nombreBuscar = filtroNombre
                        .Replace("'", "''")
                        .Replace("[", "[[]")
                        .Replace("]", "[]]");

                    condiciones.Add($"Cliente LIKE '%{nombreBuscar}%'");
                }

                
                string rowFilter = string.Join(" AND ", condiciones);

                dv.RowFilter = rowFilter;
                dgvDeudores.DataSource = dv;
                dgvDeudores.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al filtrar: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Maneja el evento TextChanged del control txtBuscarNombre.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void txtBuscarNombre_TextChanged(object sender, EventArgs e)
        {
            
            if (isFiltering) return;

            isFiltering = true;

            FiltrarDeudores();

            isFiltering = false;
        }

        /// <summary>
        /// Maneja el evento Click del control kryptonButton12.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void kryptonButton12_Click(object sender, EventArgs e)
        {
            FiltrarDeudores();
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
        /// Maneja el evento CellDoubleClick del control dgvDeudores.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="DataGridViewCellEventArgs" /> que contiene los datos del evento.</param>
        private void dgvDeudores_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                DataRowView filaSeleccionada = (DataRowView)dgvDeudores.Rows[e.RowIndex].DataBoundItem;
                ProcesarPagoDeuda(filaSeleccionada);
                dgvDeudores.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar procesar el pago: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Maneja el evento Click del control kryptonButton15.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void kryptonButton15_Click(object sender, EventArgs e)
        {
            if (dgvDeudores.SelectedRows.Count == 0 || dgvDeudores.CurrentRow == null)
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
                    dgvDeudores.ClearSelection();
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
        /// Maneja el evento Click del control button12.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void button12_Click(object sender, EventArgs e)
        {
            new NotificacionesAdmin().Show();
        }

        /// <summary>
        /// Maneja el evento CellContentClick del control kryptonDataGridView1.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="DataGridViewCellEventArgs" /> que contiene los datos del evento.</param>
        private void kryptonDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        /// <summary>
        /// Maneja el evento DoubleClick del control dgvDeudores.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void dgvDeudores_DoubleClick(object sender, EventArgs e) { }

        /// <summary>
        /// Maneja el evento Load del control DeudoresAdmin.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void DeudoresAdmin_Load(object sender, EventArgs e)
        {
            
            ConfigurarPlaceholder(txtBuscarNombre, "Buscar por nombre del cliente...");

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
            MenuPrincipalAdm MPA = new MenuPrincipalAdm();
            MPA.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del control btnFacturas.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnFacturas_Click(object sender, EventArgs e)
        {
            FacturasAdm FA = new FacturasAdm();
            FA.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del control btnCompra.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnCompra_Click(object sender, EventArgs e)
        {
            Compras CF = new Compras();
            CF.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del control btnClientes.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnClientes_Click(object sender, EventArgs e)
        {
            ClientesAdm CA = new ClientesAdm();
            CA.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del control btnInventario.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnInventario_Click(object sender, EventArgs e)
        {
            InventarioAdmin IA = new InventarioAdmin();
            IA.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del control btnProveedores.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnProveedores_Click(object sender, EventArgs e)
        {
            ProveedoresAdmin PA = new ProveedoresAdmin();
            PA.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del control btnReportes.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnReportes_Click(object sender, EventArgs e)
        {
            ReportesAdmin RA = new ReportesAdmin();
            RA.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del control btnBitacora.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnBitacora_Click(object sender, EventArgs e)
        {
            BitacoraAdmin BA = new BitacoraAdmin();
            BA.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del control btnCerrar.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
           "¿Está seguro que desea cerrar sesión?",
           "Confirmación",
           MessageBoxButtons.YesNo,
           MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                Login.Login login = new Login.Login();
                login.Show();
                this.Close();
            }
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