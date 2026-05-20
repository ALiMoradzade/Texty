using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Texty.Registery
{
    internal class RegAddress
    {
        public static string RegFont { get => $"Software\\{Application.ProductName}\\Settings\\Font"; }
        public static string RegLocation { get => $"Software\\{Application.ProductName}\\Settings\\Location"; }
        public static string RegSize { get => $"Software\\{Application.ProductName}\\Settings\\Size"; }
        public static string RegWindowsState { get => $"Software\\{Application.ProductName}\\Settings\\WindowsState"; }

    }
}
