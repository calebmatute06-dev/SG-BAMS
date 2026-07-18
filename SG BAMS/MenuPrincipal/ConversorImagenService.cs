using System;
using System.Drawing;
using System.IO;

namespace SG_BAMS
{
    /// <summary>
    /// Servicio de utilidad para conversión de imágenes.
    /// Centraliza la lógica de conversión de bytes a Image que estaba duplicada.
    /// </summary>
    public static class ConversorImagenService
    {
        /// <summary>
        /// Convierte un arreglo de bytes en una imagen.
        /// </summary>
        /// <param name="imagenBytes">Bytes de la imagen.</param>
        /// <returns>Imagen convertida, o null si los bytes son nulos o vacíos.</returns>
        public static Image BytesAImagen(byte[] imagenBytes)
        {
            if (imagenBytes == null || imagenBytes.Length == 0)
                return null;

            using (MemoryStream ms = new MemoryStream(imagenBytes))
            {
                return Image.FromStream(ms);
            }
        }
    }
}