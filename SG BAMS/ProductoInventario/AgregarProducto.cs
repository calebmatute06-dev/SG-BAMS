using Krypton.Toolkit;
using SG_BAMS.ProductoInventario;
using SG_BAMS.ProductoInventario.DTO;
using System;
using System.Drawing;
using System.Windows.Forms;
using SG_BAMS.AccesoDatos;
using SG_BAMS.Dominio;
using SG_BAMS.LogicaNegocio;

namespace SG_BAMS
{
    /// <summary>
    /// Formulario para agregar un nuevo producto al inventario.
    /// Única responsabilidad de esta clase: coordinar la interacción con el usuario.
    /// Toda la validación de formato delega en ClsValidaciones y todo el acceso a datos
    /// delega en IProductoRepository / IComboRepository, recibidos por inyección
    /// (ver auditoría SOLID, hallazgos AGP01-AGP04).
    /// </summary>
    public partial class AgregarProducto : Form
    {
        private readonly IProductoRepository productoRepositorio;
        private readonly IComboRepository comboRepositorio;

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
        /// <param name="productoRepositorio">Acceso a datos de productos.</param>
        /// <param name="comboRepositorio">Acceso a datos de los catálogos de combo.</param>
        public AgregarProducto(IProductoRepository productoRepositorio, IComboRepository comboRepositorio)
        {
            InitializeComponent();
            this.productoRepositorio = productoRepositorio;
            this.comboRepositorio = comboRepositorio;

            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        /// <summary>
        /// Maneja el evento Click del botón Aceptar.
        /// Orquesta los tres pasos independientes: validar formato, validar duplicados y guardar.
        /// </summary>
        private void btnAceptar_Click_1(object sender, EventArgs e)
        {
            if (!ValidarFormulario(out ProductoDTO productoDTO))
                return;

            if (!ValidarDuplicados(productoDTO))
                return;

            GuardarProducto(productoDTO);
        }

        /// <summary>
        /// Valida el formato de todos los campos del formulario y arma el DTO si son válidos.
        /// Única responsabilidad: validación de formato (delega en ClsValidaciones).
        /// </summary>
        private bool ValidarFormulario(out ProductoDTO productoDTO)
        {
            productoDTO = null;

            if (!ValidarNombreProducto())
                return false;

            if (!ClsValidaciones.ValidarPrecio(txtPrecio, "Precio"))
                return false;

            if (!ClsValidaciones.ValidarCodigoBarra(txtCodigoBarra))
                return false;

            if (!ValidarComboSeleccionado(cmbMarca, phMarca, "la Marca"))
                return false;

            if (!ValidarComboSeleccionado(cmbTipo, phTipo, "el Tipo de Producto"))
                return false;

            if (!ValidarComboSeleccionado(cmbModelo, phModelo, "el Modelo de Auto"))
                return false;

            if (!ValidarComboSeleccionado(cmbProveedor, phProveedor, "el Proveedor"))
                return false;

            productoDTO = new ProductoDTO
            {
                Nombre = phNombre.GetRealValue().Trim(),
                IdMarca = (int)cmbMarca.SelectedValue,
                IdTipo = (int)cmbTipo.SelectedValue,
                IdModelo = (int)cmbModelo.SelectedValue,
                Precio = decimal.Parse(phPrecio.GetRealValue().Trim()),
                CodigoBarra = phCodigoBarra.GetRealValue().Trim(),
                IdProveedor = (int)cmbProveedor.SelectedValue,
                Stock = decimal.ToInt32(txtStock.Value)
            };

            return true;
        }

        /// <summary>
        /// Valida que un combo tenga una selección real (no el placeholder ni -1).
        /// Muestra un mensaje claro indicando qué campo falta seleccionar.
        /// </summary>
        private bool ValidarComboSeleccionado(Krypton.Toolkit.KryptonComboBox cmb, PlaceholderComboBox placeholder, string nombreCampo)
        {
            if (placeholder.IsPlaceholderActive || cmb.SelectedIndex == -1)
            {
                MessageBox.Show($"Debe seleccionar {nombreCampo} antes de continuar.",
                                "Campo requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmb.Focus();
                return false;
            }

            return ClsValidaciones.ValidarSeleccion(cmb, nombreCampo);
        }

        /// <summary>
        /// Valida el nombre del producto usando el valor real (sin placeholder).
        /// </summary>
        private bool ValidarNombreProducto()
        {
            if (phNombre.IsPlaceholderActive || string.IsNullOrWhiteSpace(phNombre.GetRealValue()))
            {
                MessageBox.Show("El campo 'Nombre del Producto' es obligatorio.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            string nombreReal = phNombre.GetRealValue().Trim();

            using (var tempNombre = new KryptonTextBox())
            {
                tempNombre.Text = nombreReal;
                return ClsValidaciones.EsAlfanumericoValido(tempNombre, "Nombre del Producto", minLength: 3, maxLength: 100);
            }
        }

        /// <summary>
        /// Verifica que el producto y el código de barra no estén duplicados.
        /// Única responsabilidad: reglas de duplicado (delega la consulta en IProductoRepository).
        /// </summary>
        private bool ValidarDuplicados(ProductoDTO productoDTO)
        {
            if (productoRepositorio.ExisteProductoDuplicado(productoDTO.Nombre, productoDTO.IdMarca, productoDTO.IdProveedor))
            {
                MessageBox.Show("Este producto con esta marca ya está registrado para el proveedor seleccionado.\n\n" +
                                "Si es un proveedor distinto, sí puede usar el mismo nombre.",
                                "Producto Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (productoRepositorio.ExisteCodigoBarraDuplicado(productoDTO.CodigoBarra))
            {
                MessageBox.Show("El código de barras ya pertenece a otro producto en el sistema.",
                                "Código Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtCodigoBarra.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Persiste el producto ya validado. Única responsabilidad: guardar y cerrar el formulario.
        /// </summary>
        private void GuardarProducto(ProductoDTO productoDTO)
        {
            try
            {
                productoRepositorio.EjecutarInsercion(productoDTO);

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
        /// Maneja el evento Click del botón Cancelar.
        /// </summary>
        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Maneja el evento Click del botón Salir.
        /// </summary>
        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Maneja el evento Load del formulario.
        /// </summary>
        private void AgregarProducto_Load(object sender, EventArgs e)
        {
            phNombre = new PlaceholderTextBox(txtNombre, "Ingrese elNombre del producto");
            phPrecio = new PlaceholderTextBox(txtPrecio, "Ingrese Precio del producto");
            phCodigoBarra = new PlaceholderTextBox(txtCodigoBarra, "Ingrese o escanee el código");

            phMarca = new PlaceholderComboBox(cmbMarca, "Seleccione marca");
            phTipo = new PlaceholderComboBox(cmbTipo, "Seleccione tipo");
            phModelo = new PlaceholderComboBox(cmbModelo, "Seleccione modelo");
            phProveedor = new PlaceholderComboBox(cmbProveedor, "Seleccione proveedor");

            LlenarTodosLosCombos();

            // Forzar placeholder después del bind
            phMarca.Activar();
            phTipo.Activar();
            phModelo.Activar();
            phProveedor.Activar();
        }

        /// <summary>
        /// Llena todos los ComboBox del formulario delegando en ComboBoxConfigurator + IComboRepository.
        /// </summary>
        private void LlenarTodosLosCombos()
        {
            try
            {
                ComboBoxConfigurator.Configurar(cmbMarca, comboRepositorio, "Marca");
                ComboBoxConfigurator.Configurar(cmbTipo, comboRepositorio, "Tipo");
                ComboBoxConfigurator.Configurar(cmbModelo, comboRepositorio, "Modelo");
                ComboBoxConfigurator.Configurar(cmbProveedor, comboRepositorio, "Proveedor");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
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

        private void cmbMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMarca.SelectedIndex == 0)
            {
                cmbMarca.StateCommon.ComboBox.Content.Color1 = Color.Black;
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e) { }
        private void txtCodigoBarra_TextChanged(object sender, EventArgs e) { }
    }
}