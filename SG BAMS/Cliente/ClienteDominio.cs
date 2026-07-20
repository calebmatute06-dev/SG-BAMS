using Krypton.Toolkit;
using SG_BAMS.Cliente;
using System.Threading.Tasks;

namespace SG_BAMS.Cliente
{
    /// <summary>
    /// Resultado de una validación: si es válida y, si no, el mensaje a mostrar.
    /// </summary>
    public struct ResultadoValidacion
    {
        public bool EsValido;
        public string Mensaje;

        public static ResultadoValidacion Ok() => new ResultadoValidacion { EsValido = true, Mensaje = null };
        public static ResultadoValidacion Error(string mensaje) => new ResultadoValidacion { EsValido = false, Mensaje = mensaje };
    }

    /// <summary>
    /// Reglas de negocio del módulo Cliente: formato de los campos y
    /// unicidad del RTN. Separa esta lógica de los formularios (SRP) para
    /// que ClienteAgregar y ClienteModificar solo orquesten la llamada,
    /// igual que Comprasdominio.cs separa las reglas de negocio de Compras.
    /// </summary>
    public class ClienteDominio
    {
        private readonly IClienteRepository _repositorio;

        public ClienteDominio(IClienteRepository repositorio)
        {
            _repositorio = repositorio;
        }

        /// <summary>
        /// Normaliza el RTN ingresado: si viene vacío, lo convierte a "Sin RTN".
        /// </summary>
        public string NormalizarRTN(string rtnIngresado)
        {
            if (string.IsNullOrWhiteSpace(rtnIngresado) || rtnIngresado.Trim().ToUpper() == "SIN RTN")
                return "Sin RTN";
            return rtnIngresado.Trim();
        }

        /// <summary>
        /// Valida el formato de nombre, apellido, teléfono y RTN.
        /// No consulta la base de datos.
        /// </summary>
        public ResultadoValidacion ValidarFormato(string nombre, string apellido, string telefono, string rtn)
        {
            using (var tempNombre = new KryptonTextBox { Text = nombre })
            using (var tempApellido = new KryptonTextBox { Text = apellido })
            using (var tempTelefono = new KryptonTextBox { Text = telefono })
            using (var tempRTN = new KryptonTextBox { Text = rtn })
            {
                if (!ClsValidaciones.EsNombrePersonalValido(tempNombre, "El Nombre"))
                    return ResultadoValidacion.Error("El nombre no tiene un formato válido.");

                if (!ClsValidaciones.EsNombrePersonalValido(tempApellido, "El Apellido"))
                    return ResultadoValidacion.Error("El apellido no tiene un formato válido.");

                if (!ClsValidaciones.EsTelefonoHondurasValido(tempTelefono))
                    return ResultadoValidacion.Error("El teléfono no tiene un formato válido.");

                if (!string.IsNullOrWhiteSpace(rtn) && rtn.ToUpper() != "SIN RTN" && !ClsValidaciones.EsRTNValido(tempRTN))
                    return ResultadoValidacion.Error("El RTN no tiene un formato válido.");
            }
            return ResultadoValidacion.Ok();
        }

        /// <summary>
        /// Valida que el RTN normalizado no pertenezca ya a otro cliente.
        /// "Sin RTN" nunca se considera duplicado.
        /// </summary>
        public async Task<ResultadoValidacion> ValidarRTNDuplicado(string rtnNormalizado, int idClienteActual = 0)
        {
            if (rtnNormalizado == "Sin RTN")
                return ResultadoValidacion.Ok();

            bool yaExiste = await _repositorio.RTNYaExiste(rtnNormalizado, idClienteActual);
            return yaExiste
                ? ResultadoValidacion.Error("Este RTN ya está registrado para otro cliente.")
                : ResultadoValidacion.Ok();
        }
    }
}