using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS.Facturas
{
    internal class BusquedaProductoService
    {
        private readonly ClsFactura FAC;

        public BusquedaProductoService(ClsFactura clsFactura)
        {
            FAC = clsFactura;
        }

        public async Task<DataRow> BuscarPorCodigoBarra(string codigo)
        {
            return await FAC.ObtenerProductoPorCodigoBarra(codigo);
        }
    }
}
