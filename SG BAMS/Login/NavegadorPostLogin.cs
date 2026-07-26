using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SG_BAMS.Dominio;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Servicio de navegación posterior al inicio de sesión.
    /// Utiliza un diccionario de acciones para dirigir al usuario al formulario
    /// correspondiente según su rol, eliminando la necesidad de estructuras condicionales.
    /// </summary>
    public class NavegadorPostLogin
    {
        private readonly Dictionary<int, IAccionPostLogin> acciones;
        private readonly IRepositorioRostros repositorioRostros;

        /// <summary>
        /// Constructor que registra las acciones de navegación para cada rol de usuario.
        /// </summary>
        /// <param name="repositorioRostros">Repositorio para verificar registros faciales.</param>
        public NavegadorPostLogin(IRepositorioRostros repositorioRostros)
        {
            this.repositorioRostros = repositorioRostros;
            acciones = new Dictionary<int, IAccionPostLogin>
            {
                { 1, new AccionAdmin(repositorioRostros) },
                { 2, new AccionEmpleado(repositorioRostros) },
                { 3, new AccionSoporte() },
                { -1, new AccionInactivo() },
                { 0, new AccionCredencialesInvalidas() }
            };
        }

        /// <summary>
        /// Ejecuta la acción de navegación correspondiente al rol del usuario autenticado.
        /// </summary>
        /// <param name="rol">Identificador del rol del usuario.</param>
        /// <param name="formularioActual">Formulario de login que será ocultado.</param>
        /// <param name="nombreUsuario">Nombre del usuario que inició sesión.</param>
        /// <returns>True si el inicio de sesión fue exitoso (rol mayor que 0).</returns>
        public bool Navegar(int rol, Form formularioActual, string nombreUsuario)
        {
            if (acciones.TryGetValue(rol, out var accion))
            {
                accion.Ejecutar(formularioActual, nombreUsuario, rol);
                return rol > 0;
            }

            acciones[0].Ejecutar(formularioActual, nombreUsuario, rol);
            return false;
        }
    }

    /// <summary>
    /// Acción de navegación para usuarios con rol Administrador.
    /// Requiere validación facial antes de mostrar el menú principal.
    /// </summary>
    internal class AccionAdmin : IAccionPostLogin
    {
        private readonly IRepositorioRostros repositorioRostros;

        /// <summary>
        /// Constructor de la acción para administradores.
        /// </summary>
        /// <param name="repositorioRostros">Repositorio para verificar registros faciales.</param>
        public AccionAdmin(IRepositorioRostros repositorioRostros)
        {
            this.repositorioRostros = repositorioRostros;
        }

        /// <inheritdoc/>
        public void Ejecutar(Form formularioActual, string nombreUsuario, int rol)
        {
            if (!repositorioRostros.TieneRegistroFacial(nombreUsuario))
            {
                MessageBox.Show($"El usuario '{nombreUsuario}' no tiene un registro facial registrado.",
                    "Sin registro facial", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Login correcto. ¡Bienvenido Administrador!", "Éxito",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            var frmFacial = new LoginFacial { UsuarioAValidar = nombreUsuario, RolAsignado = rol };
            frmFacial.Show();
            formularioActual.Hide();
        }
    }

    /// <summary>
    /// Acción de navegación para usuarios con rol Empleado.
    /// Requiere validación facial antes de mostrar el menú principal.
    /// </summary>
    internal class AccionEmpleado : IAccionPostLogin
    {
        private readonly IRepositorioRostros repositorioRostros;

        /// <summary>
        /// Constructor de la acción para empleados.
        /// </summary>
        /// <param name="repositorioRostros">Repositorio para verificar registros faciales.</param>
        public AccionEmpleado(IRepositorioRostros repositorioRostros)
        {
            this.repositorioRostros = repositorioRostros;
        }

        /// <inheritdoc/>
        public void Ejecutar(Form formularioActual, string nombreUsuario, int rol)
        {
            if (!repositorioRostros.TieneRegistroFacial(nombreUsuario))
            {
                MessageBox.Show($"El usuario '{nombreUsuario}' no tiene un registro facial registrado.",
                    "Sin registro facial", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Login correcto. ¡Bienvenido Empleado!", "Éxito",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            var frmFacial = new LoginFacial { UsuarioAValidar = nombreUsuario, RolAsignado = rol };
            frmFacial.Show();
            formularioActual.Hide();
        }
    }

    /// <summary>
    /// Acción de navegación para usuarios con rol Soporte.
    /// No requiere validación facial. Muestra directamente el formulario de soporte.
    /// </summary>
    internal class AccionSoporte : IAccionPostLogin
    {
        /// <inheritdoc/>
        public void Ejecutar(Form formularioActual, string nombreUsuario, int rol)
        {
            MessageBox.Show("Login correcto. ¡Bienvenido Soporte!", "Éxito",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            IServicioSeguridad servicioSeguridad = new ServicioSeguridad();
            IServicioCorreo servicioCorreo = new ServicioCorreo(new ConfiguracionCorreo());
            INavegacionFormsService navegacionForms = new NavegacionFormsService(servicioCorreo, servicioSeguridad);

            Soporte soporte = new Soporte(navegacionForms);
            soporte.Show();
            formularioActual.Hide();
        }
    }

    /// <summary>
    /// Acción para usuarios que están inactivos en el sistema.
    /// Muestra un mensaje de advertencia y no permite el acceso.
    /// </summary>
    internal class AccionInactivo : IAccionPostLogin
    {
        /// <inheritdoc/>
        public void Ejecutar(Form formularioActual, string nombreUsuario, int rol)
        {
            MessageBox.Show("El usuario está inactivo. No puede ingresar.",
                "Cuenta Inactiva", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    /// <summary>
    /// Acción para credenciales inválidas.
    /// No realiza ninguna acción, el manejo de intentos fallidos
    /// es gestionado por el control de bloqueo en el formulario de login.
    /// </summary>
    internal class AccionCredencialesInvalidas : IAccionPostLogin
    {
        /// <inheritdoc/>
        public void Ejecutar(Form formularioActual, string nombreUsuario, int rol)
        {
           
        }
    }
}