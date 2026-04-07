using SG_BAMS.Reporte;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class AdministracionBAMS : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdministracionBAMS"/> class.
        /// </summary>
        public AdministracionBAMS()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the label1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void label1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the label2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void label2_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the label4 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void label4_Click(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// Handles the Click event of the btnCerrarSesion control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Hide();
            SG_BAMS.Login.Login log = new SG_BAMS.Login.Login();
            log.Show();
        }


        /// <summary>
        /// Handles the Click event of the btnMproducto control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnMproducto_Click(object sender, EventArgs e)
        {
            frmMarcaProductos verMproducto = new frmMarcaProductos();
            verMproducto.Show();
        }


        /// <summary>
        /// Handles the Click event of the btnMenuP control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnMenuP_Click(object sender, EventArgs e)
        {
            MenuPrincipalAdm menuPrincipalAdm = new MenuPrincipalAdm();
            menuPrincipalAdm.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles the Click event of the btnFactura control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnFactura_Click(object sender, EventArgs e)
        {
            FacturasAdm facturas = new FacturasAdm();
            facturas.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles the Click event of the btnCompras control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnCompras_Click(object sender, EventArgs e)
        {
            Compras vercompras = new Compras();
            vercompras.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles the Click event of the btnClientes control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnClientes_Click(object sender, EventArgs e)
        {
            ClientesAdm clientes = new ClientesAdm();
            clientes.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles the Click event of the btnInventario control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnInventario_Click(object sender, EventArgs e)
        {
            InventarioAdmin inventario = new InventarioAdmin();
            inventario.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles the Click event of the btnProveedores control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnProveedores_Click(object sender, EventArgs e)
        {
            Proveedor.ProveedoresAdmin proveedores = new Proveedor.ProveedoresAdmin();
            proveedores.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles the Click event of the btnDeudores control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnDeudores_Click(object sender, EventArgs e)
        {
            DeudoresAdmin deudoresAdm = new DeudoresAdmin();
            deudoresAdm.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles the Click event of the btnReporte control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnReporte_Click(object sender, EventArgs e)
        {
            ReportesAdmin frmReportes = new ReportesAdmin();
            frmReportes.Show();
            this.Hide();

        }

        /// <summary>
        /// Handles the Click event of the btnBitacora control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnBitacora_Click(object sender, EventArgs e)
        {
            Bitacora.BitacoraAdmin bitacora = new Bitacora.BitacoraAdmin();
            bitacora.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles the Click event of the btnNoti control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnNoti_Click(object sender, EventArgs e)
        {
            NotificacionesAdmin notificaciones = new NotificacionesAdmin();
            notificaciones.Show();
        }

        /// <summary>
        /// Handles the Click event of the btnPerfil control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnPerfil_Click(object sender, EventArgs e)
        {
            Perfil perfil = new Perfil();
            perfil.Show();
        }

        /// <summary>
        /// Handles the Load event of the AdministracionBAMS control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void AdministracionBAMS_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the btnVerUsuarios control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnVerUsuarios_Click(object sender, EventArgs e)
        {
            frmUsuarios usuarios = new frmUsuarios();
            usuarios.Show();
        }

        /// <summary>
        /// Handles the Click event of the btnRoles control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnRoles_Click(object sender, EventArgs e)
        {
            frmRoles verRoles = new frmRoles();
            verRoles.Show();
        }

        /// <summary>
        /// Handles the 1 event of the btnTproducto_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnTproducto_Click_1(object sender, EventArgs e)
        {
            frmTipoProducto verTproducto = new frmTipoProducto();
            verTproducto.Show();
        }

        /// <summary>
        /// Handles the Click event of the kryptonButton1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            frmFormaPago verFormaPago = new frmFormaPago();
            verFormaPago.Show();
        }

        /// <summary>
        /// Handles the Click event of the btnEstado control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnEstado_Click(object sender, EventArgs e)
        {
            frmEstado verEstado = new frmEstado();
            verEstado.Show();
        }

        /// <summary>
        /// Handles the 1 event of the btnMproducto_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnMproducto_Click_1(object sender, EventArgs e)
        {
            frmMarcaProductos verMproducto = new frmMarcaProductos();
            verMproducto.Show();
        }

        /// <summary>
        /// Handles the Click event of the btnMauto control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnMauto_Click(object sender, EventArgs e)
        {
            frmModeloAuto verMauto = new frmModeloAuto();
            verMauto.Show();
        }
    }
}
