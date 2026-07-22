using SG_BAMS.Administracion_de_BAMS;
using SG_BAMS.Administracion_de_BAMS.MarcaProd;
using System;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Interfaz de usuario para la gestión y visualización del catálogo de marcas de productos.
    /// DIP: recibe ICatalogoRepository inyectado, no instancia clsMarca directamente.
    /// </summary>
    public partial class frmMarcaProductos : Form
    {
        private readonly ICatalogoRepository _repositorio;

        public frmMarcaProductos(ICatalogoRepository repositorio)
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
        public frmMarcaProductos() : this(new clsMarca()) { }

        private async System.Threading.Tasks.Task CargarGridMarcas()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                dgvMarcas.DataSource = await _repositorio.LeerAsync();

                if (dgvMarcas.Columns.Contains("id_marca_producto"))
                    dgvMarcas.Columns["id_marca_producto"].Visible = false;

                if (dgvMarcas.Columns.Contains("nombre_marca"))
                    dgvMarcas.Columns["nombre_marca"].HeaderText = "Marca";

                if (dgvMarcas.Columns.Contains("cantidad_productos"))
                    dgvMarcas.Columns["cantidad_productos"].HeaderText = "Productos Asociados";

                dgvMarcas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvMarcas.AllowUserToAddRows = false;
                dgvMarcas.ReadOnly = true;
                dgvMarcas.ClearSelection();

                EstiloDataGridView.Aplicar(dgvMarcas);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar: {ex.Message}", "Sistema BAMS",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private async void frmMarcaProductos_Load(object sender, EventArgs e)
        {
            await CargarGridMarcas();
        }

        private void dgvMarcas_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvMarcas.CurrentRow != null && dgvMarcas.SelectedRows.Count > 0)
                AbrirModificar();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            using (var frm = new frmIngresarMarcaProducto(_repositorio))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    _ = CargarGridMarcas();
            }
            dgvMarcas.ClearSelection();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvMarcas.CurrentRow != null && dgvMarcas.SelectedRows.Count > 0)
            {
                try
                {
                    AbrirModificar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar editar el registro: " + ex.Message, "Sistema BAMS",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una marca de la lista para modificar.", "SG-BAMS",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AbrirModificar()
        {
            int id = Convert.ToInt32(dgvMarcas.CurrentRow.Cells["id_marca_producto"].Value);
            string nombre = dgvMarcas.CurrentRow.Cells["nombre_marca"].Value.ToString();

            using (var frm = new frmModificarMarcaProducto(id, nombre, _repositorio))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    _ = CargarGridMarcas();
            }
            dgvMarcas.ClearSelection();
        }
    }
}
