using SG_BAMS.Administracion_de_BAMS;
using SG_BAMS.Administracion_de_BAMS.ModeloAuto;
using System;
using System.Windows.Forms;

namespace SG_BAMS
{
    // ============================================================
    // NOTA DE CORRECCIÓN ADICIONAL:
    // El archivo original tenía DOS manejadores de Load distintos:
    // uno agregado a mano en el constructor (frmModelosAuto_Load,
    // que solo cargaba el grid) y otro conectado por el Diseñador
    // (frmModeloAuto_Load, que solo aplicaba estilos). Esto ejecutaba
    // ambos en cada apertura del formulario de forma redundante y
    // desordenada. Se consolidó en un solo manejador conectado por
    // el Diseñador, que ahora carga el grid Y aplica el estilo
    // (a través de EstiloDataGridView.Aplicar dentro de CargarGridModelos).
    // ============================================================

    /// <summary>
    /// Interfaz de usuario para la visualización y gestión de los modelos de automóviles en el sistema.
    /// DIP: recibe ICatalogoRepository inyectado, no instancia clsModeloAuto directamente.
    /// </summary>
    public partial class frmModeloAuto : Form
    {
        private readonly ICatalogoRepository _repositorio;

        public frmModeloAuto(ICatalogoRepository repositorio)
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
        public frmModeloAuto() : this(new clsModeloAuto()) { }

        private async System.Threading.Tasks.Task CargarGridModelos()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                dgvModelos.DataSource = await _repositorio.LeerAsync();

                if (dgvModelos.Columns.Contains("id_modelo_auto"))
                    dgvModelos.Columns["id_modelo_auto"].Visible = false;

                if (dgvModelos.Columns.Contains("nombre_modelo_auto"))
                    dgvModelos.Columns["nombre_modelo_auto"].HeaderText = "Modelo de Vehículo";

                if (dgvModelos.Columns.Contains("productos_compatibles"))
                    dgvModelos.Columns["productos_compatibles"].HeaderText = "Cant. Productos Relacionados";

                dgvModelos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvModelos.AllowUserToAddRows = false;
                dgvModelos.ReadOnly = true;
                dgvModelos.ClearSelection();

                EstiloDataGridView.Aplicar(dgvModelos);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar modelos: {ex.Message}", "SG-BAMS",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private async void frmModeloAuto_Load(object sender, EventArgs e)
        {
            await CargarGridModelos();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            using (var frm = new frmAgregarModeloAuto(_repositorio))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    _ = CargarGridModelos();
            }
            dgvModelos.ClearSelection();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvModelos.CurrentRow != null && dgvModelos.SelectedRows.Count > 0)
                AbrirModificar();
            else
                MessageBox.Show("Por favor, seleccione un modelo de la lista.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void dgvModelos_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvModelos.CurrentRow != null && dgvModelos.SelectedRows.Count > 0)
                AbrirModificar();
            else
                MessageBox.Show("Por favor, seleccione una fila completa de la lista.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);

            dgvModelos.ClearSelection();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AbrirModificar()
        {
            int id = Convert.ToInt32(dgvModelos.CurrentRow.Cells["id_modelo_auto"].Value);
            string nombre = dgvModelos.CurrentRow.Cells["nombre_modelo_auto"].Value.ToString();

            using (var frm = new frmModificarModelos(id, nombre, _repositorio))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    _ = CargarGridModelos();
            }
            dgvModelos.ClearSelection();
        }
    }
}
