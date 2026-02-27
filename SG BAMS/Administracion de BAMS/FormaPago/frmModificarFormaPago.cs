using SG_BAMS.Administracion_de_BAMS.FormaPago;
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
    public partial class frmModificarFormaPago : Form
    {
        private int _idFormaPago;
        public frmModificarFormaPago(int id, string descripcionActual)
        {
            InitializeComponent();
            this._idFormaPago = id;
            txtDescri.Text = descripcionActual;
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {

        }

        private async void btnModificar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescri.Text))
            {
                MessageBox.Show("Escriba una descripción válida.");
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                clsFormaPago objetoFP = new clsFormaPago();

                // Llamamos a la clase de datos
                bool exito = await objetoFP.ModificarFormaPagoAsync(_idFormaPago, txtDescri.Text.Trim());

                if (exito)
                {
                    MessageBox.Show("Actualizado correctamente.");
                    this.DialogResult = DialogResult.OK; // <-- CRUCIAL para avisar al padre
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }
    }

