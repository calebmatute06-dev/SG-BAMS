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

        private void btmAgregar2_Click(object sender, EventArgs e)
        {
            frmAgregarFormaPago AgregarFpago = new frmAgregarFormaPago();
            AgregarFpago.Show();
            this.Close();
        }

        private void dgvFormasPago_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvFormasPago.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvFormasPago.CurrentRow.Cells["id_tipo_forma_pago"].Value);
                string descripcion = dgvFormasPago.CurrentRow.Cells["descripcion_forma_pago"].Value.ToString();

                using (frmModificarFormaPago frmModificar = new frmModificarFormaPago(id, descripcion))
                {
                    if (frmModificar.ShowDialog() == DialogResult.OK)
                    {
                        _ = CargarGridFormasPago();
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila para editar.");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAgregarFormaPago agregarFpago = new frmAgregarFormaPago();
            agregarFpago.Show();
            this.Close();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvFormasPago.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvFormasPago.CurrentRow.Cells["id_tipo_forma_pago"].Value);
                string descripcion = dgvFormasPago.CurrentRow.Cells["descripcion_forma_pago"].Value.ToString();

                using (frmModificarFormaPago frmModificar = new frmModificarFormaPago(id, descripcion))
                {
                    if (frmModificar.ShowDialog() == DialogResult.OK)
                    {
                        _ = CargarGridFormasPago();
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila para editar.");
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
