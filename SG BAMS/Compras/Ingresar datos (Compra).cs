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

        private string ObtenerSiguienteID()
        {
            try
            {
                ClsCargaCombos carga = new ClsCargaCombos();
                return carga.SugerirSiguienteID();
            }
            catch (Exception ex)
            {
                return "1";
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // 1. Validaciones de Interfaz
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
                // 2. Llenar la lista de detalles desde el DataGridView
                List<DetalleCompra> listaDetalles = new List<DetalleCompra>();

                foreach (DataGridViewRow fila in dgvProductosCompra.Rows)
                {
                    if (fila.Cells[0].Value != null) // Aseguramos que la fila no esté vacía
                    {
                        listaDetalles.Add(new DetalleCompra
                        {
                            IdProducto = Convert.ToInt32(fila.Cells[0].Value),
                            Cantidad = Convert.ToInt32(fila.Cells[2].Value),
                            Precio = Convert.ToDecimal(fila.Cells[3].Value)
                        });
                    }
                }

                // 3. Llamar a la lógica para guardar
                ClsCompras logic = new ClsCompras();

                // El ID de usuario lo dejamos en 1 por ahora (puedes cambiarlo luego por el del login)
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
                    this.Close(); // Cerramos el formulario al terminar
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al procesar la compra: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
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