using SG_BAMS.Administracion_de_BAMS;
using System;
using System.Windows.Forms;
using SG_BAMS.AccesoDatos;
using SG_BAMS.Dominio.AdministracionBAMS;

namespace SG_BAMS.Administracion_de_BAMS.Clasificacion
{
    public partial class Clasificacion : Form
    {
        private readonly ICatalogoRepository _repositorio;

        public Clasificacion(ICatalogoRepository repositorio)
        {
            InitializeComponent();
            _repositorio = repositorio;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        public Clasificacion() : this(new clsClasificacion()) { }

        private async System.Threading.Tasks.Task CargarGrid()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                dgvClasificacion.DataSource = await _repositorio.LeerAsync();

                if (dgvClasificacion.Columns.Contains("id_clasificacion_proveedor"))
                    dgvClasificacion.Columns["id_clasificacion_proveedor"].Visible = false;

                if (dgvClasificacion.Columns.Contains("clasificacion_proveedor"))
                    dgvClasificacion.Columns["clasificacion_proveedor"].HeaderText = "Clasificación";

                dgvClasificacion.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvClasificacion.AllowUserToAddRows = false;
                dgvClasificacion.ReadOnly = true;
                dgvClasificacion.ClearSelection();

                EstiloDataGridView.Aplicar(dgvClasificacion);
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

        private async void Clasificacion_Load(object sender, EventArgs e)
        {
            await CargarGrid();
        }

        private void dgvClasificacion_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvClasificacion.SelectedRows.Count > 0)
                AbrirModificar();
        }

        private void btmAgregar_Click(object sender, EventArgs e)
        {
            using (var frm = new AgregarClasificacion(_repositorio))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    _ = CargarGrid();
            }
            dgvClasificacion.ClearSelection();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvClasificacion.SelectedRows.Count > 0)
                AbrirModificar();
            else
                MessageBox.Show("Por favor, seleccione una clasificación de la lista.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            dgvClasificacion.ClearSelection();
        }

        private void btnSalir_Click(object sender, EventArgs e) => this.Close();

        private void AbrirModificar()
        {
            int id = Convert.ToInt32(dgvClasificacion.CurrentRow.Cells["id_clasificacion_proveedor"].Value);
            string descrip = dgvClasificacion.CurrentRow.Cells["clasificacion_proveedor"].Value.ToString();
            using (var frm = new ModificarClasificacion(id, descrip, _repositorio))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                    _ = CargarGrid();
            }
        }
    }
}