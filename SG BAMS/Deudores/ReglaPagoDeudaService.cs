using System;

namespace SG_BAMS
{
    /// <summary>
    /// Regla de dominio: determina si corresponde procesar el pago de una deuda según su estado.
    /// Antes esta decisión vivía directamente en el formulario (DeudoresAdmin/Deudores_Emp);
    /// ahora se delega a un servicio de dominio independiente de la interfaz
    /// (ver auditoría SOLID, hallazgo DAD03).
    /// </summary>
    public static class ReglaPagoDeudaService
    {
        /// <summary>
        /// Indica si una deuda con el estado dado admite el registro de un nuevo pago.
        /// </summary>
        public static bool PuedeRegistrarPago(string estadoDeuda)
        {
            return !string.IsNullOrWhiteSpace(estadoDeuda) &&
                   estadoDeuda.Trim().Equals("Activo", StringComparison.OrdinalIgnoreCase);
        }
    }
}
