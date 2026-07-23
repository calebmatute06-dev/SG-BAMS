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
    /// Formulario para modificar usuarios existentes.
    /// DIP: depende de IUsuarioRepository, no de clsUsuario directamente.
    /// SRP: única responsabilidad — capturar y validar datos para actualizar un usuario.
    /// </summary>
    public partial class frmModificarUsuarios : Form
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly int _idUsuarioSeleccionado;
        private readonly int _rolInicial;
        private readonly int _estadoInicial;
        private readonly string _nombreOriginal;

        private PlaceholderTextBox phNombre;
        private PlaceholderTextBox phCorreo;
        private PlaceholderTextBox phContra;

        /// <summary>
        /// Constructor que recibe el repositorio por inyección de dependencias.
        /// </summary>
        public frmModificarUsuarios(int id, string nombre, int rol, int estado, string correo,
            IUsuarioRepository usuarioRepository)
        {
            InitializeComponent();
            _usuarioRepository = usuarioRepository;
            _idUsuarioSeleccionado = id;
            _rolInicial = rol;
            _estadoInicial = estado;
            _nombreOriginal = nombre;

            this.StartPosition = FormStartPosition.CenterScreen;
            cmbRol.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;

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

        /// <summary>
        /// Constructor sin parámetros de repositorio para compatibilidad con formularios existentes.
        /// </summary>
        public frmModificarUsuarios(int id, string nombre, int rol, int estado, string correo)
            : this(id, nombre, rol, estado, correo, new clsUsuario()) { }

        private async void fmrModificarUsuarios_Load(object sender, EventArgs e)
        {
            await CargarCombos();

            cmbRol.SelectedValue = _rolInicial;
            cmbEstado.SelectedValue = _estadoInicial;

            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            btnImagen.Enabled = (_rolInicial == 1 || _rolInicial == 2);
            btnImagen.Visible = (_rolInicial == 1 || _rolInicial == 2);

            phNombre = new PlaceholderTextBox(txtNombre, "Ingrese el Nombre del usuario");
            phCorreo = new PlaceholderTextBox(txtCorreo, "Ingrese el Correo electrónico");
            phContra = new PlaceholderTextBox(txtContra, "Nueva contraseña (opcional)");
        }

        private async System.Threading.Tasks.Task CargarCombos()
        {
            try
            {
                DataTable dtRoles = await _usuarioRepository.ListarRolesAsync();
                cmbRol.DataSource = dtRoles;
                cmbRol.DisplayMember = "descripcion_rol";
                cmbRol.ValueMember = "id_rol_usuario";

                DataTable dtEstados = await _usuarioRepository.ListarEstadosAsync();
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

        /// <summary>
        /// Renombra los archivos de rostro cuando el nombre del usuario cambia.
        /// SRP: esta responsabilidad pertenece al formulario porque depende del estado de la UI
        /// y del directorio local — no de la base de datos.
        /// </summary>
        private void RenombrarArchivosRostro(string nombreViejo, string nombreNuevo)
        {
            try
            {
                var archivos = Directory.GetFiles(DetectorRostroService.DirectorioRostros, "*.jpg")
                    .Where(f =>
                    {
                        string sinExtension = Path.GetFileNameWithoutExtension(f);
                        return sinExtension == nombreViejo || sinExtension.StartsWith(nombreViejo + "_");
                    })
                    .ToList();

                if (archivos.Count == 0) return;

                foreach (string archivoViejo in archivos)
                {
                    string nombreArchivo = Path.GetFileName(archivoViejo);
                    string nombreArchivoNuevo = nombreNuevo + nombreArchivo.Substring(nombreViejo.Length);
                    string rutaNueva = Path.Combine(DetectorRostroService.DirectorioRostros, nombreArchivoNuevo);
                    File.Move(archivoViejo, rutaNueva);
                }
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

                bool existe = await _usuarioRepository.ExisteUsuarioAsync(nombreReal, _idUsuarioSeleccionado);
                if (existe)
                {
                    MessageBox.Show("El nombre de usuario ya está en uso. Por favor elija otro.",
                        "Usuario duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return;
                }

                bool correoEnUso = _usuarioRepository.CorreoModificar(correoReal, _idUsuarioSeleccionado);
                if (correoEnUso)
                {
                    MessageBox.Show("El correo ya está registrado por otro usuario. Por favor use otro.",
                        "Correo duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCorreo.Focus();
                    return;
                }

                int idRol = (int)cmbRol.SelectedValue;
                int idEstado = (int)cmbEstado.SelectedValue;

                bool exito = await _usuarioRepository.ModificarUsuarioAsync(
                    _idUsuarioSeleccionado,
                    nombreReal,
                    string.IsNullOrWhiteSpace(contraReal) ? null : contraReal,
                    idRol,
                    idEstado,
                    null,
                    correoReal);

                if (exito)
                {
                    bool nombreCambio = !string.Equals(_nombreOriginal, nombreReal, StringComparison.OrdinalIgnoreCase);
                    if (nombreCambio)
                        RenombrarArchivosRostro(_nombreOriginal, nombreReal);

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

        private void btnSalir_Click(object sender, EventArgs e) => this.Close();

        private void btnImagen_Click_1(object sender, EventArgs e)
        {
            frmImagenEmpleado agregarImagen = new frmImagenEmpleado(phNombre.GetRealValue().Trim());
            agregarImagen.ShowDialog();
        }

        private void txtCorreo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !Regex.IsMatch(e.KeyChar.ToString(), @"^[a-zA-Z0-9@._]$"))
                e.Handled = true;
        }

        private void txtCorreo_TextChanged(object sender, EventArgs e) { }
    }
}