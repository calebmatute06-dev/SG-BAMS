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
    public partial class MenuPrincipalAdm : MenuPrincipalBase
    {
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

        protected override bool EsAdmin => true;

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

        private void btninventario2_Click(object sender, EventArgs e) => NavegarA<InventarioAdmin>();
        private void btninventario3_Click(object sender, EventArgs e) => NavegarA<InventarioAdmin>();
        private void btndeudores2_Click(object sender, EventArgs e) => NavegarA<DeudoresAdmin>();
        private void btnclientes2_Click(object sender, EventArgs e) => NavegarA<ClientesAdm>();
        private void btnAdministracion_Click(object sender, EventArgs e) => NavegarA<AdministracionBAMS>();
        private void btnProd_Click(object sender, EventArgs e) => NavegarA<InventarioAdmin>();
        private void btnFacturas_Click(object sender, EventArgs e) => NavegarA<FacturasAdm>();
        private void btnCompra_Click(object sender, EventArgs e) => NavegarA<Compras>();
        private void btnClientes_Click_1(object sender, EventArgs e) => NavegarA<ClientesAdm>();
        private void btnInventario_Click(object sender, EventArgs e) => NavegarA<InventarioAdmin>();
        private void btnDeudores_Click(object sender, EventArgs e) => NavegarA<DeudoresAdmin>();
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

        private void btnBitacora_Click(object sender, EventArgs e)
        {
            var Bi = new BitacoraAdmin(
                new BitacoraRepository(),
                new FiltroBitacoraService(),
                new ReporteBitacoraPdfExportador());
            Bi.Show();
            this.Hide();
        }

        private void btnadmin_Click(object sender, EventArgs e) => AbrirNotificaciones();
        private void btnCerrar_Click(object sender, EventArgs e) => CerrarSesion();
        private void btnPerfil_Click_1(object sender, EventArgs e) => AbrirPerfil();
    }
}