using System.Data;
using System.Threading.Tasks;

namespace SG_BAMS.Administracion_de_BAMS
{
    // ============================================================
    // ICatalogoRepository — ISP + DIP
    // ============================================================
    // Contrato mínimo para todos los catálogos simples del sistema:
    // Clasificación, Estado, FormaPago, Marca, ModeloAuto, Rol,
    // TipoProducto.
    //
    // ISP: define solo las 3 operaciones que realmente comparten
    //      todos los catálogos (Leer, Insertar, Modificar).
    //      Si un catálogo necesitara operaciones adicionales,
    //      extendería con una interfaz más específica, sin romper
    //      a los demás consumidores.
    //
    // DIP: los formularios frmCatalogoBase y sus subclases
    //      dependen de esta abstracción, no de clsClasificacion
    //      ni de ninguna implementación concreta.
    // ============================================================

    /// <summary>
    /// Contrato de acceso a datos para catálogos simples (Leer, Insertar, Modificar).
    /// ISP: contrato mínimo — solo las operaciones comunes a todos los catálogos.
    /// DIP: los formularios dependen de esta interfaz, no de la clase concreta.
    /// </summary>
    public interface ICatalogoRepository
    {
        /// <summary>
        /// Obtiene todos los registros del catálogo.
        /// </summary>
        Task<DataTable> LeerAsync();

        /// <summary>
        /// Inserta un nuevo registro con la descripción dada.
        /// </summary>
        Task<bool> InsertarAsync(string descripcion);

        /// <summary>
        /// Modifica la descripción de un registro existente.
        /// </summary>
        Task<bool> ModificarAsync(int id, string nuevaDescripcion);
    }
}