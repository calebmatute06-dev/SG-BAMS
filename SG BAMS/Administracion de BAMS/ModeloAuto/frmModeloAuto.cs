using SG_BAMS.Administracion_de_BAMS.ModeloAuto;
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
    public partial class frmModeloAuto : Form
    {
        clsModeloAuto objetoModelo = new clsModeloAuto();

        public frmModeloAuto()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmModelosAuto_Load);
        }

        private async void frmModelosAuto_Load(object sender, EventArgs e)
        {
            await CargarGridModelos();
        }

        private async Task CargarGridModelos()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DataTable dt = await objetoModelo.LeerModelosAsync();
                dgvModelos.DataSource = dt;
                ConfigurarDisenoGrid();
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

        private void ConfigurarDisenoGrid()
        {
            if (dgvModelos.Columns.Contains("id_modelo_auto"))
                dgvModelos.Columns["id_modelo_auto"].Visible = false;

            if (dgvModelos.Columns.Contains("nombre_modelo_auto"))
                dgvModelos.Columns["nombre_modelo_auto"].HeaderText = "Modelo de Vehículo";

            if (dgvModelos.Columns.Contains("productos_compatibles"))
                dgvModelos.Columns["productos_compatibles"].HeaderText = "Cant. Productos Relacionados";

            dgvModelos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvModelos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvModelos.AllowUserToAddRows = false;
            dgvModelos.ReadOnly = true;
            dgvModelos.ClearSelection();
        }


        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAgregarModeloAuto agregarMauto = new frmAgregarModeloAuto();


            if (agregarMauto.ShowDialog() == DialogResult.OK)
            {

                _ = CargarGridModelos();
            }


        }


        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvModelos.CurrentRow != null && dgvModelos.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvModelos.CurrentRow.Cells["id_modelo_auto"].Value);
                string nombre = dgvModelos.CurrentRow.Cells["nombre_modelo_auto"].Value.ToString();

                frmModificarModelos frmMod = new frmModificarModelos(id, nombre);

                if (frmMod.ShowDialog() == DialogResult.OK)
                {
                    _ = CargarGridModelos();
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un modelo de la lista.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvModelos_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnModificar_Click(sender, e);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmModeloAuto_Load(object sender, EventArgs e)
        {
            dgvModelos.BorderStyle = BorderStyle.None;
            dgvModelos.BackgroundColor = Color.White;
            dgvModelos.RowHeadersVisible = false;
            dgvModelos.EnableHeadersVisualStyles = false;
            dgvModelos.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgvModelos.ColumnHeadersDefaultCellStyle.BackColor = Color.SkyBlue;
            dgvModelos.ColumnHeadersDefaultCellStyle.ForeColor = Color.Navy;
            dgvModelos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvModelos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvModelos.ColumnHeadersHeight = 28;

            dgvModelos.DefaultCellStyle.BackColor = Color.White;
            dgvModelos.DefaultCellStyle.ForeColor = Color.Navy;
            dgvModelos.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvModelos.DefaultCellStyle.Padding = new Padding(3);
            dgvModelos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 245, 255);
            dgvModelos.AlternatingRowsDefaultCellStyle.ForeColor = Color.Navy;

            dgvModelos.DefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            dgvModelos.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvModelos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvModelos.GridColor = Color.LightGray;
            dgvModelos.RowTemplate.Height = 32;
            dgvModelos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvModelos.ClearSelection();
        }
    }
}