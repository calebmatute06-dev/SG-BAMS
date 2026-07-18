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
using SG_BAMS.MenuPrincipal;

namespace SG_BAMS
{
    public partial class Perfil : Form
    {
        public string UsuarioActual { get; set; }

        public Perfil()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private async void Perfil_Load(object sender, EventArgs e)
        {
            await CargarDatosUsuario();
        }

        private async Task CargarDatosUsuario()
        {
            try
            {
                // Usa el nombre completo para buscar en la BD
                string usuarioLogueado = SG_BAMS.Login.Login.UsuarioLogueadoCompleto;

                // Si está vacío, intenta con el nombre recortado
                if (string.IsNullOrEmpty(usuarioLogueado))
                    usuarioLogueado = SG_BAMS.Login.Login.UsuarioLogueado;

                if (!string.IsNullOrEmpty(usuarioLogueado))
                {
                    ClsPerfil objUsuario = new ClsPerfil();
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

        private async void kryptonButton1_Click(object sender, EventArgs e)
        {
        }

        private void Perfil_Shown(object sender, EventArgs e)
        {
            Ayudante_UI.AplicarZoomGlobal(this);
        }

        private async void btnimagen_Click(object sender, EventArgs e)
        {
            OpenFileDialog selectorImagen = new OpenFileDialog();
            selectorImagen.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png";

            if (selectorImagen.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    byte[] imagenBytes = File.ReadAllBytes(selectorImagen.FileName);

                    string usuarioLogueado = SG_BAMS.Login.Login.UsuarioLogueadoCompleto;
                    if (string.IsNullOrEmpty(usuarioLogueado))
                        usuarioLogueado = SG_BAMS.Login.Login.UsuarioLogueado;

                    ClsPerfil objUsuario = new ClsPerfil();
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

        private void btnsalir1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}