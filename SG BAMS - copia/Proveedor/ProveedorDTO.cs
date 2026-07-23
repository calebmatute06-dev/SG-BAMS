using System;

namespace SG_BAMS.Proveedor.DTO
{
    /// <summary>
    /// Transporta todos los datos de un proveedor, tanto para registrarlo
    /// como para modificarlo. Reemplaza el paso de parámetros sueltos entre
    /// AgregarProveedores, ModificarProveedor, ProveedoresAdmin y ClsProveedor.
    /// </summary>
    public class ProveedorDTO
    {
        /// <summary>
        /// Se llena solo al modificar un proveedor ya existente (0 = proveedor nuevo).
        /// </summary>
        public int IdProveedor { get; set; }

        public string Nombre { get; set; }

        /// <summary>
        /// Teléfono de contacto del proveedor.
        /// </summary>
        public string Contacto { get; set; }

        public string Direccion { get; set; }

        public string Rtn { get; set; }

        /// <summary>
        /// Solo aplica al modificar (al crear, el estado inicial lo define la BD).
        /// </summary>
        public int IdEstado { get; set; }

        public int IdClasificacion { get; set; }

        /// <summary>
        /// Usuario que realiza la operación (para el contexto de sesión en BD).
        /// </summary>
        public int IdUsuario { get; set; }
    }
}
