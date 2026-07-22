using SG_BAMS.Administracion_de_BAMS;
using SG_BAMS.Administracion_de_BAMS.TipoProd;
using System;
using System.Windows.Forms;

namespace SG_BAMS
{
    // ============================================================
    // NOTA DE CORRECCIÓN ADICIONAL:
    // El constructor original llamaba a CargarGridTipos() sin await
    // (fire-and-forget) Y el evento Load (frmTipoProducto_Load_1)
    // también la llamaba, cargando el grid dos veces en cada apertura.
    // Se dejó una sola carga, en el manejador de Load conectado por
    // el Diseñador.
    // ============================================================

    /// <summary>
    /// Interfaz de usuario para la visualización y gestión de las categorías de productos (Tipos de Producto).
    /// DIP: recibe ICatalogoRepository inyectado, no instancia clsTipoProducto directamente.
    /// </summary>
    public partial class frmTipoProducto : Form
    {
        private readonly ICatalogoRepository _repositorio;

        public frmTipoProducto(ICatalogoRepository repositorio)
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
        public frmTipoProducto() : this(new clsTipoProducto()) { }

        private async System.Threading.Tasks.Task CargarGridTipos()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                dgvTipoProducto.DataSource = await _repositorio.LeerAsync();

                if (dgvTipoProducto.Columns.Contains("nombre_tipo_producto"))
                    dgvTipoProducto.Columns["nombre_tipo_producto"].HeaderText = "Tipo de Producto";

                if (dgvTipoProducto.Columns.Contains("id_tipo_producto"))
                    dgvTipoProducto.Columns["id_tipo_producto"].HeaderText = "ID";

                if (dgvTipoProducto.Columns.Contains("cantidad_productos_asociados"))
                    dgvTipoProducto.Columns["cantidad_productos_asociados"].HeaderText = "Cantidad";

                dgvTipoProducto.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvTipoProducto.AllowUserToAddRows = false;
                dgvTipoProducto.ReadOnly = true;
                dgvTipoProducto.ClearSelection();

                EstiloDataGridView.Aplicar(dgvTipoProducto);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private async void frmTipoProducto_Load_1(object sender, EventArgs e)
        {
            await CargarGridTipos();
        }

        private void dgvTipoProducto_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvTipoProducto.CurrentRow != null && dgvTipoProducto.SelectedRows.Count > 0)
                AbrirModificar();
            else
                MessageBox.Show("Por favor, seleccione una fila completa de la lista.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            dgvTipoProducto.ClearSelection();
        }

        private void btmAgregar_Click(object sender, EventArgs e)
        {
            using (var frm = new frnAgregarTipoProducto(_repositorio))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    _ = CargarGridTipos();
            }
            dgvTipoProducto.ClearSelection();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvTipoProducto.CurrentRow != null && dgvTipoProducto.SelectedRows.Count > 0)
                AbrirModificar();
            else
                MessageBox.Show("Por favor, seleccione una fila completa de la lista.", "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AbrirModificar()
        {
            int id = Convert.ToInt32(dgvTipoProducto.CurrentRow.Cells["id_tipo_producto"].Value);
            string descripcion = dgvTipoProducto.CurrentRow.Cells["nombre_tipo_producto"].Value.ToString();

            using (var frm = new frmModificarTipoProducto(id, descripcion, _repositorio))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    _ = CargarGridTipos();
            }
            dgvTipoProducto.ClearSelection();
        }
    }
}
