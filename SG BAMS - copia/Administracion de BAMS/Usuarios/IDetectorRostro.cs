using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace SG_BAMS.Administracion_de_BAMS.Usuarios
{
    /// <summary>
    /// Contrato para el servicio de detección de rostros.
    /// SRP: separa la responsabilidad de visión artificial de la de acceso a datos.
    /// DIP: los formularios dependen de esta abstracción, no de Emgu CV directamente.
    /// </summary>
    public interface IDetectorRostro
    {
        /// <summary>
        /// Inicializa el directorio donde se almacenarán las imágenes capturadas.
        /// </summary>
        void InicializarDirectorio();

        /// <summary>
        /// Detecta el primer rostro en el frame y lo retorna recortado y normalizado.
        /// Retorna null si no se detecta ningún rostro.
        /// </summary>
        Image<Gray, byte> DetectarRostro(Image<Bgr, byte> frame);
    }
}