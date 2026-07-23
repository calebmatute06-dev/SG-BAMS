using System.Data;

namespace SG_BAMS.ComprasContratos
{
    /// <summary>
    /// Resultado uniforme de una validación, para no repetir en cada
    /// formulario el mismo patrón de "condición + MessageBox".
    /// </summary>
    public class ResultadoValidacion
    {
        public bool EsValido { get; private set; }
        public string Mensaje { get; private set; }

        public static ResultadoValidacion Ok() => new ResultadoValidacion { EsValido = true };

        public static ResultadoValidacion Error(string mensaje) =>
            new ResultadoValidacion { EsValido = false, Mensaje = mensaje };
    }

    /// <summary>
    /// Reglas de validación y de negocio del módulo de Compras, antes
    /// mezcladas dentro de los manejadores de eventos de
    /// Ingresar_datos__Compra_ y Modificar_datos__Compra_. Al ser métodos
    /// estáticos y puros (sin dependencias de UI ni de base de datos),
    /// son fáciles de probar de forma aislada.
    /// </summary>
    public static class ComprasDominio
    {
        public static ResultadoValidacion ValidarNuevaCompra(int filasConDatos, bool proveedorSeleccionado, bool formaPagoSeleccionada)
        {
            if (filasConDatos == 0)
                return ResultadoValidacion.Error("Debe agregar al menos un producto a la lista.");

            if (!proveedorSeleccionado || !formaPagoSeleccionada)
                return ResultadoValidacion.Error("Seleccione el Proveedor y la Forma de Pago.");

            return ResultadoValidacion.Ok();
        }

        public static ResultadoValidacion ValidarProveedorSeleccionado(bool proveedorSeleccionado)
        {
            if (!proveedorSeleccionado)
                return ResultadoValidacion.Error("Por favor, seleccione un proveedor válido de la lista");

            return ResultadoValidacion.Ok();
        }

        public static ResultadoValidacion ValidarCantidadOPrecio(string nombreCampo, decimal valor)
        {
            if (valor <= 0)
                return ResultadoValidacion.Error($"{nombreCampo} no puede ser cero o menor.");

            return ResultadoValidacion.Ok();
        }

        /// <summary>
        /// Determina si un producto pertenece a la sesión de edición actual
        /// (recién agregado en pantalla) o si ya existía antes de abrir el
        /// formulario de modificación. Antes esta regla vivía directamente
        /// dentro de btnEliminarProducto_Click.
        /// </summary>
        public static bool EsProductoDeSesionActual(DataTable respaldo, int idProducto)
        {
            if (respaldo == null) return true;

            foreach (DataRow r in respaldo.Rows)
            {
                if ((int)r["ID"] == idProducto) return false;
            }
            return true;
        }
    }
}
