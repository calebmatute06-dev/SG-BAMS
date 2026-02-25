using OpenCvSharp;
using OpenCvSharp.Extensions;
using OpenCvSharp.Face;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SG_BAMS
{
    public partial class frmImagenEmpleado : Form
    {
        enum RecordingType
        {
            training = 0,
            recognition = 1
        }

        RecordingType recording_type;

        // Tamaño para redimensionar rostros
        int model_width = 100;
        int model_height = 100;

        // Rutas de guardado
        string path_saved_faces = Path.Combine(Application.StartupPath, "Faces");
        string path_trained_face_model = $"{Application.StartupPath}\\Faces\\stateModel.yaml";

        // HaarCascade (OpenCvSharp) - DETECTOR DE ROSTROS EN IMAGENES XML
        string path_reconzier_facesModel = $"{Application.StartupPath}\\haarcascade_frontalface_default.xml";

        // Cámara y detector
        VideoCapture cam;
        Mat frame;
        CascadeClassifier face_detector;
        bool running = false;

        List<Mat> trainedImages = new List<Mat>();
        List<string> labels = new List<string>();

        EigenFaceRecognizer eigen_face_recognizer;
        //Indexar cada rostro empezando desde el 1
        int face_id = 1;
        string face_name = "";
        //Detectar si es una cara
        bool is_anew_face = false;
        //componentes
        int eigen_face_recognizer_componentes = 80;
        //margen de error o de fallo = 5000
        int threshold = 3000;

        public frmImagenEmpleado()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            frame = new Mat();

            //MessageBox.Show("StartupPath: " + Application.StartupPath);

            // Verificación del XML
            if (!File.Exists(path_reconzier_facesModel))
            {
                MessageBox.Show("No se encontró el archivo haarcascade_frontalface_default.xml");
            }
            else
            {
                face_detector = new CascadeClassifier(path_reconzier_facesModel);
            }

            TurnOffCamera();

            CargarUsuariosCombo();

            //inicializamos el objeto que ejecutara el algoritmo EIGEN
            //Pero primero necesitamos especificar el numero de componentes y el margen de error
            eigen_face_recognizer = EigenFaceRecognizer.Create(eigen_face_recognizer_componentes, threshold);
        }
        private void ResetInitValues()
        {
            is_anew_face = true;
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

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            return folder;
        }
        void RecognizeFace()
        {
            string name = "unknown";

            // Capturar frame
            Mat image_frame = new Mat();
            cam.Read(image_frame);

            if (image_frame.Empty())
                return;

            // Convertir a gris
            Mat gray_frame = new Mat();
            Cv2.CvtColor(image_frame, gray_frame, ColorConversionCodes.BGR2GRAY);

            // Detectar rostros
            Rect[] faces = face_detector.DetectMultiScale(
                gray_frame,
                1.4,
                4,
                OpenCvSharp.HaarDetectionTypes.ScaleImage,
                new OpenCvSharp.Size(image_frame.Width / 8, image_frame.Height / 8)
            );

            foreach (var face in faces)
            {
                // Recorte del rostro
                Mat face_region = new Mat(gray_frame, face);

                // Redimensionar
                Mat image_to_compare = new Mat();
                Cv2.Resize(face_region, image_to_compare,
                    new OpenCvSharp.Size(model_width, model_height),
                    0, 0, InterpolationFlags.Cubic);

                // PROTECCIÓN CONTRA CRASH EN PREDICT()
                try
                {
                    eigen_face_recognizer.Predict(image_to_compare,
                        out int predicted_label,
                        out double confidence);

                    if (predicted_label > 0 && confidence < threshold)
                        name = GetFacesName(predicted_label);
                    else
                        name = "unknown";
                }
                catch (Exception ex)
                {
                    // EVITAR QUE LA APLICACIÓN SE CIERRE
                    Console.WriteLine("Error en Predict(): " + ex.Message);
                    name = "unknown";
                }

                // Dibujar contorno
                Cv2.Rectangle(image_frame, face, Scalar.BurlyWood, 3);
                Cv2.PutText(image_frame, name, new OpenCvSharp.Point(face.X, face.Y - 10),
                    HersheyFonts.HersheyComplexSmall, 1, Scalar.BurlyWood);
            }

            // Mostrar imagen
            pctCamara.Image = BitmapConverter.ToBitmap(image_frame);
        }
        private int GetNextFaceId()
        {
            int face_id = 0;
            var paths = GetAllFacesPath();
            foreach (var p in paths)
            {
                int cId = int.Parse(GetfaceIdFromPath(p));
                if (cId > face_id)
                {
                    face_id = cId;
                }
            }
            return Math.Max(face_id, 1) + 1;
        }
        private KeyValuePair<string, int> GetItemListFace(string path)
        {
            var slices = path.Split('\\');
            var name_and_index = slices[slices.Length - 1].Replace(".bmp", "");

            var parts = name_and_index.Split('_');

            // si el archivo NO cumple el formato ID_Nombre_Indice → evitar crash
            if (parts.Length < 3)
                return new KeyValuePair<string, int>("INVALID", -1);

            // VALIDACIÓN: si el ID NO es un número → evitar crash
            if (!int.TryParse(parts[0], out int id))
                return new KeyValuePair<string, int>("INVALID", -1);

            string name = parts[1];

            return new KeyValuePair<string, int>(name, id);
        }
        private DateTime lastSave = DateTime.MinValue;
        private void Spotface()
        {
            if (!running || cam == null || !cam.IsOpened())
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

                    Mat face_crop = new Mat(gray, face);
                    Cv2.Resize(face_crop, face_crop, new OpenCvSharp.Size(model_width, model_height));

                    // *** aseguro que se usan fotos del usuario correcto ***
                    trainedImages.Add(face_crop.Clone());

                    // guardar cada 500ms
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

                                // Guardar también en carpeta local
                                string folder = GetLocalUserFolder(face_id);
                                string file_path = Path.Combine(folder, $"{new_photo_id}.bmp");
                                File.WriteAllBytes(file_path, data);

                                lastSave = DateTime.Now;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error SQL: " + ex.Message);
                            }
                        }
                        else
                        {
                            running = false;
                            TurnOffCamera();
                            MessageBox.Show("Entrenamiento completado (30 fotos).");
                            return; // <-- IMPORTANTÍSIMO: corta Spotface de inmediato
                        }
                    }
                }
            }

            pctCamara.Image = BitmapConverter.ToBitmap(frame);
        }
        private void TurnOnCamera()
        {
            try
            {
                cam = new VideoCapture(0);

                if (!cam.IsOpened())
                {
                    MessageBox.Show("No se pudo abrir la cámara.");
                    return;
                }

                running = true;

                Task.Run(() =>
                {
                    while (running)
                    {
                        if (recording_type == RecordingType.training)
                            Spotface();
                        else if (recording_type == RecordingType.recognition)
                            RecognizeFace();
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

            // Esperar un poco para que el loop se detenga
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
            catch (Exception ex)
            {
                MessageBox.Show("Error al apagar cámara: " + ex.Message);
            }
        }
        private string[] GetAllFacesPath()
        {
            return Directory.GetFiles(path_saved_faces, "*.bmp");
        }
        private string GetfaceIdFromPath(string path)
        {
            // Extraer solo el nombre del archivo sin extensión
            var slices = path.Split("\\");
            var name_and_index = slices[slices.Length - 1].Replace(".bmp", "");

            // Separar por "_"
            var parts = name_and_index.Split('_');

            //si el archivo NO cumple el formato ID_Nombre_Indice
            // evitar crashear y devolver "-1" (así se ignora en el entrenamiento)
            if (parts.Length < 3)
                return "-1";

            //si el ID NO es un número, evitar crash
            if (!int.TryParse(parts[0], out int id))
                return "-1";

            return id.ToString();
        }
        private string GetAIndexFaceFromPath(string path)
        {
            var slices = path.Split("\\");
            var name_and_index = slices[slices.Length - 1].Replace(".bmp", "");
            if (face_id == int.Parse(name_and_index.Split("_")[0]))
                return name_and_index.Split("_")[2];
            return "";
        }
        //se guardaran cuando la camara deje de grabar
        private void SaveFaces(string faces_name)
        {
            if (trainedImages.Any() && !string.IsNullOrEmpty(faces_name))
            {
                faces_name = faces_name.Replace("_", "");
            }

            int current_count = CountFacesOfId(face_id);

            int indx = GetNextIndexFace();
            var faces_to_save = trainedImages.ToList();

            foreach (var face in faces_to_save)
            {
                if (current_count >= 30)
                    break;

                face.SaveImage($"{path_saved_faces}/{face_id}_{faces_name}_{indx}.bmp");

                indx++;
                current_count++;
            }
        }
        private int NextIndexFromAnExistingFace()
        {
            int index = -1;
            var all_faces = GetAllFacesPath();
            foreach (var p in all_faces)
            {
                var face_index = GetAIndexFaceFromPath(p);
                if (!string.IsNullOrEmpty(face_index))
                    if (int.Parse(face_index) > index)
                        index = int.Parse(face_index);
            }
            return index + 1;
        }
        int GetNextIndexFace()
        {
            if (!is_anew_face)
            {
                return NextIndexFromAnExistingFace();
            }
            else
            {
                return 1;
            }
        }
        private void TrainingFace()
        {
            //revisamos si el archivo que guarda el resultado del entrenamiento y se borra porque se
            //tiene que actualizar el resultado

            if (File.Exists(path_trained_face_model))
            {
                File.Delete(path_trained_face_model);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (recording_type == RecordingType.training)
            {
                Spotface();
            }
            if (recording_type == RecordingType.recognition)
            {
                RecognizeFace();
            }
        }

        private void btnEncender_Click(object sender, EventArgs e)
        {
            if (cmbUsuarios2.SelectedItem is Usuario seleccionado)
            {
                face_id = seleccionado.Usuario_id;
                face_name = seleccionado.NombreCompleto;

                // *** REINICIAR BUFERS ***
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

        private void btnDetener_Click(object sender, EventArgs e)
        {
            TurnOffCamera();
            try
            {
                if (cam != null)
                {
                    if (cam.IsOpened())
                    {
                        cam.Release();   // Se libera solo si está abierta
                    }

                    cam.Dispose();        // Se libera memoria (solo si no está ya disposed)
                    cam = null;           // Evita llamar Release() dos veces
                }

                running = false;
                pctCamara.Image = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al apagar cámara: " + ex.Message);
            }
            //salvamos las imagenes
            //etiquetar las caras por las que no preguntamos nombres.

            SaveFaces(face_name);

            cmbUsuarios2.SelectedIndex = cmbUsuarios2.Items.Count - 1;

            is_anew_face = false;
            face_id = GetNextFaceId();
        }
        private bool TrainDataSetWithEigenFaceRecognizer()
        {
            //En caso de que exista un archivo de entrenamiento removerlo
            if (File.Exists(path_trained_face_model))
            {
                File.Delete(path_trained_face_model);
            }

            // Obtener TODAS las fotos desde SQL
            ClsAcciones db = new ClsAcciones();
            List<Usuario> usuarios = db.ObtenerUsuarios();

            List<Mat> images = new List<Mat>();
            List<int> labels = new List<int>();

            //si existe alguna
            foreach (var usuario in usuarios)
            {
                //obtener fotos del usuario directamente desde BD ***
                List<byte[]> fotos = db.ObtenerRostrosPorUsuario(usuario.Usuario_id);

                foreach (byte[] foto in fotos)
                {
                    // obtenemos el rostro en el mismo tamaño que hemos estado guardandolo
                    Mat face_image = Mat.FromImageData(foto, ImreadModes.Grayscale);

                    // Redimensionar igual que en EmguCV
                    Cv2.Resize(face_image, face_image, new OpenCvSharp.Size(model_width, model_height),
                               0, 0, InterpolationFlags.Cubic);

                    //guardamos el rostro
                    images.Add(face_image);

                    // Obtener ID desde la BD
                    labels.Add(usuario.Usuario_id);
                }
            }

            // listas que OpenCvSharp sí acepta
            if (images.Count == 0)
                return false;

            // entrenar
            eigen_face_recognizer.Train(images, labels);

            // guardar
            eigen_face_recognizer.Write(path_trained_face_model);

            return true;
        }

        private void frmImagenEmpleado_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }

        private void pctCamara_Click(object sender, EventArgs e)
        {

        }

        private void cmbUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            Usuario seleccionado = cmbUsuarios2.SelectedItem as Usuario;

            if (seleccionado != null)
            {
                face_id = seleccionado.Usuario_id;
                face_name = seleccionado.NombreCompleto;

                // esto indica que NO es una nueva cara
                is_anew_face = false;
            }
        }
        private string GetFacesName(int label)
        {
            string[] files = Directory.GetFiles(path_saved_faces, "*.bmp");

            foreach (string f in files)
            {
                string file = Path.GetFileNameWithoutExtension(f);
                string[] parts = file.Split('_');

                if (parts.Length >= 2)
                {
                    int id = int.Parse(parts[0]);
                    string name = parts[1];

                    if (id == label)
                        return name;
                }
            }

            return "unknown";
        }

        private void btnEntrenar_Click(object sender, EventArgs e)
        {
            bool was_trained = TrainDataSetWithEigenFaceRecognizer();
            if (was_trained)
            {
                MessageBox.Show("Entrenamiendo exitoso");
            }
            else
            {
                MessageBox.Show("Entrenamiento fallido");
            }
        }
        private int CountFacesOfId(int id)
        {
            int count = 0;
            var files = Directory.GetFiles(path_saved_faces, "*.bmp");

            foreach (var f in files)
            {
                var file = Path.GetFileNameWithoutExtension(f);
                var parts = file.Split('_');

                if (parts.Length >= 3 && int.TryParse(parts[0], out int fileId))
                {
                    if (fileId == id)
                        count++;
                }
            }

            return count;
        }
        private byte[] MatToByteArray(Mat img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Bitmap bitmap = BitmapConverter.ToBitmap(img);
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                return ms.ToArray();
            }
        }
        private void BorrarFotosLocales(int usuario_id)
        {
            string folder = Path.Combine(Application.StartupPath, "Faces");

            if (!Directory.Exists(folder))
                return;

            // Buscar archivos del usuario (2_*.bmp)
            string patron = $"{usuario_id}_*.bmp";

            string[] archivos = Directory.GetFiles(folder, patron);

            foreach (var archivo in archivos)
            {
                try
                {
                    File.Delete(archivo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo borrar: " + archivo + "\n" + ex.Message);
                }
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Detener cualquier reconocimiento o captura
                running = false;

                // 2. Apagar cámara si está en uso
                if (cam != null)
                {
                    try { cam.Release(); } catch { }
                    try { cam.Dispose(); } catch { }
                    cam = null;
                }

                // 3. Liberar imagen del PictureBox
                if (pctCamara.Image != null)
                {
                    pctCamara.Image.Dispose();
                    pctCamara.Image = null;
                }

                // 4. Forzar liberación de archivos bloqueados
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

                // 5. Eliminar archivos locales
                BorrarFotosLocales(face_id);

                // 6. Eliminar fotos en BD
                ClsAcciones db = new ClsAcciones();
                db.BorrarFotosUsuario(face_id);

                // 7. REENTRENAR el sistema para eliminar el modelo del usuario
                if (TrainDataSetWithEigenFaceRecognizer())
                {
                    MessageBox.Show("Fotos eliminadas y modelo actualizado.");
                }
                else
                {
                    MessageBox.Show("Fotos eliminadas, pero no se pudo regenerar el modelo.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al borrar fotos: " + ex.Message);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //------------------------------------------------Fin
    }
}