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
                // 1. Instanciamos la clase
                SG_BAMS.ProductoInventario.ClsActualizarProducto logica = new SG_BAMS.ProductoInventario.ClsActualizarProducto();

                // 2. Limpieza rápida del precio (por si acaso tiene espacios o símbolos)
                string precioLimpio = txtPrecio.Text.Replace("Lps", "").Replace("$", "").Trim();

                // 3. Ejecutamos la actualización
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

                // 4. Mensaje de éxito
                MessageBox.Show("¡Producto actualizado correctamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 5. ASIGNACIÓN CLAVE: Esto dispara el refresco en el formulario principal
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
        }

        public void LlenarCombosModificar()
        {
            ClsLlenarCombo llenar = new ClsLlenarCombo();

            // Llenar Marcas
            cmbMarca.DataSource = llenar.ObtenerDatosCombo("Marca");
            cmbMarca.DisplayMember = "nombre_marca";
            cmbMarca.ValueMember = "id_marca_producto";

            // Llenar Tipos
            cmbTipo.DataSource = llenar.ObtenerDatosCombo("Tipo");
            cmbTipo.DisplayMember = "descripcion_forma_pago";
            cmbTipo.ValueMember = "id_tipo_producto";

            // Llenar Modelos
            cmbModelo.DataSource = llenar.ObtenerDatosCombo("Modelo");
            cmbModelo.DisplayMember = "nombre_modelo_auto";
            cmbModelo.ValueMember = "id_modelo_auto";

            // Llenar el nuevo de Estado
            cmbEstado.DataSource = llenar.ObtenerDatosCombo("Estado");
            cmbEstado.DisplayMember = "descripcion_estado";
            cmbEstado.ValueMember = "id_estado";
        }
    }
}
