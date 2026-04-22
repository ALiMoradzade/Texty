using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Texty
{
    internal class RegFont
    {
        private static string RegAddress = $"Software\\{Application.ProductName}\\Settings\\Font";
        private static Font defaultFont = new Font("Comic Sans MS", 12, FontStyle.Regular);

        public static bool IsExisted
        {
            get
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegAddress, false))
                {
                   if (key != null) return true;
                }
                return false;
            }
        }

        public static void Write()
        {
            Write(defaultFont);
        }

        public static void Write(Font font)
        {
            using (RegistryKey key = Registry.CurrentUser.CreateSubKey(RegAddress))
            {
                key.SetValue("Name", font.Name, RegistryValueKind.String);
                key.SetValue("Size", font.Size, RegistryValueKind.DWord);
                key.SetValue("Style", font.Style, RegistryValueKind.String);
            }
        }

        public static Font Read()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegAddress, false))
            {
                if (key != null)
                {
                    string fontName = key.GetValue("Name").ToString();
                    int fontSize = (int)key.GetValue("Size");
                    FontStyle fontStyle = (FontStyle)Enum.Parse(typeof(FontStyle), (string)key.GetValue("Style"));

                    Font font = new Font(fontName, fontSize, fontStyle);
                    return font;
                }
            }
            return defaultFont;
        }
    }
}
