using SG_BAMS.Administracion_de_BAMS.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Interfaz de usuario para la creación de nuevos usuarios, incluyendo la asignación de roles y el registro facial obligatorio para roles administrativos.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class frmAgregarUsuarios : Form
    {
        private DataTable dtRoles;
        private List<string> listaOriginalRoles = new List<string>();

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="frmAgregarUsuarios"/>.
        /// </summary>
        public frmAgregarUsuarios()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            _ = CargarComboRoles();
            cmbRol.DropDownStyle = ComboBoxStyle.DropDownList;

            txtNombre.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetrasNumerosSinEspacios(e);
        }

        /// <summary>
        /// Carga de forma asíncrona el catálogo de roles en el control ComboBox.
        /// </summary>
        /// <returns>Una tarea que representa la operación asíncrona.</returns>
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

        /// <summary>
        /// Maneja el cambio de selección en el combo de roles.
        /// </summary>
        private void cmbRol_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Procesa el registro del nuevo usuario y gestiona el flujo de captura facial si el rol lo requiere.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private async void btmModificar_Click(object sender, EventArgs e)
        {
            if (!ClsValidaciones.EsNombreUsuarioValido(txtNombre.TextBox, "Nombre de Usuario"))
                return;

            if (!ClsValidaciones.EsPasswordValido(txtContra.TextBox, "Contraseña"))
                return;

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
                string nombreUsuario = txtNombre.Text.Trim();

                bool existe = await objetoUsuario.ExisteUsuarioAsync(nombreUsuario);
                if (existe)
                {
                    MessageBox.Show("El nombre de usuario ya está en uso. Por favor elija otro.",
                        "Usuario duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return;
                }

                int idRol = (int)cmbRol.SelectedValue;
                byte[] imagenByte = null;

                bool exito = await objetoUsuario.InsertarUsuarioAsync(
                    txtNombre.Text.Trim(),
                    txtContra.Text,
                    idRol,
                    imagenByte,
                    txtCorreo.Text
                );

                if (exito)
                {
                    MessageBox.Show("Usuario guardado exitosamente.", "SG-BAMS", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (idRol == 1 || idRol == 2)
                    {
                        bool imagenRegistrada = false;

                        while (!imagenRegistrada)
                        {
                            frmImagenEmpleado agregarImagen = new frmImagenEmpleado(txtNombre.Text.Trim());
                            agregarImagen.ShowDialog();

                            var archivos = Directory.GetFiles(clsSoporte.DirectorioRostros, "*.jpg")
                                .Where(f => Path.GetFileNameWithoutExtension(f) == txtNombre.Text.Trim() ||
                                            Path.GetFileNameWithoutExtension(f).StartsWith(txtNombre.Text.Trim() + "_"))
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

        /// <summary>
        /// Cierra el formulario actual.
        /// </summary>
        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAgregarUsuarios_Load(object sender, EventArgs e)
        {
            ClsMensajeGuia.ActivarK(txtNombre);
            ClsMensajeGuia.ActivarK(txtCorreo);
        }

        private void txtContra_TextChanged(object sender, EventArgs e)
        {

        }
    }
}