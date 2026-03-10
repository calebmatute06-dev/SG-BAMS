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
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            frmFormaPago frmFormaPago = new frmFormaPago();
            frmFormaPago.Show();
            this.Close();
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtdescri.Text))
            {
                MessageBox.Show("Por favor, ingrese el nombre de la marca.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtdescri.Text.Length < 3)
            {
                MessageBox.Show("El nombre debe tener mas de 3 caracteres.", "Error de Longitud", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtdescri.Text.Contains("  "))
            {
                MessageBox.Show("El nombre no puede contener dos espacios seguidos.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Regex.IsMatch(txtdescri.Text, @"(\w)\1{2,}"))
            {
                MessageBox.Show("No se permite repetir la misma letra más de dos veces seguidas.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (!Regex.IsMatch(txtdescri.Text, @"^[a-zA-Z \s & ñ Ñ @,.;:<>]+$"))
            {
                MessageBox.Show("El nombre solo debe contener caracteres validos.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    frmFormaPago verFpago = new frmFormaPago();
                    verFpago.Show();
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
    }
}
