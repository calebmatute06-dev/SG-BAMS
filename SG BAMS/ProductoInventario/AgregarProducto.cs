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

            if (!ClsValidacion.ValidarNombre(txtNombre.Text)) return;
            if (!ClsValidacion.ValidarPrecio(txtPrecio.Text)) return;
            if (!ClsValidacion.ValidarSeleccion(cmbMarca, "la Marca")) return;
            if (!ClsValidacion.ValidarSeleccion(cmbTipo, "el Tipo de Producto")) return;
            if (!ClsValidacion.ValidarSeleccion(cmbModelo, "el Modelo de Auto")) return;
            if (!ClsValidacion.ValidarCodigoBarra(txtCodigoBarra.Text)) return;
            if (!ClsValidacion.ValidarServicio(txtServicio.Text)) return;

            try
            {
                ClsAgregarProducto logicaInsertar = new ClsAgregarProducto();

                string nombre = txtNombre.Text;
                int idMarca = (int)cmbMarca.SelectedValue;
                int idTipo = (int)cmbTipo.SelectedValue;
                int idModelo = (int)cmbModelo.SelectedValue;

                decimal precio = decimal.Parse(txtPrecio.Text);

                string servicio = txtServicio.Text; 
                string codBarra = txtCodigoBarra.Text;

                logicaInsertar.EjecutarInsercion(nombre, idMarca, idTipo, idModelo, precio, servicio, codBarra);

                MessageBox.Show("Producto guardado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar en el sistema: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                cmbMarca.DataSource = llenar.ObtenerDatosCombo("Marca");
                cmbMarca.DisplayMember = "nombre_marca";
                cmbMarca.ValueMember = "id_marca_producto";

                cmbTipo.DataSource = llenar.ObtenerDatosCombo("Tipo");
                cmbTipo.DisplayMember = "descripcion_forma_pago"; 
                cmbTipo.ValueMember = "id_tipo_producto";

                cmbModelo.DataSource = llenar.ObtenerDatosCombo("Modelo");
                cmbModelo.DisplayMember = "nombre_modelo_auto";
                cmbModelo.ValueMember = "id_modelo_auto";

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
