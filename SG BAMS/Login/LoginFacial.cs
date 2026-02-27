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
        public string UsuarioAValidar { get; set; }
        private VideoCapture camara;
        private List<Image<Gray, byte>> rostrosReferencia = new List<Image<Gray, byte>>();
        private CascadeClassifier faceDetector = new CascadeClassifier("haarcascade_frontalface_default.xml");
        public LoginFacial()
        {
            InitializeComponent();
        }

        private void LoginFacial_Load(object sender, EventArgs e)
        {
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

            foreach (var archivo in archivos)
            {
                rostrosReferencia.Add(new Image<Gray, byte>(archivo));
            }

            camara = new VideoCapture(0);
            Application.Idle += ProcesoValidacion;
        }
        private void ProcesoValidacion(object sender, EventArgs e)
        {
            if (camara == null) return;

            try
            {
                Mat m = new Mat();
                camara.Read(m);

                if (m.IsEmpty) return;

                using (var frame = m.ToImage<Bgr, byte>())
                {
                    using (var grayFrame = frame.Convert<Gray, byte>())
                    {
                        Rectangle[] rostros = faceDetector.DetectMultiScale(grayFrame, 1.2, 5);
                        foreach (Rectangle rostro in rostros)
                        {
                            frame.Draw(rostro, new Bgr(Color.Cyan), 2);
                        }
                    }

                    if (picValidar.Image != null)
                    {
                        picValidar.Image.Dispose();
                    }

                    picValidar.Image = frame.ToBitmap();

                    var rostroActual = clsSoporte.DetectarRostro(frame);

                    if (rostroActual != null && rostrosReferencia != null)
                    {
                        CompararRostros(rostroActual);
                    }
                }
            }
            catch (Exception) { }
        }

        private void CompararRostros(Image<Gray, byte> rostroActual)
        {
            try
            {
                foreach (var referencia in rostrosReferencia)
                {
                    Mat resultado = new Mat();
                    CvInvoke.MatchTemplate(rostroActual, referencia, resultado, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed);

                    double minVal = 0, maxVal = 0;
                    Point minLoc = new Point(), maxLoc = new Point();
                    CvInvoke.MinMaxLoc(resultado, ref minVal, ref maxVal, ref minLoc, ref maxLoc);

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
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnReintentar_Click(object sender, EventArgs e)
        {
            lblEstado.Text = "Reintentando escaneo...";
            lblEstado.ForeColor = Color.Black;
        }
    }
}
