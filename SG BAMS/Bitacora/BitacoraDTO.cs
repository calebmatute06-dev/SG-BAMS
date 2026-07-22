using System;

namespace SG_BAMS.Bitacora
{
    /// <summary>
    /// Objeto de transferencia de datos que representa un registro de la bitácora.
    /// </summary>
    public class BitacoraDTO
    {
        public string Nombre { get; set; }
        public string Accion { get; set; }
        public string Modulo { get; set; }
        public DateTime Fecha { get; set; }
    }
}