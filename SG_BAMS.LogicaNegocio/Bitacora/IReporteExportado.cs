using System;
using System.Collections.Generic;


namespace SG_BAMS.Bitacora
{
    /// <summary>
    /// Contrato para exportar registros de bitácora a un archivo,
    /// independientemente del formato final (PDF, Excel, etc.).
    /// </summary>
    public interface IReporteExportador
    {
        void Exportar(List<BitacoraDTO> datos, string rutaArchivo);
    }
}