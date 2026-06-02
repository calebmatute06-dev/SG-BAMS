using Microsoft.Data.SqlClient;
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
    public partial class ModificarProducto : Form
    {
        /// <summary>
        /// La marca actual
        /// </summary>
        public string marcaActual, tipoActual, modeloActual, estadoActual, proveedorActual;

        private PlaceholderTextBox phNombre;
        private PlaceholderTextBox phPrecio;
        private PlaceholderTextBox phCodigoBarra;
        private PlaceholderComboBox phMarca;
        private PlaceholderComboBox phTipo;
        private PlaceholderComboBox phModelo;
        private PlaceholderComboBox phEstado;
        private PlaceholderComboBox phProveedor;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ModificarProducto" />.
        /// </summary>
        public ModificarProducto()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            txtNombre.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
        }

        /// <summary>
        /// Maneja el evento Click del control kryptonButton20.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Obtener valores reales (sin placeholder)
            string nombreReal = phNombre.GetRealValue().Trim();
            string precioReal = phPrecio.GetRealValue().Trim();
            string codigoReal = phCodigoBarra.GetRealValue().Trim();

            // Guardar textos originales
            string originalNombre = txtNombre.Text;
            string originalPrecio = txtPrecio.Text;
            string originalCodigo = txtCodigoBarra.Text;

            // Asignar valores reales temporalmente para que ClsValidaciones funcione
            txtNombre.Text = nombreReal;
            txtPrecio.Text = precioReal;
            txtCodigoBarra.Text = codigoReal;

            bool valido = true;

            // Validaciones usando ClsValidaciones
            if (!ClsValidaciones.EsAlfanumericoValido(txtNombre, "Nombre del Producto"))
                valido = false;
            else
            {
                string precioLimpio = txtPrecio.Text.Replace("Lps", "").Replace("L.", "").Replace("$", "").Trim();
                if (!ClsValidaciones.ValidarPrecio(precioLimpio))
                    valido = false;
                else if (!ClsValidaciones.ValidarCodigoBarra(txtCodigoBarra.Text))
                    valido = false;
                else if (phMarca.IsPlaceholderActive || cmbMarca.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione una marca válida.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    valido = false;
                }
                else if (phTipo.IsPlaceholderActive || cmbTipo.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione un tipo válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    valido = false;
                }
                else if (phModelo.IsPlaceholderActive || cmbModelo.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione un modelo válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    valido = false;
                }
                else if (phEstado.IsPlaceholderActive || cmbEstado.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione un estado válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    valido = false;
                }
                else if (phProveedor.IsPlaceholderActive || cmbProveedor.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione un proveedor válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    valido = false;
                }
            }

            // Restaurar textos originales
            txtNombre.Text = originalNombre;
            txtPrecio.Text = originalPrecio;
            txtCodigoBarra.Text = originalCodigo;

            if (!valido) return;

            try
            {
                int idActual = Convert.ToInt32(txtID.Text);
                string nombreNuevo = nombreReal;
                string codigoNuevo = codigoReal;
                int idMarca = Convert.ToInt32(cmbMarca.SelectedValue);
                int idProveedor = Convert.ToInt32(cmbProveedor.SelectedValue);
                int stockNuevo = Convert.ToInt32(txtStock.Value);
                decimal precioNumerico = Convert.ToDecimal(precioReal);

                ClsActualizarProducto logica = new ClsActualizarProducto();

                if (logica.ExisteProductoEnOtros(idActual, nombreNuevo, idMarca, idProveedor))
                {
                    MessageBox.Show("Este producto con esta marca ya está registrado para el proveedor seleccionado.\n\n" +
                                    "Si es un proveedor distinto, sí puede usar el mismo nombre.",
                                    "Producto Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtNombre.Focus();
                    return;
                }

                if (logica.ExisteCodigoEnOtros(idActual, codigoNuevo))
                {
                    MessageBox.Show("El código de barras ya está asignado a otro producto.",
                                    "Código Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtCodigoBarra.Focus();
                    return;
                }

                logica.EjecutarActualizacion(
                    idActual,
                    nombreNuevo,
                    idMarca,
                    Convert.ToInt32(cmbTipo.SelectedValue),
                    Convert.ToInt32(cmbModelo.SelectedValue),
                    Convert.ToInt32(cmbEstado.SelectedValue),
                    precioNumerico,
                    codigoNuevo,
                    idProveedor,
                    stockNuevo
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

        /// <summary>
        /// Maneja el evento Load del control ModificarProducto.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void ModificarProducto_Load(object sender, EventArgs e)
        {
            LlenarCombosModificar();

            cmbMarca.SelectedIndex = cmbMarca.FindStringExact(marcaActual?.Trim());
            cmbTipo.SelectedIndex = cmbTipo.FindStringExact(tipoActual?.Trim());
            cmbModelo.SelectedIndex = cmbModelo.FindStringExact(modeloActual?.Trim());
            cmbEstado.SelectedIndex = cmbEstado.FindStringExact(estadoActual?.Trim());
            cmbProveedor.SelectedIndex = cmbProveedor.FindStringExact(proveedorActual?.Trim());

            // Inicializar placeholders después de cargar los combos y asignar valores actuales
            phNombre = new PlaceholderTextBox(txtNombre, "Nombre del producto");
            phPrecio = new PlaceholderTextBox(txtPrecio, "Precio del producto");
            phCodigoBarra = new PlaceholderTextBox(txtCodigoBarra, "Ingrese o escanee el código");

            phMarca = new PlaceholderComboBox(cmbMarca, "Seleccione marca");
            phTipo = new PlaceholderComboBox(cmbTipo, "Seleccione tipo");
            phModelo = new PlaceholderComboBox(cmbModelo, "Seleccione modelo");
            phEstado = new PlaceholderComboBox(cmbEstado, "Seleccione estado");
            phProveedor = new PlaceholderComboBox(cmbProveedor, "Seleccione proveedor");
        }

        /// <summary>
        /// Llena los combos para modificar.
        /// </summary>
        public void LlenarCombosModificar()
        {
            ClsLlenarCombo llenar = new ClsLlenarCombo();
            try
            {
                llenar.ConfigurarComboBox(cmbMarca, "Marca");
                llenar.ConfigurarComboBox(cmbTipo, "Tipo");
                llenar.ConfigurarComboBox(cmbModelo, "Modelo");
                llenar.ConfigurarComboBox(cmbEstado, "Estado");
                llenar.ConfigurarComboBox(cmbProveedor, "Proveedor");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
        }

        /// <summary>
        /// Maneja el evento Click del control btnCancelar.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Maneja el evento TextChanged del control txtCodigoBarra.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void txtCodigoBarra_TextChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Maneja el evento KeyPress del control txtCodigoBarra.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="KeyPressEventArgs" /> que contiene los datos del evento.</param>
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
        /// Maneja el evento Click del control label2.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void label2_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Maneja el evento KeyPress del control txtPrecio.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="KeyPressEventArgs" /> que contiene los datos del evento.</param>
        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.PermitirNumerosYDecimales(sender, e);
        }
    }
}