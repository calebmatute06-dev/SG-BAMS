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
    public class ClsConexion
    {
        private String CadenaConexion = "Data Source = AutoBattDB.mssql.somee.com; " +
                                        "Initial catalog = AutoBattDB; " +
                                        "User ID = exobonnie_SQLLogin_1; " +
                                        "Password = w6et2uoghs;" +
                                        "TrustServerCertificate=True;";

        public SqlConnection Conectar = new SqlConnection();


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

        public void Cerrar()
        {
            if (Conectar.State == ConnectionState.Open)
            {
                Conectar.Close();
            }
        }
    }
}

