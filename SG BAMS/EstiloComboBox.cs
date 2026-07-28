using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krypton.Toolkit;
using System.Drawing;

namespace SG_BAMS
{
    /// <summary>
    /// Fuerza el color de texto navy en uno o varios KryptonComboBox.
    /// Sin lógica de placeholder: solo para combos que siempre muestran un valor real
    /// (ej. pantallas de "Modificar", donde el dato ya viene cargado).
    /// </summary>
    internal static class EstiloComboBox
    {
        private static readonly Color ColorTexto = Color.Navy;

        public static void Aplicar(params KryptonComboBox[] combos)
        {
            foreach (var cmb in combos)
            {
                ForzarColor(cmb);

                cmb.SelectedIndexChanged += (s, e) => ForzarColor(cmb);
            }
        }

        private static void ForzarColor(KryptonComboBox cmb)
        {
            cmb.StateCommon.ComboBox.Content.Color1 = ColorTexto;
            cmb.StateNormal.ComboBox.Content.Color1 = ColorTexto;
            cmb.StateActive.ComboBox.Content.Color1 = ColorTexto;
        }
    }
}