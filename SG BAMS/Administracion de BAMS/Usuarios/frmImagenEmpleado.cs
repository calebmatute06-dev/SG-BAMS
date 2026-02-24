using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using OpenCvSharp.Face;

namespace SG_BAMS
{
    public partial class frmImagenEmpleado : Form
    {
        #region Variables

        private enum RecordingType { Idle = 0, Capture30 = 1, Recognition = 2 }

        private RecordingType recordingType = RecordingType.Idle;

        private VideoCapture cam;
        private Mat frame;
        private CascadeClassifier faceDetector;
        private EigenFaceRecognizer eigenFaceRecognizer;

        private volatile bool running = false;
        private DateTime lastSave = DateTime.MinValue;

        private readonly int modelWidth = 100;
        private readonly int modelHeight = 100;
        private readonly int eigenComponents = 80;
        private readonly int threshold = 3000;

        // ✅ Esta ruta termina exactamente en:
        // ...\bin\Debug\net8.0-windows\Faces
        private readonly string pathFaces = Path.Combine(AppContext.BaseDirectory, "Faces");

        private readonly string pathXml = Path.Combine(AppContext.BaseDirectory, "haarcascade_frontalface_default.xml");
        private readonly string pathModel = Path.Combine(AppContext.BaseDirectory, "Faces", "stateModel.yaml");

        #endregion

        public frmImagenEmpleado()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;

            frame = new Mat();
            pctCamara.SizeMode = PictureBoxSizeMode.Zoom;

            if (!Directory.Exists(pathFaces))
                Directory.CreateDirectory(pathFaces);

            // Eventos
            btnEncender.Click += btnEncender_Click; // ✅ Captura 30 fotos
            btnEntrenar.Click += btnEntrenar_Click; // ✅ Entrena con fotos locales
            btnDetener.Click += btnDetener_Click;
            btnBorrar.Click += btnBorrar_Click;
            btnSalir.Click += btnSalir_Click;

            // Cargar Haar
            if (!File.Exists(pathXml))
            {
                MessageBox.Show("No se encontró el XML:\n" + pathXml);
                BloquearBotones();
                return;
            }

            faceDetector = new CascadeClassifier(pathXml);

            // EigenFaces
            eigenFaceRecognizer = EigenFaceRecognizer.Create(eigenComponents, threshold);

            // Cargar modelo si existe
            if (File.Exists(pathModel))
            {
                try { eigenFaceRecognizer.Read(pathModel); } catch { }
            }

            // Cargar usuarios (si tu BD funciona)
            // Si no quieres BD, comenta esto y carga el combo manual.
            CargarUsuariosCombo();
        }

        private void BloquearBotones()
        {
            btnEncender.Enabled = false;
            btnEntrenar.Enabled = false;
            btnDetener.Enabled = false;
            btnBorrar.Enabled = false;
        }

        private void CargarUsuariosCombo()
        {
            try
            {
                ClsAcciones db = new ClsAcciones();
                var usuarios = db.ObtenerUsuarios();

                cmbUsuarios.DataSource = null;
                cmbUsuarios.DisplayMember = "NombreCompleto";
                cmbUsuarios.ValueMember = "Usuario_id";
                cmbUsuarios.DataSource = usuarios;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo cargar usuarios:\n" + ex.Message);
            }
        }

        private int GetSelectedUserId()
        {
            try
            {
                if (cmbUsuarios.SelectedValue == null) return 0;
                return int.Parse(cmbUsuarios.SelectedValue.ToString());
            }
            catch { return 0; }
        }

        private string GetUserFolder(int userId)
        {
            string folder = Path.Combine(pathFaces, userId.ToString());
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
            return folder;
        }

        #region Cámara

