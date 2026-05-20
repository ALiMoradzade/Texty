using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texty.Registery;

namespace Texty
{
    internal class RegSize
    {
        private static string address = RegAddress.RegSize;
        private static Size defaultSize = new Size(716, 525);

        public static bool IsExisted
        {
            get
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(address, false))
                {
                    if (key != null) return true;
                }
                return false;
            }
        }

        public static void Write()
        {
            Write(defaultSize);
        }

        public static void Write(Size size)
        {
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(address))
            {
                key.SetValue("Width", size.Width, RegistryValueKind.DWord);
                key.SetValue("Height", size.Height, RegistryValueKind.DWord);
            }
        }

        public static Size Read()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(address, false))
            {
                if (key != null)
                {
                    int width = (int)key.GetValue("Width");
                    int height = (int)key.GetValue("Height");

                    Size size = new Size(width, height);
                    return size;
                }
            }
            return defaultSize;
        }
    }
}
