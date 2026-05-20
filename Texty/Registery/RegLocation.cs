using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texty.Registery
{
    internal class RegLocation
    {
        private static string address = RegAddress.RegLocation;
        private static Point defaultLocation = new Point(442, 163);

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
            Write(defaultLocation);
        }

        public static void Write(Point location)
        {
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(address))
            {
                key.SetValue("X", location.X, RegistryValueKind.DWord);
                key.SetValue("Y", location.Y, RegistryValueKind.DWord);
            }
        }

        public static Point Read()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(address, false))
            {
                if (key != null)
                {
                    int X = (int)key.GetValue("X");
                    int Y = (int)key.GetValue("Y");

                    Point location = new Point(X, Y);
                    return location;
                }
            }
            return defaultLocation;
        }
    }
}
