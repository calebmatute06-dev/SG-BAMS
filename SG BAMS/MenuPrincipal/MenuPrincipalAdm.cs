using SG_BAMS.Bitacora;
using SG_BAMS.Login;
using SG_BAMS.MenuPrincipal;
using SG_BAMS.Proveedor;
using SG_BAMS.Reporte;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Formulario de menú principal para usuarios con rol Administrador.
    /// Proporciona acceso a todas las funciones del sistema: inventario, facturas,
    /// clientes, proveedores, deudores, reportes, bitácora y administración.
    /// </summary>
    public partial class MenuPrincipalAdm : MenuPrincipalBase
    {
        private readonly NavegacionService navegacion = new NavegacionService();
        /// <summary>
        /// Constructor sin parámetros para compatibilidad con código existente.
        /// </summary>
        public MenuPrincipalAdm() : this(
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
        public MenuPrincipalAdm(
            IDashboardService dashboard,
            INotificacionesService notificacionesService,
            IChartBuilderService chartBuilder,
            IServicioCerrarSesion servicioCerrarSesion,
            NavegacionService navegacion,
            IToastService toastService)
            : base(dashboard, notificacionesService, chartBuilder, servicioCerrarSesion, navegacion, toastService)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        /// <inheritdoc/>
        protected override bool EsAdmin => true;

        /// <summary>
        /// Evento Load del formulario. Inicializa el dashboard con contadores y gráficos.
        /// </summary>
        private async void MenuPrincipalAdm_Load(object sender, EventArgs e)
        {
            ConfigurarBotonMenuActivo(btnMenu);
            VerificarNotificacionesAlCargar();
            await ActualizarLabelClientes(label7);
            await ActualizarLabelDeudores(label6);
            await ActualizarLabelProductos(label8);
            await CargarGraficoStock(chartStock);
            await CargarGraficoMasVendidos(chartMasVendidos);
        }

        private void btninventario2_Click(object sender, EventArgs e) { InventarioAdmin Invad = new InventarioAdmin(new ProductoInventario.ProductoRepository(), new ProductoInventario.ComboRepository()); Invad.Show(); this.Hide(); }
        private void btninventario3_Click(object sender, EventArgs e) { InventarioAdmin Invad = new InventarioAdmin(new ProductoInventario.ProductoRepository(), new ProductoInventario.ComboRepository()); Invad.Show(); this.Hide(); }
        private void btndeudores2_Click(object sender, EventArgs e) { DeudoresAdmin Deu = new DeudoresAdmin(new DeudaRepository()); Deu.Show(); this.Hide(); }
        private void btnclientes2_Click(object sender, EventArgs e) { ClientesAdm Client = new ClientesAdm(new Cliente.ClienteRepository()); Client.Show(); this.Hide(); }
        private void btnAdministracion_Click(object sender, EventArgs e) => NavegarA<AdministracionBAMS>();
        private void btnProd_Click(object sender, EventArgs e) { InventarioAdmin Invad = new InventarioAdmin(new ProductoInventario.ProductoRepository(), new ProductoInventario.ComboRepository()); Invad.Show(); this.Hide(); }
        private void btnFacturas_Click(object sender, EventArgs e) => NavegarA<FacturasAdm>();
        private void btnCompra_Click(object sender, EventArgs e) => NavegarA<Compras>();
        private void btnClientes_Click_1(object sender, EventArgs e) { ClientesAdm CA = new ClientesAdm(new Cliente.ClienteRepository()); CA.Show(); this.Hide(); }
        private void btnInventario_Click(object sender, EventArgs e) { InventarioAdmin IA = new InventarioAdmin(new ProductoInventario.ProductoRepository(), new ProductoInventario.ComboRepository()); IA.Show(); this.Hide(); }
        private void btnDeudores_Click(object sender, EventArgs e) { DeudoresAdmin DA = new DeudoresAdmin(new DeudaRepository()); DA.Show(); this.Hide(); }
        private void btnReportes_Click(object sender, EventArgs e) => NavegarA<ReportesAdmin>();

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            var PA = new ProveedoresAdmin(
                new ProveedorRepository(),
                new EstadoRepository(),
                new ClasificacionRepository());
            PA.Show();
            this.Hide();
        }

        private void btnBitacora_Click(object sender, EventArgs e) => navegacion.IrA(this, new BitacoraAdmin());


        private void btnadmin_Click(object sender, EventArgs e) => AbrirNotificaciones();
        private void btnCerrar_Click(object sender, EventArgs e) => CerrarSesion();
        private void btnPerfil_Click_1(object sender, EventArgs e) => AbrirPerfil();
    }
}