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
            try
            {
                ClsLLenarCombo logica = new ClsLLenarCombo();

                // --- CARGAR MARCAS ---
                cmbMarca.DataSource = logica.LlenarMarca();
                cmbMarca.DisplayMember = "nombre_marca";       // Lo que ve el usuario
                cmbMarca.ValueMember = "id_marca_producto";    // El ID que se guarda
                cmbMarca.SelectedIndex = -1;                   // Inicia vacío

                // --- CARGAR TIPOS ---
                cmbTipo.DataSource = logica.LlenarTipo();
                cmbTipo.DisplayMember = "descripcion_forma_pago"; // Nombre exacto en tu SQL
                cmbTipo.ValueMember = "id_tipo_producto";
                cmbTipo.SelectedIndex = -1;

                // --- CARGAR MODELOS ---
                cmbModelo.DataSource = logica.LlenarModelo();
                cmbModelo.DisplayMember = "nombre_modelo_auto";
                cmbModelo.ValueMember = "id_modelo_auto";
                cmbModelo.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al inicializar el formulario: " + ex.Message, "Sistema BAMS");
            }
        }

        

    }
}
