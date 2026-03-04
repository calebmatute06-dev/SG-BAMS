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

        private void kryptonLabel8_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

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

                // 1. Cargar Formas de Pago (basado en tu tabla Tipo_Forma_de_pago)
                string qPago = "SELECT id_tipo_forma_pago, descripcion_forma_pago FROM Tipo_Forma_de_pago";
                SqlDataAdapter daPago = new SqlDataAdapter(qPago, conexion.Conectar);
                DataTable dtPago = new DataTable();
                daPago.Fill(dtPago);

                cmbFormaPago.DataSource = dtPago;
                cmbFormaPago.DisplayMember = "descripcion_forma_pago";
                cmbFormaPago.ValueMember = "id_tipo_forma_pago";

                // 2. Cargar Proveedores (basado en tu tabla Proveedor)
                string qProv = "SELECT id_proveedor, nombre_proveedor FROM Proveedor WHERE id_estado = 1";
                SqlDataAdapter daProv = new SqlDataAdapter(qProv, conexion.Conectar);
                DataTable dtProv = new DataTable();
                daProv.Fill(dtProv);

                cmbProveedor.DataSource = dtProv;
                cmbProveedor.DisplayMember = "nombre_proveedor";
                cmbProveedor.ValueMember = "id_proveedor";
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
            string proximoID = "1"; // Por si la tabla está vacía

            try
            {
                conexion.AbrirConexion();
                // Buscamos el ID más alto actualmente en la tabla Compra
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
            // 1. Validaciones básicas
            if (cmbProveedor.SelectedValue == null || cmbFormaPago.SelectedValue == null)
            {
                MessageBox.Show("Por favor, seleccione un proveedor y una forma de pago.");
                return;
            }

            ClsConexion conexion = new ClsConexion();
            try
            {
                conexion.AbrirConexion();

                // 2. La consulta SQL para insertar
                // Nota: id_compra no se incluye porque es IDENTITY (automático)
                string query = @"INSERT INTO Compra (id_usuario, fecha_pedido, id_tipo_forma_pago, id_proveedor, desc_compra) 
                         VALUES (@id_usuario, @fecha, @id_pago, @id_prov, @desc)";

                using (SqlCommand cmd = new SqlCommand(query, conexion.Conectar))
                {
                    // Parámetros
                    // Aquí deberías usar el ID del usuario que inició sesión. 
                    // Si no lo tienes aún, usaremos el '1' por defecto para las pruebas.
                    cmd.Parameters.AddWithValue("@id_usuario", 1);

                    // Tomamos la fecha del calendario de Krypton
                    cmd.Parameters.AddWithValue("@fecha", dtpFechaPedido.SelectionStart);

                    // Tomamos los IDs de los ComboBox
                    cmd.Parameters.AddWithValue("@id_pago", cmbFormaPago.SelectedValue);
                    cmd.Parameters.AddWithValue("@id_prov", cmbProveedor.SelectedValue);

                    // Descripción (si tienes un textbox para notas, úsalo aquí)
                    cmd.Parameters.AddWithValue("@desc", txtDescripcion.Text);

                    // 3. Ejecutar
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("¡Compra guardada exitosamente!");

                    // Cerramos el formulario para volver a la tabla general
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar la compra: " + ex.Message);
            }
            finally
            {
                conexion.Cerrar();
            }
        }
    }
}
