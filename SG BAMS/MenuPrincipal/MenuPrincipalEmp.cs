using SG_BAMS.Login;
using SG_BAMS.MenuPrincipal;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS
{
    public partial class MenuPrincipalEmp : MenuPrincipalBase
    {
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
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        protected override bool EsAdmin => false;

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

        private void btninventario2_Click(object sender, EventArgs e) => NavegarA<InventarioEmp>();
        private void btninventario3_Click(object sender, EventArgs e) => NavegarA<InventarioEmp>();
        private void btnFacturas_Click(object sender, EventArgs e) => NavegarA<FacturasEmp>();
        private void btnClientes_Click(object sender, EventArgs e) => NavegarA<ClientesEmp>();
        private void btnInventario_Click(object sender, EventArgs e) => NavegarA<InventarioEmp>();
        private void btnDeudores_Click(object sender, EventArgs e) => NavegarA<Deudores_Emp>();
        private void btnVentas_Click(object sender, EventArgs e) => NavegarA<FacturasEmp>();

        private async void btndeudores2_Click(object sender, EventArgs e)
        {
            Deudores_Emp deudoresForm = new Deudores_Emp();
            deudoresForm.Show();
            await ActualizarLabelDeudores(label6);
            this.Hide();
        }

        private async void btnclientes2_Click(object sender, EventArgs e)
        {
            ClientesEmp clienemp = new ClientesEmp();
            clienemp.Show();
            await ActualizarLabelClientes(label7);
            this.Hide();
        }

        private void btnnotificaciones_Click(object sender, EventArgs e) => AbrirNotificaciones();
        private void btnCerrar_Click(object sender, EventArgs e) => CerrarSesion();
        private void btnPerfil_Click(object sender, EventArgs e) => AbrirPerfil();
    }
}