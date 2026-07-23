using Microsoft.Data.SqlClient;
using SG_BAMS.Administracion_de_BAMS.MarcaProd;
using SG_BAMS.Bitacora;
using SG_BAMS.Login;
using SG_BAMS.ProductoInventario;
using SG_BAMS.ProductoInventario.DTO;
using SG_BAMS.Proveedor;
using SG_BAMS.Reporte;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class InventarioAdmin : Form
    {
        /// <summary>
        /// La lógica de negocio
        /// </summary>
        ClsProducto logica = new ClsProducto();

        /// <summary>
        /// Texto del placeholder para el campo de búsqueda
        /// </summary>
        private string placeholderTexto = "Buscar por nombre del producto...";

        /// <summary>
        /// Color del texto placeholder
        /// </summary>
        private Color placeholderColor = Color.Gray;

        /// <summary>
        /// Color del texto normal
        /// </summary>
        private Color textoColor = Color.Black;

        /// <summary>
        /// La última tecla del escáner
        /// </summary>
        private DateTime ultimaTeclaEscaner = DateTime.Now;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="InventarioAdmin" />.
        /// </summary>
        public InventarioAdmin()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.KeyPreview = true;
            ConfigurarPlaceholder();

        }

        /// <summary>
        /// Configura el placeholder en el TextBox de búsqueda.
        /// </summary>
        private void ConfigurarPlaceholder()
        {
            textoColor = txtBuscar.ForeColor;
            placeholderColor = Color.Gray;

            txtBuscar.Text = placeholderTexto;
            txtBuscar.ForeColor = placeholderColor;


            txtBuscar.Enter += txtBuscar_Enter;
            txtBuscar.Leave += txtBuscar_Leave;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
        }

        /// <summary>
        /// Maneja el evento Enter del TextBox de búsqueda.
        /// </summary>
        private void txtBuscar_Enter(object sender, EventArgs e)
        {
            if (txtBuscar.Text == placeholderTexto)
            {
                txtBuscar.Text = "";
                txtBuscar.ForeColor = textoColor;
            }
        }

        /// <summary>
        /// Maneja el evento Leave del TextBox de búsqueda.
        /// </summary>
        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                txtBuscar.Text = placeholderTexto;
                txtBuscar.ForeColor = placeholderColor;
            }
        }

        /// <summary>
        /// Carga el inventario completo.
        /// </summary>
        public void CargarInventarioCompleto()
        {
            try
            {
                dgvProductosAdmin.DataSource = logica.MostrarProductosCompleto();
                dgvProductosAdmin.ReadOnly = true;
                dgvProductosAdmin.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvProductosAdmin.AllowUserToAddRows = false;
                dgvProductosAdmin.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                if (dgvProductosAdmin.Columns.Contains("Producto"))
                {
                    dgvProductosAdmin.Columns["Producto"].MinimumWidth = 150;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el inventario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Maneja el evento Load del control InventarioAdmin.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void InventarioAdmin_Load(object sender, EventArgs e)
        {
            btnInventario.Enabled = false;
            btnInventario.BackColor = Color.SkyBlue;
            btnInventario.ForeColor = Color.White;



            CargarInventarioCompleto();

            dgvProductosAdmin.BorderStyle = BorderStyle.None;
            dgvProductosAdmin.BackgroundColor = Color.White;
            dgvProductosAdmin.RowHeadersVisible = false;
            dgvProductosAdmin.EnableHeadersVisualStyles = false;
            dgvProductosAdmin.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgvProductosAdmin.ColumnHeadersDefaultCellStyle.BackColor = Color.SkyBlue;
            dgvProductosAdmin.ColumnHeadersDefaultCellStyle.ForeColor = Color.Navy;
            dgvProductosAdmin.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvProductosAdmin.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvProductosAdmin.ColumnHeadersHeight = 28;

            dgvProductosAdmin.DefaultCellStyle.BackColor = Color.White;
            dgvProductosAdmin.DefaultCellStyle.ForeColor = Color.Navy;
            dgvProductosAdmin.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvProductosAdmin.DefaultCellStyle.Padding = new Padding(3);
            dgvProductosAdmin.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 245, 255);
            dgvProductosAdmin.AlternatingRowsDefaultCellStyle.ForeColor = Color.Navy;

            dgvProductosAdmin.DefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            dgvProductosAdmin.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvProductosAdmin.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvProductosAdmin.GridColor = Color.LightGray;
            dgvProductosAdmin.RowTemplate.Height = 32;
            dgvProductosAdmin.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProductosAdmin.ClearSelection();

            dgvProductosAdmin.CellFormatting += dgvProductosAdmin_CellFormatting;
        }

        /// <summary>
        /// Maneja el evento Click del control btnAgregar.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarProducto frm = new AgregarProducto();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                CargarInventarioCompleto();
            }

            dgvProductosAdmin.ClearSelection();
        }

        /// <summary>
        /// Maneja el evento Click del control kryptonButton10.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void kryptonButton10_Click(object sender, EventArgs e)
        {
            if (dgvProductosAdmin.SelectedRows.Count > 0)
            {
                ModificarProducto frmMod = new ModificarProducto(ArmarProductoDTODesdeFila());

                if (frmMod.ShowDialog() == DialogResult.OK)
                {
                    CargarInventarioCompleto();
                }

                dgvProductosAdmin.ClearSelection();
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una fila para modificar.", "BAMS");
            }
        }

        /// <summary>
        /// Arma un ProductoDTO a partir de la fila actualmente seleccionada en el grid.
        /// Reemplaza el llenado manual de campos sueltos del formulario ModificarProducto.
        /// </summary>
        private ProductoDTO ArmarProductoDTODesdeFila()
        {
            string precio = dgvProductosAdmin.CurrentRow.Cells["Precio Venta"].Value.ToString();

            var dto = new ProductoDTO
            {
                IdProducto = Convert.ToInt32(dgvProductosAdmin.CurrentRow.Cells["ID"].Value),
                Nombre = dgvProductosAdmin.CurrentRow.Cells["Producto"].Value.ToString(),
                Precio = Convert.ToDecimal(precio.Replace("L.", "").Trim()),
                CodigoBarra = dgvProductosAdmin.CurrentRow.Cells["Codigo Barra"].Value.ToString(),
                ProveedorActual = dgvProductosAdmin.CurrentRow.Cells["Proveedor"].Value.ToString(),
                MarcaActual = dgvProductosAdmin.CurrentRow.Cells["Marca"].Value.ToString(),
                TipoActual = dgvProductosAdmin.CurrentRow.Cells["Tipo"].Value.ToString(),
                ModeloActual = dgvProductosAdmin.CurrentRow.Cells["Modelo Auto"].Value.ToString(),
                EstadoActual = dgvProductosAdmin.CurrentRow.Cells["Estado"].Value.ToString()
            };

            if (dgvProductosAdmin.CurrentRow.Cells["Stock Actual"].Value != DBNull.Value)
            {
                dto.Stock = Convert.ToInt32(dgvProductosAdmin.CurrentRow.Cells["Stock Actual"].Value);
            }

            return dto;
        }

        /// <summary>
        /// Maneja el evento TextChanged del control txtBuscar.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {

            if (txtBuscar.Text == placeholderTexto || txtBuscar.ForeColor == placeholderColor)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                CargarInventarioCompleto();
                return;
            }

            try
            {
                dgvProductosAdmin.DataSource = logica.BuscarProductos(txtBuscar.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar: " + ex.Message, "BAMS");
            }
        }

        /// <summary>
        /// Maneja el evento Click del control btnNoti.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void btnNoti_Click(object sender, EventArgs e)
        {
            NotificacionesAdmin notificaciones = new NotificacionesAdmin();
            notificaciones.ShowDialog();
        }

        /// <summary>
        /// Maneja el evento CellDoubleClick del control dgvProductosAdmin.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="DataGridViewCellEventArgs" /> que contiene los datos del evento.</param>
        private void dgvProductosAdmin_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvProductosAdmin.SelectedRows.Count > 0)
            {
                ModificarProducto frmMod = new ModificarProducto(ArmarProductoDTODesdeFila());

                if (frmMod.ShowDialog() == DialogResult.OK)
                {
                    CargarInventarioCompleto();
                }

                dgvProductosAdmin.ClearSelection();
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una fila para modificar.", "BAMS");
            }
        }

        /// <summary>
        /// Procesa una tecla de comando.
        /// </summary>
        /// <param name="msg">Un <see cref="T:System.Windows.Forms.Message" />, pasado por referencia, que representa el mensaje Win32 a procesar.</param>
        /// <param name="keyData">Uno de los valores de <see cref="T:System.Windows.Forms.Keys" /> que representa la tecla a procesar.</param>
        /// <returns>
        ///   <see langword="true" /> si la pulsación de tecla fue procesada y consumida por el control; de lo contrario, <see langword="false" /> para permitir el procesamiento adicional.
        /// </returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys key = keyData & Keys.KeyCode;

            if ((key >= Keys.D0 && key <= Keys.Z) || (key >= Keys.NumPad0 && key <= Keys.NumPad9))
            {
                TimeSpan intervalo = DateTime.Now - ultimaTeclaEscaner;
                ultimaTeclaEscaner = DateTime.Now;

                if (intervalo.TotalMilliseconds < 50 || !txtBuscar.Focused)
                {

                    txtBuscar.Text = string.Empty;
                    txtBuscar.ForeColor = textoColor;

                    if (!txtBuscar.Focused) txtBuscar.Focus();

                    char c = (char)key;
                    txtBuscar.AppendText(c.ToString().ToLower());

                    return true;
                }
            }

            if (key == Keys.Enter)
            {
                if (txtBuscar.Focused && !string.IsNullOrWhiteSpace(txtBuscar.Text) && txtBuscar.Text != placeholderTexto)
                {
                    dgvProductosAdmin.DataSource = logica.BuscarProductos(txtBuscar.Text.Trim());
                    txtBuscar.SelectAll();

                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Maneja el evento Click del control btnMenu.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnMenu_Click(object sender, EventArgs e)
        {
            MenuPrincipalAdm MPA = new MenuPrincipalAdm();
            MPA.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del control btnFacturas.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnFacturas_Click(object sender, EventArgs e)
        {
            FacturasAdm FA = new FacturasAdm();
            FA.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del control btnCompra.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnCompra_Click(object sender, EventArgs e)
        {
            Compras CF = new Compras();
            CF.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del control btnClientes.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnClientes_Click(object sender, EventArgs e)
        {
            ClientesAdm CA = new ClientesAdm();
            CA.Show();
            this.Hide();
        }


        /// <summary>
        /// Maneja el evento Click del control btnProveedores.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnProveedores_Click(object sender, EventArgs e)
        {
            var PA = new ProveedoresAdmin(
                new ProveedorRepository(),
                new EstadoRepository(),
                new ClasificacionRepository());
            PA.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del control btnDeudores.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnDeudores_Click(object sender, EventArgs e)
        {
            DeudoresAdmin DA = new DeudoresAdmin();
            DA.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del control btnReportes.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnReportes_Click(object sender, EventArgs e)
        {
            ReportesAdmin RA = new ReportesAdmin();
            RA.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del control btnBitacora.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnBitacora_Click(object sender, EventArgs e)
        {
            var Bi = new BitacoraAdmin(
                new BitacoraRepository(),
                new FiltroBitacoraService(),
                new ReporteBitacoraPdfExportador());
            Bi.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del control btnCerrar.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
            "¿Está seguro que desea cerrar sesión?",
            "Confirmación",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                Login.Login login = new Login.Login();
                login.Show();
                this.Close();
            }
        }

        /// <summary>
        /// Maneja el evento Click del control btnPerfil.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnPerfil_Click(object sender, EventArgs e)
        {
            Perfil perfil = new Perfil();
            perfil.ShowDialog();
        }

        private void dgvProductosAdmin_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvProductosAdmin.Columns[e.ColumnIndex].Name == "Stock Actual" && e.Value != null)
            {
                if (int.TryParse(e.Value.ToString(), out int stock))
                {
                    if (stock < 1)
                    {
                        e.CellStyle.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
                        e.CellStyle.ForeColor = System.Drawing.Color.DarkRed;
                    }
                    else if (stock < 10)
                    {
                        e.CellStyle.BackColor = System.Drawing.Color.FromArgb(255, 224, 192);
                        e.CellStyle.ForeColor = System.Drawing.Color.Brown;
                    }
                    else
                    {
                        e.CellStyle.BackColor = System.Drawing.Color.FromArgb(192, 255, 192);
                        e.CellStyle.ForeColor = System.Drawing.Color.DarkGreen;
                    }
                }
            }
        }

        
    }
}