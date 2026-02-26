using Microsoft.Data.SqlClient;
using SG_BAMS;
using System.Data;
using System.IO;
internal class ClsUsuario : ClsConexion
{
    public async Task<DataTable> ObtenerPerfilDesdeVista(string nombreUsuario)
    {
        DataTable tablaUsuario = new DataTable();
        try
        {
            AbrirConexion();
            
            string consultaSql = "SELECT nombre_usuario, descripcion_rol, imagen_usuario FROM vista_perfil_usuario WHERE nombre_usuario = @usuario";

            using (SqlCommand comandoSql = new SqlCommand(consultaSql, Conectar))
            {
                comandoSql.Parameters.AddWithValue("@usuario", nombreUsuario);
                using (SqlDataReader lectorDatos = await comandoSql.ExecuteReaderAsync())
                {
                    tablaUsuario.Load(lectorDatos);
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
            
            string consultaSql = "UPDATE Usuario SET imagen_usuario = @foto WHERE nombre_usuario = @usuario";

            using (SqlCommand comandoSql = new SqlCommand(consultaSql, Conectar))
            {
                
                comandoSql.Parameters.Add("@foto", SqlDbType.VarBinary).Value = imagenBytes;
                comandoSql.Parameters.AddWithValue("@usuario", nombreUsuario);

                await comandoSql.ExecuteNonQueryAsync();
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error al subir imagen a Somee: " + ex.Message);
        }
        finally
        {
            Cerrar();
        }
    }

}