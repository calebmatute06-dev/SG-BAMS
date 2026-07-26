using SG_BAMS.AccesoDatos;


namespace SG_BAMS.AccesoDatos
{
    public class clsRol : ClsCatalogoBase
    {
        protected override string SpLeer => "sp_Roles_Detalle";
        protected override string SpInsertar => "PA_insertar_rol";
        protected override string SpModificar => "PA_actualizar_rol";
        protected override string ParamDescripcion => "@descripcion_rol";
        protected override string ParamId => "@id_rol_usuario";
        protected override string NombreCatalogo => "los roles";

        public System.Threading.Tasks.Task<System.Data.DataTable> LeerRolesAsync() => LeerAsync();
        public System.Threading.Tasks.Task<bool> InsertarRolAsync(string desc) => InsertarAsync(desc);
        public System.Threading.Tasks.Task<bool> ModificarRolAsync(int id, string desc) => ModificarAsync(id, desc);
    }
}