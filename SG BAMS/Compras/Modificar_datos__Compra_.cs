using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using SG_BAMS.ComprasContratos;
using SG_BAMS.ComprasDTO;
using SG_BAMS.ProductoInventario;
using SG_BAMS.AccesoDatos;
using SG_BAMS.Facturas;

namespace SG_BAMS
{
    public partial class Modificar_datos__Compra_ : Form
    {
        private readonly IModificarComprasRepository logic;
        private readonly NavegacionService _navegacion;
        private DataTable dtRespaldo;
        private bool huboCambios = false;
        private object valorAntesDeCambio;
        private int idCompraAEditar;
        private List<int> listaEliminados = new List<int>();

        private PlaceholderTextBox phNotaDetalle;
        private PlaceholderComboBox phProveedor;
        private PlaceholderComboBox phFormaPago;

        // Repositorio de Compras (no el de "Modificar") usado únicamente para
        // el flujo de escaneo: es el mismo que ya usaba Agregar_Producto_Mod
        // para consultar productos por proveedor y agregar detalle existente.
        private readonly IComprasRepository comprasRepo = new ClsCompras();
        private readonly ServicioEscaneoBarras _servicioEscaneo = new ServicioEscaneoBarras();
        private readonly IBuscadorProductoProveedorService _buscadorCodigo = new BuscadorProductoProveedorService();

        public Modificar_datos__Compra_(int id) : this(id, new ClsModificarCompras(), new NavegacionService()) { }

        public Modificar_datos__Compra_(int id, IModificarComprasRepository logic, NavegacionService navegacion)
        {
            this.logic = logic;
            this._navegacion = navegacion;
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.idCompraAEditar = id;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            dgvProductosModificar.CellValueChanged += dgvProductosModificar_CellValueChanged;
            dgvProductosModificar.CurrentCellDirtyStateChanged += dgvProductosModificar_CurrentCellDirtyStateChanged;
            dgvProductosModificar.CellBeginEdit += dgvProductosModificar_CellBeginEdit;

            _servicioEscaneo.CodigoEscaneado += ManejarCodigoEscaneado;
        }

        /// <summary>
        /// Captura las teclas del lector de código de barras en cualquier
        /// parte del formulario, siempre que el usuario no esté escribiendo
        /// en un control de texto/combo/grid.
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys key = keyData & Keys.KeyCode;

            bool escribiendoEnControl = txtNotaDetalle.Focused
                || cmbFormaPago.Focused
                || dgvProductosModificar.IsCurrentCellInEditMode;

            if (escribiendoEnControl)
                return base.ProcessCmdKey(ref msg, keyData);

            if (_servicioEscaneo.ProcesarTecla(key))
                return true;

            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Se dispara cuando el lector de código de barras completa un
        /// código. Como en esta pantalla el proveedor de la compra ya está
        /// fijo (no se puede cambiar), solo se valida que el código
        /// escaneado pertenezca a ese proveedor; si no, se avisa que no
        /// está vinculado. Si es válido, se pide cantidad y precio unitario
        /// y se agrega directamente a la compra existente.
        /// </summary>
        private void ManejarCodigoEscaneado(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo)) return;
            if (cmbProveedor.SelectedValue == null) return;

            int idProveedorActual = Convert.ToInt32(cmbProveedor.SelectedValue);
            var resultado = _buscadorCodigo.BuscarEnProveedor(comprasRepo, idProveedorActual, codigo);

