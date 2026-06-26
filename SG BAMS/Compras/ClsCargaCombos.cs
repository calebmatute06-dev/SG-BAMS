using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace SG_BAMS
{
    /// <summary>
    /// Clase para cargar combos usando solo Procedimientos Almacenados.
    /// </summary>
    public class ClsCargaCombos
    {
        private readonly ClsConexion conexion = new ClsConexion();

        private DataTable EjecutarPA(string nombrePA)
        {
            DataTable dt = new DataTable();
            try
            {
                conexion.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand(nombrePA, conexion.Conectar))
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
                throw new Exception("Error en la base de datos: " + ex.Message);
            }
            finally
            {
                conexion.Cerrar();
            }
            return dt;
        }

        public DataTable ListarFormasPago()
        {
            return EjecutarPA("sp_FormasPago_Listar");
        }

        public DataTable ListarProveedoresActivos()
        {
            return EjecutarPA("sp_Proveedores_Activos");
        }

        public string SugerirSiguienteID()
        {
            DataTable dt = EjecutarPA("sp_Compra_SugerirSiguienteID");
            if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
                return dt.Rows[0][0].ToString();
            return "1";
        }
    }
}