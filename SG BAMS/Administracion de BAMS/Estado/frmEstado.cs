using SG_BAMS.Administracion_de_BAMS;
using System;
using System.Windows.Forms;
using SG_BAMS.AccesoDatos;
using SG_BAMS.Dominio.AdministracionBAMS;

namespace SG_BAMS
{
    public partial class frmEstado : Form
    {
        private readonly ICatalogoRepository _repositorio;

        public frmEstado(ICatalogoRepository repositorio)
        {
            InitializeComponent();
            _repositorio = repositorio;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        public frmEstado() : this(new clsEstado()) { }

        private async System.Threading.Tasks.Task CargarGrid()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                dgvEstados.DataSource = await _repositorio.LeerAsync();

                if (dgvEstados.Columns.Contains("id_estado"))
                    dgvEstados.Columns["id_estado"].Visible = false;

                if (dgvEstados.Columns.Contains("descripcion_estado"))
                    dgvEstados.Columns["descripcion_estado"].HeaderText = "Estado";

                dgvEstados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvEstados.AllowUserToAddRows = false;
                dgvEstados.ReadOnly = true;
                dgvEstados.ClearSelection();

                EstiloDataGridView.Aplicar(dgvEstados);
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

        private async void frmEstado_Load(object sender, EventArgs e)
        {
            await CargarGrid();
        }

        private void dgvEstados_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvEstados.SelectedRows.Count > 0)
                AbrirModificar();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            using (var frm = new frmAgregarEstado(_repositorio))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    _ = CargarGrid();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvEstados.SelectedRows.Count > 0)
                AbrirModificar();
            else
                MessageBox.Show("Por favor, seleccione un estado de la lista.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnSalir_Click(object sender, EventArgs e) => this.Close();

        private void AbrirModificar()
        {
            int id = Convert.ToInt32(dgvEstados.CurrentRow.Cells["id_estado"].Value);
            string descrip = dgvEstados.CurrentRow.Cells["descripcion_estado"].Value.ToString();
            using (var frm = new frmModificarEstado(id, descrip, _repositorio))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    _ = CargarGrid();
            }
        }
    }
}