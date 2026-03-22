using Microsoft.Data.SqlClient;
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
            if (!ClsValidacion.ValidarSeleccion(cmbProveedor, "el Proveedor")) return; 
            if (!ClsValidacion.ValidarCodigoBarra(txtCodigoBarra.Text)) return;

            try
            {
                ClsAgregarProducto logicaInsertar = new ClsAgregarProducto();
                string nombre = txtNombre.Text.Trim();
                int idMarca = (int)cmbMarca.SelectedValue;
                int idProveedor = (int)cmbProveedor.SelectedValue;
                string codBarra = txtCodigoBarra.Text.Trim();

                // 1. Validar Triple Coincidencia (Nombre + Marca + Proveedor)
                if (logicaInsertar.ExisteProductoMarcaProveedor(nombre, idMarca, idProveedor))
                {
                    MessageBox.Show("Este producto con esta marca ya está registrado para el proveedor seleccionado.\n\n" +
                                    "Si es un proveedor distinto, sí puede usar el mismo nombre.",
                                    "Producto Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                // 2. Validar Código de Barras (Este sigue siendo ÚNICO en todo el sistema)
                if (logicaInsertar.ExisteCodigoBarra(codBarra))
                {
                    MessageBox.Show("El código de barras ya pertenece a otro producto en el sistema.",
                                    "Código Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtCodigoBarra.Focus();
                    return;
                }

                // 3. Si pasa ambas, insertamos
                logicaInsertar.EjecutarInsercion(nombre, idMarca, (int)cmbTipo.SelectedValue,
                                                (int)cmbModelo.SelectedValue, decimal.Parse(txtPrecio.Text),
                                                codBarra, idProveedor);

                MessageBox.Show("¡Producto guardado exitosamente!", "Éxito");
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
                llenar.ConfigurarComboBox(cmbMarca, "Marca");
                llenar.ConfigurarComboBox(cmbTipo, "Tipo");
                llenar.ConfigurarComboBox(cmbModelo, "Modelo");
                llenar.ConfigurarComboBox(cmbProveedor, "Proveedor");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
        }

        private void txtCodigoBarra_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCodigoBarra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            if (txtCodigoBarra.Text.Length >= 13 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
