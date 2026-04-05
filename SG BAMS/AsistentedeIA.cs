using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS
{
    public partial class AsistentedeIA : Form
    {
        private ClsServicioAyudaIA _servicioIA = new ClsServicioAyudaIA();

        public AsistentedeIA()
        {
            InitializeComponent();
            MostrarBienvenida();
        }

        private void MostrarBienvenida()
        {
            lstIA.Items.Add("🤖 Asistente: ¡Hola! Soy tu asistente de SG BAMS.");
            lstIA.Items.Add("🤖 Asistente: ¿En qué puedo ayudarte hoy?");
            lstIA.Items.Add("*********************************************");
            lstIA.Items.Add("📊 Resúmenes: Pregunta por lo más vendido o el stock.");
            lstIA.Items.Add("💰 Cuentas: Consulta quién debe y cuánto es el saldo.");
            lstIA.Items.Add("📦 Productos: Busca precios, marcas y modelos de auto.");
            lstIA.Items.Add("*********************************************");
            lstIA.Items.Add("💡 Prueba diciendo: '¿Qué productos se venden más?'");
        }

        private async Task EnviarMensaje()
        {
            string pregunta = txtInfo.Text.Trim();
            if (string.IsNullOrEmpty(pregunta)) return;

            lstIA.Items.Add("*********************************************");
            lstIA.Items.Add("👤 Tú: " + pregunta);
            lstIA.Items.Add("⏳ Asistente escribiendo...");
            lstIA.TopIndex = lstIA.Items.Count - 1;

            txtInfo.Clear();
            btnEnviar.Enabled = false;

            try
            {
                string respuesta = await _servicioIA.ConsultarAsync(pregunta);

              
                if (lstIA.Items.Count > 0)
                    lstIA.Items.RemoveAt(lstIA.Items.Count - 1);

                lstIA.Items.Add("🤖 Asistente:");

                int limiteCaracteres = 100;
                string[] lineasOriginales = respuesta.Split(new[] { "\n", "\r\n" }, StringSplitOptions.None);

                foreach (string linea in lineasOriginales)
                {
                    string textoRestante = linea.Trim();

                    if (string.IsNullOrEmpty(textoRestante))
                    {
                        lstIA.Items.Add(""); 
                        continue;
                    }

                    while (textoRestante.Length > limiteCaracteres)
                    {
                       
                        int puntoDeCorte = textoRestante.LastIndexOf(' ', limiteCaracteres);

                       
                        if (puntoDeCorte <= 0) puntoDeCorte = limiteCaracteres;

                        lstIA.Items.Add("   " + textoRestante.Substring(0, puntoDeCorte).Trim());
                        textoRestante = textoRestante.Substring(puntoDeCorte).Trim();
                    }

                  
                    if (!string.IsNullOrEmpty(textoRestante))
                        lstIA.Items.Add("   " + textoRestante);
                }
            }
            catch (Exception ex)
            {
                if (lstIA.Items.Count > 0)
                    lstIA.Items.RemoveAt(lstIA.Items.Count - 1);
                lstIA.Items.Add("⚠️ Error: " + ex.Message);
            }
            finally
            {
                btnEnviar.Enabled = true;
                lstIA.TopIndex = lstIA.Items.Count - 1;
            }
        }

        private async void btnEnviar_Click(object sender, EventArgs e)
        {
            await EnviarMensaje();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            lstIA.Items.Clear();
            txtInfo.Clear();
            MostrarBienvenida();
        }

        private async void txtInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; 
                await EnviarMensaje();
            }
        }

        private void AsistentedeIA_Load(object sender, EventArgs e)
        {
            
            lstIA.HorizontalScrollbar = false;
        }
    }
}