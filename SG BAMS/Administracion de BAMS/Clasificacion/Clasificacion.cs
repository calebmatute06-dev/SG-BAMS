using SG_BAMS.Administracion_de_BAMS.TipoProd;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS.Administracion_de_BAMS.Clasificacion
{
    public partial class Clasificacion : Form
    {
        public Clasificacion()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            CargarGridClasi();
        }
        private async Task CargarGridClasi()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                clsClasificacion objetoCla = new clsClasificacion();

                dgvClasificacion.DataSource = await objetoCla.LeerClasificacionAsync();

                if (dgvClasificacion.Columns.Contains("clasificacion_proveedor"))
                    dgvClasificacion.Columns["clasificacion_proveedor"].HeaderText = "Clasificación";

                if (dgvClasificacion.Columns.Contains("id_clasificacion_proveedor"))
                    dgvClasificacion.Columns["id_clasificacion_proveedor"].HeaderText = "ID";

                dgvClasificacion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvClasificacion.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvClasificacion.AllowUserToAddRows = false;
                dgvClasificacion.ReadOnly = true;
                dgvClasificacion.ClearSelection();

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

        private void dgvClasificacion_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvClasificacion.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvClasificacion.CurrentRow.Cells["id_clasificacion_proveedor"].Value);
                string descripcion = dgvClasificacion.CurrentRow.Cells["clasificacion_proveedor"].Value.ToString();

                ModificarClasificacion modificarClasificacion = new ModificarClasificacion(id, descripcion);

                if (modificarClasificacion.ShowDialog() == DialogResult.OK)
                {
                    _ = CargarGridClasi();
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una clasificación de la lista.");
            }
        }

        private void btmAgregar_Click(object sender, EventArgs e)
        {
            AgregarClasificacion agregarClasificacion = new AgregarClasificacion();
            if (agregarClasificacion.ShowDialog() == DialogResult.OK)
            {
                _ = CargarGridClasi();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvClasificacion.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvClasificacion.SelectedRows[0].Cells["id_clasificacion_proveedor"].Value);
                string descripcion = dgvClasificacion.SelectedRows[0].Cells["clasificacion_proveedor"].Value.ToString();

                ModificarClasificacion modificarClasificacion = new ModificarClasificacion(id, descripcion);

                if (modificarClasificacion.ShowDialog() == DialogResult.OK)
                {
                    _ = CargarGridClasi();
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila completa de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
            AdministracionBAMS administracionBAMS = new AdministracionBAMS();
            administracionBAMS.Show();
        }

        private async void Clasificacion_Load(object sender, EventArgs e)
        {
            await CargarGridClasi();
            dgvClasificacion.BorderStyle = BorderStyle.None;
            dgvClasificacion.BackgroundColor = Color.White;
            dgvClasificacion.RowHeadersVisible = false;
            dgvClasificacion.EnableHeadersVisualStyles = false;
            dgvClasificacion.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgvClasificacion.ColumnHeadersDefaultCellStyle.BackColor = Color.SkyBlue;
            dgvClasificacion.ColumnHeadersDefaultCellStyle.ForeColor = Color.Navy;
            dgvClasificacion.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvClasificacion.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvClasificacion.ColumnHeadersHeight = 28;

            dgvClasificacion.DefaultCellStyle.BackColor = Color.White;
            dgvClasificacion.DefaultCellStyle.ForeColor = Color.Navy;
            dgvClasificacion.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvClasificacion.DefaultCellStyle.Padding = new Padding(3);
            dgvClasificacion.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 245, 255);
            dgvClasificacion.AlternatingRowsDefaultCellStyle.ForeColor = Color.Navy;

            dgvClasificacion.DefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            dgvClasificacion.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvClasificacion.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvClasificacion.GridColor = Color.LightGray;
            dgvClasificacion.RowTemplate.Height = 32;
            dgvClasificacion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvClasificacion.ClearSelection();
        }
    }
}
