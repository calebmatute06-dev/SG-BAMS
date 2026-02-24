using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using OpenCvSharp.Face;

namespace SG_BAMS
{
    public partial class frmImagenEmpleado : Form
    {
        #region Variables y configuración

        private enum RecordingType { Training = 0, Recognition = 1 }

        private RecordingType recordingType = RecordingType.Recognition;

        private VideoCapture cam;
        private Mat frame;

        private CascadeClassifier faceDetector;
        private EigenFaceRecognizer eigenFaceRecognizer;

        private volatile bool running = false;
        private DateTime lastSave = DateTime.MinValue;

        // Tamaño para redimensionar rostros
        private readonly int modelWidth = 100;
        private readonly int modelHeight = 100;

        // Modelo Eigen
        private readonly int eigenComponents = 80;
        private readonly int threshold = 3000;

        // Rutas
        private readonly string pathFaces = Path.Combine(Application.StartupPath, "Faces");
        private readonly string pathModel = Path.Combine(Application.StartupPath, "Faces", "stateModel.yaml");
        private readonly string pathXml = Path.Combine(Application.StartupPath, "haarcascade_frontalface_default.xml");

        #endregion

        public frmImagenEmpleado()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;
            frame = new Mat();

            if (!Directory.Exists(pathFaces))
                Directory.CreateDirectory(pathFaces);

            pctCamara.SizeMode = PictureBoxSizeMode.Zoom;

            // Eventos (en tu Designer no venían enlazados)
            btnEntrenar.Click += btnEntrenar_Click;
            btnEncender.Click += btnEncender_Click;
            btnDetener.Click += btnDetener_Click;
            btnBorrar.Click += btnBorrar_Click;
            btnSalir.Click += btnSalir_Click;

            // Verificar XML
            if (!File.Exists(pathXml))
            {
                MessageBox.Show("No se encontró haarcascade_frontalface_default.xml en:\n" + pathXml);
                BloquearBotones();
                return;
            }

            faceDetector = new CascadeClassifier(pathXml);

            // Inicializar Eigen
            eigenFaceRecognizer = EigenFaceRecognizer.Create(eigenComponents, threshold);

            // Cargar modelo si existe (evita crash)
            if (File.Exists(pathModel))
            {
                try { eigenFaceRecognizer.Read(pathModel); }
                catch { /* ignorar si está corrupto */ }
            }

            // Inicia con cámara apagada
            TurnOffCamera();

            // Combo usuarios
            CargarUsuariosCombo();
        }

