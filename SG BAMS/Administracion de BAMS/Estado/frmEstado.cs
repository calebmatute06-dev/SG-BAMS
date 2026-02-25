using SG_BAMS.Administracion_de_BAMS.Estado;
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
    public partial class frmEstado : Form
    {
        clsEstado objetoEstado = new clsEstado();
        public frmEstado()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmEstados_Load);
        }

        private async void frmEstados_Load(object sender, EventArgs e)
        {
            await CargarGridEstados();
        }

        private async Task CargarGridEstados()
        {
            try
            {
                // Cambiamos el cursor para indicar que el sistema está trabajando
                this.Cursor = Cursors.WaitCursor;

                // Llamada asíncrona a la base de datos
                DataTable dt = await objetoEstado.LeerEstadosAsync();

                // Asignación al Grid (verifica que el nombre sea dgvEstados)
                dgvEstados.DataSource = dt;

                PersonalizarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al cargar: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void PersonalizarGrid()
        {
            // Ocultamos el ID si no es necesario que el usuario lo vea
            if (dgvEstados.Columns.Contains("id_estado"))
                dgvEstados.Columns["id_estado"].Visible = false;

            // Cambiamos los encabezados para que se vean más limpios
            if (dgvEstados.Columns.Contains("descripcion_estado"))
                dgvEstados.Columns["descripcion_estado"].HeaderText = "Nombre del Estado";

            // Estética general
            dgvEstados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEstados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEstados.AllowUserToAddRows = false;
            dgvEstados.ReadOnly = true;
        }

        private void btmAgregarEstado_Click(object sender, EventArgs e)
        {
            frmAgregarEstado agregarEstado = new frmAgregarEstado();
            agregarEstado.Show();
            this.Close();
        }

        private void btmModificar_Click(object sender, EventArgs e)
        {
            frmModificarEstado modificarEstado = new frmModificarEstado();
            modificarEstado.Show();
            this.Close();
        }

        private void btmSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
