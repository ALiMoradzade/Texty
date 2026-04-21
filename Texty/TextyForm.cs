using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Texty
{
    public partial class TextyForm : Form
    {
        public TextyForm()
        {
            InitializeComponent();
        }

        public string GetOpenedFileAddress()
        {
            return openFileDialog1.FileName;
        }

        public void SetOpenedFileAddress(string fileAddress)
        {
            openFileDialog1.FileName = fileAddress;
        }

        public string GetNewFileAddress()
        {
            return saveFileDialog1.FileName;
        }

        public string GetFileName()
        {
            return openFileDialog1.SafeFileName;
        }

        public string GetNewFileName()
        {
            return Path.GetFileName(saveFileDialog1.FileName);
        }

        public bool IsFileOpened()
        {
            return closeOpenedFileToolStripMenuItem.Visible;
        }

        public void IsFileOpened(bool status, string fileName="")
        {
            closeOpenedFileToolStripMenuItem.Visible = status;
            toolStripSeparator1.Visible = status;
            Text = fileName;
            if (!status)
            {
                richTextBox1.Clear();
            }
        }

        public bool IsFileEdited()
        {
            return Text.Contains('*');
        }

        public void IsFileEdited(bool status)
        {
            if (status)
            {
                Text += '*';
            }
            else
            {
                Text = Text.Replace("*", "");
            }
        }

        public bool IsTextEmpty()
        {
            return richTextBox1.Text.All(character => char.IsWhiteSpace(character));
        }

        private void TextyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((IsFileOpened() && IsFileEdited()) || (!IsFileOpened() && !IsTextEmpty()))
            {
                var r = MessageBox.Show("Do you want to save?",
                                        "Magic is over!, Vanish✨ the form!",
                                        MessageBoxButtons.YesNoCancel,
                                        MessageBoxIcon.Question,
                                        MessageBoxDefaultButton.Button3);

                if (r == DialogResult.Yes)
                {
                    saveToolStripMenuItem_Click(sender, EventArgs.Empty);
                }
                else if (r == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private async void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!IsTextEmpty())
            {
                var r = MessageBox.Show("Overwrite text file over your text?",
                                      "Text Mix-Up! 😵‍💫",
                                      MessageBoxButtons.YesNo,
                                      MessageBoxIcon.Exclamation,
                                      MessageBoxDefaultButton.Button2);

                if (r == DialogResult.No) return;
            }

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader sr = new StreamReader(GetOpenedFileAddress()))
                {
                    richTextBox1.Text = await sr.ReadToEndAsync();
                    richTextBox1.SelectionStart = richTextBox1.Text.Length;
                }
                IsFileOpened(true, GetFileName());
            }
        }
        

        private async void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsFileOpened() && IsFileEdited())
            {
                using (StreamWriter streamWriter = new StreamWriter(GetOpenedFileAddress()))
                {
                    await streamWriter.WriteLineAsync(richTextBox1.Text);
                    IsFileEdited(false);
                }
            }
            else if (!IsFileOpened() && IsFileEdited())
            {
                saveAsToolStripMenuItem_Click(sender, e);
            }
        }

        private async void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter streamWriter = new StreamWriter(GetNewFileAddress()))
                {
                    SetOpenedFileAddress(GetNewFileAddress());
                    IsFileOpened(true, GetNewFileName());
                    await streamWriter.WriteLineAsync(richTextBox1.Text);
                }
            }
        }

        private void closeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
           Close();
        }

        private void richTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (!IsFileEdited())
            {
                IsFileEdited(true);
            }
        }

        private void closeOpenedFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // When you came here, it means file is opened
            if (IsFileEdited())
            {
                var r = MessageBox.Show("Do you want to discard the changes?",
                                       "We're about to close!",
                                       MessageBoxButtons.YesNo,
                                       MessageBoxIcon.Question,
                                       MessageBoxDefaultButton.Button2);

                if (r == DialogResult.No) return;
            }
            IsFileOpened(false);
        }

        private void statusBarToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            statusStrip1.Visible = (sender as ToolStripMenuItem).Checked;
        }
    }
}