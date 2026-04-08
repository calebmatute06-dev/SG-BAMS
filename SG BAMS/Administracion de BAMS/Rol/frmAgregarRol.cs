using SG_BAMS.Administracion_de_BAMS.Rol;
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
    /// <summary>
    /// Representa la interfaz de usuario para el registro de nuevos roles de usuario en el sistema.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class frmAgregarRol : Form
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="frmAgregarRol"/>.
        /// </summary>
        public frmAgregarRol()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            // Restricción para permitir únicamente la entrada de letras en el campo de descripción
            txtDescri.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
        }

        /// <summary>
        /// Maneja el evento Click de la imagen 16.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void pictureBox16_Click(object sender, EventArgs e)
        {
            // Espacio para implementación futura o decoración
        }

        /// <summary>
        /// Maneja el evento Click de la etiqueta 1.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void label1_Click(object sender, EventArgs e)
        {
            // Espacio para implementación futura o decoración
        }

        /// <summary>
        /// Procesa el registro del nuevo rol de forma asíncrona tras validar que el nombre sea válido.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private async void btmAgregar_Click(object sender, EventArgs e)
        {
            // Validación de formato para nombres de roles (Solo letras y espacios)
            if (!ClsValidaciones.EsNombrePersonalValido(txtDescri.TextBox, "Nombre del Rol"))
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
                btmAgregar.Enabled = false;

                clsRol objetoRol = new clsRol();

                // Intento de inserción asíncrona en la base de datos
                bool exito = await objetoRol.InsertarRolAsync(txtDescri.Text.Trim());

                if (exito)
                {
                    MessageBox.Show("Rol registrado correctamente.", "SG-BAMS",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Establecer resultado OK para actualizar grids en formularios padres
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btmAgregar.Enabled = true;
            }
        }

        /// <summary>
        /// Cierra el formulario actual y regresa a la vista de gestión de roles.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btmSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}