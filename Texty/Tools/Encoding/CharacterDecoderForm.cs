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

namespace Texty.Tools.Encoding
{
    public partial class CharacterDecoderForm : Form
    {
        public CharacterDecoderForm()
        {
            InitializeComponent();

            comboBox1.DataSource = Enum.GetValues(typeof(CharacterEncodingConverter.Base));
            comboBox1.SelectedIndex = -1;
        }

        private void textBoxEncode_TextChanged(object sender, EventArgs e)
        {
            string code;
            CharacterEncodingConverter.Base codeBase;

            if (textBoxCode.Text.All(character => char.IsWhiteSpace(character)))
            {
                return;
            }
            else if (comboBox1.SelectedItem == null)
            {
                textBoxCharacter.Text = "Please, select a base";
                return;
            }

            code = textBoxCode.Text;
            codeBase = (CharacterEncodingConverter.Base)comboBox1.SelectedItem;

            if (!CharacterEncodingConverter.IsCodeBaseCorrect(code, codeBase))
            {
                textBoxCharacter.Text = "Please, enter a valid code or base";
                return;
            }
            else if (!CharacterEncodingConverter.IsCodeLengthCorrect(code, codeBase))
            {
                textBoxCharacter.Text = "The value is out of range (0 – 0xFFFF)";
                return;
            }

            int decimalCode = Convert.ToInt32(textBoxCode.Text, (int)(CharacterEncodingConverter.Base)comboBox1.SelectedItem);

            CharacterEncodingConverter converter = new CharacterEncodingConverter();
            converter.Decode(decimalCode);
            textBoxCharacter.Text = converter.Character.ToString();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBoxCode.Paste();
        }

        private void textBoxCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\b') return;
            if (e.KeyChar == 'x') return;
            else if (!Regex.IsMatch(e.KeyChar.ToString(), "[0-9a-fA-F]{1}"))
            {
                e.Handled = true;
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBoxCharacter.Copy();
        }
    }
}
