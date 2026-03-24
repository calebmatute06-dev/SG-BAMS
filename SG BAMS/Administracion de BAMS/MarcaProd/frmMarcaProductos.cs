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
    public partial class frmMarcaProductos : Form
    {
        clsMarca objetoMarca = new clsMarca();
        public frmMarcaProductos()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmMarcas_Load);
        }
        private async void frmMarcas_Load(object sender, EventArgs e)
        {
            await CargarGridMarcas();
        }
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

        private void frmMarcaProductos_Load(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmIngresarMarcaProducto agregarMproducto = new frmIngresarMarcaProducto();
            agregarMproducto.Show();
            this.Close();
        }

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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
