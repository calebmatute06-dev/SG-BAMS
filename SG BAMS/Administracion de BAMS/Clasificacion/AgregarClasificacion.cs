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

namespace SG_BAMS.Administracion_de_BAMS.Clasificacion
{
    public partial class AgregarClasificacion : Form
    {
        public AgregarClasificacion()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            txtDescri.KeyPress += (s, e) => ClsValidaciones.PermitirAlfanumerico(e);
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!ClsValidaciones.EsAlfanumericoValido(txtDescri, "Clasificación"))
            {
                return;
            }

            if (Regex.IsMatch(txtDescri.Text.Trim(), @"(?i)\b(?![yY]\b)[a-zñáéíóú]\b"))
            {
                MessageBox.Show("No se permiten letras aisladas en el nombre (excepto la 'y').",
                                "Formato Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescri.Focus();
                return;
            }

            if (!ClsValidaciones.ValidarNombreUnico(
                    control: txtDescri,
                    tabla: "clasificacion_proveedor",
                    columnaNombre: "clasificacion_proveedor",
                    nombreCampo: "Clasificación",
                    idExcluir: 0,
                    idColumna: "id_clasificacion_proveedor"))
            {
                return;
            }
            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnAgregar.Enabled = false;

                clsClasificacion objetoCla = new clsClasificacion();

                bool exito = await objetoCla.InsertarClasificacionAsync(txtDescri.Text.Trim());

                if (exito)
                {
                    MessageBox.Show("Clasificación registrada con éxito.", "SG-BAMS",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error de Sistema",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
