using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS
{
    public partial class NotificacionesAdmin : Form
    {
        private bool esAdministrador;

        public NotificacionesAdmin()
        {
            InitializeComponent();
            // Determinamos el rol usando la variable estática que ya tienes en Login
            DeterminarPermisos();
        }

        private void NotificacionesAdmin_Load(object sender, EventArgs e)
        {
            CargarListBox();
            Ayudante_UI.AplicarZoomGlobal(this);
        }

        private void DeterminarPermisos()
        {
            // Obtenemos el nombre que guardaste en el Login
            string usuarioActivo = SG_BAMS.Login.Login.UsuarioLogueado;

            // Aquí puedes hacer una consulta rápida o, si lo prefieres, 
            // pasar el rol desde el login también como estático.
            // Por ahora, asumiremos que el sistema cargará según el usuario.
            // Si quieres que el form sepa si es Admin, podrías comparar el nombre o rol.
            this.esAdministrador = true; // Por defecto true para que veas todo ahora
        }

        private void CargarListBox()
        {
            try
            {
                ClsNotificaciones objNoti = new ClsNotificaciones();
                // Usamos la variable que definimos arriba
                DataTable dt = objNoti.ListarNotificaciones(esAdministrador);

                if (dt != null)
                {
                    listBox1.DataSource = dt;
                    listBox1.DisplayMember = "titulo";
                    listBox1.ValueMember = "id_notificacion";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar: " + ex.Message);
            }
        }




        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1 && listBox1.SelectedItem != null)
            {
                DataRowView fila = (DataRowView)listBox1.SelectedItem;
                string titulo = fila["titulo"].ToString();
                string mensaje = fila["mensaje"].ToString();

                MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void NotificacionesAdmin_Shown(object sender, EventArgs e)
        {
            Ayudante_UI.AplicarZoomGlobal(this);
        }
    }
}
