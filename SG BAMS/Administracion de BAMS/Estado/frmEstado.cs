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
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class frmEstado : Form
    {
        /// <summary>
        /// The objeto estado
        /// </summary>
        clsEstado objetoEstado = new clsEstado();
        /// <summary>
        /// Initializes a new instance of the <see cref="frmEstado"/> class.
        /// </summary>
        public frmEstado()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Load += new EventHandler(frmEstados_Load);
            
        }

        /// <summary>
        /// Handles the Load event of the frmEstados control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void frmEstados_Load(object sender, EventArgs e)
        {
            await CargarGridEstados();
        }

        /// <summary>
        /// Cargars the grid estados.
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
        /// Personalizars the grid.
        /// </summary>
        private void PersonalizarGrid()
        {
            if (dgvEstados.Columns.Contains("id_estado"))
                dgvEstados.Columns["id_estado"].Visible = false;

            if (dgvEstados.Columns.Contains("descripcion_estado"))
                dgvEstados.Columns["descripcion_estado"].HeaderText = "Nombre del Estado";

            if (dgvEstados.Columns.Contains("total_usuarios"))
                dgvEstados.Columns["total_usuarios"].HeaderText = "Total de Usuarios";

            if (dgvEstados.Columns.Contains("total_productos"))
                dgvEstados.Columns["total_productos"].HeaderText = "Total de Productos";

            if (dgvEstados.Columns.Contains("total_proveedores"))
                dgvEstados.Columns["total_proveedores"].HeaderText = "Total de Proveedores";


            dgvEstados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEstados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEstados.AllowUserToAddRows = false;
            dgvEstados.ReadOnly = true;
            dgvEstados.ClearSelection();
        }

        /// <summary>
        /// Handles the CellContentDoubleClick event of the dgvEstados control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
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
        /// Handles the Click event of the btnAgregar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAgregarEstado frm = new frmAgregarEstado();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                _ = CargarGridEstados();
            }
        }

        /// <summary>
        /// Handles the Click event of the btnModificar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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
        /// Handles the Click event of the btnSalir control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Load event of the frmEstado control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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
