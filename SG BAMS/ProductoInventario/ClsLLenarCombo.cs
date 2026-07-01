using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Krypton.Toolkit;

namespace SG_BAMS.ProductoInventario
{
    /// <summary>
    /// Clase para llenar ComboBox usando solo Procedimientos Almacenados.
    /// </summary>
    public class ClsLlenarCombo
    {
        private readonly ClsRepositorioBaseDatos conexion = new ClsRepositorioBaseDatos();

        public void ConfigurarComboBox(KryptonComboBox combo, string tipoTabla)
        {
            ConfigurarComboBox(combo, tipoTabla, 0);
        }

        public void ConfigurarComboBox(KryptonComboBox combo, string tipoTabla, int idProveedorActual)
        {
            DataTable dt = ObtenerDatosCombo(tipoTabla, idProveedorActual);
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

            combo.SelectedIndex = 0;
        }

        private DataTable ObtenerDatosCombo(string tabla)
        {
            return ObtenerDatosCombo(tabla, 0);
        }

        private DataTable ObtenerDatosCombo(string tabla, int idProveedorActual)
        {
            DataTable dt = new DataTable();
            string nombrePA;

            switch (tabla)
            {
                case "Marca":
                    nombrePA = "sp_Combo_Marcas";
                    break;
                case "Tipo":
                    nombrePA = "sp_Combo_Tipos";
                    break;
                case "Modelo":
                    nombrePA = "sp_Combo_Modelos";
                    break;
                case "Estado":
                    nombrePA = "sp_Combo_Estados";
                    break;
                case "Proveedor":
                    nombrePA = "sp_Combo_ProveedoresActivos";
                    break;
                default:
                    throw new Exception("La tabla solicitada no está configurada.");
            }

            try
            {
                conexion.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand(nombrePA, conexion.Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (tabla == "Proveedor")
                    {
                        cmd.Parameters.AddWithValue("@idProveedorActual", idProveedorActual);
                    }

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