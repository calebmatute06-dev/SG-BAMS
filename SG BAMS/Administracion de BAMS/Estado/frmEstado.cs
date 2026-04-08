using SG_BAMS.Administracion_de_BAMS.Estado;
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
    /// Representa la interfaz de usuario para la visualización y gestión de estados.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class frmEstado : Form
    {
        /// <summary>
        /// Instancia de la clase lógica de negocio para los estados.
        /// </summary>
        clsEstado objetoEstado = new clsEstado();

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="frmEstado"/>.
        /// </summary>
        public frmEstado()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Load += new EventHandler(frmEstados_Load);

        }

        /// <summary>
        /// Maneja el evento de carga del formulario de forma asíncrona.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private async void frmEstados_Load(object sender, EventArgs e)
        {
            await CargarGridEstados();
        }

        /// <summary>
        /// Carga los datos de los estados en el control DataGridView de forma asíncrona.
        /// </summary>
        private async Task CargarGridEstados()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                DataTable dt = await objetoEstado.LeerEstadosAsync();

                dgvEstados.DataSource = dt;

                PersonalizarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al cargar: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Aplica configuraciones visuales y de formato al DataGridView.
        /// </summary>
        private void PersonalizarGrid()
        {
            if (dgvEstados.Columns.Contains("id_estado"))
                dgvEstados.Columns["id_estado"].Visible = false;

            if (dgvEstados.Columns.Contains("descripcion_estado"))
                dgvEstados.Columns["descripcion_estado"].HeaderText = "Estado";

            if (dgvEstados.Columns.Contains("total_usuarios"))
                dgvEstados.Columns["total_usuarios"].HeaderText = "Total Usuarios";

            if (dgvEstados.Columns.Contains("total_productos"))
                dgvEstados.Columns["total_productos"].HeaderText = "Total Productos";

            if (dgvEstados.Columns.Contains("total_proveedores"))
                dgvEstados.Columns["total_proveedores"].HeaderText = "Total Proveedores";


            dgvEstados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEstados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEstados.AllowUserToAddRows = false;
            dgvEstados.ReadOnly = true;
            dgvEstados.ClearSelection();
        }

        /// <summary>
        /// Maneja el evento de doble clic en el contenido de una celda para modificar el registro.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="DataGridViewCellEventArgs"/> que contiene los datos del evento.</param>
        private void dgvEstados_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvEstados.SelectedRows.Count > 0)
            {

                int id = Convert.ToInt32(dgvEstados.CurrentRow.Cells["id_estado"].Value);
                string descripcion = dgvEstados.CurrentRow.Cells["descripcion_estado"].Value.ToString();

                frmModificarEstado frmMod = new frmModificarEstado(id, descripcion);

                if (frmMod.ShowDialog() == DialogResult.OK)
                {
                    _ = CargarGridEstados();
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un estado de la lista para modificar.");
            }
        }

        /// <summary>
        /// Maneja el evento Click del botón agregar.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAgregarEstado frm = new frmAgregarEstado();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                _ = CargarGridEstados();
            }
        }

        /// <summary>
        /// Maneja el evento Click del botón modificar para el registro seleccionado.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvEstados.SelectedRows.Count > 0)
            {

                int id = Convert.ToInt32(dgvEstados.CurrentRow.Cells["id_estado"].Value);
                string descripcion = dgvEstados.CurrentRow.Cells["descripcion_estado"].Value.ToString();

                frmModificarEstado frmMod = new frmModificarEstado(id, descripcion);

                if (frmMod.ShowDialog() == DialogResult.OK)
                {
                    _ = CargarGridEstados();
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un estado de la lista para modificar.");
            }
        }

        /// <summary>
        /// Maneja el evento Click del botón salir para cerrar el formulario.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Configura el estilo visual avanzado del control DataGridView al cargar el formulario.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void frmEstado_Load(object sender, EventArgs e)
        {
            dgvEstados.BorderStyle = BorderStyle.None;
            dgvEstados.BackgroundColor = Color.White;
            dgvEstados.RowHeadersVisible = false;
            dgvEstados.EnableHeadersVisualStyles = false;
            dgvEstados.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgvEstados.ColumnHeadersDefaultCellStyle.BackColor = Color.SkyBlue;
            dgvEstados.ColumnHeadersDefaultCellStyle.ForeColor = Color.Navy;
            dgvEstados.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvEstados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvEstados.ColumnHeadersHeight = 28;

            dgvEstados.DefaultCellStyle.BackColor = Color.White;
            dgvEstados.DefaultCellStyle.ForeColor = Color.Navy;
            dgvEstados.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvEstados.DefaultCellStyle.Padding = new Padding(3);
            dgvEstados.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 245, 255);
            dgvEstados.AlternatingRowsDefaultCellStyle.ForeColor = Color.Navy;

            dgvEstados.DefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            dgvEstados.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvEstados.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvEstados.GridColor = Color.LightGray;
            dgvEstados.RowTemplate.Height = 32;
            dgvEstados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEstados.ClearSelection();
        }
    }
}