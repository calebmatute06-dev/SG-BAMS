using Krypton.Toolkit;
using SG_BAMS.Administracion_de_BAMS.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

            this.Load += async (s, e) =>
            {
                await CargarCombos();
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


        private async void btmModificar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("Por favor complete los campos obligatorios.");
                return;
            }

            if (txtContra.Text.Length > 0 && txtContra.Text.Length < 3 )
            {
                MessageBox.Show("La Contraseña debe tener mas de 3 caracteres.", "Error de Longitud", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtNombre.Text.Contains("  "))
            {
                MessageBox.Show("El nombre no puede contener dos espacios seguidos.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Regex.IsMatch(txtNombre.Text, @"(\w)\1{2,}"))
            {
                MessageBox.Show("No se permite repetir la misma letra más de dos veces seguidas.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtNombre.Text.Length < 3)
            {
                MessageBox.Show("La direccion debe tener mas de 3 caracteres.", "Error de Longitud", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Regex.IsMatch(txtNombre.Text, @"^[a-zA-Z \s & ñ Ñ @,.;:<>]+$"))
            {
                MessageBox.Show("El campos de Nombre solo deben contener caracteres validos.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cmbRol.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un Rol.");
                return;
            }
            try
            {
                clsUsuario objetoUsuario = new clsUsuario();

                int idRol = (int)cmbRol.SelectedValue;
                int idEstado = (int)cmbEstado.SelectedValue;
                byte[] imagenByte = null;

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
                    MessageBox.Show("Usuario actualizado con éxito.", "SG-BAMS");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { this.Cursor = Cursors.Default; }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImagen_Click(object sender, EventArgs e)
        {
            string nombreParaEnviar = txtNombre.Text;

            frmImagenEmpleado agregarImagen = new frmImagenEmpleado(nombreParaEnviar);

            agregarImagen.Show();
        }
    }
}
