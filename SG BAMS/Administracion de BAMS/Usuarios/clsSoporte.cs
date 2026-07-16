using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace SG_BAMS.Administracion_de_BAMS.Usuarios
{
    /// <summary>
    /// Repositorio de datos para el módulo de reconocimiento facial.
    /// SRP: única responsabilidad — consultas a la base de datos relacionadas con usuarios faciales.
    /// La detección de rostros fue extraída a DetectorRostroService para cumplir SRP.
    /// DIP: implementa IUsuarioFacialRepository.
    /// </summary>
    internal class clsSoporte : ClsRepositorioBaseDatos, IUsuarioFacialRepository
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

        /// <summary>
        /// Alias de compatibilidad. Delega a DetectorRostroService.
        /// </summary>
        public static void InicializarDirectorio()
        {
            new DetectorRostroService().InicializarDirectorio();
        }
    }
}