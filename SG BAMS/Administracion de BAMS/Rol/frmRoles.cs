using SG_BAMS.Administracion_de_BAMS.Rol;
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
    public partial class frmRoles : Form
    {
        /// <summary>
        /// The objeto rol
        /// </summary>
        clsRol objetoRol = new clsRol();
        /// <summary>
        /// Initializes a new instance of the <see cref="frmRoles"/> class.
        /// </summary>
        public frmRoles()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Load += new EventHandler(frmRoles_Load);
        }

        /// <summary>
        /// Handles the Load event of the frmRoles control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void frmRoles_Load(object sender, EventArgs e)
        {
            await CargarGridRoles();
        }

        /// <summary>
        /// Cargars the grid roles.
        /// </summary>
        private async Task CargarGridRoles()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                dgvRoles.DataSource = await objetoRol.LeerRolesAsync();

                ConfigurarDisenoGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "SG-BAMS",
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
            if (dgvRoles.Columns.Contains("id_rol_usuario"))
                dgvRoles.Columns["id_rol_usuario"].Visible = false;

            if (dgvRoles.Columns.Contains("descripcion_rol"))
                dgvRoles.Columns["descripcion_rol"].HeaderText = "Nombre del Rol";

            if (dgvRoles.Columns.Contains("total_usuarios_asignados"))
                dgvRoles.Columns["total_usuarios_asignados"].HeaderText = "Usuarios Activos";


            dgvRoles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRoles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRoles.ReadOnly = true;
            dgvRoles.ClearSelection();
        }


        /// <summary>
        /// Handles the Click event of the pictureBox3 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// Handles the CellContentDoubleClick event of the dgvRoles control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dgvRoles_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(dgvRoles.CurrentRow.Cells["id_rol_usuario"].Value);
            string nombre = dgvRoles.CurrentRow.Cells["descripcion_rol"].Value.ToString();

            frmModificarRol frmMod = new frmModificarRol(id, nombre);

            if (frmMod.ShowDialog() == DialogResult.OK)
            {
                _ = CargarGridRoles();
            }
        }

        /// <summary>
        /// Handles the Click event of the btmAgregar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btmAgregar_Click(object sender, EventArgs e)
        {
            frmAgregarRol agregarRol = new frmAgregarRol();
            agregarRol.Show();
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the btmModificar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btmModificar_Click(object sender, EventArgs e)
        {
            if (dgvRoles.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvRoles.CurrentRow.Cells["id_rol_usuario"].Value);
                string nombre = dgvRoles.CurrentRow.Cells["descripcion_rol"].Value.ToString();



                frmModificarRol frmMod = new frmModificarRol(id, nombre);


                if (frmMod.ShowDialog() == DialogResult.OK)
                {
                    _ = CargarGridRoles();

                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un usuario de la lista.");
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
        /// Handles the 1 event of the frmRoles_Load control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void frmRoles_Load_1(object sender, EventArgs e)
        {
            dgvRoles.BorderStyle = BorderStyle.None;
            dgvRoles.BackgroundColor = Color.White;
            dgvRoles.RowHeadersVisible = false;
            dgvRoles.EnableHeadersVisualStyles = false;
            dgvRoles.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgvRoles.ColumnHeadersDefaultCellStyle.BackColor = Color.SkyBlue;
            dgvRoles.ColumnHeadersDefaultCellStyle.ForeColor = Color.Navy;
            dgvRoles.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvRoles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvRoles.ColumnHeadersHeight = 28;

            dgvRoles.DefaultCellStyle.BackColor = Color.White;
            dgvRoles.DefaultCellStyle.ForeColor = Color.Navy;
            dgvRoles.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvRoles.DefaultCellStyle.Padding = new Padding(3);
            dgvRoles.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 245, 255);
            dgvRoles.AlternatingRowsDefaultCellStyle.ForeColor = Color.Navy;

            dgvRoles.DefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            dgvRoles.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvRoles.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvRoles.GridColor = Color.LightGray;
            dgvRoles.RowTemplate.Height = 32;
            dgvRoles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRoles.ClearSelection();
        }
    }
}

