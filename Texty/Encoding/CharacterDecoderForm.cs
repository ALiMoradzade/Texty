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
                textBoxCharacter.Text = "Please, select a valid code or base";
                return;
            }
            else if (!CharacterEncodingConverter.IsCodeLengthCorrect(code, codeBase))
            {
                textBoxCharacter.Text = "The value is outside the range (0 – 65535)";
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
    }
}
