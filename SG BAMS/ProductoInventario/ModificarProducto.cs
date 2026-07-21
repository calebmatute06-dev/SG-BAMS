using Krypton.Toolkit;
using Microsoft.Data.SqlClient;
using SG_BAMS.ProductoInventario;
using SG_BAMS.ProductoInventario.DTO;
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
        /// Datos del producto a modificar, recibidos desde el listado (InventarioAdmin/InventarioEmp).
        /// Reemplaza los campos públicos sueltos (marcaActual, tipoActual, etc.) que se llenaban
        /// desde afuera después de crear el formulario.
        /// </summary>
        private readonly IProductoRepository _productoRepositorio;
        private readonly IComboRepository _comboRepositorio;
        private readonly ProductoDTO _dto;

        private PlaceholderTextBox phNombre;
        private PlaceholderTextBox phPrecio;
        private PlaceholderTextBox phCodigoBarra;
        private PlaceholderComboBox phMarca;
        private PlaceholderComboBox phTipo;
        private PlaceholderComboBox phModelo;
        private PlaceholderComboBox phEstado;
        private PlaceholderComboBox phProveedor;

        /// <summary>
        /// Inicializa el formulario con los datos del producto que se va a modificar,
        /// recibiendo sus dependencias de acceso a datos por inyección
        /// (ver auditoría SOLID, hallazgo relacionado con ClsProducto/ClsLlenarCombo).
        /// </summary>
        /// <param name="productoRepositorio">Acceso a datos de productos.</param>
        /// <param name="comboRepositorio">Acceso a datos de los catálogos de combo.</param>
        /// <param name="dto">Datos del producto seleccionado en el listado.</param>
        public ModificarProducto(IProductoRepository productoRepositorio, IComboRepository comboRepositorio, ProductoDTO dto)
        {
            InitializeComponent();
            _productoRepositorio = productoRepositorio;
            _comboRepositorio = comboRepositorio;
            _dto = dto;

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

                if (_productoRepositorio.ExisteProductoDuplicado(nombreReal, idMarca, idProveedor, idActual))
                {
                    MessageBox.Show("Este producto con esta marca ya está registrado para el proveedor seleccionado.\n\n" +
                                    "Si es un proveedor distinto, sí puede usar el mismo nombre.",
                                    "Producto Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtNombre.Focus();
                    return;
                }

                if (_productoRepositorio.ExisteCodigoBarraDuplicado(codigoReal, idActual))
                {
                    MessageBox.Show("El código de barras ya está asignado a otro producto.",
                                    "Código Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtCodigoBarra.Focus();
                    return;
                }

                ProductoDTO dtoActualizado = new ProductoDTO
                {
                    IdProducto = idActual,
                    Nombre = nombreReal,
                    IdMarca = idMarca,
                    IdTipo = Convert.ToInt32(cmbTipo.SelectedValue),
                    IdModelo = Convert.ToInt32(cmbModelo.SelectedValue),
                    IdEstado = Convert.ToInt32(cmbEstado.SelectedValue),
                    Precio = precioNumerico,
                    CodigoBarra = codigoReal,
                    IdProveedor = idProveedor,
                    Stock = stockNuevo
                };

                _productoRepositorio.EjecutarActualizacion(dtoActualizado);

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
            if (_dto != null)
            {
                txtID.Text = _dto.IdProducto.ToString();
                txtNombre.Text = _dto.Nombre;
                txtPrecio.Text = _dto.Precio.ToString();
                txtCodigoBarra.Text = _dto.CodigoBarra;
                txtStock.Value = _dto.Stock;
            }

            LlenarCombosModificar();

            cmbMarca.SelectedIndex = cmbMarca.FindStringExact(_dto?.MarcaActual?.Trim());
            cmbTipo.SelectedIndex = cmbTipo.FindStringExact(_dto?.TipoActual?.Trim());
            cmbModelo.SelectedIndex = cmbModelo.FindStringExact(_dto?.ModeloActual?.Trim());
            cmbEstado.SelectedIndex = cmbEstado.FindStringExact(_dto?.EstadoActual?.Trim());
            cmbProveedor.SelectedIndex = cmbProveedor.FindStringExact(_dto?.ProveedorActual?.Trim());


            phNombre = new PlaceholderTextBox(txtNombre, "Ingrese Nombre del producto");
            phPrecio = new PlaceholderTextBox(txtPrecio, "Ingrese Precio del producto");
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
            try
            {
                ComboBoxConfigurator.Configurar(cmbMarca, _comboRepositorio, "Marca");
                ComboBoxConfigurator.Configurar(cmbTipo, _comboRepositorio, "Tipo");
                ComboBoxConfigurator.Configurar(cmbModelo, _comboRepositorio, "Modelo");
                ComboBoxConfigurator.Configurar(cmbEstado, _comboRepositorio, "Estado");

                int idProvActual = ObtenerIdProveedorPorNombre(_dto?.ProveedorActual);
                ComboBoxConfigurator.Configurar(cmbProveedor, _comboRepositorio, "Proveedor", idProvActual);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
        }

        private int ObtenerIdProveedorPorNombre(string nombreProv)
        {
            if (string.IsNullOrWhiteSpace(nombreProv)) return 0;

            ClsRepositorioBaseDatos conexionTemp = new ClsRepositorioBaseDatos();
            try
            {
                conexionTemp.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Proveedor_ObtenerIdPorNombre", conexionTemp.Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", nombreProv.Trim());
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                        return Convert.ToInt32(result);
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
            return 0;
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
            ClsValidaciones.ForzarCodigoBarraKeyPress(e);

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