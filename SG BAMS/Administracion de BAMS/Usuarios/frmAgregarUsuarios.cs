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

        private void btnImagen_Click(object sender, EventArgs e)
        {
            // Se manda el nombre de usuario escrito en la caja de texto al formulario de la cámara
            frmImagenEmpleado agregarImagen = new frmImagenEmpleado(txtNombre.Text);
            agregarImagen.Show();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Variable para guardar la lista original de roles y no perderla al filtrar
        private List<string> listaOriginalRoles = new List<string>();

        private void ConfigurarFiltroRoles()
        {
            cmbRol.DropDownStyle = ComboBoxStyle.DropDown;

            cmbRol.TextUpdate += (s, e) =>
            {
                string filtro = cmbRol.Text;

                if (dtRoles != null)
                {
                    // Creamos una vista filtrada del DataTable
                    DataView dv = dtRoles.DefaultView;

                    // Filtramos por la columna de texto (descripcion_rol)
                    dv.RowFilter = $"descripcion_rol LIKE '%{filtro}%'";

                    // Actualizamos el origen de datos
                    cmbRol.DataSource = dv;

                    // Mantenemos el desplegable abierto y el texto que el usuario escribe
                    cmbRol.DroppedDown = true;
                    cmbRol.Text = filtro;

                    // Ponemos el cursor al final del texto
                    cmbRol.SelectionStart = filtro.Length;

                    // Evitamos que el cursor cambie de forma
                    Cursor.Current = Cursors.Default;
                }
            };
        }

        private void cmbRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRol.SelectedIndex != -1)
            {
                // Obtenemos el texto del rol seleccionado
                string rolSeleccionado = cmbRol.Text;

                // Activamos si es Administrador o Empleado (ajusta las strings si varían en tu BD)
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
    }
}