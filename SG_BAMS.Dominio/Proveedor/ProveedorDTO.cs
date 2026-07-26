using System;

namespace SG_BAMS.Proveedor.DTO
{
    /// <summary>
    /// Objeto de transferencia de datos para proveedores.
    /// Transporta todos los datos necesarios para registrar o modificar un proveedor
    /// entre las capas de presentación y acceso a datos.
    /// </summary>
    public class ProveedorDTO
    {
        /// <summary>
        /// Identificador único del proveedor.
        /// Vale 0 para proveedores nuevos, y el ID existente para modificaciones.
        /// </summary>
        public int IdProveedor { get; set; }

        /// <summary>
        /// Nombre del proveedor.
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Teléfono de contacto del proveedor.
        /// </summary>
        public string Contacto { get; set; }

        /// <summary>
        /// Dirección física del proveedor.
        /// </summary>
        public string Direccion { get; set; }

        /// <summary>
        /// Registro Tributario Nacional del proveedor.
        /// </summary>
        public string Rtn { get; set; }

        /// <summary>
        /// Identificador del estado del proveedor.
        /// Solo aplica al modificar; al crear, el estado inicial lo define la base de datos.
        /// </summary>
        public int IdEstado { get; set; }

        /// <summary>
        /// Identificador de la clasificación del proveedor.
        /// </summary>
        public int IdClasificacion { get; set; }

        /// <summary>
        /// Identificador del usuario que realiza la operación.
        /// Utilizado para auditoría en la base de datos.
        /// </summary>
        public int IdUsuario { get; set; }
    }
}