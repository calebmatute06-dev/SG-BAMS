using System.Drawing;
using System.Windows.Forms;

namespace SG_BAMS
{
    // ============================================================
    // EstiloDataGridView — DRY + SRP
    // ============================================================
    // PROBLEMA DETECTADO EN LA REVISIÓN:
    //   El bloque de ~20 líneas que aplica el estilo visual al
    //   DataGridView (fondo blanco, encabezado azul, fuente Segoe UI,
    //   filas alternas, altura 32px, etc.) estaba copiado
    //   literalmente en:
    //     - frmUsuarios.cs
    //     - frmEstado.cs
    //     - Clasificacion.cs
    //     - frmFormaPago.cs
    //     - frmMarcaProductos.cs
    //     - frmModeloAuto.cs
    //     - frmRoles.cs
    //     - frmTipoProducto.cs
    //
    // DRY: un solo lugar para el estilo. Si cambia el diseño
    //      visual, se modifica aquí y aplica a todos.
    // SRP: los formularios solo se responsabilizan de su lógica;
    //      el estilo del grid es responsabilidad de esta clase.
    // ============================================================

    /// <summary>
    /// Clase estática que centraliza el estilo visual de los DataGridView del sistema.
    /// DRY: elimina el bloque de estilos repetido en todos los formularios de catálogo.
    /// SRP: única responsabilidad — aplicar el tema visual corporativo de SG-BAMS a un grid.
    /// </summary>
    public static class EstiloDataGridView
    {
        // Colores centralizados — si cambia el diseño, solo se modifica aquí
        private static readonly Color ColorEncabezadoFondo = Color.SkyBlue;
        private static readonly Color ColorEncabezadoTexto = Color.Navy;
        private static readonly Color ColorFilaNormal = Color.White;
        private static readonly Color ColorFilaAlterna = Color.FromArgb(230, 245, 255);
        private static readonly Color ColorTextoNormal = Color.Navy;
        private static readonly Color ColorSeleccionFondo = Color.DeepSkyBlue;
        private static readonly Color ColorSeleccionTexto = Color.White;
        private static readonly Color ColorBorde = Color.LightGray;

        private static readonly Font FuenteEncabezado = new Font("Segoe UI", 10, FontStyle.Bold);
        private static readonly Font FuenteCelda = new Font("Segoe UI", 10);

        /// <summary>
        /// Aplica el estilo visual corporativo de SG-BAMS al DataGridView indicado.
        /// Llamar al final del evento Load del formulario, después de cargar los datos.
        /// </summary>
        /// <param name="dgv">El DataGridView al que se aplica el estilo.</param>
        public static void Aplicar(DataGridView dgv)
        {
            // Borde y fondo
            dgv.BorderStyle = BorderStyle.None;
            dgv.BackgroundColor = ColorFilaNormal;
            dgv.RowHeadersVisible = false;

            // Encabezado
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = ColorEncabezadoFondo;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = ColorEncabezadoTexto;
            dgv.ColumnHeadersDefaultCellStyle.Font = FuenteEncabezado;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 28;

            // Celdas normales
            dgv.DefaultCellStyle.BackColor = ColorFilaNormal;
            dgv.DefaultCellStyle.ForeColor = ColorTextoNormal;
            dgv.DefaultCellStyle.Font = FuenteCelda;
            dgv.DefaultCellStyle.Padding = new Padding(3);

            // Filas alternas
            dgv.AlternatingRowsDefaultCellStyle.BackColor = ColorFilaAlterna;
            dgv.AlternatingRowsDefaultCellStyle.ForeColor = ColorTextoNormal;

            // Selección
            dgv.DefaultCellStyle.SelectionBackColor = ColorSeleccionFondo;
            dgv.DefaultCellStyle.SelectionForeColor = ColorSeleccionTexto;

            // Borde de celdas y filas
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = ColorBorde;
            dgv.RowTemplate.Height = 32;

            // Columnas
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.ClearSelection();
        }
    }
}