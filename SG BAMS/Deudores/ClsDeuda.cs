using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SG_BAMS
{
    /// <summary>
    /// Clase para listar deudores usando solo Procedimientos Almacenados.
    /// </summary>
    internal class ClsDeuda : ClsRepositorioBaseDatos
    {
        public DataTable ListarDeudores()
        {
            DataTable tablaDeudores = new DataTable();
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_vista_lista_deudores", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter adaptador = new SqlDataAdapter(cmd))
                    {
                        adaptador.Fill(tablaDeudores);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar deudores: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
            return tablaDeudores;
        }
    }
}