using SG_BAMS.Administracion_de_BAMS;
using SG_BAMS.Administracion_de_BAMS.Rol;
using System;
using System.Windows.Forms;

namespace SG_BAMS
{
    // ============================================================
    // NOTA DE CORRECCIÓN ADICIONAL:
    // Igual que en frmModeloAuto, existían dos manejadores de Load
    // (uno agregado a mano en el constructor para cargar datos, y
    // otro conectado por el Diseñador para aplicar estilos). Se
    // consolidó todo en el manejador conectado por el Diseñador
    // (frmRoles_Load_1), usando EstiloDataGridView.Aplicar().
    // ============================================================

    /// <summary>
    /// Interfaz de usuario para la visualización y gestión de los roles de usuario en el sistema SG-BAMS.
    /// DIP: recibe ICatalogoRepository inyectado, no instancia clsRol directamente.
    /// </summary>
    public partial class frmRoles : Form
    {
        private readonly ICatalogoRepository _repositorio;

        public frmRoles(ICatalogoRepository repositorio)
        {
            InitializeComponent();
            _repositorio = repositorio;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        /// <summary>
        /// Constructor sin parámetros para compatibilidad con el diseñador de WinForms.
        /// </summary>
        public frmRoles() : this(new clsRol()) { }

        private async System.Threading.Tasks.Task CargarGridRoles()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                dgvRoles.DataSource = await _repositorio.LeerAsync();

                if (dgvRoles.Columns.Contains("id_rol_usuario"))
                    dgvRoles.Columns["id_rol_usuario"].Visible = false;

                if (dgvRoles.Columns.Contains("descripcion_rol"))
                    dgvRoles.Columns["descripcion_rol"].HeaderText = "Nombre del Rol";

                if (dgvRoles.Columns.Contains("total_usuarios_asignados"))
                    dgvRoles.Columns["total_usuarios_asignados"].HeaderText = "Usuarios Activos";

                dgvRoles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvRoles.ReadOnly = true;
                dgvRoles.ClearSelection();

                EstiloDataGridView.Aplicar(dgvRoles);
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

        private async void frmRoles_Load_1(object sender, EventArgs e)
        {
            await CargarGridRoles();
        }

        private void dgvRoles_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvRoles.CurrentRow != null && dgvRoles.SelectedRows.Count > 0)
                AbrirModificar();
            else
                MessageBox.Show("Por favor, seleccione una fila completa de la lista.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            dgvRoles.ClearSelection();
        }

        private void btmAgregar_Click(object sender, EventArgs e)
        {
            using (var frm = new frmAgregarRol(_repositorio))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    _ = CargarGridRoles();
            }
            dgvRoles.ClearSelection();
        }

        private void btmModificar_Click(object sender, EventArgs e)
        {
            if (dgvRoles.SelectedRows.Count > 0 && dgvRoles.CurrentRow != null)
                AbrirModificar();
            else
                MessageBox.Show("Por favor, seleccione un rol de la lista.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AbrirModificar()
        {
            int id = Convert.ToInt32(dgvRoles.CurrentRow.Cells["id_rol_usuario"].Value);
            string nombre = dgvRoles.CurrentRow.Cells["descripcion_rol"].Value.ToString();

            using (var frm = new frmModificarRol(id, nombre, _repositorio))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    _ = CargarGridRoles();
            }
            dgvRoles.ClearSelection();
        }
    }
}
