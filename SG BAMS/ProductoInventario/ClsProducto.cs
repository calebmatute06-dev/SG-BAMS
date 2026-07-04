using System;
using System.Data;
using Microsoft.Data.SqlClient;
using SG_BAMS.ProductoInventario.DTO;

namespace SG_BAMS.ProductoInventario
{
    /// <summary>
    /// Clase para administrar productos usando solo Procedimientos Almacenados.
    /// </summary>
    internal class ClsProducto : ClsRepositorioBaseDatos
    {
        /// <summary>
        /// Muestra el listado completo de productos usando PA.
        /// </summary>
        public DataTable MostrarProductosCompleto()
        {
            DataTable tabla = new DataTable();
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Vista_Productos_Detallada", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader leer = cmd.ExecuteReader())
                    {
                        tabla.Load(leer);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }

            return tabla;
        }

        /// <summary>
        /// Busca productos por filtro usando PA.
        /// </summary>
        public DataTable BuscarProductos(string filtro)
        {
            DataTable dt = new DataTable();

            try
            {
                AbrirConexion();

                using (SqlCommand cmd = new SqlCommand("sp_BuscarProductos", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@filtro", filtro);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar procedimiento: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }

            return dt;
        }

        /// <summary>
        /// Inserta un nuevo producto usando solo Procedimientos Almacenados,
        /// a partir de los datos contenidos en el DTO.
        /// </summary>
        /// <param name="dto">Datos del producto a registrar.</param>
        public void EjecutarInsercion(ProductoDTO dto)
        {
            try
            {
                AbrirConexion();

                using (SqlCommand cmd = new SqlCommand("PA_insertar_producto", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre_producto", dto.Nombre);
                    cmd.Parameters.AddWithValue("@id_marca_producto", dto.IdMarca);
                    cmd.Parameters.AddWithValue("@id_tipo_producto", dto.IdTipo);
                    cmd.Parameters.AddWithValue("@id_modelo_auto", dto.IdModelo);
                    cmd.Parameters.Add("@precio_venta", SqlDbType.Money).Value = dto.Precio;
                    cmd.Parameters.AddWithValue("@codigo_barra", dto.CodigoBarra);
                    cmd.Parameters.AddWithValue("@id_proveedor", dto.IdProveedor);
                    cmd.Parameters.AddWithValue("@stock", dto.Stock);

                    cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                Cerrar();
            }
        }
        /// <summary>
        /// Actualiza un producto usando solo Procedimientos Almacenados,
        /// a partir de los datos contenidos en el DTO.
        /// </summary>
        /// <param name="dto">Datos actualizados del producto, incluyendo su IdProducto.</param>
        public void EjecutarActualizacion(ProductoDTO dto)
        {
            try
            {
                AbrirConexion();

                using (SqlCommand cmd = new SqlCommand("PA_actualizar_producto", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_producto", dto.IdProducto);
                    cmd.Parameters.AddWithValue("@nombre_producto", dto.Nombre);
                    cmd.Parameters.AddWithValue("@id_marca_producto", dto.IdMarca);
                    cmd.Parameters.AddWithValue("@id_tipo_producto", dto.IdTipo);
                    cmd.Parameters.AddWithValue("@id_modelo_auto", dto.IdModelo);
                    cmd.Parameters.AddWithValue("@id_estado", dto.IdEstado);
                    cmd.Parameters.Add("@precio_venta", SqlDbType.Money).Value = dto.Precio;
                    cmd.Parameters.AddWithValue("@codigo_barra", dto.CodigoBarra);
                    cmd.Parameters.AddWithValue("@id_proveedor", dto.IdProveedor);
                    cmd.Parameters.AddWithValue("@stock", dto.Stock);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar producto e inventario: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }

        public bool ExisteProductoMarcaProveedor(string nombre, int idMarca, int idProveedor)
        {
            try
            {
                AbrirConexion();

                using (SqlCommand cmd = new SqlCommand("sp_Producto_ExisteEnOtros", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@idMarca", idMarca);
                    cmd.Parameters.AddWithValue("@idProveedor", idProveedor);
                    cmd.Parameters.AddWithValue("@id", 0);

                    int conteo = Convert.ToInt32(cmd.ExecuteScalar());
                    return conteo > 0;
                }
            }
            finally
            {
                Cerrar();
            }
        }

        public bool ExisteProductoEnOtros(int idActual, string nombre, int idMarca, int idProveedor)
        {
            try
            {
                AbrirConexion();

                using (SqlCommand cmd = new SqlCommand("sp_Producto_ExisteEnOtros", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@idMarca", idMarca);
                    cmd.Parameters.AddWithValue("@idProveedor", idProveedor);
                    cmd.Parameters.AddWithValue("@id", idActual);

                    int conteo = Convert.ToInt32(cmd.ExecuteScalar());
                    return conteo > 0;
                }
            }
            finally
            {
                Cerrar();
            }
        }

        public bool ExisteCodigoBarra(string codigo)
        {
            try
            {
                AbrirConexion();

                using (SqlCommand cmd = new SqlCommand("sp_Producto_ExisteCodigoEnOtros", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codigo", codigo);
                    cmd.Parameters.AddWithValue("@id", 0);

                    int conteo = Convert.ToInt32(cmd.ExecuteScalar());
                    return conteo > 0;
                }
            }
            finally
            {
                Cerrar();
            }
        }

        public bool ExisteCodigoEnOtros(int idActual, string codigo)
        {
            try
            {
                AbrirConexion();

                using (SqlCommand cmd = new SqlCommand("sp_Producto_ExisteCodigoEnOtros", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codigo", codigo);
                    cmd.Parameters.AddWithValue("@id", idActual);

                    int conteo = Convert.ToInt32(cmd.ExecuteScalar());
                    return conteo > 0;
                }
            }
            finally
            {
                Cerrar();
            }
        }
    }
}