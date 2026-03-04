using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using SG_BAMS.Reportes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS
{
    public partial class ReporteAdmin : Form
    {
        // Se mantiene tu instancia original
        ClsReporte objReporte = new ClsReporte();

        public ReporteAdmin()
        {
            InitializeComponent();
        }

        // Evento Load: Carga inicial de datos al abrir la ventana
        private void ReporteAdmin_Load(object sender, EventArgs e)
        {
            CargarGridPrincipal();
        }

        // Método de carga para mostrar todos los registros inicialmente
        private void CargarGridPrincipal()
        {
            objReporte.CargarDatosReporte(dgvReporte);
        }

        // --- LÓGICA DE FILTRADO POR FECHA ---

        // Este es tu botón (kryptonButton12), ahora ejecutará el filtro al hacer clic
        private void kryptonButton12_Click(object sender, EventArgs e)
        {
            // 1. Regresamos los calendarios a la fecha de hoy
            dtpDesde.Value = DateTime.Now;
            dtpHasta.Value = DateTime.Now;

            // 2. Volvemos a cargar todos los datos sin filtros
            CargarGridPrincipal();

            // Opcional: Mostrar un mensaje en la barra de estado o consola
            // MessageBox.Show("Filtros restablecidos");
        }

        // Evento para el calendario "Desde": se dispara al cambiar la fecha
        private void dtpDesde_ValueChanged(object sender, EventArgs e)
        {
            EjecutarFiltrado();
        }

        // Evento para el calendario "Hasta": se dispara al cambiar la fecha
        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {
            EjecutarFiltrado();
        }

        // Método centralizado para filtrar. No usa 'txtFiltro' para evitar errores.
        // ESTO VA EN ReporteAdmin.cs
        private void EjecutarFiltrado()
        {
            // Mandamos 3 argumentos: Fecha Inicio, Fecha Fin y el Grid.
            // Esto ya no marcará error porque ahora coincide con la clase.
            objReporte.BuscarReporte(dtpDesde.Value, dtpHasta.Value, dgvReporte);
        }

        // Se mantiene tu método de diseño original
        private void kryptonGroup3_Paint(object sender, PaintEventArgs e)
        {
            // Evento de diseño original
        }

        private void dtpDesde_ValueChanged_1(object sender, EventArgs e)
        {
            EjecutarFiltrado();
        }

        private void dtpHasta_ValueChanged_1(object sender, EventArgs e)
        {
            EjecutarFiltrado();
        }

        private void btninicioSesion_Click(object sender, EventArgs e)
        {
            try
            {
                // Configuración de licencia obligatoria para QuestPDF
                QuestPDF.Settings.License = LicenseType.Community;

                // 1) Llenamos la lista de DTOs con lo que hay actualmente en el Grid (filtrado)
                List<ReporteDTO> listaParaPdf = new List<ReporteDTO>();

                foreach (DataGridViewRow row in dgvReporte.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        listaParaPdf.Add(new ReporteDTO
                        {
                            // IMPORTANTE: Estos nombres entre comillas deben ser iguales al SQL de ClsReporte
                            ID = row.Cells["ID"].Value?.ToString(),
                            Usuario = row.Cells["Usuario"].Value?.ToString(),
                            Tipo = row.Cells["Tipo"].Value?.ToString(),
                            Descripcion = row.Cells["Descripción"].Value?.ToString(),
                            Fecha = Convert.ToDateTime(row.Cells["Fecha"].Value)
                        });
                    }
                }

                if (listaParaPdf.Count == 0)
                {
                    MessageBox.Show("No hay datos en la tabla para exportar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2) Definimos la ruta temporal para el archivo
                string fileName = $"Reporte_Administrativo_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
                string filePath = Path.Combine(Path.GetTempPath(), fileName);

                // 3) Generamos el documento usando la clase que acabamos de corregir
                // Le pasamos la lista, y las fechas de los calendarios para el encabezado
                var documento = new SG_BAMS.Reportes.ReporteDocumento(listaParaPdf, dtpDesde.Value, dtpHasta.Value);
                documento.GeneratePdf(filePath);

                // 4) Abrimos el PDF automáticamente
                Process.Start(new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el PDF: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}