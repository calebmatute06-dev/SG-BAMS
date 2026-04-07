using Emgu.CV;
using Emgu.CV.Structure;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS.Administracion_de_BAMS.Usuarios
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="SG_BAMS.ClsConexion" />
    internal class clsSoporte: ClsConexion
    {
        /// <summary>
        /// The directorio rostros
        /// </summary>
        public static string DirectorioRostros = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Rostros");

        /// <summary>
        /// The face cascaide
        /// </summary>
        private static CascadeClassifier faceCascaide = new CascadeClassifier(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "haarcascade_frontalface_default.xml"));

        /// <summary>
        /// Inicializars the directorio.
        /// </summary>
        public static void InicializarDirectorio()
        {
            if (!Directory.Exists(DirectorioRostros)) Directory.CreateDirectory(DirectorioRostros);
        }

        /// <summary>
        /// Detectars the rostro.
        /// </summary>
        /// <param name="frame">The frame.</param>
        /// <returns></returns>
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
        /// Obteners the usuarios.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error en clsSoporte al filtrar activos: " + ex.Message</exception>
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
