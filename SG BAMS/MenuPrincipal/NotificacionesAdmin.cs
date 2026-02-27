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
        private HashSet<int> notificacionesLeidas = new HashSet<int>();
        private int contadorNoLeidas = 0;

        public NotificacionesAdmin()
        {
            InitializeComponent();
            DeterminarPermisos();

            notificaciones.DrawMode = DrawMode.OwnerDrawFixed;
            notificaciones.DrawItem += new DrawItemEventHandler(Notificaciones_DrawItem);
            notificaciones.DoubleClick += new EventHandler(Notificaciones_DoubleClick);
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
                DataTable dtNotificaciones = objNoti.ListarNotificaciones(esAdministrador);

                if (dtNotificaciones != null)
                {
                    notificaciones.DataSource = null;
                    notificaciones.DisplayMember = "titulo";
                    notificaciones.ValueMember = "id_notificacion";
                    notificaciones.DataSource = dtNotificaciones;

                    contadorNoLeidas = dtNotificaciones.Rows.Count;
                    ActualizarLabelContador();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar: " + ex.Message);
            }
        }

        private void ActualizarLabelContador()
        {
            cantidadnotificaciones.Text = contadorNoLeidas.ToString();
            cantidadnotificaciones.ForeColor = (contadorNoLeidas > 0) ? Color.Red : Color.Gray;
        }

        // --- MODIFICADO: Ahora guarda en la Base de Datos ---
        private async void Notificaciones_DoubleClick(object sender, EventArgs e)
        {
            if (notificaciones.SelectedIndex != -1 && notificaciones.SelectedItem != null)
            {
                DataRowView filaSeleccionada = (DataRowView)notificaciones.SelectedItem;
                int idNotificacion = Convert.ToInt32(filaSeleccionada["id_notificacion"]);
                string tituloNotif = filaSeleccionada["titulo"].ToString();
                string mensajeNotif = filaSeleccionada["mensaje"].ToString();

                MessageBox.Show(mensajeNotif, tituloNotif, MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (!notificacionesLeidas.Contains(idNotificacion))
                {
                    // 1. Llamamos a la clase de lógica para actualizar SQL
                    ClsNotificaciones objNoti = new ClsNotificaciones();
                    bool exito = await objNoti.MarcarComoLeida(idNotificacion);

                    if (exito)
                    {
                        // 2. Si se guardó en SQL, actualizamos la interfaz
                        notificacionesLeidas.Add(idNotificacion);
                        if (contadorNoLeidas > 0)
                        {
                            contadorNoLeidas--;
                            ActualizarLabelContador();
                            notificaciones.Invalidate();
                        }
                    }
                }
            }
        }

        private void Notificaciones_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            DataRowView fila = (DataRowView)notificaciones.Items[e.Index];
            string tituloTexto = fila["titulo"].ToString().ToLower();
            int idActual = Convert.ToInt32(fila["id_notificacion"]);

            Color colorFondo = Color.White;

            if (tituloTexto.Contains("acabado") || tituloTexto.Contains("agotado"))
                colorFondo = Color.FromArgb(255, 210, 210);
            else if (tituloTexto.Contains("critico") || tituloTexto.Contains("bajo"))
                colorFondo = Color.FromArgb(255, 255, 210);

            if (notificacionesLeidas.Contains(idActual))
                colorFondo = Color.FromArgb(245, 245, 245);

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                colorFondo = SystemColors.Highlight;

            using (SolidBrush pincelFondo = new SolidBrush(colorFondo))
            {
                e.Graphics.FillRectangle(pincelFondo, e.Bounds);
            }

            Color colorTexto = (e.State & DrawItemState.Selected) == DrawItemState.Selected ? Color.White : Color.Black;

            FontStyle estilo = notificacionesLeidas.Contains(idActual) ? FontStyle.Regular : FontStyle.Bold;
            using (Font fuentePersonalizada = new Font(e.Font, estilo))
            {
                TextRenderer.DrawText(e.Graphics, fila["titulo"].ToString(), fuentePersonalizada, e.Bounds, colorTexto, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
            }

            e.DrawFocusRectangle();
        }

        private void btnsalir_Click(object sender, EventArgs e) => this.Close();

        private void NotificacionesAdmin_Shown(object sender, EventArgs e) => Ayudante_UI.AplicarZoomGlobal(this);

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) { }

        private void listBox1_MouseClick(object sender, MouseEventArgs e) { }
    }
}