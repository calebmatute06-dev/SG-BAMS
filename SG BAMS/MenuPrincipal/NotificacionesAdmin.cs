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
            DeterminarPermisos();

            
            listBox1.DrawMode = DrawMode.OwnerDrawFixed;
            listBox1.DrawItem += new DrawItemEventHandler(ListBox1_DrawItem);
        }

        private void NotificacionesAdmin_Load(object sender, EventArgs e)
        {
            CargarListBox();
            Ayudante_UI.AplicarZoomGlobal(this);
        }

        private void DeterminarPermisos()
        {
            string usuarioActivo = SG_BAMS.Login.Login.UsuarioLogueado;
            this.esAdministrador = true;
        }

        private void CargarListBox()
        {
            try
            {
                ClsNotificaciones objNoti = new ClsNotificaciones();
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

      
        private void ListBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            
            DataRowView fila = (DataRowView)listBox1.Items[e.Index];

           
            string tituloBusqueda = fila["titulo"].ToString().ToLower().Trim();

            Color colorFondo = Color.White;
            Color colorTexto = Color.Black;

            
            if (tituloBusqueda.Contains("agotado"))
            {
                colorFondo = Color.Firebrick;
                colorTexto = Color.White;
            }
            
            else if (tituloBusqueda.Contains("critic"))
            {
                colorFondo = Color.Gold;
                colorTexto = Color.Black;
            }

            
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                
                e.Graphics.FillRectangle(Brushes.LightBlue, e.Bounds);
                colorTexto = Color.Black;
            }
            else
            {
                using (SolidBrush brushFondo = new SolidBrush(colorFondo))
                {
                    e.Graphics.FillRectangle(brushFondo, e.Bounds);
                }
            }

            
            using (SolidBrush brushTexto = new SolidBrush(colorTexto))
            {
                
                string textoMostrar = listBox1.GetItemText(listBox1.Items[e.Index]);
                e.Graphics.DrawString(textoMostrar, e.Font, brushTexto, e.Bounds);
            }

            e.DrawFocusRectangle();
        }

  

        
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (listBox1.SelectedIndex != -1 && listBox1.SelectedItem != null)
            {
                DataRowView fila = (DataRowView)listBox1.SelectedItem;
                string titulo = fila["titulo"].ToString();
                string mensaje = fila["mensaje"].ToString();

                MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NotificacionesAdmin_Shown(object sender, EventArgs e)
        {
            Ayudante_UI.AplicarZoomGlobal(this);
        }
    }
}