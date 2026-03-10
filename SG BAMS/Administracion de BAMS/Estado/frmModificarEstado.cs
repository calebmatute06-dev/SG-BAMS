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
    public partial class frmModificarEstado : Form
    {
        int idEstado;
        public frmModificarEstado(int id, string descripcionActual)
        {
            InitializeComponent();
            this.idEstado = id;
            txtDescri.Text = descripcionActual;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnModificar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescri.Text))
            {
                MessageBox.Show("Por favor, ingrese el nombre de la marca.",
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


            if (!Regex.IsMatch(txtDescri.Text, @"^[a-zA-Z \s & ñ Ñ @,.;:<>]+$"))
            {
                MessageBox.Show("El nombre solo debe contener caracteres validos.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                clsEstado objetoEstado = new clsEstado();

                bool exito = await objetoEstado.ModificarEstadoAsync(idEstado, txtDescri.Text.Trim());

                if (exito)
                {
                    MessageBox.Show("Estado actualizado con éxito.", "SG-BAMS");
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
