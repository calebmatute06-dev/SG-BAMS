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

            // Configuración de Tema
            chkModoOscuro.Checked = ClsTemas.modoOscuroEnabled;
            ClsTemas.AplicarTema(this);

            // Sincronizar ComboBox con el factor actual (progresivo)
            // Esto asegura que si el zoom es 1.1f, el combo marque "110%"
            string zoomTexto = Math.Round(Config_Sistema.FactorZoom * 100).ToString() + "%";

            if (cmbZoom.Items.Contains(zoomTexto))
            {
                cmbZoom.SelectedItem = zoomTexto;
            }
            else
            {
                // Si el valor no está en la lista (por ejemplo al inicio), ponemos 100%
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
                // 1. Guardar el nuevo factor de zoom en el archivo config
                Config_Sistema.FactorZoom = porcentaje / 100f;
                Config_Sistema.GuardarConfiguracion();

                // 2. Solo informar al usuario (sin botones de Sí/No, solo OK)
                MessageBox.Show(
                    "El nivel de zoom se ha guardado. Los cambios se aplicarán por completo al iniciar la próxima sesión.",
                    "Configuración Guardada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void btnsalirLogin_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void kryptonCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            ClsTemas.GuardarPreferencia(chkModoOscuro.Checked);
            ClsTemas.AplicarTema(this);
        }
    }
}