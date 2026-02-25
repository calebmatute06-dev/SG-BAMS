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
            frmModificarModelos modificarMauto = new frmModificarModelos();
            modificarMauto.Show();
            this.Close();
        }
    }
}
