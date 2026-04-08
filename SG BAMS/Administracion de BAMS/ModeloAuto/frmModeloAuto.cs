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
    /// <summary>
    /// Interfaz de usuario para la visualización y gestión de los modelos de automóviles en el sistema.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class frmModeloAuto : Form
    {
        /// <summary>
        /// Instancia de la lógica de negocio para las operaciones de modelos de auto.
        /// </summary>
        clsModeloAuto objetoModelo = new clsModeloAuto();

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="frmModeloAuto"/>.
        /// </summary>
        public frmModeloAuto()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Load += new EventHandler(frmModelosAuto_Load);
        }

        /// <summary>
        /// Maneja el evento de carga del formulario para llenar el grid de datos.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private async void frmModelosAuto_Load(object sender, EventArgs e)
        {
            await CargarGridModelos();
        }

        /// <summary>
        /// Obtiene de forma asíncrona la lista de modelos de auto y la vincula al DataGridView.
        /// </summary>
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

        /// <summary>
        /// Configura las columnas, encabezados y comportamientos de selección del DataGridView.
        /// </summary>
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


        /// <summary>
        /// Abre el formulario de creación de modelo y actualiza el grid si la operación fue exitosa.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAgregarModeloAuto agregarMauto = new frmAgregarModeloAuto();


            if (agregarMauto.ShowDialog() == DialogResult.OK)
            {
                _ = CargarGridModelos();
            }
        }


        /// <summary>
        /// Valida la selección actual y abre el formulario para modificar el modelo seleccionado.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
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

        /// <summary>
        /// Ejecuta la acción de modificación al hacer doble clic sobre una celda del grid.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="DataGridViewCellEventArgs"/> que contiene los datos del evento.</param>
        private void dgvModelos_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btnModificar_Click(sender, e);
        }

        /// <summary>
        /// Cierra la ventana de gestión de modelos de auto.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Aplica estilos visuales y de formato al DataGridView para mejorar la legibilidad.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
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