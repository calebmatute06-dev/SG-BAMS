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
    public partial class frmAgregarUsuarios : Form
    {

        public frmAgregarUsuarios()
        {
            InitializeComponent();
            CargarComboRoles();
        }

        private async void fmrAgregarUsuarios_Load(object sender, EventArgs e)
        {
            await CargarComboRoles();
        }

        private async Task CargarComboRoles()
        {
            try
            {
                clsUsuario objetoUsuario = new clsUsuario();
                DataTable dt = await objetoUsuario.ListarRolesAsync();

                // Configuración del ComboBox de Krypton
                cmbRol.DataSource = dt;

                // Lo que el usuario ve (Descripción)
                cmbRol.DisplayMember = "descripcion_rol";

                // Lo que el programa guarda internamente (ID)
                cmbRol.ValueMember = "id_rol_usuario";

                // Opcional: Que no aparezca nada seleccionado al inicio
                cmbRol.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            this.Close();
            frmUsuarios verUsuario = new frmUsuarios();
            verUsuario.Show();
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            frmImagenEmpleado agregarImagen = new frmImagenEmpleado();
            agregarImagen.Show();
        }

        private async void btmAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtContra.Text))
    {
        MessageBox.Show("Por favor complete los campos obligatorios.");
        return;
    }

    try
    {
        clsUsuario objetoUsuario = new clsUsuario();
        
        // Asumiendo que obtienes los IDs de ComboBoxes (CmbRol y CmbEstado)
        int idRol = (int)cmbRol.SelectedValue;
                int idEstado = 1;
        
        // Si tienes una imagen en un PictureBox, convertirla a byte[]
        byte[] imagenByte = null; 
        // Lógica de conversión de imagen omitida por brevedad

        bool exito = await objetoUsuario.InsertarUsuarioAsync(
            txtNombre.Text, 
            txtContra.Text, 
            idRol, 
            idEstado, 
            imagenByte
        );

        if (exito)
        {
            MessageBox.Show("Usuario guardado exitosamente.");
            
            // Regresar al formulario principal
            frmUsuarios principal = new frmUsuarios();
            principal.Show();
            this.Close();
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("Ocurrió un error: " + ex.Message);
    }
        }
    }
}
