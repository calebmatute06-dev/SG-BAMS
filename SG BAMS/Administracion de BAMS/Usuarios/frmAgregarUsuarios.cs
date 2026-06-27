using SG_BAMS.Administracion_de_BAMS.Usuarios;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Interfaz de usuario para la creación de nuevos usuarios, incluyendo la asignación de roles y el registro facial obligatorio para roles administrativos.
    /// </summary>
    public partial class frmAgregarUsuarios : Form
    {
        private DataTable dtRoles;
        private List<string> listaOriginalRoles = new List<string>();

        private PlaceholderTextBox phNombre;
        private PlaceholderTextBox phCorreo;
        private PlaceholderTextBox phContra;
        private PlaceholderComboBox phRol;

        public frmAgregarUsuarios()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

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
                cmbRol.SelectedIndexChanged += cmbRol_SelectedIndexChanged;


                phRol.Activar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbRol_SelectedIndexChanged(object sender, EventArgs e) { }

        private async void frmAgregarUsuarios_Load(object sender, EventArgs e)
        {

            phNombre = new PlaceholderTextBox(txtNombre, "Ingrese el Nombre del usuario");
            phCorreo = new PlaceholderTextBox(txtCorreo, "Ingrese el Correo electrónico");
            phContra = new PlaceholderTextBox(txtContra, "Ingrese la Contraseña");
            phRol = new PlaceholderComboBox(cmbRol, "Seleccione un rol");


            cmbRol.DropDownStyle = ComboBoxStyle.DropDown;


            await CargarComboRoles();
        }

        private async void btmModificar_Click(object sender, EventArgs e)
        {
            string nombreReal = phNombre.GetRealValue().Trim();
            string correoReal = phCorreo.GetRealValue().Trim();
            string contraReal = phContra.GetRealValue().Trim();

            using (var tempNombre = new TextBox { Text = nombreReal })
            {
                if (!ClsValidaciones.EsNombreUsuarioValido(tempNombre, "Nombre de Usuario"))
                    return;
            }


            using (var tempCorreo = new TextBox { Text = correoReal })
            {
                if (!ClsValidaciones.ValidacionCorreo(tempCorreo))
                    return;
            }



            using (var tempContra = new TextBox { Text = contraReal })
            {
                if (!ClsValidaciones.EsPasswordValido(tempContra, "Contraseña"))
                    return;
            }

            if (phRol.IsPlaceholderActive || cmbRol.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un Rol.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btmModificar.Enabled = false;

                clsUsuario objetoUsuario = new clsUsuario();

                bool existe = await objetoUsuario.ExisteUsuarioAsync(nombreReal);
                if (existe)
                {
                    MessageBox.Show("El nombre de usuario ya está en uso. Por favor elija otro.",
                        "Usuario duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return;
                }

                bool correoExiste = await objetoUsuario.ExisteCorreo(correoReal);
                if (correoExiste)
                {
                    MessageBox.Show("El correo ya está registrado. Por favor use otro.",
                        "Correo duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCorreo.Focus();
                    return;
                }

                int idRol = (int)cmbRol.SelectedValue;
                byte[] imagenByte = null;

                bool exito = await objetoUsuario.InsertarUsuarioAsync(
                    nombreReal,
                    contraReal,
                    idRol,
                    imagenByte,
                    correoReal
                );

                if (exito)
                {
                    MessageBox.Show("Usuario guardado exitosamente.", "SG-BAMS", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (idRol == 1 || idRol == 2)
                    {
                        bool imagenRegistrada = false;

                        while (!imagenRegistrada)
                        {
                            frmImagenEmpleado agregarImagen = new frmImagenEmpleado(nombreReal);
                            agregarImagen.ShowDialog();

                            var archivos = Directory.GetFiles(clsSoporte.DirectorioRostros, "*.jpg")
                                .Where(f => Path.GetFileNameWithoutExtension(f) == nombreReal ||
                                            Path.GetFileNameWithoutExtension(f).StartsWith(nombreReal + "_"))
                                .ToList();

                            if (archivos.Count > 0)
                            {
                                imagenRegistrada = true;
                            }
                            else
                            {
                                DialogResult respuesta = MessageBox.Show(
                                    "Este usuario requiere registro facial para poder iniciar sesión.\n\n" +
                                    "¿Deseas cancelar el registro facial?\n\n" +
                                    "Si cancelas, el usuario quedará guardado pero NO podrá iniciar sesión hasta que registre su rostro.",
                                    "Registro facial requerido",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Warning
                                );

                                if (respuesta == DialogResult.Yes)
                                {
                                    break;
                                }
                            }
                        }
                    }

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

        private void kryptonButton1_Click(object sender, EventArgs e) => this.Close();

        private void txtContra_TextChanged(object sender, EventArgs e) { }

        private void txtCorreo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !Regex.IsMatch(e.KeyChar.ToString(), @"^[a-zA-Z0-9@._]$"))
            {
                e.Handled = true;
            }
        }

       
    }
}