using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using SG_BAMS.Login;

namespace SG_BAMS
{
    /// <summary>
    /// Formulario para visualizar y gestionar las notificaciones del sistema.
    /// Permite al usuario ver el detalle de cada notificación y marcarlas como leídas.
    /// </summary>
    public partial class NotificacionesAdmin : Form
    {
        private readonly INotificacionesService notificacionesService;
        private readonly ISesionUsuarioService sesionUsuario;
        private readonly NotificacionListBoxRenderer listBoxRenderer;
        private bool esAdministrador;
        private HashSet<int> notificacionesLeidas = new HashSet<int>();
        private int contadorNoLeidas = 0;

        /// <summary>
        /// Constructor sin parámetros para compatibilidad con código existente.
        /// </summary>
        public NotificacionesAdmin() : this(
            new ClsNotificaciones(),
            SesionUsuarioService.Instancia)
        {
        }

        /// <summary>
        /// Constructor principal que recibe los servicios necesarios.
        /// </summary>
        /// <param name="notificacionesService">Servicio de gestión de notificaciones.</param>
        /// <param name="sesionUsuario">Servicio de sesión del usuario actual.</param>
        /// <exception cref="ArgumentNullException">Si algún servicio es nulo.</exception>
        public NotificacionesAdmin(
            INotificacionesService notificacionesService,
            ISesionUsuarioService sesionUsuario)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            this.notificacionesService = notificacionesService ?? throw new ArgumentNullException(nameof(notificacionesService));
            this.sesionUsuario = sesionUsuario ?? throw new ArgumentNullException(nameof(sesionUsuario));

            listBoxRenderer = new NotificacionListBoxRenderer(notificacionesLeidas);

            DeterminarPermisos();

            notificaciones.DrawMode = DrawMode.OwnerDrawFixed;
            notificaciones.DrawItem += listBoxRenderer.DrawItem;
            notificaciones.DoubleClick += Notificaciones_DoubleClick;
        }

        /// <summary>
        /// Evento Load del formulario. Carga las notificaciones en el ListBox.
        /// </summary>
        private void NotificacionesAdmin_Load(object sender, EventArgs e)
        {
            CargarListBox();
            Ayudante_UI.AplicarZoomGlobal(this);
        }

        /// <summary>
        /// Determina los permisos del usuario actual basándose en el servicio de sesión.
        /// </summary>
        private void DeterminarPermisos()
        {
            string usuarioActivo = sesionUsuario.NombreUsuario;
            this.esAdministrador = true;
        }

        /// <summary>
        /// Carga la lista de notificaciones desde el servicio y las muestra en el ListBox.
        /// </summary>
        private void CargarListBox()
        {
            try
            {
                DataTable dtNotificaciones = notificacionesService.ListarNotificaciones(esAdministrador);

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
        /// Actualiza el contador visual de notificaciones no leídas.
        /// </summary>
        private void ActualizarLabelContador()
        {
            cantidadnotificaciones.Text = contadorNoLeidas.ToString();
            cantidadnotificaciones.ForeColor = (contadorNoLeidas > 0) ? Color.Red : Color.Gray;
        }

        /// <summary>
        /// Evento DoubleClick en una notificación. Muestra el detalle y la marca como leída.
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
                    bool exito = await notificacionesService.MarcarComoLeida(idNotificacion);

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

        /// <summary>
        /// Evento Click del botón Salir. Cierra el formulario de notificaciones.
        /// </summary>
        private void btnsalir1_Click(object sender, EventArgs e) => this.Close();
    }
}