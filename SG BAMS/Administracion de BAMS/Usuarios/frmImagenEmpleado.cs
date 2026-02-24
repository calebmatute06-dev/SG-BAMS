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

        // OpenCV
        private VideoCapture cam;
        private Mat frame;
        private CascadeClassifier faceDetector;
        private EigenFaceRecognizer eigenFaceRecognizer;

        // Control
        private volatile bool running = false;
        private RecordingType recordingType = RecordingType.Recognition;
        private DateTime lastSave = DateTime.MinValue;

        // Modelo
        private readonly int modelWidth = 100;
        private readonly int modelHeight = 100;
        private readonly int eigenComponents = 80;
        private readonly int threshold = 3000;

        // Rutas
        private readonly string pathFaces = Path.Combine(Application.StartupPath, "Faces");
        private readonly string pathXml = Path.Combine(Application.StartupPath, "haarcascade_frontalface_default.xml");
        private readonly string pathModel = Path.Combine(Application.StartupPath, "Faces", "stateModel.yaml");

        private enum RecordingType
        {
            Training = 0,
            Recognition = 1
        }

        #endregion

        #region Constructor / Init

        public frmImagenEmpleado()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;
            frame = new Mat();

            // Asegura carpeta Faces
            if (!Directory.Exists(pathFaces))
                Directory.CreateDirectory(pathFaces);

            // Mejor visual
            pctCamara.SizeMode = PictureBoxSizeMode.Zoom;

            // Conectar eventos (TU DESIGNER no los tenía)
            btnEntrenar.Click += btnEntrenar_Click;
            btnEncender.Click += btnEncender_Click;
            btnDetener.Click += btnDetener_Click;
            btnBorrar.Click += btnBorrar_Click;
            btnSalir.Click += btnSalir_Click;

            // Cargar detector
            if (!File.Exists(pathXml))
            {
                MessageBox.Show("Error: No se encontró el archivo XML de detección facial:\n" + pathXml);

                // Si no hay detector, no tiene sentido continuar
                btnEntrenar.Enabled = false;
                btnEncender.Enabled = false;
                btnDetener.Enabled = false;
                btnBorrar.Enabled = false;
                return;
            }

            faceDetector = new CascadeClassifier(pathXml);

            // Reconocedor
            eigenFaceRecognizer = EigenFaceRecognizer.Create(eigenComponents, threshold);

            // Cargar modelo si existe
            if (File.Exists(pathModel))
            {
                try
                {
                    eigenFaceRecognizer.Read(pathModel);
                }
                catch
                {
                    // Si el archivo está corrupto, lo ignoramos
                }
            }

            // Cargar usuarios
            CargarUsuariosCombo();
        }

        private void CargarUsuariosCombo()
        {
            try
            {
                ClsAcciones db = new ClsAcciones();
                var usuarios = db.ObtenerUsuarios();

                cmbUsuarios.DisplayMember = "NombreCompleto";
                cmbUsuarios.ValueMember = "Usuario_id";
                cmbUsuarios.DataSource = usuarios;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar usuarios: " + ex.Message);
            }
        }

        #endregion

        #region Cámara (On/Off)

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

                        // Evita 100% CPU
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
            await Task.Delay(150);

            try
            {
                if (cam != null)
                {
                    if (cam.IsOpened())
                        cam.Release();

                    cam.Dispose();
                    cam = null;
                }
            }
            catch { /* ignore */ }

            ActualizarUI(null);
        }

        #endregion

        #region Entrenamiento (captura)

        private void SpotFace()
        {
            if (!running || cam == null || faceDetector == null) return;

            cam.Read(frame);
            if (frame.Empty()) return;

            using (var gray = new Mat())
            {
                Cv2.CvtColor(frame, gray, ColorConversionCodes.BGR2GRAY);
                var faces = faceDetector.DetectMultiScale(gray, 1.3, 4);

                foreach (var face in faces)
                {
                    Cv2.Rectangle(frame, face, Scalar.Red, 2);

                    // Cada 500ms
                    if ((DateTime.Now - lastSave).TotalMilliseconds < 500)
                        continue;

                    int currentUserId = GetSelectedUserId();
                    if (currentUserId <= 0) continue;

                    ClsAcciones db = new ClsAcciones();
                    int count = db.ContarFotosUsuario(currentUserId);

                    if (count < 30)
                    {
                        using (Mat faceCrop = new Mat(gray, face))
                        {
                            Cv2.Resize(faceCrop, faceCrop, new OpenCvSharp.Size(modelWidth, modelHeight));

                            byte[] data = MatToByteArray(faceCrop);
                            db.GuardarFotoRostro(currentUserId, data);

                            // Guardado local
                            string userDir = Path.Combine(pathFaces, currentUserId.ToString());
                            if (!Directory.Exists(userDir))
                                Directory.CreateDirectory(userDir);

                            File.WriteAllBytes(Path.Combine(userDir, $"{DateTime.Now.Ticks}.bmp"), data);

                            lastSave = DateTime.Now;
                        }
                    }
                    else
                    {
                        // Ya tiene 30 -> detener y entrenar automáticamente
                        Invoke(new Action(() =>
                        {
                            TurnOffCamera();

                            bool ok = TrainDataSet();
                            if (ok)
                                MessageBox.Show("Entrenamiento completado y modelo generado.");
                            else
                                MessageBox.Show("Entrenamiento completado, pero NO se pudo generar modelo (sin datos).");
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
            if (!running || cam == null || faceDetector == null) return;

            cam.Read(frame);
            if (frame.Empty()) return;

            using (var gray = new Mat())
            {
                Cv2.CvtColor(frame, gray, ColorConversionCodes.BGR2GRAY);
                var faces = faceDetector.DetectMultiScale(gray, 1.3, 4);

                foreach (var face in faces)
                {
                    using (Mat faceCrop = new Mat(gray, face))
                    {
                        Cv2.Resize(faceCrop, faceCrop, new OpenCvSharp.Size(modelWidth, modelHeight));

                        string name = "Desconocido";

                        try
                        {
                            // Si el modelo no existe, no intentes predecir
                            if (File.Exists(pathModel))
                            {
                                eigenFaceRecognizer.Predict(faceCrop, out int label, out double conf);

                                if (label >= 0 && conf < threshold)
                                    name = GetFacesName(label);
                            }
                            else
                            {
                                name = "Sin modelo (entrena)";
                            }
                        }
                        catch
                        {
                            name = "Error modelo";
                        }

                        Cv2.Rectangle(frame, face, Scalar.Green, 2);
                        Cv2.PutText(frame, name, new OpenCvSharp.Point(face.X, face.Y - 10),
                            HersheyFonts.HersheyComplex, 0.8, Scalar.White);
                    }
                }
            }

            ActualizarUI(frame);
        }

        private string GetFacesName(int label)
        {
            foreach (var item in cmbUsuarios.Items)
            {
                dynamic u = item;
                if (u.Usuario_id == label)
                    return u.NombreCompleto;
            }

            return "ID: " + label;
        }

        #endregion

        #region Entrenar modelo (dataset)

        private bool TrainDataSet()
        {
            try
            {
                if (File.Exists(pathModel))
                    File.Delete(pathModel);

                ClsAcciones db = new ClsAcciones();
                var usuarios = db.ObtenerUsuarios();

                List<Mat> images = new List<Mat>();
                List<int> labels = new List<int>();

                foreach (var u in usuarios)
                {
                    // ✅ CORRECCIÓN: usar Usuario_id (como lo devuelve ClsAcciones)
                    int id = u.Usuario_id;

                    var fotos = db.ObtenerRostrosPorUsuario(id);

                    foreach (var f in fotos)
                    {
                        Mat m = Mat.FromImageData(f, ImreadModes.Grayscale);
                        Cv2.Resize(m, m, new OpenCvSharp.Size(modelWidth, modelHeight));

                        images.Add(m);
                        labels.Add(id);
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

        #region UI / utilidades

        private void ActualizarUI(Mat img)
        {
            if (!IsHandleCreated) return;

            Invoke(new Action(() =>
            {
                if (img == null || img.Empty())
                {
                    pctCamara.Image = null;
                }
                else
                {
                    // Reemplazar imagen anterior (evita fugas)
                    var old = pctCamara.Image;
                    pctCamara.Image = BitmapConverter.ToBitmap(img);
                    old?.Dispose();
                }
            }));
        }

        private byte[] MatToByteArray(Mat m)
        {
            using (MemoryStream ms = new MemoryStream())
            using (Bitmap bmp = BitmapConverter.ToBitmap(m))
            {
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                return ms.ToArray();
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

        private void BorrarCarpetaUsuarioLocal(int usuarioId)
        {
            try
            {
                string userDir = Path.Combine(pathFaces, usuarioId.ToString());
                if (Directory.Exists(userDir))
                    Directory.Delete(userDir, true);
            }
            catch { /* ignore */ }
        }

        #endregion

        #region Eventos botones (ya conectados en el constructor)

        private void btnEntrenar_Click(object sender, EventArgs e)
        {
            int id = GetSelectedUserId();
            if (id <= 0)
            {
                MessageBox.Show("Seleccione un usuario.");
                return;
            }

            recordingType = RecordingType.Training;
            TurnOnCamera();
        }

        private void btnEncender_Click(object sender, EventArgs e)
        {
            // Encender cámara para reconocimiento
            recordingType = RecordingType.Recognition;
            TurnOnCamera();
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

            var confirm = MessageBox.Show(
                "¿Seguro que deseas borrar las fotos del usuario seleccionado?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                TurnOffCamera();

                ClsAcciones db = new ClsAcciones();
                db.BorrarFotosUsuario(id);

                // Borrar carpeta local
                BorrarCarpetaUsuarioLocal(id);

                // Borrar modelo para obligar a regenerarlo
                if (File.Exists(pathModel))
                    File.Delete(pathModel);

                MessageBox.Show("Fotos borradas. Vuelve a entrenar para generar modelo.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al borrar: " + ex.Message);
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