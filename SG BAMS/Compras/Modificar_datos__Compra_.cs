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
        /// La tabla de respaldo
        /// </summary>
        private DataTable dtRespaldo;
        /// <summary>
        /// Indica si hubo cambios
        /// </summary>
        private bool huboCambios = false;
        /// <summary>
        /// El valor antes del cambio
        /// </summary>
        private object valorAntesDeCambio;
        /// <summary>
        /// El identificador de la compra a editar
        /// </summary>
        private int idCompraAEditar;
        /// <summary>
        /// La lógica de negocio
        /// </summary>
        private ClsModificarCompras logic = new ClsModificarCompras();
        /// <summary>
        /// La lista de elementos eliminados
        /// </summary>
        private List<int> listaEliminados = new List<int>();

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Modificar_datos__Compra_"/>.
        /// </summary>
        /// <param name="id">El identificador.</param>
        public Modificar_datos__Compra_(int id)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.idCompraAEditar = id;

            dgvProductosModificar.CellValueChanged += dgvProductosModificar_CellValueChanged;
            dgvProductosModificar.CurrentCellDirtyStateChanged += dgvProductosModificar_CurrentCellDirtyStateChanged;
            dgvProductosModificar.CellBeginEdit += dgvProductosModificar_CellBeginEdit;
        }

        /// <summary>
        /// Maneja el evento Load del control Modificar_datos__Compra_.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void Modificar_datos__Compra__Load(object sender, EventArgs e)
        {
            cmbProveedor.Enabled = false;
            cmbProveedor.BackColor = Color.LightGray;


            cmbProveedor.SelectedIndexChanged -= cmbProveedor_SelectedIndexChanged;
            cmbFormaPago.SelectedIndexChanged -= cmbFormaPago_SelectedIndexChanged;

            LlenarCombos();

            DataTable dtOriginal = logic.ObtenerDetalleCompra(idCompraAEditar);
            dgvProductosModificar.DataSource = dtOriginal;

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

            dgvProductosModificar.BorderStyle = BorderStyle.None;
            dgvProductosModificar.BackgroundColor = Color.White;
            dgvProductosModificar.RowHeadersVisible = false;
            dgvProductosModificar.EnableHeadersVisualStyles = false;
            dgvProductosModificar.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgvProductosModificar.ColumnHeadersDefaultCellStyle.BackColor = Color.SkyBlue;
            dgvProductosModificar.ColumnHeadersDefaultCellStyle.ForeColor = Color.Navy;
            dgvProductosModificar.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvProductosModificar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvProductosModificar.ColumnHeadersHeight = 28;

            dgvProductosModificar.DefaultCellStyle.BackColor = Color.White;
            dgvProductosModificar.DefaultCellStyle.ForeColor = Color.Navy;
            dgvProductosModificar.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvProductosModificar.DefaultCellStyle.Padding = new Padding(3);
            dgvProductosModificar.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 245, 255);
            dgvProductosModificar.AlternatingRowsDefaultCellStyle.ForeColor = Color.Navy;

            dgvProductosModificar.DefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            dgvProductosModificar.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvProductosModificar.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvProductosModificar.GridColor = Color.LightGray;
            dgvProductosModificar.RowTemplate.Height = 32;
            dgvProductosModificar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProductosModificar.ClearSelection();
        }

        /// <summary>
        /// Actualiza el total general.
        /// </summary>
        private void ActualizarTotalGeneral()
        {
            decimal total = 0;
            foreach (DataGridViewRow fila in dgvProductosModificar.Rows)
            {
                if (fila.Cells["Subtotal"].Value != null)
                    total += Convert.ToDecimal(fila.Cells["Subtotal"].Value);
            }
            lblTotal.Text = "Total: L " + total.ToString("N2");
        }

        /// <summary>
        /// Configura la edición de la cuadrícula.
        /// </summary>
        private void ConfigurarEdicionGrid()
        {
            if (dgvProductosModificar.Columns.Contains("ID")) dgvProductosModificar.Columns["ID"].ReadOnly = true;
            if (dgvProductosModificar.Columns.Contains("Producto")) dgvProductosModificar.Columns["Producto"].ReadOnly = true;
            if (dgvProductosModificar.Columns.Contains("Subtotal")) dgvProductosModificar.Columns["Subtotal"].ReadOnly = true;
            if (dgvProductosModificar.Columns.Contains("Cantidad")) dgvProductosModificar.Columns["Cantidad"].ReadOnly = false;
            if (dgvProductosModificar.Columns.Contains("Precio")) dgvProductosModificar.Columns["Precio"].ReadOnly = false;

            if (dgvProductosModificar.Columns.Contains("Precio")) dgvProductosModificar.Columns["Precio"].DefaultCellStyle.Format = "N2";
            if (dgvProductosModificar.Columns.Contains("Subtotal")) dgvProductosModificar.Columns["Subtotal"].DefaultCellStyle.Format = "N2";
        }

        /// <summary>
        /// Llena los combos.
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
        /// Carga los datos de cabecera.
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
                }
            }
            catch (Exception ex) { MessageBox.Show("Error al cargar datos: " + ex.Message); }
        }

        /// <summary>
        /// Maneja el evento Click del control btnAceptar.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
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

                foreach (DataGridViewRow fila in dgvProductosModificar.Rows)
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
        /// Maneja el evento Click del control btnEliminarProducto.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnEliminarProducto_Click(object sender, EventArgs e)
        {
            if (dgvProductosModificar.CurrentRow == null || dgvProductosModificar.CurrentRow.IsNewRow) return;

            if (dgvProductosModificar.Rows.Count <= 1)
            {
                MessageBox.Show("Debe mantener al menos un artículo.", "BAMS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("¿Quitar este producto?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int idAEliminar = Convert.ToInt32(dgvProductosModificar.CurrentRow.Cells["ID"].Value);
                int cant = Convert.ToInt32(dgvProductosModificar.CurrentRow.Cells["Cantidad"].Value);

                bool esNuevoSesion = true;
                if (dtRespaldo != null)
                {
                    foreach (DataRow r in dtRespaldo.Rows)
                        if ((int)r["ID"] == idAEliminar) { esNuevoSesion = false; break; }
                }

                if (esNuevoSesion) logic.RevertirStockProductoNuevo(idCompraAEditar, idAEliminar, cant);
                else listaEliminados.Add(idAEliminar);

                huboCambios = true;
                dgvProductosModificar.Rows.RemoveAt(dgvProductosModificar.CurrentRow.Index);
                ActualizarTotalGeneral();
            }
        }

        /// <summary>
        /// Maneja el evento Click del control kryptonButton5.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
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

        /// <summary>
        /// Maneja el evento Click del control kryptonButton4.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            if (!huboCambios) { this.Close(); return; }

            if (MessageBox.Show("¿Desea cancelar? Se perderán los cambios.", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        /// <summary>
        /// Maneja el evento SelectedIndexChanged del control cmbProveedor.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void cmbProveedor_SelectedIndexChanged(object sender, EventArgs e) => huboCambios = true;
        /// <summary>
        /// Maneja el evento SelectedIndexChanged del control cmbFormaPago.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void cmbFormaPago_SelectedIndexChanged(object sender, EventArgs e) => huboCambios = true;

        /// <summary>
        /// Maneja el evento CellValueChanged del control dgvProductosModificar.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="DataGridViewCellEventArgs"/> que contiene los datos del evento.</param>
        private void dgvProductosModificar_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string nombreCol = dgvProductosModificar.Columns[e.ColumnIndex].Name;

            if (nombreCol == "Cantidad" || nombreCol == "Precio")
            {
                huboCambios = true;
                var fila = dgvProductosModificar.Rows[e.RowIndex];

                if (!decimal.TryParse(fila.Cells[e.ColumnIndex].Value?.ToString(), out decimal nuevoValor) || nuevoValor <= 0)
                {
                    MessageBox.Show($"El valor en '{nombreCol}' debe ser un número mayor a cero.", "BAMS", MessageBoxButtons.OK, MessageBoxIcon.Warning);

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

        /// <summary>
        /// Maneja el evento CellBeginEdit del control dgvProductosModificar.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="DataGridViewCellCancelEventArgs"/> que contiene los datos del evento.</param>
        private void dgvProductosModificar_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                valorAntesDeCambio = dgvProductosModificar.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            }
        }

        /// <summary>
        /// Maneja el evento CurrentCellDirtyStateChanged del control dgvProductosModificar.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void dgvProductosModificar_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvProductosModificar.IsCurrentCellDirty)
            {
                dgvProductosModificar.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
    }
}