using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Capa_de_acceso_de_datos; // Asegúrate de que esta referencia sea correcta
using OpenCvSharp;
using OpenCvSharp.Extensions;
using OpenCvSharp.Face;

namespace SG_BAMS
{
    public partial class frmImagenEmpleado : Form
    {
        #region Variables de OpenCV y Reconocimiento
        private VideoCapture cam;
        private Mat frame;
        private CascadeClassifier face_detector;
        private EigenFaceRecognizer eigen_face_recognizer;

        private bool running = false;
        private RecordingType recording_type;
        private DateTime lastSave = DateTime.MinValue;
        private List<Mat> trainedImages = new List<Mat>();

        // Configuración de modelo
        private int model_width = 100;
        private int model_height = 100;
        private int eigen_components = 80;
        private int threshold = 3000;

        // Rutas
        private string path_faces = Path.Combine(Application.StartupPath, "Faces");
        private string path_xml = Path.Combine(Application.StartupPath, "haarcascade_frontalface_default.xml");

        enum RecordingType { training = 0, recognition = 1 }
        #endregion

        public frmImagenEmpleado()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            // Inicializar objetos de visión
            frame = new Mat();
            if (File.Exists(path_xml))
                face_detector = new CascadeClassifier(path_xml);
            else
                MessageBox.Show("No se encontró el archivo XML de detección facial.");

            eigen_face_recognizer = EigenFaceRecognizer.Create(eigen_components, threshold);

            // Suscribir eventos de botones
            btnEncender.Click += (s, e) => { recording_type = RecordingType.recognition; TurnOnCamera(); };
            btnDetener.Click += (s, e) => TurnOffCamera();
            btnEntrenar.Click += btnEntrenar_Click;
            btnSalir.Click += (s, e) => this.Close();

            CargarUsuariosCombo();
        }

        private void CargarUsuariosCombo()
        {
            try
            {
                ClsAcciones objacciones = new ClsAcciones();
                var usuarios = objacciones.ObtenerUsuarios();
                cmbUsuarios.DisplayMember = "NombreCompleto";
                cmbUsuarios.ValueMember = "Usuario_id";
                cmbUsuarios.DataSource = usuarios;
            }
            catch { /* Manejar error de DB */ }
        }

        private void TurnOnCamera()
        {
            if (running) return;
            try
            {
                cam = new VideoCapture(0);
                if (!cam.IsOpened()) return;

                running = true;
                Task.Run(() =>
                {
                    while (running)
                    {
                        if (recording_type == RecordingType.training)
                            Spotface();
                        else
                            RecognizeFace();
                    }
                });
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private async void TurnOffCamera()
        {
            running = false;
            await Task.Delay(150);
            if (cam != null)
            {
                cam.Release();
                cam.Dispose();
                cam = null;
            }
            pctCamara.Image = null;
        }

        private void Spotface()
        {
            if (!running || cam == null) return;
            cam.Read(frame);
            if (frame.Empty()) return;

            using (var gray = new Mat())
            {
                Cv2.CvtColor(frame, gray, ColorConversionCodes.BGR2GRAY);
                var faces = face_detector.DetectMultiScale(gray, 1.3, 4);

                foreach (var face in faces)
                {
                    Cv2.Rectangle(frame, face, Scalar.Red, 2);

                    if ((DateTime.Now - lastSave).TotalMilliseconds >= 600)
                    {
                        // USAR INVOKE PARA OBTENER EL ID DEL COMBOBOX
                        int currentUserId = 0;
                        this.Invoke(new Action(() => {
                            currentUserId = (int)cmbUsuarios.SelectedValue;
                        }));

                        Mat face_crop = new Mat(gray, face);
                        Cv2.Resize(face_crop, face_crop, new OpenCvSharp.Size(model_width, model_height));

                        SaveToDatabaseAndLocal(currentUserId, face_crop);
                        lastSave = DateTime.Now;
                    }
                }
            }

            // USAR INVOKE PARA MOSTRAR LA IMAGEN
            this.Invoke(new Action(() => {
                pctCamara.Image = BitmapConverter.ToBitmap(frame);
            }));
        }

        private void RecognizeFace()
        {
            if (!running || cam == null) return;
            cam.Read(frame);
            if (frame.Empty()) return;

            using (var gray = new Mat())
            {
                Cv2.CvtColor(frame, gray, ColorConversionCodes.BGR2GRAY);
                var faces = face_detector.DetectMultiScale(gray, 1.3, 4);

                foreach (var face in faces)
                {
                    Mat face_crop = new Mat(gray, face);
                    Cv2.Resize(face_crop, face_crop, new OpenCvSharp.Size(model_width, model_height));

                    string name = "Desconocido";
                    try
                    {
                        eigen_face_recognizer.Predict(face_crop, out int label, out double conf);
                        if (label > 0 && conf < threshold) name = $"ID: {label}";
                    }
                    catch { }

                    Cv2.Rectangle(frame, face, Scalar.Green, 2);
                    Cv2.PutText(frame, name, new OpenCvSharp.Point(face.X, face.Y - 10),
                        HersheyFonts.HersheyComplex, 0.8, Scalar.White);
                }
            }

            // ACTUALIZACIÓN SEGURA DE UI
            this.Invoke(new Action(() => {
                pctCamara.Image = BitmapConverter.ToBitmap(frame);
            }));
        }

        private void SaveToDatabaseAndLocal(int userId, Mat face)
        {
            try
            {
                ClsAcciones db = new ClsAcciones();
                if (db.ContarFotosUsuario(userId) < 30)
                {
                    byte[] data = MatToByteArray(face);
                    db.GuardarFotoRostro(userId, data);

                    // Local
                    string userDir = Path.Combine(path_faces, userId.ToString());
                    if (!Directory.Exists(userDir)) Directory.CreateDirectory(userDir);
                    File.WriteAllBytes(Path.Combine(userDir, $"{DateTime.Now.Ticks}.bmp"), data);
                }
                else
                {
                    this.Invoke(new Action(() => {
                        TurnOffCamera();
                        MessageBox.Show("Entrenamiento completado.");
                    }));
                }
            }
            catch { }
        }

        private byte[] MatToByteArray(Mat m) => m.ToBytes(".bmp");

        private void btnEntrenar_Click(object sender, EventArgs e)
        {
            if (cmbUsuarios.SelectedValue == null) return;
            recording_type = RecordingType.training;
            TurnOnCamera();
        }
    }
}