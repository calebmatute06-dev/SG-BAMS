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
                MessageBox.Show("No tienes un registro facial.", "Error");
                Finalizar(DialogResult.Abort);
                return;
            }

            rostroReferencia = new Image<Gray, byte>(rutaFoto);
            camara = new VideoCapture(0);
            Application.Idle += ProcesoValidacion;
        }
        private void ProcesoValidacion(object sender, EventArgs e)
        {
            // Protección contra nulos
            if (camara == null) return;

            try
            {
                Mat m = new Mat();

                // CORRECCIÓN AQUÍ: Usar Read() en lugar de Retrieve()
                // Read() captura el frame de la cámara de forma segura
                camara.Read(m);

                // Si la cámara aún está calentando o falló el frame, salimos y esperamos al siguiente ciclo
                if (m.IsEmpty) return;

                using (var frame = m.ToImage<Bgr, byte>())
                {
                    // Opcional pero recomendado: Liberar la imagen anterior del PictureBox para no llenar la RAM
                    if (picValidar.Image != null)
                    {
                        picValidar.Image.Dispose();
                    }

                    // Mostramos la imagen en pantalla
                    picValidar.Image = frame.ToBitmap();

                    // Detectamos el rostro
                    var rostroActual = clsSoporte.DetectarRostro(frame);

                    if (rostroActual != null && rostroReferencia != null)
                    {
                        // Llamamos a la comparación
                        CompararRostros(rostroActual);
                    }
                }
            }
            catch (Exception) { /* Ignorar errores temporales de lectura */ }
        }

        // ESTE ES EL MÉTODO QUE FALTABA (Solución al error CS0103)
        private void CompararRostros(Image<Gray, byte> rostroActual)
        {
            try
            {
                // Creamos una matriz para almacenar el resultado de la comparación
                Mat resultado = new Mat();

                // MatchTemplate compara la estructura y los patrones de los píxeles, no solo los colores.
                // CcoeffNormed devuelve un valor entre -1.0 y 1.0 (1.0 es una coincidencia perfecta).
                CvInvoke.MatchTemplate(rostroActual, rostroReferencia, resultado, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed);

                double minVal = 0, maxVal = 0;
                Point minLoc = new Point(), maxLoc = new Point();

                // Extraemos el valor máximo de coincidencia
                CvInvoke.MinMaxLoc(resultado, ref minVal, ref maxVal, ref minLoc, ref maxLoc);

                // maxVal es nuestro porcentaje de similitud real.
                // 0.70 (70%) es un buen punto de partida para rostros. Puedes subirlo a 0.75 o 0.80 si es muy permisivo.
                if (maxVal > 0.70)
                {
                    Finalizar(DialogResult.OK);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error comp: " + ex.Message);
            }
        }

        private void Finalizar(DialogResult resultado)
        {
            Application.Idle -= ProcesoValidacion;
            if (camara != null)
            {
                camara.Dispose();
                camara = null;
            }
            this.DialogResult = resultado;

            // Solo cerramos si el formulario no se está cerrando ya
            if (this.Visible) this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.Idle -= ProcesoValidacion;
            if (camara != null)
            {
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
