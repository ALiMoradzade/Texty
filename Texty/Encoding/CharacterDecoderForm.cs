using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Texty.Encoding;

namespace Texty.Encoding
{
    public partial class CharacterDecoderForm : Form
    {
        public enum CodeBase
        {
            Binary = 2,
            Octal = 8,
            Decimal = 10,
            Hexadecimal = 16
        }

        public CharacterDecoderForm()
        {
            InitializeComponent();

            comboBox1.DataSource = Enum.GetValues(typeof(CodeBase));
            comboBox1.SelectedIndex = -1;
        }

        public bool isCodeEmpty()
        {
            return textBoxCode.Text.All(character => char.IsWhiteSpace(character));
        }

        public bool isBaseEmpty()
        {
            if (comboBox1.SelectedItem == null) return true;
            CodeBase? codeBase = null;
            
            try
            {
                codeBase = (CodeBase)comboBox1.SelectedItem;
            }
            catch (Exception)
            {
            }

            return codeBase == null;
        }

        public bool isBaseCorrect()
        {
            try
            {
                var codeBase = (CodeBase)comboBox1.SelectedItem;
                Convert.ToInt32(textBoxCode.Text, (int)codeBase);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool isCharDecimalLengthCorrect()
        {
            var codeBase = (CodeBase)comboBox1.SelectedItem;
            int decimalCode = Convert.ToInt32(textBoxCode.Text, (int)codeBase);
            return decimalCode >= char.MinValue && decimalCode <= char.MaxValue;
        }

        private void textBoxEncode_TextChanged(object sender, EventArgs e)
        {
            if (isCodeEmpty())
            {
                return;
            }
            else if (isBaseEmpty())
            {
                textBoxChar.Text = "Please, select a base";
                return;
            }
            else if (!isBaseCorrect())
            {
                textBoxChar.Text = "Please, select a valid code or base";
                return;
            }
            else if (!isCharDecimalLengthCorrect())
            {
                textBoxChar.Text = "The value is outside the range (0 – 65535)";
                return;
            }
            
            var codeBase = (CodeBase)comboBox1.SelectedItem;
            int decimalCode = Convert.ToInt32(textBoxCode.Text, (int)codeBase);

            CharacterEncodingConverter converter = new CharacterEncodingConverter();
            converter.Decode(decimalCode);
            textBoxChar.Text = converter.Character.ToString();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBoxCode.Paste();
        }
    }
}
