using SG_BAMS.Administracion_de_BAMS.Usuarios;
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
using static Azure.Core.HttpHeader;

namespace SG_BAMS
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class frmAgregarUsuarios : Form
    {
        /// <summary>
        /// The dt roles
        /// </summary>
        private DataTable dtRoles;
        /// <summary>
        /// Initializes a new instance of the <see cref="frmAgregarUsuarios" /> class.
        /// </summary>
        public frmAgregarUsuarios()
        {
            InitializeComponent();
            CargarComboRoles();
            cmbRol.DropDownStyle = ComboBoxStyle.DropDownList;

            txtNombre.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
        }

        /// <summary>
        /// Handles the Load event of the fmrAgregarUsuarios control.
        /// </summary>
        private async Task CargarComboRoles()
        {
            try
            {
                clsUsuario objetoUsuario = new clsUsuario();
                DataTable dt = await objetoUsuario.ListarRolesAsync();
                cmbRol.SelectedIndexChanged -= cmbRol_SelectedIndexChanged;

                cmbRol.DataSource = dt;
                cmbRol.DisplayMember = "descripcion_rol";
                cmbRol.ValueMember = "id_rol_usuario";
                cmbRol.SelectedIndex = -1;

                cmbRol.SelectedIndexChanged += cmbRol_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Handles the Click event of the label2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private List<string> listaOriginalRoles = new List<string>();



        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbRol control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void cmbRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Handles the TextChanged event of the txtNombre control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the btmModificar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private async void btmModificar_Click(object sender, EventArgs e)
        {
            if (!ClsValidaciones.EsNombrePersonalValido(txtNombre.TextBox, "Nombre de Usuario"))
                return;

            if (!ClsValidaciones.EsPasswordValido(txtContra.TextBox, "Contraseña"))
                return;

            if (cmbRol.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un Rol.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btmModificar.Enabled = false;

                clsUsuario objetoUsuario = new clsUsuario();
                int idRol = (int)cmbRol.SelectedValue;
                byte[] imagenByte = null;

                bool exito = await objetoUsuario.InsertarUsuarioAsync(
                    txtNombre.Text.Trim(),
                    txtContra.Text,
                    idRol,
                    imagenByte
                );

                if (exito)
                {
                    MessageBox.Show("Usuario guardado exitosamente.", "SG-BAMS", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (idRol == 1 || idRol == 2)
                    {
                        frmImagenEmpleado agregarImagen = new frmImagenEmpleado(txtNombre.Text.Trim());
                        agregarImagen.ShowDialog(); 
                    }

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Error de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btmModificar.Enabled = true;
            }
        }

        /// <summary>
        /// Handles the Click event of the kryptonButton1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the btnImagen control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>

    }
}