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
            // --- 1. VALIDACIÓN POR TOTAL ---
            decimal totalValidar = 0;
            if (!decimal.TryParse(lblTotal.Text, out totalValidar) || totalValidar <= 0)
            {
                MessageBox.Show("No se puede guardar una compra con total L. 0.00.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // --- 2. VALIDACIONES DE COMBOS ---
            if (cmbProveedor.SelectedValue == null || cmbFormaPago.SelectedValue == null)
            {
                MessageBox.Show("Por favor, seleccione un proveedor valido de la lista");
                return;
            }

            ClsConexion conexion = new ClsConexion();
            conexion.AbrirConexion();
            SqlTransaction transaccion = conexion.Conectar.BeginTransaction();

            try
            {
                // 3. INSERTAR CABECERA (Tabla Compra)
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
                    cmd.Parameters.AddWithValue("@desc", txtPrecio.Text);
                    idCompraRecienCreada = Convert.ToInt32(cmd.ExecuteScalar());
                }

                // 4. CONSULTAS PARA DETALLE E INVENTARIO
                string queryDetalle = @"INSERT INTO Compra_producto (id_compra, id_producto, cantidad, precio_costo_unitario) 
                        VALUES (@idC, @idP, @cant, @precio)";

                // Esta consulta asegura que el producto EXISTA en la tabla Inventario para que el TRIGGER funcione
                string queryAsegurarInventario = @"IF NOT EXISTS (SELECT 1 FROM Inventario WHERE id_producto = @idP)
                                         BEGIN
                                            INSERT INTO Inventario (id_producto, stock) VALUES (@idP, 0)
                                         END";

                foreach (DataGridViewRow fila in dgvProductosCompra.Rows)
                {
                    if (fila.Cells[0].Value != null)
                    {
                        int idProd = Convert.ToInt32(fila.Cells[0].Value);
                        int cant = Convert.ToInt32(fila.Cells[2].Value);
                        decimal precio = Convert.ToDecimal(fila.Cells[3].Value);

                        // PASO A: Asegurar que el registro exista en Inventario (sin sumar nada aún)
                        using (SqlCommand cmdAsegurar = new SqlCommand(queryAsegurarInventario, conexion.Conectar, transaccion))
                        {
                            cmdAsegurar.Parameters.AddWithValue("@idP", idProd);
                            cmdAsegurar.ExecuteNonQuery();
                        }

                        // PASO B: Insertar el detalle. 
                        // Al existir ya el producto en Inventario, el Trigger 'tr_actualizar_stock_por_compra'
                        // ahora sí podrá hacer el UPDATE correctamente.
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
                MessageBox.Show("Compra #" + idCompraRecienCreada + " guardada. El stock se ha actualizado exitosamente.", "Éxito");
                this.Close();
            }
            catch (Exception ex)
            {
                transaccion.Rollback();
                MessageBox.Show("Error crítico: " + ex.Message);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        private void kryptonButton5_Click(object sender, EventArgs e)
        {
            using (var formularioHijo = new Agregar_Producto__Compras_())
            {
                if (formularioHijo.ShowDialog() == DialogResult.OK)
                {
                    decimal subtotal = formularioHijo.CantidadSeleccionada * formularioHijo.PrecioSeleccionado;

                    dgvProductosCompra.Rows.Add(
                        formularioHijo.IdSeleccionado,
                        formularioHijo.NombreSeleccionado,
                        formularioHijo.CantidadSeleccionada,
                        formularioHijo.PrecioSeleccionado,
                        subtotal
                    );

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

        // Métodos vacíos por si acaso diste doble clic accidental en el diseño
        private void kryptonLabel8_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
    }
}