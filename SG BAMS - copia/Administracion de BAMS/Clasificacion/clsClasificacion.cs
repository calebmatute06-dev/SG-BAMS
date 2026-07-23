using SG_BAMS.Administracion_de_BAMS;

namespace SG_BAMS.Administracion_de_BAMS.Clasificacion
{
    internal class clsClasificacion : ClsCatalogoBase
    {
        protected override string SpLeer => "sp_vista_clasificacion";
        protected override string SpInsertar => "PA_insertar_clasificacion";
        protected override string SpModificar => "PA_actualizar_clasificacion";
        protected override string ParamDescripcion => "@clasificacion_proveedor";
        protected override string ParamId => "@id_clasificacion_proveedor";
        protected override string NombreCatalogo => "las clasificaciones";

        public System.Threading.Tasks.Task<System.Data.DataTable> LeerClasificacionAsync() => LeerAsync();
        public System.Threading.Tasks.Task<bool> InsertarClasificacionAsync(string desc) => InsertarAsync(desc);
        public System.Threading.Tasks.Task<bool> ModificarClasificacionAsync(int id, string desc) => ModificarAsync(id, desc);
    }
}