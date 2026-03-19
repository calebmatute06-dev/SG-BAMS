using SG_BAMS.Administracion_de_BAMS.MarcaProd;
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
    public partial class frmModificarMarcaProducto : Form
    {
        int idMarca;
        public frmModificarMarcaProducto(int id, string nombreActual)
        {

            InitializeComponent();
            this.idMarca = id;
            txtDescri.Text = nombreActual;

            txtDescri.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);

        }

        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            frmMarcaProductos verMproducto = new frmMarcaProductos();
            verMproducto.Show();
            this.Close();
        }

        private void frmModificarMarcaProducto_Load(object sender, EventArgs e)
        {

        }

        private async void btnModificar_Click(object sender, EventArgs e)
        {
            
            if (!ClsValidaciones.EsNombrePersonalValido(txtDescri.TextBox, "Nombre de la Marca"))
            {
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnModificar.Enabled = false;

                clsMarca objetoMarca = new clsMarca();

                
                bool exito = await objetoMarca.ModificarMarcaAsync(idMarca, txtDescri.Text.Trim());

                if (exito)
                {
                    MessageBox.Show("Marca actualizada correctamente.", "SG-BAMS",
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
    

