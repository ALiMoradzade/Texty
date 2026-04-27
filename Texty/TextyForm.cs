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
using System.Globalization;
using Microsoft.Win32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Texty.Registery;

namespace Texty
{
    public partial class TextyForm : Form
    {
        float zoom = 0.1f;
        public TextyForm()
        {
            InitializeComponent();
        }

        #region Form
        private void TextyForm_Load(object sender, EventArgs e)
        {
            if (!RegFont.IsExisted) RegFont.Write();
            richTextBox1.Font = fontDialog1.Font = RegFont.Read();

            if (!RegFont.IsExisted) RegFont.Write();
            Size = RegSize.Read();

            if (!RegWindowState.IsExisted) RegWindowState.Write();
            WindowState = RegWindowState.Read();

            if (!RegLocation.IsExisted) RegLocation.Write();
            Location = RegLocation.Read();

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
                    saveToolStripMenuItem.PerformClick();
                }
                else if (r == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void TextyForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            RegFont.Write(fontDialog1.Font);
            if (WindowState == FormWindowState.Normal)
            {
                RegSize.Write(Size);
                RegWindowState.Write(WindowState);
                RegLocation.Write(Location);
            }
        }
        #endregion

        #region Set Methods
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

        public void IsFileOpened(bool status, string fileName = "")
        {
            Text = fileName;
            closeOpenedFileToolStripMenuItem.Visible = status;
            toolStripSeparator1.Visible = status;
            if (!status)
            {
                richTextBox1.Clear();
            }
        }

        public void SetOpenedFileAddress(string fileAddress)
        {
            openFileDialog1.FileName = fileAddress;
        }

        public void SetTextZoomFactorStatus()
        {
            textZoomFactor.Text = $"{richTextBox1.ZoomFactor * 100}%";
        }
        #endregion

        #region Get Methods
        public bool IsTextEmpty()
        {
            return richTextBox1.Text.All(character => char.IsWhiteSpace(character));
        }

        public bool IsFileOpened()
        {
            return !string.IsNullOrEmpty(Text.Replace("*", ""));
        }

        public bool IsFileEdited()
        {
            return Text.Contains('*');
        }

        public string GetOpenedFileAddress()
        {
            return openFileDialog1.FileName;
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
        #endregion


        #region File Tab
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
                saveAsToolStripMenuItem.PerformClick();
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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
           Close();
        }
        #endregion

        #region Edit Tab
        private void timeDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string GetPersianDate(DateTime dateTime)
            {
                PersianCalendar persianCalendar = new PersianCalendar();

                string persianYear = persianCalendar.GetYear(dateTime).ToString("0000");
                string persianMonth = persianCalendar.GetMonth(dateTime).ToString("00");
                string persianDayOfMonth = persianCalendar.GetDayOfMonth(dateTime).ToString("00");

                return $"{persianYear}/{persianMonth}/{persianDayOfMonth}";
            }

            DateTime systemDateTime = DateTime.Now;
            
            string gregorianDate = $"{systemDateTime.Year:0000}/{systemDateTime.Month:00}/{systemDateTime.Day:00}";
            string solarHijriDate = GetPersianDate(systemDateTime);

            string value = $"\r\n{solarHijriDate}\r\n{gregorianDate}";
            int selectionLen = richTextBox1.SelectionStart + value.Replace("\r\n", "x").Length;

            richTextBox1.Text = richTextBox1.Text.Insert(richTextBox1.SelectionStart, value);
            richTextBox1.SelectionStart = selectionLen;
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Font = fontDialog1.Font;
            }
        }
        #endregion

        #region View Tab
        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.ZoomFactor += zoom;
            SetTextZoomFactorStatus();

        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.ZoomFactor -= zoom;
            SetTextZoomFactorStatus();
        }

        private void restoreDeafaultZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.ZoomFactor = 1;
            SetTextZoomFactorStatus();
        }

        private void statusBarToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            statusStrip1.Visible = (sender as ToolStripMenuItem).Checked;
        }
        #endregion


        #region richTextBox
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (!IsFileEdited())
            {
                IsFileEdited(true);
            }
        }

        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            int lineCount = richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart);
            int charCount = richTextBox1.SelectionStart - richTextBox1.GetFirstCharIndexFromLine(lineCount);
            textLineChar.Text = $"Ln {lineCount + 1}, Char {charCount + 1}";
            textLen.Text = $"Length {richTextBox1.Text.Length}";
        }
        #endregion

        #region Right Click
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Undo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Paste();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionLength > 0)
            {
                int temp = richTextBox1.SelectionStart;
                richTextBox1.Text = richTextBox1.Text.Remove(temp, richTextBox1.SelectionLength);
                richTextBox1.SelectionStart = temp;
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectAll();
        }

        private void rightToLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rightToLeftState = (int)richTextBox1.RightToLeft;
            rightToLeftState = -1 * ~(-1 * rightToLeftState);

            richTextBox1.RightToLeft = (RightToLeft)rightToLeftState;
            rightToLeftToolStripMenuItem.Checked = Convert.ToBoolean(rightToLeftState);
            contextMenuStrip1.RightToLeft = RightToLeft.No;
        }
        #endregion

    }
}