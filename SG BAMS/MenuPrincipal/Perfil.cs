using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO; // Necesario para manejar la memoria de la imagen

namespace SG_BAMS
{
    public partial class Perfil : Form
    {
        public string UsuarioActual { get; set; }

        public Perfil()
        {
            InitializeComponent();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // --- CORRECCIÓN AQUÍ: Debes llamar al método para que se ejecute ---
        private async void Perfil_Load(object sender, EventArgs e)
        {
            await CargarDatosUsuario();
        }

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

                        // 1. Mostrar Texto
                        label4.Text = fila["nombre_usuario"].ToString();
                        label5.Text = fila["descripcion_rol"].ToString();

                        // 2. Mostrar Imagen
                        if (fila["imagen_usuario"] != DBNull.Value)
                        {
                            byte[] imagenBytes = (byte[])fila["imagen_usuario"];

                            // Usamos MemoryStream para convertir los bytes de SQL en imagen
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
            OpenFileDialog selectorImagen = new OpenFileDialog();
            selectorImagen.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png";

            if (selectorImagen.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // 1. Convertimos el archivo seleccionado a bytes
                    byte[] imagenBytes = File.ReadAllBytes(selectorImagen.FileName);

                    // 2. Usamos el nombre que ya tenemos guardado en el Login
                    string usuarioLogueado = SG_BAMS.Login.Login.UsuarioLogueado;

                    ClsUsuario objUsuario = new ClsUsuario();
                    await objUsuario.ActualizarFotoUsuario(usuarioLogueado, imagenBytes);

                    // 3. Mostramos la imagen en el PictureBox inmediatamente
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
    }
}
