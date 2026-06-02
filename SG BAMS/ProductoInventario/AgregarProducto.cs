using Microsoft.Data.SqlClient;
using SG_BAMS.Facturas;
using SG_BAMS.ProductoInventario;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class AgregarProducto : Form
    {
        private PlaceholderTextBox phNombre;
        private PlaceholderTextBox phPrecio;
        private PlaceholderComboBox phMarca;
        private PlaceholderComboBox phTipo;
        private PlaceholderComboBox phModelo;
        private PlaceholderTextBox phCodigoBarra;
        private PlaceholderComboBox phProveedor;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="AgregarProducto"/>.
        /// </summary>
        public AgregarProducto()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        /// <summary>
        /// Maneja el evento Click del control btnAceptar.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnAceptar_Click_1(object sender, EventArgs e)
        {
            
            string nombreReal = phNombre.GetRealValue().Trim();
            string precioReal = phPrecio.GetRealValue().Trim();
            string codigoReal = phCodigoBarra.GetRealValue().Trim();

           
            string originalNombre = txtNombre.Text;
            string originalPrecio = txtPrecio.Text;
            string originalCodigo = txtCodigoBarra.Text;

           
            txtNombre.Text = nombreReal;
            txtPrecio.Text = precioReal;
            txtCodigoBarra.Text = codigoReal;

            bool valido = true;

            
            if (string.IsNullOrWhiteSpace(nombreReal) || nombreReal.Length < 3)
            {
                MessageBox.Show("El nombre del producto debe tener al menos 3 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                valido = false;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(nombreReal, @"^[a-zA-Z0-9ñÑáéíóúÁÉÍÓÚ\s&]+$"))
            {
                MessageBox.Show("El nombre solo permite letras, números, espacios y el símbolo '&'.", "Formato Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                valido = false;
            }
            else if (!ClsValidaciones.EsAlfanumericoValido(txtNombre, "Nombre del Producto"))
                valido = false;
            else if (!ClsValidaciones.ValidarPrecio(txtPrecio.Text))
                valido = false;
            else if (phMarca.IsPlaceholderActive || cmbMarca.SelectedIndex == -1 || !ClsValidaciones.ValidarSeleccion(cmbMarca, "la Marca"))
                valido = false;
            else if (phTipo.IsPlaceholderActive || cmbTipo.SelectedIndex == -1 || !ClsValidaciones.ValidarSeleccion(cmbTipo, "el Tipo de Producto"))
                valido = false;
            else if (phModelo.IsPlaceholderActive || cmbModelo.SelectedIndex == -1 || !ClsValidaciones.ValidarSeleccion(cmbModelo, "el Modelo de Auto"))
                valido = false;
            else if (phProveedor.IsPlaceholderActive || cmbProveedor.SelectedIndex == -1 || !ClsValidaciones.ValidarSeleccion(cmbProveedor, "el Proveedor"))
                valido = false;
            else if (!ClsValidaciones.ValidarCodigoBarra(txtCodigoBarra.Text))
                valido = false;

            
            txtNombre.Text = originalNombre;
            txtPrecio.Text = originalPrecio;
            txtCodigoBarra.Text = originalCodigo;

            if (!valido) return;

            try
            {
                ClsAgregarProducto logicaInsertar = new ClsAgregarProducto();
                string nombre = nombreReal;
                int idMarca = (int)cmbMarca.SelectedValue;
                int idProveedor = (int)cmbProveedor.SelectedValue;
                string codBarra = codigoReal;
                int stockInicial = decimal.ToInt32(txtStock.Value);

                if (logicaInsertar.ExisteProductoMarcaProveedor(nombre, idMarca, idProveedor))
                {
                    MessageBox.Show("Este producto con esta marca ya está registrado para el proveedor seleccionado.\n\n" +
                                    "Si es un proveedor distinto, sí puede usar el mismo nombre.",
                                    "Producto Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                if (logicaInsertar.ExisteCodigoBarra(codBarra))
                {
                    MessageBox.Show("El código de barras ya pertenece a otro producto en el sistema.",
                                    "Código Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtCodigoBarra.Focus();
                    return;
                }

                logicaInsertar.EjecutarInsercion(
                    nombre,
                    idMarca,
                    (int)cmbTipo.SelectedValue,
                    (int)cmbModelo.SelectedValue,
                    decimal.Parse(precioReal),
                    codBarra,
                    idProveedor,
                    stockInicial
                );

                MessageBox.Show("¡Producto y stock guardados exitosamente!", "Éxito");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Maneja el evento Click del control btnCancelar.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Maneja el evento Click del control btnsalir.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Maneja el evento Load del control AgregarProducto.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void AgregarProducto_Load(object sender, EventArgs e)
        {
            LlenarTodosLosCombos();

            
            phNombre = new PlaceholderTextBox(txtNombre, "Nombre del producto");
            phPrecio = new PlaceholderTextBox(txtPrecio, "Precio del producto");
            phCodigoBarra = new PlaceholderTextBox(txtCodigoBarra, "Ingrese o escanee el código");

            phMarca = new PlaceholderComboBox(cmbMarca, "Seleccione marca");
            phTipo = new PlaceholderComboBox(cmbTipo, "Seleccione tipo");
            phModelo = new PlaceholderComboBox(cmbModelo, "Seleccione modelo");
            phProveedor = new PlaceholderComboBox(cmbProveedor, "Seleccione proveedor");
        }

        /// <summary>
        /// Llena todos los combos del formulario.
        /// </summary>
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

        /// <summary>
        /// Maneja el evento TextChanged del control txtCodigoBarra.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void txtCodigoBarra_TextChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Maneja el evento KeyPress del control txtCodigoBarra.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="KeyPressEventArgs"/> que contiene los datos del evento.</param>
        private void txtCodigoBarra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            if (txtCodigoBarra.Text.Length >= 20 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
                return;
            }

            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;

                int longitud = txtCodigoBarra.Text.Length;

                if (longitud >= 6 && longitud <= 20)
                {
                    cmbProveedor.Focus();
                }
                else
                {
                    MessageBox.Show("Código inválido. Debe tener entre 6 y 20 caracteres (letras o números).\n" +
                                    "Intenta escanear o escribir de nuevo.",
                                    "Código Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCodigoBarra.Clear();
                    txtCodigoBarra.Focus();
                }
            }
        }

        /// <summary>
        /// Maneja el evento KeyPress del control txtNombre.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="KeyPressEventArgs"/> que contiene los datos del evento.</param>
        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        /// <summary>
        /// Maneja el evento KeyPress del control txtPrecio.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="KeyPressEventArgs"/> que contiene los datos del evento.</param>
        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.PermitirNumerosYDecimales(sender, e);
        }

        private void cmbMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMarca.SelectedIndex == 0)
            {
                cmbMarca.StateCommon.ComboBox.Content.Color1 = Color.Black;
            }
        }
    }
}