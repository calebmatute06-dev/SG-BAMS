using SG_BAMS.Login;
using SG_BAMS.MenuPrincipal;
using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SG_BAMS
{
    /// <summary>
    /// Clase base para los formularios de menú principal (Admin y Empleado).
    /// Contiene toda la lógica compartida: dashboard, notificaciones, gráficos,
    /// cierre de sesión y navegación genérica.
    /// </summary>
    public partial class MenuPrincipalBase : Form
    {
        protected readonly IDashboardService _dashboard;
        protected readonly INotificacionesService _notificacionesService;
        protected readonly IChartBuilderService _chartBuilder;
        protected readonly IServicioCerrarSesion _servicioCerrarSesion;
        protected readonly NavegacionService _navegacion;
        protected readonly IToastService _toastService;

        protected virtual bool EsAdmin => true;

        protected MenuPrincipalBase(
            IDashboardService dashboard,
            INotificacionesService notificacionesService,
            IChartBuilderService chartBuilder,
            IServicioCerrarSesion servicioCerrarSesion,
            NavegacionService navegacion,
            IToastService toastService)
        {
            _dashboard = dashboard ?? throw new ArgumentNullException(nameof(dashboard));
            _notificacionesService = notificacionesService ?? throw new ArgumentNullException(nameof(notificacionesService));
            _chartBuilder = chartBuilder ?? throw new ArgumentNullException(nameof(chartBuilder));
            _servicioCerrarSesion = servicioCerrarSesion ?? throw new ArgumentNullException(nameof(servicioCerrarSesion));
            _navegacion = navegacion ?? throw new ArgumentNullException(nameof(navegacion));
            _toastService = toastService ?? throw new ArgumentNullException(nameof(toastService));
        }


        protected async void VerificarNotificacionesAlCargar()
        {
            try
            {
                var dt = _notificacionesService.ListarNotificaciones(EsAdmin);
                if (dt != null && dt.Rows.Count > 0)
                    _toastService.MostrarResumen(dt);
            }
            catch { }
        }

        protected async void VerificarNotificacionesAhora()
        {
            try
            {
                var dt = _notificacionesService.ListarNotificaciones(EsAdmin);
                if (dt != null && dt.Rows.Count > 0)
                    _toastService.MostrarResumen(dt);
                else
                    _toastService.Mostrar("Centro de Notificaciones", "Sin notificaciones nuevas", 4);
            }
            catch { }
        }



        protected async Task ActualizarLabelClientes(Label label)
        {
            int total = await _dashboard.ObtenerTotalClientes();
            label.Text = total != -1 ? total.ToString() : "0";
        }

        protected async Task ActualizarLabelDeudores(Label label)
        {
            int totalDeudores = await _dashboard.ObtenerTotalDeudores();
            label.Text = totalDeudores != -1 ? totalDeudores.ToString() : "0";
        }

        protected async Task ActualizarLabelProductos(Label label)
        {
            int totalProductos = await _dashboard.ObtenerTotalProductos();
            label.Text = totalProductos != -1 ? totalProductos.ToString() : "0";
        }


        protected async Task CargarGraficoStock(Chart chart)
        {
            DataTable tablaStock = await _dashboard.ObtenerDatosGraficoStock();
            await _chartBuilder.CargarGraficoStock(chart, tablaStock);
        }

        protected async Task CargarGraficoMasVendidos(Chart chart)
        {
            try
            {
                DataTable datosVentas = await _dashboard.ObtenerProductosMasVendidos();
                await _chartBuilder.CargarGraficoMasVendidos(chart, datosVentas);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar gráfico: " + ex.Message);
            }
        }

        protected async Task CargarVentasRecientes(DataGridView dgv)
        {
            DataTable datosVentas = await _dashboard.ObtenerVentasRecientes();
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



        protected void NavegarA<T>() where T : Form, new()
        {
            T destino = new T();
            _navegacion.IrA(this, destino);
        }


        protected void AbrirNotificaciones()
        {
            NotificacionesAdmin admin = new NotificacionesAdmin(
                _notificacionesService,
                SesionUsuarioService.Instancia);
            admin.ShowDialog();
        }



        protected void AbrirPerfil()
        {
            Perfil perfil = new Perfil(
                new ClsPerfil(new ClsRepositorioBaseDatos()),
                SesionUsuarioService.Instancia);
            perfil.ShowDialog();
        }


        protected void CerrarSesion()
        {
            _servicioCerrarSesion.CerrarSesion(this);
        }

   

        protected void ConfigurarBotonMenuActivo(Control btnMenu)
        {
            btnMenu.Enabled = false;
            btnMenu.BackColor = Color.SkyBlue;
            btnMenu.ForeColor = Color.White;
        }

        /// <summary>
        /// Constructor sin parámetros SOLO para el diseñador de Windows Forms.
        /// </summary>
        protected MenuPrincipalBase()
        {
        }
    }
}