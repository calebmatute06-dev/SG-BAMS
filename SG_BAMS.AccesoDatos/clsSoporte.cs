using Microsoft.Data.SqlClient;
using System;
using System.Data;
using SG_BAMS.AccesoDatos;
using SG_BAMS.Dominio.AdministracionBAMS;

namespace SG_BAMS.AccesoDatos
{
    /// <summary>
    /// Repositorio de datos para el módulo de reconocimiento facial.
    /// SRP: única responsabilidad — consultas a la base de datos relacionadas con usuarios faciales.
    /// La detección de rostros fue extraída a DetectorRostroService para cumplir SRP.
    /// DIP: implementa IUsuarioFacialRepository.
    /// </summary>
    public class clsSoporte : ClsRepositorioBaseDatos, IUsuarioFacialRepository
    {
        /// <inheritdoc/>
        public DataTable ObtenerUsuarios()
        {
            DataTable dt = new DataTable();
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("PA_CargarUsuarios", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener usuarios para reconocimiento facial: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
            return dt;
        }

     
    }
}