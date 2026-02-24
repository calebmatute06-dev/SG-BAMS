using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS.Administracion_de_BAMS.Usuarios
{
    internal class VerUsuarios : ClsConexion
    {
        public async Task<DataTable> VUsurios()
        {
            DataTable TablaUsuarios = new DataTable();
            try
            {
                AbrirConexion();

                string sqlQuery = "SELECT * FROM vista_ultimas_ventas";
                using (SqlCommand sqlCommand = new SqlCommand(sqlQuery, Conectar))
                {
                    using (SqlDataReader sqlReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        TablaUsuarios.Load(sqlReader);
                    }
                }
            }

            catch (Exception)
            {
                return null;
            }

            return TablaUsuarios;
        }
    }
}
