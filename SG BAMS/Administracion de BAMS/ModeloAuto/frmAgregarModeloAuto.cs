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
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 2. Feedback visual al usuario
                this.Cursor = Cursors.WaitCursor;
                btnAgregar.Enabled = false;

                clsModeloAuto objetoModelo = new clsModeloAuto();

                // 3. Ejecución de la lógica
                bool exito = await objetoModelo.InsertarModeloAutoAsync(txtDescri.Text.Trim());

                if (exito)
                {
                    MessageBox.Show("Modelo de auto agregado con éxito.", "SG-BAMS",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 4. Cerrar con éxito para que el padre refresque el Grid
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
