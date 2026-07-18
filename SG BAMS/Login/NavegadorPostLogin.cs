using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Implementación de navegación post-login usando un diccionario polimórfico.
    /// Reemplaza el switch de roles con un mapeo extensible.
    /// </summary>
    public class NavegadorPostLogin
    {
        private readonly Dictionary<int, IAccionPostLogin> _acciones;
        private readonly IRepositorioRostros _repositorioRostros;

        /// <summary>
        /// Constructor. Registra las acciones para cada rol.
        /// </summary>
        /// <param name="repositorioRostros">Repositorio para verificar registros faciales.</param>
        public NavegadorPostLogin(IRepositorioRostros repositorioRostros)
        {
            _repositorioRostros = repositorioRostros;
            _acciones = new Dictionary<int, IAccionPostLogin>
            {
                { 1, new AccionAdmin(repositorioRostros) },
                { 2, new AccionEmpleado(repositorioRostros) },
                { 3, new AccionSoporte() },
                { -1, new AccionInactivo() },
                { 0, new AccionCredencialesInvalidas() }
            };
        }

        /// <summary>
        /// Navega según el rol del usuario autenticado.
        /// </summary>
        /// <param name="rol">Rol del usuario.</param>
        /// <param name="formularioActual">Formulario de login.</param>
        /// <param name="nombreUsuario">Nombre del usuario.</param>
        /// <returns>True si la navegación fue exitosa (login válido).</returns>
        public bool Navegar(int rol, Form formularioActual, string nombreUsuario)
        {
            if (_acciones.TryGetValue(rol, out var accion))
            {
                accion.Ejecutar(formularioActual, nombreUsuario, rol);
                return rol > 0;
            }


            _acciones[0].Ejecutar(formularioActual, nombreUsuario, rol);
            return false;
        }
    }

    /// <summary>Acción para rol Administrador (1).</summary>
    internal class AccionAdmin : IAccionPostLogin
    {
        private readonly IRepositorioRostros _repositorioRostros;
        public AccionAdmin(IRepositorioRostros repositorioRostros) => _repositorioRostros = repositorioRostros;

        public void Ejecutar(Form formularioActual, string nombreUsuario, int rol)
        {
            if (!_repositorioRostros.TieneRegistroFacial(nombreUsuario))
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

    /// <summary>Acción para rol Empleado (2).</summary>
    internal class AccionEmpleado : IAccionPostLogin
    {
        private readonly IRepositorioRostros _repositorioRostros;
        public AccionEmpleado(IRepositorioRostros repositorioRostros) => _repositorioRostros = repositorioRostros;

        public void Ejecutar(Form formularioActual, string nombreUsuario, int rol)
        {
            if (!_repositorioRostros.TieneRegistroFacial(nombreUsuario))
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

    /// <summary>Acción para rol Soporte (3).</summary>
    internal class AccionSoporte : IAccionPostLogin
    {
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

    /// <summary>Acción para usuario inactivo (-1).</summary>
    internal class AccionInactivo : IAccionPostLogin
    {
        public void Ejecutar(Form formularioActual, string nombreUsuario, int rol)
        {
            MessageBox.Show("El usuario está inactivo. No puede ingresar.",
                "Cuenta Inactiva", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }

    /// <summary>Acción para credenciales inválidas (0).</summary>
    internal class AccionCredencialesInvalidas : IAccionPostLogin
    {
        public void Ejecutar(Form formularioActual, string nombreUsuario, int rol)
        {

        }
    }
}