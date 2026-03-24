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
                MessageBox.Show($"Error al cargar roles: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRol.SelectedIndex != -1)
            {
                string rolSeleccionado = cmbRol.Text;
                
                btnImagen.Enabled = (rolSeleccionado == "Administrador" || rolSeleccionado == "Empleado");
            }
            else
            {
                btnImagen.Enabled = false;
            }
        }

        private async void btmModificar_Click(object sender, EventArgs e)
        {
           
            if (!ClsValidaciones.EsNombrePersonalValido(txtNombre, "Nombre de Usuario")) return;

           
            if (!ClsValidaciones.EsPasswordValido(txtContra, "Contraseña")) return;

            if (cmbRol.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, seleccione un Rol para el usuario.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    MessageBox.Show("Usuario guardado exitosamente.", "SG-BAMS",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al guardar: " + ex.Message, "Error de Sistema",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Escriba el nombre del usuario antes de asignar una imagen.", "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            frmImagenEmpleado agregarImagen = new frmImagenEmpleado(txtNombre.Text);
            agregarImagen.ShowDialog(); 
        }
    }
}