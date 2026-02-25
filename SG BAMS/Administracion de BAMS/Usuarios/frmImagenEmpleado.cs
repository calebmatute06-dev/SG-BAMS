using OpenCvSharp;
using OpenCvSharp.Extensions;
using OpenCvSharp.Face;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SG_BAMS
{
    public partial class frmImagenEmpleado : Form
    {
        enum RecordingType { training = 0, recognition = 1 }
        RecordingType recording_type;

        int model_width = 100;
        int model_height = 100;

        string path_saved_faces = Path.Combine(Application.StartupPath, "Faces");
        string path_trained_face_model = Path.Combine(Application.StartupPath, "Faces", "stateModel.yaml");
        string path_reconzier_facesModel = Path.Combine(Application.StartupPath, "haarcascade_frontalface_default.xml");

        VideoCapture cam;
        Mat frame;
        CascadeClassifier face_detector;

        volatile bool running = false;
        Task captureTask;

        List<Mat> trainedImages = new List<Mat>();

        EigenFaceRecognizer eigen_face_recognizer;

        int face_id = 1;
        string face_name = "";
        bool is_anew_face = false;

        int eigen_face_recognizer_componentes = 80;
        int threshold = 3000;

        DateTime lastSave = DateTime.MinValue;

        public frmImagenEmpleado()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            // Asegurar carpeta Faces
            Directory.CreateDirectory(path_saved_faces);

            frame = new Mat();

            // Verificar XML
            if (!File.Exists(path_reconzier_facesModel))
            {
                MessageBox.Show("No se encontró el archivo haarcascade_frontalface_default.xml\n" + path_reconzier_facesModel);
                // Deshabilitar controles que dependen de cámara/detector
                btnEncender.Enabled = false;
                btnDetener.Enabled = false;
                btnEntrenar.Enabled = false;
                btnBorrar.Enabled = false;
                return;
            }
            face_detector = new CascadeClassifier(path_reconzier_facesModel);

            // Crear recognizer
            eigen_face_recognizer = EigenFaceRecognizer.Create(eigen_face_recognizer_componentes, threshold);

            // Cargar modelo si existe
            if (File.Exists(path_trained_face_model))
            {
                try { eigen_face_recognizer.Read(path_trained_face_model); }
                catch { /* si el modelo está corrupto, luego reentrenas */ }
            }

            // Importante: NO usar Timer + Task a la vez
            if (timer1 != null) timer1.Enabled = false;

            CargarUsuariosCombo();
        }

        public void CargarUsuariosCombo()
        {
            ClsAcciones objacciones = new ClsAcciones();
            List<Usuario> usuarios = objacciones.ObtenerUsuarios();

            cmbUsuarios2.DisplayMember = "NombreCompleto";
            cmbUsuarios2.ValueMember = "Usuario_id";
            cmbUsuarios2.DataSource = usuarios;
        }

        private string GetLocalUserFolder(int usuarioId)
        {
            string folder = Path.Combine(Application.StartupPath, "Faces", usuarioId.ToString());
            Directory.CreateDirectory(folder);
            return folder;
        }

        private async Task TurnOffCameraAsync()
        {
            running = false;

            // Esperar a que el loop termine (si existe)
            if (captureTask != null)
            {
                try { await captureTask; }
                catch { /* ignorar si terminó con excepción */ }
                captureTask = null;
            }

            try
            {
                // Liberar imagen
                if (pctCamara.Image != null)
                {
                    pctCamara.Image.Dispose();
                    pctCamara.Image = null;
                }

                // Liberar cámara
                if (cam != null)
                {
                    try { if (cam.IsOpened()) cam.Release(); } catch { }
                    try { cam.Dispose(); } catch { }
                    cam = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al apagar cámara: " + ex.Message);
            }
        }

        private void TurnOnCamera()
        {
            if (face_detector == null)
            {
                MessageBox.Show("Detector de rostros no inicializado (XML faltante).");
                return;
            }

            if (running) return;

            try
            {
                cam = new VideoCapture(0);
                if (!cam.IsOpened())
                {
                    MessageBox.Show("No se pudo abrir la cámara.");
                    cam?.Dispose();
                    cam = null;
                    return;
                }

                running = true;

                captureTask = Task.Run(() =>
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
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar cámara: " + ex.Message);
            }
        }

        private void Spotface()
        {
            if (!running || cam == null || !cam.IsOpened() || face_detector == null)
                return;

            cam.Read(frame);
            if (frame.Empty()) return;

            using (var gray = new Mat())
            {
                Cv2.CvtColor(frame, gray, ColorConversionCodes.BGR2GRAY);

                var faces = face_detector.DetectMultiScale(gray, 1.3, 4);

                foreach (var face in faces)
                {
                    Cv2.Rectangle(frame, face, Scalar.Red, 2);

                    using var face_crop = new Mat(gray, face);
                    Cv2.Resize(face_crop, face_crop, new OpenCvSharp.Size(model_width, model_height));

                    // Buffer local para entrenamiento (opcional)
                    trainedImages.Add(face_crop.Clone());

                    // Guardar cada 500ms
                    if ((DateTime.Now - lastSave).TotalMilliseconds >= 500)
                    {
                        ClsAcciones db = new ClsAcciones();
                        int fotos_sql = db.ContarFotosUsuario(face_id);

                        if (fotos_sql < 30)
                        {
                            try
                            {
                                byte[] data = MatToByteArray(face_crop);
                                int new_photo_id = db.GuardarFotoRostro(face_id, data);

                                // Guardar también en carpeta por usuario
                                string folder = GetLocalUserFolder(face_id);
                                string file_path = Path.Combine(folder, $"{new_photo_id}.bmp");
                                File.WriteAllBytes(file_path, data);

                                lastSave = DateTime.Now;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error guardando foto: " + ex.Message);
                            }
                        }
                        else
                        {
                            running = false;

                            this.BeginInvoke(new Action(async () =>
                            {
                                await TurnOffCameraAsync();
                                MessageBox.Show("Entrenamiento completado (30 fotos).");
                                btnEntrenar.PerformClick(); // entrenar automáticamente
                            }));

                            return;
                        }
                    }
                }
            }

            var bmp = BitmapConverter.ToBitmap(frame);
            this.BeginInvoke(new Action(() =>
            {
                pctCamara.Image?.Dispose();
                pctCamara.Image = bmp;
            }));
        }

        private void RecognizeFace()
        {
            if (!running || cam == null || !cam.IsOpened() || face_detector == null)
                return;

            Mat image_frame = new Mat();
            cam.Read(image_frame);

            if (image_frame.Empty())
            {
                image_frame.Dispose();
                return;
            }

            string name = "unknown";

            using (var gray_frame = new Mat())
            {
                Cv2.CvtColor(image_frame, gray_frame, ColorConversionCodes.BGR2GRAY);

                Rect[] faces = face_detector.DetectMultiScale(
                    gray_frame,
                    1.4,
                    4,
                    HaarDetectionTypes.ScaleImage,
                    new OpenCvSharp.Size(image_frame.Width / 8, image_frame.Height / 8)
                );

                foreach (var face in faces)
                {
                    using var face_region = new Mat(gray_frame, face);
                    using var image_to_compare = new Mat();

                    Cv2.Resize(face_region, image_to_compare,
                        new OpenCvSharp.Size(model_width, model_height),
                        0, 0, InterpolationFlags.Cubic);

                    try
                    {
                        eigen_face_recognizer.Predict(image_to_compare,
                            out int predicted_label,
                            out double confidence);

                        // EigenFaces: confidence suele ser distancia, menor = mejor
                        if (predicted_label != -1 && confidence < threshold)
                            name = predicted_label.ToString(); // o GetFacesName(predicted_label);
                        else
                            name = "unknown";
                    }
                    catch
                    {
                        name = "unknown";
                    }

                    Cv2.Rectangle(image_frame, face, Scalar.BurlyWood, 3);
                    Cv2.PutText(image_frame, name, new OpenCvSharp.Point(face.X, face.Y - 10),
                        HersheyFonts.HersheyComplexSmall, 1, Scalar.BurlyWood);
                }
            }

            var bmp = BitmapConverter.ToBitmap(image_frame);
            image_frame.Dispose();

            this.BeginInvoke(new Action(() =>
            {
                pctCamara.Image?.Dispose();
                pctCamara.Image = bmp;
            }));
        }

        private bool TrainDataSetWithEigenFaceRecognizer()
        {
            // Borrar modelo anterior
            if (File.Exists(path_trained_face_model))
            {
                try { File.Delete(path_trained_face_model); } catch { }
            }

            ClsAcciones db = new ClsAcciones();
            List<Usuario> usuarios = db.ObtenerUsuarios();

            List<Mat> images = new List<Mat>();
            List<int> labels = new List<int>();

            foreach (var usuario in usuarios)
            {
                List<byte[]> fotos = db.ObtenerRostrosPorUsuario(usuario.Usuario_id);

                foreach (byte[] foto in fotos)
                {
                    Mat face_image = Mat.FromImageData(foto, ImreadModes.Grayscale);
                    Cv2.Resize(face_image, face_image, new OpenCvSharp.Size(model_width, model_height),
                               0, 0, InterpolationFlags.Cubic);

                    images.Add(face_image);
                    labels.Add(usuario.Usuario_id);
                }
            }

            if (images.Count == 0)
                return false;

            eigen_face_recognizer.Train(images, labels);
            eigen_face_recognizer.Write(path_trained_face_model);

            // liberar Mats del entrenamiento
            foreach (var m in images) m.Dispose();

            return true;
        }

        private byte[] MatToByteArray(Mat img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (Bitmap bitmap = BitmapConverter.ToBitmap(img))
                {
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                    return ms.ToArray();
                }
            }
        }

        private void BorrarFotosLocales(int usuario_id)
        {
            // Borra subcarpeta del usuario
            string userFolder = Path.Combine(Application.StartupPath, "Faces", usuario_id.ToString());
            if (Directory.Exists(userFolder))
            {
                try { Directory.Delete(userFolder, true); }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo borrar carpeta de usuario:\n" + ex.Message);
                }
            }

            // (Opcional) también borrar archivos root con patrón usuario_id_*.bmp si existieran
            string root = Path.Combine(Application.StartupPath, "Faces");
            if (!Directory.Exists(root)) return;

            foreach (var archivo in Directory.GetFiles(root, $"{usuario_id}_*.bmp"))
            {
                try { File.Delete(archivo); } catch { }
            }
        }

        private void frmImagenEmpleado_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }

        private void cmbUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUsuarios2.SelectedItem is Usuario seleccionado)
            {
                face_id = seleccionado.Usuario_id;
                face_name = seleccionado.NombreCompleto;
                is_anew_face = false;
            }
        }

        private void btnEncender_Click(object sender, EventArgs e)
        {
            if (cmbUsuarios2.SelectedItem is Usuario seleccionado)
            {
                face_id = seleccionado.Usuario_id;
                face_name = seleccionado.NombreCompleto;

                trainedImages.Clear();
                is_anew_face = false;

                recording_type = RecordingType.training;
                TurnOnCamera();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un usuario antes de iniciar el entrenamiento.");
            }
        }

        private async void btnDetener_Click(object sender, EventArgs e)
        {
            await TurnOffCameraAsync();

            // Limpieza de buffer
            foreach (var m in trainedImages) m.Dispose();
            trainedImages.Clear();
        }

        private void btnEntrenar_Click(object sender, EventArgs e)
        {
            bool was_trained = TrainDataSetWithEigenFaceRecognizer();
            MessageBox.Show(was_trained ? "Entrenamiento exitoso" : "Entrenamiento fallido");
        }

        private async void btnBorrar_Click(object sender, EventArgs e)
        {
            try
            {
                await TurnOffCameraAsync();

                // 1) borrar local
                BorrarFotosLocales(face_id);

                // 2) borrar BD
                ClsAcciones db = new ClsAcciones();
                db.BorrarFotosUsuario(face_id);

                // 3) reentrenar
                if (TrainDataSetWithEigenFaceRecognizer())
                    MessageBox.Show("Fotos eliminadas y modelo actualizado.");
                else
                    MessageBox.Show("Fotos eliminadas, pero no se pudo regenerar el modelo.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al borrar fotos: " + ex.Message);
            }
        }

        private async void btnSalir_Click(object sender, EventArgs e)
        {
            await TurnOffCameraAsync();
            this.Close();
        }

        // Si todavía tienes el timer en el diseñador, déjalo apagado
        private void timer1_Tick(object sender, EventArgs e)
        {
            // NO USAR (evita doble loop)
        }
    }
}