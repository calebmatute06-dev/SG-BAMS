using SG_BAMS.Facturas;
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
    public partial class AgregarProducto : Form
    {
        public AgregarProducto()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // 1. Validaciones básicas
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || cmbMarca.SelectedValue == null)
            {
                MessageBox.Show("Por favor llene los campos obligatorios", "Advertencia");
                return;
            }

            try
            {
                ClsAgregarProducto logicaInsertar = new ClsAgregarProducto();

                // Convertimos los valores de los controles
                string nombre = txtNombre.Text;
                int idMarca = (int)cmbMarca.SelectedValue;
                int idTipo = (int)cmbTipo.SelectedValue;
                int idModelo = (int)cmbModelo.SelectedValue;
                decimal precio = decimal.Parse(txtPrecio.Text);
                string servicio = txtServicio.Text;
                string codBarra = txtCodigoBarra.Text;

                // 2. Ejecutamos la inserción
                logicaInsertar.EjecutarInsercion(nombre, idMarca, idTipo, idModelo, precio, servicio, codBarra);

                MessageBox.Show("Producto guardado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Indicamos que todo salió bien para que el formulario principal se refresque
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void AgregarProducto_Load(object sender, EventArgs e)
        {
            LlenarTodosLosCombos();
        }

        private void LlenarTodosLosCombos()
        {
            ClsLlenarCombo llenar = new ClsLlenarCombo();

            try
            {
                // 1. Llenar Marcas
                cmbMarca.DataSource = llenar.ObtenerDatosCombo("Marca");
                cmbMarca.DisplayMember = "nombre_marca";
                cmbMarca.ValueMember = "id_marca_producto";

                // 2. Llenar Tipos
                cmbTipo.DataSource = llenar.ObtenerDatosCombo("Tipo");
                cmbTipo.DisplayMember = "descripcion_forma_pago"; // Nombre exacto en tu SQL
                cmbTipo.ValueMember = "id_tipo_producto";

                // 3. Llenar Modelos
                cmbModelo.DataSource = llenar.ObtenerDatosCombo("Modelo");
                cmbModelo.DisplayMember = "nombre_modelo_auto";
                cmbModelo.ValueMember = "id_modelo_auto";

                // Opcional: Que aparezcan vacíos al inicio
                cmbMarca.SelectedIndex = -1;
                cmbTipo.SelectedIndex = -1;
                cmbModelo.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
