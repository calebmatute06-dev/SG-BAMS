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
    public partial class frmAgregarUsuarios : Form
    {
        private DataTable dtRoles;
        public frmAgregarUsuarios()
        {
            InitializeComponent();
            CargarComboRoles();
            btnImagen.Enabled = false;
            cmbRol.DropDownStyle = ComboBoxStyle.DropDownList;
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private async void btmAgregar_Click(object sender, EventArgs e)
        {

            string nombreLimpio = txtNombre.Text.Trim();
            string contraLimpia = txtContra.Text.Trim();

            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("Por favor complete los campos obligatorios.");
                return;
            }

            if (contraLimpia.Contains(" "))
            {
                MessageBox.Show("La contraseña no puede contener espacios.",
                                "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (contraLimpia.Length > 0 && contraLimpia.Length < 6)
            {
                MessageBox.Show("La contraseña debe tener al menos 6 caracteres.", "Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nombreLimpio.Contains("  "))
            {
                MessageBox.Show("El nombre no puede contener dos espacios seguidos.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Regex.IsMatch(nombreLimpio, @"(\w)\1{2,}"))
            {
                MessageBox.Show("No se permite repetir la misma letra más de dos veces seguidas.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (nombreLimpio.Length < 3)
            {
                MessageBox.Show("El nombre debe tener mas de 3 caracteres.", "Error de Longitud", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Regex.IsMatch(nombreLimpio, @"^[a-zA-ZñÑáéíóúÁÉÍÓÚ]{3,}(\s[a-zA-ZñÑáéíóúÁÉÍÓÚ]{3,})*$"))
            {
                MessageBox.Show("Escriba un nombre Valido.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cmbRol.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un Rol.");
                return;
            }

            try
                {
                    clsUsuario objetoUsuario = new clsUsuario();

                    int idRol = (int)cmbRol.SelectedValue;
                    int idEstado = 1;
                    byte[] imagenByte = null;

                    bool exito = await objetoUsuario.InsertarUsuarioAsync(
                        txtNombre.Text,
                        txtContra.Text,
                        idRol,
                        imagenByte
                    );

                    if (exito)
                    {
                        MessageBox.Show("Usuario guardado exitosamente.");

                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error: " + ex.Message);
                }
            }
        

        private void btnImagen_Click(object sender, EventArgs e)
        {
            frmImagenEmpleado agregarImagen = new frmImagenEmpleado(txtNombre.Text);
            agregarImagen.Show();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private List<string> listaOriginalRoles = new List<string>();

        

        private void cmbRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRol.SelectedIndex != -1)
            {
                string rolSeleccionado = cmbRol.Text;

                if (rolSeleccionado == "Administrador" || rolSeleccionado == "Empleado")
                {
                    btnImagen.Enabled = true;
                }
                else
                {
                    btnImagen.Enabled = false;
                }
            }
            else
            {
                btnImagen.Enabled = false;
            }
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }
    }
}