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
    public partial class frmFormaPago : Form
    {
        clsFormaPago objetoFP = new clsFormaPago();
        public frmFormaPago()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmFormasPago_Load);
        }
        private async void frmFormasPago_Load(object sender, EventArgs e)
        {
            await CargarGridFormasPago();
        }

        private async Task CargarGridFormasPago()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                // Llamada asíncrona a la base de datos
                DataTable dt = await objetoFP.LeerFormasPagoAsync();

                // Asignación al DataGridView (asegúrate que el nombre coincida)
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

        private void ConfigurarDisenoGrid()
        {
            // Ocultamos el ID si no quieres que el usuario lo vea
            if (dgvFormasPago.Columns.Contains("id_tipo_forma_pago"))
                dgvFormasPago.Columns["id_tipo_forma_pago"].Visible = false;

            // Ajustamos los títulos de las columnas
            if (dgvFormasPago.Columns.Contains("descripcion_forma_pago"))
                dgvFormasPago.Columns["descripcion_forma_pago"].HeaderText = "Método de Pago";

            if (dgvFormasPago.Columns.Contains("total_uso_facturas"))
                dgvFormasPago.Columns["total_uso_facturas"].HeaderText = "Uso en Facturas";

            dgvFormasPago.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvFormasPago.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvFormasPago.AllowUserToAddRows = false;
        }

        private void btmAgregar2_Click(object sender, EventArgs e)
        {
            frmAgregarFormaPago AgregarFpago = new frmAgregarFormaPago();
            AgregarFpago.Show();
            this.Close();
        }

        private void btmSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAgregarFormaPago agregarFpago = new frmAgregarFormaPago();
            agregarFpago.Show();
            this.Close();
        }

        private void btmModificar_Click(object sender, EventArgs e)
        {
            if (dgvFormasPago.SelectedRows.Count > 0)
            {
                // 1. Capturamos los datos de la fila actual del DataGridView
                // Asegúrate de que los nombres de las celdas coincidan con tu SELECT de la Vista
                int id = Convert.ToInt32(dgvFormasPago.CurrentRow.Cells["id_tipo_forma_pago"].Value);
                string descripcion = dgvFormasPago.CurrentRow.Cells["descripcion_forma_pago"].Value.ToString();

                // 2. Instanciamos el formulario enviándole los datos por el constructor
                using (frmModificarFormaPago frmModificar = new frmModificarFormaPago(id, descripcion))
                {
                    // 3. Abrimos como cuadro de diálogo
                    if (frmModificar.ShowDialog() == DialogResult.OK)
                    {
                        // 4. Si se guardó con éxito (DialogResult.OK), refrescamos la tabla
                        _ = CargarGridFormasPago();
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila para editar.");
            }
        }
    }
}
