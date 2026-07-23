using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using SG_BAMS.Administracion_de_BAMS.FormaPago;
using SG_BAMS.ComprasContratos;
using SG_BAMS.ComprasDTO;
using SG_BAMS.Login;
using SG_BAMS.Proveedor;

namespace SG_BAMS
{
    public partial class Ingresar_datos__Compra_ : Form
    {
        private readonly IComprasRepository logic;
        private readonly ICargaCombosRepository combos;
        private readonly NavegacionService _navegacion;
        private PlaceholderTextBox phNotaDetalle;
        private PlaceholderComboBox phProveedor;
        private PlaceholderComboBox phFormaPago;
        private object valorOriginal;

        public Ingresar_datos__Compra_() : this(new ClsCompras(), new ClsCargaCombos(), new NavegacionService()) { }

        public Ingresar_datos__Compra_(IComprasRepository logic, ICargaCombosRepository combos, NavegacionService navegacion)
        {
            this.logic = logic;
            this.combos = combos;
            this._navegacion = navegacion;
            InitializeComponent();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void Ingresar_datos__Compra__Load(object sender, EventArgs e)
        {
            dgvIngresarCompra.Columns.Clear();
            dtpFechaPedido.Enabled = false;
            dgvIngresarCompra.Columns.Add("ID", "ID");
            dgvIngresarCompra.Columns.Add("Nombre", "Nombre");
            dgvIngresarCompra.Columns.Add("Cantidad", "Cantidad");
            dgvIngresarCompra.Columns.Add("Precio", "Precio");
            dgvIngresarCompra.Columns.Add("Subtotal", "Subtotal");
            dgvIngresarCompra.Columns[0].ReadOnly = true;
            dgvIngresarCompra.Columns[1].ReadOnly = true;
            dgvIngresarCompra.Columns[4].ReadOnly = true;
            dgvIngresarCompra.Columns[2].ReadOnly = false;
            dgvIngresarCompra.Columns[3].ReadOnly = false;

            EstiloDataGridView.Aplicar(dgvIngresarCompra);

            LlenarCombos();

            dtpFechaPedido.Value = DateTime.Now;
            lblIDCompra.Text = ObtenerSiguienteID();

            dgvIngresarCompra.CellFormatting += (s, ev) =>
            {
                if (ev.RowIndex < 0 || ev.Value == null) return;
                string col = dgvIngresarCompra.Columns[ev.ColumnIndex].Name;
                if ((col == "Precio" || col == "Subtotal") &&
                    decimal.TryParse(ev.Value.ToString(), out decimal monto))
                {
                    ev.Value = $"L. {monto:N2}";
                    ev.FormattingApplied = true;
                }
            };

            phNotaDetalle = new PlaceholderTextBox(txtNotaDetalle, "Nota opcional...");
        }

        private void LlenarCombos()
        {
            try
            {
                cmbFormaPago.DataSource = combos.ListarFormasPago();
                cmbFormaPago.DisplayMember = "descripcion_forma_pago";
                cmbFormaPago.ValueMember = "id_tipo_forma_pago";

                cmbProveedor.DataSource = combos.ListarProveedoresActivos();
                cmbProveedor.DisplayMember = "nombre_proveedor";
                cmbProveedor.ValueMember = "id_proveedor";
                cmbProveedor.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbProveedor.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmbProveedor.DropDownStyle = ComboBoxStyle.DropDown;
                cmbProveedor.SelectedIndex = -1;

                phProveedor = new PlaceholderComboBox(cmbProveedor, "Seleccione un proveedor");
                phFormaPago = new PlaceholderComboBox(cmbFormaPago, "Seleccione una forma de pago");
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
                return combos.SugerirSiguienteID();
            }
            catch (Exception)
            {
                return "1";
            }
        }

        private void ActualizarGranTotal()
        {
            decimal granTotal = 0;
            foreach (DataGridViewRow fila in dgvIngresarCompra.Rows)
            {
                if (fila.Cells[4].Value != null)
                    granTotal += Convert.ToDecimal(fila.Cells[4].Value);
            }
            lblTotal.Text = $"L. {granTotal:N2}";
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (phProveedor.IsPlaceholderActive || cmbProveedor.SelectedIndex == -1)
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
                    foreach (DataGridViewRow fila in dgvIngresarCompra.Rows)
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

                    dgvIngresarCompra.Rows.Add(
                        formularioHijo.IdSeleccionado,
                        formularioHijo.NombreSeleccionado,
                        formularioHijo.CantidadSeleccionada,
                        formularioHijo.PrecioSeleccionado,
                        subtotal
                    );

                    if (dgvIngresarCompra.Rows.Count > 0)
                        cmbProveedor.Enabled = false;

                    ActualizarGranTotal();
                }
            }
        }

