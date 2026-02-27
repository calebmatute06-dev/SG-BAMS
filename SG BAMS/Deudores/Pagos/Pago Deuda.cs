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
    public partial class Pago_Deuda : Form
    {

        private ClsDeudas objetoDeudas = new ClsDeudas();
        private string nombreRecibido = "";

        public Pago_Deuda()
        {
            InitializeComponent();
            ConfigurarFormulario();
        }

        public Pago_Deuda(string nombreDesdeBuscador)
        {
            InitializeComponent();
            this.nombreRecibido = nombreDesdeBuscador;
            ConfigurarFormulario();
        }


        private void ConfigurarFormulario()
        {
            DataTable dt = objetoDeudas.ObtenerDeudoresActivos();

            if (dt != null && dt.Rows.Count > 0)
            {
                cmbDeudores.DataSource = dt;
                cmbDeudores.DisplayMember = "Cliente"; // Alias de tu vista
                cmbDeudores.ValueMember = "ID";      // Alias de mi SELECT en la clase
                cmbDeudores.SelectedIndex = -1;
            }
        }




        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void kryptonButton3_Click(object sender, EventArgs e)
        {

            if (cmbDeudores.SelectedValue != null && decimal.TryParse(txtMonto.Text, out decimal monto))
            {
                int idDeuda = (int)cmbDeudores.SelectedValue;
                bool ok = await objetoDeudas.InsertarPago(idDeuda, monto, DateTime.Now);

                if (ok)
                {
                    MessageBox.Show("Pago procesado");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }


        }

        private void Pago_Deuda_Load(object sender, EventArgs e)
        {
            // Si recibimos un nombre desde la Factura...
            if (!string.IsNullOrEmpty(nombreRecibido))
            {
                // Buscamos el índice exacto o parcial en el ComboBox
                int index = cmbDeudores.FindStringExact(nombreRecibido);

                // Si no lo encuentra exacto, buscamos parcial
                if (index == -1) index = cmbDeudores.FindString(nombreRecibido);

                if (index != -1)
                {
                    cmbDeudores.SelectedIndex = index;
                    // Opcional: Bloqueamos el combo para que no lo cambien por error 
                    // si vienes directamente de la factura
                    // cmbDeudores.Enabled = false; 
                }
            }
        }
    }


}

