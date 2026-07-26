using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SG_BAMS.AccesoDatos;

namespace SG_BAMS.LogicaNegocio
{
    public class BusquedaProductoService
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
