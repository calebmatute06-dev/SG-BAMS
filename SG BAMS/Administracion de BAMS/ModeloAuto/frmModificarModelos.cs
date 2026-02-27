using SG_BAMS.Administracion_de_BAMS.ModeloAuto;
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
    public partial class frmModificarModelos : Form
    {
        int idModeloSeleccionado;
        public frmModificarModelos(int id, string nombreActual)
        {
            InitializeComponent();
            this.idModeloSeleccionado = id;
            txtDescri.Text = nombreActual;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            frmModeloAuto verMauto = new frmModeloAuto();
            verMauto.Show();
            this.Close();
        }

        private async void btnModificar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescri.Text))
            {
                MessageBox.Show("El nombre del modelo no puede estar vacío.");
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                clsModeloAuto objetoModelo = new clsModeloAuto();

                bool exito = await objetoModelo.ModificarModeloAutoAsync(idModeloSeleccionado, txtDescri.Text.Trim());

                if (exito)
                {
                    MessageBox.Show("Modelo actualizado con éxito.", "SG-BAMS");
                    this.DialogResult = DialogResult.OK; 
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
}
