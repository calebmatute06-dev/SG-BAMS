using System;
using System.Data;

namespace SG_BAMS
{
    /// <summary>
    /// Calcula el resumen de una deuda (productos, subtotal, descuento y saldos) a partir
    /// de los datos crudos que entrega IDeudasRepository. Única responsabilidad: los cálculos
    /// de negocio; deja al formulario Pago_Deuda únicamente el formateo final para mostrarlo
    /// (ver auditoría SOLID, hallazgo PGD03).
    /// </summary>
    public class ResumenPagoService
    {
        private readonly IDeudasRepository _repositorio;

        public ResumenPagoService(IDeudasRepository repositorio)
        {
            _repositorio = repositorio;
        }

        /// <summary>
        /// Obtiene y calcula el resumen completo de una deuda.
        /// </summary>
        public ResumenPagoDeudaDTO ObtenerResumen(int idDeuda)
        {
            var resumen = new ResumenPagoDeudaDTO();

            DataTable dtProductos = _repositorio.ObtenerProductosPorDeuda(idDeuda);
            if (dtProductos != null)
            {
                foreach (DataRow row in dtProductos.Rows)
                {
                    var linea = new LineaProductoDeudaDTO
                    {
                        Producto = row["Producto"].ToString(),
                        Cantidad = Convert.ToInt32(row["Cantidad"]),
                        PrecioUnitario = Convert.ToDecimal(row["PrecioUnitario"]),
                        Total = Convert.ToDecimal(row["total"])
                    };
                    resumen.Productos.Add(linea);
                    resumen.Subtotal += linea.Total;
                }
            }

            DataRow detalle = _repositorio.ObtenerSaldoDetalle(idDeuda);
            resumen.Descuento = detalle != null ? Convert.ToDecimal(detalle["Descuento"]) : 0;
            resumen.SaldoPagado = detalle != null ? Convert.ToDecimal(detalle["SaldoPagado"]) : 0;
            resumen.SaldoPendiente = detalle != null ? Convert.ToDecimal(detalle["SaldoPendiente"]) : 0;

            return resumen;
        }
    }
}
