using SG_BAMS.AccesoDatos;


namespace SG_BAMS.AccesoDatos
{
    public class clsModeloAuto : ClsCatalogoBase
    {
        protected override string SpLeer => "sp_ModelosAuto_Detalle";
        protected override string SpInsertar => "PA_insertar_modelo_auto";
        protected override string SpModificar => "PA_actualizar_modelo_de_auto";
        protected override string ParamDescripcion => "@nombre_modelo_auto";
        protected override string ParamId => "@id_modelo_auto";
        protected override string NombreCatalogo => "los modelos de auto";

        public System.Threading.Tasks.Task<System.Data.DataTable> LeerModelosAsync() => LeerAsync();
        public System.Threading.Tasks.Task<bool> InsertarModeloAutoAsync(string nombre) => InsertarAsync(nombre);
        public System.Threading.Tasks.Task<bool> ModificarModeloAutoAsync(int id, string nombre) => ModificarAsync(id, nombre);
    }
}