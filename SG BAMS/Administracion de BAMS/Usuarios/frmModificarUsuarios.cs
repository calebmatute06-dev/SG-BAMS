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
    /// Clase que gestiona la interfaz de usuario para la modificación de registros de usuarios existentes.
    /// </summary>
    public partial class frmModificarUsuarios : Form
    {
        private int idUsuarioSeleccionado;
        private int rolInicial;
        private int estadoInicial;
        private string nombreOriginal;


        private PlaceholderTextBox phNombre;
        private PlaceholderTextBox phCorreo;
        private PlaceholderTextBox phContra;

        public frmModificarUsuarios(int id, string nombre, int rol, int estado, string correo)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            cmbRol.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;


            this.idUsuarioSeleccionado = id;
            this.rolInicial = rol;
            this.estadoInicial = estado;
            this.nombreOriginal = nombre;
            txtNombre.Text = nombre;
            txtCorreo.Text = correo;

            cmbRol.SelectedIndexChanged += (s, e) =>
            {
                if (cmbRol.SelectedValue == null) return;
                if (!int.TryParse(cmbRol.SelectedValue.ToString(), out int rolSeleccionado)) return;

                btnImagen.Enabled = (rolSeleccionado == 1 || rolSeleccionado == 2);
                btnImagen.Visible = (rolSeleccionado == 1 || rolSeleccionado == 2);
            };
        }

        private async void fmrModificarUsuarios_Load(object sender, EventArgs e)
        {
            await CargarCombos();

            cmbRol.SelectedValue = rolInicial;
            cmbEstado.SelectedValue = estadoInicial;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            btnImagen.Enabled = (rolInicial == 1 || rolInicial == 2);
            btnImagen.Visible = (rolInicial == 1 || rolInicial == 2);


            phNombre = new PlaceholderTextBox(txtNombre, "Ingrese el Nombre del usuario");
            phCorreo = new PlaceholderTextBox(txtCorreo, "Ingrese el Correo electrónico");
            phContra = new PlaceholderTextBox(txtContra, "Nueva contraseña (opcional)");
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
                MessageBox.Show("Error al cargar listas: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RenombrarArchivosRostro(string nombreViejo, string nombreNuevo)
        {
            try
            {
                var archivos = Directory.GetFiles(clsSoporte.DirectorioRostros, "*.jpg")
                    .Where(f =>
                    {
                        string sinExtension = Path.GetFileNameWithoutExtension(f);
                        return sinExtension == nombreViejo ||
                               sinExtension.StartsWith(nombreViejo + "_");
                    })
                    .ToList();

                if (archivos.Count == 0) return;

                int renombrados = 0;
                foreach (string archivoViejo in archivos)
                {
                    string nombreArchivo = Path.GetFileName(archivoViejo);
                    string nombreArchivoNuevo = nombreNuevo +
                        nombreArchivo.Substring(nombreViejo.Length);
                    string rutaNueva = Path.Combine(clsSoporte.DirectorioRostros, nombreArchivoNuevo);

                    File.Move(archivoViejo, rutaNueva);
                    renombrados++;
                }

                System.Diagnostics.Debug.WriteLine(
                    $"[INFO] Rostros renombrados: {renombrados} archivos " +
                    $"de '{nombreViejo}' a '{nombreNuevo}'");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"El usuario se actualizó correctamente, pero ocurrió un error " +
                    $"al renombrar los archivos de reconocimiento facial:\n\n{ex.Message}\n\n" +
                    "El usuario deberá volver a registrar su rostro para poder iniciar sesión.",
                    "Advertencia - Archivos de rostro",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private async void btmModificar_Click_1(object sender, EventArgs e)
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

            if (!string.IsNullOrWhiteSpace(contraReal))
            {
                using (var tempContra = new TextBox { Text = contraReal })
                {
                    if (!ClsValidaciones.EsPasswordValido(tempContra, "Contraseña"))
                        return;
                }
            }

            if (cmbRol.SelectedIndex == -1 || cmbEstado.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un Rol y un Estado.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btmModificar.Enabled = false;

                clsUsuario objetoUsuario = new clsUsuario();
                bool existe = await objetoUsuario.ExisteUsuarioAsync(nombreReal, idUsuarioSeleccionado);
                if (existe)
                {
                    MessageBox.Show("El nombre de usuario ya está en uso. Por favor elija otro.",
                        "Usuario duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return;
                }

                if (objetoUsuario.CorreoModificar(correoReal, idUsuarioSeleccionado))
                {
                    MessageBox.Show("El correo ya está registrado por otro usuario. Por favor use otro.",
                        "Correo duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCorreo.Focus();
                    return;
                }



                int idRol = (int)cmbRol.SelectedValue;
                int idEstado = (int)cmbEstado.SelectedValue;
                byte[] imagenByte = null;

                bool exito = await objetoUsuario.ModificarUsuarioAsync(
                    idUsuarioSeleccionado,
                    nombreReal,
                    string.IsNullOrWhiteSpace(contraReal) ? null : contraReal,
                    idRol,
                    idEstado,
                    imagenByte,
                    correoReal
                );

                if (exito)
                {
                    bool nombreCambio = !string.Equals(
                        nombreOriginal, nombreReal,
                        StringComparison.OrdinalIgnoreCase);

                    if (nombreCambio)
                        RenombrarArchivosRostro(nombreOriginal, nombreReal);

                    MessageBox.Show("Usuario actualizado con éxito.", "SG-BAMS",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message,
                    "Error de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            frmImagenEmpleado agregarImagen = new frmImagenEmpleado(phNombre.GetRealValue().Trim());
            agregarImagen.ShowDialog();
        }

        private void txtCorreo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !Regex.IsMatch(e.KeyChar.ToString(), @"^[a-zA-Z0-9@._]$"))
            {
                e.Handled = true;
            }
        }

        private void txtCorreo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}