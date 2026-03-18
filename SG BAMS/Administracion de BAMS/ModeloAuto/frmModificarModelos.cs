using SG_BAMS.Administracion_de_BAMS.ModeloAuto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

            txtDescri.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras( e);
        }

        private async void btnModificar_Click_1(object sender, EventArgs e)
        {
           
            if (!ClsValidaciones.EsNombrePersonalValido(txtDescri.TextBox, "Nombre del Modelo"))
            {
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnModificar.Enabled = false;

                clsModeloAuto objetoModelo = new clsModeloAuto();

                
                bool exito = await objetoModelo.ModificarModeloAutoAsync(idModeloSeleccionado, txtDescri.Text.Trim());

                if (exito)
                {
                    MessageBox.Show("Modelo actualizado con éxito.", "SG-BAMS",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar: " + ex.Message, "Error de Sistema",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btnModificar.Enabled = true;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
