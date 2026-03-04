using SG_BAMS.Administracion_de_BAMS.MarcaProd;
using SG_BAMS.Login;
using SG_BAMS.ProductoInventario;
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
    public partial class InventarioAdmin : Form
    {

        ClsVerProducto logica = new ClsVerProducto();

        public InventarioAdmin()
        {
            InitializeComponent();
        }

        public void CargarInventarioCompleto()
        {
            try
            {
                dgvProductosAdmin.DataSource = logica.MostrarProductosCompleto();
                dgvProductosAdmin.ReadOnly = true;
                dgvProductosAdmin.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvProductosAdmin.AllowUserToAddRows = false;
                dgvProductosAdmin.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                if (dgvProductosAdmin.Columns.Contains("Producto"))
                {
                    dgvProductosAdmin.Columns["Producto"].MinimumWidth = 150;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el inventario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InventarioAdmin_Load(object sender, EventArgs e)
        {
            CargarInventarioCompleto();
        }

        private void CargarCombosFormulario()
        {
            try
            {
                ClsLLenarCombo helper = new ClsLLenarCombo();

                // 1. Configurar Combo Marcas
                cmbMarca.DataSource = helper.LlenarMarca();
                cmbMarca.DisplayMember = "nombre_marca";
                cmbMarca.ValueMember = "id_marca_producto";

                // 2. Configurar Combo Tipos
                cmbTipo.DataSource = helper.LlenarTipo();
                cmbTipo.DisplayMember = "descripcion_forma_pago";
                cmbTipo.ValueMember = "id_tipo_producto";

                // 3. Configurar Combo Modelos
                cmbModelo.DataSource = helper.LlenarModelo();
                cmbModelo.DisplayMember = "nombre_modelo_auto";
                cmbModelo.ValueMember = "id_modelo_auto";

                // Dejar los combos vacíos al inicio (opcional)
                cmbMarca.SelectedIndex = -1;
                cmbTipo.SelectedIndex = -1;
                cmbModelo.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudieron cargar las listas: " + ex.Message, "Error de Datos");
            }
        }
    }
}
