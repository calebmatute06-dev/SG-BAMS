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
using System.Drawing;


namespace SG_BAMS
{
    public partial class Ajustes : Form
    {
        // Variable para evitar que el mensaje salga al inicializar
        private bool cargado = false;

        public Ajustes()
        {
            InitializeComponent();

            chkModoOscuro.Checked = ClsTemas.modoOscuroEnabled;
            ClsTemas.AplicarTema(this);

            // Sincronizamos el ComboBox sin disparar el mensaje todavía
            string zoomTexto = (Config_Sistema.FactorZoom * 100).ToString() + "%";

            if (cmbZoom.Items.Contains(zoomTexto))
            {
                cmbZoom.SelectedItem = zoomTexto;
            }
            else
            {
                cmbZoom.SelectedIndex = 0;
            }

            // Marcamos que el formulario terminó de configurar sus controles iniciales
            cargado = true;
        }

        private void Ajustes_Load(object sender, EventArgs e)
        {
            // ÚNICA ADICIÓN: Aplicar el zoom a esta ventana al abrirse
            Ayudante_UI.AplicarZoomGlobal(this);
        }

        private void kryptonComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Si el formulario no ha terminado de cargar, no ejecutes el código
            if (!cargado) return;

            string valorSeleccionado = cmbZoom.Text.Replace("%", "");

            if (float.TryParse(valorSeleccionado, out float porcentaje))
            {
                Config_Sistema.FactorZoom = porcentaje / 100f;

                MessageBox.Show("Configuración guardada. El zoom se aplicará en las nuevass al reiniciar.",
                                "Ajustes", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
