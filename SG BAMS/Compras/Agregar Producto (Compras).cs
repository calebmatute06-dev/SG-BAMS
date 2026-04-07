using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class Agregar_Producto__Compras_ : Form
    {
        /// <summary>
        /// Gets or sets the identifier seleccionado.
        /// </summary>
        /// <value>
        /// The identifier seleccionado.
        /// </value>
        public string IdSeleccionado { get; set; }
        /// <summary>
        /// Gets or sets the nombre seleccionado.
        /// </summary>
        /// <value>
        /// The nombre seleccionado.
        /// </value>
        public string NombreSeleccionado { get; set; }
        /// <summary>
        /// Gets or sets the cantidad seleccionada.
        /// </summary>
        /// <value>
        /// The cantidad seleccionada.
        /// </value>
        public int CantidadSeleccionada { get; set; }
        /// <summary>
        /// Gets or sets the precio seleccionado.
        /// </summary>
        /// <value>
        /// The precio seleccionado.
        /// </value>
        public decimal PrecioSeleccionado { get; set; }

        /// <summary>
        /// The identifier proveedor
        /// </summary>
        private int _idProveedor;


        /// <summary>
        /// Initializes a new instance of the <see cref="Agregar_Producto__Compras_"/> class.
        /// </summary>
        /// <param name="idProv">The identifier prov.</param>
        public Agregar_Producto__Compras_(int idProv)
        {
            InitializeComponent();
            this._idProveedor = idProv;

        }

        /// <summary>
        /// Handles the Click event of the kryptonLabel1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void kryptonLabel1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the kryptonLabel4 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void kryptonLabel4_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Load event of the Agregar_Producto__Compras_ control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Agregar_Producto__Compras__Load(object sender, EventArgs e)
        {
            LlenarComboProductos();
            numCantidad.DecimalPlaces = 0;
            numCantidad.ThousandsSeparator = true;
        }

        /// <summary>
        /// Llenars the combo productos.
        /// </summary>
        private void LlenarComboProductos()
        {
            try
            {
                ClsCompras objCompras = new ClsCompras();
                DataTable dt = objCompras.ObtenerProductosPorProveedor(_idProveedor);

                cmbProductos.DataSource = dt;
                cmbProductos.DisplayMember = "DisplayFull";
                cmbProductos.ValueMember = "id_producto";
                cmbProductos.DropDownStyle = ComboBoxStyle.DropDown;
                cmbProductos.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbProductos.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmbProductos.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los productos: " + ex.Message);
            }
        }

        /// <summary>
        /// Handles the Click event of the kryptonButton3 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            if (cmbProductos.SelectedValue == null || cmbProductos.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un producto de la lista.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (numCantidad.Value <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor a cero.", "Cantidad Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numCantidad.Focus();
                return;
            }

            if (!ClsValidaciones.EsNumeroDecimalValido(txtPrecio, "El precio", out decimal precioAux))
            {
                return;
            }

            IdSeleccionado = cmbProductos.SelectedValue.ToString();
            NombreSeleccionado = cmbProductos.Text;
            CantidadSeleccionada = (int)numCantidad.Value;
            PrecioSeleccionado = precioAux;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the btnCancelar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the kryptonButton1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            using (AgregarProducto frmCrear = new AgregarProducto())
            {
                if (frmCrear.ShowDialog() == DialogResult.OK)
                {
                    LlenarComboProductos();
                    MessageBox.Show("¡Producto registrado! Ya puede seleccionarlo en la lista.");
                }
                else
                {
                    LlenarComboProductos();
                }
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the txtPrecio control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.PermitirNumerosYDecimales(sender, e);
        }

        /// <summary>
        /// Handles the Leave event of the txtPrecio control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txtPrecio_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtPrecio.Text))
            {
                if (decimal.TryParse(txtPrecio.Text, out decimal valor))
                {
                    txtPrecio.Text = valor.ToString("N2", CultureInfo.InvariantCulture);
                }
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the numCantidad control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void numCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.ValidarSoloNumeros(e);
        }

        /// <summary>
        /// The ultima tecla escaner
        /// </summary>
        private DateTime ultimaTeclaEscaner = DateTime.Now;

        /// <summary>
        /// Processes a command key.
        /// </summary>
        /// <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the Win32 message to process.</param>
        /// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
        /// <returns>
        ///   <see langword="true" /> if the keystroke was processed and consumed by the control; otherwise, <see langword="false" /> to allow further processing.
        /// </returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys key = keyData & Keys.KeyCode;

            if (cmbProductos.Focused || numCantidad.Focused || txtPrecio.Focused)
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }

            if ((key >= Keys.D0 && key <= Keys.Z) || (key >= Keys.NumPad0 && key <= Keys.NumPad9))
            {
                TimeSpan intervalo = DateTime.Now - ultimaTeclaEscaner;
                ultimaTeclaEscaner = DateTime.Now;

                if (intervalo.TotalMilliseconds > 100)
                {
                    txtCodigo.Text = "";
                }

                char c = (char)key;
                txtCodigo.AppendText(c.ToString().ToLower());

                return true; 
            }

            if (key == Keys.Enter)
            {
                if (!cmbProductos.Focused && !numCantidad.Focused && !txtPrecio.Focused)
                {
                    if (!string.IsNullOrWhiteSpace(txtCodigo.Text))
                    {
                        BuscarProductoPorCodigo(txtCodigo.Text.Trim());
                        return true;
                    }
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Buscars the producto por codigo.
        /// </summary>
        /// <param name="codigo">The codigo.</param>
        private void BuscarProductoPorCodigo(string codigo)
        {
            try
            {
                ClsCompras objCompras = new ClsCompras();
                DataTable dt = objCompras.ObtenerProductosPorProveedor(_idProveedor);

                bool encontrado = false;

                foreach (DataRow row in dt.Rows)
                {
                    if (row["codigo_barra"].ToString().Trim() == codigo.Trim())
                    {
                        cmbProductos.SelectedValue = row["id_producto"];
                        numCantidad.Focus(); 
                        encontrado = true;
                        break;
                    }
                }

                if (!encontrado)
                {
                    MessageBox.Show($"El código [{codigo}] no está asociado a este proveedor.", "BAMS");
                    txtCodigo.Clear();
                    txtCodigo.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en búsqueda: " + ex.Message);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnEscanear control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnEscanear_Click(object sender, EventArgs e)
        {
            txtCodigo.Clear();
            txtCodigo.Focus();
            txtCodigo.StateCommon.Back.Color1 = Color.SkyBlue;
        }
    }
}