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
    public partial class frmRoles : Form
    {
        clsRol objetoRol = new clsRol();
        public frmRoles()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmRoles_Load);
        }

        private async void frmRoles_Load(object sender, EventArgs e)
        {
            await CargarGridRoles();
        }

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
            dgvRoles.AllowUserToAddRows = false;
            dgvRoles.ReadOnly = true;
        }


        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }


        private void btmAgregar_Click(object sender, EventArgs e)
        {
            frmAgregarRol agregarRol = new frmAgregarRol();
            agregarRol.Show();
            this.Close();
        }

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
                MessageBox.Show("Por favor, seleccione un rol de la lista para modificar.");
            }
        }

        private void btmSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
