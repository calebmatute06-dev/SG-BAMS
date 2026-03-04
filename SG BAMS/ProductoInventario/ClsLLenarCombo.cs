using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SG_BAMS.ProductoInventario
{
    internal class ClsLLenarCombo
    {
        private ClsConexion conexion = new ClsConexion();

        private DataTable Consultar(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                // Usamos el objeto Conectar de tu ClsConexion
                using (SqlDataAdapter da = new SqlDataAdapter(sql, conexion.Conectar))
                {
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar: " + ex.Message);
            }
            return dt;
        }

        // 1. Tabla: Marca_producto | Columnas: id_marca_producto, nombre_marca
        public DataTable GetMarcas() => Consultar("SELECT id_marca_producto, nombre_marca FROM Marca_producto");

        // 2. Tabla: Tipo_producto | Columnas: id_tipo_producto, descripcion_forma_pago
        public DataTable GetTipos() => Consultar("SELECT id_tipo_producto, descripcion_forma_pago FROM Tipo_producto");

        // 3. Tabla: Modelo_de_auto | Columnas: id_modelo_auto, nombre_modelo_auto
        public DataTable GetModelos() => Consultar("SELECT id_modelo_auto, nombre_modelo_auto FROM Modelo_de_auto");
    }
}