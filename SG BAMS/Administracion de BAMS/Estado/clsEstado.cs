using SG_BAMS.Administracion_de_BAMS;

namespace SG_BAMS.Administracion_de_BAMS.Estado
{
    internal class clsEstado : ClsCatalogoBase
    {
        protected override string SpLeer => "sp_Estados_Detalle";
        protected override string SpInsertar => "PA_insertar_estado";
        protected override string SpModificar => "PA_actualizar_estado";
        protected override string ParamDescripcion => "@descripcion_estado";
        protected override string ParamId => "@id_estado";
        protected override string NombreCatalogo => "los estados";

        public System.Threading.Tasks.Task<System.Data.DataTable> LeerEstadosAsync() => LeerAsync();
        public System.Threading.Tasks.Task<bool> InsertarEstadoAsync(string desc) => InsertarAsync(desc);
        public System.Threading.Tasks.Task<bool> ModificarEstadoAsync(int id, string desc) => ModificarAsync(id, desc);
    }
}