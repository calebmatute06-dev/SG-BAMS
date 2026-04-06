using SG_BAMS.Administracion_de_BAMS.Usuarios;
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
    public partial class frmUsuarios : Form
    {

        clsUsuario objetoUsuario = new clsUsuario();
        public frmUsuarios()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmUsuarios_Load);

        }

       

        private async Task CargarGridUsuarios()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                dgvUsuarios.DataSource = await objetoUsuario.LeerUsuariosAsync();
                ConfigurarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar: " + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void ConfigurarGrid()
        {
            if (dgvUsuarios.Columns.Contains("imagen_usuario"))
                dgvUsuarios.Columns["imagen_usuario"].Visible = false;

            if (dgvUsuarios.Columns.Contains("id_rol_usuario"))
                dgvUsuarios.Columns["id_rol_usuario"].Visible = false;

            if (dgvUsuarios.Columns.Contains("id_estado"))
                dgvUsuarios.Columns["id_estado"].Visible = false;

            if (dgvUsuarios.Columns.Contains("id_usuario"))
                dgvUsuarios.Columns["id_usuario"].Visible = false;

            if (dgvUsuarios.Columns.Contains("nombre_usuario"))
                dgvUsuarios.Columns["nombre_usuario"].HeaderText = "Nombres de Usuarios";

            dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.AllowUserToAddRows = false;
            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.ClearSelection();
        }

        private void dgvUsuarios_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["id_usuario"].Value);
                string nombre = dgvUsuarios.CurrentRow.Cells["nombre_usuario"].Value.ToString();


                int idRol = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["id_rol_usuario"].Value);
                int idEstado = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["id_estado"].Value);

                frmModificarUsuarios frmMod = new frmModificarUsuarios(id, nombre, idRol, idEstado);

                if (frmMod.ShowDialog() == DialogResult.OK)
                {
                    _ = CargarGridUsuarios();

                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un usuario de la lista.");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAgregarUsuarios agregarUsuario = new frmAgregarUsuarios();
            agregarUsuario.Show();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["id_usuario"].Value);
                string nombre = dgvUsuarios.CurrentRow.Cells["nombre_usuario"].Value.ToString();

                int idRol = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["id_rol_usuario"].Value);
                int idEstado = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["id_estado"].Value);

                frmModificarUsuarios frmMod = new frmModificarUsuarios(id, nombre, idRol, idEstado);


                if (frmMod.ShowDialog() == DialogResult.OK)
                {
                    _ = CargarGridUsuarios();

                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un usuario de la lista.");
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void frmUsuarios_Load(object sender, EventArgs e)
        {
            await CargarGridUsuarios();
            dgvUsuarios.BorderStyle = BorderStyle.None;
            dgvUsuarios.BackgroundColor = Color.White;
            dgvUsuarios.RowHeadersVisible = false;
            dgvUsuarios.EnableHeadersVisualStyles = false;
            dgvUsuarios.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgvUsuarios.ColumnHeadersDefaultCellStyle.BackColor = Color.SkyBlue;
            dgvUsuarios.ColumnHeadersDefaultCellStyle.ForeColor = Color.Navy;
            dgvUsuarios.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvUsuarios.ColumnHeadersHeight = 28;

            dgvUsuarios.DefaultCellStyle.BackColor = Color.White;
            dgvUsuarios.DefaultCellStyle.ForeColor = Color.Navy;
            dgvUsuarios.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvUsuarios.DefaultCellStyle.Padding = new Padding(3);
            dgvUsuarios.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 245, 255);
            dgvUsuarios.AlternatingRowsDefaultCellStyle.ForeColor = Color.Navy;

            dgvUsuarios.DefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            dgvUsuarios.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvUsuarios.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvUsuarios.GridColor = Color.LightGray;
            dgvUsuarios.RowTemplate.Height = 32;
            dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsuarios.ClearSelection();
        }
    }
}
