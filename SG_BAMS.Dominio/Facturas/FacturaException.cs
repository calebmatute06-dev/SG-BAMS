using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS.Dominio
{
    public class FacturaException : Exception
    {
        public FacturaException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
