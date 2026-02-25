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
    }
}