        private void TurnOnCamera()
        {
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

                Task.Run(async () =>
                {
                    while (running)
                    {
                        if (recordingType == RecordingType.Capture30)
                            Capture30Photos();
                        else if (recordingType == RecordingType.Recognition)
                            RecognizeFace();
                        else
                            ShowVideoOnly();

                        await Task.Delay(10);
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de cámara: " + ex.Message);
            }
        }

        private async void TurnOffCamera()
        {
            running = false;
            await Task.Delay(120);

            try
            {
                if (cam != null)
                {
                    if (cam.IsOpened()) cam.Release();
                    cam.Dispose();
                    cam = null;
                }
            }
            catch { }

            ActualizarUI(null);
        }

        private void ShowVideoOnly()
        {
            if (!running || cam == null) return;

            cam.Read(frame);
            if (frame.Empty()) return;

            ActualizarUI(frame);
        }

        #endregion

        #region Encender = Capturar 30 fotos

        private void Capture30Photos()
        {
            if (!running || cam == null || faceDetector == null) return;

            cam.Read(frame);
            if (frame.Empty()) return;

            using var gray = new Mat();
            Cv2.CvtColor(frame, gray, ColorConversionCodes.BGR2GRAY);

            var faces = faceDetector.DetectMultiScale(gray, 1.3, 4);

            foreach (var face in faces)
            {
                Cv2.Rectangle(frame, face, Scalar.Red, 2);

                // cada 500ms
                if ((DateTime.Now - lastSave).TotalMilliseconds < 500)
                    continue;

                int userId = GetSelectedUserId();
                if (userId <= 0) continue;

                string userFolder = GetUserFolder(userId);

                // ✅ contar fotos locales (no BD)
                int localCount = Directory.GetFiles(userFolder, "*.bmp").Length;

                if (localCount >= 30)
                {
                    // Ya completó 30
                    Invoke(new Action(() =>
                    {
                        TurnOffCamera();
                        recordingType = RecordingType.Idle;
                        MessageBox.Show("Listo: ya se capturaron 30 fotos.\nAhora presiona ENTRENAR para generar el modelo.");
                    }));
                    break;
                }

                using (Mat faceCrop = new Mat(gray, face))
                {
                    Cv2.Resize(faceCrop, faceCrop, new OpenCvSharp.Size(modelWidth, modelHeight));

                    // Guardar BMP en carpeta requerida
                    string filePath = Path.Combine(userFolder, $"{DateTime.Now.Ticks}.bmp");
                    byte[] data = MatToByteArray(faceCrop);
                    File.WriteAllBytes(filePath, data);

                    lastSave = DateTime.Now;
                }
            }

            ActualizarUI(frame);
        }

        #endregion

        #region Entrenar (desde carpeta Faces)

        private bool TrainFromLocalFacesFolder()
        {
            try
            {
                if (File.Exists(pathModel))
                    File.Delete(pathModel);

                if (!Directory.Exists(pathFaces))
                    return false;

                // Busca subcarpetas con ID (Faces\{id}\*.bmp)
                var userDirs = Directory.GetDirectories(pathFaces);

                List<Mat> images = new List<Mat>();
                List<int> labels = new List<int>();

                foreach (var dir in userDirs)
                {
                    // folder name debe ser el id
                    string folderName = new DirectoryInfo(dir).Name;
                    if (!int.TryParse(folderName, out int userId))
                        continue;

                    var files = Directory.GetFiles(dir, "*.bmp");
                    foreach (var file in files)
                    {
                        byte[] bytes = File.ReadAllBytes(file);
                        Mat m = Mat.FromImageData(bytes, ImreadModes.Grayscale);
                        Cv2.Resize(m, m, new OpenCvSharp.Size(modelWidth, modelHeight));
                        images.Add(m);
                        labels.Add(userId);
                    }
                }

                if (images.Count == 0)
                    return false;

                eigenFaceRecognizer.Train(images, labels);
                eigenFaceRecognizer.Write(pathModel);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error entrenando modelo: " + ex.Message);
                return false;
            }
        }

        #endregion

        #region Reconocimiento (opcional)

        private void RecognizeFace()
        {
            if (!running || cam == null || faceDetector == null) return;

            cam.Read(frame);
            if (frame.Empty()) return;

            using var gray = new Mat();
            Cv2.CvtColor(frame, gray, ColorConversionCodes.BGR2GRAY);

            var faces = faceDetector.DetectMultiScale(gray, 1.3, 4);

            foreach (var face in faces)
            {
                using var faceCrop = new Mat(gray, face);
                Cv2.Resize(faceCrop, faceCrop, new OpenCvSharp.Size(modelWidth, modelHeight));

                string name = "Desconocido";

                try
                {
                    if (!File.Exists(pathModel))
                    {
                        name = "Sin modelo";
                    }
                    else
                    {
                        eigenFaceRecognizer.Predict(faceCrop, out int label, out double conf);
                        if (label >= 0 && conf < threshold)
                            name = "ID: " + label;
                    }
                }
                catch { name = "Error"; }

                Cv2.Rectangle(frame, face, Scalar.Green, 2);
                Cv2.PutText(frame, name, new OpenCvSharp.Point(face.X, face.Y - 10),
                    HersheyFonts.HersheyComplex, 0.8, Scalar.White);
            }

            ActualizarUI(frame);
        }

        #endregion

        #region Utilidades

        private void ActualizarUI(Mat img)
        {
            if (!IsHandleCreated) return;

            Invoke(new Action(() =>
            {
                if (img == null || img.Empty())
                {
                    pctCamara.Image?.Dispose();
                    pctCamara.Image = null;
                }
                else
                {
                    Image old = pctCamara.Image;
                    pctCamara.Image = BitmapConverter.ToBitmap(img);
                    old?.Dispose();
                }
            }));
        }

        private byte[] MatToByteArray(Mat m)
        {
            using var ms = new MemoryStream();
            using var bmp = BitmapConverter.ToBitmap(m);
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            return ms.ToArray();
        }

        private void DeleteFolderSafe(string folder)
        {
            try
            {
                if (Directory.Exists(folder))
                    Directory.Delete(folder, true);
            }
            catch
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                try { Directory.Delete(folder, true); } catch { }
            }
        }

        #endregion

        #region Eventos

        // ✅ Encender Cámara = Capturar 30 fotos
        private void btnEncender_Click(object sender, EventArgs e)
        {
            int id = GetSelectedUserId();
            if (id <= 0)
            {
                MessageBox.Show("Seleccione un usuario primero.");
                return;
            }

            // Reinicia timer para capturas
            lastSave = DateTime.MinValue;

            recordingType = RecordingType.Capture30;
            TurnOnCamera();
        }

        // ✅ Entrenar = Generar modelo con las fotos guardadas en Faces
        private void btnEntrenar_Click(object sender, EventArgs e)
        {
            bool ok = TrainFromLocalFacesFolder();
            MessageBox.Show(ok
                ? "Modelo generado correctamente (stateModel.yaml)."
                : "No hay fotos para entrenar. Primero usa ENCENDER para capturar 30 fotos.");
        }

        private void btnDetener_Click(object sender, EventArgs e)
        {
            recordingType = RecordingType.Idle;
            TurnOffCamera();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            int id = GetSelectedUserId();
            if (id <= 0)
            {
                MessageBox.Show("Seleccione un usuario.");
                return;
            }

            if (MessageBox.Show("¿Borrar las fotos locales del usuario seleccionado?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            TurnOffCamera();

            string folder = Path.Combine(pathFaces, id.ToString());
            DeleteFolderSafe(folder);

            MessageBox.Show("Fotos locales borradas.");
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            TurnOffCamera();
            Close();
        }

        #endregion
    }
}