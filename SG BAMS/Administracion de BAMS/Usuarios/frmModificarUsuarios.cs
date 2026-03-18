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
            cmbRol.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;

            txtNombre.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);

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



        private async void btmModificar_Click_1(object sender, EventArgs e)
        {
            
            if (!ClsValidaciones.EsNombrePersonalValido(txtNombre.TextBox, "Nombre de Usuario"))
            {
                return;
            }

            
            if (!ClsValidaciones.EsPasswordValido(txtContra.TextBox, "Contraseña"))
            {
                return;
            }

            
            if (cmbRol.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un Rol.");
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btmModificar.Enabled = false;

                clsUsuario objetoUsuario = new clsUsuario();

                int idRol = (int)cmbRol.SelectedValue;
                int idEstado = (int)cmbEstado.SelectedValue;
                byte[] imagenByte = null;

                bool exito = await objetoUsuario.ModificarUsuarioAsync(
                    idUsuarioSeleccionado,
                    txtNombre.Text.Trim(),
                    txtContra.Text,
                    idRol,
                    idEstado,
                    imagenByte
                );

                if (exito)
                {
                    MessageBox.Show("Usuario actualizado con éxito.", "SG-BAMS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btmModificar.Enabled = true;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImagen_Click_1(object sender, EventArgs e)
        {
            string nombreParaEnviar = txtNombre.Text;

            frmImagenEmpleado agregarImagen = new frmImagenEmpleado(nombreParaEnviar);

            agregarImagen.Show();
        }
    }
}
