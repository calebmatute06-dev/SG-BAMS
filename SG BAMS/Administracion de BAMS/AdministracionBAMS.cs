using SG_BAMS.Bitacora;
using SG_BAMS.Proveedor;
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
            this.StartPosition = FormStartPosition.CenterScreen;
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

        private void btnMenu_Click(object sender, EventArgs e)
        {
            MenuPrincipalAdm MPA = new MenuPrincipalAdm();
            MPA.Show();
            this.Hide();
        }

        private void btnFacturas_Click(object sender, EventArgs e)
        {
            FacturasAdm FA = new FacturasAdm();
            FA.Show();
            this.Hide();
        }

        private void btnCompra_Click(object sender, EventArgs e)
        {
            Compras CF = new Compras();
            CF.Show();
            this.Hide();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            ClientesAdm CA = new ClientesAdm();
            CA.Show();
            this.Hide();
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            InventarioAdmin IA = new InventarioAdmin();
            IA.Show();
            this.Hide();
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            ProveedoresAdmin PA = new ProveedoresAdmin();
            PA.Show();
            this.Hide();
        }

        private void btnDeudores_Click(object sender, EventArgs e)
        {
            DeudoresAdmin DA = new DeudoresAdmin();
            DA.Show();
            this.Hide();
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            ReportesAdmin RA = new ReportesAdmin();
            RA.Show();
            this.Hide();
        }

        private void btnBitacora_Click(object sender, EventArgs e)
        {
            BitacoraAdmin BA = new BitacoraAdmin();
            BA.Show();
            this.Hide();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Login.Login login = new Login.Login();
            login.Show();
            this.Close();
        }

        private void btnPerfil_Click(object sender, EventArgs e)
        {
            Perfil perfil = new Perfil();
            perfil.Show();
        }
    }
}
