using Microsoft.Data.SqlClient;
using SG_BAMS.ProductoInventario;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class Modificar_datos__Compra_ : Form
    {
        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbProveedor control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbProveedor_SelectedIndexChanged(object sender, EventArgs e) { huboCambios = true; }
        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbFormaPago control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbFormaPago_SelectedIndexChanged(object sender, EventArgs e) { huboCambios = true; }
        /// <summary>
        /// The dt respaldo
        /// </summary>
        private DataTable dtRespaldo;
        /// <summary>
        /// The hubo cambios
        /// </summary>
        private bool huboCambios = false;
        /// <summary>
        /// The valor antes de cambio
        /// </summary>
        private object valorAntesDeCambio;
        /// <summary>
        /// The identifier compra a editar
        /// </summary>
        private int idCompraAEditar;
        /// <summary>
        /// The logic
        /// </summary>
        private ClsModificarCompras logic = new ClsModificarCompras();
        /// <summary>
        /// The lista eliminados
        /// </summary>
        private List<int> listaEliminados = new List<int>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Modificar_datos__Compra_"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public Modificar_datos__Compra_(int id)
        {
            InitializeComponent();
            this.idCompraAEditar = id;
            dgvProductosCompraMod.CellValueChanged += dgvProductosCompraMod_CellValueChanged;
            dgvProductosCompraMod.CurrentCellDirtyStateChanged += dgvProductosCompraMod_CurrentCellDirtyStateChanged;
            dgvProductosCompraMod.CellBeginEdit += dgvProductosCompraMod_CellBeginEdit;
        }

        /// <summary>
        /// Handles the Load event of the Modificar_datos__Compra_ control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Modificar_datos__Compra__Load(object sender, EventArgs e)
        {
            cmbProveedor.Enabled = false;
            cmbProveedor.BackColor = Color.LightGray;

            cmbProveedor.SelectedIndexChanged -= cmbProveedor_SelectedIndexChanged;
            cmbFormaPago.SelectedIndexChanged -= cmbFormaPago_SelectedIndexChanged;

            LlenarCombos();

            DataTable dtOriginal = logic.ObtenerDetalleCompra(idCompraAEditar);
            dgvProductosCompraMod.DataSource = dtOriginal;

            if (dtOriginal != null)
            {
                dtRespaldo = dtOriginal.Copy();
            }

            ConfigurarEdicionGrid();
            CargarDatosCabecera();
            ActualizarTotalGeneral();

            huboCambios = false;
            cmbProveedor.SelectedIndexChanged += cmbProveedor_SelectedIndexChanged;
            cmbFormaPago.SelectedIndexChanged += cmbFormaPago_SelectedIndexChanged;
        }

        /// <summary>
        /// Handles the CurrentCellDirtyStateChanged event of the dgvProductosCompraMod control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void dgvProductosCompraMod_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvProductosCompraMod.IsCurrentCellDirty)
            {
                dgvProductosCompraMod.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        /// <summary>
        /// Handles the CellValueChanged event of the dgvProductosCompraMod control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dgvProductosCompraMod_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && (dgvProductosCompraMod.Columns[e.ColumnIndex].Name == "Cantidad" ||
                                    dgvProductosCompraMod.Columns[e.ColumnIndex].Name == "Precio"))
            {
                try
                {
                    decimal cantidad = Convert.ToDecimal(dgvProductosCompraMod.Rows[e.RowIndex].Cells["Cantidad"].Value ?? 0);
                    decimal precio = Convert.ToDecimal(dgvProductosCompraMod.Rows[e.RowIndex].Cells["Precio"].Value ?? 0);
                    dgvProductosCompraMod.Rows[e.RowIndex].Cells["Subtotal"].Value = cantidad * precio;
                    ActualizarTotalGeneral();
                }
                catch { }
            }
        }

        /// <summary>
        /// Actualizars the total general.
        /// </summary>
        private void ActualizarTotalGeneral()
        {
            decimal total = 0;
            foreach (DataGridViewRow fila in dgvProductosCompraMod.Rows)
            {
                if (fila.Cells["Subtotal"].Value != null)
                    total += Convert.ToDecimal(fila.Cells["Subtotal"].Value);
            }
            lblTotal.Text = "Total: L " + total.ToString("N2");
        }

        /// <summary>
        /// Configurars the edicion grid.
        /// </summary>
        private void ConfigurarEdicionGrid()
        {
            if (dgvProductosCompraMod.Columns.Contains("ID")) dgvProductosCompraMod.Columns["ID"].ReadOnly = true;
            if (dgvProductosCompraMod.Columns.Contains("Producto")) dgvProductosCompraMod.Columns["Producto"].ReadOnly = true;
            if (dgvProductosCompraMod.Columns.Contains("Subtotal")) dgvProductosCompraMod.Columns["Subtotal"].ReadOnly = true;
            if (dgvProductosCompraMod.Columns.Contains("Cantidad")) dgvProductosCompraMod.Columns["Cantidad"].ReadOnly = false;
            if (dgvProductosCompraMod.Columns.Contains("Precio")) dgvProductosCompraMod.Columns["Precio"].ReadOnly = false;
            if (dgvProductosCompraMod.Columns.Contains("Precio"))
                dgvProductosCompraMod.Columns["Precio"].DefaultCellStyle.Format = "N2";
            if (dgvProductosCompraMod.Columns.Contains("Subtotal"))
                dgvProductosCompraMod.Columns["Subtotal"].DefaultCellStyle.Format = "N2";
        }

        /// <summary>
        /// Llenars the combos.
        /// </summary>
        private void LlenarCombos()
        {
            try
            {
                cmbFormaPago.DataSource = logic.ListarFormasPago();
                cmbFormaPago.DisplayMember = "descripcion_forma_pago";
                cmbFormaPago.ValueMember = "id_tipo_forma_pago";
                cmbProveedor.DataSource = logic.ListarProveedoresActivos();
                cmbProveedor.DisplayMember = "nombre_proveedor";
                cmbProveedor.ValueMember = "id_proveedor";
            }
            catch (Exception ex) { MessageBox.Show("Error al llenar listas: " + ex.Message); }
        }

        /// <summary>
        /// Cargars the datos cabecera.
        /// </summary>
        private void CargarDatosCabecera()
        {
            try
            {
                DataTable dt = logic.ObtenerCabeceraCompra(idCompraAEditar);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow fila = dt.Rows[0];
                    cmbFormaPago.SelectedValue = fila["id_tipo_forma_pago"];
                    cmbProveedor.SelectedValue = fila["id_proveedor"];
                    DateTime fecha = Convert.ToDateTime(fila["fecha_pedido"]);
                    dtpFechaPedido.SelectionStart = fecha;
                    dtpFechaPedido.SelectionEnd = fecha;
                    txtNotaDetalle.Text = fila["desc_compra"].ToString();

                    cmbProveedor.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cmbProveedor.AutoCompleteSource = AutoCompleteSource.ListItems;
                    cmbProveedor.DropDownStyle = ComboBoxStyle.DropDown;
                }
            }
            catch (Exception ex) { MessageBox.Show("Error al cargar datos: " + ex.Message); }
        }

        /// <summary>
        /// Handles the Click event of the btnAceptar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (cmbProveedor.SelectedValue == null || cmbProveedor.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, seleccione un proveedor válido de la lista",
                                "BAMS - Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbProveedor.Focus();
                return;
            }

            try
            {
                int idProv = Convert.ToInt32(cmbProveedor.SelectedValue);
                int idPago = Convert.ToInt32(cmbFormaPago.SelectedValue);
                DateTime fecha = dtpFechaPedido.SelectionStart;
                string nota = txtNotaDetalle.Text;

                logic.ActualizarCabeceraCompra(idCompraAEditar, idProv, idPago, fecha, nota);

                foreach (int idEliminado in listaEliminados)
                {
                    logic.EliminarProductoDeBD(idCompraAEditar, idEliminado);
                }

                foreach (DataGridViewRow fila in dgvProductosCompraMod.Rows)
                {
                    if (fila.Cells["ID"].Value != null && fila.Cells["ID"].Value != DBNull.Value)
                    {
                        int idProd = Convert.ToInt32(fila.Cells["ID"].Value);
                        int cant = Convert.ToInt32(fila.Cells["Cantidad"].Value);
                        decimal precio = Convert.ToDecimal(fila.Cells["Precio"].Value);
                        logic.GuardarCambiosDetalle(idCompraAEditar, idProd, cant, precio);
                    }
                }

                MessageBox.Show("¡Datos de compra, productos e inventario actualizados con éxito!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar cambios: " + ex.Message);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnEliminarProducto control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnEliminarProducto_Click(object sender, EventArgs e)
        {
            if (dgvProductosCompraMod.CurrentRow != null && !dgvProductosCompraMod.CurrentRow.IsNewRow)
            {
                int filasMinimas = dgvProductosCompraMod.AllowUserToAddRows ? 2 : 1;

                if (dgvProductosCompraMod.Rows.Count <= filasMinimas)
                {
                    MessageBox.Show("Una compra no puede quedarse sin productos. Debe mantener al menos un artículo.",
                                    "BAMS - Restricción", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult respuesta = MessageBox.Show("¿Quitar este producto de la compra?", "Confirmar",
                                             MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        int idAEliminar = Convert.ToInt32(dgvProductosCompraMod.CurrentRow.Cells["ID"].Value);
                        int cantidadARestar = Convert.ToInt32(dgvProductosCompraMod.CurrentRow.Cells["Cantidad"].Value);

                        bool esNuevoDeEstaSesion = true;
                        if (dtRespaldo != null)
                        {
                            foreach (DataRow filaRespaldo in dtRespaldo.Rows)
                            {
                                if (Convert.ToInt32(filaRespaldo["ID"]) == idAEliminar)
                                {
                                    esNuevoDeEstaSesion = false;
                                    break;
                                }
                            }
                        }

                        if (esNuevoDeEstaSesion)
                        {
                            logic.RevertirStockProductoNuevo(idCompraAEditar, idAEliminar, cantidadARestar);
                        }
                        else
                        {
                            listaEliminados.Add(idAEliminar);
                        }

                        huboCambios = true;
                        dgvProductosCompraMod.Rows.RemoveAt(dgvProductosCompraMod.CurrentRow.Index);
                        ActualizarTotalGeneral();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al procesar la eliminación y stock: " + ex.Message,
                                        "Error BAMS", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the kryptonButton5 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void kryptonButton5_Click(object sender, EventArgs e)
        {
            if (cmbProveedor.SelectedValue == null)
            {
                MessageBox.Show("No se pudo detectar el proveedor de esta compra.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int idProv = Convert.ToInt32(cmbProveedor.SelectedValue);

            using (Agregar_Producto_Mod frm = new Agregar_Producto_Mod(idProv))
            {
                frm.IdCompraActual = idCompraAEditar.ToString();

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        huboCambios = true;
                        dgvProductosCompraMod.DataSource = logic.ObtenerDetalleCompra(idCompraAEditar);
                        ConfigurarEdicionGrid();
                        ActualizarTotalGeneral();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al actualizar la lista: " + ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the kryptonButton4 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            if (!huboCambios)
            {
                this.Close();
                return;
            }

            if (MessageBox.Show("¿Desea cancelar? Se eliminarán los cambios de esta sesión.",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    foreach (DataGridViewRow fila in dgvProductosCompraMod.Rows)
                    {
                        if (fila.Cells["ID"].Value == null || fila.Cells["ID"].Value == DBNull.Value) continue;

                        int idProd = Convert.ToInt32(fila.Cells["ID"].Value);
                        int cantidadARestar = Convert.ToInt32(fila.Cells["Cantidad"].Value);

                        bool esNuevo = true;
                        if (dtRespaldo != null)
                        {
                            foreach (DataRow filaRespaldo in dtRespaldo.Rows)
                            {
                                if (Convert.ToInt32(filaRespaldo["ID"]) == idProd) { esNuevo = false; break; }
                            }
                        }

                        if (esNuevo)
                        {
                            logic.RevertirStockProductoNuevo(idCompraAEditar, idProd, cantidadARestar);
                        }
                    }
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al revertir stock: " + ex.Message, "Error de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Handles the CellBeginEdit event of the dgvProductosCompraMod control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellCancelEventArgs"/> instance containing the event data.</param>
        private void dgvProductosCompraMod_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                valorAntesDeCambio = dgvProductosCompraMod.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            }
        }

        /// <summary>
        /// Handles the 1 event of the dgvProductosCompraMod_CellValueChanged control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dgvProductosCompraMod_CellValueChanged_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string nombreCol = dgvProductosCompraMod.Columns[e.ColumnIndex].Name;

            if (nombreCol == "Cantidad" || nombreCol == "Precio")
            {
                huboCambios = true;
                var fila = dgvProductosCompraMod.Rows[e.RowIndex];
                decimal nuevoValor;
                string valorCelda = fila.Cells[e.ColumnIndex].Value?.ToString();
                bool esValido = decimal.TryParse(valorCelda, out nuevoValor);

                if (!esValido || nuevoValor <= 0)
                {
                    MessageBox.Show($"El valor en '{nombreCol}' debe ser un número mayor a cero.",
                                    "BAMS - Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    dgvProductosCompraMod.CellValueChanged -= dgvProductosCompraMod_CellValueChanged;
                    fila.Cells[e.ColumnIndex].Value = valorAntesDeCambio;
                    dgvProductosCompraMod.CellValueChanged += dgvProductosCompraMod_CellValueChanged;
                    return;
                }

                try
                {
                    decimal cantidad = Convert.ToDecimal(fila.Cells["Cantidad"].Value ?? 0);
                    decimal precio = Convert.ToDecimal(fila.Cells["Precio"].Value ?? 0);
                    fila.Cells["Subtotal"].Value = cantidad * precio;
                    ActualizarTotalGeneral();
                }
                catch { }
            }
        }

        /// <summary>
        /// Handles the CellDoubleClick event of the dgvProductosCompraMod control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dgvProductosCompraMod_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
        }

        /// <summary>
        /// Handles the AllowUserToAddRowsChanged event of the dgvProductosCompraMod control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void dgvProductosCompraMod_AllowUserToAddRowsChanged(object sender, EventArgs e)
        {
        }
    }
}