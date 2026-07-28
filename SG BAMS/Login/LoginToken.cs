using Krypton.Toolkit;
using System;
using System.Windows.Forms;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Formulario para la validación del token de recuperación de contraseña.
    /// El token se ingresa en 6 cajas separadas (TK1 a TK6), una letra/número por caja.
    /// </summary>
    public partial class LoginToken : Form
    {
        private readonly string correo;
        private readonly string token;
        private readonly IValidadorTokenService validadorToken;
        private readonly IRecuperacionService recuperacionService;
        private readonly INavegacionFormsService navegacionForms;

        public LoginToken(
            string correo,
            string token,
            IValidadorTokenService validadorToken,
            IRecuperacionService recuperacionService,
            INavegacionFormsService navegacionForms)
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            this.correo = !string.IsNullOrWhiteSpace(correo)
                ? correo
                : throw new ArgumentNullException(nameof(correo));
            this.token = !string.IsNullOrWhiteSpace(token)
                ? token
                : throw new ArgumentNullException(nameof(token));
            this.validadorToken = validadorToken ?? throw new ArgumentNullException(nameof(validadorToken));
            this.recuperacionService = recuperacionService ?? throw new ArgumentNullException(nameof(recuperacionService));
            this.navegacionForms = navegacionForms ?? throw new ArgumentNullException(nameof(navegacionForms));

   
            TK1.MaxLength = 1;
            TK2.MaxLength = 1;
            TK3.MaxLength = 1;
            TK4.MaxLength = 1;
            TK5.MaxLength = 1;
            TK6.MaxLength = 1;

      
            TK1.TextChanged += TK1_TextChanged;
            TK2.TextChanged += TK2_TextChanged;
            TK3.TextChanged += TK3_TextChanged;
            TK4.TextChanged += TK4_TextChanged;
            TK5.TextChanged += TK5_TextChanged;
       

           
            TK1.KeyDown += TK_KeyDown;
            TK2.KeyDown += TK_KeyDown;
            TK3.KeyDown += TK_KeyDown;
            TK4.KeyDown += TK_KeyDown;
            TK5.KeyDown += TK_KeyDown;
            TK6.KeyDown += TK_KeyDown;

            this.Load += (s, e) => TK1.Focus();
        }


        private void TK1_TextChanged(object sender, EventArgs e)
        {
            if (TK1.Text.Length == 1) TK2.Focus();
        }

        private void TK2_TextChanged(object sender, EventArgs e)
        {
            if (TK2.Text.Length == 1) TK3.Focus();
        }

        private void TK3_TextChanged(object sender, EventArgs e)
        {
            if (TK3.Text.Length == 1) TK4.Focus();
        }

        private void TK4_TextChanged(object sender, EventArgs e)
        {
            if (TK4.Text.Length == 1) TK5.Focus();
        }

        private void TK5_TextChanged(object sender, EventArgs e)
        {
            if (TK5.Text.Length == 1) TK6.Focus();
        }

    

        private void TK_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Back) return;

            KryptonTextBox cajaActual = (KryptonTextBox)sender;
            if (cajaActual.Text.Length > 0) return; 

            if (cajaActual == TK2) TK1.Focus();
            else if (cajaActual == TK3) TK2.Focus();
            else if (cajaActual == TK4) TK3.Focus();
            else if (cajaActual == TK5) TK4.Focus();
            else if (cajaActual == TK6) TK5.Focus();
        }

     
        private string ObtenerTokenIngresado()
        {
            return TK1.Text + TK2.Text + TK3.Text + TK4.Text + TK5.Text + TK6.Text;
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            string tokenIngresado = ObtenerTokenIngresado().Trim();

            if (tokenIngresado.Length < 6)
            {
                MessageBox.Show("Ingrese el token completo.");
                return;
            }

            if (validadorToken.ValidarToken(correo, tokenIngresado, token))
            {
                navegacionForms.NavegarANuevaContrasena(correo, recuperacionService);
                this.Hide();
            }
            else
            {
                MessageBox.Show("Token incorrecto. Intente de nuevo.");
                TK1.Text = "";
                TK2.Text = "";
                TK3.Text = "";
                TK4.Text = "";
                TK5.Text = "";
                TK6.Text = "";
                TK1.Focus();
            }
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            navegacionForms.NavegarAlLogin();
            navegacionForms.CerrarFormulario(this);
        }
    }
}