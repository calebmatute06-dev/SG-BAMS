using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace SG_BAMS
{
    public partial class ProveedorAdmin : Form
    {
        public ProveedorAdmin()
        {
            InitializeComponent();
        }

        private void kryptonButton31_Click(object sender, EventArgs e)
        {

        }

        private void kryptonButton19_Click(object sender, EventArgs e)
        {
            using (var ventana = new AgregarProveedor())
            {
                if (ventana.ShowDialog() == DialogResult.OK)
                {
                    CrearTarjetaUI(ventana.InfoProveedor);
                }
            }
        }


        private void CrearTarjetaUI(ClsProveedor p)
        {
            // 1. Aumentamos el ancho a 240 (Alto se mantiene en 326)
            KryptonGroup card = new KryptonGroup();
            card.Size = new Size(235, 326);
            card.Margin = new Padding(12); // Un poquito más de margen entre tarjetas
            card.Tag = p.ID;

            // Estilo de la tarjeta
            card.StateCommon.Border.DrawBorders = PaletteDrawBorders.All;
            card.StateCommon.Border.Rounding = 25;
            card.StateCommon.Back.Color1 = Color.FromArgb(135, 206, 250);
            card.Panel.StateCommon.Color1 = Color.FromArgb(135, 206, 250);

            // --- ID (Ancho ajustado a 240) ---
            KryptonLabel lblID = new KryptonLabel
            {
                Text = p.ID,
                Location = new Point(0, 15),
                Size = new Size(240, 30),
                AutoSize = false
            };
            lblID.StateCommon.ShortText.TextH = PaletteRelativeAlign.Center;
            lblID.StateCommon.ShortText.Font = new Font("Segoe UI", 12, FontStyle.Bold);

            // --- NOMBRE (Ancho ajustado a 240) ---
            KryptonLabel lblNom = new KryptonLabel
            {
                Text = p.Nombre,
                Location = new Point(0, 45),
                Size = new Size(240, 40),
                AutoSize = false
            };
            lblNom.StateCommon.ShortText.TextH = PaletteRelativeAlign.Center;
            lblNom.StateCommon.ShortText.Font = new Font("Segoe UI", 15, FontStyle.Bold);

            // --- DATOS (Ahora con más espacio lateral) ---
            int yInicio = 95;
            int espacio = 32;

            string[] datos = {
        "Tel: " + p.Telefono,
        "RTN: " + p.RTN,
        "Dir: " + p.Direccion,
        "Clas: " + p.Clasificacion
    };

            foreach (string info in datos)
            {
                KryptonLabel lblInfo = new KryptonLabel
                {
                    Text = info,
                    Location = new Point(0, yInicio),
                    Size = new Size(240, 30), // Ajustado al nuevo ancho
                    AutoSize = false
                };
                lblInfo.StateCommon.ShortText.TextH = PaletteRelativeAlign.Center;
                lblInfo.StateCommon.ShortText.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                card.Panel.Controls.Add(lblInfo);
                yInicio += espacio;
            }

            // --- BOTONES (Repocicionados para el nuevo ancho) ---
            // Los moví un poco más hacia afuera para aprovechar el espacio
            KryptonButton btnFav = new KryptonButton
            {
                Text = "☆",
                Location = new Point(40, 245),
                Size = new Size(60, 60)
            };
            btnFav.StateCommon.Border.Rounding = 30;

            KryptonButton btnOpciones = new KryptonButton
            {
                Text = "...",
                Location = new Point(140, 245),
                Size = new Size(60, 60)
            };
            btnOpciones.StateCommon.Border.Rounding = 30;
            btnOpciones.Click += (s, ev) => { flowLayoutPanel1.Controls.Remove(card); };

            // Agregar todo al panel interno
            card.Panel.Controls.Add(lblID);
            card.Panel.Controls.Add(lblNom);
            card.Panel.Controls.Add(btnFav);
            card.Panel.Controls.Add(btnOpciones);

            flowLayoutPanel1.Controls.Add(card);
        }
    }
}
