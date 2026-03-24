using SG_BAMS.Administracion_de_BAMS.FormaPago;
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
    public partial class frmAgregarFormaPago : Form
    {
        public frmAgregarFormaPago()
        {
            InitializeComponent();
            
            txtdescri.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            
            if (!ClsValidaciones.EsNombrePersonalValido(txtdescri, "Descripción de Forma de Pago"))
            {
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnAgregar.Enabled = false;

                clsFormaPago objetoFP = new clsFormaPago();

               
                bool insertado = await objetoFP.InsertarFormaPagoAsync(txtdescri.Text.Trim());

                if (insertado)
                {
                    MessageBox.Show("Forma de pago agregada correctamente.", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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