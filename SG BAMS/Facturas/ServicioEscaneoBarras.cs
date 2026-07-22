using System;
using System.Text;
using System.Windows.Forms;

namespace SG_BAMS.Facturas
{
    internal class ServicioEscaneoBarras
    {
        private readonly StringBuilder _buffer = new StringBuilder();
        private DateTime _ultimaTecla = DateTime.MinValue;

        public event Action<string> CodigoEscaneado;
        public bool ProcesarTecla(Keys key)
        {
            bool esNumeroFila = key >= Keys.D0 && key <= Keys.D9;
            bool esNumeroPad = key >= Keys.NumPad0 && key <= Keys.NumPad9;

            if (esNumeroFila || esNumeroPad)
            {
                if ((DateTime.Now - _ultimaTecla).TotalMilliseconds > 100)
                    _buffer.Clear();

                _ultimaTecla = DateTime.Now;

                char digito = esNumeroFila ? (char)('0' + (key - Keys.D0)) : (char)('0' + (key - Keys.NumPad0));
                _buffer.Append(digito);

                return true;
            }

            if (key == Keys.Enter)
            {
                string codigo = _buffer.ToString().Trim();
                _buffer.Clear();

                if (!string.IsNullOrEmpty(codigo))
                    CodigoEscaneado?.Invoke(codigo);

                return true;
            }
            return false;
        }
    }
}