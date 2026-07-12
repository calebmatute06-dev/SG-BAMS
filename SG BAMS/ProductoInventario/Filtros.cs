using Krypton.Toolkit;
using SG_BAMS.Administracion_de_BAMS.Estado;
using SG_BAMS.Proveedor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SG_BAMS.ProductoInventario.DTO.ProductoDTO;

namespace SG_BAMS.ProductoInventario
{
    public partial class Filtros : Form
    {
        public FiltroInventarioDTO FiltroSeleccionado { get; private set; }

        public Filtros()
        {
            InitializeComponent();
        }



        private string ObtenerValorOTodos(KryptonComboBox combo, CheckBox checkboxTodos)
        {
            if (checkboxTodos.Checked) return null;
            return string.IsNullOrEmpty(combo.Text) ? null : combo.Text;
        }


        private void btnAceptar_Click_1(object sender, EventArgs e)
        {
            FiltroSeleccionado = new FiltroInventarioDTO
            {
                Marca = ObtenerValorOTodos(cmbMarca, chkmarca),
                TipoProducto = ObtenerValorOTodos(cmbproducto, chkproductos),
                ModeloAuto = ObtenerValorOTodos(cmbmodelo, chkmodelos),
                Proveedor = ObtenerValorOTodos(cmbproveedor, chkproveedores)
            };

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
