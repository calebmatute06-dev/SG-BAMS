using Krypton.Toolkit;
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
    public partial class InventarioAdmin : Form
    {
        public InventarioAdmin()
        {
            InitializeComponent();
        }

        private void kryptonButton11_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox17_Click(object sender, EventArgs e)
        {

        }

        private void kryptonButton19_Click(object sender, EventArgs e)
        {
            // 1. Abrimos el formulario para agregar el producto
            using (AgregarProducto frm = new AgregarProducto())
            {
                // 2. Si el usuario presiona "Aceptar" (DialogResult.OK)
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    // 3. Mandamos los datos capturados a la función que crea la tarjeta
                    CrearTarjetaProductoUI(frm.InfoProducto);
                }
            }
        }

        private void CrearTarjetaProductoUI(ClsProducto p)
        {
            KryptonGroup card = new KryptonGroup();
            card.Size = new Size(240, 326); // Tu medida de ancho ajustada
            card.Margin = new Padding(10);
            card.Tag = p.ID;

            // Estilo visual
            card.StateCommon.Border.Rounding = 25;
            card.StateCommon.Border.DrawBorders = PaletteDrawBorders.All;
            card.StateCommon.Back.Color1 = Color.FromArgb(135, 206, 250);
            card.Panel.StateCommon.Color1 = Color.FromArgb(135, 206, 250);

            // --- TÍTULOS ---
            KryptonLabel lblID = new KryptonLabel { Text = p.ID, Location = new Point(0, 10), Size = new Size(240, 25), AutoSize = false };
            lblID.StateCommon.ShortText.TextH = PaletteRelativeAlign.Center;
            lblID.StateCommon.ShortText.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            KryptonLabel lblNom = new KryptonLabel { Text = p.Nombre, Location = new Point(0, 35), Size = new Size(240, 30), AutoSize = false };
            lblNom.StateCommon.ShortText.TextH = PaletteRelativeAlign.Center;
            lblNom.StateCommon.ShortText.Font = new Font("Segoe UI", 13, FontStyle.Bold);

            // --- BLOQUE DE DATOS (8 CAMPOS) ---
            // Ajustamos el 'y' y el 'espacio' para que no se salgan del cuadro
            int y = 75;
            int salto = 22;

            string[] info = {
        "Precio: L. " + p.Precio,
        "Marca: " + p.Marca,
        "Tipo: " + p.Tipo,
        "Modelo: " + p.ModeloAuto,
        "Estado: " + p.Estado,
        "Stock: " + p.Stock
    };

            foreach (string dato in info)
            {
                KryptonLabel l = new KryptonLabel
                {
                    Text = dato,
                    Location = new Point(0, y),
                    Size = new Size(240, 22),
                    AutoSize = false
                };
                l.StateCommon.ShortText.TextH = PaletteRelativeAlign.Center;
                l.StateCommon.ShortText.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                card.Panel.Controls.Add(l);
                y += salto;
            }

            // --- BOTONES INFERIORES ---
            KryptonButton btnFav = new KryptonButton { Text = "☆", Location = new Point(45, 250), Size = new Size(60, 60) };
            btnFav.StateCommon.Border.Rounding = 30;

            KryptonButton btnDel = new KryptonButton { Text = "...", Location = new Point(135, 250), Size = new Size(60, 60) };
            btnDel.StateCommon.Border.Rounding = 30;
            btnDel.Click += (s, ev) => { flowLayoutPanel1.Controls.Remove(card); };

            card.Panel.Controls.Add(lblID);
            card.Panel.Controls.Add(lblNom);
            card.Panel.Controls.Add(btnFav);
            card.Panel.Controls.Add(btnDel);

            flowLayoutPanel1.Controls.Add(card);
        }
    }
}
