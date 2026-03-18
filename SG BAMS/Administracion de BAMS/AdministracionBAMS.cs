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
    public partial class AdministracionBAMS : Form
    {
        public AdministracionBAMS()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }


        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Hide();
            SG_BAMS.Login.Login log = new SG_BAMS.Login.Login();
            log.Show();
        }


        private void btnMproducto_Click(object sender, EventArgs e)
        {
            frmMarcaProductos verMproducto = new frmMarcaProductos();
            verMproducto.Show();
        }


        private void btnMenuP_Click(object sender, EventArgs e)
        {
            MenuPrincipalAdm menuPrincipalAdm = new MenuPrincipalAdm();
            menuPrincipalAdm.Show();
            this.Hide();
        }

        private void btnFactura_Click(object sender, EventArgs e)
        {
            FacturasAdm facturas = new FacturasAdm();
            facturas.Show();
            this.Hide();
        }

        private void btnCompras_Click(object sender, EventArgs e)
        {
            Compras vercompras = new Compras();
            vercompras.Show();
            this.Hide();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            ClientesAdm clientes = new ClientesAdm();
            clientes.Show();
            this.Hide();
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            InventarioAdmin inventario = new InventarioAdmin();
            inventario.Show();
            this.Hide();
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            Proveedor.ProveedoresAdmin proveedores = new Proveedor.ProveedoresAdmin();
            proveedores.Show();
            this.Hide();
        }

        private void btnDeudores_Click(object sender, EventArgs e)
        {
            DeudoresAdmin deudoresAdm = new DeudoresAdmin();
            deudoresAdm.Show();
            this.Hide();
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            ReportesAdmin frmReportes = new ReportesAdmin();
            frmReportes.Show();
            this.Hide();

        }

        private void btnBitacora_Click(object sender, EventArgs e)
        {
            Bitacora.BitacoraAdmin bitacora = new Bitacora.BitacoraAdmin();
            bitacora.Show();
            this.Hide();
        }

        private void btnNoti_Click(object sender, EventArgs e)
        {
            NotificacionesAdmin notificaciones = new NotificacionesAdmin();
            notificaciones.Show();
        }

        private void btnPerfil_Click(object sender, EventArgs e)
        {
            Perfil perfil = new Perfil();
            perfil.Show();
        }

        private void AdministracionBAMS_Load(object sender, EventArgs e)
        {

        }

        private void btnVerUsuarios_Click(object sender, EventArgs e)
        {
            frmUsuarios usuarios = new frmUsuarios();
            usuarios.Show();
        }

        private void btnRoles_Click(object sender, EventArgs e)
        {
            frmRoles verRoles = new frmRoles();
            verRoles.Show();
        }

        private void btnTproducto_Click_1(object sender, EventArgs e)
        {
            frmTipoProducto verTproducto = new frmTipoProducto();
            verTproducto.Show();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            frmFormaPago verFormaPago = new frmFormaPago();
            verFormaPago.Show();
        }

        private void btnEstado_Click(object sender, EventArgs e)
        {
            frmEstado verEstado = new frmEstado();
            verEstado.Show();
        }

        private void btnMproducto_Click_1(object sender, EventArgs e)
        {
            frmMarcaProductos verMproducto = new frmMarcaProductos();
            verMproducto.Show();
        }

        private void btnMauto_Click(object sender, EventArgs e)
        {
            frmModeloAuto verMauto = new frmModeloAuto();
            verMauto.Show();
        }
    }
}
