using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SG_BAMS.ProductoInventario
{
    public class ClsLlenarCombo
    {
        private ClsConexion conexion = new ClsConexion();

        public DataTable ObtenerDatosCombo(string tabla)
        {
            DataTable dt = new DataTable();
            string query = "";

            // Definimos la consulta según la tabla que necesitemos
            // Es vital que el primer campo sea el ID y el segundo el Nombre/Descripción
            switch (tabla)
            {
                case "Marca":
                    query = "SELECT id_marca_producto, nombre_marca FROM Marca_producto";
                    break;
                case "Tipo":
                    query = "SELECT id_tipo_producto, descripcion_forma_pago FROM Tipo_producto";
                    break;
                case "Modelo":
                    query = "SELECT id_modelo_auto, nombre_modelo_auto FROM Modelo_de_auto";
                    break;
            }

            try
            {
                conexion.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand(query, conexion.Conectar))
                {
                    using (SqlDataReader leer = cmd.ExecuteReader())
                    {
                        dt.Load(leer);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al llenar combo " + tabla + ": " + ex.Message);
            }
            finally
            {
                conexion.Cerrar();
            }
            return dt;
        }
    }
}