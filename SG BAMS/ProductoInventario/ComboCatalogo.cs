using System;
using System.Collections.Generic;

namespace SG_BAMS.ProductoInventario
{
    /// <summary>
    /// Describe un catálogo disponible para llenar un ComboBox: el procedimiento almacenado
    /// que provee los datos y los nombres de columna a usar como texto/valor mostrado.
    /// </summary>
    public sealed class ComboCatalogo
    {
        public string NombreProcedimiento { get; }
        public string DisplayMember { get; }
        public string ValueMember { get; }

        /// <summary>
        /// Indica si este catálogo necesita recibir el id del proveedor actual
        /// (usado únicamente por "Proveedor" al modificar, para incluir al proveedor
        /// actual aunque ya no esté activo).
        /// </summary>
        public bool RequiereIdProveedorActual { get; }

        public ComboCatalogo(string nombreProcedimiento, string displayMember, string valueMember, bool requiereIdProveedorActual = false)
        {
            NombreProcedimiento = nombreProcedimiento;
            DisplayMember = displayMember;
            ValueMember = valueMember;
            RequiereIdProveedorActual = requiereIdProveedorActual;
        }

        /// <summary>
        /// Catálogos disponibles para los ComboBox del módulo de Inventario.
        /// Agregar un nuevo tipo de combo solo requiere añadir una entrada aquí,
        /// sin modificar ningún switch existente (ver auditoría SOLID, hallazgo LC02).
        /// </summary>
        public static readonly IReadOnlyDictionary<string, ComboCatalogo> Configuraciones =
            new Dictionary<string, ComboCatalogo>
            {
                ["Marca"] = new ComboCatalogo("sp_Combo_Marcas", "nombre_marca", "id_marca_producto"),
                ["Tipo"] = new ComboCatalogo("sp_Combo_Tipos", "descripcion_producto", "id_tipo_producto"),
                ["Modelo"] = new ComboCatalogo("sp_Combo_Modelos", "nombre_modelo_auto", "id_modelo_auto"),
                ["Estado"] = new ComboCatalogo("sp_Combo_Estados", "descripcion_estado", "id_estado"),
                ["Proveedor"] = new ComboCatalogo("sp_Combo_ProveedoresActivos", "nombre_proveedor", "id_proveedor", requiereIdProveedorActual: true),
            };

        /// <summary>
        /// Obtiene la configuración del catálogo solicitado o lanza si no está registrado.
        /// </summary>
        public static ComboCatalogo Obtener(string tipoTabla)
        {
            if (Configuraciones.TryGetValue(tipoTabla, out var config))
                return config;

            throw new ArgumentException($"El catálogo '{tipoTabla}' no está configurado.", nameof(tipoTabla));
        }
    }
}
