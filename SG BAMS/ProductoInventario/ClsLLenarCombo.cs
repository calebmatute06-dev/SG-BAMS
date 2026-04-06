using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace SG_BAMS.ProductoInventario
{
    public class ClsLlenarCombo
    {
        private ClsConexion conexion = new ClsConexion();

        public void ConfigurarComboBox(KryptonComboBox combo, string tipoTabla)
        {
            DataTable dt = ObtenerDatosCombo(tipoTabla);
            combo.DataSource = dt;

            switch (tipoTabla)
            {
                case "Marca":
                    combo.DisplayMember = "nombre_marca";
                    combo.ValueMember = "id_marca_producto";
                    break;
                case "Tipo":
                    combo.DisplayMember = "descripcion_producto";
                    combo.ValueMember = "id_tipo_producto";
                    break;
                case "Modelo":
                    combo.DisplayMember = "nombre_modelo_auto";
                    combo.ValueMember = "id_modelo_auto";
                    break;
                case "Estado":
                    combo.DisplayMember = "descripcion_estado";
                    combo.ValueMember = "id_estado";
                    break;
                case "Proveedor":
                    combo.DisplayMember = "nombre_proveedor";
                    combo.ValueMember = "id_proveedor";
                    break;
            }

            combo.SelectedIndex = -1;
        }

        private DataTable ObtenerDatosCombo(string tabla)
        {
            DataTable dt = new DataTable();
            string query = "";

            switch (tabla)
            {
                case "Marca":
                    query = "SELECT id_marca_producto, nombre_marca FROM Marca_producto";
                    break;
                case "Tipo":
                    query = "SELECT id_tipo_producto,descripcion_producto FROM Tipo_producto";
                    break;
                case "Modelo":
                    query = "SELECT id_modelo_auto, nombre_modelo_auto FROM Modelo_de_auto";
                    break;
                case "Estado":
                    query = "SELECT id_estado, descripcion_estado FROM Estado";
                    break;
                case "Proveedor":
                    query = "SELECT id_proveedor, nombre_proveedor FROM Proveedor";
                    break;
                default:
                    throw new Exception("La tabla solicitada no está configurada.");
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
                throw new Exception("Error al obtener datos para " + tabla + ": " + ex.Message);
            }
            finally
            {
                conexion.Cerrar();
            }
            return dt;
        }
    }
}