using SG_BAMS.Administracion_de_BAMS.MarcaProd;
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
    public partial class frmIngresarMarcaProducto : Form
    {
        public frmIngresarMarcaProducto()
        {
            InitializeComponent();
        }

        private void kryptonTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void btmAgregar_Click(object sender, EventArgs e)
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
                btmAgregar.Enabled = false;

                clsMarca objetoMarca = new clsMarca();

                bool exito = await objetoMarca.InsertarMarcaAsync(txtDescri.Text.Trim());

                if (exito)
                {
                    MessageBox.Show("Marca agregada con éxito.", "SG-BAMS",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK; 
                    frmMarcaProductos verMproductos = new frmMarcaProductos();
                    verMproductos.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btmAgregar.Enabled = true;
            }
        }

        private void btmSalir_Click(object sender, EventArgs e)
        {
            frmMarcaProductos verMproducto = new frmMarcaProductos();
            verMproducto.Show();
            this.Close();
        }
    }
}
