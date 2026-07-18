using System;
using System.Windows.Forms;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Formulario de soporte que permite elegir entre vista de Administrador o Empleado.
    /// </summary>
    public partial class Soporte : Form
    {
        private readonly INavegacionFormsService _navegacionForms;

        /// <summary>
        /// Constructor sin parámetros para compatibilidad con código existente.
        /// </summary>
        public Soporte() : this(new NavegacionFormsService(
            new ServicioCorreo(new ConfiguracionCorreo()),
            new ServicioSeguridad()))
        {
        }

        /// <summary>
        /// Constructor principal con inyección de dependencias.
        /// 
        /// DIP: Recibe INavegacionFormsService en lugar de instanciar
        /// MenuPrincipalAdm o MenuPrincipalEmp directamente.
        /// </summary>
        /// <param name="navegacionForms">Servicio de navegación entre formularios.</param>
        /// <exception cref="ArgumentNullException">Si navegacionForms es nulo.</exception>
        public Soporte(INavegacionFormsService navegacionForms)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            _navegacionForms = navegacionForms ?? throw new ArgumentNullException(nameof(navegacionForms));
        }

        /// <summary>
        /// Evento Click del botón "Ver como Administrador".
        /// 
        /// DIP: La navegación se delega en INavegacionFormsService.IrA<T>()
        /// en lugar de hacer "new MenuPrincipalAdm()" directamente.
        /// </summary>
        private void btnVerUsuarios_Click(object sender, EventArgs e)
        {
            _navegacionForms.IrA<MenuPrincipalAdm>();
            this.Hide();
        }

        /// <summary>
        /// Evento Click del botón "Ver como Empleado".
        /// 
        /// DIP: La navegación se delega en INavegacionFormsService.IrA<T>()
        /// en lugar de hacer "new MenuPrincipalEmp()" directamente.
        /// </summary>
        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            _navegacionForms.IrA<MenuPrincipalEmp>();
            this.Hide();
        }
    }
}