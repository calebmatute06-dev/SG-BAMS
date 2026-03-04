using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SG_BAMS.ProductoInventario
{
    internal class ClsLLenarCombo
    {
        private ClsConexion conexion = new ClsConexion();

        // Método privado para evitar repetir código de llenado
        private DataTable Consultar(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                // Usamos SqlDataAdapter que es el más robusto para llenar ComboBox
                using (SqlDataAdapter da = new SqlDataAdapter(query, conexion.Conectar))
                {
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar datos desde SQL: " + ex.Message);
            }
            return dt;
        }

        // 1. Llenar Marca (Tabla: Marca_producto)
        public DataTable LlenarMarca()
        {
            return Consultar("SELECT id_marca_producto, nombre_marca FROM Marca_producto");
        }

        // 2. Llenar Tipo (Tabla: Tipo_producto) 
        // ¡OJO! En tu SQL pusiste 'descripcion_forma_pago' como columna de nombre aquí.
        public DataTable LlenarTipo()
        {
            return Consultar("SELECT id_tipo_producto, descripcion_forma_pago FROM Tipo_producto");
        }

        // 3. Llenar Modelo (Tabla: Modelo_de_auto)
        public DataTable LlenarModelo()
        {
            return Consultar("SELECT id_modelo_auto, nombre_modelo_auto FROM Modelo_de_auto");
        }
    }
}