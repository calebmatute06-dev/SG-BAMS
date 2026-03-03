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
            if (string.IsNullOrWhiteSpace(txtDescri.Text))
            {
                MessageBox.Show("Por favor complete los campos obligatorios.");
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

