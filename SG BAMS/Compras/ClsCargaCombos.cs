using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS
{
    /// <summary>
    /// 
    /// </summary>
    public class ClsCargaCombos
    {
        /// <summary>
        /// The conexion
        /// </summary>
        private ClsConexion conexion = new ClsConexion();

        /// <summary>
        /// Ejecutars the query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error en la base de datos: " + ex.Message</exception>
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

        /// <summary>
        /// Listars the formas pago.
        /// </summary>
        /// <returns></returns>
        public DataTable ListarFormasPago()
        {
            return ejecutarQuery("SELECT id_tipo_forma_pago, descripcion_forma_pago FROM Tipo_Forma_de_pago");
        }

        /// <summary>
        /// Listars the proveedores activos.
        /// </summary>
        /// <returns></returns>
        public DataTable ListarProveedoresActivos()
        {
            return ejecutarQuery("SELECT id_proveedor, nombre_proveedor FROM Proveedor WHERE id_estado = 1");
        }

        /// <summary>
        /// Sugerirs the siguiente identifier.
        /// </summary>
        /// <returns></returns>
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
