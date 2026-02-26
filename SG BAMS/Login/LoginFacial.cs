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
        private List<Image<Gray, byte>> rostrosReferencia = new List<Image<Gray, byte>>();
        public LoginFacial()
        {
            InitializeComponent();
        }

        private void LoginFacial_Load(object sender, EventArgs e)
        {
            // Buscamos todas las fotos del usuario (la original .jpg y las nuevas _ticks.jpg)
            var archivos = Directory.GetFiles(clsSoporte.DirectorioRostros, "*.jpg")
                .Where(f => Path.GetFileNameWithoutExtension(f) == UsuarioAValidar ||
                            Path.GetFileNameWithoutExtension(f).StartsWith(UsuarioAValidar + "_"))
                .ToList();

            if (archivos.Count == 0)
            {
                MessageBox.Show("No tienes un registro facial.", "Error");
                Finalizar(DialogResult.Abort);
                return;
            }

            // Cargamos todas las fotos a la lista en memoria
            foreach (var archivo in archivos)
            {
                rostrosReferencia.Add(new Image<Gray, byte>(archivo));
            }

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

                    if (rostroActual != null && rostrosReferencia != null)
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
                // Comparamos el rostro de la cámara con CADA UNA de las fotos guardadas
                foreach (var referencia in rostrosReferencia)
                {
                    Mat resultado = new Mat();
                    CvInvoke.MatchTemplate(rostroActual, referencia, resultado, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed);

                    double minVal = 0, maxVal = 0;
                    Point minLoc = new Point(), maxLoc = new Point();
                    CvInvoke.MinMaxLoc(resultado, ref minVal, ref maxVal, ref minLoc, ref maxLoc);

                    // Si AL MENOS UNA foto coincide con más del 70%, damos acceso y salimos del ciclo
                    if (maxVal > 0.70)
                    {
                        Finalizar(DialogResult.OK);
                        return;
                    }
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
            foreach (var img in rostrosReferencia) { img.Dispose(); }
            rostrosReferencia.Clear();
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
