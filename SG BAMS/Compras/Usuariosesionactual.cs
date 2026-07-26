using SG_BAMS.ComprasContratos;
using SG_BAMS.Login;
using SG_BAMS.LogicaNegocio.Login;

namespace SG_BAMS
{
    /// <summary>
    /// Implementación por defecto de IUsuarioSesion. Se investigó en el
    /// proyecto y se confirmó que "ClsPasarUsuario" no existe: el usuario
    /// autenticado en la sesión actual se guarda en el campo estático
    /// ClsLogin.idusuario, que ClsLogin.ValidarUsuario() llena cuando el
    /// login es exitoso. Esta clase simplemente expone ese valor detrás
    /// de IUsuarioSesion, para que ClsCompras (y cualquier otra clase)
    /// no dependa directamente de ClsLogin ni de un campo estático.
    /// </summary>
    public class UsuarioSesionActual : IUsuarioSesion
    {
        public int IdUsuario()
        {
            return ClsLogin.idusuario;
        }
    }
}