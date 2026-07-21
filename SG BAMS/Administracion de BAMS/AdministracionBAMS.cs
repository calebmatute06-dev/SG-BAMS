using SG_BAMS.Administracion_de_BAMS;
using SG_BAMS.Administracion_de_BAMS.Clasificacion;
using SG_BAMS.Administracion_de_BAMS.Usuarios;
using SG_BAMS.Bitacora;
using SG_BAMS.Proveedor;
using SG_BAMS.Reporte;
using System;
using System.Windows.Forms;

namespace SG_BAMS
{
    

    public partial class AdministracionBAMS : Form
    {
        public AdministracionBAMS()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void AdministracionBAMS_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void AbrirOEnfocarFormulario(Form formulario)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f.GetType() == formulario.GetType())
                {
                    formulario.Dispose();
                    if (f.WindowState == FormWindowState.Minimized)
                        f.WindowState = FormWindowState.Normal;
                    f.BringToFront();
                    f.Focus();
                    return;
                }
            }
            formulario.Show();
        }

        // ---- Catálogos ----

        private void btnVerUsuarios_Click(object sender, EventArgs e)
        {
            AbrirOEnfocarFormulario(new frmUsuarios(new clsUsuario()));
        }

        private void btnClasificacion_Click(object sender, EventArgs e)
        {
            AbrirOEnfocarFormulario(new Clasificacion(new clsClasificacion()));
        }

        // Los siguientes formularios aún no tienen constructor con repositorio
        private void btnRoles_Click(object sender, EventArgs e)
        {
            AbrirOEnfocarFormulario(new frmRoles());
        }

        private void btnTproducto_Click_1(object sender, EventArgs e)
        {
            AbrirOEnfocarFormulario(new frmTipoProducto());
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            AbrirOEnfocarFormulario(new frmFormaPago());
        }

        private void btnEstado_Click(object sender, EventArgs e)
        {
            AbrirOEnfocarFormulario(new frmEstado());
        }

        private void btnMproducto_Click_1(object sender, EventArgs e)
        {
            AbrirOEnfocarFormulario(new frmMarcaProductos());
        }

        private void btnMauto_Click(object sender, EventArgs e)
        {
            AbrirOEnfocarFormulario(new frmModeloAuto());
        }

        // ---- Módulos del sistema ----

        private void btnMenu_Click(object sender, EventArgs e)
        {
            new MenuPrincipalAdm().Show();
            this.Hide();
        }

        private void btnFacturas_Click(object sender, EventArgs e)
        {
            new FacturasAdm().Show();
            this.Hide();
        }

        private void btnCompra_Click(object sender, EventArgs e)
        {
            new Compras().Show();
            this.Hide();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            new ClientesAdm(new Cliente.ClienteRepository()).Show();
            this.Hide();
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            new InventarioAdmin(new ProductoInventario.ProductoRepository(), new ProductoInventario.ComboRepository()).Show();
            this.Hide();
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            new ProveedoresAdmin(
                new ProveedorRepository(),
                new EstadoRepository(),
                new ClasificacionRepository()).Show();
            this.Hide();
        }

        private void btnDeudores_Click(object sender, EventArgs e)
        {
            new DeudoresAdmin(new DeudaRepository()).Show();
            this.Hide();
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            new ReportesAdmin().Show();
            this.Hide();
        }

        private void btnBitacora_Click(object sender, EventArgs e)
        {
            new BitacoraAdmin(
                new BitacoraRepository(),
                new FiltroBitacoraService(),
                new ReporteBitacoraPdfExportador()).Show();
            this.Hide();
        }

        // ---- Sesión y perfil ----

        private void btnNoti_Click(object sender, EventArgs e)
        {
            new NotificacionesAdmin().ShowDialog();
        }

        private void btnPerfil_Click(object sender, EventArgs e)
        {
            new Perfil().ShowDialog();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
                "¿Está seguro que desea cerrar sesión?",
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                new Login.Login().Show();
                this.Close();
            }
        }
    }
}