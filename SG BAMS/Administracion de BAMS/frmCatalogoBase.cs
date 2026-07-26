using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SG_BAMS.Dominio.AdministracionBAMS;

namespace SG_BAMS.Administracion_de_BAMS
{
    

    /// <summary>
    /// Formulario base para todos los catálogos simples del sistema.
    /// SRP: gestiona la lógica de pantalla de listado de un catálogo.
    /// OCP: abierto para extensión mediante herencia; cerrado para modificación.
    /// DIP: depende de ICatalogoRepository, no de implementaciones concretas.
    /// DRY: elimina la lógica duplicada presente en los 7 formularios de catálogo.
    /// </summary>
    public abstract class frmCatalogoBase : Form
    {
        // Dependencia inyectada — DIP
        private readonly ICatalogoRepository _repositorio;

        // ---- Propiedades que cada subclase debe definir ----

        /// <summary>Nombre de la columna que contiene el ID en el DataGrid.</summary>
        protected abstract string ColumnaId { get; }

        /// <summary>Nombre de la columna que contiene la descripción en el DataGrid.</summary>
        protected abstract string ColumnaDescripcion { get; }

        /// <summary>Texto del encabezado para la columna de descripción.</summary>
        protected abstract string EncabezadoDescripcion { get; }

        /// <summary>Nombre legible del catálogo para mensajes al usuario.</summary>
        protected abstract string NombreCatalogo { get; }

        /// <summary>
        /// El DataGridView de la subclase. Cada subclase debe retornar
        /// su propio control dgv (que viene del Designer).
        /// </summary>
        protected abstract DataGridView Grid { get; }

        // ---- Constructor ----

        /// <summary>
        /// Inicializa el formulario base con el repositorio inyectado.
        /// </summary>
        /// <param name="repositorio">Repositorio de datos del catálogo.</param>
        protected frmCatalogoBase(ICatalogoRepository repositorio)
        {
            _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
        }

        // ---- Métodos protegidos para uso de subclases ----

        /// <summary>
        /// Carga y actualiza el DataGridView con los datos del repositorio.
        /// Las subclases llaman a este método en Load y después de Agregar/Modificar.
        /// </summary>
        protected async System.Threading.Tasks.Task CargarGridAsync()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DataTable dt = await _repositorio.LeerAsync();
                Grid.DataSource = dt;
                ConfigurarColumnas();
                EstiloDataGridView.Aplicar(Grid);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar {NombreCatalogo}: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Aplica la configuración de columnas del catálogo al grid.
        /// </summary>
        private void ConfigurarColumnas()
        {
            if (Grid.Columns.Contains(ColumnaId))
                Grid.Columns[ColumnaId].Visible = false;

            if (Grid.Columns.Contains(ColumnaDescripcion))
                Grid.Columns[ColumnaDescripcion].HeaderText = EncabezadoDescripcion;

            Grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Grid.AllowUserToAddRows = false;
            Grid.ReadOnly = true;
            Grid.ClearSelection();
        }

        /// <summary>
        /// Abre o enfoca un formulario hijo (Agregar o Modificar).
        /// Después de cerrar con OK, recarga el grid.
        /// </summary>
        protected void AbrirOEnfocarDialogo<T>(Func<T> creadorFormulario) where T : Form
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
                        _ = CargarGridAsync();
                }
            }
            Grid.ClearSelection();
        }

        /// <summary>
        /// Obtiene el ID de la fila seleccionada actualmente en el grid.
        /// </summary>
        protected int ObtenerIdSeleccionado()
        {
            return Convert.ToInt32(Grid.CurrentRow.Cells[ColumnaId].Value);
        }

        /// <summary>
        /// Obtiene la descripción de la fila seleccionada actualmente en el grid.
        /// </summary>
        protected string ObtenerDescripcionSeleccionada()
        {
            return Grid.CurrentRow.Cells[ColumnaDescripcion].Value?.ToString() ?? string.Empty;
        }

        /// <summary>
        /// Retorna true si hay una fila seleccionada en el grid.
        /// </summary>
        protected bool HaySeleccion()
        {
            return Grid.CurrentRow != null && Grid.SelectedRows.Count > 0;
        }

        /// <summary>
        /// Muestra el aviso estándar de "seleccione un registro primero".
        /// </summary>
        protected void MostrarAvisoSinSeleccion()
        {
            MessageBox.Show($"Por favor, seleccione un {NombreCatalogo.ToLower()} de la lista.",
                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Acceso al repositorio para las subclases que necesiten llamadas adicionales.
        /// </summary>
        protected ICatalogoRepository Repositorio => _repositorio;
    }
}