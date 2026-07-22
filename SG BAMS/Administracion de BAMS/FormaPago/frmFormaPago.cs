using SG_BAMS.Administracion_de_BAMS;
using SG_BAMS.Administracion_de_BAMS.FormaPago;
using System;
using System.Windows.Forms;

namespace SG_BAMS
{
    // ============================================================
    // frmFormaPago — MIGRADO A DIP (igual patrón que Clasificacion/Estado)
    // ============================================================
    // PROBLEMA ANTERIOR:
    //   clsFormaPago objetoFP = new clsFormaPago(); → viola DIP.
    //   El formulario estaba acoplado a la implementación concreta
    //   en vez de depender de la abstracción ICatalogoRepository.
    //
    // CORRECCIÓN:
    //   - Inyecta ICatalogoRepository por constructor (DIP)
    //   - Constructor sin parámetros para el diseñador de WinForms
    //   - Usa EstiloDataGridView.Aplicar() en vez del bloque de estilos duplicado
    //   - Delega la apertura de Agregar/Modificar pasando el mismo repositorio
    // ============================================================

    /// <summary>
    /// Representa la interfaz de usuario para la visualización y administración de las formas de pago.
    /// DIP: recibe ICatalogoRepository inyectado, no instancia clsFormaPago directamente.
    /// </summary>
    public partial class frmFormaPago : Form
    {
        private readonly ICatalogoRepository _repositorio;

        public frmFormaPago(ICatalogoRepository repositorio)
        {
            InitializeComponent();
            _repositorio = repositorio;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        /// <summary>
        /// Constructor sin parámetros para compatibilidad con el diseñador de WinForms
        /// y con el código existente que abre este formulario sin inyección explícita.
        /// </summary>
        public frmFormaPago() : this(new clsFormaPago()) { }

        private async System.Threading.Tasks.Task CargarGridFormasPago()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                dgvFormasPago.DataSource = await _repositorio.LeerAsync();

                if (dgvFormasPago.Columns.Contains("id_tipo_forma_pago"))
                    dgvFormasPago.Columns["id_tipo_forma_pago"].Visible = false;

                if (dgvFormasPago.Columns.Contains("descripcion_forma_pago"))
                    dgvFormasPago.Columns["descripcion_forma_pago"].HeaderText = "Método de Pago";

                if (dgvFormasPago.Columns.Contains("total_uso_facturas"))
                    dgvFormasPago.Columns["total_uso_facturas"].HeaderText = "Uso en Facturas";

                dgvFormasPago.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvFormasPago.AllowUserToAddRows = false;
                dgvFormasPago.ReadOnly = true;
                dgvFormasPago.ClearSelection();

                EstiloDataGridView.Aplicar(dgvFormasPago);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error de Carga",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private async void frmFormaPago_Load(object sender, EventArgs e)
        {
            await CargarGridFormasPago();
        }

        private void dgvFormasPago_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvFormasPago.SelectedRows.Count > 0)
                AbrirModificar();
            else
                MessageBox.Show("Por favor, seleccione una fila para editar.");
        }

        private void btmAgregar2_Click(object sender, EventArgs e)
        {
            AbrirAgregar();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AbrirAgregar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvFormasPago.SelectedRows.Count > 0)
                AbrirModificar();
            else
                MessageBox.Show("Por favor, seleccione una fila para editar.");
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AbrirAgregar()
        {
            using (var frm = new frmAgregarFormaPago(_repositorio))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    _ = CargarGridFormasPago();
            }
            dgvFormasPago.ClearSelection();
        }

        private void AbrirModificar()
        {
            int id = Convert.ToInt32(dgvFormasPago.CurrentRow.Cells["id_tipo_forma_pago"].Value);
            string descripcion = dgvFormasPago.CurrentRow.Cells["descripcion_forma_pago"].Value.ToString();

            using (var frm = new frmModificarFormaPago(id, descripcion, _repositorio))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    _ = CargarGridFormasPago();
            }
            dgvFormasPago.ClearSelection();
        }
    }
}
