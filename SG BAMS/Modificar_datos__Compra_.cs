using Microsoft.Data.SqlClient;
using SG_BAMS.ProductoInventario;
using System;
using System.Collections.Generic; // Necesario para List<>
using System.Data;
using System.Windows.Forms;

namespace SG_BAMS
{
    public partial class Modificar_datos__Compra_ : Form
    {
        private int idCompraAEditar;
        private ClsModificarCompras logic = new ClsModificarCompras();

        // LISTA NEGRA: Para recordar qué productos borrar de la base de datos
        private List<int> listaEliminados = new List<int>();

        public Modificar_datos__Compra_(int id)
        {
            InitializeComponent();
            this.idCompraAEditar = id;
        }

        private void Modificar_datos__Compra__Load(object sender, EventArgs e)
        {
            LlenarCombos();
            ClsDetalleCompra objetoDetalle = new ClsDetalleCompra();
            dgvProductosCompraMod.DataSource = objetoDetalle.ListarProductosDeCompra(idCompraAEditar);
            CargarDatosCabecera();
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

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. ELIMINAR: Procesamos los productos que el usuario quitó de la tabla
                foreach (int idEliminado in listaEliminados)
                {
                    logic.EliminarProductoDeBD(idCompraAEditar, idEliminado);
                }

                // 2. ACTUALIZAR/INSERTAR: Procesamos lo que quedó en el Grid
                foreach (DataGridViewRow fila in dgvProductosCompraMod.Rows)
                {
                    // Verificamos que la fila tenga un ID (que no sea la fila vacía del final)
                    if (fila.Cells["ID"].Value != null && fila.Cells["ID"].Value != DBNull.Value)
                    {
                        int idProd = Convert.ToInt32(fila.Cells["ID"].Value);
                        int cant = Convert.ToInt32(fila.Cells["Cantidad"].Value);
                        decimal precio = Convert.ToDecimal(fila.Cells["Precio"].Value);

                        logic.GuardarCambiosDetalle(idCompraAEditar, idProd, cant, precio);
                    }
                }

                MessageBox.Show("¡Cambios guardados e Inventario sincronizado!");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message);
            }
        }

        private void btnEliminarProducto_Click(object sender, EventArgs e)
        {
            if (dgvProductosCompraMod.CurrentRow != null && !dgvProductosCompraMod.CurrentRow.IsNewRow)
            {
                DialogResult respuesta = MessageBox.Show("¿Está seguro de quitar este producto de la compra?",
                    "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    // Guardamos el ID en la lista negra para borrarlo de SQL al dar 'Aceptar'
                    int idAEliminar = Convert.ToInt32(dgvProductosCompraMod.CurrentRow.Cells["ID"].Value);
                    listaEliminados.Add(idAEliminar);

                    // Eliminación visual inmediata
                    dgvProductosCompraMod.Rows.RemoveAt(dgvProductosCompraMod.CurrentRow.Index);
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un producto de la tabla.");
            }
        }
    }
}