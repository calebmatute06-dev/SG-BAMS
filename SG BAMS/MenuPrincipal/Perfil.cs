using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using SG_BAMS.Login;
using SG_BAMS.MenuPrincipal;
using SG_BAMS.AccesoDatos;

namespace SG_BAMS
{
    /// <summary>
    /// Formulario para visualizar y modificar el perfil del usuario que ha iniciado sesión.
    /// Permite ver los datos personales y cambiar la foto de perfil.
    /// </summary>
    public partial class Perfil : Form
    {
        private readonly IPerfilService perfilService;
        private readonly ISesionUsuarioService sesionUsuario;

        /// <summary>
        /// Constructor sin parámetros para compatibilidad con código existente.
        /// </summary>
        public Perfil() : this(
            new ClsPerfil(new ClsRepositorioBaseDatos()),
            SesionUsuarioService.Instancia)
        {
        }

        /// <summary>
        /// Constructor principal que recibe los servicios necesarios.
        /// </summary>
        /// <param name="perfilService">Servicio de gestión del perfil de usuario.</param>
        /// <param name="sesionUsuario">Servicio de sesión del usuario actual.</param>
        /// <exception cref="ArgumentNullException">Si algún servicio es nulo.</exception>
        public Perfil(IPerfilService perfilService, ISesionUsuarioService sesionUsuario)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            this.perfilService = perfilService ?? throw new ArgumentNullException(nameof(perfilService));
            this.sesionUsuario = sesionUsuario ?? throw new ArgumentNullException(nameof(sesionUsuario));
        }

        /// <summary>
        /// Propiedad para compatibilidad con código existente.
        /// </summary>
        public string UsuarioActual { get; set; }

        /// <summary>
        /// Evento Load del formulario. Carga los datos del perfil del usuario.
        /// </summary>
        private async void Perfil_Load(object sender, EventArgs e)
        {
            await CargarDatosUsuario();
        }

        /// <summary>
        /// Carga los datos del perfil del usuario desde la base de datos
        /// y los muestra en los controles del formulario.
        /// </summary>
        private async Task CargarDatosUsuario()
        {
            try
            {
                string usuarioLogueado = sesionUsuario.NombreCompleto;

                if (string.IsNullOrEmpty(usuarioLogueado))
                    usuarioLogueado = sesionUsuario.NombreUsuario;

                if (!string.IsNullOrEmpty(usuarioLogueado))
                {
                    DataTable datos = await perfilService.ObtenerPerfilDesdeVista(usuarioLogueado);

                    if (datos.Rows.Count > 0)
                    {
                        DataRow fila = datos.Rows[0];

                        label4.Text = fila["nombre_usuario"].ToString();
                        label5.Text = fila["descripcion_rol"].ToString();

                        if (fila["imagen_usuario"] != DBNull.Value)
                        {
                            byte[] imagenBytes = (byte[])fila["imagen_usuario"];
                            pbFotoPerfil.Image = ConversorImagenService.BytesAImagen(imagenBytes);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar perfil e imagen: " + ex.Message);
            }
        }

        private async void kryptonButton1_Click(object sender, EventArgs e)
        {
        }

        private void Perfil_Shown(object sender, EventArgs e)
        {
            Ayudante_UI.AplicarZoomGlobal(this);
        }

        /// <summary>
        /// Evento Click del botón para cambiar la imagen de perfil.
        /// Abre un cuadro de diálogo para seleccionar una imagen y la guarda en la base de datos.
        /// </summary>
        private async void btnimagen_Click(object sender, EventArgs e)
        {
            OpenFileDialog selectorImagen = new OpenFileDialog();
            selectorImagen.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png";

            if (selectorImagen.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    byte[] imagenBytes = File.ReadAllBytes(selectorImagen.FileName);

                    string usuarioLogueado = sesionUsuario.NombreCompleto;
                    if (string.IsNullOrEmpty(usuarioLogueado))
                        usuarioLogueado = sesionUsuario.NombreUsuario;

                    await perfilService.ActualizarFotoUsuario(usuarioLogueado, imagenBytes);

                    pbFotoPerfil.Image = ConversorImagenService.BytesAImagen(imagenBytes);

                    MessageBox.Show("Imagen de perfil actualizada correctamente.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al procesar la imagen: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Evento Click del botón Salir. Cierra el formulario de perfil.
        /// </summary>
        private void btnsalir1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}