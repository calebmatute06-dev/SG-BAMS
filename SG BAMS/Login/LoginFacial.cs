using Emgu.CV;
using Emgu.CV.Structure;
using SG_BAMS.Administracion_de_BAMS.Usuarios;
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
                    // Mostramos el video en un PictureBox llamado 'picValidar'
                    picValidar.Image = frame.ToBitmap();

                    // 3. Detectamos el rostro en vivo
                    var rostroActual = clsSoporte.DetectarRostro(frame);

                    if (rostroActual != null)
                    {
                        // 4. Comparamos Histogramas (Similitud)
                        // CvInvoke.CompareHist requiere que ambos tengan el mismo tamaño
                        double similitud = CvInvoke.CompareHist(rostroActual, rostroReferencia, Emgu.CV.CvEnum.HistogramCompMethod.Correl);

                        // Si la coincidencia es mayor al 80%
                        if (similitud > 0.8)
                        {
                            Finalizar(DialogResult.OK);
                        }
                    }
                }
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
            this.Close();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Finalizar(this.DialogResult);
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
