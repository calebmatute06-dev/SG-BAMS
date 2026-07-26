using SG_BAMS.Login;
using SG_BAMS.MenuPrincipal;
using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using SG_BAMS.AccesoDatos;
using SG_BAMS.Dominio;
using SG_BAMS.LogicaNegocio;

namespace SG_BAMS
{
    /// <summary>
    /// Clase base para los formularios de menú principal del sistema.
    /// Contiene la lógica compartida entre los menús de Administrador y Empleado:
    /// dashboard, notificaciones, gráficos, cierre de sesión y navegación.
    /// </summary>
    public partial class MenuPrincipalBase : Form
    {
        /// <summary>Servicio de dashboard con contadores y gráficos.</summary>
        protected readonly IDashboardService dashboard;

        /// <summary>Servicio de gestión de notificaciones.</summary>
        protected readonly INotificacionesService notificacionesService;

        /// <summary>Servicio de construcción de gráficos.</summary>
        protected readonly IChartBuilderService chartBuilder;

        /// <summary>Servicio de cierre de sesión.</summary>
        protected readonly IServicioCerrarSesion servicioCerrarSesion;

        /// <summary>Servicio de navegación entre formularios.</summary>
        protected readonly NavegacionService navegacion;

        /// <summary>Servicio de notificaciones toast.</summary>
        protected readonly IToastService toastService;

        /// <summary>
        /// Indica si el usuario actual tiene rol de administrador.
        /// </summary>
        protected virtual bool EsAdmin => true;

        /// <summary>
        /// Constructor principal que recibe todas las dependencias necesarias.
        /// </summary>
        /// <param name="dashboard">Servicio de dashboard.</param>
        /// <param name="notificacionesService">Servicio de notificaciones.</param>
        /// <param name="chartBuilder">Servicio de gráficos.</param>
        /// <param name="servicioCerrarSesion">Servicio de cierre de sesión.</param>
        /// <param name="navegacion">Servicio de navegación.</param>
        /// <param name="toastService">Servicio de notificaciones toast.</param>
        /// <exception cref="ArgumentNullException">Si algún servicio es nulo.</exception>
        protected MenuPrincipalBase(
            IDashboardService dashboard,
            INotificacionesService notificacionesService,
            IChartBuilderService chartBuilder,
            IServicioCerrarSesion servicioCerrarSesion,
            NavegacionService navegacion,
            IToastService toastService)
        {
            this.dashboard = dashboard ?? throw new ArgumentNullException(nameof(dashboard));
            this.notificacionesService = notificacionesService ?? throw new ArgumentNullException(nameof(notificacionesService));
            this.chartBuilder = chartBuilder ?? throw new ArgumentNullException(nameof(chartBuilder));
            this.servicioCerrarSesion = servicioCerrarSesion ?? throw new ArgumentNullException(nameof(servicioCerrarSesion));
            this.navegacion = navegacion ?? throw new ArgumentNullException(nameof(navegacion));
            this.toastService = toastService ?? throw new ArgumentNullException(nameof(toastService));
        }

        /// <summary>
        /// Verifica si hay notificaciones pendientes y muestra un toast al cargar el formulario.
        /// </summary>
        protected async void VerificarNotificacionesAlCargar()
        {
            try
            {
                var dt = notificacionesService.ListarNotificaciones(EsAdmin);
                if (dt != null && dt.Rows.Count > 0)
                    toastService.MostrarResumen(dt);
            }
            catch { }
        }

        /// <summary>
        /// Verifica notificaciones manualmente y muestra resultado.
        /// </summary>
        protected async void VerificarNotificacionesAhora()
        {
            try
            {
                var dt = notificacionesService.ListarNotificaciones(EsAdmin);
                if (dt != null && dt.Rows.Count > 0)
                    toastService.MostrarResumen(dt);
                else
                    toastService.Mostrar("Centro de Notificaciones", "Sin notificaciones nuevas", 4);
            }
            catch { }
        }

        /// <summary>
        /// Actualiza el contador de clientes activos en la etiqueta especificada.
        /// </summary>
        /// <param name="label">Etiqueta donde se mostrará el total.</param>
        protected async Task ActualizarLabelClientes(Label label)
        {
            int total = await dashboard.ObtenerTotalClientes();
            label.Text = total != -1 ? total.ToString() : "0";
        }

        /// <summary>
        /// Actualiza el contador de deudores activos en la etiqueta especificada.
        /// </summary>
        /// <param name="label">Etiqueta donde se mostrará el total.</param>
        protected async Task ActualizarLabelDeudores(Label label)
        {
            int totalDeudores = await dashboard.ObtenerTotalDeudores();
            label.Text = totalDeudores != -1 ? totalDeudores.ToString() : "0";
        }

