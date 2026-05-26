using SG_BAMS.Administracion_de_BAMS.Usuarios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Clase que gestiona la interfaz de usuario para la modificación de registros de usuarios existentes.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class frmModificarUsuarios : Form
    {
        /// <summary>
        /// El identificador del usuario seleccionado para modificar.
        /// </summary>
        private int idUsuarioSeleccionado;

        /// <summary>
        /// El rol inicial del usuario cargado.
        /// </summary>
        private int rolInicial;

        /// <summary>
        /// El estado inicial del usuario cargado.
        /// </summary>
        private int estadoInicial;

        /// <summary>
        /// El nombre original del usuario antes de cualquier modificación.
        /// Se usa para renombrar los archivos de rostro si el nombre cambia.
        /// </summary>
        private string nombreOriginal;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="frmModificarUsuarios" />.
        /// </summary>
        /// <param name="id">El identificador del usuario.</param>
        /// <param name="nombre">El nombre del usuario.</param>
        /// <param name="rol">El identificador del rol.</param>
        /// <param name="estado">El identificador del estado.</param>
        public frmModificarUsuarios(int id, string nombre, int rol, int estado)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            cmbRol.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            txtNombre.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetrasNumerosSinEspacios(e);

            this.idUsuarioSeleccionado = id;
            this.rolInicial = rol;
            this.estadoInicial = estado;
            this.nombreOriginal = nombre;
            txtNombre.Text = nombre;

            cmbRol.SelectedIndexChanged += (s, e) =>
            {
                if (cmbRol.SelectedValue == null) return;
                if (!int.TryParse(cmbRol.SelectedValue.ToString(), out int rolSeleccionado)) return;

                btnImagen.Enabled = (rolSeleccionado == 1 || rolSeleccionado == 2);
                btnImagen.Visible = (rolSeleccionado == 1 || rolSeleccionado == 2);
            };
        }

        /// <summary>
        /// Maneja el evento Load del control frmModificarUsuarios.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private async void fmrModificarUsuarios_Load(object sender, EventArgs e)
        {
            await CargarCombos();

            cmbRol.SelectedValue = rolInicial;
            cmbEstado.SelectedValue = estadoInicial;

            btnImagen.Enabled = (rolInicial == 1 || rolInicial == 2);
            btnImagen.Visible = (rolInicial == 1 || rolInicial == 2);
        }

        /// <summary>
        /// Carga de forma asíncrona los datos de los ComboBox de roles y estados.
        /// </summary>
        /// <returns>Una tarea que representa la operación de carga.</returns>
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

        /// <summary>
        /// Renombra los archivos de rostro del usuario cuando su nombre de usuario cambia,
        /// para que el reconocimiento facial siga funcionando con el nuevo nombre.
        /// </summary>
        /// <param name="nombreViejo">Nombre original del usuario.</param>
        /// <param name="nombreNuevo">Nuevo nombre del usuario.</param>
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

        /// <summary>
        /// Maneja el evento Click del control btmModificar realizando las validaciones pertinentes.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private async void btmModificar_Click_1(object sender, EventArgs e)
        {
           
            if (!ClsValidaciones.EsNombreUsuarioValido(txtNombre.TextBox, "Nombre de Usuario"))
                return;

            if (!string.IsNullOrWhiteSpace(txtContra.Text))
            {
                if (!ClsValidaciones.EsPasswordValido(txtContra, "Contraseña")) return;
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
                string nombreNuevo = txtNombre.Text.Trim();

                
                bool existe = await objetoUsuario.ExisteUsuarioAsync(nombreNuevo, idUsuarioSeleccionado);
                if (existe)
                {
                    MessageBox.Show("El nombre de usuario ya está en uso. Por favor elija otro.",
                        "Usuario duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNombre.Focus();
                    return;
                }

                int idRol = (int)cmbRol.SelectedValue;
                int idEstado = (int)cmbEstado.SelectedValue;
                byte[] imagenByte = null;

                bool exito = await objetoUsuario.ModificarUsuarioAsync(
                    idUsuarioSeleccionado,
                    nombreNuevo,
                    txtContra.Text,   
                    idRol,
                    idEstado,
                    imagenByte
                );

                if (exito)
                {
                    
                    bool nombreCambio = !string.Equals(
                        nombreOriginal, nombreNuevo,
                        StringComparison.OrdinalIgnoreCase);

                    if (nombreCambio)
                        RenombrarArchivosRostro(nombreOriginal, nombreNuevo);

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

        /// <summary>
        /// Maneja el evento Click del control btnSalir.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Maneja el evento Click del control btnImagen para la gestión de biometría.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void btnImagen_Click_1(object sender, EventArgs e)
        {
            frmImagenEmpleado agregarImagen = new frmImagenEmpleado(txtNombre.Text);
            agregarImagen.ShowDialog();
        }
    }
}