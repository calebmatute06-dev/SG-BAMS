using Krypton.Toolkit;

namespace SG_BAMS.ProductoInventario
{
    /// <summary>
    /// Configura un KryptonComboBox a partir de los datos de un <see cref="IComboRepository"/>.
    /// Única responsabilidad: enlazar el control de interfaz (DataSource, DisplayMember,
    /// ValueMember, selección inicial). No ejecuta ninguna consulta a base de datos —
    /// eso es responsabilidad exclusiva de IComboRepository (ver auditoría SOLID, hallazgo LC01).
    /// </summary>
    public static class ComboBoxConfigurator
    {
        /// <summary>
        /// Llena y configura el ComboBox con los datos del catálogo indicado.
        /// </summary>
        public static void Configurar(KryptonComboBox combo, IComboRepository repositorio, string tipoTabla, int idProveedorActual = 0)
        {
            ComboCatalogo config = ComboCatalogo.Obtener(tipoTabla);
            var datos = repositorio.ObtenerDatos(tipoTabla, idProveedorActual);

            combo.DataSource = datos;
            combo.DisplayMember = config.DisplayMember;
            combo.ValueMember = config.ValueMember;
            combo.SelectedIndex = datos.Rows.Count > 0 ? 0 : -1;
        }
    }
}
