using Krypton.Toolkit;
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
    public partial class frmModificarUsuarios : Form
    {

        private int idUsuarioSeleccionado;

        public frmModificarUsuarios(int id, string nombre, int rol, int estado)
        {
            InitializeComponent();

            this.idUsuarioSeleccionado = id;

            txtNombre.Text = nombre;

            // IMPORTANTE: Para que SelectedValue funcione, los combos ya deben tener datos
            // Es recomendable llamar a los métodos de cargar combos en el evento Load
            this.Load += async (s, e) => {
                await CargarCombos(); // Método que rellena cmbRol y cmbEstado
                cmbRol.SelectedValue = rol;
                cmbEstado.SelectedValue = estado;
            };
        }

        private async Task CargarCombos()
        {
            try
            {
                clsUsuario objetoUsuario = new clsUsuario();

                // Cargar Roles
                DataTable dtRoles = await objetoUsuario.ListarRolesAsync();
                cmbRol.DataSource = dtRoles;
                cmbRol.DisplayMember = "descripcion_rol";
                cmbRol.ValueMember = "id_rol_usuario";

                DataTable dtEstados = await objetoUsuario.ListarEstadosAsync();
                cmbEstado.DataSource = dtEstados;
                cmbEstado.DisplayMember = "descripcion_estado";
                cmbEstado.ValueMember = "id_estado";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar listas: " + ex.Message);
            }
        }
        private void fmrModificarUsuarios_Load(object sender, EventArgs e)
        {

        }


        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            this.Close();
            frmUsuarios verUsuario = new frmUsuarios();
            verUsuario.Show();

        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            frmImagenEmpleado agregarImagen = new frmImagenEmpleado();
            agregarImagen.Show();
        }

        private async void btmModificar_Click(object sender, EventArgs e)
        {
            try
            {
                clsUsuario objetoUsuario = new clsUsuario();

                // Obtenemos los valores de los controles
                int idRol = (int)cmbRol.SelectedValue;
                int idEstado = (int)cmbEstado.SelectedValue;
                byte[] imagenByte = null; // Lógica de imagen similar a la de agregar

                bool exito = await objetoUsuario.ModificarUsuarioAsync(
                    idUsuarioSeleccionado,
                    txtNombre.Text,
                    txtContra.Text,
                    idRol,
                    idEstado,
                    imagenByte
                );

                if (exito)
                {
                    MessageBox.Show("Usuario actualizado correctamente.");
                    frmUsuarios verUsuarios = new frmUsuarios();
                    verUsuarios.Show();
                    this.Close(); // Regresa al listado
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
