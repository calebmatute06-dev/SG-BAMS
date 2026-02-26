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
    internal class clsSoporte: ClsConexion
    {
        // Ruta donde se guardarán las fotos de los rostros
        public static string DirectorioRostros = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Rostros");

        // Clasificador para detectar rostros (debes tener el archivo haarcascade_frontalface_default.xml)
        private static CascadeClassifier faceCascaide = new CascadeClassifier(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "haarcascade_frontalface_default.xml"));

        public static void InicializarDirectorio()
        {
            if (!Directory.Exists(DirectorioRostros)) Directory.CreateDirectory(DirectorioRostros);
        }

        public static Image<Gray, byte> DetectarRostro(Image<Bgr, byte> frame)
        {
            var grayFrame = frame.Convert<Gray, byte>();
            var faces = faceCascaide.DetectMultiScale(grayFrame, 1.1, 10, Size.Empty);

            if (faces.Length > 0)
            {
                // Retornamos solo el primer rostro detectado, redimensionado para consistencia
                return grayFrame.Copy(faces[0]).Resize(200, 200, Emgu.CV.CvEnum.Inter.Cubic);
            }
            return null;
        }
        public DataTable ObtenerUsuarios()
        {
            DataTable dt = new DataTable();
            try
            {
                AbrirConexion();
                // Al ejecutar este SP, ya vendrán filtrados desde SQL
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