        /// <summary>
        /// Actualiza el contador de productos activos en la etiqueta especificada.
        /// </summary>
        /// <param name="label">Etiqueta donde se mostrará el total.</param>
        protected async Task ActualizarLabelProductos(Label label)
        {
            int totalProductos = await dashboard.ObtenerTotalProductos();
            label.Text = totalProductos != -1 ? totalProductos.ToString() : "0";
        }

        /// <summary>
        /// Carga el gráfico de stock de productos en el control especificado.
        /// </summary>
        /// <param name="chart">Control Chart donde se mostrará el gráfico.</param>
        protected async Task CargarGraficoStock(Chart chart)
        {
            DataTable tablaStock = await dashboard.ObtenerDatosGraficoStock();
            await chartBuilder.CargarGraficoStock(chart, tablaStock);
        }

        /// <summary>
        /// Carga el gráfico de productos más vendidos en el control especificado.
        /// </summary>
        /// <param name="chart">Control Chart donde se mostrará el gráfico.</param>
        protected async Task CargarGraficoMasVendidos(Chart chart)
        {
            try
            {
                DataTable datosVentas = await dashboard.ObtenerProductosMasVendidos();
                await chartBuilder.CargarGraficoMasVendidos(chart, datosVentas);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar gráfico: " + ex.Message);
            }
        }

        /// <summary>
        /// Carga las ventas recientes en el DataGridView especificado.
        /// </summary>
        /// <param name="dgv">DataGridView donde se mostrarán las ventas.</param>
        protected async Task CargarVentasRecientes(DataGridView dgv)
        {
            DataTable datosVentas = await dashboard.ObtenerVentasRecientes();
            if (datosVentas != null)
            {
                dgv.DataSource = datosVentas;
                if (dgv.Columns.Contains("factura_id"))
                    dgv.Columns["factura_id"].HeaderText = "N° Factura";
                if (dgv.Columns.Contains("nombre_completo_cliente"))
                    dgv.Columns["nombre_completo_cliente"].HeaderText = "Cliente";
                if (dgv.Columns.Contains("fecha_registro"))
                    dgv.Columns["fecha_registro"].HeaderText = "Fecha";
                if (dgv.Columns.Contains("metodo_pago"))
                    dgv.Columns["metodo_pago"].HeaderText = "Pago";
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        /// <summary>
        /// Aplica el estilo visual estándar a un DataGridView.
        /// </summary>
        /// <param name="dgv">DataGridView a estilizar.</param>
        protected void EstilizarGrid(DataGridView dgv)
        {
            dgv.BorderStyle = BorderStyle.None;
            dgv.BackgroundColor = Color.White;
            dgv.RowHeadersVisible = false;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.SkyBlue;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Navy;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 28;

            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = Color.Navy;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgv.DefaultCellStyle.Padding = new Padding(3);

            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 245, 255);
            dgv.AlternatingRowsDefaultCellStyle.ForeColor = Color.Navy;

            dgv.DefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;

            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.LightGray;
            dgv.RowTemplate.Height = 32;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.ClearSelection();
        }

        /// <summary>
        /// Navega a un formulario del tipo especificado y oculta el formulario actual.
        /// </summary>
        /// <typeparam name="T">Tipo de formulario destino.</typeparam>
        protected void NavegarA<T>() where T : Form, new()
        {
            T destino = new T();
            navegacion.IrA(this, destino);
        }

        /// <summary>
        /// Abre el formulario de notificaciones como diálogo.
        /// </summary>
        protected void AbrirNotificaciones()
        {
            NotificacionesAdmin admin = new NotificacionesAdmin(
                notificacionesService,
                SesionUsuarioService.Instancia);
            admin.ShowDialog();
        }

        /// <summary>
        /// Abre el formulario de perfil de usuario como diálogo.
        /// </summary>
        protected void AbrirPerfil()
        {
            Perfil perfil = new Perfil(
                new ClsPerfil(new ClsRepositorioBaseDatos()),
                SesionUsuarioService.Instancia);
            perfil.ShowDialog();
        }

        /// <summary>
        /// Cierra la sesión actual y regresa al formulario de inicio de sesión.
        /// </summary>
        protected void CerrarSesion()
        {
            servicioCerrarSesion.CerrarSesion(this);
        }

        /// <summary>
        /// Configura el estilo visual del botón de menú activo.
        /// </summary>
        /// <param name="btnMenu">Control de tipo botón a configurar.</param>
        protected void ConfigurarBotonMenuActivo(Control btnMenu)
        {
            btnMenu.Enabled = false;
            btnMenu.BackColor = Color.SkyBlue;
            btnMenu.ForeColor = Color.White;
        }

        /// <summary>
        /// Constructor sin parámetros requerido por el diseñador de Windows Forms.
        /// </summary>
        protected MenuPrincipalBase()
        {
        }
    }
}