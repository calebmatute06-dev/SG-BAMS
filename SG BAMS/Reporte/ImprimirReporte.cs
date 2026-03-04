using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// Añadimos estas librerías para que reconozca los DTOs y QuestPDF
using SG_BAMS.Reportes;
using QuestPDF.Fluent;
using System.Diagnostics;
using System.IO;

namespace SG_BAMS
{
    public partial class ImprimirReporte : Form
    {
        // --- NUEVAS VARIABLES (No borres lo anterior) ---
        private List<ReporteDTO> _datosRecibidos;
        private DateTime _fDesde;
        private DateTime _fHasta;

        public ImprimirReporte()
        {
            InitializeComponent();
        }

        // --- NUEVO CONSTRUCTOR (Este recibe los datos de ReporteAdmin) ---
        public ImprimirReporte(List<ReporteDTO> lista, DateTime desde, DateTime hasta)
        {
            InitializeComponent();
            this._datosRecibidos = lista;
            this._fDesde = desde;
            this._fHasta = hasta;
        }

        private void ImprimirReporte_Load(object sender, EventArgs e)
        {
            // Aquí puedes poner código que se ejecute al abrir, 
            // como marcar "Vertical" por defecto en tus RadioButtons
        }

        // Supongamos que tu botón azul de "Imprimir" se llama btnGenerar
        private void btnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                if (_datosRecibidos == null || _datosRecibidos.Count == 0)
                {
                    MessageBox.Show("No hay datos cargados para imprimir.");
                    return;
                }

                // Configuramos la ruta temporal
                string ruta = Path.Combine(Path.GetTempPath(), "ReporteFinal.pdf");

                // Creamos el documento usando la clase que ya tenemos
                var documento = new ReporteDocumento(_datosRecibidos, _fDesde, _fHasta);

                // Generamos el PDF
                documento.GeneratePdf(ruta);

                // Abrimos el visor de PDF
                Process.Start(new ProcessStartInfo { FileName = ruta, UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al imprimir: " + ex.Message);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}