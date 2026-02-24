using OpenCvSharp;
using OpenCvSharp.Extensions;
using OpenCvSharp.Face;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SG_BAMS
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class frmImagenEmpleado : Form
    {

        /// <summary>
        /// 
        /// </summary>
        enum RecordingType
        {
            /// <summary>
            /// The training
            /// </summary>
            training = 0,
            /// <summary>
            /// The recognition
            /// </summary>
            recognition = 1
        }
        /// <summary>
        /// The recording type
        /// </summary>
        RecordingType recording_type;

        // Tamaño para redimensionar rostros
        /// <summary>
        /// The model width
        /// </summary>
        int model_width = 100;
        /// <summary>
        /// The model height
        /// </summary>
        int model_height = 100;

        // Rutas de guardado
        /// <summary>
        /// The path saved faces
        /// </summary>
        string path_saved_faces = Path.Combine(Application.StartupPath, "Faces");
        /// <summary>
        /// The path trained face model
        /// </summary>
        string path_trained_face_model = $"{Application.StartupPath}\\Faces\\stateModel.yaml";

        // HaarCascade (OpenCvSharp) - DETECTOR DE ROSTROS EN IMAGENES XML
        /// <summary>
        /// The path reconzier faces model
        /// </summary>
        string path_reconzier_facesModel = $"{Application.StartupPath}\\haarcascade_frontalface_default.xml";

        // Cámara y detector
        /// <summary>
        /// The cam
        /// </summary>
        VideoCapture cam;
        /// <summary>
        /// The frame
        /// </summary>
        Mat frame;
        /// <summary>
        /// The face detector
        /// </summary>
        CascadeClassifier face_detector;
        /// <summary>
        /// The running
        /// </summary>
        bool running = false;

        /// <summary>
        /// The trained images
        /// </summary>
        List<Mat> trainedImages = new List<Mat>();
        /// <summary>
        /// The labels
        /// </summary>
        List<string> labels = new List<string>();

        /// <summary>
        /// The eigen face recognizer
        /// </summary>
        EigenFaceRecognizer eigen_face_recognizer;
        //Indexar cada rostro empezando desde el 1
        /// <summary>
        /// The face identifier
        /// </summary>
        int face_id = 1;
        /// <summary>
        /// The face name
        /// </summary>
        string face_name = "";
        //Detectar si es una cara
        /// <summary>
        /// The is anew face
        /// </summary>
        bool is_anew_face = false;
        //componentes
        /// <summary>
        /// The eigen face recognizer componentes
        /// </summary>
        int eigen_face_recognizer_componentes = 80;
        //margen de error o de fallo = 5000
        /// <summary>
        /// The threshold
        /// </summary>
        int threshold = 3000;



        /// <summary>
        /// Initializes a new instance of the <see cref="RECONOCIMIENTO_FACIAL"/> class.
        /// </summary>

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

        //reseteamos
        /// <summary>
        /// Resets the initialize values.
        /// </summary>
        private void ResetInitValues()
        {
            is_anew_face = true;
        }

        /// <summary>
        /// Cargars the usuarios combo.
        /// </summary>
        public void CargarUsuariosCombo()
        {
            ClsAcciones objacciones = new();
            List<Usuario> usuarios = objacciones.ObtenerUsuarios();

            cmbUsuarios.DisplayMember = "NombreCompleto";
            cmbUsuarios.ValueMember = "Usuario_id";
            cmbUsuarios.DataSource = usuarios;
        }

        /// <summary>
        /// Gets the local user folder.
        /// </summary>
        /// <param name="usuarioId">The usuario identifier.</param>
        /// <returns></returns>
        private string GetLocalUserFolder(int usuarioId)
        {
            string folder = Path.Combine(Application.StartupPath, "Faces", usuarioId.ToString());

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            return folder;
        }





        /// <summary>
        /// Recognizes the face.
        /// </summary>
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
            }

            // Mostrar imagen
            pctCamara.Image = BitmapConverter.ToBitmap(image_frame);
        }

        /// <summary>
        /// Gets the next face identifier.
        /// </summary>
        /// <returns></returns>
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

        //distinguir nombres de las caras
        /// <summary>
        /// Gets the item list face.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
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

        /// <summary>
        /// The last save
        /// </summary>
        private DateTime lastSave = DateTime.MinValue;

        /// <summary>
        /// Spotfaces this instance.
        /// </summary>
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
                        }
                    }
                }
            }

            pctCamara.Image = BitmapConverter.ToBitmap(frame);
        }


        /// <summary>
        /// Turns the on camera.
        /// </summary>
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

        /// <summary>
        /// Turns the off camera.
        /// </summary>
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
        //obtenemos las imagenes de rostros
        /// <summary>
        /// Gets all faces path.
        /// </summary>
        /// <returns></returns>
        private string[] GetAllFacesPath()
        {
            return Directory.GetFiles(path_saved_faces, "*.bmp");
        }

        //obtenemos el id de un rostro existente
        /// <summary>
        /// Getfaces the identifier from path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
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

        //obtenemos el siguiente indice de unas secuencia del mismo rostro
        /// <summary>
        /// Gets a index face from path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        private string GetAIndexFaceFromPath(string path)
        {

            var slices = path.Split("\\");
            var name_and_index = slices[slices.Length - 1].Replace(".bmp", "");
            if (face_id == int.Parse(name_and_index.Split("_")[0]))
                return name_and_index.Split("_")[2];
            return "";

        }
        //se guardaran cuando la camara deje de grabar

        /// <summary>
        /// Saves the faces.
        /// </summary>
        /// <param name="faces_name">Name of the faces.</param>
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

        //recuperar el actual indice del rostro si ya fue guardado si no vamos a guardarlo con el indice 1
        /// <summary>
        /// Nexts the index from an existing face.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the next index face.
        /// </summary>
        /// <returns></returns>
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


        //entrenar rostros
        /// <summary>
        /// Trainings the face.
        /// </summary>
        private void TrainingFace()
        {
            //revisamos si el archivo que guarda el resultado del entrenamiento y se borra porque se
            //tiene que actualizar el resultado

            if (File.Exists(path_trained_face_model))
            {
                File.Delete(path_trained_face_model);
            }
        }
        /// <summary>
        /// Handles the Tick event of the timer1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (cmbUsuarios.SelectedItem is Usuario seleccionado)
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

        /// <summary>
        /// Handles the Click event of the button2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button2_Click(object sender, EventArgs e)
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

            cmbUsuarios.SelectedIndex = cmbUsuarios.Items.Count - 1;

            is_anew_face = false;
            face_id = GetNextFaceId();
        }

        //Entrenamiento de rostros
        /// <summary>
        /// Trains the data set with eigen face recognizer.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Handles the Load event of the RECONOCIMIENTO_FACIAL control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void RECONOCIMIENTO_FACIAL_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }

        /// <summary>
        /// Handles the Click event of the PictureBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void PictureBox1_Click(object sender, EventArgs e) { }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the comboBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Usuario seleccionado = cmbUsuarios.SelectedItem as Usuario;

            if (seleccionado != null)
            {
                face_id = seleccionado.Usuario_id;
                face_name = seleccionado.NombreCompleto;

                // esto indica que NO es una nueva cara
                is_anew_face = false;
            }
        }

        /// <summary>
        /// Gets the name of the faces.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Handles the Click event of the button4 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button4_Click(object sender, EventArgs e)
        {
           
        }

        /// <summary>
        /// Counts the faces of identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Handles the Click event of the button3 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        //CONVERTIR A BYTE PARA INGRESAR A LA DB
        /// <summary>
        /// Mats to byte array.
        /// </summary>
        /// <param name="img">The img.</param>
        /// <returns></returns>
        private byte[] MatToByteArray(Mat img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Bitmap bitmap = BitmapConverter.ToBitmap(img);
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Borrars the fotos locales.
        /// </summary>
        /// <param name="usuario_id">The usuario identifier.</param>
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


        /// <summary>
        /// Handles the Click event of the button5 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Handles the 1 event of the pictureBox1_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEntrenar_Click(object sender, EventArgs e)
        {
            if (cmbUsuarios.SelectedItem is Usuario seleccionado)
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

        private void btnEncender_Click(object sender, EventArgs e)
        {
            cam = new VideoCapture();
            recording_type = RecordingType.recognition;


            //antes de encender la camara debemos decirle a eigen donde esta el archivo preentrenado sino
            //existe no existira el reconocimiento
            if (File.Exists(path_trained_face_model))
            {
                eigen_face_recognizer.Read(path_trained_face_model);
                TurnOnCamera();

            }
            else
            {
                MessageBox.Show("Modelo entrenado invalido");
            }
        }

        private void btnDetener_Click(object sender, EventArgs e)
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
    }


}