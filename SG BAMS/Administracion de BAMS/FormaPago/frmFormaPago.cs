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
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class frmFormaPago : Form
    {
        /// <summary>
        /// The objeto fp
        /// </summary>
        clsFormaPago objetoFP = new clsFormaPago();
        /// <summary>
        /// Initializes a new instance of the <see cref="frmFormaPago"/> class.
        /// </summary>
        public frmFormaPago()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmFormasPago_Load);
        }
        /// <summary>
        /// Handles the Load event of the frmFormasPago control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void frmFormasPago_Load(object sender, EventArgs e)
        {
            await CargarGridFormasPago();
            this.Load += async (s, e) => await CargarGridFormasPago();
        }

        /// <summary>
        /// Cargars the grid formas pago.
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
        /// Configurars the diseno grid.
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

        /// <summary>
        /// Handles the Click event of the btmAgregar2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btmAgregar2_Click(object sender, EventArgs e)
        {
            frmAgregarFormaPago AgregarFpago = new frmAgregarFormaPago();
            AgregarFpago.Show();
            this.Close();
        }

        /// <summary>
        /// Handles the CellContentDoubleClick event of the dgvFormasPago control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Handles the Click event of the btnAgregar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAgregarFormaPago agregarFpago = new frmAgregarFormaPago();
            agregarFpago.Show();
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the btnModificar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Handles the Click event of the btnSalir control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Load event of the frmFormaPago control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void frmFormaPago_Load(object sender, EventArgs e)
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
        }
    }
}
