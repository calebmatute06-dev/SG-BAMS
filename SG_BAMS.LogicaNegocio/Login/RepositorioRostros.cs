using System.IO;
using System.Linq;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Implementación del repositorio de rostros que busca archivos de imagen
    /// en el directorio de registros faciales del sistema.
    /// </summary>
    public class RepositorioRostros : IRepositorioRostros
    {
        private readonly string directorioRostros;

        /// <summary>
        /// Constructor del repositorio de rostros.
        /// </summary>
        /// <param name="directorioRostros">Ruta completa del directorio donde se almacenan las imágenes faciales.</param>
        public RepositorioRostros(string directorioRostros)
        {
            this.directorioRostros = directorioRostros;
        }

        /// <inheritdoc/>
        public bool TieneRegistroFacial(string nombreUsuario)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario) || !Directory.Exists(directorioRostros))
                return false;

            var archivos = Directory.GetFiles(directorioRostros, "*.jpg")
                .Where(f => Path.GetFileNameWithoutExtension(f) == nombreUsuario ||
                            Path.GetFileNameWithoutExtension(f).StartsWith(nombreUsuario + "_"))
                .ToList();

            return archivos.Count > 0;
        }
    }
}