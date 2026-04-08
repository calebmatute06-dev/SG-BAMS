using Microsoft.Data.SqlClient;
using SG_BAMS.Administracion_de_BAMS.FormaPago;
using SG_BAMS.Login;
using SG_BAMS.Proveedor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SG_BAMS.ClsCompras;

namespace SG_BAMS
{
    /// <summary>
    /// Proporciona la interfaz de usuario para registrar la entrada de datos de una nueva compra.
    /// Permite gestionar productos, proveedores y calcular totales dinámicamente.
    /// </summary>
    public partial class Ingresar_datos__Compra_ : Form
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Ingresar_datos__Compra_"/>.
        /// </summary>
        public Ingresar_datos__Compra_()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        /// <summary>
        /// Configura el estado inicial del formulario, incluyendo la estructura de la tabla y estilos visuales.
        /// </summary>
        private void Ingresar_datos__Compra__Load(object sender, EventArgs e)
        {

            dgvIngresarCompra.Columns.Clear();

            
            dgvIngresarCompra.Columns.Add("ID", "ID");
            dgvIngresarCompra.Columns.Add("Nombre", "Nombre");
            dgvIngresarCompra.Columns.Add("Cantidad", "Cantidad");
            dgvIngresarCompra.Columns.Add("Precio", "Precio");
            dgvIngresarCompra.Columns.Add("Subtotal", "Subtotal");

            
            dgvIngresarCompra.Columns[0].ReadOnly = true;
            dgvIngresarCompra.Columns[1].ReadOnly = true;
            dgvIngresarCompra.Columns[4].ReadOnly = true;
            dgvIngresarCompra.Columns[2].ReadOnly = false;
            dgvIngresarCompra.Columns[3].ReadOnly = false;

            LlenarCombos();
            dtpFechaPedido.SelectionStart = DateTime.Now;
            dtpFechaPedido.SelectionEnd = DateTime.Now;
            lblIDCompra.Text = ObtenerSiguienteID();

            
            dgvIngresarCompra.BorderStyle = BorderStyle.None;
            dgvIngresarCompra.BackgroundColor = Color.White;
            dgvIngresarCompra.RowHeadersVisible = false;
            dgvIngresarCompra.EnableHeadersVisualStyles = false;
            dgvIngresarCompra.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgvIngresarCompra.ColumnHeadersDefaultCellStyle.BackColor = Color.SkyBlue;
            dgvIngresarCompra.ColumnHeadersDefaultCellStyle.ForeColor = Color.Navy;
            dgvIngresarCompra.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvIngresarCompra.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvIngresarCompra.ColumnHeadersHeight = 28;

            dgvIngresarCompra.DefaultCellStyle.BackColor = Color.White;
            dgvIngresarCompra.DefaultCellStyle.ForeColor = Color.Navy;
            dgvIngresarCompra.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvIngresarCompra.DefaultCellStyle.Padding = new Padding(3);
            dgvIngresarCompra.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 245, 255);
            dgvIngresarCompra.AlternatingRowsDefaultCellStyle.ForeColor = Color.Navy;

