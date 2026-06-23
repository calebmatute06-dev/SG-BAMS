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
        /// Inicializa una nueva instancia de la clase <see cref="Deudores_Emp" />.
        /// </summary>
        public Deudores_Emp()
        {
            InitializeComponent();
            CargarGridDeudores();
            this.StartPosition = FormStartPosition.CenterScreen;

            dgvDeudores.CellDoubleClick += dgvDeudores_CellDoubleClick;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        

            this.txtBuscarNombre.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
            this.txtBuscarNombre.TextChanged += txtBuscarNombre_TextChanged;

            // Configurar eventos para el placeholder
            this.txtBuscarNombre.Enter += txtBuscarNombre_Enter;
            this.txtBuscarNombre.Leave += txtBuscarNombre_Leave;

            dgvDeudores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;


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

            
            dgvDeudores.MultiSelect = false;


            FiltrarDeudores();
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

                // Si el texto es el placeholder, tratarlo como vacío
                if (filtroNombre == placeholderTexto || txtBuscarNombre.ForeColor == placeholderColor)
                {
                    filtroNombre = "";
                }

                var condiciones = new List<string>();

                // Siempre filtrar por estado activo
                condiciones.Add("[Estado Deuda] = 'Activo'");

                // Agregar filtro de nombre si hay texto real
                if (!string.IsNullOrWhiteSpace(filtroNombre))
                {
                    // Escapar caracteres especiales para el filtro
                    string nombreBuscar = filtroNombre
                        .Replace("'", "''")
                        .Replace("[", "[[]")
                        .Replace("]", "[]]");

                    condiciones.Add($"Cliente LIKE '%{nombreBuscar}%'");
                }

                // Combinar todas las condiciones con AND
                string rowFilter = string.Join(" AND ", condiciones);

                dv.RowFilter = rowFilter;
                dgvDeudores.DataSource = dv;
                dgvDeudores.ClearSelection();
                dgvDeudores.CurrentCell = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al filtrar: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (dgvDeudores.SelectedRows.Count == 0 || dgvDeudores.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccione una fila.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    dgvDeudores.ClearSelection();
                }

                dgvDeudores.ClearSelection();
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
            CargarGridDeudores();
            dgvDeudores.ClearSelection();
            dgvDeudores.CurrentCell = null;
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
            dgvDeudores.ClearSelection();
            
            // Configurar el placeholder para el TextBox estándar
            ConfigurarPlaceholder(txtBuscarNombre, "Buscar por nombre del cliente...");

            btnDeudores.Enabled = false;
            btnDeudores.BackColor = Color.SkyBlue;
            btnDeudores.ForeColor = Color.White;

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