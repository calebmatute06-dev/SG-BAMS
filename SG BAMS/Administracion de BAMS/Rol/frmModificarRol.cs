using SG_BAMS.Administracion_de_BAMS.Rol;
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
    /// Representa la interfaz de usuario para la modificación de un rol de usuario existente.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class frmModificarRol : Form
    {
        /// <summary>
        /// Almacena el identificador del rol seleccionado para su edición.
        /// </summary>
        private int idRolSeleccionado;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="frmModificarRol"/>.
        /// </summary>
        /// <param name="id">El identificador único del rol.</param>
        /// <param name="nombreActual">El nombre actual del rol que se cargará en el campo de texto.</param>
        public frmModificarRol(int id, string nombreActual)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.idRolSeleccionado = id;
            txtDescri.Text = nombreActual;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            // Restricción de entrada para permitir únicamente letras durante la escritura
            txtDescri.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
        }

        /// <summary>
        /// Procesa la actualización del rol de forma asíncrona tras validar los datos ingresados.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private async void btmModificar_Click(object sender, EventArgs e)
        {
            // Validación de formato de nombre (solo letras y espacios permitidos)
            if (!ClsValidaciones.EsNombrePersonalValido(txtDescri, "Nombre del Rol"))
            {
                return;
            }

            if (!ClsValidaciones.ValidarNombreUnico(
                    control: txtDescri,
                    tabla: "Rol",
                    columnaNombre: "descripcion_rol",
                    nombreCampo: "Tipo de Rol",
                    idExcluir: 0,
                    idColumna: "id_rol_usuario"))
            {
                return;
            }
            try
            {
                this.Cursor = Cursors.WaitCursor;
                btmModificar.Enabled = false;

                clsRol objetoRol = new clsRol();

                // Intento de modificación en la base de datos a través de la capa de lógica
                bool exito = await objetoRol.ModificarRolAsync(idRolSeleccionado, txtDescri.Text.Trim());

                if (exito)
                {
                    MessageBox.Show("Rol actualizado con éxito.", "SG-BAMS",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Establecer el resultado como OK para notificar al formulario que realizó la llamada
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar: " + ex.Message, "Error de Sistema",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btmModificar.Enabled = true;
            }
        }

        /// <summary>
        /// Cierra el formulario actual sin realizar cambios.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btmSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}