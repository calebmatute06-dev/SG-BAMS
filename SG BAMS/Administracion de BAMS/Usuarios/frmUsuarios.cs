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
            this.Load += new EventHandler(FormUsuarios_Load);
        }

        private async void FormUsuarios_Load(object sender, EventArgs e)
        {
            await CargarDatosUsuarios();
        }

        private async Task CargarDatosUsuarios()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                clsUsuario objetoUsuario = new clsUsuario();

                DataTable dt = await objetoUsuario.LeerUsuariosAsync();

                dgvUsuarios.DataSource = dt;

                ConfigurarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuarios: {ex.Message}", "Error de Sistema",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void ConfigurarGrid()
        {
            if (dgvUsuarios.Columns.Contains("imagen_usuario"))
            {
                dgvUsuarios.Columns["imagen_usuario"].Visible = false;
            }

            dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.AllowUserToAddRows = false; // Evita la fila vacía al final
        }


        private void btmModificar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["id_usuario"].Value);
                string nombre = dgvUsuarios.CurrentRow.Cells["nombre_usuario"].Value.ToString();

       
                int idRol = 1; // Valor temporal o dgvUsuarios.CurrentRow.Cells["id_rol"].Value
                int idEstado = 1;

                frmModificarUsuarios frmMod = new frmModificarUsuarios(id, nombre, idRol, idEstado);
                this.Close();

                if (frmMod.ShowDialog() == DialogResult.OK)
                {
                    _ = CargarDatosUsuarios();
                    
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un usuario de la lista.");
            }
            
        }

        private void btmAgregar_Click(object sender, EventArgs e)
        {
            frmAgregarUsuarios agregarUsuario = new frmAgregarUsuarios();
            agregarUsuario.Show();
            this.Hide();
        }

        private void BtmSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
