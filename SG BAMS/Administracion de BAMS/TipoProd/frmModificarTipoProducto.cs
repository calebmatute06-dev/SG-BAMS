using SG_BAMS.Administracion_de_BAMS.TipoProd;
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
    public partial class frmModificarTipoProducto : Form
    {
        int idSeleccionado;
        public frmModificarTipoProducto(int id, string descripcionActual)
        {
            InitializeComponent();
            idSeleccionado = id;
            txtDescri.Text = descripcionActual;

            txtDescri.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
        }



        private void frmModificarTipoProducto_Load(object sender, EventArgs e)
        {

        }

        private async void btnModificar_Click(object sender, EventArgs e)
        {
            
            if (!ClsValidaciones.EsNombrePersonalValido(txtDescri.TextBox, "Tipo de Producto"))
            {
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnModificar.Enabled = false;

                clsTipoProducto objetoTipo = new clsTipoProducto();

                
                bool exito = await objetoTipo.ModificarTipoProductoAsync(idSeleccionado, txtDescri.Text.Trim());

                if (exito)
                {
                    MessageBox.Show("Tipo de producto actualizado correctamente.", "SG-BAMS",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar: " + ex.Message, "Error de Sistema",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
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
