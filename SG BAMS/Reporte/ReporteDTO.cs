using System;

namespace SG_BAMS.Reportes
{
    // Este es el "molde" para los datos que irán al PDF
    public class ReporteDTO
    {
        public string ID { get; set; }
        public string Usuario { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
    }
}