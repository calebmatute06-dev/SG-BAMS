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

        public Pago_Deuda()
        {
            InitializeComponent();
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
    }


}

