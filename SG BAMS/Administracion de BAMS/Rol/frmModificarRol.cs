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

            txtDescri.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
        
        }



        private async void btmModificar_Click(object sender, EventArgs e)
        {
            
            if (!ClsValidaciones.EsNombrePersonalValido(txtDescri.TextBox, "Nombre del Rol"))
            {
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btmModificar.Enabled = false;

                clsRol objetoRol = new clsRol();

                
                bool exito = await objetoRol.ModificarRolAsync(idRolSeleccionado, txtDescri.Text.Trim());

                if (exito)
                {
                    MessageBox.Show("Rol actualizado con éxito.", "SG-BAMS",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btmModificar.Enabled = true;
            }
        }

        private void btmSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
