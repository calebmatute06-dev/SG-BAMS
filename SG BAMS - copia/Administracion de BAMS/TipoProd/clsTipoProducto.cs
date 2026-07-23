using SG_BAMS.Administracion_de_BAMS;

namespace SG_BAMS.Administracion_de_BAMS.TipoProd
{
    internal class clsTipoProducto : ClsCatalogoBase
    {
        protected override string SpLeer => "sp_TipoProducto_Detalle";
        protected override string SpInsertar => "PA_insertar_tipo_producto";
        protected override string SpModificar => "PA_actualizar_tipo_producto";
        protected override string ParamDescripcion => "@descripcion_producto";
        protected override string ParamId => "@id_tipo_producto";
        protected override string NombreCatalogo => "los tipos de producto";

        public System.Threading.Tasks.Task<System.Data.DataTable> LeerTiposProductoAsync() => LeerAsync();
        public System.Threading.Tasks.Task<bool> InsertarTipoProductoAsync(string desc) => InsertarAsync(desc);
        public System.Threading.Tasks.Task<bool> ModificarTipoProductoAsync(int id, string desc) => ModificarAsync(id, desc);
    }
}