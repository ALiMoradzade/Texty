using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Clipboard_Manager;

namespace Texty.Clipboard_Watcher
{
    public partial class ClipboardWatcherForm : Form
    {
        public ClipboardWatcherForm()
        {
            InitializeComponent();
        }

        private void ClipboardWatcherForm_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;

            Clipboard.Clear();
        }

        private void ClipboardWatcherForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Enabled = false;
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            ClipboardManager.CopyToClipboard(textBox1.Text);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                string clipboardText = Clipboard.GetText(TextDataFormat.UnicodeText);
                
                if (!string.IsNullOrEmpty(clipboardText))
                {
                    textBox1.Text += clipboardText + "\r\n";
                    textBox1.SelectionStart = textBox1.TextLength;
                    textBox1.ScrollToCaret();

                    Clipboard.Clear();
                }
            }
            catch (Exception)
            {
            }
        }

        private void rightToLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rightToLeftState = (int)textBox1.RightToLeft;
            rightToLeftState = -1 * ~(-1 * rightToLeftState);

            textBox1.RightToLeft = (RightToLeft)rightToLeftState;
            rightToLeftToolStripMenuItem.Checked = Convert.ToBoolean(rightToLeftState);
            contextMenuStrip1.RightToLeft = RightToLeft.No;
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }
    }
}
