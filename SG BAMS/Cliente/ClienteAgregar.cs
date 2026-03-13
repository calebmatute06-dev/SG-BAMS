using Microsoft.Data.SqlClient;
using SG_BAMS.Administracion_de_BAMS.Estado;
using SG_BAMS.Cliente;
using SG_BAMS.Facturas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SG_BAMS
{
    public partial class ClienteAgregar : Form
    {
        public ClienteAgregar()
        {
            InitializeComponent();
        }
        private bool ValidarFormatoTexto(string texto, string nombreCampo)
        {
            string textoLimpio = texto.Trim();

            if (textoLimpio.Length < 3 || textoLimpio.Length > 70)
            {
                MessageBox.Show($"{nombreCampo} debe tener entre 3 y 70 caracteres.", "Error de Largo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            string[] palabras = textoLimpio.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string palabra in palabras)
            {
                if (palabra.Length < 2)
                {
                    MessageBox.Show($"{nombreCampo} no puede tener palabras de una sola letra (como '{palabra}').", "Nombre Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            if (!Regex.IsMatch(textoLimpio, @"^[a-zA-Z\sñÑáéíóúÁÉÍÓÚ]+$"))
            {
                MessageBox.Show($"{nombreCampo} solo debe contener letras.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (Regex.IsMatch(textoLimpio, @"\s{2,}"))
            {
                MessageBox.Show($"{nombreCampo} no puede contener dobles espacios.", "Error de Espacios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (Regex.IsMatch(textoLimpio, @"([a-zA-ZñÑáéíóúÁÉÍÓÚ])\1{2,}", RegexOptions.IgnoreCase))
            {
                MessageBox.Show($"{nombreCampo} contiene demasiadas letras repetidas seguidas.", "Error de Escritura", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                string.IsNullOrWhiteSpace(txtApellido.Text) ||
                string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                MessageBox.Show("Debe llenar todos los campos obligatorios antes de continuar.",
                                "Campos Vacíos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!ValidarFormatoTexto(txtNombre.Text, "El Nombre") ||
                !ValidarFormatoTexto(txtApellido.Text, "El Apellido"))
            {
                return;
            }

            if (!Regex.IsMatch(txtTelefono.Text, @"^[0-9]+$") || txtTelefono.Text.Length < 8)
            {
                MessageBox.Show("El teléfono debe contener solo números (mínimo 8 dígitos).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtRTN.Text))
            {
                if (!Regex.IsMatch(txtRTN.Text, @"^([0-9]+|Sin RTN)$"))
                {
                    MessageBox.Show("El RTN solo debe contener números o la frase 'Sin RTN'", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            try
            {
                ClsAgregarClientes objAC = new ClsAgregarClientes();

                int filasInsertadas = await objAC.AgregarClientes(
                    txtNombre.Text.Trim(),
                    txtApellido.Text.Trim(),
                    txtTelefono.Text.Trim(),
                    txtRTN.Text.Trim()
                );

                if (filasInsertadas > 0)
                {
                    MessageBox.Show("Cliente agregado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar cliente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void ClienteAgregar_Load(object sender, EventArgs e)
        {

        }

        private void BtnExistente_Click(object sender, EventArgs e)
        {
           
            using (ClienteExistente frmCE = new ClienteExistente())
            {
                if (frmCE.ShowDialog() == DialogResult.OK)
                {
                    this.DialogResult = DialogResult.OK; 
                    
                }
            }


        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();


        }
    }
}
