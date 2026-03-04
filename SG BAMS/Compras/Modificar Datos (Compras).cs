using System;
using System.Windows.Forms;

namespace SG_BAMS
{
    public partial class Modificar_Datos__Compras_ : Form
    {
        // Esta variable recibe el ID desde el formulario principal
        public int idCompraRecibida;

        public Modificar_Datos__Compras_()
        {
            InitializeComponent();
        }

        private void Modificar_Datos__Compras__Load(object sender, EventArgs e)
        {
            
        }

        // Cierra la ventana de visualización
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Cierra la ventana de visualización
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
    }
}