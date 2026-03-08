using Microsoft.Data.SqlClient;
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
    public partial class InventarioEmp : Form
    {
        public InventarioEmp()
        {
            InitializeComponent();
        }

        private void InventarioEmp_Load(object sender, EventArgs e)
        {
            MostrarInventarioEmpleado();
        }

        public void MostrarInventarioEmpleado()
        {
            ClsConexion conexion = new ClsConexion();
            DataTable dt = new DataTable();

            try
            {
                conexion.AbrirConexion();

                string query = @"SELECT 
                            p.id_producto AS ID, 
                            p.nombre_producto AS Producto, 
                            m.nombre_marca AS Marca, 
                            t.descripcion_forma_pago AS Tipo, 
                            mo.nombre_modelo_auto AS Modelo, 
                            e.descripcion_estado AS Estado,
                            p.precio_venta AS Precio,
                            p.descripcion_tipo_servicio AS Servicio,
                            p.codigo_barra AS [Código de Barra]
                         FROM Producto p
                         INNER JOIN Marca_producto m ON p.id_marca_producto = m.id_marca_producto
                         INNER JOIN Tipo_producto t ON p.id_tipo_producto = t.id_tipo_producto
                         INNER JOIN Modelo_de_auto mo ON p.id_modelo_auto = mo.id_modelo_auto
                         INNER JOIN Estado e ON p.id_estado = e.id_estado";

                using (SqlCommand cmd = new SqlCommand(query, conexion.Conectar))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }

                dgvInventarioEmp.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar inventario: " + ex.Message);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                MostrarInventarioEmpleado();
                return;
            }

            ClsConexion conexion = new ClsConexion();
            DataTable dt = new DataTable();

            try
            {
                conexion.AbrirConexion();

                string query = @"SELECT 
                            p.id_producto AS ID, 
                            p.nombre_producto AS Producto, 
                            m.nombre_marca AS Marca, 
                            t.descripcion_forma_pago AS Tipo, 
                            mo.nombre_modelo_auto AS Modelo, 
                            e.descripcion_estado AS Estado,
                            p.precio_venta AS Precio,
                            p.descripcion_tipo_servicio AS Servicio,
                            p.codigo_barra AS [Código de Barra]
                         FROM Producto p
                         INNER JOIN Marca_producto m ON p.id_marca_producto = m.id_marca_producto
                         INNER JOIN Tipo_producto t ON p.id_tipo_producto = t.id_tipo_producto
                         INNER JOIN Modelo_de_auto mo ON p.id_modelo_auto = mo.id_modelo_auto
                         INNER JOIN Estado e ON p.id_estado = e.id_estado
                         WHERE p.nombre_producto LIKE '%' + @filtro + '%' 
                         OR p.codigo_barra LIKE '%' + @filtro + '%'";

                using (SqlCommand cmd = new SqlCommand(query, conexion.Conectar))
                {
                    cmd.Parameters.AddWithValue("@filtro", txtBuscar.Text.Trim());

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }

                dgvInventarioEmp.DataSource = dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en búsqueda empleado: " + ex.Message);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvInventarioEmp_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
