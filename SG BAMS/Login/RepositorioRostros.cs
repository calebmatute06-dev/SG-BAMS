using System.IO;
using System.Linq;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Implementación de IRepositorioRostros que busca archivos de imagen
    /// en el directorio de rostros del sistema.
    /// </summary>
    public class RepositorioRostros : IRepositorioRostros
    {
        private readonly string _directorioRostros;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="directorioRostros">Ruta del directorio de rostros.</param>
        public RepositorioRostros(string directorioRostros)
        {
            _directorioRostros = directorioRostros;
        }

        /// <inheritdoc/>
        public bool TieneRegistroFacial(string nombreUsuario)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario) || !Directory.Exists(_directorioRostros))
                return false;

            var archivos = Directory.GetFiles(_directorioRostros, "*.jpg")
                .Where(f => Path.GetFileNameWithoutExtension(f) == nombreUsuario ||
                            Path.GetFileNameWithoutExtension(f).StartsWith(nombreUsuario + "_"))
                .ToList();

            return archivos.Count > 0;
        }
    }
}