using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SG_BAMS.Administracion_de_BAMS.FormaPago;
using SG_BAMS.Login;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SG_BAMS.Proveedor
{
    public partial class ModificarProveedor : Form
    {
        ClsProveedor proveedor = new ClsProveedor();

        private int _idEstado;
        private int _idClasificacion;

        public ModificarProveedor(string nombre, string contacto,
            string direccion, string rtn, int idEstado, int idClasificacion)
        {
            InitializeComponent();

            txtNombre.Text = nombre;
            txtTelefono.Text = contacto;
            txtDireccion.Text = direccion;
            txtRTN.Text = rtn;

            _idEstado = idEstado;
            _idClasificacion = idClasificacion;
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            ProveedoresAdmin proveedor = new ProveedoresAdmin();
            proveedor.Show();
            this.Close();
        }

        private void ModificarProveedor_Load(object sender, EventArgs e)
        {
            proveedor.CargarComboEstado(cmbEstado);
            proveedor.CargarComboClasificacion(cmbClasificacion);

            cmbEstado.SelectedValue = _idEstado;
            cmbClasificacion.SelectedValue = _idClasificacion;
        }

        private void cmbEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEstado.SelectedIndex != -1 && cmbEstado.SelectedItem is DataRowView)
            {
                DataRowView drv = (DataRowView)cmbEstado.SelectedItem;
                int idEstado = Convert.ToInt32(drv["id_estado"]);
                string nombreEstado = drv["descripcion_estado"].ToString();
            }

        }

        private void cmbClasificacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEstado.SelectedIndex != -1 && cmbEstado.SelectedItem is DataRowView)
            {
                DataRowView drv = (DataRowView)cmbEstado.SelectedItem;
                int idEstado = Convert.ToInt32(drv["id_estado"]);
                string nombreEstado = drv["descripcion_estado"].ToString();
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            int idEstado = Convert.ToInt32(cmbEstado.SelectedValue);
            int idClasificacion = Convert.ToInt32(cmbClasificacion.SelectedValue);

            int idUsuario = new ClsPasarUsuario().IdUsuario();

            proveedor.ModificarProveedor(
                txtNombre.Text.Trim(),
                txtTelefono.Text.Trim(),
                txtDireccion.Text.Trim(),
                txtRTN.Text.Trim(),
                idEstado,
                idClasificacion,
                idUsuario
            );

            ProveedoresAdmin frm = new ProveedoresAdmin();
            frm.Show();
            this.Close();
        }
    }
}
