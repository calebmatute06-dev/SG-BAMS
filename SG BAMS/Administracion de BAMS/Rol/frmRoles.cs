using SG_BAMS.Administracion_de_BAMS.Rol;
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
    /// Interfaz de usuario para la visualización y gestión de los roles de usuario en el sistema SG-BAMS.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class frmRoles : Form
    {
        /// <summary>
        /// Instancia de la lógica de negocio para las operaciones de roles.
        /// </summary>
        clsRol objetoRol = new clsRol();

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="frmRoles"/>.
        /// </summary>
        public frmRoles()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.Load += new EventHandler(frmRoles_Load);
        }

        /// <summary>
        /// Maneja el evento de carga del formulario para disparar la obtención de datos.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private async void frmRoles_Load(object sender, EventArgs e)
        {
            await CargarGridRoles();
        }

        /// <summary>
        /// Obtiene de forma asíncrona la lista de roles y la vincula al control DataGridView.
        /// </summary>
        private async Task CargarGridRoles()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                dgvRoles.DataSource = await objetoRol.LeerRolesAsync();

                ConfigurarDisenoGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la información: " + ex.Message, "SG-BAMS",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Configura el comportamiento, visibilidad y encabezados de las columnas del grid.
        /// </summary>
        private void ConfigurarDisenoGrid()
        {
            if (dgvRoles.Columns.Contains("id_rol_usuario"))
                dgvRoles.Columns["id_rol_usuario"].Visible = false;

            if (dgvRoles.Columns.Contains("descripcion_rol"))
                dgvRoles.Columns["descripcion_rol"].HeaderText = "Nombre del Rol";

            if (dgvRoles.Columns.Contains("total_usuarios_asignados"))
                dgvRoles.Columns["total_usuarios_asignados"].HeaderText = "Usuarios Activos";

            dgvRoles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRoles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRoles.ReadOnly = true;
            dgvRoles.ClearSelection();
        }

        /// <summary>
        /// Permite abrir el formulario de modificación al hacer doble clic sobre un registro.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="DataGridViewCellEventArgs"/> que contiene los datos del evento.</param>
        private void dgvRoles_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvRoles.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvRoles.CurrentRow.Cells["id_rol_usuario"].Value);
                string nombre = dgvRoles.CurrentRow.Cells["descripcion_rol"].Value.ToString();

                frmModificarRol frmMod = new frmModificarRol(id, nombre);

                if (frmMod.ShowDialog() == DialogResult.OK)
                {
                    _ = CargarGridRoles();
                }
                dgvRoles.ClearSelection();
            }
        }

        /// <summary>
        /// Abre el formulario para registrar un nuevo rol.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btmAgregar_Click(object sender, EventArgs e)
        {
            frmAgregarRol agregarRol = new frmAgregarRol();

            // Si se cierra con OK (éxito), se recarga el grid
            if (agregarRol.ShowDialog() == DialogResult.OK)
            {
                _ = CargarGridRoles();
            }
            dgvRoles.ClearSelection();
        }

        /// <summary>
        /// Valida la selección de un registro y abre la ventana de edición.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btmModificar_Click(object sender, EventArgs e)
        {
            if (dgvRoles.SelectedRows.Count > 0 && dgvRoles.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvRoles.CurrentRow.Cells["id_rol_usuario"].Value);
                string nombre = dgvRoles.CurrentRow.Cells["descripcion_rol"].Value.ToString();

                frmModificarRol frmMod = new frmModificarRol(id, nombre);

                if (frmMod.ShowDialog() == DialogResult.OK)
                {
                    _ = CargarGridRoles();
                }
                dgvRoles.ClearSelection();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un rol de la lista.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Cierra la ventana actual de gestión de roles.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Aplica estilos visuales personalizados al DataGridView para mantener la estética del sistema.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void frmRoles_Load_1(object sender, EventArgs e)
        {
            dgvRoles.BorderStyle = BorderStyle.None;
            dgvRoles.BackgroundColor = Color.White;
            dgvRoles.RowHeadersVisible = false;
            dgvRoles.EnableHeadersVisualStyles = false;
            dgvRoles.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgvRoles.ColumnHeadersDefaultCellStyle.BackColor = Color.SkyBlue;
            dgvRoles.ColumnHeadersDefaultCellStyle.ForeColor = Color.Navy;
            dgvRoles.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvRoles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvRoles.ColumnHeadersHeight = 28;

            dgvRoles.DefaultCellStyle.BackColor = Color.White;
            dgvRoles.DefaultCellStyle.ForeColor = Color.Navy;
            dgvRoles.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvRoles.DefaultCellStyle.Padding = new Padding(3);
            dgvRoles.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 245, 255);
            dgvRoles.AlternatingRowsDefaultCellStyle.ForeColor = Color.Navy;

            dgvRoles.DefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            dgvRoles.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvRoles.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvRoles.GridColor = Color.LightGray;
            dgvRoles.RowTemplate.Height = 32;
            dgvRoles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRoles.ClearSelection();
        }
    }
}