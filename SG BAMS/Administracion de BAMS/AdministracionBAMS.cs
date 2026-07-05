using SG_BAMS.Administracion_de_BAMS.Clasificacion;
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
    /// Formulario principal del módulo de administración del sistema SG_BAMS.
    /// Proporciona acceso a la gestión de usuarios, roles, catálogos de productos y navegación general.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class AdministracionBAMS : Form
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="AdministracionBAMS"/>.
        /// </summary>
        public AdministracionBAMS()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        /// <summary>
        /// Maneja el evento Click del botón de notificaciones para abrir el panel de alertas administrativas.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">Instancia de <see cref="EventArgs"/> con los datos del evento.</param>
        private void btnNoti_Click(object sender, EventArgs e)
        {
            NotificacionesAdmin notificaciones = new NotificacionesAdmin();
            notificaciones.ShowDialog();
        }

        /// <summary>
        /// Maneja el evento de carga del formulario AdministracionBAMS.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">Instancia de <see cref="EventArgs"/> con los datos del evento.</param>
        private void AdministracionBAMS_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        /// <summary>
        /// Maneja el evento Click del botón Ver Usuarios para abrir el catálogo de gestión de usuarios.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">Instancia de <see cref="EventArgs"/> con los datos del evento.</param>
        private void btnVerUsuarios_Click(object sender, EventArgs e)
        {
            frmUsuarios usuarios = new frmUsuarios();
            usuarios.Show();
        }

        /// <summary>
        /// Maneja el evento Click del botón Roles para gestionar los permisos y tipos de usuario.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">Instancia de <see cref="EventArgs"/> con los datos del evento.</param>
        private void btnRoles_Click(object sender, EventArgs e)
        {
            frmRoles verRoles = new frmRoles();
            verRoles.Show();
        }

        /// <summary>
        /// Maneja el evento Click del botón Tipo de Producto para gestionar las categorías de inventario.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">Instancia de <see cref="EventArgs"/> con los datos del evento.</param>
        private void btnTproducto_Click_1(object sender, EventArgs e)
        {
            frmTipoProducto verTproducto = new frmTipoProducto();
            verTproducto.Show();
        }

        /// <summary>
        /// Maneja el evento Click para abrir la gestión de formas de pago aceptadas por el sistema.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">Instancia de <see cref="EventArgs"/> con los datos del evento.</param>
        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            frmFormaPago verFormaPago = new frmFormaPago();
            verFormaPago.Show();
        }

        /// <summary>
        /// Maneja el evento Click para gestionar los estados lógicos de los registros en el sistema.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">Instancia de <see cref="EventArgs"/> con los datos del evento.</param>
        private void btnEstado_Click(object sender, EventArgs e)
        {
            frmEstado verEstado = new frmEstado();
            verEstado.Show();
        }

        /// <summary>
        /// Maneja el evento Click para abrir el formulario de marcas de productos.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">Instancia de <see cref="EventArgs"/> con los datos del evento.</param>
        private void btnMproducto_Click_1(object sender, EventArgs e)
        {
            frmMarcaProductos verMproducto = new frmMarcaProductos();
            verMproducto.Show();
        }

        /// <summary>
        /// Maneja el evento Click para gestionar el catálogo de modelos de automóviles.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">Instancia de <see cref="EventArgs"/> con los datos del evento.</param>
        private void btnMauto_Click(object sender, EventArgs e)
        {
            frmModeloAuto verMauto = new frmModeloAuto();
            verMauto.Show();
        }

        /// <summary>
        /// Redirige al usuario al menú principal administrativo.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">Instancia de <see cref="EventArgs"/> con los datos del evento.</param>
        private void btnMenu_Click(object sender, EventArgs e)
        {
            MenuPrincipalAdm MPA = new MenuPrincipalAdm();
            MPA.Show();
            this.Hide();
        }

        /// <summary>
        /// Abre el módulo de gestión de facturas administrativas.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">Instancia de <see cref="EventArgs"/> con los datos del evento.</param>
        private void btnFacturas_Click(object sender, EventArgs e)
        {
            FacturasAdm FA = new FacturasAdm();
            FA.Show();
            this.Hide();
        }

        /// <summary>
        /// Abre el módulo de gestión de compras a proveedores.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">Instancia de <see cref="EventArgs"/> con los datos del evento.</param>
        private void btnCompra_Click(object sender, EventArgs e)
        {
            Compras CF = new Compras();
            CF.Show();
            this.Hide();
        }

        /// <summary>
        /// Abre el módulo de administración de clientes.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">Instancia de <see cref="EventArgs"/> con los datos del evento.</param>
        private void btnClientes_Click(object sender, EventArgs e)
        {
            ClientesAdm CA = new ClientesAdm();
            CA.Show();
            this.Hide();
        }

        /// <summary>
        /// Abre el módulo de administración de inventarios.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">Instancia de <see cref="EventArgs"/> con los datos del evento.</param>
        private void btnInventario_Click(object sender, EventArgs e)
        {
            InventarioAdmin IA = new InventarioAdmin();
            IA.Show();
            this.Hide();
        }

        /// <summary>
        /// Abre el módulo de gestión de proveedores.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">Instancia de <see cref="EventArgs"/> con los datos del evento.</param>
        private void btnProveedores_Click(object sender, EventArgs e)
        {
            ProveedoresAdmin PA = new ProveedoresAdmin();
            PA.Show();
            this.Hide();
        }

        /// <summary>
        /// Abre el módulo de gestión de deudores y cuentas por cobrar.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">Instancia de <see cref="EventArgs"/> con los datos del evento.</param>
        private void btnDeudores_Click(object sender, EventArgs e)
        {
            DeudoresAdmin DA = new DeudoresAdmin();
            DA.Show();
            this.Hide();
        }

        /// <summary>
        /// Abre el panel de generación de reportes administrativos.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">Instancia de <see cref="EventArgs"/> con los datos del evento.</param>
        private void btnReportes_Click(object sender, EventArgs e)
        {
            ReportesAdmin RA = new ReportesAdmin();
            RA.Show();
            this.Hide();
        }

        /// <summary>
        /// Abre la bitácora de eventos y auditoría del sistema.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">Instancia de <see cref="EventArgs"/> con los datos del evento.</param>
        private void btnBitacora_Click(object sender, EventArgs e)
        {
            BitacoraAdmin BA = new BitacoraAdmin();
            BA.Show();
            this.Hide();
        }

        /// <summary>
        /// Cierra la sesión actual y redirige al usuario al formulario de Login.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">Instancia de <see cref="EventArgs"/> con los datos del evento.</param>
        private void btnCerrar_Click(object sender, EventArgs e)
        {
           DialogResult resultado = MessageBox.Show(
           "¿Está seguro que desea cerrar sesión?",
           "Confirmación",
           MessageBoxButtons.YesNo,
           MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
                {
                    Login.Login login = new Login.Login();
                    login.Show();
                    this.Close();
                }
        }

        /// <summary>
        /// Abre el formulario de perfil del usuario actual para visualización o edición.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">Instancia de <see cref="EventArgs"/> con los datos del evento.</param>
        private void btnPerfil_Click(object sender, EventArgs e)
        {
            Perfil perfil = new Perfil();
            perfil.ShowDialog();
        }

        private void btnClasificacion_Click(object sender, EventArgs e)
        {
            Clasificacion clasificacion = new Clasificacion();
            clasificacion.Show();
        }
    }
}