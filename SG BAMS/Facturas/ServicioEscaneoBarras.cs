using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS.Facturas
{
    internal class ServicioEscaneoBarras
    {

        private readonly StringBuilder _buffer = new StringBuilder();
        private DateTime _ultimaTecla = DateTime.MinValue;

        public event Action<string> CodigoEscaneado;

        public bool ProcesarTecla(Keys key)
        {
            if ((key >= Keys.D0 && key <= Keys.D9) ||
                (key >= Keys.A && key <= Keys.Z) ||
                (key >= Keys.NumPad0 && key <= Keys.NumPad9))
            {
                if ((DateTime.Now - _ultimaTecla).TotalMilliseconds > 100)
                    _buffer.Clear();

                _ultimaTecla = DateTime.Now;
                _buffer.Append(new KeysConverter().ConvertToString(key));
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
