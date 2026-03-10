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
        }



        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescri.Text))
            {
                MessageBox.Show("Por favor, ingrese el nombre del modelo de auto.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtDescri.Text.Length < 3)
            {
                MessageBox.Show("El nombre debe tener mas de 3 caracteres.", "Error de Longitud", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtDescri.Text.Contains("  "))
            {
                MessageBox.Show("El nombre no puede contener dos espacios seguidos.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Regex.IsMatch(txtDescri.Text, @"(\w)\1{2,}"))
            {
                MessageBox.Show("No se permite repetir la misma letra más de dos veces seguidas.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Regex.IsMatch(txtDescri.Text, @"^[a-zA-Z \s & ñ Ñ @,.;:<> 0-9]+$"))
            {
                MessageBox.Show("El nombre solo debe contener caracteres validos.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnAgregar.Enabled = false;

                clsModeloAuto objetoModelo = new clsModeloAuto();

                bool exito = await objetoModelo.InsertarModeloAutoAsync(txtDescri.Text.Trim());

                if (exito)
                {
                    MessageBox.Show("Modelo de auto agregado con éxito.", "SG-BAMS",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    frmModeloAuto verMAuto = new frmModeloAuto();
                    verMAuto.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btnAgregar.Enabled = true;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            frmModeloAuto verMauto = new frmModeloAuto();
            verMauto.Show();
            this.Close();
        }
    }
}
