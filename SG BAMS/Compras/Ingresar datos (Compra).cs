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

namespace SG_BAMS
{
    public partial class Ingresar_datos__Compra_ : Form
    {
        public Ingresar_datos__Compra_()
        {
            InitializeComponent();
        }

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

                cmbProveedor.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbProveedor.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmbProveedor.DropDownStyle = ComboBoxStyle.DropDown;

                cmbProveedor.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos iniciales: " + ex.Message);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        private string ObtenerSiguienteID()
        {
            ClsConexion conexion = new ClsConexion();
            string proximoID = "1";

            try
            {
                conexion.AbrirConexion();
                string query = "SELECT ISNULL(MAX(id_compra), 0) + 1 FROM Compra";

                using (SqlCommand cmd = new SqlCommand(query, conexion.Conectar))
                {
                    proximoID = cmd.ExecuteScalar().ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar ID: " + ex.Message);
            }
            finally
            {
                conexion.Cerrar();
            }
            return proximoID;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            decimal totalValidar = 0;
            if (!decimal.TryParse(lblTotal.Text, out totalValidar) || totalValidar <= 0)
            {
                MessageBox.Show("No se puede guardar una compra con total L. 0.00.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbProveedor.SelectedValue == null || cmbFormaPago.SelectedValue == null)
            {
                MessageBox.Show("Asegúrese de seleccionar Proveedor y Forma de Pago.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ClsConexion conexion = new ClsConexion();
            conexion.AbrirConexion();
            SqlTransaction transaccion = conexion.Conectar.BeginTransaction();

            try
            {
                // 1. INSERTAR CABECERA
                string queryCabecera = @"INSERT INTO Compra (id_usuario, fecha_pedido, id_tipo_forma_pago, id_proveedor, desc_compra) 
                                 VALUES (@id_usuario, @fecha, @id_pago, @id_prov, @desc);
                                 SELECT SCOPE_IDENTITY();";

                int idCompraRecienCreada;
                using (SqlCommand cmd = new SqlCommand(queryCabecera, conexion.Conectar, transaccion))
                {
                    cmd.Parameters.AddWithValue("@id_usuario", 1);
                    cmd.Parameters.AddWithValue("@fecha", dtpFechaPedido.SelectionStart);
                    cmd.Parameters.AddWithValue("@id_pago", cmbFormaPago.SelectedValue);
                    cmd.Parameters.AddWithValue("@id_prov", cmbProveedor.SelectedValue);

                    // CORRECCIÓN AQUÍ: Antes tenías txtPrecio.Text. 
                    // Debe ser el cuadro de texto donde escribes la nota de la compra.
                    cmd.Parameters.AddWithValue("@desc", txtNotaDetalle.Text);

                    idCompraRecienCreada = Convert.ToInt32(cmd.ExecuteScalar());
                }

                // 2. INSERTAR DETALLES Y ACTUALIZAR STOCK
                string queryDetalle = @"INSERT INTO Compra_producto (id_compra, id_producto, cantidad, precio_costo_unitario) 
                                VALUES (@idC, @idP, @cant, @precio)";

                string queryAsegurarInventario = @"IF NOT EXISTS (SELECT 1 FROM Inventario WHERE id_producto = @idP)
                                           BEGIN
                                               INSERT INTO Inventario (id_producto, stock) VALUES (@idP, 0)
                                           END";

                // Nueva consulta para SUMAR el stock
                string querySumarStock = "UPDATE Inventario SET stock = stock + @cant WHERE id_producto = @idP";

                foreach (DataGridViewRow fila in dgvProductosCompra.Rows)
                {
                    if (fila.Cells[0].Value != null)
                    {
                        int idProd = Convert.ToInt32(fila.Cells[0].Value);
                        int cant = Convert.ToInt32(fila.Cells[2].Value);
                        decimal precio = Convert.ToDecimal(fila.Cells[3].Value);

                        // Asegurar que el producto existe en inventario
                        using (SqlCommand cmdAsegurar = new SqlCommand(queryAsegurarInventario, conexion.Conectar, transaccion))
                        {
                            cmdAsegurar.Parameters.AddWithValue("@idP", idProd);
                            cmdAsegurar.ExecuteNonQuery();
                        }

                        // Insertar el detalle de la compra
                        using (SqlCommand cmdDetalle = new SqlCommand(queryDetalle, conexion.Conectar, transaccion))
                        {
                            cmdDetalle.Parameters.AddWithValue("@idC", idCompraRecienCreada);
                            cmdDetalle.Parameters.AddWithValue("@idP", idProd);
                            cmdDetalle.Parameters.AddWithValue("@cant", cant);
                            cmdDetalle.Parameters.AddWithValue("@precio", precio);
                            cmdDetalle.ExecuteNonQuery();
                        }
                    }
                }

                transaccion.Commit();
                MessageBox.Show("Compra #" + idCompraRecienCreada + " guardada y stock actualizado.", "Éxito");
                this.Close();
            }
            catch (Exception ex)
            {
                transaccion.Rollback();
                MessageBox.Show("Error crítico al guardar: " + ex.Message);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        private void kryptonButton5_Click(object sender, EventArgs e)
        {
            if (cmbProveedor.SelectedValue == null || cmbProveedor.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un proveedor primero para ver sus productos vinculados.",
                                "Proveedor Requerido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtenemos el ID del proveedor seleccionado
            int idProv = Convert.ToInt32(cmbProveedor.SelectedValue);

            // 2. Abrir el formulario hijo pasando el ID del proveedor al constructor
            using (var formularioHijo = new Agregar_Producto__Compras_(idProv))
            {
                if (formularioHijo.ShowDialog() == DialogResult.OK)
                {
                    // --- INICIO DE LÓGICA PARA PRODUCTO REPETIDO ---
                    // Usamos ToString() para asegurar que la comparación sea entre cadenas
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
                    // --- FIN DE LÓGICA PARA PRODUCTO REPETIDO ---

                    // 3. Calcular subtotal y agregar a la tabla
                    decimal subtotal = formularioHijo.CantidadSeleccionada * formularioHijo.PrecioSeleccionado;

                    dgvProductosCompra.Rows.Add(
                        formularioHijo.IdSeleccionado,
                        formularioHijo.NombreSeleccionado,
                        formularioHijo.CantidadSeleccionada,
                        formularioHijo.PrecioSeleccionado,
                        subtotal
                    );

                    // 4. LÓGICA DE BLOQUEO: Al agregar el primer producto, congelamos el proveedor
                    if (dgvProductosCompra.Rows.Count > 0)
                    {
                        cmbProveedor.Enabled = false;
                    }

                    ActualizarGranTotal();
                }
            }
        }

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

        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void kryptonLabel8_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }

        private void btnEliminarProducto_Click(object sender, EventArgs e)
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

        // Variable a nivel de clase para el respaldo
        private object valorOriginal;

        private void dgvProductosCompra_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // SOLO guardamos el valor actual antes de que cambie
            if (e.RowIndex >= 0 && (e.ColumnIndex == 2 || e.ColumnIndex == 3))
            {
                valorOriginal = dgvProductosCompra.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            }
        }

        private void dgvProductosCompra_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // Validamos solo cuando el cambio ocurre en Cantidad (2) o Precio (3)
            if (e.RowIndex >= 0 && (e.ColumnIndex == 2 || e.ColumnIndex == 3))
            {
                var fila = dgvProductosCompra.Rows[e.RowIndex];

                // Intentamos obtener el nuevo valor ingresado
                decimal valorNuevo;
                bool esNumerico = decimal.TryParse(fila.Cells[e.ColumnIndex].Value?.ToString(), out valorNuevo);

                if (!esNumerico || valorNuevo <= 0)
                {
                    string campo = (e.ColumnIndex == 2) ? "La cantidad" : "El precio";
                    MessageBox.Show($"{campo} no puede ser cero o menor.",
                                    "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Desconectamos el evento para evitar recursividad infinita al restaurar
                    dgvProductosCompra.CellValueChanged -= dgvProductosCompra_CellValueChanged;
                    fila.Cells[e.ColumnIndex].Value = valorOriginal;
                    dgvProductosCompra.CellValueChanged += dgvProductosCompra_CellValueChanged;
                    return;
                }

                // Si el valor es válido, procedemos al cálculo normal
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

        private void dgvProductosCompra_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return; // Salimos del método sin hacer nada
            }
        }
    }
}