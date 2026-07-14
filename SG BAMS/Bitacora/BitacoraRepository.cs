
using System.Data;
using Microsoft.Data.SqlClient;
using System;

namespace SG_BAMS.Bitacora;

/// <summary>
/// Implementación de acceso a datos de la bitácora sobre SQL Server.
/// Única responsabilidad: obtener los registros desde la base de datos,
/// sin conocer nada sobre controles de interfaz gráfica.
/// </summary>
internal sealed class BitacoraRepository : ClsRepositorioBaseDatos, IBitacoraRepository
{
    /// <inheritdoc />
    public async Task<DataTable> ObtenerRegistrosAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            AbrirConexion();
        }
        catch (FormatException ex)
        {
            // Si explota aquí, el problema está en cómo se arma la cadena de conexión
            // o algún parámetro de configuración (timeout, puerto, etc.) dentro de
            // ClsRepositorioBaseDatos.AbrirConexion(), no en la consulta en sí.
            throw new ApplicationException(
                "La cadena de conexión o un parámetro de configuración tiene un formato inválido. " +
                "Revisa AbrirConexion() en ClsRepositorioBaseDatos.", ex);
        }

        if (Conectar is null || Conectar.State != ConnectionState.Open)
            throw new InvalidOperationException("La conexión a la base de datos no se pudo abrir.");

        try
        {
            using var cmd = new SqlCommand("sp_Bitacora_Listar", Conectar)
            {
                CommandType = CommandType.StoredProcedure
            };
            using var adapter = new SqlDataAdapter(cmd);

            var dt = new DataTable();
            await Task.Run(() => adapter.Fill(dt), cancellationToken);
            return dt;
        }
        catch (FormatException ex)
        {
            // Si explota aquí en vez de arriba, el problema está en los datos
            // que devuelve sp_Bitacora_Listar (una columna con formato inconsistente).
            throw new ApplicationException(
                "Los datos devueltos por 'sp_Bitacora_Listar' tienen un formato inválido. " +
                "Revisa si alguna columna numérica o de fecha llega como cadena vacía.", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Error al cargar los datos de la bitácora.", ex);
        }
        finally
        {
            Cerrar();
        }
    }
}