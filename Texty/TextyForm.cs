using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Texty.Encoding;
using Texty.Registery;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Texty
{
    public partial class TextyForm : Form
    {
        float zoom = 0.1f;
        bool flagEnableTextChange = true;
        public TextyForm()
        {
            InitializeComponent();

            richTextBox1.AllowDrop = true;
            
            // no need to Enable methods because richTextBox.SelectionLength Event invokes them
        }

        #region Form Events
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
                var r = MessageBoxSaveTextOrFile();
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

        #region Message Boxes
        public DialogResult MessageBoxDragDrop()
        {
            var r = MessageBox.Show("Would you like to open the file?",
                         "Incoming file detected...",
                         MessageBoxButtons.YesNo,
                         MessageBoxIcon.Question,
                         MessageBoxDefaultButton.Button2);
            return r;
        }

        public DialogResult MessageBoxOpenFile()
        {
            var r = MessageBox.Show("Overwrite text file over your text?",
                                  "Text Mix-Up! 😵‍💫",
                                  MessageBoxButtons.YesNo,
                                  MessageBoxIcon.Exclamation,
                                  MessageBoxDefaultButton.Button2);

            return r;
        }

        public DialogResult MessageBoxDiscardChanges()
        {
            var r = MessageBox.Show("Do you want to discard the changes?",
                                      "We're about to close!",
                                      MessageBoxButtons.YesNo,
                                      MessageBoxIcon.Question,
                                      MessageBoxDefaultButton.Button2);
            return r;
        }

        public DialogResult MessageBoxSaveTextOrFile()
        {
            var r = MessageBox.Show("Do you want to save?",
                                        "Magic is over!, Vanish✨ the form!",
                                        MessageBoxButtons.YesNoCancel,
                                        MessageBoxIcon.Question,
                                        MessageBoxDefaultButton.Button3);
            return r;
        }
        #endregion

        #region Edit Commands
        public void Undo()
        {
            richTextBox1.Undo();
        }

        public void Cut()
        {
            richTextBox1.Cut();
        }

        public void Copy()
        {
            richTextBox1.Copy();
        }

        public void Paste()
        {
            richTextBox1.Paste();
        }

        public void Delete()
        {
            if (richTextBox1.SelectionLength > 0)
            {
                int temp = richTextBox1.SelectionStart;
                richTextBox1.Text = richTextBox1.Text.Remove(temp, richTextBox1.SelectionLength);
                richTextBox1.SelectionStart = temp;
            }
        }

        public void SelectAll()
        {
            richTextBox1.SelectAll();
        }

        public void EnableContextualEditing(bool state)
        {
            // Menu item
            cutToolStripMenuItem1.Enabled = state;
            copyToolStripMenuItem1.Enabled = state;
            deleteToolStripMenuItem1.Enabled = state;

            // Context menu strip
            cutToolStripMenuItem.Enabled = state;
            copyToolStripMenuItem.Enabled = state;
            deleteToolStripMenuItem.Enabled = state;
        }

        public void EnableSingleCharEncoding(bool stateSingleChar)
        {
           statusStrip2.Visible = stateSingleChar;
        }

        public void EnableMultipleCharEncoding(bool stateMultipleChar)
        {
            statusStrip3.Visible = stateMultipleChar;
            statusStrip4.Visible = stateMultipleChar;
        }
        #endregion

        #region File Status Methods
        public bool IsTextEmpty()
        {
            return richTextBox1.Text.All(character => char.IsWhiteSpace(character));
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

        public bool IsFileOpened()
        {
            return !string.IsNullOrEmpty(Text.Replace("*", ""));
        }

        public void IsFileOpened(bool status, string fileName = "")
        {
            Text = fileName;
            closeOpenedFileToolStripMenuItem.Visible = status;
            toolStripSeparator1.Visible = status;
            if (!status)
            {
                flagEnableTextChange = false;
                richTextBox1.Clear();
                flagEnableTextChange = true;
            }
        }

        public void SetTextZoomFactorStatus()
        {
            textZoomFactor.Text = $"{richTextBox1.ZoomFactor * 100}%";
        }
        #endregion

        #region Open/Save Methods
        private async Task<string> ReadFile(string address)
        {
            string text;
            using (StreamReader sr = new StreamReader(address))
            {
                text = await sr.ReadToEndAsync();
            }
            return text;
        }

        private async Task WriteFile(string address)
        {
            using (StreamWriter streamWriter = new StreamWriter(address))
            {
                await streamWriter.WriteLineAsync(richTextBox1.Text);
            }
        }



        public async void OpenFile(string fileAddress, string fileName)
        {
            flagEnableTextChange = false;
            richTextBox1.Text = await ReadFile(fileAddress);
            flagEnableTextChange = true;

            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            IsFileOpened(true, fileName);
        }



        #endregion

        #region File Tab
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!IsTextEmpty())
            {
                var r = MessageBoxOpenFile();
                if (r == DialogResult.No) return;
            }

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                OpenFile(openFileDialog1.FileName, openFileDialog1.SafeFileName);
            }
        }

        private void closeOpenedFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // When you came here, it means file is opened
            if (IsFileEdited())
            {
                var r = MessageBoxDiscardChanges();
                if (r == DialogResult.No) return;
            }
            IsFileOpened(false);
        }

        private async void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsFileOpened() && IsFileEdited())
            {
                await WriteFile(openFileDialog1.FileName);
                IsFileEdited(false);
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
                await WriteFile(saveFileDialog1.FileName);
                openFileDialog1.FileName = saveFileDialog1.FileName;
                IsFileOpened(true, Path.GetFileName(saveFileDialog1.FileName));
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region Edit Tab
        private void undoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Cut();
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Copy();
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Paste();
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void selectAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SelectAll();
        }

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

        #region Tools
        private void characterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CharacterDecoderForm form = new CharacterDecoderForm();
            form.Show();
        }

        private void textToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextDecoderForm form = new TextDecoderForm();
            form.Show();
        }
        #endregion

        #region richTextBox

        int richTextBoxSelection;

        private async void richTextBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] filesAddresses = (string[])e.Data.GetData(DataFormats.FileDrop);
            string fileAddress = filesAddresses[0];

            var r = MessageBoxDragDrop();
            if (r == DialogResult.Yes)
            {
                if (!IsTextEmpty())
                {
                    r = MessageBoxOpenFile();
                    if (r == DialogResult.No) return;
                }

                OpenFile(fileAddress, Path.GetFileName(fileAddress));
                return;
            }
            string text = await ReadFile(fileAddress);
            richTextBox1.Text += text;
            richTextBox1.SelectionStart = richTextBoxSelection;
        }

        private void richTextBox1_DragEnter(object sender, DragEventArgs e)
        {
            richTextBoxSelection = richTextBox1.SelectionStart;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (flagEnableTextChange && !IsFileEdited())
            {
                IsFileEdited(true);
            }
        }

        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            int currentTextCursorIndex = richTextBox1.SelectionStart;
            int lineCount = richTextBox1.GetLineFromCharIndex(currentTextCursorIndex);
            int charCount = currentTextCursorIndex - richTextBox1.GetFirstCharIndexFromLine(lineCount);
            textLineChar.Text = $"Ln {lineCount + 1}, Char {charCount + 1}";
            textLen.Text = $"Length {richTextBox1.Text.Length}";

            if (richTextBox1.SelectionLength > 0)
            {
                EnableContextualEditing(true);

                if (richTextBox1.SelectionLength == 1)
                {
                    EnableSingleCharEncoding(true);
                    EnableMultipleCharEncoding(false);

                    char c = richTextBox1.SelectedText[0];

                    CharacterEncodingConverter converter = new CharacterEncodingConverter();
                    converter.Encode(c);
                    toolStripStatusLabelBinary.Text = $"Binary: {converter.BinaryCode}";
                    toolStripStatusLabelOctal.Text = $"Octal: {converter.OctalCode}";
                    toolStripStatusLabelDecimal.Text = $"Decimal: {converter.DecimalCode}";
                    toolStripStatusLabelHexadecimal.Text = $"Hexadecimal: {converter.HexadecimalCode}";
                }
                else if (richTextBox1.SelectionLength > 1 && richTextBox1.SelectionLength <= 19)
                {
                    EnableSingleCharEncoding(false);
                    EnableMultipleCharEncoding(true);

                    TextEncodingConverter encode = new TextEncodingConverter();
                    encode.Encode(richTextBox1.SelectedText);
                    toolStripStatusLabelUTF8.Text = $"UTF-8: {encode.UTF8}";
                    toolStripStatusLabelUTF16.Text = $"UTF-16: {encode.UTF16}";
                    toolStripStatusLabelUTF32.Text = $"UTF-32: {encode.UTF32}";
                }
            }
            else
            {
                EnableContextualEditing(false);
                EnableSingleCharEncoding(false);
                EnableMultipleCharEncoding(false);
            }
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control)
            {
                #region File
                if (e.KeyCode == Keys.O) // Open
                {
                    openToolStripMenuItem.PerformClick();
                }
                else if (e.KeyCode == Keys.S) // Save
                {
                    saveToolStripMenuItem.PerformClick();
                }
                else if (e.KeyCode == Keys.E) // Exit
                {
                    exitToolStripMenuItem.PerformClick();
                }
                #endregion

                #region Edit
                else if (e.KeyCode == Keys.F) // Find
                {
                    // coming soon
                }
                else if (e.KeyCode == Keys.H) // Replace
                {
                    // coming soon
                }
                #endregion

                #region View
                else if (e.KeyCode == Keys.Add || e.KeyCode == Keys.Oemplus) // Zoom In
                {
                    zoomInToolStripMenuItem.PerformClick();
                }
                else if (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.OemMinus) // Zoom Out
                {
                    zoomOutToolStripMenuItem.PerformClick();
                }
                else if (e.KeyCode == Keys.D0 || e.KeyCode == Keys.NumPad0) // Restore Default Zoom
                {
                    restoreDeafaultZoomToolStripMenuItem.PerformClick();
                }
                #endregion

            }
            else if (e.Modifiers == Keys.None)
            {
                if (e.KeyCode == Keys.F5) // Edit > Date
                {
                    dateToolStripMenuItem.PerformClick();
                }
            }
            else if (e.Modifiers == (Keys.Shift | Keys.Control))
            {
                if (e.KeyCode == Keys.S) // File > Save as
                {
                    saveAsToolStripMenuItem.PerformClick();
                }
                else if (e.KeyCode == Keys.O) // File > Close Opened File
                {
                    closeOpenedFileToolStripMenuItem.PerformClick();
                }
            }
        }
        #endregion

        #region Right Click
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Paste();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectAll();
        }

        private void rightToLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int rightToLeftState = (int)richTextBox1.RightToLeft;
            rightToLeftState = -1 * ~(-1 * rightToLeftState);

            richTextBox1.RightToLeft = (RightToLeft)rightToLeftState;
            rightToLeftToolStripMenuItem.Checked = Convert.ToBoolean(rightToLeftState);
            contextMenuStrip1.RightToLeft = RightToLeft.No;
        }

        private void normalizeDigitsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string input = richTextBox1.SelectedText;
            if (richTextBox1.SelectionLength == 0 ||
                !input.All(character => character != '\0' && char.IsDigit(character))) return;

            StringBuilder result = new StringBuilder(0, input.Length);
            foreach (char c in input)
            {
                if (c >= '۰' && c <= '۹')      // Persian digits 1776-1785
                {
                    result.Append((char)('0' + (c - '۰')));
                }
                else if (c >= '٠' && c <= '٩') // Arabic-Indic digits 1632-1641
                {
                    result.Append((char)('0' + (c - '٠')));
                }
                else                           // ASCII digits 48-57
                {
                    result.Append(c);
                }
            }
            richTextBox1.SelectedText = result.ToString();
        }
        #endregion

        #region Right Click Status bar
        private ToolStripStatusLabel clickedStatusLabel;
        private void toolStripStatusLabelUTF32_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                clickedStatusLabel = sender as ToolStripStatusLabel;
                contextMenuStrip2.Show(Cursor.Position);
            }
        }

        private void copyToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            string prefix = ": ";
            string text = clickedStatusLabel.Text;
            string filteredText = text.Substring(text.IndexOf(prefix) + prefix.Length);

            try
            {
                Clipboard.SetText(filteredText);
            }
            catch (Exception)
            {
                Clipboard.SetText("Texy failed to copy!");
            }
        }
        #endregion
    }
}