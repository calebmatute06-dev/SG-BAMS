namespace SG_BAMS.Login
{
    /// <summary>
    /// Define el contrato para verificar registros faciales de usuarios.
    /// </summary>
    public interface IRepositorioRostros
    {
        /// <summary>
        /// Verifica si un usuario tiene registro facial.
        /// </summary>
        /// <param name="nombreUsuario">Nombre del usuario a verificar.</param>
        /// <returns>True si existen archivos de imagen facial para ese usuario.</returns>
        bool TieneRegistroFacial(string nombreUsuario);
    }
}