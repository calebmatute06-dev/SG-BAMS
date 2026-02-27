using SG_BAMS.Administracion_de_BAMS.MarcaProd;
using SG_BAMS.Administracion_de_BAMS.Rol;
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
    public partial class frmModificarMarcaProducto : Form
    {
        int idMarca;
        public frmModificarMarcaProducto(int id, string nombreActual)
        {
           
            InitializeComponent();
            this.idMarca = id;
            txtDescri.Text = nombreActual;

        }

        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            frmMarcaProductos verMproducto = new frmMarcaProductos();
            verMproducto.Show();
            this.Close();
        }

        private async void btnModificar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDescri.Text)) return;

            try
            {
                this.Cursor = Cursors.WaitCursor;
                clsMarca objetoMarca = new clsMarca();

                bool exito = await objetoMarca.ModificarMarcaAsync(idMarca, txtDescri.Text.Trim());

                if (exito)
                {
                    MessageBox.Show("Marca actualizada correctamente.", "SG-BAMS");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally { this.Cursor = Cursors.Default; }
        }
    }
    }
    

