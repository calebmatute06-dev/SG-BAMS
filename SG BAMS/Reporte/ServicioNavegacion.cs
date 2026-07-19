using System.Windows.Forms;

namespace SG_BAMS.Reporte
{
    /// <summary>
    /// Implementación por defecto de IServicioNavegacion. Si en el futuro
    /// estas pantallas también reciben sus dependencias por constructor
    /// (como ya ocurre con ProveedoresAdmin y BitacoraAdmin), solo hay que
    /// tocar esta clase, no cada formulario que navega hacia ellas.
    /// </summary>
    public class ServicioNavegacion : IServicioNavegacion
    {
        public void AbrirMenuPrincipal(Form actual) => AbrirYOcultar(new MenuPrincipalAdm(), actual);
        public void AbrirFacturas(Form actual) => AbrirYOcultar(new FacturasAdm(), actual);
        public void AbrirCompras(Form actual) => AbrirYOcultar(new Compras(), actual);
        public void AbrirClientes(Form actual) => AbrirYOcultar(new ClientesAdm(), actual);
        public void AbrirInventario(Form actual) => AbrirYOcultar(new InventarioAdmin(), actual);
        public void AbrirDeudores(Form actual) => AbrirYOcultar(new DeudoresAdmin(), actual);

        private void AbrirYOcultar(Form destino, Form actual)
        {
            destino.Show();
            actual.Hide();
        }
    }
}
