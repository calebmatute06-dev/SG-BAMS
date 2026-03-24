using SG_BAMS.Administracion_de_BAMS.TipoProd;
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
    public partial class frnAgregarTipoProducto : Form
    {
        public frnAgregarTipoProducto()
        {
            InitializeComponent();

            
            txtDescri.KeyPress += (s, e) => ClsValidaciones.PermitirAlfanumerico(e);
        }

        private async void btnAgregar_Click_1(object sender, EventArgs e)
        {
            
            if (!ClsValidaciones.EsAlfanumericoValido(txtDescri, "Tipo de Producto"))
            {
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnAgregar.Enabled = false;

                clsTipoProducto objetoTipo = new clsTipoProducto();

                
                bool exito = await objetoTipo.InsertarTipoProductoAsync(txtDescri.Text.Trim());

                if (exito)
                {
                    MessageBox.Show("Tipo de producto registrado con éxito.", "SG-BAMS",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK; 
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error de Sistema",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btnAgregar.Enabled = true;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }
    }
}