using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SG_BAMS
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class Perfil : Form
    {
        /// <summary>
        /// Obtiene o establece el usuario actual.
        /// </summary>
        /// <value>
        /// El usuario actual.
        /// </value>
        public string UsuarioActual { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Perfil"/>.
        /// </summary>
        public Perfil()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        /// <summary>
        /// Maneja el evento Load del control Perfil.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private async void Perfil_Load(object sender, EventArgs e)
        {
            await CargarDatosUsuario();


        }

        /// <summary>
        /// Carga los datos del usuario.
        /// </summary>
        private async Task CargarDatosUsuario()
        {
            try
            {
                string usuarioLogueado = SG_BAMS.Login.Login.UsuarioLogueado;

                if (!string.IsNullOrEmpty(usuarioLogueado))
                {
                    ClsUsuario objUsuario = new ClsUsuario();
                    DataTable datos = await objUsuario.ObtenerPerfilDesdeVista(usuarioLogueado);

                    if (datos.Rows.Count > 0)
                    {
                        DataRow fila = datos.Rows[0];


                        label4.Text = fila["nombre_usuario"].ToString();
                        label5.Text = fila["descripcion_rol"].ToString();


                        if (fila["imagen_usuario"] != DBNull.Value)
                        {
                            byte[] imagenBytes = (byte[])fila["imagen_usuario"];


                            using (MemoryStream ms = new MemoryStream(imagenBytes))
                            {
                                pbFotoPerfil.Image = Image.FromStream(ms);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar perfil e imagen: " + ex.Message);
            }
        }

        /// <summary>
        /// Maneja el evento Click del control kryptonButton1.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private async void kryptonButton1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Maneja el evento Shown del control Perfil.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void Perfil_Shown(object sender, EventArgs e)
        {
            Ayudante_UI.AplicarZoomGlobal(this);
        }

        /// <summary>
        /// Maneja el evento Click del control btnimagen.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private async void btnimagen_Click(object sender, EventArgs e)
        {
            OpenFileDialog selectorImagen = new OpenFileDialog();
            selectorImagen.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png";

            if (selectorImagen.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    byte[] imagenBytes = File.ReadAllBytes(selectorImagen.FileName);


                    string usuarioLogueado = SG_BAMS.Login.Login.UsuarioLogueado;

                    ClsUsuario objUsuario = new ClsUsuario();
                    await objUsuario.ActualizarFotoUsuario(usuarioLogueado, imagenBytes);


                    using (MemoryStream ms = new MemoryStream(imagenBytes))
                    {
                        pbFotoPerfil.Image = Image.FromStream(ms);
                    }

                    MessageBox.Show("Imagen de perfil actualizada correctamente.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al procesar la imagen: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Maneja el evento Click del control btnsalir1.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnsalir1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}