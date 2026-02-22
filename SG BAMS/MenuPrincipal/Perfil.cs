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
    }
}
