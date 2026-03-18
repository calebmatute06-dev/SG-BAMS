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
            string nombreLimpio = txtDescri.Text.Trim();
            if (string.IsNullOrWhiteSpace(nombreLimpio))
            {
                MessageBox.Show("El nombre de la marca no puede estar vacío.");
                return;
            }

            if (nombreLimpio.Length < 3)
            {
                MessageBox.Show("El nombre debe tener al menos 3 caracteres.", "Error de Longitud", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string[] partes = nombreLimpio.Split(' ');
            if (partes.Any(p => p.Length < 2))
            {
                MessageBox.Show("Cada palabra en el nombre debe tener al menos 2 caracteres y no se permiten espacios dobles.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string patron = @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ]+(\s[a-zA-ZñÑáéíóúÁÉÍÓÚ]+)*$";
            if (!Regex.IsMatch(nombreLimpio, patron))
            {
                MessageBox.Show("Formato inválido. No se permiten números, símbolos ni espacios dobles.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string sinEspacios = nombreLimpio.Replace(" ", "");
            if (Regex.IsMatch(sinEspacios, @"(.)\1{2,}"))
            {
                MessageBox.Show("No se permiten caracteres repetidos más de dos veces seguidas.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnModificar.Enabled = false;

                clsMarca objetoMarca = new clsMarca();
                bool exito = await objetoMarca.ModificarMarcaAsync(idMarca, nombreLimpio);

                if (exito)
                {
                    MessageBox.Show("Marca actualizada correctamente.", "SG-BAMS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar: " + ex.Message, "Error de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    

