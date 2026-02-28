using SG_BAMS.Administracion_de_BAMS.Estado;
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
                MessageBox.Show("La descripción no puede estar vacía.");
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
