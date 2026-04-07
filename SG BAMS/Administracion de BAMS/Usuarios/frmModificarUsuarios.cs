using SG_BAMS.Administracion_de_BAMS.Usuarios;
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
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class frmModificarUsuarios : Form
    {
        /// <summary>
        /// The identifier usuario seleccionado
        /// </summary>
        private int idUsuarioSeleccionado;
        /// <summary>
        /// The rol inicial
        /// </summary>
        private int rolInicial;
        /// <summary>
        /// The estado inicial
        /// </summary>
        private int estadoInicial;

        /// <summary>
        /// Initializes a new instance of the <see cref="frmModificarUsuarios"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="nombre">The nombre.</param>
        /// <param name="rol">The rol.</param>
        /// <param name="estado">The estado.</param>
        public frmModificarUsuarios(int id, string nombre, int rol, int estado)
        {
            InitializeComponent();

            
            cmbRol.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            txtNombre.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);

           
            this.idUsuarioSeleccionado = id;
            this.rolInicial = rol;
            this.estadoInicial = estado;
            txtNombre.Text = nombre;
        }


        /// <summary>
        /// Handles the Load event of the fmrModificarUsuarios control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void fmrModificarUsuarios_Load(object sender, EventArgs e)
        {
            await CargarCombos();

            
            cmbRol.SelectedValue = rolInicial;
            cmbEstado.SelectedValue = estadoInicial;
        }

        /// <summary>
        /// Cargars the combos.
        /// </summary>
        private async Task CargarCombos()
        {
            try
            {
                clsUsuario objetoUsuario = new clsUsuario();

                DataTable dtRoles = await objetoUsuario.ListarRolesAsync();
                cmbRol.DataSource = dtRoles;
                cmbRol.DisplayMember = "descripcion_rol";
                cmbRol.ValueMember = "id_rol_usuario";

                DataTable dtEstados = await objetoUsuario.ListarEstadosAsync();
                cmbEstado.DataSource = dtEstados;
                cmbEstado.DisplayMember = "descripcion_estado";
                cmbEstado.ValueMember = "id_estado";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar listas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the 1 event of the btmModificar_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void btmModificar_Click_1(object sender, EventArgs e)
        {
            
            if (!ClsValidaciones.EsNombrePersonalValido(txtNombre, "Nombre de Usuario")) return;

            
            if (!string.IsNullOrWhiteSpace(txtContra.Text))
            {
                if (!ClsValidaciones.EsPasswordValido(txtContra, "Contraseña")) return;
            }

            
            if (cmbRol.SelectedIndex == -1 || cmbEstado.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un Rol y un Estado.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btmModificar.Enabled = false;

                clsUsuario objetoUsuario = new clsUsuario();

                int idRol = (int)cmbRol.SelectedValue;
                int idEstado = (int)cmbEstado.SelectedValue;
                byte[] imagenByte = null;

                bool exito = await objetoUsuario.ModificarUsuarioAsync(
                    idUsuarioSeleccionado,
                    txtNombre.Text.Trim(),
                    txtContra.Text, 
                    idRol,
                    idEstado,
                    imagenByte
                );

                if (exito)
                {
                    MessageBox.Show("Usuario actualizado con éxito.", "SG-BAMS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message, "Error de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btmModificar.Enabled = true;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnSalir control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the 1 event of the btnImagen_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnImagen_Click_1(object sender, EventArgs e)
        {
            frmImagenEmpleado agregarImagen = new frmImagenEmpleado(txtNombre.Text);
            agregarImagen.ShowDialog();
        }
    }
}