using SG_BAMS.Login;
using SG_BAMS.MenuPrincipal;

namespace SG_BAMS
{
    /// <summary>
    /// Formulario de menú principal para usuarios con rol Empleado.
    /// Proporciona acceso a las funciones permitidas para este rol: inventario,
    /// facturas, clientes, deudores y notificaciones.
    /// </summary>
    public partial class MenuPrincipalEmp : MenuPrincipalBase
    {
        /// <summary>
        /// Constructor sin parámetros para compatibilidad con código existente.
        /// </summary>
        public MenuPrincipalEmp() : this(
            new ClsDashboard(new ClsRepositorioBaseDatos()),
            new ClsNotificaciones(new ClsRepositorioBaseDatos()),
            new ChartBuilderService(),
            new ServicioCerrarSesion(new NavegacionFormsService(
                new ServicioCorreo(new ConfiguracionCorreo()),
                new ServicioSeguridad())),
            new NavegacionService(),
            new ToastNotificacion())
        {
        }

        /// <summary>
        /// Constructor principal que recibe todas las dependencias necesarias.
        /// </summary>
        /// <param name="dashboard">Servicio de dashboard con contadores y gráficos.</param>
        /// <param name="notificacionesService">Servicio de gestión de notificaciones.</param>
        /// <param name="chartBuilder">Servicio de construcción de gráficos.</param>
        /// <param name="servicioCerrarSesion">Servicio de cierre de sesión.</param>
        /// <param name="navegacion">Servicio de navegación entre formularios.</param>
        /// <param name="toastService">Servicio de notificaciones toast.</param>
        public MenuPrincipalEmp(
            IDashboardService dashboard,
            INotificacionesService notificacionesService,
            IChartBuilderService chartBuilder,
            IServicioCerrarSesion servicioCerrarSesion,
            NavegacionService navegacion,
            IToastService toastService)
            : base(dashboard, notificacionesService, chartBuilder, servicioCerrarSesion, navegacion, toastService)
        {
            InitializeComponent();
            AdaptadorPantallaCompleta.Habilitar(this);
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        /// <inheritdoc/>
        protected override bool EsAdmin => false;

        /// <summary>
        /// Evento Load del formulario. Inicializa el dashboard, aplica el tema
        /// configurado y carga las ventas recientes.
        /// </summary>
        private async void MenuPrincipalEmp_Load(object sender, EventArgs e)
        {
            ConfigurarBotonMenuActivo(btnMenu);
            VerificarNotificacionesAlCargar();
            await ActualizarLabelClientes(label7);
            await ActualizarLabelDeudores(label6);
            await ActualizarLabelProductos(lblConteoProductos);
            await CargarVentasRecientes(dgvVentas);
            await CargarGraficoStock(chartStock1);
            ClsTemas.CargarPreferencia();
            ClsTemas.AplicarTema(this);
            EstilizarGrid(dgvVentas);
        }

        private void MenuPrincipalEmp_Shown(object sender, EventArgs e) => Ayudante_UI.AplicarZoomGlobal(this);

        private void btninventario2_Click(object sender, EventArgs e) { InventarioEmp invemp = new InventarioEmp(new ProductoInventario.ProductoRepository()); invemp.Show(); this.Hide(); }
        private void btninventario3_Click(object sender, EventArgs e) { InventarioEmp invemp = new InventarioEmp(new ProductoInventario.ProductoRepository()); invemp.Show(); this.Hide(); }
        private void btnFacturas_Click(object sender, EventArgs e) => NavegarA<FacturasEmp>();
        private void btnClientes_Click(object sender, EventArgs e) { ClientesEmp CE = new ClientesEmp(new Cliente.ClienteRepository()); CE.Show(); this.Hide(); }
        private void btnInventario_Click(object sender, EventArgs e) { InventarioEmp IE = new InventarioEmp(new ProductoInventario.ProductoRepository()); IE.Show(); this.Hide(); }
        private void btnDeudores_Click(object sender, EventArgs e) { Deudores_Emp DE = new Deudores_Emp(new DeudaRepository()); DE.Show(); this.Hide(); }
        private void btnVentas_Click(object sender, EventArgs e) => NavegarA<FacturasEmp>();

        private async void btndeudores2_Click(object sender, EventArgs e) { Deudores_Emp deudoresForm = new Deudores_Emp(new DeudaRepository()); deudoresForm.Show(); await ActualizarLabelDeudores(); this.Hide(); }
        private async Task ActualizarLabelDeudores()
        {
            int totalDeudores = await dashboard.ObtenerTotalDeudores();
            label6.Text = totalDeudores != -1 ? totalDeudores.ToString() : "0";
        }

        private async void btnclientes2_Click(object sender, EventArgs e)
        {
            ClientesEmp clienemp = new ClientesEmp(new Cliente.ClienteRepository());
            clienemp.Show();
            await ActualizarLabelClientes(label7);
            this.Hide();
        }

        private void btnnotificaciones_Click(object sender, EventArgs e) => AbrirNotificaciones();
        private void btnCerrar_Click(object sender, EventArgs e) => CerrarSesion();
        private void btnPerfil_Click(object sender, EventArgs e) => AbrirPerfil();
    }
}