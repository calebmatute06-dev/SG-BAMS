using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS.Login
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="SG_BAMS.Login.ClsLogin" />
    internal class ClsPasarUsuario : ClsLogin
    {

        /// <summary>
        /// Obtiene el identificador del usuario.
        /// </summary>
        /// <returns></returns>
        public int IdUsuario()
        {
            return idusuario;
        }
    }
}