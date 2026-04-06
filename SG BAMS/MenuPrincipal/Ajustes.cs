using SG_BAMS.MenuPrincipal;
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
    public partial class Ajustes : Form
    {
        private bool cargado = false;

        public Ajustes()
        {
            InitializeComponent();



           
            string zoomTexto = Math.Round(Config_Sistema.FactorZoom * 100).ToString() + "%";

            if (cmbZoom.Items.Contains(zoomTexto))
            {
                cmbZoom.SelectedItem = zoomTexto;
            }
            else
            {
                
                cmbZoom.SelectedIndex = cmbZoom.FindString("100%");
            }

            cargado = true;
        }

        private void Ajustes_Load(object sender, EventArgs e)
        {
            Ayudante_UI.AplicarZoomGlobal(this);
        }

        private void kryptonComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cargado) return;

            string textoLimpio = cmbZoom.Text.Replace("%", "");

            if (float.TryParse(textoLimpio, out float porcentaje))
            {
                
                Config_Sistema.FactorZoom = porcentaje / 100f;
                Config_Sistema.GuardarConfiguracion();

               
                MessageBox.Show(
                    "El nivel de zoom se ha guardado. Los cambios se aplicarán por completo al iniciar la próxima sesión.",
                    "Configuración Guardada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void btnsalir1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}