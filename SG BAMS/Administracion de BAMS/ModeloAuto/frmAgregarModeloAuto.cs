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
using System.Windows.Media.TextFormatting;

namespace SG_BAMS
{
    public partial class frmAgregarModeloAuto : Form
    {
        public frmAgregarModeloAuto()
        {
            InitializeComponent();
            txtDescri.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
        }


        private async void btnAgregar_Click_1(object sender, EventArgs e)
        {
            
            if (!ClsValidaciones.EsNombrePersonalValido(txtDescri.TextBox, "Nombre del Modelo de Auto"))
            {
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnAgregar1.Enabled = false;

                clsModeloAuto objetoModelo = new clsModeloAuto();

                
                bool exito = await objetoModelo.InsertarModeloAutoAsync(txtDescri.Text.Trim());

                if (exito)
                {
                    MessageBox.Show("Modelo de auto agregado con éxito.", "SG-BAMS",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de sistema: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btnAgregar1.Enabled = true;
            }
        }

        private void btnSalir_Click_1(object sender, EventArgs e)
        {
            frmModeloAuto verMauto = new frmModeloAuto();
            verMauto.Show();
            this.Close();
        }
    }
}
