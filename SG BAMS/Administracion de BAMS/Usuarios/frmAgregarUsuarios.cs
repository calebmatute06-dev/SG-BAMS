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
using static Azure.Core.HttpHeader;

namespace SG_BAMS
{
    public partial class frmAgregarUsuarios : Form
    {
        private DataTable dtRoles;
        public frmAgregarUsuarios()
        {
            InitializeComponent();
            CargarComboRoles();
            btnImagen.Enabled = false;
            cmbRol.DropDownStyle = ComboBoxStyle.DropDownList;

            txtNombre.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
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
                cmbRol.SelectedIndexChanged -= cmbRol_SelectedIndexChanged;

                cmbRol.DataSource = dt;
                cmbRol.DisplayMember = "descripcion_rol";
                cmbRol.ValueMember = "id_rol_usuario";
                cmbRol.SelectedIndex = -1;

                cmbRol.SelectedIndexChanged += cmbRol_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }


        private List<string> listaOriginalRoles = new List<string>();



        private void cmbRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRol.SelectedIndex != -1)
            {
                string rolSeleccionado = cmbRol.Text;

                if (rolSeleccionado == "Administrador" || rolSeleccionado == "Empleado")
                {
                    btnImagen.Enabled = true;
                }
                else
                {
                    btnImagen.Enabled = false;
                }
            }
            else
            {
                btnImagen.Enabled = false;
            }
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private async void btmModificar_Click(object sender, EventArgs e)
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
                MessageBox.Show("Debe seleccionar un Rol.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btmModificar.Enabled = false;

                
                clsUsuario objetoUsuario = new clsUsuario();

                int idRol = (int)cmbRol.SelectedValue;
                byte[] imagenByte = null;

                bool exito = await objetoUsuario.InsertarUsuarioAsync(
                    txtNombre.Text.Trim(),
                    txtContra.Text, 
                    idRol,
                    imagenByte
                );

                if (exito)
                {
                    MessageBox.Show("Usuario guardado exitosamente.", "SG-BAMS", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Error de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btmModificar.Enabled = true;
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnImagen_Click(object sender, EventArgs e)
        {
            frmImagenEmpleado agregarImagen = new frmImagenEmpleado(txtNombre.Text);
            agregarImagen.Show();
        }
    }
}