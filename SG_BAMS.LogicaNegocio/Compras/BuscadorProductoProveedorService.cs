using System;
using System.Data;

namespace SG_BAMS.ComprasContratos
{
    /// <summary>
    /// Resultado de buscar un código de barra escaneado dentro del catálogo
    /// de productos de uno o varios proveedores.
    /// </summary>
    public class ResultadoBusquedaCodigoBarra
    {
        public bool Encontrado { get; set; }
        public int IdProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }

        public static ResultadoBusquedaCodigoBarra NoEncontrado() =>
            new ResultadoBusquedaCodigoBarra { Encontrado = false };
    }

    /// <summary>
    /// Contrato para localizar, a partir de un código de barra escaneado,
    /// a qué producto y a qué proveedor pertenece — sin depender de la UI.
    /// </summary>
    public interface IBuscadorProductoProveedorService
    {
        /// <summary>Busca el código dentro del catálogo de un proveedor puntual.</summary>
        ResultadoBusquedaCodigoBarra BuscarEnProveedor(IComprasRepository comprasRepo, int idProveedor, string codigoBarra);

        /// <summary>Recorre todos los proveedores activos hasta encontrar a cuál pertenece el código.</summary>
        ResultadoBusquedaCodigoBarra BuscarEnCualquierProveedor(IComprasRepository comprasRepo, DataTable proveedoresActivos, string codigoBarra);
    }

    /// <summary>
    /// Implementación que reutiliza el método ya existente
    /// IComprasRepository.ObtenerProductosPorProveedor para hacer el cruce
    /// entre el código de barra escaneado y el proveedor/producto al que
    /// pertenece. No agrega ninguna consulta ni procedimiento nuevo a la
    /// base de datos: solo reorganiza datos que la aplicación ya trae.
    /// </summary>
    public class BuscadorProductoProveedorService : IBuscadorProductoProveedorService
    {
        public ResultadoBusquedaCodigoBarra BuscarEnProveedor(IComprasRepository comprasRepo, int idProveedor, string codigoBarra)
        {
            if (comprasRepo == null || string.IsNullOrWhiteSpace(codigoBarra))
                return ResultadoBusquedaCodigoBarra.NoEncontrado();

            string codigoLimpio = codigoBarra.Trim();
            DataTable productos = comprasRepo.ObtenerProductosPorProveedor(idProveedor);

            if (productos == null || !productos.Columns.Contains("codigo_barra") || !productos.Columns.Contains("id_producto"))
                return ResultadoBusquedaCodigoBarra.NoEncontrado();

            foreach (DataRow fila in productos.Rows)
            {
                string codigoFila = fila["codigo_barra"]?.ToString().Trim();
                if (!string.IsNullOrEmpty(codigoFila) && codigoFila == codigoLimpio)
                {
                    return new ResultadoBusquedaCodigoBarra
                    {
                        Encontrado = true,
                        IdProveedor = idProveedor,
                        IdProducto = Convert.ToInt32(fila["id_producto"]),
                        NombreProducto = productos.Columns.Contains("DisplayFull")
                            ? fila["DisplayFull"].ToString()
                            : fila["id_producto"].ToString()
                    };
                }
            }

            return ResultadoBusquedaCodigoBarra.NoEncontrado();
        }

        public ResultadoBusquedaCodigoBarra BuscarEnCualquierProveedor(IComprasRepository comprasRepo, DataTable proveedoresActivos, string codigoBarra)
        {
            if (proveedoresActivos == null || !proveedoresActivos.Columns.Contains("id_proveedor"))
                return ResultadoBusquedaCodigoBarra.NoEncontrado();

            foreach (DataRow filaProveedor in proveedoresActivos.Rows)
            {
                int idProveedor = Convert.ToInt32(filaProveedor["id_proveedor"]);
                var resultado = BuscarEnProveedor(comprasRepo, idProveedor, codigoBarra);

                if (resultado.Encontrado)
                {
                    resultado.NombreProveedor = proveedoresActivos.Columns.Contains("nombre_proveedor")
                        ? filaProveedor["nombre_proveedor"].ToString()
                        : string.Empty;
                    return resultado;
                }
            }

            return ResultadoBusquedaCodigoBarra.NoEncontrado();
        }
    }
}