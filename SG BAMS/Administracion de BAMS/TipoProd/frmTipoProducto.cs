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

namespace SG_BAMS
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class frmTipoProducto : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="frmTipoProducto"/> class.
        /// </summary>
        public frmTipoProducto()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            CargarGridTipos();
        }



        /// <summary>
        /// Cargars the grid tipos.
        /// </summary>
        private async Task CargarGridTipos()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                clsTipoProducto objetoTipo = new clsTipoProducto();

                dgvTipoProducto.DataSource = await objetoTipo.LeerTiposProductoAsync();

                if (dgvTipoProducto.Columns.Contains("nombre_tipo_producto"))
                    dgvTipoProducto.Columns["nombre_tipo_producto"].HeaderText = "Nombre del Tipo de Producto";

                if (dgvTipoProducto.Columns.Contains("id_tipo_producto"))
                    dgvTipoProducto.Columns["id_tipo_producto"].HeaderText = "ID";

                if (dgvTipoProducto.Columns.Contains("cantidad_productos_asociados"))
                    dgvTipoProducto.Columns["cantidad_productos_asociados"].HeaderText = "Cantidad de productos";

                dgvTipoProducto.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvTipoProducto.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvTipoProducto.AllowUserToAddRows = false;
                dgvTipoProducto.ReadOnly = true;
                dgvTipoProducto.ClearSelection();

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

        /// <summary>
        /// Handles the CellContentDoubleClick event of the dgvTipoProducto control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dgvTipoProducto_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvTipoProducto.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvTipoProducto.CurrentRow.Cells["id_tipo_producto"].Value);
                string descripcion = dgvTipoProducto.CurrentRow.Cells["nombre_tipo_producto"].Value.ToString();

                frmModificarTipoProducto ModificarTProducto = new frmModificarTipoProducto(id, descripcion);


                if (ModificarTProducto.ShowDialog() == DialogResult.OK)
                {
                    _ = CargarGridTipos();
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un tipo de producto de la lista.");
            }
        }

        /// <summary>
        /// Handles the Click event of the btmAgregar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btmAgregar_Click(object sender, EventArgs e)
        {
            frnAgregarTipoProducto agregarTproducto = new frnAgregarTipoProducto();
            agregarTproducto.Show();
            this.Close();
        }


        /// <summary>
        /// Handles the Click event of the btnModificar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvTipoProducto.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvTipoProducto.SelectedRows[0].Cells["id_tipo_producto"].Value);
                string descripcion = dgvTipoProducto.SelectedRows[0].Cells["nombre_tipo_producto"].Value.ToString();

                frmModificarTipoProducto ModificarTProducto = new frmModificarTipoProducto(id, descripcion);

                if (ModificarTProducto.ShowDialog() == DialogResult.OK)
                {
                    _ = CargarGridTipos();
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila completa de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Handles the Click event of the kryptonButton2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the 1 event of the frmTipoProducto_Load control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void frmTipoProducto_Load_1(object sender, EventArgs e)
        {
            await CargarGridTipos();
            dgvTipoProducto.BorderStyle = BorderStyle.None;
            dgvTipoProducto.BackgroundColor = Color.White;
            dgvTipoProducto.RowHeadersVisible = false;
            dgvTipoProducto.EnableHeadersVisualStyles = false;
            dgvTipoProducto.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgvTipoProducto.ColumnHeadersDefaultCellStyle.BackColor = Color.SkyBlue;
            dgvTipoProducto.ColumnHeadersDefaultCellStyle.ForeColor = Color.Navy;
            dgvTipoProducto.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvTipoProducto.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvTipoProducto.ColumnHeadersHeight = 28;

            dgvTipoProducto.DefaultCellStyle.BackColor = Color.White;
            dgvTipoProducto.DefaultCellStyle.ForeColor = Color.Navy;
            dgvTipoProducto.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvTipoProducto.DefaultCellStyle.Padding = new Padding(3);
            dgvTipoProducto.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 245, 255);
            dgvTipoProducto.AlternatingRowsDefaultCellStyle.ForeColor = Color.Navy;

            dgvTipoProducto.DefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            dgvTipoProducto.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvTipoProducto.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvTipoProducto.GridColor = Color.LightGray;
            dgvTipoProducto.RowTemplate.Height = 32;
            dgvTipoProducto.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTipoProducto.ClearSelection();
        }
    }
}
