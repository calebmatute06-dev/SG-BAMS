using SG_BAMS.Facturas.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS.Facturas
{
    internal class ExtractorDatosFactura
    {
        public static FacturaDTO DesdeFilaGrid(DataGridViewRow fila)
        {
            int bateriaVieja = 0;
            var valorBateria = fila.Cells["Batería Vieja"].Value?.ToString();
            if (!string.IsNullOrEmpty(valorBateria) && valorBateria != "No dejó")
            {
                string soloNumero = System.Text.RegularExpressions.Regex.Match(valorBateria, @"\d+").Value;
                if (!string.IsNullOrEmpty(soloNumero))
                    bateriaVieja = int.Parse(soloNumero);
            }

            string valorCelda = fila.Cells["Rebaja"].Value?.ToString() ?? "0";
            valorCelda = valorCelda.Replace("L.", "").Trim();
            double rebaja = Convert.ToDouble(valorCelda);

            return new FacturaDTO
            {
                IdFactura = Convert.ToInt32(fila.Cells["Factura"].Value),
                NombreCliente = fila.Cells["Cliente"].Value.ToString(),
                Fecha = Convert.ToDateTime(fila.Cells["Fecha"].Value),
                IdFormaPago = Convert.ToInt32(fila.Cells["ID Método de Pago"].Value),
                Vendedor = fila.Cells["Vendedor"].Value.ToString(),
                CantidadBateriaVieja = bateriaVieja,
                RebajaBateria = rebaja
            };
        }
    }
}
