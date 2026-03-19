using SG_BAMS.Administracion_de_BAMS.Estado;
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
    public partial class frmAgregarEstado : Form
    {
        public frmAgregarEstado()
        {
            InitializeComponent();
            this.txtDescri.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescri_KeyPress);
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {

        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtDescri_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            ClsValidaciones.PermitirSoloLetras(e);
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!ClsValidaciones.EsNombrePersonalValido(txtDescri, "Descripción del Estado"))
            {
                return; 
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnAgregar.Enabled = false;

                clsEstado objetoEstado = new clsEstado();

                
                bool exito = await objetoEstado.InsertarEstadoAsync(txtDescri.Text.Trim());

                if (exito)
                {
                    MessageBox.Show("Estado registrado correctamente.", "SG-BAMS",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btnAgregar.Enabled = true;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
