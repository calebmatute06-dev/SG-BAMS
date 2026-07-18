namespace SG_BAMS.Login
{
    /// <summary>
    /// Define el contrato para la verificación de registros faciales de usuarios.
    /// </summary>
    public interface IRepositorioRostros
    {
        /// <summary>
        /// Verifica si un usuario dispone de imágenes de registro facial en el sistema.
        /// </summary>
        /// <param name="nombreUsuario">Nombre del usuario a verificar.</param>
        /// <returns>True si existen archivos de imagen facial asociados al usuario.</returns>
        bool TieneRegistroFacial(string nombreUsuario);
    }
}