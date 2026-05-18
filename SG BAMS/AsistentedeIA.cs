using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Asistente de IA — carga el manual automáticamente al iniciar.
    /// Coloca "Manual.pdf" o "Manual.txt" en la misma carpeta del .exe
    /// Compatible con el designer original (lstIA, txtInfo, btnEnviar, btnBorrar, btnSalir).
    /// </summary>
    public partial class AsistentedeIA : Form
    {
       
        private ClsServicioAyudaIA _servicioIA = new ClsServicioAyudaIA();
        private bool _enviando = false;

     
        private const string NOMBRE_MANUAL_PDF = "Manual.pdf";
        private const string NOMBRE_MANUAL_TXT = "Manual.txt";

      
        public AsistentedeIA()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }


        private void AsistentedeIA_Load(object sender, EventArgs e)
        {
            lstIA.HorizontalScrollbar = false;
            MostrarBienvenida();
            CargarManualAutomatico();  
            txtInfo.Focus();
        }

       

        /// <summary>
        /// Busca el manual en la carpeta del .exe y lo carga automáticamente.
        /// Primero busca Manual.pdf, si no existe busca Manual.txt.
        /// Si no encuentra ninguno, el asistente sigue funcionando solo con la BD.
        /// </summary>
        private void CargarManualAutomatico()
        {
            
            string carpetaExe = AppDomain.CurrentDomain.BaseDirectory;

            string rutaPdf = Path.Combine(carpetaExe, NOMBRE_MANUAL_PDF);
            string rutaTxt = Path.Combine(carpetaExe, NOMBRE_MANUAL_TXT);

            string rutaFinal = "";

            
            if (File.Exists(rutaPdf))
                rutaFinal = rutaPdf;
            else if (File.Exists(rutaTxt))
                rutaFinal = rutaTxt;

            if (string.IsNullOrEmpty(rutaFinal))
            {
                lstIA.Items.Add("*********************************************");
                lstIA.Items.Add("ℹ️  No se encontró manual. Coloca 'Manual.pdf'");
                lstIA.Items.Add("    o 'Manual.txt' en la carpeta del programa.");
                lstIA.Items.Add("*********************************************");
                return;
            }

        
            try
            {
                int fragmentos = _servicioIA.CargarManual(rutaFinal);

                lstIA.Items.Add("*********************************************");
                lstIA.Items.Add($"✅ Manual cargado: {Path.GetFileName(rutaFinal)}");
                lstIA.Items.Add($"📚 {fragmentos} secciones listas para consultar.");
                lstIA.Items.Add("*********************************************");
            }
            catch (Exception ex)
            {
                lstIA.Items.Add("*********************************************");
                lstIA.Items.Add($"⚠️  No se pudo leer el manual: {ex.Message}");
                lstIA.Items.Add("*********************************************");
            }

            lstIA.TopIndex = lstIA.Items.Count - 1;
        }

      

        /// <summary>Muestra el mensaje de bienvenida inicial.</summary>
        private void MostrarBienvenida()
        {
            lstIA.Items.Clear();
            lstIA.Items.Add("*********************************************");
            lstIA.Items.Add("🤖 Asistente: ¡Hola! Soy tu asistente de SG BAMS.");
            lstIA.Items.Add("🤖 Asistente: ¿En qué puedo ayudarte hoy?");
            lstIA.Items.Add("*********************************************");
            lstIA.Items.Add("📊 Resúmenes: Pregunta por lo más vendido o el stock.");
            lstIA.Items.Add("💰 Cuentas: Consulta quién debe y cuánto es el saldo.");
            lstIA.Items.Add("📦 Productos: Busca precios, marcas y modelos de auto.");
            lstIA.Items.Add("📄 Manual: Pregunta cómo usar cualquier parte del sistema.");
            lstIA.Items.Add("*********************************************");
            lstIA.Items.Add("💡 Prueba: '¿Qué productos se venden más?'");
            lstIA.Items.Add("💡 Prueba: '¿Cómo registro una venta?'");
        }

        

        /// <summary>Envía la pregunta a la IA y muestra la respuesta.</summary>
        private async Task EnviarMensaje()
        {
            if (_enviando) return;

            string pregunta = txtInfo.Text.Trim();
            if (string.IsNullOrEmpty(pregunta)) return;

            _enviando = true;
            btnEnviar.Enabled = false;
            txtInfo.Clear();

            lstIA.Items.Add("*********************************************");
            lstIA.Items.Add("👤 Tú: " + pregunta);
            lstIA.Items.Add("⏳ Asistente escribiendo...");
            lstIA.TopIndex = lstIA.Items.Count - 1;

            try
            {
                string respuesta = await _servicioIA.ConsultarAsync(pregunta);

                
                if (lstIA.Items.Count > 0)
                    lstIA.Items.RemoveAt(lstIA.Items.Count - 1);

                lstIA.Items.Add("🤖 Asistente:");
                MostrarTextoFormateado(respuesta);
            }
            catch (Exception ex)
            {
                if (lstIA.Items.Count > 0)
                    lstIA.Items.RemoveAt(lstIA.Items.Count - 1);
                lstIA.Items.Add("⚠️ Error: " + ex.Message);
            }
            finally
            {
                _enviando = false;
                btnEnviar.Enabled = true;
                lstIA.TopIndex = lstIA.Items.Count - 1;
                txtInfo.Focus();
            }
        }

        /// <summary>Parte líneas largas y las agrega al ListBox.</summary>
        private void MostrarTextoFormateado(string texto)
        {
            const int LIMITE = 100;

            string[] lineas = texto.Split(new[] { "\n", "\r\n" }, StringSplitOptions.None);

            foreach (string linea in lineas)
            {
                string resto = linea.Trim();

                if (string.IsNullOrEmpty(resto))
                {
                    lstIA.Items.Add("");
                    continue;
                }

                while (resto.Length > LIMITE)
                {
                    int corte = resto.LastIndexOf(' ', LIMITE);
                    if (corte <= 0) corte = LIMITE;
                    lstIA.Items.Add("   " + resto.Substring(0, corte).Trim());
                    resto = resto.Substring(corte).Trim();
                }

                if (!string.IsNullOrEmpty(resto))
                    lstIA.Items.Add("   " + resto);
            }
        }


        private async void btnEnviar_Click(object sender, EventArgs e) =>
            await EnviarMensaje();

        /// <summary>Borra el chat y recarga el manual automáticamente.</summary>
        private void btnBorrar_Click(object sender, EventArgs e)
        {
            _servicioIA.LimpiarHistorial();
            txtInfo.Clear();
            MostrarBienvenida();
            CargarManualAutomatico();  
            txtInfo.Focus();
        }

        private void btnSalir_Click(object sender, EventArgs e) =>
            this.Close();

        private async void txtInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                await EnviarMensaje();
            }
        }
    }
}