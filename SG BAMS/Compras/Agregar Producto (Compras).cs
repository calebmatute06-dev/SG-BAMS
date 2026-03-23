using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS
{
    public partial class Agregar_Producto__Compras_ : Form
    {
        public string IdSeleccionado { get; set; }
        public string NombreSeleccionado { get; set; }
        public int CantidadSeleccionada { get; set; }
        public decimal PrecioSeleccionado { get; set; }

        private int _idProveedor;


        public Agregar_Producto__Compras_(int idProv)
        {
            InitializeComponent();
            this._idProveedor = idProv;

            txtPrecio.KeyPress += (s, e) => ClsValidaciones.ValidarDecimales(txtPrecio, e);
        }

        private void kryptonLabel1_Click(object sender, EventArgs e)
        {

        }

        private void kryptonLabel4_Click(object sender, EventArgs e)
        {

        }

        private void Agregar_Producto__Compras__Load(object sender, EventArgs e)
        {
            LlenarComboProductos();
        }

        private void LlenarComboProductos()
        {
            ClsConexion conexion = new ClsConexion();
            DataTable dt = new DataTable();

            try
            {
                conexion.AbrirConexion();

                string query = @"SELECT p.id_producto, p.nombre_producto 
                 FROM Producto p
                 INNER JOIN Proveedor_Producto pp ON p.id_producto = pp.id_producto
                 WHERE p.id_estado = 1 AND pp.id_proveedor = @idProv";

                using (SqlCommand cmd = new SqlCommand(query, conexion.Conectar))
                {
                    cmd.Parameters.AddWithValue("@idProv", _idProveedor);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }

                cmbProductos.DataSource = dt;
                cmbProductos.DisplayMember = "nombre_producto";
                cmbProductos.ValueMember = "id_producto";

                cmbProductos.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al filtrar productos por proveedor: " + ex.Message);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            if (cmbProductos.SelectedValue == null || cmbProductos.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un producto de la lista.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (numCantidad.Value <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor a cero.", "Cantidad Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numCantidad.Focus();
                return;
            }

            if (!ClsValidaciones.EsNumeroDecimalValido(txtPrecio, "El precio", out decimal precioAux))
            {
                return;
            }

            IdSeleccionado = cmbProductos.SelectedValue.ToString();
            NombreSeleccionado = cmbProductos.Text;
            CantidadSeleccionada = (int)numCantidad.Value;
            PrecioSeleccionado = precioAux;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            using (AgregarProducto frmCrear = new AgregarProducto())
            {
                if (frmCrear.ShowDialog() == DialogResult.OK)
                {
                    LlenarComboProductos();
                    MessageBox.Show("¡Producto registrado! Ya puede seleccionarlo en la lista.");
                }
                else
                {
                    LlenarComboProductos();
                }
            }
        }
    }
}