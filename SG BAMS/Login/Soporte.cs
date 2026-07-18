using System;
using System.Windows.Forms;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Formulario de soporte que permite al usuario elegir entre la vista
    /// de Administrador o la vista de Empleado después de iniciar sesión.
    /// </summary>
    public partial class Soporte : Form
    {
        private readonly INavegacionFormsService navegacionForms;

        /// <summary>
        /// Constructor sin parámetros para compatibilidad con código existente.
        /// </summary>
        public Soporte() : this(new NavegacionFormsService(
            new ServicioCorreo(new ConfiguracionCorreo()),
            new ServicioSeguridad()))
        {
        }

        /// <summary>
        /// Constructor principal que recibe el servicio de navegación entre formularios.
        /// </summary>
        /// <param name="navegacionForms">Servicio de navegación entre formularios.</param>
        /// <exception cref="ArgumentNullException">Si navegacionForms es nulo.</exception>
        public Soporte(INavegacionFormsService navegacionForms)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            this.navegacionForms = navegacionForms ?? throw new ArgumentNullException(nameof(navegacionForms));
        }

        /// <summary>
        /// Evento Click del botón "Ver como Administrador".
        /// Navega al menú principal con vista de administrador.
        /// </summary>
        private void btnVerUsuarios_Click(object sender, EventArgs e)
        {
            navegacionForms.IrA<MenuPrincipalAdm>();
            this.Hide();
        }

        /// <summary>
        /// Evento Click del botón "Ver como Empleado".
        /// Navega al menú principal con vista de empleado.
        /// </summary>
        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            navegacionForms.IrA<MenuPrincipalEmp>();
            this.Hide();
        }
    }
}