using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS
{
    public class ClsCargaCombos
    {
        private ClsConexion conexion = new ClsConexion();

        private DataTable ejecutarQuery(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                conexion.AbrirConexion();
                using (SqlDataAdapter da = new SqlDataAdapter(query, conexion.Conectar))
                {
                    da.Fill(dt);
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
            return ejecutarQuery("SELECT id_tipo_forma_pago, descripcion_forma_pago FROM Tipo_Forma_de_pago");
        }

        public DataTable ListarProveedoresActivos()
        {
            return ejecutarQuery("SELECT id_proveedor, nombre_proveedor FROM Proveedor WHERE id_estado = 1");
        }

        public string SugerirSiguienteID()
        {
            DataTable dt = ejecutarQuery("SELECT ISNULL(MAX(id_compra), 0) + 1 FROM Compra");

            if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
            {
                return dt.Rows[0][0].ToString();
            }
            return "1";
        }
    }
}