            if (!resultado.Encontrado)
            {
                MessageBox.Show($"El código [{codigo}] no está vinculado al proveedor de esta compra ({cmbProveedor.Text}).",
                    "Producto no vinculado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comprasRepo.ValidarProductoEnCompra(idCompraAEditar.ToString(), resultado.IdProducto))
            {
                MessageBox.Show("Este producto ya está incluido en la compra.\nModifique la cantidad en la tabla (doble clic sobre la celda).",
                    "Producto Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var popup = new FrmCantidadPrecioEscaneo(resultado.NombreProducto))
            {
                if (popup.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        comprasRepo.AgregarDetalleACompraExistente(idCompraAEditar.ToString(), resultado.IdProducto,
                            popup.CantidadResultado, popup.PrecioResultado);

                        huboCambios = true;
                        dgvProductosModificar.DataSource = logic.ObtenerDetalleCompra(idCompraAEditar);
                        ConfigurarEdicionGrid();
                        ActualizarTotalGeneral();

                        MessageBox.Show("Producto añadido correctamente a la compra.", "SG-BAMS",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public void Modificar_datos__Compra__Load(object sender, EventArgs e)
        {
            cmbProveedor.Enabled = false;
            cmbProveedor.BackColor = Color.LightGray;
            dtpFechaPedido.Enabled = false;

            cmbProveedor.SelectedIndexChanged -= cmbProveedor_SelectedIndexChanged;
            cmbFormaPago.SelectedIndexChanged -= cmbFormaPago_SelectedIndexChanged;

            LlenarCombos();

            phNotaDetalle = new PlaceholderTextBox(txtNotaDetalle, "Solo letras y espacios");
            phProveedor = new PlaceholderComboBox(cmbProveedor, "Seleccione un proveedor");
            phFormaPago = new PlaceholderComboBox(cmbFormaPago, "Seleccione una forma de pago");

            DataTable dtOriginal = logic.ObtenerDetalleCompra(idCompraAEditar);
            dgvProductosModificar.DataSource = dtOriginal;
            if (dtOriginal != null)
            {
                dtRespaldo = dtOriginal.Copy();
            }

            EstiloDataGridView.Aplicar(dgvProductosModificar);

            ConfigurarEdicionGrid();
            CargarDatosCabecera();
            ActualizarTotalGeneral();
            huboCambios = false;

            cmbProveedor.SelectedIndexChanged += cmbProveedor_SelectedIndexChanged;
            cmbFormaPago.SelectedIndexChanged += cmbFormaPago_SelectedIndexChanged;

            this.ActiveControl = null;
        }

        private void ActualizarTotalGeneral()
        {
            decimal total = 0;
            foreach (DataGridViewRow fila in dgvProductosModificar.Rows)
            {
                if (fila.Cells["Subtotal"].Value != null)
                    total += Convert.ToDecimal(fila.Cells["Subtotal"].Value);
            }
            lblTotal.Text = $"Total: L. {total:N2}";
        }

        private void ConfigurarEdicionGrid()
        {
            if (dgvProductosModificar.Columns.Contains("ID")) dgvProductosModificar.Columns["ID"].ReadOnly = true;
            if (dgvProductosModificar.Columns.Contains("Producto")) dgvProductosModificar.Columns["Producto"].ReadOnly = true;
            if (dgvProductosModificar.Columns.Contains("Subtotal")) dgvProductosModificar.Columns["Subtotal"].ReadOnly = true;
            if (dgvProductosModificar.Columns.Contains("Cantidad")) dgvProductosModificar.Columns["Cantidad"].ReadOnly = false;
            if (dgvProductosModificar.Columns.Contains("Precio")) dgvProductosModificar.Columns["Precio"].ReadOnly = false;
            if (dgvProductosModificar.Columns.Contains("Precio")) dgvProductosModificar.Columns["Precio"].DefaultCellStyle.Format = "\"L. \"#,##0.00";
            if (dgvProductosModificar.Columns.Contains("Subtotal")) dgvProductosModificar.Columns["Subtotal"].DefaultCellStyle.Format = "\"L. \"#,##0.00";
        }

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
                    dtpFechaPedido.Value = fecha;
                    txtNotaDetalle.Text = fila["desc_compra"].ToString();
                }
            }
            catch (Exception ex) { MessageBox.Show("Error al cargar datos: " + ex.Message); }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            bool proveedorSeleccionado = cmbProveedor.SelectedValue != null && cmbProveedor.SelectedIndex != -1;
            var validacion = ComprasDominio.ValidarProveedorSeleccionado(proveedorSeleccionado);
            if (!validacion.EsValido)
            {
                MessageBox.Show(validacion.Mensaje, "BAMS - Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbProveedor.Focus();
                return;
            }

            try
            {
                CompraDTO compraDTO = new CompraDTO
                {
                    IdCompra = idCompraAEditar,
                    IdProveedor = Convert.ToInt32(cmbProveedor.SelectedValue),
                    IdFormaPago = Convert.ToInt32(cmbFormaPago.SelectedValue),
                    Fecha = dtpFechaPedido.Value,
                    Nota = phNotaDetalle.GetRealValue()
                };

                logic.ActualizarCabeceraCompra(compraDTO);

                foreach (int idEliminado in listaEliminados)
                {
                    logic.EliminarProductoDeBD(idCompraAEditar, idEliminado);
                }

                foreach (DataGridViewRow fila in dgvProductosModificar.Rows)
                {
                    if (fila.Cells["ID"].Value != null && fila.Cells["ID"].Value != DBNull.Value)
                    {
                        compraDTO.Detalle.Add(new DetalleCompraDTO
                        {
                            IdProducto = Convert.ToInt32(fila.Cells["ID"].Value),
                            Cantidad = Convert.ToInt32(fila.Cells["Cantidad"].Value),
                            Precio = Convert.ToDecimal(fila.Cells["Precio"].Value)
                        });
                    }
                }

                logic.ActualizarDetalleCompra(compraDTO.IdCompra, compraDTO.Detalle);

                MessageBox.Show("¡Datos de compra, productos e inventario actualizados con éxito!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar cambios: " + ex.Message);
            }
        }

        private void btnEliminarProducto_Click(object sender, EventArgs e)
        {
            if (dgvProductosModificar.CurrentRow == null || dgvProductosModificar.CurrentRow.IsNewRow) return;

            int filasConDatos = 0;
            foreach (DataGridViewRow fila in dgvProductosModificar.Rows)
            {
                if (!fila.IsNewRow && fila.Cells["ID"].Value != null && fila.Cells["ID"].Value != DBNull.Value)
                {
                    filasConDatos++;
                }
            }

            if (filasConDatos <= 1)
            {
                MessageBox.Show("Debe mantener al menos un artículo.", "BAMS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Quitar este producto?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int idAEliminar = Convert.ToInt32(dgvProductosModificar.CurrentRow.Cells["ID"].Value);
                int cant = Convert.ToInt32(dgvProductosModificar.CurrentRow.Cells["Cantidad"].Value);

                bool esNuevoSesion = ComprasDominio.EsProductoDeSesionActual(dtRespaldo, idAEliminar);

                if (esNuevoSesion) logic.RevertirStockProductoNuevo(idCompraAEditar, idAEliminar, cant);
                else listaEliminados.Add(idAEliminar);

                huboCambios = true;
                dgvProductosModificar.Rows.RemoveAt(dgvProductosModificar.CurrentRow.Index);
                ActualizarTotalGeneral();
            }
        }

        private void kryptonButton5_Click(object sender, EventArgs e)
        {
            if (cmbProveedor.SelectedValue == null) return;

            using (Agregar_Producto_Mod frm = new Agregar_Producto_Mod((int)cmbProveedor.SelectedValue))
            {
                frm.IdCompraActual = idCompraAEditar.ToString();

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    huboCambios = true;
                    dgvProductosModificar.DataSource = logic.ObtenerDetalleCompra(idCompraAEditar);
                    ConfigurarEdicionGrid();
                    ActualizarTotalGeneral();
                }
            }
        }

        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            if (!huboCambios) { this.Close(); return; }

            if (MessageBox.Show("¿Desea cancelar? Se perderán los cambios.", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void cmbProveedor_SelectedIndexChanged(object sender, EventArgs e) => huboCambios = true;

        private void cmbFormaPago_SelectedIndexChanged(object sender, EventArgs e) => huboCambios = true;

        private void dgvProductosModificar_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string nombreCol = dgvProductosModificar.Columns[e.ColumnIndex].Name;
            if (nombreCol == "Cantidad" || nombreCol == "Precio")
            {
                huboCambios = true;
                var fila = dgvProductosModificar.Rows[e.RowIndex];

                decimal.TryParse(fila.Cells[e.ColumnIndex].Value?.ToString(), out decimal nuevoValor);
                var validacion = ComprasDominio.ValidarCantidadOPrecio($"El valor en '{nombreCol}'", nuevoValor);
                if (!validacion.EsValido)
                {
                    MessageBox.Show(validacion.Mensaje, "BAMS", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    dgvProductosModificar.CellValueChanged -= dgvProductosModificar_CellValueChanged;
                    fila.Cells[e.ColumnIndex].Value = valorAntesDeCambio;
                    dgvProductosModificar.CellValueChanged += dgvProductosModificar_CellValueChanged;
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

        private void dgvProductosModificar_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                valorAntesDeCambio = dgvProductosModificar.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            }
        }

        private void dgvProductosModificar_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvProductosModificar.IsCurrentCellDirty)
            {
                dgvProductosModificar.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
    }
}