using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SG_BAMS
{
    public partial class AgregarProveedor : Form
    {
        public ClsProveedor InfoProveedor { get; set; }
        public AgregarProveedor()
        {
            InitializeComponent();
        }


        private void AgregarProveedor_Load(object sender, EventArgs e)
        {

        }

        private void kryptonButton20_Click(object sender, EventArgs e)
        {
            InfoProveedor = new ClsProveedor();

            InfoProveedor.ID = txtID.Text;
            InfoProveedor.Nombre = txtNombre.Text;
            InfoProveedor.Telefono = txtTelefono.Text;
            InfoProveedor.Direccion = txtDireccion.Text;
            InfoProveedor.RTN = txtRTN.Text;
            InfoProveedor.Clasificacion = cmbClasificacion.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // Avisamos que se canceló la operación
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cmbClasificacion_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
