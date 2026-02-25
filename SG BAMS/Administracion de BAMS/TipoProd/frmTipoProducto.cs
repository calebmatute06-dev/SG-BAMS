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
                // Cambiamos el cursor a modo espera
                this.Cursor = Cursors.WaitCursor;

                clsTipoProducto objetoTipo = new clsTipoProducto();

                // Asignamos el resultado de la vista al DataGridView
                dgvTipoProducto.DataSource = await objetoTipo.LeerTiposProductoAsync();

                // Configuración visual rápida
                dgvTipoProducto.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvTipoProducto.AllowUserToAddRows = false;
                dgvTipoProducto.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Restauramos el cursor
                this.Cursor = Cursors.Default;
            }
        }
        private void btmModificar_Click(object sender, EventArgs e)
        {
            // 1. Verificamos que haya una fila seleccionada en el grid de tipos de producto
            if (dgvTipoProducto.SelectedRows.Count > 0)
            {
                // 2. Obtenemos el ID y la Descripción de la fila actual
                // Asegúrate de que los nombres "id_tipo_producto" y "nombre_tipo_producto" coincidan con tu Vista SQL
                int id = Convert.ToInt32(dgvTipoProducto.CurrentRow.Cells["id_tipo_producto"].Value);
                string descripcion = dgvTipoProducto.CurrentRow.Cells["nombre_tipo_producto"].Value.ToString();

                // 3. Pasamos los datos al constructor del formulario de modificación
                frmModificarTipoProducto ModificarTProducto = new frmModificarTipoProducto(id, descripcion);

                // Usamos ShowDialog para que el usuario termine de editar antes de seguir
                if (ModificarTProducto.ShowDialog() == DialogResult.OK)
                {
                    // Opcional: Recargar el grid para ver el cambio reflejado
                    _ = CargarGridTipos();
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un tipo de producto de la lista.");
            }
        }

        private void btmSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btmAgregar_Click(object sender, EventArgs e)
        {
            frnAgregarTipoProducto agregarTproducto = new frnAgregarTipoProducto();
            agregarTproducto.Show();
            this.Close();
        }
    }
}
