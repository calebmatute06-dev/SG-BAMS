using SG_BAMS.Administracion_de_BAMS.Usuarios;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Formulario principal para la administración del listado de usuarios.
    /// DIP: depende de IUsuarioRepository, no de clsUsuario directamente.
    /// SRP: única responsabilidad — mostrar y coordinar las acciones sobre el listado de usuarios.
    /// </summary>
    public partial class frmUsuarios : Form
    {
        private readonly IUsuarioRepository _usuarioRepository;

        /// <summary>
        /// Constructor que recibe el repositorio por inyección de dependencias.
        /// </summary>
        public frmUsuarios(IUsuarioRepository usuarioRepository)
        {
            InitializeComponent();
            _usuarioRepository = usuarioRepository;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        /// <summary>
        /// Constructor sin parámetros para compatibilidad con formularios existentes.
        /// </summary>
        public frmUsuarios() : this(new clsUsuario()) { }

        private async System.Threading.Tasks.Task CargarGridUsuarios()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                dgvUsuarios.DataSource = await _usuarioRepository.LeerUsuariosAsync();
                ConfigurarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void ConfigurarGrid()
        {
            string[] columnasOcultas = { "imagen_usuario", "id_rol_usuario", "id_estado", "id_usuario" };
            foreach (string col in columnasOcultas)
            {
                if (dgvUsuarios.Columns.Contains(col))
                    dgvUsuarios.Columns[col].Visible = false;
            }

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

        private void AbrirOEnfocarDialogo<T>(Func<T> creadorFormulario, Action<T> accionesPostDialogo) where T : Form
        {
            T formExistente = Application.OpenForms.Cast<Form>().OfType<T>().FirstOrDefault();

            if (formExistente != null)
            {
                if (formExistente.WindowState == FormWindowState.Minimized)
                    formExistente.WindowState = FormWindowState.Normal;
                formExistente.BringToFront();
                formExistente.Focus();
            }
            else
            {
                using (T nuevoForm = creadorFormulario())
                {
                    if (nuevoForm.ShowDialog() == DialogResult.OK)
                        accionesPostDialogo(nuevoForm);
                }
            }
        }

        private void dgvUsuarios_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvUsuarios.CurrentRow != null && dgvUsuarios.SelectedRows.Count > 0)
            {
                AbrirModificar();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione una fila completa de la lista.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            dgvUsuarios.ClearSelection();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AbrirOEnfocarDialogo(
                () => new frmAgregarUsuarios(_usuarioRepository),
                (f) => _ = CargarGridUsuarios()
            );
            dgvUsuarios.ClearSelection();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow != null && dgvUsuarios.SelectedRows.Count > 0)
            {
                AbrirModificar();
                dgvUsuarios.ClearSelection();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un usuario de la lista.",
                    "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void AbrirModificar()
        {
            int id = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["id_usuario"].Value);
            string nombre = dgvUsuarios.CurrentRow.Cells["nombre_usuario"].Value.ToString();
            int idRol = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["id_rol_usuario"].Value);
            int idEstado = Convert.ToInt32(dgvUsuarios.CurrentRow.Cells["id_estado"].Value);
            string correo = dgvUsuarios.CurrentRow.Cells["Correo"].Value.ToString();

            AbrirOEnfocarDialogo(
                () => new frmModificarUsuarios(id, nombre, idRol, idEstado, correo, _usuarioRepository),
                (f) => _ = CargarGridUsuarios()
            );
        }

        private void btnSalir_Click(object sender, EventArgs e) => this.Close();

        private async void frmUsuarios_Load(object sender, EventArgs e)
        {
            await CargarGridUsuarios();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            AplicarEstiloGrid();
        }

        private void AplicarEstiloGrid()
        {
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