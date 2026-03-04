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
    public partial class Agregar_Producto__Compras_ : Form
    {
        public string IdSeleccionado { get; set; }
        public string NombreSeleccionado { get; set; }
        public int CantidadSeleccionada { get; set; }
        public decimal PrecioSeleccionado { get; set; }

        public Agregar_Producto__Compras_()
        {
            InitializeComponent();
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

                // Traemos el ID para guardar y el Nombre para mostrar
                // Filtramos por estado = 1 para que solo salgan productos activos
                string query = "SELECT id_producto, nombre_producto FROM Producto WHERE id_estado = 1";

                using (SqlCommand cmd = new SqlCommand(query, conexion.Conectar))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }

                // Configuración del ComboBox
                cmbProductos.DataSource = dt;
                cmbProductos.DisplayMember = "nombre_producto"; // Lo que el usuario ve
                cmbProductos.ValueMember = "id_producto";       // El ID que usaremos para el INSERT

                // Opcional: Que empiece vacío para obligar a seleccionar
                cmbProductos.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message);
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            // Validar que seleccionó un producto y puso precio
            if (cmbProductos.SelectedIndex == -1 || string.IsNullOrEmpty(txtPrecio.Text))
            {
                MessageBox.Show("Por favor llene todos los campos");
                return;
            }

            // Guardamos los datos en las propiedades públicas
            IdSeleccionado = cmbProductos.SelectedValue.ToString();
            NombreSeleccionado = cmbProductos.Text;
            CantidadSeleccionada = (int)numCantidad.Value; // Asumiendo que usas un NumericUpDown
            PrecioSeleccionado = Convert.ToDecimal(txtPrecio.Text);

            // Indicamos que el usuario aceptó y cerramos
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
                // Lo mostramos como diálogo para esperar a que termine de crear
                if (frmCrear.ShowDialog() == DialogResult.OK)
                {
                    // 2. Si se creó con éxito, refrescamos el combo de la ventana anterior
                    // para que el nuevo producto ya aparezca disponible para comprar
                    LlenarComboProductos();
                    MessageBox.Show("¡Producto registrado! Ya puede seleccionarlo en la lista.");
                }
                else
                {
                    // Por si acaso cerró la ventana, refrescamos igual para ver cambios
                    LlenarComboProductos();
                }
            }
        }
    }
}
