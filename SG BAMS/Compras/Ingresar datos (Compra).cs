using Microsoft.Data.SqlClient;
using SG_BAMS.Administracion_de_BAMS.FormaPago;
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
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class Ingresar_datos__Compra_ : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Ingresar_datos__Compra_"/> class.
        /// </summary>
        public Ingresar_datos__Compra_()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Load event of the Ingresar_datos__Compra_ control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Ingresar_datos__Compra__Load(object sender, EventArgs e)
        {
            dgvProductosCompra.Columns[0].ReadOnly = true;
            dgvProductosCompra.Columns[1].ReadOnly = true;
            dgvProductosCompra.Columns[4].ReadOnly = true;
            dgvProductosCompra.Columns[2].ReadOnly = false;
            dgvProductosCompra.Columns[3].ReadOnly = false;

            LlenarCombos();
            dtpFechaPedido.SelectionStart = DateTime.Now;
            dtpFechaPedido.SelectionEnd = DateTime.Now;
            lblIDCompra.Text = ObtenerSiguienteID();
        }

        /// <summary>
        /// Llenars the combos.
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
        /// Obteners the siguiente identifier.
        /// </summary>
        /// <returns></returns>
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
        /// Actualizars the gran total.
        /// </summary>
        private void ActualizarGranTotal()
        {
            decimal granTotal = 0;
            foreach (DataGridViewRow fila in dgvProductosCompra.Rows)
            {
                if (fila.Cells[4].Value != null)
                {
                    granTotal += Convert.ToDecimal(fila.Cells[4].Value);
                }
            }
            lblTotal.Text = granTotal.ToString("N2");
        }

        /// <summary>
        /// The valor original
        /// </summary>
        private object valorOriginal;

        /// <summary>
        /// Handles the CellBeginEdit event of the dgvProductosCompra control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellCancelEventArgs"/> instance containing the event data.</param>
        private void dgvProductosCompra_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex >= 0 && (e.ColumnIndex == 2 || e.ColumnIndex == 3))
            {
                valorOriginal = dgvProductosCompra.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            }
        }

        /// <summary>
        /// Handles the CellValueChanged event of the dgvProductosCompra control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dgvProductosCompra_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && (e.ColumnIndex == 2 || e.ColumnIndex == 3))
            {
                var fila = dgvProductosCompra.Rows[e.RowIndex];
                decimal valorNuevo;
                bool esNumerico = decimal.TryParse(fila.Cells[e.ColumnIndex].Value?.ToString(), out valorNuevo);

                if (!esNumerico || valorNuevo <= 0)
                {
                    string campo = (e.ColumnIndex == 2) ? "La cantidad" : "El precio";
                    MessageBox.Show($"{campo} no puede ser cero o menor.",
                                    "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    dgvProductosCompra.CellValueChanged -= dgvProductosCompra_CellValueChanged;
                    fila.Cells[e.ColumnIndex].Value = valorOriginal;
                    dgvProductosCompra.CellValueChanged += dgvProductosCompra_CellValueChanged;
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
        /// Handles the CellDoubleClick event of the dgvProductosCompra control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dgvProductosCompra_CellDoubleClick(object sender, DataGridViewCellEventArgs e) { }

        /// <summary>
        /// Handles the Click event of the btnAgregar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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

                    foreach (DataGridViewRow fila in dgvProductosCompra.Rows)
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

                    dgvProductosCompra.Rows.Add(
                        formularioHijo.IdSeleccionado,
                        formularioHijo.NombreSeleccionado,
                        formularioHijo.CantidadSeleccionada,
                        formularioHijo.PrecioSeleccionado,
                        subtotal
                    );

                    if (dgvProductosCompra.Rows.Count > 0)
                    {
                        cmbProveedor.Enabled = false;
                    }

                    ActualizarGranTotal();
                }
            }
        }

        /// <summary>
        /// Handles the 1 event of the btnAceptar_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnAceptar_Click_1(object sender, EventArgs e)
        {
            if (dgvProductosCompra.Rows.Count == 0)
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

                foreach (DataGridViewRow fila in dgvProductosCompra.Rows)
                {
                    if (fila.Cells[0].Value != null)
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
                    1,
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
        /// Handles the Click event of the btnCancelar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the btnQuitar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (dgvProductosCompra.CurrentRow != null && dgvProductosCompra.CurrentRow.Index >= 0)
            {
                DialogResult respuesta = MessageBox.Show("¿Está seguro de que desea quitar este producto de la lista?",
                    "Eliminar Producto", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    dgvProductosCompra.Rows.RemoveAt(dgvProductosCompra.CurrentRow.Index);
                    ActualizarGranTotal();
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione el producto que desea eliminar de la tabla.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (dgvProductosCompra.Rows.Count == 0)
            {
                cmbProveedor.Enabled = true;
            }
        }
    }
}