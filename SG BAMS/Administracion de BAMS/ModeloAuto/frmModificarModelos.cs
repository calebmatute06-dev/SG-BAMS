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
        }

        private async void btnModificar_Click_1(object sender, EventArgs e)
        {
            string nombreLimpio = txtDescri.Text.Trim();
            if (string.IsNullOrWhiteSpace(nombreLimpio))
            {
                MessageBox.Show("El nombre del modelo no puede estar vacío.");
                return;
            }

            if (nombreLimpio.Length < 3)
            {
                MessageBox.Show("El nombre debe tener al menos 3 caracteres.", "Error de Longitud", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string[] partes = nombreLimpio.Split(' ');
            if (partes.Any(p => p.Length < 2))
            {
                MessageBox.Show("Cada palabra en el nombre debe tener al menos 2 caracteres y no se permiten espacios dobles.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string patron = @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ]+(\s[a-zA-ZñÑáéíóúÁÉÍÓÚ]+)*$";
            if (!Regex.IsMatch(nombreLimpio, patron))
            {
                MessageBox.Show("El nombre solo debe contener letras. No se permiten números, símbolos ni espacios dobles.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string sinEspacios = nombreLimpio.Replace(" ", "");
            if (Regex.IsMatch(sinEspacios, @"(.)\1{2,}"))
            {
                MessageBox.Show("No se permiten caracteres repetidos más de dos veces seguidas.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnModificar.Enabled = false;

                clsModeloAuto objetoModelo = new clsModeloAuto();
                bool exito = await objetoModelo.ModificarModeloAutoAsync(idModeloSeleccionado, nombreLimpio);

                if (exito)
                {
                    MessageBox.Show("Modelo actualizado con éxito.", "SG-BAMS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar: " + ex.Message, "Error de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
