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
            await CargarGridUsuarios();
        }

        private async Task CargarGridUsuarios()
        {
            try
            {
                clsUsuario objetoUsuario = new clsUsuario();

                dgvUsuarios.DataSource = await objetoUsuario.LeerUsuariosAsync();

                // Ocultamos los IDs técnicos
                if (dgvUsuarios.Columns.Contains("id_rol_usuario"))
                    dgvUsuarios.Columns["id_rol_usuario"].Visible = false;

                if (dgvUsuarios.Columns.Contains("id_estado"))
                    dgvUsuarios.Columns["id_estado"].Visible = false;

                if (dgvUsuarios.Columns.Contains("id_usuario"))
                    dgvUsuarios.Columns["id_usuario"].Visible = false;

                ConfigurarGrid(); // Otros ajustes de diseño
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar: " + ex.Message);
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


                int idRol = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["id_rol_usuario"].Value);
                int idEstado = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["id_estado"].Value);

                frmModificarUsuarios frmMod = new frmModificarUsuarios(id, nombre, idRol, idEstado);
                this.Close();

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

        private void btmAgregar_Click(object sender, EventArgs e)
        {
            frmAgregarUsuarios agregarUsuario = new frmAgregarUsuarios();
            agregarUsuario.Show();
            this.Close();
        }

        private void BtmSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
