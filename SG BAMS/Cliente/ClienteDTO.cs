using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS.Cliente.DTO
{
    public class ClienteDTO
    {
        /// <summary>
        /// Id del cliente. 0 cuando es un cliente nuevo (todavía no insertado).
        /// </summary>
        public int IdCliente { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string RTN { get; set; }

        /// <summary>
        /// Solo se usa al modificar. Al registrar un cliente nuevo, el estado
        /// inicial lo determina el procedimiento almacenado PA_insertar_cliente.
        /// </summary>
        public int IdEstado { get; set; }
    }
}
