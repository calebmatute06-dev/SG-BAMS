using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS
{
    internal class ClsServicioAyudaIA
    {
        private readonly HttpClient _client;
        private readonly string _apiKey = "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"; 

        private readonly string _url = "https://api.groq.com/openai/v1/chat/completions";

        private readonly string _cadenaConexion = "Data Source = AutoBattDB.mssql.somee.com; " +
                                                   "Initial catalog = AutoBattDB; " +
                                                   "User ID = exobonnie_SQLLogin_1; " +
                                                   "Password = w6et2uoghs;" +
                                                   "TrustServerCertificate=True;";

        public ClsServicioAyudaIA()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
        }

        public async Task<string> ConsultarAsync(string pregunta)
        {
            try
            {
                string contexto = ObtenerContextoBD();

                string prompt = $@"
                Eres el asistente del sistema SG BAMS.
                Fecha actual: {DateTime.Now:dd/MM/yyyy}

                Datos:{contexto}

                Pregunta: {pregunta}
                Responde claro y en español.";

                var body = new
                {
                    model = "llama-3.1-8b-instant", 
                    messages = new[]
                {
                    new { role = "user", content = prompt }
                }
                };

                var json = JsonConvert.SerializeObject(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync(_url, content);
                var result = await response.Content.ReadAsStringAsync();

               
                if ((int)response.StatusCode == 429)
                    return "⚠️ Límite alcanzado. Espera un momento para seguir usando la IA.";

                if (!response.IsSuccessStatusCode)
                    return "Error IA:\n" + result;

                dynamic data = JsonConvert.DeserializeObject(result);

                return data.choices[0].message.content.ToString();
            }
            catch (Exception ex)
            {
                return "Error: " + ex.Message;
            }
        }
        private string ObtenerContextoBD()
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                using (SqlConnection con = new SqlConnection(_cadenaConexion))
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
                sb.AppendLine($"Error al conectar con la base de datos: {ex.Message}");
            }

            return sb.ToString();
        }


        private string EjecutarConsulta(SqlConnection con, string query)
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    StringBuilder fila = new StringBuilder();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        fila.Append($"{reader.GetName(i)}: {reader[i]}  |  ");
                    }
                    sb.AppendLine(fila.ToString());
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                sb.AppendLine($"(Error al leer: {ex.Message})");
            }

            return sb.Length > 0 ? sb.ToString() : "(Sin datos)";
        }
    }
}