using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using SG_BAMS.Login;

namespace SG_BAMS
{
    /// <summary>
    /// Formulario para visualizar y gestionar notificaciones.
    /// </summary>
    public partial class NotificacionesAdmin : Form
    {
        private readonly INotificacionesService _notificacionesService;
        private readonly ISesionUsuarioService _sesionUsuario;
        private readonly NotificacionListBoxRenderer _listBoxRenderer;
        private bool esAdministrador;
        private HashSet<int> notificacionesLeidas = new HashSet<int>();
        private int contadorNoLeidas = 0;

        /// <summary>
        /// Constructor sin parámetros para compatibilidad.
        /// </summary>
        public NotificacionesAdmin() : this(
            new ClsNotificaciones(),
            SesionUsuarioService.Instancia)
        {
        }

        /// <summary>
        /// Constructor principal con inyección de dependencias.
        /// 
        /// DIP: Recibe INotificacionesService e ISesionUsuarioService.
        /// SRP: El renderizado se delega en NotificacionListBoxRenderer.
        /// </summary>
        /// <param name="notificacionesService">Servicio de notificaciones.</param>
        /// <param name="sesionUsuario">Servicio de sesión del usuario.</param>
        /// <exception cref="ArgumentNullException">Si algún servicio es nulo.</exception>
        public NotificacionesAdmin(
            INotificacionesService notificacionesService,
            ISesionUsuarioService sesionUsuario)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            _notificacionesService = notificacionesService ?? throw new ArgumentNullException(nameof(notificacionesService));
            _sesionUsuario = sesionUsuario ?? throw new ArgumentNullException(nameof(sesionUsuario));


            _listBoxRenderer = new NotificacionListBoxRenderer(notificacionesLeidas);

            DeterminarPermisos();

            notificaciones.DrawMode = DrawMode.OwnerDrawFixed;
            notificaciones.DrawItem += _listBoxRenderer.DrawItem;
            notificaciones.DoubleClick += Notificaciones_DoubleClick;
        }

        private void NotificacionesAdmin_Load(object sender, EventArgs e)
        {
            CargarListBox();
            Ayudante_UI.AplicarZoomGlobal(this);
        }

        /// <summary>
        /// Determina los permisos del usuario usando el servicio de sesión inyectado.
        /// 
        /// DIP: Usa ISesionUsuarioService en lugar de Login.UsuarioLogueado estático.
        /// </summary>
        private void DeterminarPermisos()
        {
            string usuarioActivo = _sesionUsuario.NombreUsuario;
            this.esAdministrador = true;
        }

        /// <summary>
        /// Carga el ListBox con las notificaciones.
        /// 
        /// DIP: Usa el servicio inyectado en lugar de "new ClsNotificaciones()".
        /// </summary>
        private void CargarListBox()
        {
            try
            {
                DataTable dtNotificaciones = _notificacionesService.ListarNotificaciones(esAdministrador);

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

        /// <summary>
        /// Evento DoubleClick en una notificación.
        /// 
        /// DIP: Usa el servicio inyectado para marcar como leída.
        /// </summary>
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
                    bool exito = await _notificacionesService.MarcarComoLeida(idNotificacion);

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

        private void NotificacionesAdmin_Shown(object sender, EventArgs e) => Ayudante_UI.AplicarZoomGlobal(this);
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void listBox1_MouseClick(object sender, MouseEventArgs e) { }
        private void btnsalir1_Click(object sender, EventArgs e) => this.Close();
    }
}