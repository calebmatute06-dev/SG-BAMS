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

                // Obtenemos los datos de la vista
                DataTable dt = await objetoModelo.LeerModelosAsync();

                // Asignamos al DataGrid (asegúrate que el nombre sea dgvModelos)
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
            // Ocultar ID técnico
            if (dgvModelos.Columns.Contains("id_modelo_auto"))
                dgvModelos.Columns["id_modelo_auto"].Visible = false;

            // Títulos de columnas
            if (dgvModelos.Columns.Contains("nombre_modelo_auto"))
                dgvModelos.Columns["nombre_modelo_auto"].HeaderText = "Modelo de Vehículo";

            if (dgvModelos.Columns.Contains("productos_compatibles"))
                dgvModelos.Columns["productos_compatibles"].HeaderText = "Cant. Productos Relacionados";

            // Estilo del Grid
            dgvModelos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvModelos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvModelos.AllowUserToAddRows = false;
            dgvModelos.ReadOnly = true;
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btmAgregar_Click(object sender, EventArgs e)
        {
            frmAgregarModeloAuto agregarMauto = new frmAgregarModeloAuto();
            agregarMauto.Show();
            this.Close();
        }

        private void btmModificar_Click(object sender, EventArgs e)
        {
            if (dgvModelos.SelectedRows.Count > 0)
            {
                // 2. Extraemos los valores de las celdas (deben coincidir con tu Vista SQL)
                int id = Convert.ToInt32(dgvModelos.CurrentRow.Cells["id_modelo_auto"].Value);
                string nombre = dgvModelos.CurrentRow.Cells["nombre_modelo_auto"].Value.ToString();

                // 3. Abrimos el formulario de modificación pasando los datos
                frmModificarModelos frmMod = new frmModificarModelos(id, nombre);

                if (frmMod.ShowDialog() == DialogResult.OK)
                {
                    // 4. Si se guardó con éxito, recargamos el DataGrid
                    _ = CargarGridModelos();
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un modelo de la lista.");
            }
        }
    }
}
