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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarProducto frm = new AgregarProducto();
            // Usamos ShowDialog para que el usuario no pueda clickear atrás hasta terminar
            if (frm.ShowDialog() == DialogResult.OK)
            {
                // Esto refrescará tu tabla principal automáticamente después de guardar
                CargarInventarioCompleto();
            }
        }
    }
}
