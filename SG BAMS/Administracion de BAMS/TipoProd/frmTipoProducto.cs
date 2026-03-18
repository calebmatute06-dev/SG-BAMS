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
    public partial class frmTipoProducto : Form
    {
        public frmTipoProducto()
        {
            InitializeComponent();
            CargarGridTipos();
        }

        private async void frmTipoProducto_Load(object sender, EventArgs e)
        {
            await CargarGridTipos();
        }

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

        private void btmAgregar_Click(object sender, EventArgs e)
        {
            frnAgregarTipoProducto agregarTproducto = new frnAgregarTipoProducto();
            agregarTproducto.Show();
            this.Close();
        }


        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvTipoProducto1.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvTipoProducto1.CurrentRow.Cells["id_tipo_producto"].Value);
                string descripcion = dgvTipoProducto1.CurrentRow.Cells["nombre_tipo_producto"].Value.ToString();

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

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
