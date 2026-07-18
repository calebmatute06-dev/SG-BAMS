using System;
using System.Drawing;
using System.IO;

namespace SG_BAMS
{
    /// <summary>
    /// Servicio de utilidad para la conversión de imágenes.
    /// Proporciona métodos para transformar arreglos de bytes en objetos Image.
    /// </summary>
    public static class ConversorImagenService
    {
        /// <summary>
        /// Convierte un arreglo de bytes en una imagen.
        /// </summary>
        /// <param name="imagenBytes">Arreglo de bytes que representa la imagen.</param>
        /// <returns>Imagen convertida, o null si el arreglo es nulo o vacío.</returns>
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