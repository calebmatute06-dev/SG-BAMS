using SG_BAMS.Administracion_de_BAMS.ModeloAuto;
using System;
using System.Windows.Forms;

namespace SG_BAMS
{
    public partial class frmAgregarModeloAuto : Form
    {
        public frmAgregarModeloAuto()
        {
            InitializeComponent();
            
            txtDescri.KeyPress += (s, e) => ClsValidaciones.PermitirAlfanumerico(e);
        }

        private async void btnAgregar_Click_1(object sender, EventArgs e)
        {
            
            if (!ClsValidaciones.EsAlfanumericoValido(txtDescri, "Nombre del Modelo de Auto"))
            {
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;

                
                if (btnAgregar != null) btnAgregar.Enabled = false;

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
                if (btnAgregar != null) btnAgregar.Enabled = true;
            }
        }

        private void btnSalir_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}