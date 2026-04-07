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
        /// Gets or sets the usuario actual.
        /// </summary>
        /// <value>
        /// The usuario actual.
        /// </value>
        public string UsuarioActual { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Perfil"/> class.
        /// </summary>
        public Perfil()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        /// <summary>
        /// Handles the Load event of the Perfil control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void Perfil_Load(object sender, EventArgs e)
        {
            await CargarDatosUsuario();


        }

        /// <summary>
        /// Cargars the datos usuario.
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
        /// Handles the Click event of the kryptonButton1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void kryptonButton1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Shown event of the Perfil control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Perfil_Shown(object sender, EventArgs e)
        {
            Ayudante_UI.AplicarZoomGlobal(this);
        }

        /// <summary>
        /// Handles the Click event of the btnimagen control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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
        /// Handles the Click event of the btnsalir1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnsalir1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}