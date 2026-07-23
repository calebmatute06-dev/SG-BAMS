using System.Drawing;
using System.Windows.Forms;

namespace SG_BAMS
{
    internal class EstiloDataGridView()
    {
        public static void Aplicar(DataGridView grid)
        {
            grid.BorderStyle = BorderStyle.None;
            grid.BackgroundColor = Color.White;
            grid.RowHeadersVisible = false;
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.SkyBlue;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.Navy;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            grid.ColumnHeadersHeight = 28;

            grid.DefaultCellStyle.BackColor = Color.White;
            grid.DefaultCellStyle.ForeColor = Color.Navy;
            grid.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            grid.DefaultCellStyle.Padding = new Padding(3);
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 245, 255);
            grid.AlternatingRowsDefaultCellStyle.ForeColor = Color.Navy;

            grid.DefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            grid.DefaultCellStyle.SelectionForeColor = Color.White;

            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.GridColor = Color.LightGray;
            grid.RowTemplate.Height = 32;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.ClearSelection();
        }

    }
}