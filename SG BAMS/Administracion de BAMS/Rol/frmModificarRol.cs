using SG_BAMS.Administracion_de_BAMS.Rol;
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
            if (string.IsNullOrWhiteSpace(txtDescri.Text))
            {
                MessageBox.Show("El nombre del rol no puede estar vacío.");
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
                    this.DialogResult = DialogResult.OK; // Indica éxito para recargar el Grid
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
