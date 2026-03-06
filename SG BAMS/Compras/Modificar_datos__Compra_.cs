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
        }

        private void Modificar_datos__Compra__Load(object sender, EventArgs e)
        {
            LlenarCombos();

            ClsDetalleCompra objetoDetalle = new ClsDetalleCompra();
            dgvProductosCompraMod.DataSource = objetoDetalle.ListarProductosDeCompra(idCompraAEditar);

            ConfigurarEdicionGrid();
            CargarDatosCabecera();
            ActualizarTotalGeneral();
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
                }
            }
            catch (Exception ex) { MessageBox.Show("Error al cargar datos: " + ex.Message); }
        }

        // --- ACCIONES (ACEPTAR / ELIMINAR) ---

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
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

                        logic.GuardarCambiosDetalle(idCompraAEditar, idProd, cant, precio);
                    }
                }

                MessageBox.Show("¡Compra e Inventario sincronizados!");
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show("Error al guardar: " + ex.Message); }
        }

        private void btnEliminarProducto_Click(object sender, EventArgs e)
        {
            if (dgvProductosCompraMod.CurrentRow != null && !dgvProductosCompraMod.CurrentRow.IsNewRow)
            {
                DialogResult respuesta = MessageBox.Show("¿Quitar este producto de la compra?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    int idAEliminar = Convert.ToInt32(dgvProductosCompraMod.CurrentRow.Cells["ID"].Value);
                    listaEliminados.Add(idAEliminar);
                    dgvProductosCompraMod.Rows.RemoveAt(dgvProductosCompraMod.CurrentRow.Index);
                    ActualizarTotalGeneral();
                }
            }
        }

        private void kryptonButton5_Click(object sender, EventArgs e)
        {
            using (Agregar_Producto_Mod frm = new Agregar_Producto_Mod())
            {
                // Le pasamos el ID al formulario para que pueda hacer el INSERT directo
                frm.IdCompraActual = idCompraAEditar.ToString();

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // RECARGA DE DATOS: Usamos tu clase 'logic' y el ID actual
                        // Esto actualiza el DataGridView con el nuevo producto guardado
                        dgvProductosCompraMod.DataSource = logic.ObtenerDetalleCompra(idCompraAEditar);

                        // Volvemos a aplicar los permisos de edición (Cantidad y Precio)
                        ConfigurarEdicionGrid();

                        // Actualizamos el total general en la etiqueta L. xx.xx
                        ActualizarTotalGeneral();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al actualizar la lista: " + ex.Message);
                    }
                }
            }
        }
    }
}