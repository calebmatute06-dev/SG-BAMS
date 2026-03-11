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
        }

        private async void fmrAgregarUsuarios_Load(object sender, EventArgs e)
        {
            await CargarComboRoles();
            cmbRol.DropDownStyle = ComboBoxStyle.DropDown;
            cmbRol.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbRol.AutoCompleteSource = AutoCompleteSource.ListItems;
            ConfigurarFiltroRoles();
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

            if (string.IsNullOrEmpty(txtNombre.Text) || string.IsNullOrEmpty(txtContra.Text))
            {
                MessageBox.Show("Por favor complete los campos obligatorios.");
                return;
            }

            if (txtContra.Text.Length < 3)
            {
                MessageBox.Show("La direccion debe tener mas de 3 caracteres.", "Error de Longitud", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtNombre.Text.Contains("  "))
            {
                MessageBox.Show("El nombre no puede contener dos espacios seguidos.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Regex.IsMatch(txtNombre.Text, @"(\w)\1{2,}"))
            {
                MessageBox.Show("No se permite repetir la misma letra más de dos veces seguidas.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txtNombre.Text.Length < 3)
                {
                    MessageBox.Show("El nombre debe tener mas de 3 caracteres.", "Error de Longitud", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!Regex.IsMatch(txtNombre.Text, @"^[a-zA-Z \s & ñ Ñ @,.;:<>]+$"))
                {
                    MessageBox.Show("El campos de Nombre solo deben contener caracteres validos.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void ConfigurarFiltroRoles()
        {
            cmbRol.DropDownStyle = ComboBoxStyle.DropDown;

            cmbRol.TextUpdate += (s, e) =>
            {
                string filtro = cmbRol.Text;

                if (dtRoles != null)
                {
                    DataView dv = dtRoles.DefaultView;

                    dv.RowFilter = $"descripcion_rol LIKE '%{filtro}%'";

                    cmbRol.DataSource = dv;

                    cmbRol.DroppedDown = true;
                    cmbRol.Text = filtro;

                    cmbRol.SelectionStart = filtro.Length;

                    Cursor.Current = Cursors.Default;
                }
            };
        }

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