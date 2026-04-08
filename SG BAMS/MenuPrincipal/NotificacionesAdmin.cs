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
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class NotificacionesAdmin : Form
    {
        /// <summary>
        /// Indica si es administrador
        /// </summary>
        private bool esAdministrador;
        /// <summary>
        /// Conjunto de notificaciones leídas
        /// </summary>
        private HashSet<int> notificacionesLeidas = new HashSet<int>();
        /// <summary>
        /// Contador de notificaciones no leídas
        /// </summary>
        private int contadorNoLeidas = 0;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="NotificacionesAdmin"/>.
        /// </summary>
        public NotificacionesAdmin()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            DeterminarPermisos();

            notificaciones.DrawMode = DrawMode.OwnerDrawFixed;
            notificaciones.DrawItem += new DrawItemEventHandler(Notificaciones_DrawItem);
            notificaciones.DoubleClick += new EventHandler(Notificaciones_DoubleClick);
        }

        /// <summary>
        /// Maneja el evento Load del control NotificacionesAdmin.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void NotificacionesAdmin_Load(object sender, EventArgs e)
        {
            CargarListBox();
            Ayudante_UI.AplicarZoomGlobal(this);
        }

        /// <summary>
        /// Determina los permisos del usuario.
        /// </summary>
        private void DeterminarPermisos()
        {
            string usuarioActivo = SG_BAMS.Login.Login.UsuarioLogueado;
            this.esAdministrador = true;
        }

        /// <summary>
        /// Carga el ListBox con las notificaciones.
        /// </summary>
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

        /// <summary>
        /// Actualiza la etiqueta del contador.
        /// </summary>
        private void ActualizarLabelContador()
        {
            cantidadnotificaciones.Text = contadorNoLeidas.ToString();
            cantidadnotificaciones.ForeColor = (contadorNoLeidas > 0) ? Color.Red : Color.Gray;
        }


        /// <summary>
        /// Maneja el evento DoubleClick del control Notificaciones.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
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

                    ClsNotificaciones objNoti = new ClsNotificaciones();
                    bool exito = await objNoti.MarcarComoLeida(idNotificacion);

                    if (exito)
                    {

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

        /// <summary>
        /// Maneja el evento DrawItem del control Notificaciones.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="DrawItemEventArgs"/> que contiene los datos del evento.</param>
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

        /// <summary>
        /// Maneja el evento Shown del control NotificacionesAdmin.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void NotificacionesAdmin_Shown(object sender, EventArgs e) => Ayudante_UI.AplicarZoomGlobal(this);

        /// <summary>
        /// Maneja el evento SelectedIndexChanged del control listBox1.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) { }

        /// <summary>
        /// Maneja el evento MouseClick del control listBox1.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="MouseEventArgs"/> que contiene los datos del evento.</param>
        private void listBox1_MouseClick(object sender, MouseEventArgs e) { }

        /// <summary>
        /// Maneja el evento Click del control btnsalir1.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnsalir1_Click(object sender, EventArgs e) => this.Close();
    }
}