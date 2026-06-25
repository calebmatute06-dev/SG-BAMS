using Krypton.Toolkit;
using Microsoft.Data.SqlClient;
using SG_BAMS.ProductoInventario;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Formulario para modificar los datos de un producto existente.
    /// </summary>
    public partial class ModificarProducto : Form
    {
        /// <summary>
        /// Valores actuales de las propiedades externas (recibidas desde el listado).
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
        /// Inicializa una nueva instancia del formulario.
        /// </summary>
        public ModificarProducto()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            txtNombre.KeyPress += (s, e) => ClsValidaciones.PermitirAlfanumerico(e);
        }

        /// <summary>
        /// Maneja el evento Click del botón Aceptar.
        /// </summary>
        private void btnAceptar_Click(object sender, EventArgs e)
        {

            string nombreReal = phNombre.GetRealValue().Trim();
            string precioReal = phPrecio.GetRealValue().Trim();
            string codigoReal = phCodigoBarra.GetRealValue().Trim();

            bool valido = true;


            using (var tempNombre = new KryptonTextBox())
            {
                tempNombre.Text = nombreReal;
                if (!ClsValidaciones.EsAlfanumericoValido(tempNombre, "Nombre del Producto"))
                    valido = false;
            }


            if (valido)
            {
                string precioLimpio = precioReal.Replace("Lps", "").Replace("L.", "").Replace("$", "").Trim();
                using (var tempPrecio = new KryptonTextBox())
                {
                    tempPrecio.Text = precioLimpio;
                    if (!ClsValidaciones.ValidarPrecio(tempPrecio.Text))
                        valido = false;
                }
            }


            if (valido)
            {
                using (var tempCodigo = new KryptonTextBox())
                {
                    tempCodigo.Text = codigoReal;
                    if (!ClsValidaciones.ValidarCodigoBarra(tempCodigo.Text))
                        valido = false;
                }
            }

            if (valido && (phMarca.IsPlaceholderActive || cmbMarca.SelectedIndex == -1))
            {
                MessageBox.Show("Seleccione una marca válida.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                valido = false;
            }
            else if (valido && (phTipo.IsPlaceholderActive || cmbTipo.SelectedIndex == -1))
            {
                MessageBox.Show("Seleccione un tipo válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                valido = false;
            }
            else if (valido && (phModelo.IsPlaceholderActive || cmbModelo.SelectedIndex == -1))
            {
                MessageBox.Show("Seleccione un modelo válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                valido = false;
            }
            else if (valido && (phEstado.IsPlaceholderActive || cmbEstado.SelectedIndex == -1))
            {
                MessageBox.Show("Seleccione un estado válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                valido = false;
            }
            else if (valido && (phProveedor.IsPlaceholderActive || cmbProveedor.SelectedIndex == -1))
            {
                MessageBox.Show("Seleccione un proveedor válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                valido = false;
            }

            if (!valido) return;

            try
            {
                int idActual = Convert.ToInt32(txtID.Text);
                int idMarca = Convert.ToInt32(cmbMarca.SelectedValue);
                int idProveedor = Convert.ToInt32(cmbProveedor.SelectedValue);
                int stockNuevo = Convert.ToInt32(txtStock.Value);
                decimal precioNumerico = Convert.ToDecimal(precioReal);

                ClsActualizarProducto logica = new ClsActualizarProducto();

                if (logica.ExisteProductoEnOtros(idActual, nombreReal, idMarca, idProveedor))
                {
                    MessageBox.Show("Este producto con esta marca ya está registrado para el proveedor seleccionado.\n\n" +
                                    "Si es un proveedor distinto, sí puede usar el mismo nombre.",
                                    "Producto Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtNombre.Focus();
                    return;
                }

                if (logica.ExisteCodigoEnOtros(idActual, codigoReal))
                {
                    MessageBox.Show("El código de barras ya está asignado a otro producto.",
                                    "Código Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtCodigoBarra.Focus();
                    return;
                }

                logica.EjecutarActualizacion(
                    idActual,
                    nombreReal,
                    idMarca,
                    Convert.ToInt32(cmbTipo.SelectedValue),
                    Convert.ToInt32(cmbModelo.SelectedValue),
                    Convert.ToInt32(cmbEstado.SelectedValue),
                    precioNumerico,
                    codigoReal,
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
        /// Maneja el evento Load del formulario.
        /// </summary>
        private void ModificarProducto_Load(object sender, EventArgs e)
        {
            LlenarCombosModificar();

            cmbMarca.SelectedIndex = cmbMarca.FindStringExact(marcaActual?.Trim());
            cmbTipo.SelectedIndex = cmbTipo.FindStringExact(tipoActual?.Trim());
            cmbModelo.SelectedIndex = cmbModelo.FindStringExact(modeloActual?.Trim());
            cmbEstado.SelectedIndex = cmbEstado.FindStringExact(estadoActual?.Trim());
            cmbProveedor.SelectedIndex = cmbProveedor.FindStringExact(proveedorActual?.Trim());


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
        /// Llena los ComboBox con los datos necesarios.
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

                int idProvActual = ObtenerIdProveedorPorNombre(proveedorActual);
                llenar.ConfigurarComboBox(cmbProveedor, "Proveedor", idProvActual);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
        }

        private int ObtenerIdProveedorPorNombre(string nombreProv)
        {
            if (string.IsNullOrWhiteSpace(nombreProv)) return 0;

            int idFound = 0;
            ClsConexion conexionTemp = new ClsConexion();
            string query = "SELECT id_proveedor FROM Proveedor WHERE nombre_proveedor = @nombre";

            try
            {
                conexionTemp.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand(query, conexionTemp.Conectar))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombreProv.Trim());
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        idFound = Convert.ToInt32(result);
                    }
                }
            }
            catch
            {
                return 0;
            }
            finally
            {
                conexionTemp.Cerrar();
            }

            return idFound;
        }

        /// <summary>
        /// Maneja el evento Click del botón Cancelar.
        /// </summary>
        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Maneja el evento KeyPress del campo Código de Barra.
        /// </summary>
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
        /// Maneja el evento KeyPress del campo Precio.
        /// </summary>
        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.PermitirNumerosYDecimales(sender, e);
        }

        /// <summary>
        /// Eventos vacíos requeridos por el diseñador.
        /// </summary>
        private void label2_Click(object sender, EventArgs e) { }
        private void txtCodigoBarra_TextChanged(object sender, EventArgs e) { }
    }
}