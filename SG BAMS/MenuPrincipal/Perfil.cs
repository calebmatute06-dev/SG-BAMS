using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using SG_BAMS.Login;
using SG_BAMS.MenuPrincipal;

namespace SG_BAMS
{
    /// <summary>
    /// Formulario para visualizar y modificar el perfil del usuario logueado.
    /// </summary>
    public partial class Perfil : Form
    {
        private readonly IPerfilService _perfilService;
        private readonly ISesionUsuarioService _sesionUsuario;

        /// <summary>
        /// Constructor sin parámetros para compatibilidad.
        /// </summary>
        public Perfil() : this(
            new ClsPerfil(new ClsRepositorioBaseDatos()),
            SesionUsuarioService.Instancia)
        {
        }

        /// <summary>
        /// Constructor principal con inyección de dependencias.
        /// 
        /// DIP: Recibe IPerfilService e ISesionUsuarioService en lugar de
        /// instanciar ClsPerfil o usar Login.UsuarioLogueado estático.
        /// </summary>
        /// <param name="perfilService">Servicio de perfil de usuario.</param>
        /// <param name="sesionUsuario">Servicio de sesión del usuario.</param>
        /// <exception cref="ArgumentNullException">Si algún servicio es nulo.</exception>
        public Perfil(IPerfilService perfilService, ISesionUsuarioService sesionUsuario)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            _perfilService = perfilService ?? throw new ArgumentNullException(nameof(perfilService));
            _sesionUsuario = sesionUsuario ?? throw new ArgumentNullException(nameof(sesionUsuario));
        }

        /// <summary>
        /// Propiedad para compatibilidad con código existente.
        /// </summary>
        public string UsuarioActual { get; set; }

        private async void Perfil_Load(object sender, EventArgs e)
        {
            await CargarDatosUsuario();
        }

        /// <summary>
        /// Carga los datos del perfil desde el servicio inyectado.
        /// 
        /// DIP: Usa ISesionUsuarioService en lugar de SesionUsuarioService.Instancia directo.
        /// DRY: Usa ConversorImagenService para convertir bytes a imagen.
        /// </summary>
        private async Task CargarDatosUsuario()
        {
            try
            {

                string usuarioLogueado = _sesionUsuario.NombreCompleto;

                if (string.IsNullOrEmpty(usuarioLogueado))
                    usuarioLogueado = _sesionUsuario.NombreUsuario;

                if (!string.IsNullOrEmpty(usuarioLogueado))
                {
                    DataTable datos = await _perfilService.ObtenerPerfilDesdeVista(usuarioLogueado);

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
        /// 
        /// DIP: Usa IPerfilService e ISesionUsuarioService inyectados.
        /// DRY: Usa ConversorImagenService para convertir bytes a imagen.
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


                    string usuarioLogueado = _sesionUsuario.NombreCompleto;
                    if (string.IsNullOrEmpty(usuarioLogueado))
                        usuarioLogueado = _sesionUsuario.NombreUsuario;

                    await _perfilService.ActualizarFotoUsuario(usuarioLogueado, imagenBytes);

                    pbFotoPerfil.Image = ConversorImagenService.BytesAImagen(imagenBytes);

                    MessageBox.Show("Imagen de perfil actualizada correctamente.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al procesar la imagen: " + ex.Message);
                }
            }
        }

        private void btnsalir1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}