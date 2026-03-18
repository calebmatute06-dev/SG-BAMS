using SG_BAMS.Administracion_de_BAMS.Rol;
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
    public partial class frmModificarRol : Form
    {
        int idRolSeleccionado;
        public frmModificarRol(int id, string nombreActual)
        {
            InitializeComponent();
            this.idRolSeleccionado = id;
            txtDescri.Text = nombreActual;
        }



        private async void btmModificar_Click(object sender, EventArgs e)
        {
            string nombreLimpio = txtDescri.Text.Trim();
            if (string.IsNullOrWhiteSpace(txtDescri.Text))
            {
                MessageBox.Show("El nombre del rol no puede estar vacío.");
                return;
            }

            if (nombreLimpio.Contains("  "))
            {
                MessageBox.Show("El nombre no puede contener dos espacios seguidos.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Regex.IsMatch(nombreLimpio, @"(\w)\1{2,}"))
            {
                MessageBox.Show("No se permite repetir la misma letra más de dos veces seguidas.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (nombreLimpio.Length < 3)
            {
                MessageBox.Show("El nombre debe tener mas de 3 caracteres.", "Error de Longitud", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Regex.IsMatch(nombreLimpio, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ]{2,}(\s[a-zA-ZñÑáéíóúÁÉÍÓÚ]{2,})*$"))
            {
                MessageBox.Show("Escriba un nombre Valido.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                clsRol objetoRol = new clsRol();

                bool exito = await objetoRol.ModificarRolAsync(idRolSeleccionado, txtDescri.Text.Trim());

                if (exito)
                {
                    MessageBox.Show("Rol actualizado con éxito.", "SG-BAMS");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { this.Cursor = Cursors.Default; }
        }

        private void btmSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