        private void BloquearBotones()
        {
            btnEntrenar.Enabled = false;
            btnEncender.Enabled = false;
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
                MessageBox.Show("Error al cargar usuarios: " + ex.Message);
            }
        }

        private int GetSelectedUserId()
        {
            try
            {
                if (cmbUsuarios.SelectedValue == null) return 0;
                return int.Parse(cmbUsuarios.SelectedValue.ToString());
            }
            catch
            {
                return 0;
            }
        }

        private string GetSelectedUserName()
        {
            try
            {
                if (cmbUsuarios.SelectedItem == null) return "";
                dynamic u = cmbUsuarios.SelectedItem;
                return u.NombreCompleto?.ToString() ?? "";
            }
            catch
            {
                return "";
            }
        }

        private string GetLocalUserFolder(int usuarioId)
        {
            string folder = Path.Combine(pathFaces, usuarioId.ToString());
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
                        if (recordingType == RecordingType.Training)
                            SpotFace();
                        else
                            RecognizeFace();

                        await Task.Delay(10); // evita CPU 100%
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al iniciar cámara: " + ex.Message);
            }
        }

        private async void TurnOffCamera()
        {
            running = false;
            await Task.Delay(120);

            try
            {
                if (pctCamara.Image != null)
                {
                    pctCamara.Image.Dispose();
                    pctCamara.Image = null;
                }

                if (cam != null)
                {
                    if (cam.IsOpened())
                        cam.Release();
                    cam.Dispose();
                    cam = null;
                }
            }
            catch
            {
                // ignore
            }
        }

        #endregion

        #region Entrenamiento (captura)

        private void SpotFace()
        {
            if (!running || cam == null || !cam.IsOpened() || faceDetector == null)
                return;

            cam.Read(frame);
            if (frame.Empty()) return;

            using (var gray = new Mat())
            {
                Cv2.CvtColor(frame, gray, ColorConversionCodes.BGR2GRAY);

                var faces = faceDetector.DetectMultiScale(gray, 1.3, 4);

                foreach (var face in faces)
                {
                    Cv2.Rectangle(frame, face, Scalar.Red, 2);

                    // Guardar cada 500ms
                    if ((DateTime.Now - lastSave).TotalMilliseconds < 500)
                        continue;

                    int userId = GetSelectedUserId();
                    if (userId <= 0) continue;

                    try
                    {
                        ClsAcciones db = new ClsAcciones();
                        int fotosSql = db.ContarFotosUsuario(userId);

                        if (fotosSql < 30)
                        {
                            using (Mat faceCrop = new Mat(gray, face))
                            {
                                Cv2.Resize(faceCrop, faceCrop,
                                    new OpenCvSharp.Size(modelWidth, modelHeight),
                                    0, 0, InterpolationFlags.Cubic);

                                byte[] data = MatToByteArray(faceCrop);

                                // Guardar en BD
                                db.GuardarFotoRostro(userId, data);

                                // Guardar local en carpeta del usuario
                                string folder = GetLocalUserFolder(userId);
                                string filePath = Path.Combine(folder, $"{DateTime.Now.Ticks}.bmp");
                                File.WriteAllBytes(filePath, data);

                                lastSave = DateTime.Now;
                            }
                        }
                        else
                        {
                            // Ya hay 30 fotos -> detener y entrenar
                            Invoke(new Action(() =>
                            {
                                TurnOffCamera();

                                bool ok = TrainDataSetWithEigenFaceRecognizer();
                                MessageBox.Show(ok
                                    ? "Entrenamiento completado (30 fotos) y modelo actualizado."
                                    : "Hay fotos, pero no se pudo regenerar el modelo.");
                            }));
                        }
                    }
                    catch (Exception ex)
                    {
                        Invoke(new Action(() =>
                        {
                            TurnOffCamera();
                            MessageBox.Show("Error en entrenamiento: " + ex.Message);
                        }));
                    }
                }
            }

            ActualizarUI(frame);
        }

        #endregion

        #region Reconocimiento

        private void RecognizeFace()
        {
            if (!running || cam == null || !cam.IsOpened() || faceDetector == null)
                return;

            Mat imageFrame = new Mat();
            cam.Read(imageFrame);

            if (imageFrame.Empty())
                return;

            using (var gray = new Mat())
            {
                Cv2.CvtColor(imageFrame, gray, ColorConversionCodes.BGR2GRAY);

                // Similar al documento (más robusto)
                Rect[] faces = faceDetector.DetectMultiScale(
                    gray,
                    1.4,
                    4,
                    HaarDetectionTypes.ScaleImage,
                    new OpenCvSharp.Size(imageFrame.Width / 8, imageFrame.Height / 8)
                );

                foreach (var face in faces)
                {
                    string name = "unknown";

                    using (Mat faceRegion = new Mat(gray, face))
                    using (Mat imageToCompare = new Mat())
                    {
                        Cv2.Resize(faceRegion, imageToCompare,
                            new OpenCvSharp.Size(modelWidth, modelHeight),
                            0, 0, InterpolationFlags.Cubic);

                        try
                        {
                            if (!File.Exists(pathModel))
                            {
                                name = "Sin modelo";
                            }
                            else
                            {
                                eigenFaceRecognizer.Predict(imageToCompare, out int predictedLabel, out double confidence);

                                if (predictedLabel >= 0 && confidence < threshold)
                                    name = GetFacesName(predictedLabel);
                                else
                                    name = "unknown";
                            }
                        }
                        catch (Exception ex)
                        {
                            // Protección anti-crash como el documento
                            Console.WriteLine("Error en Predict(): " + ex.Message);
                            name = "unknown";
                        }
                    }

                    Cv2.Rectangle(imageFrame, face, Scalar.BurlyWood, 3);
                    Cv2.PutText(imageFrame, name, new OpenCvSharp.Point(face.X, face.Y - 10),
                        HersheyFonts.HersheyComplex, 0.8, Scalar.White);
                }
            }

            ActualizarUI(imageFrame);
            imageFrame.Dispose();
        }

        private string GetFacesName(int label)
        {
            // En tu app, lo más confiable es el ComboBox (BD)
            foreach (var item in cmbUsuarios.Items)
            {
                try
                {
                    dynamic u = item;
                    int id = Convert.ToInt32(u.Usuario_id);
                    if (id == label)
                        return u.NombreCompleto?.ToString() ?? "unknown";
                }
                catch { }
            }
            return "unknown";
        }

        #endregion

        #region Entrenamiento de modelo (BD -> Eigen)

        // Adaptación directa del método del documento
        private bool TrainDataSetWithEigenFaceRecognizer()
        {
            try
            {
                // Borrar modelo anterior
                if (File.Exists(pathModel))
                    File.Delete(pathModel);

                ClsAcciones db = new ClsAcciones();
                var usuarios = db.ObtenerUsuarios();

                List<Mat> images = new List<Mat>();
                List<int> labels = new List<int>();

                foreach (var u in usuarios)
                {
                    int userId;
                    try
                    {
                        dynamic du = u;
                        userId = Convert.ToInt32(du.Usuario_id);
                    }
                    catch
                    {
                        continue;
                    }

                    List<byte[]> fotos = db.ObtenerRostrosPorUsuario(userId);

                    foreach (byte[] foto in fotos)
                    {
                        Mat faceImage = Mat.FromImageData(foto, ImreadModes.Grayscale);

                        Cv2.Resize(faceImage, faceImage,
                            new OpenCvSharp.Size(modelWidth, modelHeight),
                            0, 0, InterpolationFlags.Cubic);

                        images.Add(faceImage);
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
                MessageBox.Show("Error al entrenar modelo: " + ex.Message);
                return false;
            }
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

        private byte[] MatToByteArray(Mat img)
        {
            using (MemoryStream ms = new MemoryStream())
            using (Bitmap bitmap = BitmapConverter.ToBitmap(img))
            {
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                return ms.ToArray();
            }
        }

        private void BorrarFotosLocales(int usuarioId)
        {
            // En tu estructura guardas por carpeta Faces\{id}
            string folder = Path.Combine(pathFaces, usuarioId.ToString());
            if (!Directory.Exists(folder))
                return;

            try
            {
                Directory.Delete(folder, true);
            }
            catch
            {
                // si hay bloqueo por archivos, forzar GC y reintentar
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

                try { Directory.Delete(folder, true); } catch { }
            }
        }

        #endregion

        #region Eventos botones

        private void btnEntrenar_Click(object sender, EventArgs e)
        {
            int id = GetSelectedUserId();
            if (id <= 0)
            {
                MessageBox.Show("Debe seleccionar un usuario antes de iniciar el entrenamiento.");
                return;
            }

            // reiniciar temporizador de guardado
            lastSave = DateTime.MinValue;

            recordingType = RecordingType.Training;
            TurnOnCamera();
        }

        private void btnEncender_Click(object sender, EventArgs e)
        {
            // Encender para reconocimiento
            recordingType = RecordingType.Recognition;

            // cargar modelo si existe
            if (File.Exists(pathModel))
            {
                try { eigenFaceRecognizer.Read(pathModel); } catch { }
                TurnOnCamera();
            }
            else
            {
                MessageBox.Show("Modelo entrenado inválido o no existe. Primero entrene y genere modelo.");
            }
        }

        private void btnDetener_Click(object sender, EventArgs e)
        {
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

            if (MessageBox.Show("¿Borrar fotos del usuario seleccionado (BD y local) y reentrenar modelo?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                // 1) detener todo
                running = false;

                // 2) apagar cámara
                TurnOffCamera();

                // 3) liberar archivos
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

                // 4) borrar local
                BorrarFotosLocales(id);

                // 5) borrar en BD
                ClsAcciones db = new ClsAcciones();
                db.BorrarFotosUsuario(id);

                // 6) reentrenar modelo
                bool ok = TrainDataSetWithEigenFaceRecognizer();

                MessageBox.Show(ok
                    ? "Fotos eliminadas y modelo actualizado."
                    : "Fotos eliminadas, pero no se pudo regenerar el modelo.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al borrar fotos: " + ex.Message);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            TurnOffCamera();
            Close();
        }

        #endregion
    }
}