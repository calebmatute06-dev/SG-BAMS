using SG_BAMS.Administracion_de_BAMS.MarcaProd;
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
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class frmMarcaProductos : Form
    {
        /// <summary>
        /// The objeto marca
        /// </summary>
        clsMarca objetoMarca = new clsMarca();
        /// <summary>
        /// Initializes a new instance of the <see cref="frmMarcaProductos"/> class.
        /// </summary>
        public frmMarcaProductos()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmMarcas_Load);
        }
        /// <summary>
        /// Handles the Load event of the frmMarcas control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void frmMarcas_Load(object sender, EventArgs e)
        {
            await CargarGridMarcas();
        }
        /// <summary>
        /// Cargars the grid marcas.
        /// </summary>
        private async Task CargarGridMarcas()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                DataTable dt = await objetoMarca.LeerMarcasAsync();

                dgvMarcas.DataSource = dt;

                ConfigurarDisenoGrid();
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

        /// <summary>
        /// Configurars the diseno grid.
        /// </summary>
        private void ConfigurarDisenoGrid()
        {
            if (dgvMarcas.Columns.Contains("id_marca_producto"))
                dgvMarcas.Columns["id_marca_producto"].Visible = false;

            if (dgvMarcas.Columns.Contains("nombre_marca"))
                dgvMarcas.Columns["nombre_marca"].HeaderText = "Marca";

            if (dgvMarcas.Columns.Contains("cantidad_productos"))
                dgvMarcas.Columns["cantidad_productos"].HeaderText = "Productos Asociados";

            dgvMarcas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMarcas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMarcas.AllowUserToAddRows = false;
            dgvMarcas.ReadOnly = true;
            dgvMarcas.ClearSelection();
        }


        /// <summary>
        /// Handles the CellContentDoubleClick event of the dgvMarcas control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dgvMarcas_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(dgvMarcas.CurrentRow.Cells["id_marca_producto"].Value);
            string nombre = dgvMarcas.CurrentRow.Cells["nombre_marca"].Value.ToString();

            frmModificarMarcaProducto frm = new frmModificarMarcaProducto(id, nombre);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                _ = CargarGridMarcas();
            }
        }

        /// <summary>
        /// Handles the Load event of the frmMarcaProductos control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void frmMarcaProductos_Load(object sender, EventArgs e)
        {
            dgvMarcas.BorderStyle = BorderStyle.None;
            dgvMarcas.BackgroundColor = Color.White;
            dgvMarcas.RowHeadersVisible = false;
            dgvMarcas.EnableHeadersVisualStyles = false;
            dgvMarcas.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgvMarcas.ColumnHeadersDefaultCellStyle.BackColor = Color.SkyBlue;
            dgvMarcas.ColumnHeadersDefaultCellStyle.ForeColor = Color.Navy;
            dgvMarcas.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvMarcas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvMarcas.ColumnHeadersHeight = 28;

            dgvMarcas.DefaultCellStyle.BackColor = Color.White;
            dgvMarcas.DefaultCellStyle.ForeColor = Color.Navy;
            dgvMarcas.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvMarcas.DefaultCellStyle.Padding = new Padding(3);
            dgvMarcas.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 245, 255);
            dgvMarcas.AlternatingRowsDefaultCellStyle.ForeColor = Color.Navy;

            dgvMarcas.DefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            dgvMarcas.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvMarcas.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvMarcas.GridColor = Color.LightGray;
            dgvMarcas.RowTemplate.Height = 32;
            dgvMarcas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMarcas.ClearSelection();
        }

        /// <summary>
        /// Handles the Click event of the btnAgregar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmIngresarMarcaProducto agregarMproducto = new frmIngresarMarcaProducto();
            agregarMproducto.Show();
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the btnModificar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnModificar_Click(object sender, EventArgs e)
        {
            
            if (dgvMarcas.CurrentRow != null && dgvMarcas.SelectedRows.Count > 0)
            {
                try
                {
                    
                    int id = Convert.ToInt32(dgvMarcas.CurrentRow.Cells["id_marca_producto"].Value);
                    string nombre = dgvMarcas.CurrentRow.Cells["nombre_marca"].Value.ToString();

                    
                    frmModificarMarcaProducto frm = new frmModificarMarcaProducto(id, nombre);

                   
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        
                        _ = CargarGridMarcas();
                    }
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

        /// <summary>
        /// Handles the Click event of the btnSalir control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
