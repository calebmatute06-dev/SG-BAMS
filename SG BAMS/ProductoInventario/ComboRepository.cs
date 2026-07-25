using Microsoft.Data.SqlClient;
using System;
using System.Data;
using SG_BAMS.AccesoDatos;

namespace SG_BAMS.ProductoInventario
{
    /// <summary>
    /// Implementación de acceso a datos para los catálogos de combo del módulo de Inventario.
    /// Única responsabilidad: ejecutar el procedimiento almacenado correspondiente al catálogo
    /// solicitado. No conoce nada sobre controles de interfaz gráfica
    /// (a diferencia de la antigua ClsLlenarCombo, ver auditoría SOLID, hallazgos LC01/LC03).
    /// </summary>
    internal class ComboRepository : ClsRepositorioBaseDatos, IComboRepository
    {
        /// <inheritdoc />
        public DataTable ObtenerDatos(string tipoTabla, int idProveedorActual = 0)
        {
            ComboCatalogo config = ComboCatalogo.Obtener(tipoTabla);
            DataTable dt = new DataTable();

            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand(config.NombreProcedimiento, Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (config.RequiereIdProveedorActual)
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
                throw new ApplicationException($"Error al obtener datos para {tipoTabla}: {ex.Message}", ex);
            }
            finally
            {
                Cerrar();
            }

            return dt;
        }
    }
}
