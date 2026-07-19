using System.Windows.Forms;

namespace SG_BAMS.Reporte
{
    /// <summary>
    /// Centraliza la apertura de las pantallas hacia las que navega
    /// ReportesAdmin. Antes cada botón de navegación instanciaba su
    /// formulario concreto directamente dentro del propio evento de clic
    /// (RA05), de forma inconsistente con btnProveedores_Click y
    /// btnBitacora_Click, que sí reciben sus dependencias por
    /// constructor. Aquí se agrupa ese patrón de "instanciar, mostrar y
    /// ocultar la pantalla actual" en un único lugar reutilizable.
    /// </summary>
    public interface IServicioNavegacion
    {
        void AbrirMenuPrincipal(Form actual);
        void AbrirFacturas(Form actual);
        void AbrirCompras(Form actual);
        void AbrirClientes(Form actual);
        void AbrirInventario(Form actual);
        void AbrirDeudores(Form actual);
    }
}
