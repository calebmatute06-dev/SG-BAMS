using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.IO;

namespace SG_BAMS.Administracion_de_BAMS.Usuarios
{
    /// <summary>
    /// Servicio de detección de rostros usando Emgu CV.
    /// SRP: única responsabilidad — detección y procesamiento de imágenes faciales.
    /// No tiene acceso a base de datos ni conoce la UI.
    /// </summary>
    public class DetectorRostroService : IDetectorRostro
    {
        /// <summary>
        /// Ruta absoluta del directorio donde se almacenan las imágenes de los rostros capturados.
        /// </summary>
        public static readonly string DirectorioRostros = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
            "SGBAMS", "Rostros");

        private readonly CascadeClassifier _faceCascade;

        /// <summary>
        /// Inicializa el detector cargando el clasificador Haar Cascade.
        /// </summary>
        public DetectorRostroService()
        {
            string rutaCascade = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "haarcascade_frontalface_default.xml");

            _faceCascade = new CascadeClassifier(rutaCascade);
        }

        /// <inheritdoc/>
        public void InicializarDirectorio()
        {
            if (!Directory.Exists(DirectorioRostros))
                Directory.CreateDirectory(DirectorioRostros);
        }
        public static void InicializarDirectorioEstatico()
        {
            if (!Directory.Exists(DirectorioRostros))
                Directory.CreateDirectory(DirectorioRostros);
        }

        /// <inheritdoc/>
        public Image<Gray, byte> DetectarRostro(Image<Bgr, byte> frame)
        {
            var grayFrame = frame.Convert<Gray, byte>();
            var faces = _faceCascade.DetectMultiScale(grayFrame, 1.1, 10, Size.Empty);

            if (faces.Length > 0)
            {
                return grayFrame.Copy(faces[0]).Resize(200, 200, Emgu.CV.CvEnum.Inter.Cubic);
            }
            return null;
        }


    }

}