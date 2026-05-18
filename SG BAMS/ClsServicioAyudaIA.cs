using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS
{
    /// <summary>
    /// Servicio de IA con soporte de manual de usuario y contexto de base de datos.
    /// Utiliza fragmentación (chunking) para manejar manuales extensos.
    /// </summary>
    internal class ClsServicioAyudaIA
    {
       
        private readonly HttpClient _client;
        private readonly string _apiKey = "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";  
        private readonly string _url = "https://api.groq.com/openai/v1/chat/completions";
        private readonly string _modelo = "llama-3.1-8b-instant";

        
        private readonly string _cadenaConexion =
            "Data Source=AutoBattDB.mssql.somee.com;" +
            "Initial Catalog=AutoBattDB;" +
            "User ID=exobonnie_SQLLogin_1;" +
            "Password=w6et2uoghs;" +
            "TrustServerCertificate=True;";

        

        /// <summary>Fragmentos del manual para búsqueda por relevancia.</summary>
        private List<string> _chunks = new List<string>();

        /// <summary>Nombre del archivo de manual cargado.</summary>
        public string NombreManualCargado { get; private set; } = "";

        /// <summary>Indica si hay un manual cargado.</summary>
        public bool TieneManual => _chunks.Count > 0;

        /// <summary>Tamaño máximo de cada fragmento en caracteres.</summary>
        private const int TAMANO_CHUNK = 1500;

        /// <summary>Cuántos fragmentos enviar al modelo por consulta.</summary>
        private const int MAX_CHUNKS_POR_CONSULTA = 3;

        
        private readonly List<object> _historial = new List<object>();
        private const int MAX_HISTORIAL = 10; // pares usuario/asistente

        
        public ClsServicioAyudaIA()
        {
            _client = new HttpClient();
            _client.Timeout = TimeSpan.FromSeconds(60);
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
        }

        

        /// <summary>
        /// Carga un archivo de manual (.txt o .pdf) y lo divide en fragmentos.
        /// </summary>
        /// <param name="rutaArchivo">Ruta completa del archivo.</param>
        /// <returns>Cantidad de fragmentos generados.</returns>
        public int CargarManual(string rutaArchivo)
        {
            string textoCompleto = "";
            string extension = Path.GetExtension(rutaArchivo).ToLower();

            switch (extension)
            {
                case ".txt":
                    textoCompleto = File.ReadAllText(rutaArchivo, Encoding.UTF8);
                    break;

                case ".pdf":
                    textoCompleto = LeerPdf(rutaArchivo);
                    break;

                default:
                    throw new NotSupportedException($"Formato no soportado: {extension}. Use .txt o .pdf");
            }

            if (string.IsNullOrWhiteSpace(textoCompleto))
                throw new Exception("El archivo está vacío o no se pudo leer.");

           
            textoCompleto = LimpiarTexto(textoCompleto);
            _chunks = Fragmentar(textoCompleto, TAMANO_CHUNK);
            NombreManualCargado = Path.GetFileName(rutaArchivo);

           
            _historial.Clear();

            return _chunks.Count;
        }

        /// <summary>Lee texto de un PDF usando UglyToad.PdfPig.</summary>
        private string LeerPdf(string ruta)
        {
           
            try
            {
                var sb = new StringBuilder();
                using (var documento = UglyToad.PdfPig.PdfDocument.Open(ruta))
                {
                    foreach (var pagina in documento.GetPages())
                    {
                        sb.AppendLine(pagina.Text);
                        sb.AppendLine(); // separador entre páginas
                    }
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al leer PDF: {ex.Message}\n\nAsegúrate de instalar el paquete NuGet: UglyToad.PdfPig");
            }
        }

        /// <summary>Limpia espacios y saltos de línea innecesarios del texto.</summary>
        private string LimpiarTexto(string texto)
        {
           
            while (texto.Contains("\r\n\r\n\r\n"))
                texto = texto.Replace("\r\n\r\n\r\n", "\r\n\r\n");
            while (texto.Contains("\n\n\n"))
                texto = texto.Replace("\n\n\n", "\n\n");

            return texto.Trim();
        }

        /// <summary>
        /// Divide el texto en fragmentos respetando párrafos.
        /// </summary>
        private List<string> Fragmentar(string texto, int tamano)
        {
            var resultado = new List<string>();
            string[] parrafos = texto.Split(new[] { "\n\n", "\r\n\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            var chunkActual = new StringBuilder();

            foreach (string parrafo in parrafos)
            {
                string p = parrafo.Trim();
                if (string.IsNullOrEmpty(p)) continue;

                
                if (chunkActual.Length + p.Length > tamano && chunkActual.Length > 0)
                {
                    resultado.Add(chunkActual.ToString().Trim());
                    chunkActual.Clear();
                }

                
                if (p.Length > tamano)
                {
                    for (int i = 0; i < p.Length; i += tamano)
                    {
                        int largo = Math.Min(tamano, p.Length - i);
                        resultado.Add(p.Substring(i, largo).Trim());
                    }
                }
                else
                {
                    chunkActual.AppendLine(p);
                }
            }

            if (chunkActual.Length > 0)
                resultado.Add(chunkActual.ToString().Trim());

            return resultado;
        }

        /// <summary>
        /// Elimina el manual cargado y reinicia el historial.
        /// </summary>
        public void EliminarManual()
        {
            _chunks.Clear();
            NombreManualCargado = "";
            _historial.Clear();
        }

       
        

        /// <summary>
        /// Busca los fragmentos del manual más relevantes para la pregunta dada.
        /// Usa coincidencia de palabras clave (simple pero efectivo para manuales).
        /// </summary>
        private string BuscarFragmentosRelevantes(string pregunta)
        {
            if (_chunks.Count == 0) return "";

           
            string[] stopwords = { "el", "la", "los", "las", "un", "una", "de", "en",
                                   "que", "es", "se", "del", "al", "por", "con", "para",
                                   "como", "qué", "cómo", "cuál", "cuáles", "me", "te",
                                   "le", "su", "sus", "mi", "mis", "y", "o", "a" };

            var palabrasClave = pregunta.ToLower()
                .Split(new[] { ' ', ',', '.', '?', '!', ';', ':', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Where(p => p.Length > 2 && !stopwords.Contains(p))
                .Distinct()
                .ToArray();

           
            var puntuados = _chunks
                .Select((chunk, idx) =>
                {
                    string chunkLower = chunk.ToLower();
                    int puntos = palabrasClave.Sum(kw =>
                    {
                        int count = 0;
                        int pos = 0;
                        while ((pos = chunkLower.IndexOf(kw, pos, StringComparison.Ordinal)) != -1)
                        {
                            count++;
                            pos += kw.Length;
                        }
                        return count;
                    });
                    return new { Indice = idx, Chunk = chunk, Puntos = puntos };
                })
                .Where(x => x.Puntos > 0)
                .OrderByDescending(x => x.Puntos)
                .Take(MAX_CHUNKS_POR_CONSULTA)
                .ToList();

            if (puntuados.Count == 0)
            {
               
                return string.Join("\n\n---\n\n", _chunks.Take(MAX_CHUNKS_POR_CONSULTA));
            }

            return string.Join("\n\n---\n\n", puntuados.Select(x => x.Chunk));
        }

      

        /// <summary>
        /// Envía una pregunta al asistente de IA con contexto del manual y la BD.
        /// </summary>
        /// <param name="pregunta">La pregunta del usuario.</param>
        /// <returns>Respuesta del modelo.</returns>
        public async Task<string> ConsultarAsync(string pregunta)
        {
            try
            {
                
                string fragmentosManual = BuscarFragmentosRelevantes(pregunta);
                string contextoBD = ObtenerContextoBD();

               
                string sistemaPrompt = ConstruirSistemaPrompt(fragmentosManual, contextoBD);

             
                _historial.Add(new { role = "user", content = pregunta });

                
                TruncaHistorial();

                
                var mensajes = new List<object>
                {
                    new { role = "system", content = sistemaPrompt }
                };
                mensajes.AddRange(_historial);

               
                var cuerpo = new
                {
                    model = _modelo,
                    messages = mensajes,
                    max_tokens = 1024,
                    temperature = 0.3  
                };

                var json = JsonConvert.SerializeObject(cuerpo);
                var contenido = new StringContent(json, Encoding.UTF8, "application/json");
                var respuesta = await _client.PostAsync(_url, contenido);
                var resultado = await respuesta.Content.ReadAsStringAsync();

              
                if ((int)respuesta.StatusCode == 429)
                {
                    _historial.RemoveAt(_historial.Count - 1); 
                    return "⚠️ Límite de solicitudes alcanzado. Espera unos segundos e intenta de nuevo.";
                }

                if (!respuesta.IsSuccessStatusCode)
                {
                    _historial.RemoveAt(_historial.Count - 1);
                    return $"❌ Error de API ({(int)respuesta.StatusCode}):\n{resultado}";
                }

              
                dynamic data = JsonConvert.DeserializeObject(resultado);
                string textoRespuesta = data.choices[0].message.content.ToString();

              
                _historial.Add(new { role = "assistant", content = textoRespuesta });

                return textoRespuesta;
            }
            catch (TaskCanceledException)
            {
                return "⏱️ La solicitud tardó demasiado. Verifica tu conexión e intenta de nuevo.";
            }
            catch (Exception ex)
            {
                return $"❌ Error inesperado: {ex.Message}";
            }
        }

        /// <summary>Construye el prompt del sistema con el contexto disponible.</summary>
        private string ConstruirSistemaPrompt(string fragmentosManual, string contextoBD)
        {
            var sb = new StringBuilder();

            sb.AppendLine("Eres el asistente inteligente del sistema SG BAMS (Sistema de Gestión de Baterías y Autopartes).");
            sb.AppendLine($"Fecha y hora actual: {DateTime.Now:dd/MM/yyyy HH:mm}");
            sb.AppendLine();
            sb.AppendLine("INSTRUCCIONES:");
            sb.AppendLine("- Responde siempre en español, de forma clara y organizada.");
            sb.AppendLine("- Para preguntas sobre cómo usar el sistema, consulta el manual primero.");
            sb.AppendLine("- Para preguntas sobre datos (ventas, stock, deudas), usa los datos de la BD.");
            sb.AppendLine("- Si no encuentras la información, dilo honestamente.");
            sb.AppendLine("- Sé conciso pero completo. Usa listas cuando sea útil.");
            sb.AppendLine();

            if (!string.IsNullOrEmpty(fragmentosManual))
            {
                sb.AppendLine("═══ MANUAL DEL SISTEMA (secciones relevantes) ═══");
                sb.AppendLine(fragmentosManual);
                sb.AppendLine();
            }

            if (!string.IsNullOrEmpty(contextoBD))
            {
                sb.AppendLine("═══ DATOS ACTUALES DEL SISTEMA ═══");
                sb.AppendLine(contextoBD);
            }

            return sb.ToString();
        }

        /// <summary>Limita el historial para no exceder el contexto del modelo.</summary>
        private void TruncaHistorial()
        {
            int maxMensajes = MAX_HISTORIAL * 2;
            while (_historial.Count > maxMensajes)
                _historial.RemoveAt(0); 
        }

        /// <summary>Limpia el historial de conversación.</summary>
        public void LimpiarHistorial() => _historial.Clear();

       

        /// <summary>Obtiene datos relevantes de la base de datos.</summary>
        private string ObtenerContextoBD()
        {
            var sb = new StringBuilder();

            try
            {
                using (var con = new SqlConnection(_cadenaConexion))
                {
                    con.Open();

                    sb.AppendLine("=== INVENTARIO Y PRODUCTOS ===");
                    sb.AppendLine(EjecutarConsulta(con, "SELECT TOP 10 * FROM Vista_Productos"));

                    sb.AppendLine("=== RANKING DE MÁS VENDIDOS ===");
                    sb.AppendLine(EjecutarConsulta(con, "SELECT TOP 10 * FROM Vista_Productos_Mas_Vendido"));

                    sb.AppendLine("=== ESTADO DE DEUDAS Y SALDOS ===");
                    sb.AppendLine(EjecutarConsulta(con, "SELECT TOP 10 * FROM Vista_Deudas"));
                }
            }
            catch (Exception ex)
            {
                sb.AppendLine($"(No se pudo conectar a la BD: {ex.Message})");
            }

            return sb.ToString();
        }

        /// <summary>Ejecuta una consulta SQL y devuelve el resultado como texto.</summary>
        private string EjecutarConsulta(SqlConnection con, string query)
        {
            var sb = new StringBuilder();

            try
            {
                using (var cmd = new SqlCommand(query, con))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var fila = new StringBuilder();
                        for (int i = 0; i < reader.FieldCount; i++)
                            fila.Append($"{reader.GetName(i)}: {reader[i]}  |  ");
                        sb.AppendLine(fila.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                sb.AppendLine($"(Error al consultar: {ex.Message})");
            }

            return sb.Length > 0 ? sb.ToString() : "(Sin datos)";
        }

   

        /// <summary>
        /// Devuelve estadísticas del manual cargado.
        /// </summary>
        public string ObtenerInfoManual()
        {
            if (!TieneManual) return "No hay manual cargado.";

            int totalChars = _chunks.Sum(c => c.Length);
            return $"📄 Archivo: {NombreManualCargado}\n" +
                   $"📊 Fragmentos: {_chunks.Count}\n" +
                   $"📝 Caracteres totales: {totalChars:N0}";
        }
    }
}