using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace SG_BAMS
{
    /// <summary>
    /// 
    /// </summary>
    public class ClsRepositorioBaseDatos
    {
        /// <summary>
        /// La cadena de conexión
        /// </summary>
        private String CadenaConexion = "Data Source = AutoBattDB.mssql.somee.com; " +
                                        "Initial catalog = AutoBattDB; " +
                                        "User ID = exobonnie_SQLLogin_1; " +
                                        "Password = w6et2uoghs;" +
                                        "TrustServerCertificate=True;";
        /// <summary>
        /// La conexión
        /// </summary>
        public SqlConnection Conectar = new SqlConnection();


        /// <summary>
        /// Abre la conexión a la base de datos.
        /// </summary>
        /// <exception cref="System.Exception">Error de conexion a la base de datos: " + ex.Message</exception>
        public void AbrirConexion()
        {
            try
            {
                Conectar.ConnectionString = CadenaConexion;
                if (Conectar.State == ConnectionState.Closed)
                {
                    Conectar.Open();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error de conexion a la base de datos: " + ex.Message);
            }
        }

        /// <summary>
        /// Cierra la conexión a la base de datos.
        /// </summary>
        public void Cerrar()
        {
            if (Conectar.State == ConnectionState.Open)
            {
                Conectar.Close();
            }
        }
    }
}