            dgvIngresarCompra.DefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            dgvIngresarCompra.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvIngresarCompra.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvIngresarCompra.GridColor = Color.LightGray;
            dgvIngresarCompra.RowTemplate.Height = 32;
            dgvIngresarCompra.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvIngresarCompra.ClearSelection();
        }

        /// <summary>
        /// Carga los datos necesarios en los ComboBox de proveedores y formas de pago.
        /// </summary>
        private void LlenarCombos()
        {
            try
            {
                ClsCargaCombos carga = new ClsCargaCombos();

                cmbFormaPago.DataSource = carga.ListarFormasPago();
                cmbFormaPago.DisplayMember = "descripcion_forma_pago";
                cmbFormaPago.ValueMember = "id_tipo_forma_pago";

                cmbProveedor.DataSource = carga.ListarProveedoresActivos();
                cmbProveedor.DisplayMember = "nombre_proveedor";
                cmbProveedor.ValueMember = "id_proveedor";
                cmbProveedor.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbProveedor.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmbProveedor.DropDownStyle = ComboBoxStyle.DropDown;
                cmbProveedor.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los combos: " + ex.Message, "BAMS - Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Obtiene el identificador correlativo para la nueva transacción de compra.
        /// </summary>
        private string ObtenerSiguienteID()
        {
            try
            {
                ClsCargaCombos carga = new ClsCargaCombos();
                return carga.SugerirSiguienteID();
            }
            catch (Exception)
            {
                return "1";
            }
        }

        /// <summary>
        /// Calcula y actualiza el total general sumando los subtotales de cada fila.
        /// </summary>
        private void ActualizarGranTotal()
        {
            decimal granTotal = 0;
            foreach (DataGridViewRow fila in dgvIngresarCompra.Rows)
            {
                if (fila.Cells[4].Value != null)
                {
                    granTotal += Convert.ToDecimal(fila.Cells[4].Value);
                }
            }
            lblTotal.Text = granTotal.ToString("N2");
        }

        private object valorOriginal;

        /// <summary>
        /// Abre el formulario de selección de productos y añade el resultado a la tabla de compra.
        /// Valida que el proveedor esté seleccionado y que no haya duplicados.
        /// </summary>
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cmbProveedor.SelectedValue == null || cmbProveedor.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un proveedor primero para ver sus productos vinculados.",
                                "Proveedor Requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idProv = Convert.ToInt32(cmbProveedor.SelectedValue);

            using (var formularioHijo = new Agregar_Producto__Compras_(idProv))
            {
                if (formularioHijo.ShowDialog() == DialogResult.OK)
                {
                    string idNuevo = formularioHijo.IdSeleccionado.ToString();
                    bool existe = false;

                    foreach (DataGridViewRow fila in dgvIngresarCompra.Rows)
                    {
                        if (fila.Cells[0].Value != null && fila.Cells[0].Value.ToString() == idNuevo)
                        {
                            existe = true;
                            break;
                        }
                    }

                    if (existe)
                    {
                        MessageBox.Show("Este producto ya está incluido en la lista de compra. \nModifique la cantidad directamente en la tabla si lo desea.",
                                        "Producto Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    decimal subtotal = formularioHijo.CantidadSeleccionada * formularioHijo.PrecioSeleccionado;

                    dgvIngresarCompra.Rows.Add(
                        formularioHijo.IdSeleccionado,
                        formularioHijo.NombreSeleccionado,
                        formularioHijo.CantidadSeleccionada,
                        formularioHijo.PrecioSeleccionado,
                        subtotal
                    );

                    if (dgvIngresarCompra.Rows.Count > 0)
                    {
                        cmbProveedor.Enabled = false;
                    }

                    ActualizarGranTotal();
                }
            }
        }

        /// <summary>
        /// Procesa y guarda la compra final en la base de datos tras validar los campos requeridos.
        /// </summary>
        private void btnAceptar_Click_1(object sender, EventArgs e)
{
    
    int filasConDatos = 0;
    foreach (DataGridViewRow fila in dgvIngresarCompra.Rows)
    {
        if (!fila.IsNewRow && fila.Cells[0].Value != null)
        {
            filasConDatos++;
        }
    }

    if (filasConDatos == 0)
    {
        MessageBox.Show("Debe agregar al menos un producto a la lista.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
    }

    if (cmbProveedor.SelectedValue == null || cmbFormaPago.SelectedValue == null)
    {
        MessageBox.Show("Seleccione el Proveedor y la Forma de Pago.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
    }

    try
    {
        List<DetalleCompra> listaDetalles = new List<DetalleCompra>();

        foreach (DataGridViewRow fila in dgvIngresarCompra.Rows)
        {
            if (!fila.IsNewRow && fila.Cells[0].Value != null)
            {
                listaDetalles.Add(new DetalleCompra
                {
                    IdProducto = Convert.ToInt32(fila.Cells[0].Value),
                    Cantidad = Convert.ToInt32(fila.Cells[2].Value),
                    Precio = Convert.ToDecimal(fila.Cells[3].Value)
                });
            }
        }

        ClsCompras logic = new ClsCompras();

            bool exito = logic.GuardarNuevaCompra(
            dtpFechaPedido.SelectionStart,
            Convert.ToInt32(cmbFormaPago.SelectedValue),
            Convert.ToInt32(cmbProveedor.SelectedValue),
            txtNotaDetalle.Text,
            listaDetalles
                );

        if (exito)
        {
            MessageBox.Show("La compra se registró correctamente y el inventario fue actualizado.", "BAMS - Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("Error al procesar la compra: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}

        /// <summary>
        /// Cierra el formulario actual sin realizar cambios.
        /// </summary>
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Remueve el producto seleccionado de la tabla de detalles de compra.
        /// </summary>
        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (dgvIngresarCompra.CurrentRow != null && dgvIngresarCompra.CurrentRow.Index >= 0)
            {
                DialogResult respuesta = MessageBox.Show("¿Está seguro de que desea quitar este producto de la lista?",
                    "Eliminar Producto", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    dgvIngresarCompra.Rows.RemoveAt(dgvIngresarCompra.CurrentRow.Index);
                    ActualizarGranTotal();
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione el producto que desea eliminar de la tabla.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (dgvIngresarCompra.Rows.Count == 0)
            {
                cmbProveedor.Enabled = true;
            }
        }

        /// <summary>
        /// Maneja los cambios en las celdas de la tabla para validar entradas y recalcular subtotales.
        /// </summary>
        private void dgvIngresarCompra_CellValueChanged_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && (e.ColumnIndex == 2 || e.ColumnIndex == 3))
            {
                var fila = dgvIngresarCompra.Rows[e.RowIndex];
                decimal valorNuevo;
                bool esNumerico = decimal.TryParse(fila.Cells[e.ColumnIndex].Value?.ToString(), out valorNuevo);

                if (!esNumerico || valorNuevo <= 0)
                {
                    string campo = (e.ColumnIndex == 2) ? "La cantidad" : "El precio";
                    MessageBox.Show($"{campo} no puede ser cero o menor.",
                                    "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    dgvIngresarCompra.CellValueChanged -= dgvIngresarCompra_CellValueChanged_1;
                    fila.Cells[e.ColumnIndex].Value = valorOriginal;
                    dgvIngresarCompra.CellValueChanged += dgvIngresarCompra_CellValueChanged_1;
                    return;
                }

                try
                {
                    decimal cant = Convert.ToDecimal(fila.Cells[2].Value ?? 0);
                    decimal prec = Convert.ToDecimal(fila.Cells[3].Value ?? 0);

                    fila.Cells[4].Value = cant * prec;
                    ActualizarGranTotal();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al calcular el subtotal: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Captura el valor de la celda antes de ser editada para permitir reversión en caso de error.
        /// </summary>
        private void dgvIngresarCompra_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex >= 0 && (e.ColumnIndex == 2 || e.ColumnIndex == 3))
            {
                valorOriginal = dgvIngresarCompra.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            }
        }
    }
}