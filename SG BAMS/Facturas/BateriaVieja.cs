using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS.Facturas
{
    public partial class BateriaVieja : Form
    {
        public BateriaVieja()
        {
            InitializeComponent();
        }

        private void BateriaVieja_Load(object sender, EventArgs e)
        {
            dgvBateria.Columns.Clear();

            dgvBateria.Columns.Add("nombre", "Batería");
            dgvBateria.Columns.Add("precio", "Precio");
            dgvBateria.Columns.Add("cantidad", "Cantidad");

            cmbBaterias.Items.Add("Moto");
            cmbBaterias.Items.Add("Carro");
            cmbBaterias.Items.Add("Camión");


        }

        private void Agregar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtCantidad.Text, out int cant))
            {
                MessageBox.Show("La cantidad debe ser un número válido.");
                return;
            }

            if (cmbBaterias.SelectedIndex == -1 ||
            string.IsNullOrWhiteSpace(txtPrecio.Text) ||
            string.IsNullOrWhiteSpace(txtCantidad.Text))
            {
                MessageBox.Show("Por favor, llene todos los campos ",
                                "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nombreBateria = cmbBaterias.Text;
            string precioBateria = txtPrecio.Text;
            string cantidadBateria = txtCantidad.Text;

            dgvBateria.Columns["nombre"].ReadOnly = true;

            dgvBateria.Columns["precio"].ReadOnly = false;
            dgvBateria.Columns["cantidad"].ReadOnly = false;

            dgvBateria.Rows.Add(nombreBateria, precioBateria, cantidadBateria);

            cmbBaterias.SelectedIndex = -1;
            txtPrecio.Clear();
            txtCantidad.Clear();
            cmbBaterias.Focus();

        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
