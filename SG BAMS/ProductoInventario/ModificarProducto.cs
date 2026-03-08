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
    public partial class ModificarProducto : Form
    {

        public string marcaActual, tipoActual, modeloActual, estadoActual;

        public ModificarProducto()
        {
            InitializeComponent();
        }

        private void kryptonTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void kryptonButton20_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ClsValidacion.ValidarNombre(txtNombre.Text)) return;
                string precioLimpio = txtPrecio.Text.Replace("Lps", "").Replace("$", "").Trim();
                if (!ClsValidacion.ValidarPrecio(precioLimpio)) return;
                if (!ClsValidacion.ValidarServicio(txtServicio.Text)) return;
                if (!ClsValidacion.ValidarCodigoBarra(txtCodigoBarra.Text)) return;
                if (!ClsValidacion.ValidarSeleccion(cmbMarca, "Marca")) return;
                if (!ClsValidacion.ValidarSeleccion(cmbTipo, "Tipo")) return;
                if (!ClsValidacion.ValidarSeleccion(cmbModelo, "Modelo")) return;
                if (!ClsValidacion.ValidarSeleccion(cmbEstado, "Estado")) return;
                SG_BAMS.ProductoInventario.ClsActualizarProducto logica = new SG_BAMS.ProductoInventario.ClsActualizarProducto();

                logica.EjecutarActualizacion(
                    Convert.ToInt32(txtID.Text),
                    txtNombre.Text,
                    Convert.ToInt32(cmbMarca.SelectedValue),
                    Convert.ToInt32(cmbTipo.SelectedValue),
                    Convert.ToInt32(cmbModelo.SelectedValue),
                    Convert.ToInt32(cmbEstado.SelectedValue),
                    Convert.ToDecimal(precioLimpio),
                    txtServicio.Text,
                    txtCodigoBarra.Text
                );

                MessageBox.Show("¡Producto actualizado correctamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar cambios: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ModificarProducto_Load(object sender, EventArgs e)
        {
            LlenarCombosModificar();

            cmbMarca.SelectedIndex = cmbMarca.FindStringExact(marcaActual);
            cmbTipo.SelectedIndex = cmbTipo.FindStringExact(tipoActual);
            cmbModelo.SelectedIndex = cmbModelo.FindStringExact(modeloActual);
            cmbEstado.SelectedIndex = cmbEstado.FindStringExact(estadoActual);
        }

        public void LlenarCombosModificar()
        {
            ClsLlenarCombo llenar = new ClsLlenarCombo();

            cmbMarca.DataSource = llenar.ObtenerDatosCombo("Marca");
            cmbMarca.DisplayMember = "nombre_marca";
            cmbMarca.ValueMember = "id_marca_producto";

            cmbTipo.DataSource = llenar.ObtenerDatosCombo("Tipo");
            cmbTipo.DisplayMember = "descripcion_forma_pago";
            cmbTipo.ValueMember = "id_tipo_producto";

            cmbModelo.DataSource = llenar.ObtenerDatosCombo("Modelo");
            cmbModelo.DisplayMember = "nombre_modelo_auto";
            cmbModelo.ValueMember = "id_modelo_auto";

            cmbEstado.DataSource = llenar.ObtenerDatosCombo("Estado");
            cmbEstado.DisplayMember = "descripcion_estado";
            cmbEstado.ValueMember = "id_estado";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
