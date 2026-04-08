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
    /// Clase encargada de cargar datos para combos y sugerencias en la interfaz.
    /// </summary>
    public class ClsCargaCombos
    {
        /// <summary>
        /// La conexión a la base de datos.
        /// </summary>
        private ClsConexion conexion = new ClsConexion();

        /// <summary>
        /// Ejecuta una consulta SQL y devuelve los resultados en un DataTable.
        /// </summary>
        /// <param name="query">La consulta SQL a ejecutar.</param>
        /// <returns>Un <see cref="DataTable"/> con los resultados de la consulta.</returns>
        /// <exception cref="System.Exception">Lanza una excepción si ocurre un error en la base de datos.</exception>
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
        /// Obtiene un listado de las formas de pago disponibles.
        /// </summary>
        /// <returns>Un <see cref="DataTable"/> con los tipos de forma de pago.</returns>
        public DataTable ListarFormasPago()
        {
            return ejecutarQuery("SELECT id_tipo_forma_pago, descripcion_forma_pago FROM Tipo_Forma_de_pago");
        }

        /// <summary>
        /// Obtiene un listado de los proveedores activos.
        /// </summary>
        /// <returns>Un <see cref="DataTable"/> con los proveedores activos.</returns>
        public DataTable ListarProveedoresActivos()
        {
            return ejecutarQuery("SELECT id_proveedor, nombre_proveedor FROM Proveedor WHERE id_estado = 1");
        }

        /// <summary>
        /// Sugiere el siguiente identificador para una nueva compra.
        /// </summary>
        /// <returns>Un <see cref="string"/> que representa el próximo ID disponible para la tabla Compra.</returns>
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