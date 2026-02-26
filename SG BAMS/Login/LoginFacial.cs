using Emgu.CV;
using Emgu.CV.Structure;
using SG_BAMS.Administracion_de_BAMS.Usuarios;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS.Login
{
    public partial class LoginFacial : Form
    {
        // Propiedad para recibir el usuario desde el Login
        public string UsuarioAValidar { get; set; }

        private VideoCapture camara;
        private Image<Gray, byte> rostroReferencia;
        public LoginFacial()
        {
            InitializeComponent();
        }

        private void LoginFacial_Load(object sender, EventArgs e)
        {
            string rutaFoto = Path.Combine(clsSoporte.DirectorioRostros, UsuarioAValidar + ".jpg");

            if (!File.Exists(rutaFoto))
            {
                MessageBox.Show("No tienes un registro facial. Contacta al administrador.", "Error de Biometría");
                this.DialogResult = DialogResult.Abort;
                this.Close();
                return;
            }

            rostroReferencia = new Image<Gray, byte>(rutaFoto);

            // 2. Iniciamos la cámara
            camara = new VideoCapture(0);
            Application.Idle += ProcesoValidacion;
        }
        private void ProcesoValidacion(object sender, EventArgs e)
        {
            if (camara == null) return;

            using (var frame = camara.QueryFrame().ToImage<Bgr, byte>())
            {
                if (frame != null)
                {
                    picValidar.Image = frame.ToBitmap();
                    var rostroActual = clsSoporte.DetectarRostro(frame);

                    if (rostroActual != null)
                    {
                        try
                        {
                            // 1. Creamos los objetos para los histogramas
                            Mat histActual = new Mat();
                            Mat histReferencia = new Mat();

                            // Definimos rangos y dimensiones (estándar para escala de grises)
                            float[] range = { 0, 256 };
                            int[] histSize = { 256 };
                            int[] channels = { 0 };

                            // 2. Calculamos el Histograma para el rostro de la cámara
                            using (VectorOfMat vMat = new VectorOfMat(rostroActual.Mat))
                            {
                                CvInvoke.CalcHist(vMat, channels, null, histActual, histSize, range, false);
                            }

                            // 3. Calculamos el Histograma para la foto guardada
                            using (VectorOfMat vRef = new VectorOfMat(rostroReferencia.Mat))
                            {
                                CvInvoke.CalcHist(vRef, channels, null, histReferencia, histSize, range, false);
                            }

                            // 4. NORMALIZACIÓN (Vital para que el tipo sea CV_32F y evitar tu error)
                            CvInvoke.Normalize(histActual, histActual, 0, 1, Emgu.CV.CvEnum.NormType.MinMax, Emgu.CV.CvEnum.DepthType.Cv32F);
                            CvInvoke.Normalize(histReferencia, histReferencia, 0, 1, Emgu.CV.CvEnum.NormType.MinMax, Emgu.CV.CvEnum.DepthType.Cv32F);

                            // 5. Ahora sí, comparamos
                            double similitud = CvInvoke.CompareHist(histActual, histReferencia, Emgu.CV.CvEnum.HistogramCompMethod.Correl);

                            // Debug opcional: ver el valor en consola para ajustar el 0.8
                            Console.WriteLine("Similitud detectada: " + similitud);

                            if (similitud > 0.8)
                            {
                                Finalizar(DialogResult.OK);
                            }
                        }
                        catch (Exception ex)
                        {
                            // Esto evitará que el programa se cierre si hay un error de procesamiento
                            Console.WriteLine("Error en comparación: " + ex.Message);
                        }
                    }
                }
            }
        }
        private void Finalizar(DialogResult resultado)
        {
            // Detenemos el evento Idle para que no siga procesando frames
            Application.Idle -= ProcesoValidacion;

            if (camara != null)
            {
                camara.Dispose();
                camara = null; // IMPORTANTE: Esto rompe el bucle infinito
            }

            this.DialogResult = resultado;

            // Solo llamamos a Close si el formulario aún no se está cerrando
            // Esto evita que vuelva a disparar OnFormClosing innecesariamente
            if (this.Visible)
            {
                this.Close();
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // Si la cámara aún existe, significa que el usuario cerró la ventana manualmente (con la X)
            // En ese caso, limpiamos recursos pero no volvemos a llamar a Close()
            if (camara != null)
            {
                Application.Idle -= ProcesoValidacion;
                camara.Dispose();
                camara = null;
            }

            base.OnFormClosing(e);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Al cerrar con Cancel, el Login sabrá que NO debe abrir el menú
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnReintentar_Click(object sender, EventArgs e)
        {
            // Simplemente limpiamos el estado y reiniciamos el evento si es necesario
            lblEstado.Text = "Reintentando escaneo...";
            lblEstado.ForeColor = Color.Black;
        }
        //--------------------------------------------------------------------------------------------------------------------------------Fin
    }
}
