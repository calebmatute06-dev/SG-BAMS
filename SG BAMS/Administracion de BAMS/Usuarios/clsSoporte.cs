using Emgu.CV;
using Emgu.CV.Structure;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS.Administracion_de_BAMS.Usuarios
{
    /// <summary>
    /// Proporciona servicios de soporte técnico para la gestión de usuarios, incluyendo procesamiento de imágenes con Emgu CV y consultas a la base de datos.
    /// </summary>
    /// <seealso cref="SG_BAMS.ClsConexion" />
    internal class clsSoporte : ClsRepositorioBaseDatos
    {
        /// <summary>
        /// Ruta absoluta del directorio donde se almacenan las imágenes de los rostros capturados.
        /// </summary>
        public static string DirectorioRostros = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
            "SGBAMS", "Rostros");

        /// <summary>
        /// Clasificador en cascada utilizado para la detección de rostros frontales mediante el algoritmo Haar.
        /// </summary>
        private static CascadeClassifier faceCascaide = new CascadeClassifier(
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "haarcascade_frontalface_default.xml"));

        /// <summary>
        /// Verifica la existencia del directorio de rostros y lo crea si no se encuentra en el sistema.
        /// </summary>
        public static void InicializarDirectorio()
        {
            if (!Directory.Exists(DirectorioRostros)) Directory.CreateDirectory(DirectorioRostros);
        }

        /// <summary>
        /// Procesa un cuadro de video para detectar un rostro, convertirlo a escala de grises y normalizar su tamaño.
        /// </summary>
        /// <param name="frame">Imagen original en formato BGR capturada por la cámara.</param>
        /// <returns>Imagen del rostro detectado en escala de grises (200x200 px) o null si no se detecta ninguno.</returns>
        public static Image<Gray, byte> DetectarRostro(Image<Bgr, byte> frame)
        {
            var grayFrame = frame.Convert<Gray, byte>();
            var faces = faceCascaide.DetectMultiScale(grayFrame, 1.1, 10, Size.Empty);

            if (faces.Length > 0)
            {
                return grayFrame.Copy(faces[0]).Resize(200, 200, Emgu.CV.CvEnum.Inter.Cubic);
            }
            return null;
        }

        /// <summary>
        /// Obtiene la lista completa de usuarios registrados mediante la ejecución de un procedimiento almacenado.
        /// </summary>
        /// <returns>Un DataTable que contiene los registros de los usuarios.</returns>
        /// <exception cref="System.Exception">Lanzada cuando ocurre un error durante la conexión o ejecución del procedimiento almacenado.</exception>
        public DataTable ObtenerUsuarios()
        {
            DataTable dt = new DataTable();
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("PA_CargarUsuarios", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error en clsSoporte al filtrar activos: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
            return dt;
        }
    }
}