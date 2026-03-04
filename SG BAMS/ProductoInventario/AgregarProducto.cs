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
            try
            {
                ClsAgregarProducto logica = new ClsAgregarProducto();

                logica.EjecutarInsercion(
                    txtNombre.Text,
                    Convert.ToInt32(cmbMarca.SelectedValue),
                    Convert.ToInt32(cmbTipo.SelectedValue),
                    Convert.ToInt32(cmbModelo.SelectedValue),
                    Convert.ToDecimal(txtPrecio.Text),
                    txtTipoServicio.Text,
                    txtCodBarra.Text
                );

                MessageBox.Show("Producto registrado con éxito.", "Sistema BAMS");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {

        }

        private void AgregarProducto_Load(object sender, EventArgs e)
        {
            LlenarTodosLosCombos();
        }

        private void LlenarTodosLosCombos()
        {
            ClsLLenarCombo logica = new ClsLLenarCombo();

            try
            {
                // LLENAR MARCAS
                DataTable dtMarcas = logica.GetMarcas();
                cmbMarca.ValueMember = "id_marca_producto";    // Nombre exacto en SQL
                cmbMarca.DisplayMember = "nombre_marca";       // Nombre exacto en SQL
                cmbMarca.DataSource = dtMarcas;

                // LLENAR TIPOS
                DataTable dtTipos = logica.GetTipos();
                cmbTipo.ValueMember = "id_tipo_producto";      // Nombre exacto en SQL
                cmbTipo.DisplayMember = "descripcion_forma_pago"; // Nombre exacto en SQL (según tu script)
                cmbTipo.DataSource = dtTipos;

                // LLENAR MODELOS
                DataTable dtModelos = logica.GetModelos();
                cmbModelo.ValueMember = "id_modelo_auto";       // Nombre exacto en SQL
                cmbModelo.DisplayMember = "nombre_modelo_auto"; // Nombre exacto en SQL
                cmbModelo.DataSource = dtModelos;

                // Resetear selección para que no aparezca el primero marcado por defecto
                cmbMarca.SelectedIndex = -1;
                cmbTipo.SelectedIndex = -1;
                cmbModelo.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("¡Ocurrió un error! Revisa esto: " + ex.Message, "Error de Carga");
            }
        }

    }
}
