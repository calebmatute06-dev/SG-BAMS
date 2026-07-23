using SG_BAMS.Administracion_de_BAMS.FormaPago;
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
    /// <summary>
    /// Representa la interfaz de usuario para la visualización y administración de las formas de pago.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class frmFormaPago : Form
    {
        /// <summary>
        /// Instancia de la clase lógica de negocio para las formas de pago.
        /// </summary>
        clsFormaPago objetoFP = new clsFormaPago();

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="frmFormaPago"/>.
        /// </summary>
        public frmFormaPago()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
           
        }

        /// <summary>
        /// Maneja el evento de carga inicial para llenar el listado de formas de pago de manera asíncrona.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        /// 
        /// <summary>
        /// Carga los datos desde la base de datos al control DataGridView de forma asíncrona.
        /// </summary>
        private async Task CargarGridFormasPago()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                DataTable dt = await objetoFP.LeerFormasPagoAsync();

                dgvFormasPago.DataSource = dt;

                ConfigurarDisenoGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error de Carga",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Aplica configuraciones visuales, encabezados y visibilidad de columnas al DataGridView.
        /// </summary>
        private void ConfigurarDisenoGrid()
        {
            if (dgvFormasPago.Columns.Contains("id_tipo_forma_pago"))
                dgvFormasPago.Columns["id_tipo_forma_pago"].Visible = false;

            if (dgvFormasPago.Columns.Contains("descripcion_forma_pago"))
                dgvFormasPago.Columns["descripcion_forma_pago"].HeaderText = "Método de Pago";

            if (dgvFormasPago.Columns.Contains("total_uso_facturas"))
                dgvFormasPago.Columns["total_uso_facturas"].HeaderText = "Uso en Facturas";

            dgvFormasPago.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvFormasPago.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvFormasPago.AllowUserToAddRows = false;
            dgvFormasPago.ReadOnly = true;
            dgvFormasPago.ClearSelection();
        }

        private void AbrirOEnfocarDialogo<T>(Func<T> creadorFormulario, Action<T> accionesPostDialogo) where T : Form
        {
            T formExistente = Application.OpenForms.Cast<Form>().OfType<T>().FirstOrDefault();

            if (formExistente != null)
            {
                if (formExistente.WindowState == FormWindowState.Minimized)
                {
                    formExistente.WindowState = FormWindowState.Normal;
                }
                formExistente.BringToFront();
                formExistente.Focus();
            }
            else
            {
                using (T nuevoForm = creadorFormulario())
                {
                    if (nuevoForm.ShowDialog() == DialogResult.OK)
                    {
                        accionesPostDialogo(nuevoForm);
                    }
                }
            }
        }


        /// <summary>
        /// Maneja el evento de clic para abrir el formulario de creación de una nueva forma de pago.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btmAgregar2_Click(object sender, EventArgs e)
        {
            AbrirOEnfocarDialogo(
                () => new frmAgregarFormaPago(),
                (f) => _ = CargarGridFormasPago()
            );
            dgvFormasPago.ClearSelection();
        }

        /// <summary>
        /// Permite editar una forma de pago al realizar doble clic sobre una celda del listado.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="DataGridViewCellEventArgs"/> que contiene los datos del evento.</param>
        private void dgvFormasPago_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvFormasPago.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvFormasPago.CurrentRow.Cells["id_tipo_forma_pago"].Value);
                string descripcion = dgvFormasPago.CurrentRow.Cells["descripcion_forma_pago"].Value.ToString();

                AbrirOEnfocarDialogo(
                    () => new frmModificarFormaPago(id, descripcion),
                    (f) => _ = CargarGridFormasPago()
                );
                dgvFormasPago.ClearSelection();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila para editar.");
            }
        }

        /// <summary>
        /// Abre la ventana para agregar un nuevo registro de forma de pago.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AbrirOEnfocarDialogo(
                () => new frmAgregarFormaPago(),
                (f) => _ = CargarGridFormasPago()
            );
            dgvFormasPago.ClearSelection();
        }

        /// <summary>
        /// Abre la ventana de modificación para el elemento seleccionado en la lista.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvFormasPago.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvFormasPago.CurrentRow.Cells["id_tipo_forma_pago"].Value);
                string descripcion = dgvFormasPago.CurrentRow.Cells["descripcion_forma_pago"].Value.ToString();

                AbrirOEnfocarDialogo(
                    () => new frmModificarFormaPago(id, descripcion),
                    (f) => _ = CargarGridFormasPago()
                );
                dgvFormasPago.ClearSelection();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila para editar.");
            }
        }

        /// <summary>
        /// Cierra el formulario actual.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Configura la apariencia visual detallada del DataGridView al cargar el formulario.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private async void frmFormaPago_Load(object sender, EventArgs e)
        {
            
            dgvFormasPago.BorderStyle = BorderStyle.None;
            dgvFormasPago.BackgroundColor = Color.White;
            dgvFormasPago.RowHeadersVisible = false;
            dgvFormasPago.EnableHeadersVisualStyles = false;
            dgvFormasPago.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgvFormasPago.ColumnHeadersDefaultCellStyle.BackColor = Color.SkyBlue;
            dgvFormasPago.ColumnHeadersDefaultCellStyle.ForeColor = Color.Navy;
            dgvFormasPago.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvFormasPago.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvFormasPago.ColumnHeadersHeight = 28;

            dgvFormasPago.DefaultCellStyle.BackColor = Color.White;
            dgvFormasPago.DefaultCellStyle.ForeColor = Color.Navy;
            dgvFormasPago.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvFormasPago.DefaultCellStyle.Padding = new Padding(3);
            dgvFormasPago.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 245, 255);
            dgvFormasPago.AlternatingRowsDefaultCellStyle.ForeColor = Color.Navy;

            dgvFormasPago.DefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            dgvFormasPago.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvFormasPago.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvFormasPago.GridColor = Color.LightGray;
            dgvFormasPago.RowTemplate.Height = 32;
            dgvFormasPago.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvFormasPago.ClearSelection();

            await CargarGridFormasPago();
        }
    }
}