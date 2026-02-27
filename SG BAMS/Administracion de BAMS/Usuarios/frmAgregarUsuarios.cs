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
    public partial class frmAgregarUsuarios : Form
    {

        public frmAgregarUsuarios()
        {
            InitializeComponent();
            CargarComboRoles();
        }

        private async void fmrAgregarUsuarios_Load(object sender, EventArgs e)
        {
            await CargarComboRoles();
        }

        private async Task CargarComboRoles()
        {
            try
            {
                clsUsuario objetoUsuario = new clsUsuario();
                DataTable dt = await objetoUsuario.ListarRolesAsync();

                cmbRol.DataSource = dt;

                cmbRol.DisplayMember = "descripcion_rol";

                cmbRol.ValueMember = "id_rol_usuario";

                cmbRol.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private async void btmAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtContra.Text))
            {
                MessageBox.Show("Por favor complete los campos obligatorios.");
                return;
            }

            try
            {
                clsUsuario objetoUsuario = new clsUsuario();

                int idRol = (int)cmbRol.SelectedValue;
                int idEstado = 1;

                byte[] imagenByte = null;


                bool exito = await objetoUsuario.InsertarUsuarioAsync(
                    txtNombre.Text,
                    txtContra.Text,
                    idRol,
                    imagenByte
                );

                if (exito)
                {
                    MessageBox.Show("Usuario guardado exitosamente.");

                    frmUsuarios principal = new frmUsuarios();
                    principal.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message);
            }
        }

        private void btnImagen_Click(object sender, EventArgs e)
        {
            frmImagenEmpleado agregarImagen = new frmImagenEmpleado();
            agregarImagen.Show();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
            frmUsuarios verUsuario = new frmUsuarios();
            verUsuario.Show();
        }
    }
}
