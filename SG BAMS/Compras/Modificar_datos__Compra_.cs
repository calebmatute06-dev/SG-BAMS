using Microsoft.Data.SqlClient;
using SG_BAMS.ProductoInventario;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SG_BAMS
{
    public partial class Modificar_datos__Compra_ : Form
    {
        private void cmbProveedor_SelectedIndexChanged(object sender, EventArgs e) { huboCambios = true; }
        private void cmbFormaPago_SelectedIndexChanged(object sender, EventArgs e) { huboCambios = true; }
        private DataTable dtRespaldo;
        private bool huboCambios = false;

        private object valorAntesDeCambio;

        private int idCompraAEditar;
        private ClsModificarCompras logic = new ClsModificarCompras();
        private List<int> listaEliminados = new List<int>();

        public Modificar_datos__Compra_(int id)
        {
            InitializeComponent();
            this.idCompraAEditar = id;

            // SUSCRIPCIÓN MANUAL A EVENTOS (Si no lo hiciste en el diseñador)
            dgvProductosCompraMod.CellValueChanged += dgvProductosCompraMod_CellValueChanged;
            dgvProductosCompraMod.CurrentCellDirtyStateChanged += dgvProductosCompraMod_CurrentCellDirtyStateChanged;
            dgvProductosCompraMod.CellBeginEdit += dgvProductosCompraMod_CellBeginEdit;
        }

        private void Modificar_datos__Compra__Load(object sender, EventArgs e)
        {
            cmbProveedor.SelectedIndexChanged -= cmbProveedor_SelectedIndexChanged;
            cmbFormaPago.SelectedIndexChanged -= cmbFormaPago_SelectedIndexChanged;

            LlenarCombos();
            ClsDetalleCompra objetoDetalle = new ClsDetalleCompra();
            DataTable dtOriginal = objetoDetalle.ListarProductosDeCompra(idCompraAEditar);
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

        // --- LÓGICA DE CÁLCULO AUTOMÁTICO ---

        private void dgvProductosCompraMod_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            // Si la celda está en edición, confirmamos el valor de inmediato
            if (dgvProductosCompraMod.IsCurrentCellDirty)
            {
                dgvProductosCompraMod.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dgvProductosCompraMod_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Validamos que el cambio sea en las columnas de Cantidad o Precio
            if (e.RowIndex >= 0 && (dgvProductosCompraMod.Columns[e.ColumnIndex].Name == "Cantidad" ||
                                    dgvProductosCompraMod.Columns[e.ColumnIndex].Name == "Precio"))
            {
                try
                {
                    decimal cantidad = Convert.ToDecimal(dgvProductosCompraMod.Rows[e.RowIndex].Cells["Cantidad"].Value ?? 0);
                    decimal precio = Convert.ToDecimal(dgvProductosCompraMod.Rows[e.RowIndex].Cells["Precio"].Value ?? 0);

                    // Actualizamos el subtotal de la fila
                    dgvProductosCompraMod.Rows[e.RowIndex].Cells["Subtotal"].Value = cantidad * precio;

                    // Actualizamos el total de la etiqueta
                    ActualizarTotalGeneral();
                }
                catch { /* Evita cierres por formatos inválidos mientras se escribe */ }
            }
        }

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

        // --- CONFIGURACIÓN Y CARGA ---

        private void ConfigurarEdicionGrid()
        {
            if (dgvProductosCompraMod.Columns.Contains("ID")) dgvProductosCompraMod.Columns["ID"].ReadOnly = true;
            if (dgvProductosCompraMod.Columns.Contains("Producto")) dgvProductosCompraMod.Columns["Producto"].ReadOnly = true;
            if (dgvProductosCompraMod.Columns.Contains("Subtotal")) dgvProductosCompraMod.Columns["Subtotal"].ReadOnly = true;

            if (dgvProductosCompraMod.Columns.Contains("Cantidad")) dgvProductosCompraMod.Columns["Cantidad"].ReadOnly = false;
            if (dgvProductosCompraMod.Columns.Contains("Precio")) dgvProductosCompraMod.Columns["Precio"].ReadOnly = false;

            if (dgvProductosCompraMod.Columns.Contains("Precio"))
            {
                dgvProductosCompraMod.Columns["Precio"].DefaultCellStyle.Format = "N2";
            }

            if (dgvProductosCompraMod.Columns.Contains("Subtotal"))
            {
                dgvProductosCompraMod.Columns["Subtotal"].DefaultCellStyle.Format = "N2";
            }
        }

        private void LlenarCombos()
        {
            ClsConexion conexion = new ClsConexion();
            try
            {
                conexion.AbrirConexion();
                string qPago = "SELECT id_tipo_forma_pago, descripcion_forma_pago FROM Tipo_Forma_de_pago";
                SqlDataAdapter daPago = new SqlDataAdapter(qPago, conexion.Conectar);
                DataTable dtPago = new DataTable();
                daPago.Fill(dtPago);
                cmbFormaPago.DataSource = dtPago;
                cmbFormaPago.DisplayMember = "descripcion_forma_pago";
                cmbFormaPago.ValueMember = "id_tipo_forma_pago";

                string qProv = "SELECT id_proveedor, nombre_proveedor FROM Proveedor WHERE id_estado = 1";
                SqlDataAdapter daProv = new SqlDataAdapter(qProv, conexion.Conectar);
                DataTable dtProv = new DataTable();
                daProv.Fill(dtProv);
                cmbProveedor.DataSource = dtProv;
                cmbProveedor.DisplayMember = "nombre_proveedor";
                cmbProveedor.ValueMember = "id_proveedor";
            }
            catch (Exception ex) { MessageBox.Show("Error al llenar listas: " + ex.Message); }
            finally { conexion.Cerrar(); }
        }

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

        // --- ACCIONES (ACEPTAR / ELIMINAR) ---

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (cmbProveedor.SelectedValue == null || cmbProveedor.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, seleccione un proveedor válido de la lista",
                                "BAMS - Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbProveedor.Focus();
                return; // Detiene la ejecución antes de tocar la base de datos
            }

            try
            {
                // 0. ACTUALIZAR CABECERA (Proveedor, Pago, Fecha, Nota)
                int idProv = Convert.ToInt32(cmbProveedor.SelectedValue);
                int idPago = Convert.ToInt32(cmbFormaPago.SelectedValue);
                DateTime fecha = dtpFechaPedido.SelectionStart; // Captura la fecha del MonthCalendar
                string nota = txtNotaDetalle.Text;

                logic.ActualizarCabeceraCompra(idCompraAEditar, idProv, idPago, fecha, nota);

                // 1. ELIMINAR productos borrados visualmente
                foreach (int idEliminado in listaEliminados)
                {
                    logic.EliminarProductoDeBD(idCompraAEditar, idEliminado);
                }

                // 2. ACTUALIZAR O INSERTAR productos actuales
                foreach (DataGridViewRow fila in dgvProductosCompraMod.Rows)
                {
                    if (fila.Cells["ID"].Value != null && fila.Cells["ID"].Value != DBNull.Value)
                    {
                        int idProd = Convert.ToInt32(fila.Cells["ID"].Value);
                        int cant = Convert.ToInt32(fila.Cells["Cantidad"].Value);
                        decimal precio = Convert.ToDecimal(fila.Cells["Precio"].Value);

                        // Esto ahora también disparará el stock con el nuevo Procedure
                        logic.GuardarCambiosDetalle(idCompraAEditar, idProd, cant, precio);
                    }
                }

                MessageBox.Show("¡Datos de compra, productos e inventario actualizados con éxito!");
                this.Close();
            }
            catch
            (Exception ex)
            {
                MessageBox.Show("Error al guardar cambios: " + ex.Message);
            }
        }


        private void btnEliminarProducto_Click(object sender, EventArgs e)
        {
            if (dgvProductosCompraMod.CurrentRow != null && !dgvProductosCompraMod.CurrentRow.IsNewRow)
            {
                // 1. RESTRICCIÓN: No permitir eliminar si es el último producto
                int filasMinimas = dgvProductosCompraMod.AllowUserToAddRows ? 2 : 1;

                if (dgvProductosCompraMod.Rows.Count <= filasMinimas)
                {
                    MessageBox.Show("Una compra no puede quedarse sin productos. Debe mantener al menos un artículo.",
                                    "BAMS - Restricción", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. CONFIRMACIÓN DEL USUARIO
                DialogResult respuesta = MessageBox.Show("¿Quitar este producto de la compra?", "Confirmar",
                                         MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    try
                    {
                        int idAEliminar = Convert.ToInt32(dgvProductosCompraMod.CurrentRow.Cells["ID"].Value);
                        int cantidadARestar = Convert.ToInt32(dgvProductosCompraMod.CurrentRow.Cells["Cantidad"].Value);

                        // 3. IDENTIFICAR SI EL PRODUCTO ES NUEVO (No está en el respaldo original)
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

                        // 4. ACCIÓN SEGÚN EL TIPO DE PRODUCTO
                        if (esNuevoDeEstaSesion)
                        {
                            // Es un producto recién metido: Borrar de BD y revertir stock YA
                            logic.RevertirStockProductoNuevo(idCompraAEditar, idAEliminar, cantidadARestar);
                        }
                        else
                        {
                            // Ya existía en la compra: Solo marcar para eliminar al dar click en "Aceptar"
                            listaEliminados.Add(idAEliminar);
                        }

                        // 5. ACTUALIZAR INTERFAZ
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

        private void kryptonButton5_Click(object sender, EventArgs e)
        {
            using (Agregar_Producto_Mod frm = new Agregar_Producto_Mod())
            {
                frm.IdCompraActual = idCompraAEditar.ToString();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        huboCambios = true; // Marcamos que hubo una inserción real en la BD
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

        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            if (!huboCambios)
            {
                this.Close();
                return;
            }

            if (MessageBox.Show("¿Desea cancelar? Se eliminarán los cambios de esta sesion.",
          "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    ClsConexion con = new ClsConexion();
                    con.AbrirConexion();

                    foreach (DataGridViewRow fila in dgvProductosCompraMod.Rows)
                    {
                        if (fila.Cells["ID"].Value == null || fila.Cells["ID"].Value == DBNull.Value) continue;

                        int idProd = Convert.ToInt32(fila.Cells["ID"].Value);
                        int cantidadARestar = Convert.ToInt32(fila.Cells["Cantidad"].Value);

                        // Comparamos con el respaldo para identificar solo lo NUEVO de esta sesión
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
                            // 1. RESTAR DEL INVENTARIO (Tabla Inventario, columna stock)
                            string sqlStock = "UPDATE Inventario SET stock = stock - @cant WHERE id_producto = @idP";
                            using (SqlCommand cmdStock = new SqlCommand(sqlStock, con.Conectar))
                            {
                                cmdStock.Parameters.AddWithValue("@cant", cantidadARestar);
                                cmdStock.Parameters.AddWithValue("@idP", idProd);
                                cmdStock.ExecuteNonQuery();
                            }

                            // 2. ELIMINAR EL REGISTRO DE LA TABLA COMPRA_PRODUCTO
                            string sqlDel = "DELETE FROM Compra_producto WHERE id_compra = @idC AND id_producto = @idP";
                            using (SqlCommand cmdDel = new SqlCommand(sqlDel, con.Conectar))
                            {
                                cmdDel.Parameters.AddWithValue("@idC", idCompraAEditar);
                                cmdDel.Parameters.AddWithValue("@idP", idProd);
                                cmdDel.ExecuteNonQuery();
                            }
                        }
                    }
                    con.Cerrar();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al revertir stock: " + ex.Message, "Error de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvProductosCompraMod_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Guardamos el valor justo antes de que el usuario lo toque
                valorAntesDeCambio = dgvProductosCompraMod.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            }
        }

        private void dgvProductosCompraMod_CellValueChanged_1(object sender, DataGridViewCellEventArgs e)
        {
            // 1. Validamos que no sea el encabezado y que sean las columnas editables
            if (e.RowIndex < 0) return;

            string nombreCol = dgvProductosCompraMod.Columns[e.ColumnIndex].Name;

            if (nombreCol == "Cantidad" || nombreCol == "Precio")
            {
                // Activamos la bandera de que el usuario modificó datos
                huboCambios = true;

                var fila = dgvProductosCompraMod.Rows[e.RowIndex];

                // 2. Intentamos validar el nuevo valor ingresado
                decimal nuevoValor;
                string valorCelda = fila.Cells[e.ColumnIndex].Value?.ToString();
                bool esValido = decimal.TryParse(valorCelda, out nuevoValor);

                if (!esValido || nuevoValor <= 0)
                {
                    MessageBox.Show($"El valor en '{nombreCol}' debe ser un número mayor a cero.",
                                    "BAMS - Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    // 3. Restauramos el valor anterior (importante desvincular el evento para evitar bucle)
                    dgvProductosCompraMod.CellValueChanged -= dgvProductosCompraMod_CellValueChanged;
                    fila.Cells[e.ColumnIndex].Value = valorAntesDeCambio;
                    dgvProductosCompraMod.CellValueChanged += dgvProductosCompraMod_CellValueChanged;
                    return;
                }

                // 4. Si el valor es correcto, recalculamos la fila y el total
                try
                {
                    decimal cantidad = Convert.ToDecimal(fila.Cells["Cantidad"].Value ?? 0);
                    decimal precio = Convert.ToDecimal(fila.Cells["Precio"].Value ?? 0);

                    // Actualizamos el subtotal de la celda
                    fila.Cells["Subtotal"].Value = cantidad * precio;

                    // Actualizamos el total general de la etiqueta L.
                    ActualizarTotalGeneral();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error en cálculo: " + ex.Message);
                }
            }
        }

        private void dgvProductosCompraMod_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return; // Salimos del método sin hacer nada
            }
        }

        private void dgvProductosCompraMod_AllowUserToAddRowsChanged(object sender, EventArgs e)
        {

        }
    }
}