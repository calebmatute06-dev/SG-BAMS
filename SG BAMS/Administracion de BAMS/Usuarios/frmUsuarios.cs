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
    /// Formulario principal para la administración y visualización del listado de usuarios del sistema.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class frmUsuarios : Form
    {
        /// <summary>
        /// Instancia de la clase de negocio para la gestión de datos de usuarios.
        /// </summary>
        clsUsuario objetoUsuario = new clsUsuario();

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="frmUsuarios"/>.
        /// </summary>
        public frmUsuarios()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        /// <summary>
        /// Carga de forma asíncrona la lista de usuarios en el control DataGridView.
        /// </summary>
        /// <returns>Tarea que representa la operación asíncrona.</returns>
        private async Task CargarGridUsuarios()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                dgvUsuarios.DataSource = await objetoUsuario.LeerUsuariosAsync();
                ConfigurarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar: " + ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Aplica configuraciones de visualización, visibilidad de columnas y estilos al DataGridView de usuarios.
        /// </summary>
        private void ConfigurarGrid()
        {
            if (dgvUsuarios.Columns.Contains("imagen_usuario"))
                dgvUsuarios.Columns["imagen_usuario"].Visible = false;

            if (dgvUsuarios.Columns.Contains("id_rol_usuario"))
                dgvUsuarios.Columns["id_rol_usuario"].Visible = false;

            if (dgvUsuarios.Columns.Contains("id_estado"))
                dgvUsuarios.Columns["id_estado"].Visible = false;

            if (dgvUsuarios.Columns.Contains("id_usuario"))
                dgvUsuarios.Columns["id_usuario"].Visible = false;

            if (dgvUsuarios.Columns.Contains("nombre_usuario"))
                dgvUsuarios.Columns["nombre_usuario"].HeaderText = "Nombre";

            dgvUsuarios.Columns["nombre_usuario"].FillWeight = 100;
            dgvUsuarios.Columns["Correo"].FillWeight = 250;
            dgvUsuarios.Columns["Rol"].FillWeight = 120;


            dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.AllowUserToAddRows = false;
            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.ClearSelection();
        }

        /// <summary>
        /// Maneja el evento de doble clic en una celda para abrir el formulario de edición del usuario seleccionado.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">Instancia de <see cref="DataGridViewCellEventArgs"/> con los datos del evento.</param>
        private void dgvUsuarios_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["id_usuario"].Value);
                string nombre = dgvUsuarios.CurrentRow.Cells["nombre_usuario"].Value.ToString();

                int idRol = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["id_rol_usuario"].Value);
                int idEstado = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["id_estado"].Value);

                string correo = dgvUsuarios.CurrentRow.Cells["Correo"].Value.ToString();

                frmModificarUsuarios frmMod = new frmModificarUsuarios(id, nombre, idRol, idEstado, correo);

                if (frmMod.ShowDialog() == DialogResult.OK)
                {
                    _ = CargarGridUsuarios();
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un usuario de la lista.");
            }
        }

        /// <summary>
        /// Maneja el evento Click del botón agregar para desplegar el formulario de registro de nuevos usuarios.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">Instancia de <see cref="EventArgs"/> con los datos del evento.</param>
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAgregarUsuarios agregarUsuario = new frmAgregarUsuarios();
            agregarUsuario.Show();
        }

        /// <summary>
        /// Maneja el evento Click del botón modificar para editar el registro del usuario seleccionado en la lista.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">Instancia de <see cref="EventArgs"/> con los datos del evento.</param>
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["id_usuario"].Value);
                string nombre = dgvUsuarios.CurrentRow.Cells["nombre_usuario"].Value.ToString();

                int idRol = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["id_rol_usuario"].Value);
                int idEstado = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["id_estado"].Value);

                string correo = dgvUsuarios.CurrentRow.Cells["Correo"].Value.ToString();

                frmModificarUsuarios frmMod = new frmModificarUsuarios(id, nombre, idRol, idEstado, correo);

                if (frmMod.ShowDialog() == DialogResult.OK)
                {
                    _ = CargarGridUsuarios();
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un usuario de la lista.");
            }
        }

        /// <summary>
        /// Cierra el formulario actual de administración de usuarios.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">Instancia de <see cref="EventArgs"/> con los datos del evento.</param>
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Maneja el evento Load del formulario. Ejecuta la carga inicial de datos y aplica estilos visuales al grid.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">Instancia de <see cref="EventArgs"/> con los datos del evento.</param>
        private async void frmUsuarios_Load(object sender, EventArgs e)
        {
            await CargarGridUsuarios();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            dgvUsuarios.BorderStyle = BorderStyle.None;
            dgvUsuarios.BackgroundColor = Color.White;
            dgvUsuarios.RowHeadersVisible = false;
            dgvUsuarios.EnableHeadersVisualStyles = false;
            dgvUsuarios.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgvUsuarios.ColumnHeadersDefaultCellStyle.BackColor = Color.SkyBlue;
            dgvUsuarios.ColumnHeadersDefaultCellStyle.ForeColor = Color.Navy;
            dgvUsuarios.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvUsuarios.ColumnHeadersHeight = 28;

            dgvUsuarios.DefaultCellStyle.BackColor = Color.White;
            dgvUsuarios.DefaultCellStyle.ForeColor = Color.Navy;
            dgvUsuarios.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvUsuarios.DefaultCellStyle.Padding = new Padding(3);
            dgvUsuarios.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 245, 255);
            dgvUsuarios.AlternatingRowsDefaultCellStyle.ForeColor = Color.Navy;

            dgvUsuarios.DefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            dgvUsuarios.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvUsuarios.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvUsuarios.GridColor = Color.LightGray;
            dgvUsuarios.RowTemplate.Height = 32;
            dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsuarios.ClearSelection();
        }
    }
}