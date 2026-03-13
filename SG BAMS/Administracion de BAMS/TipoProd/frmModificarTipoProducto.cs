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
        }



        private void frmModificarTipoProducto_Load(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private async void btnModificar_Click(object sender, EventArgs e)
        {

            string nombreLimpio = txtDescri.Text.Trim();
            if (string.IsNullOrWhiteSpace(txtDescri.Text))
            {
                MessageBox.Show("Debe ingresar una descripción para el tipo de producto.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (nombreLimpio.Contains("  "))
            {
                MessageBox.Show("El nombre no puede contener dos espacios seguidos.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Regex.IsMatch(nombreLimpio, @"(\w)\1{2,}"))
            {
                MessageBox.Show("No se permite repetir la misma letra más de dos veces seguidas.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (nombreLimpio.Length < 3)
            {
                MessageBox.Show("El nombre debe tener mas de 3 caracteres.", "Error de Longitud", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Regex.IsMatch(nombreLimpio, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ]{2,}(\s[a-zA-ZñÑáéíóúÁÉÍÓÚ]{2,})*$"))
            {
                MessageBox.Show("El campos de Nombre solo deben contener caracteres validos.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            try
            {
                clsTipoProducto objetoTipo = new clsTipoProducto();
                bool exito = await objetoTipo.ModificarTipoProductoAsync(idSeleccionado, txtDescri.Text.Trim());

                if (exito)
                {
                    MessageBox.Show("Actualizado correctamente");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
    }