        private void btnAceptar_Click_1(object sender, EventArgs e)
        {
            int filasConDatos = 0;
            foreach (DataGridViewRow fila in dgvIngresarCompra.Rows)
            {
                if (!fila.IsNewRow && fila.Cells[0].Value != null)
                    filasConDatos++;
            }

            bool proveedorSeleccionado = !phProveedor.IsPlaceholderActive && cmbProveedor.SelectedIndex != -1;
            bool formaPagoSeleccionada = !phFormaPago.IsPlaceholderActive && cmbFormaPago.SelectedIndex != -1;

            var validacion = ComprasDominio.ValidarNuevaCompra(filasConDatos, proveedorSeleccionado, formaPagoSeleccionada);
            if (!validacion.EsValido)
            {
                MessageBox.Show(validacion.Mensaje, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                CompraDTO compraDTO = new CompraDTO
                {
                    IdProveedor = Convert.ToInt32(cmbProveedor.SelectedValue),
                    IdFormaPago = Convert.ToInt32(cmbFormaPago.SelectedValue),
                    Fecha = dtpFechaPedido.Value,
                    Nota = phNotaDetalle.GetRealValue()
                };

                foreach (DataGridViewRow fila in dgvIngresarCompra.Rows)
                {
                    if (!fila.IsNewRow && fila.Cells[0].Value != null)
                    {
                        compraDTO.Detalle.Add(new DetalleCompraDTO
                        {
                            IdProducto = Convert.ToInt32(fila.Cells[0].Value),
                            Cantidad = Convert.ToInt32(fila.Cells[2].Value),
                            Precio = Convert.ToDecimal(fila.Cells[3].Value)
                        });
                    }
                }

                bool exito = logic.GuardarNuevaCompra(compraDTO);

                if (exito)
                {
                    MessageBox.Show("La compra se registró correctamente y el inventario fue actualizado.",
                        "BAMS - Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al procesar la compra: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e) => this.Close();

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (dgvIngresarCompra.CurrentRow != null && dgvIngresarCompra.CurrentRow.Index >= 0)
            {
                DialogResult respuesta = MessageBox.Show("¿Está seguro de que desea quitar este producto de la lista?",
                    "Eliminar Producto", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    dgvIngresarCompra.Rows.RemoveAt(dgvIngresarCompra.CurrentRow.Index);
                    ActualizarGranTotal();
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione el producto que desea eliminar de la tabla.",
                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (dgvIngresarCompra.Rows.Count == 0)
                cmbProveedor.Enabled = true;
        }

        private void dgvIngresarCompra_CellValueChanged_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && (e.ColumnIndex == 2 || e.ColumnIndex == 3))
            {
                var fila = dgvIngresarCompra.Rows[e.RowIndex];
                decimal valorNuevo;
                bool esNumerico = decimal.TryParse(fila.Cells[e.ColumnIndex].Value?.ToString(), out valorNuevo);
                string campo = (e.ColumnIndex == 2) ? "La cantidad" : "El precio";

                var validacion = ComprasDominio.ValidarCantidadOPrecio(campo, esNumerico ? valorNuevo : 0);
                if (!validacion.EsValido)
                {
                    MessageBox.Show(validacion.Mensaje, "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    dgvIngresarCompra.CellValueChanged -= dgvIngresarCompra_CellValueChanged_1;
                    fila.Cells[e.ColumnIndex].Value = valorOriginal;
                    dgvIngresarCompra.CellValueChanged += dgvIngresarCompra_CellValueChanged_1;
                    return;
                }

                try
                {
                    decimal cant = Convert.ToDecimal(fila.Cells[2].Value ?? 0);
                    decimal prec = Convert.ToDecimal(fila.Cells[3].Value ?? 0);
                    decimal subtotal = cant * prec;
                    fila.Cells[4].Value = subtotal;
                    ActualizarGranTotal();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al calcular el subtotal: " + ex.Message);
                }
            }
        }

        private void dgvIngresarCompra_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex >= 0 && (e.ColumnIndex == 2 || e.ColumnIndex == 3))
            {
                valorOriginal = dgvIngresarCompra.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            }
        }
    }
}