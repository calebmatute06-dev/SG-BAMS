using SG_BAMS.AccesoDatos;
using SG_BAMS.Administracion_de_BAMS.Usuarios;
using SG_BAMS.Bitacora;
using SG_BAMS.Facturas;
using SG_BAMS.LogicaNegocio.AdministracionBAMS;
using SG_BAMS.Login;
using SG_BAMS.ProductoInventario;
using SG_BAMS.Proveedor;
using SG_BAMS.Reporte;
using SG_BAMS.Dominio;                          

namespace SG_BAMS
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal de la aplicación.
        /// Composition Root: único lugar donde se instancian clases concretas.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DetectorRostroService.InicializarDirectorioEstatico();
            ApplicationConfiguration.Initialize();

            IGeneradorToken generadorToken = new GeneradorTokenCriptografico();
            IServicioSeguridad servicioSeguridad = new ServicioSeguridad(generadorToken);
            ClsRepositorioBaseDatos repositorio = new ClsRepositorioBaseDatos();
            ClsRecuperacion recuperacion = new ClsRecuperacion(repositorio, servicioSeguridad);
            IRecuperacionService recuperacionService = new RecuperacionService(recuperacion);
            ILoginService loginService = new LoginService(recuperacionService);
            IConfiguracionCorreo configCorreo = new ConfiguracionCorreo();
            IServicioCorreo servicioCorreo = new ServicioCorreo(configCorreo);
            ISesionUsuarioService sesionUsuario = SesionUsuarioService.Instancia;
            IRepositorioRostros repositorioRostros = new RepositorioRostros(DetectorRostroService.DirectorioRostros);

            Application.Run(new Login.Login(loginService, servicioCorreo, servicioSeguridad, sesionUsuario, repositorioRostros));
        }
    }
}