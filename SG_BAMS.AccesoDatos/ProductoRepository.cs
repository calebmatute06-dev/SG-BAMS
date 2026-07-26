using Microsoft.Data.SqlClient;
using SG_BAMS.ProductoInventario.DTO;
using System;
using System.Data;
using SG_BAMS.AccesoDatos;

namespace SG_BAMS.ProductoInventario
{
    /// <summary>
    /// Implementación de acceso a datos del catálogo de productos sobre SQL Server,
    /// usando solo Procedimientos Almacenados. Única responsabilidad: ejecutar las
    /// operaciones de datos de producto, sin conocer nada sobre controles de interfaz
    /// gráfica ni sobre reglas de validación de formulario.
    /// Reemplaza a la antigua clase ClsProducto (ver auditoría SOLID, hallazgos CP01-CP04).
    /// </summary>
    public class ProductoRepository : ClsRepositorioBaseDatos, IProductoRepository
    {
        /// <inheritdoc />
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
                throw new ApplicationException("Error al obtener la lista de productos.", ex);
            }
            finally
            {
                Cerrar();
            }

            return tabla;
        }

        /// <inheritdoc />
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
                throw new ApplicationException("Error al buscar productos.", ex);
            }
            finally
            {
                Cerrar();
            }

            return dt;
        }

        /// <inheritdoc />
        public bool ExisteProductoDuplicado(string nombre, int idMarca, int idProveedor, int idExcluir = 0)
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
                    cmd.Parameters.AddWithValue("@id", idExcluir);

                    int conteo = Convert.ToInt32(cmd.ExecuteScalar());
                    return conteo > 0;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al verificar el producto duplicado.", ex);
            }
            finally
            {
                Cerrar();
            }
        }

        /// <inheritdoc />
        public bool ExisteCodigoBarraDuplicado(string codigo, int idExcluir = 0)
        {
            try
            {
                AbrirConexion();

                using (SqlCommand cmd = new SqlCommand("sp_Producto_ExisteCodigoEnOtros", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codigo", codigo);
                    cmd.Parameters.AddWithValue("@id", idExcluir);

                    int conteo = Convert.ToInt32(cmd.ExecuteScalar());
                    return conteo > 0;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al verificar el código de barra duplicado.", ex);
            }
            finally
            {
                Cerrar();
            }
        }

        /// <inheritdoc />
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
            catch (Exception ex)
            {
                throw new ApplicationException("Error al insertar el producto.", ex);
            }
            finally
            {
                Cerrar();
            }
        }

        /// <inheritdoc />
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
                throw new ApplicationException("Error al actualizar producto e inventario.", ex);
            }
            finally
            {
                Cerrar();
            }
        }
    }
}
