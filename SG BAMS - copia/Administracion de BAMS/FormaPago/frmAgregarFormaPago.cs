using SG_BAMS.Administracion_de_BAMS.FormaPago;
using System;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Representa la ventana para agregar una nueva forma de pago al sistema.
    /// </summary>
    public partial class frmAgregarFormaPago : Form
    {
        private PlaceholderTextBox phDescri;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="frmAgregarFormaPago"/>.
        /// </summary>
        public frmAgregarFormaPago()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            txtdescri.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
            phDescri = new PlaceholderTextBox(txtdescri, "Ingrese el nuevo metodo de pago");
        }

        private void frmAgregarFormaPago_Load(object sender, EventArgs e)
        {
            phDescri = new PlaceholderTextBox(txtdescri, "Ingrese el nuevo metodo de pago");
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            
            string descripcionReal = phDescri.GetRealValue().Trim();

            
            using (var temp = new TextBox { Text = descripcionReal })
            {
                if (!ClsValidaciones.EsNombrePersonalValido(temp, "Descripción de Forma de Pago"))
                    return;
            }

            
            using (var temp = new TextBox { Text = descripcionReal })
            {
                if (!ClsValidaciones.ValidarNombreUnico(
                        control: temp,
                        tabla: "Tipo_Forma_de_pago",
                        columnaNombre: "descripcion_forma_pago",
                        nombreCampo: "Tipo de Forma de Pago",
                        idExcluir: 0,
                        idColumna: "id_tipo_forma_pago"))
                    return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnAgregar.Enabled = false;

                clsFormaPago objetoFP = new clsFormaPago();
                bool insertado = await objetoFP.InsertarFormaPagoAsync(descripcionReal);

                if (insertado)
                {
                    MessageBox.Show("Forma de pago agregada correctamente.", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
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
                btnAgregar.Enabled = true;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label5_Click(object sender, EventArgs e) { }
    }
}