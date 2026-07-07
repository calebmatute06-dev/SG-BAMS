using Microsoft.Data.SqlClient;
using SG_BAMS;
using System.Data;
using System.Threading.Tasks;


namespace SG_BAMS.MenuPrincipal
{

    internal class ClsPerfil : ClsRepositorioBaseDatos
    {
        public async Task<DataTable> ObtenerPerfilDesdeVista(string nombreUsuario)
        {
            DataTable tablaUsuario = new DataTable();
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Usuario_ObtenerPerfil", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@usuario", nombreUsuario);
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        tablaUsuario.Load(reader);
                    }
                }
            }
            finally { Cerrar(); }
            return tablaUsuario;
        }

        public async Task ActualizarFotoUsuario(string nombreUsuario, byte[] imagenBytes)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Usuario_ActualizarFoto", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@usuario", nombreUsuario);
                    cmd.Parameters.Add("@foto", SqlDbType.VarBinary).Value = imagenBytes;
                    await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al subir imagen: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }
    }
}
    