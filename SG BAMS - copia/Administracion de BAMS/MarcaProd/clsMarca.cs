using SG_BAMS.Administracion_de_BAMS;

namespace SG_BAMS.Administracion_de_BAMS.MarcaProd
{
    internal class clsMarca : ClsCatalogoBase
    {
        protected override string SpLeer => "sp_Marcas_Detalle";
        protected override string SpInsertar => "PA_insertar_marca_producto";
        protected override string SpModificar => "PA_actualizar_marca_producto";
        protected override string ParamDescripcion => "@nombre_marca";
        protected override string ParamId => "@id_marca_producto";
        protected override string NombreCatalogo => "las marcas";

        // Alias para mantener compatibilidad con los formularios existentes
        public System.Threading.Tasks.Task<System.Data.DataTable> LeerMarcasAsync() => LeerAsync();
        public System.Threading.Tasks.Task<bool> InsertarMarcaAsync(string nombre) => InsertarAsync(nombre);
        public System.Threading.Tasks.Task<bool> ModificarMarcaAsync(int id, string nombre) => ModificarAsync(id, nombre);
    }
}