using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS.Dominio
{ 
    public class ResultadoImpuestosFactura
    {
        public double ImporteExonerado { get; set; }
        public double ImporteExento { get; set; }
        public double ImporteGravado { get; set; }
        public double Isv15 { get; set; }
        public double TotalFinal { get; set; }
    }
}
