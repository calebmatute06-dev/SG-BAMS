using SG_BAMS.AccesoDatos;

namespace SG_BAMS.AccesoDatos
{
    public class clsFormaPago : ClsCatalogoBase
    {
        protected override string SpLeer => "sp_FormasPago_Detalle";
        protected override string SpInsertar => "PA_insertar_tipo_forma_pago";
        protected override string SpModificar => "PA_actualizar_tipo_forma_pago";
        protected override string ParamDescripcion => "@descripcion_forma_pago";
        protected override string ParamId => "@id_tipo_forma_pago";
        protected override string NombreCatalogo => "las formas de pago";

        public System.Threading.Tasks.Task<System.Data.DataTable> LeerFormasPagoAsync() => LeerAsync();
        public System.Threading.Tasks.Task<bool> InsertarFormaPagoAsync(string desc) => InsertarAsync(desc);
        public System.Threading.Tasks.Task<bool> ModificarFormaPagoAsync(int id, string desc) => ModificarAsync(id, desc);
    }
}