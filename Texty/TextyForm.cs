using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Texty;
using Texty.Clipboard_Watcher;
using Texty.Directory_Manager;
using Texty.Encoding_Converter;
using Texty.Utilities;
using RegistrySettings;
using Texty.File;
using Texty.Date_Converter;

namespace Texty
{
    public partial class TextyForm : Form
    {
        bool flagEnableTextChange = true;

        public TextyForm()
        {
            InitializeComponent();

            richTextBox1.AllowDrop = true;

            EnableContextualEditing(false);

            EnableSingleCharEncoding(false);
            EnableMultipleCharEncoding(false);
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
            normalizeToolStripMenuItem.Enabled = state;
            convertCaseToToolStripMenuItem.Enabled = state;
        }

        #region Form Events
        private void TextyForm_Load(object sender, EventArgs e)
        {
            RegistryForm formSettings = new RegistryForm(Application.ProductName);

            if (!formSettings.IsExisted)
            {
                formSettings.FormSize = new Size(716, 525);
                formSettings.FormLocation = new Point(442, 163);
                formSettings.FormWindowState = FormWindowState.Normal;
                formSettings.Write();
            }

            formSettings.Read();
            Size = formSettings.FormSize;
            Location = formSettings.FormLocation;
            WindowState = formSettings.FormWindowState;


            RegistryFont registryFont = new RegistryFont(Application.ProductName);
            if (!registryFont.IsExisted)
            {
                registryFont.Font = new Font("Comic Sans MS", 12, FontStyle.Regular);
                registryFont.Write();
            }

            registryFont.Read();
            richTextBox1.Font = fontDialog1.Font = registryFont.Font;
        }

        private void TextyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((IsFileLoaded() && IsFileEdited()) || (!IsFileLoaded() && !IsAllTextWhiteSpace()))
            {
                var r = MessageBoxSaveChoice();
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
            if (WindowState == FormWindowState.Normal)
            {
                RegistryForm formSettings = new RegistryForm(Application.ProductName);
                formSettings.FormSize = Size;
                formSettings.FormLocation = Location;
                formSettings.FormWindowState = WindowState;
                formSettings.Write();
            }

            RegistryFont registryFont = new RegistryFont(Application.ProductName);
            registryFont.Font = Font;
            registryFont.Write();
        }
        #endregion

        #region Message Boxes
        public DialogResult MessageBoxMultipleSelectionFilesError()
        {
            var r = MessageBox.Show("Can't drag and drop multiple files!",
                         "Multiple files detected...",
                         MessageBoxButtons.OK,
                         MessageBoxIcon.Exclamation,
                         MessageBoxDefaultButton.Button1);
            return r;
        }

        public DialogResult MessageBoxDragDropChoice()
        {
            var r = MessageBox.Show("Would you like to open the file?",
                         "Incoming file detected...",
                         MessageBoxButtons.YesNo,
                         MessageBoxIcon.Question,
                         MessageBoxDefaultButton.Button2);
            return r;
        }

        public DialogResult MessageBoxOverWriteFileChoice()
        {
            var r = MessageBox.Show("Overwrite text file over your text?",
                                  "Text Mix-Up! 😵‍💫",
                                  MessageBoxButtons.YesNo,
                                  MessageBoxIcon.Exclamation,
                                  MessageBoxDefaultButton.Button2);

            return r;
        }

        public DialogResult MessageBoxDiscardChangesChoice()
        {
            var r = MessageBox.Show("Do you want to discard the changes?",
                                      "We're about to close!",
                                      MessageBoxButtons.YesNo,
                                      MessageBoxIcon.Question,
                                      MessageBoxDefaultButton.Button2);
            return r;
        }

        public DialogResult MessageBoxSaveChoice()
        {
            var r = MessageBox.Show("Do you want to save your changes?",
                                        "Magic is over!, Vanish✨, Texty!",
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
        public bool IsAllTextWhiteSpace()
        {
            return richTextBox1.Text.All(character => char.IsWhiteSpace(character));
        }

        public bool IsTextEmpty()
        {
            return richTextBox1.Text.Length == 0;
        }

        public bool IsFileEdited()
        {
            return Text.Contains('*');
        }

        public void FileIsEditedState(bool status)
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

        public bool IsFileLoaded()
        {
            return !string.IsNullOrEmpty(Text.Replace("*", ""));
        }

        public void FileIsLoadedState(bool status, string fileName)
        {
            Text = fileName;
            closeOpenedFileToolStripMenuItem.Visible = status;
            toolStripSeparator1.Visible = status;
        }
        #endregion

        #region File I/O
        private async void AppendTextFile(string fileAddress)
        {
            richTextBox1.Text += await FileManager.Read(fileAddress);
            richTextBox1.SelectionStart = richTextBoxSelection;
        }

        private async void LoadFile(string fileAddress, string fileName)
        {
            flagEnableTextChange = false;
            richTextBox1.Text = await FileManager.Read(fileAddress);
            flagEnableTextChange = true;

            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            FileIsLoadedState(true, fileName);
        }

        private void OpenFile()
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                LoadFile(openFileDialog1.FileName, openFileDialog1.SafeFileName);
            }
        }

        private async void SaveFile()
        {
            await FileManager.Write(openFileDialog1.FileName, richTextBox1.Text);
            FileIsEditedState(false);
        }

        private async void SaveAsFile()
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                await FileManager.Write(saveFileDialog1.FileName, richTextBox1.Text);
                openFileDialog1.FileName = saveFileDialog1.FileName;
                FileIsLoadedState(true, Path.GetFileName(saveFileDialog1.FileName));
            }
        }

        private void CloseFile()
        {
            FileIsLoadedState(false, "");
            flagEnableTextChange = false;
            richTextBox1.Clear();
            flagEnableTextChange = true;
        }
        #endregion

        #region File Tab
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!IsAllTextWhiteSpace())
            {
                if (MessageBoxOverWriteFileChoice() == DialogResult.No)
                {
                    return;
                }
            }

            OpenFile();
        }

        private void closeOpenedFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // When you came here, it means file is opened
            if (IsFileEdited())
            {
                if (MessageBoxDiscardChangesChoice() == DialogResult.No)
                {
                    return;
                }
            }

            CloseFile();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsFileLoaded() && IsFileEdited())
            {
                SaveFile();
            }
            else if (!IsFileLoaded() && IsFileEdited())
            {
                SaveAsFile();
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAsFile();
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

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void timeDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DateTime systemDateTime = DateTime.Now;
            PersianDate persianDate = DateConverter.GetPersianDate(systemDateTime);
            
            string gregorianDate = $"{systemDateTime.Year:0000}/{systemDateTime.Month:00}/{systemDateTime.Day:00}";
            string solarHijriDate = $"{persianDate.Year:0000}/{persianDate.Month:00}/{persianDate.Day:00}";

            string value = $"\r\n{solarHijriDate}\r\n{gregorianDate}";

            richTextBox1.Text = richTextBox1.Text.Insert(richTextBox1.SelectionStart, value);
            richTextBox1.SelectionStart = richTextBox1.SelectionStart + value.Replace("\r\n", "\n").Length;
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

        float zoom = 0.1f;

        public void SetTextZoomFactorStatus()
        {
            textZoomFactor.Text = $"{richTextBox1.ZoomFactor * 100}%";
        }

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

        #region Tools Tab
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

        private void directoryManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DirectoryManagerForm form = new DirectoryManagerForm();
            form.Show();
        }

        private void clipboardWatcherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClipboardWatcherForm form = new ClipboardWatcherForm();
            form.Show();
        }

        private void gregorianToSolarHijriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GregorianToSolarHijriForm form = new GregorianToSolarHijriForm();
            form.Show();
        }

        private void solarHijriToGregorianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SolarHijriToGregorianForm form = new SolarHijriToGregorianForm();
            form.Show();
        }
        #endregion

        #region Help Tab
        private void textySourceCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string link = @"https://github.com/ALiMoradzade/Texty";
            Process.Start(link);
        }
        #endregion

        #region Status Srip Methods
        private int CurrentCursorLine()
        {
            return richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart);
        }

        private int CurrentCursorCharCount()
        {
            return richTextBox1.SelectionStart - richTextBox1.GetFirstCharIndexFromLine(CurrentCursorLine());
        }

        private string CurrentCursorLength()
        {
            if (richTextBox1.SelectionLength > 0)
            {
               return $"Length {richTextBox1.SelectionLength}";
            }
            else
            {
                return $"Length {richTextBox1.Text.Length}";
            }
        }

        private void SetTextInfo()
        {
            textCurrentLineAndChar.Text = $"Ln {CurrentCursorLine() + 1}, Char {CurrentCursorCharCount() + 1}";
            textLengthOrCursorLength.Text = CurrentCursorLength();
        }

        private void SetCharEncoding()
        {
            CharacterEncodingConverter converter = new CharacterEncodingConverter();
            converter.Encode(richTextBox1.SelectedText[0]);
            toolStripStatusLabelBinary.Text = $"Binary: {converter.BinaryCode}";
            toolStripStatusLabelOctal.Text = $"Octal: {converter.OctalCode}";
            toolStripStatusLabelDecimal.Text = $"Decimal: {converter.DecimalCode}";
            toolStripStatusLabelHexadecimal.Text = $"Hexadecimal: {converter.HexadecimalCode}";
        }

        private void Set_UTF_8_16_32()
        {
            TextEncodingConverter encode = new TextEncodingConverter();
            encode.Encode(richTextBox1.SelectedText);
            toolStripStatusLabelUTF8.Text = $"UTF-8: {encode.UTF8}";
            toolStripStatusLabelUTF16.Text = $"UTF-16: {encode.UTF16}";
            toolStripStatusLabelUTF32.Text = $"UTF-32: {encode.UTF32}";
        }
        #endregion

        #region richTextBox

        #region Drag and Drop File
        int richTextBoxSelection;

        private void richTextBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] filesAddresses = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (filesAddresses.Length > 1)
            {
                MessageBoxMultipleSelectionFilesError();
                return;
            }

            string fileAddress = filesAddresses[0];
            if (MessageBoxDragDropChoice() == DialogResult.No)
            {
               return;
            }

            if (!IsAllTextWhiteSpace())
            {
                if (MessageBoxOverWriteFileChoice() == DialogResult.No)
                {
                    AppendTextFile(fileAddress);
                    return;
                }
            }

            LoadFile(fileAddress, Path.GetFileName(fileAddress));
        }

        private void richTextBox1_DragEnter(object sender, DragEventArgs e)
        {
            richTextBoxSelection = richTextBox1.SelectionStart;
        }
        #endregion

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (flagEnableTextChange)
            {
                if (IsTextEmpty())
                {
                    FileIsEditedState(false);
                }
                else if (!IsFileEdited())
                {
                    FileIsEditedState(true);
                }
            }
        }

        private void richTextBox1_SelectionChanged(object sender, EventArgs e)
        {
            SetTextInfo();

            if (richTextBox1.SelectionLength > 1) EnableContextualEditing(true);
            else EnableContextualEditing(false);

            // UTF-8, UTF-16, UTF-32 status bar
            if (richTextBox1.SelectionLength > 31 || richTextBox1.SelectionLength == 0)
            {
                EnableSingleCharEncoding(false);
                EnableMultipleCharEncoding(false);
            }
            else
            {
                if (richTextBox1.SelectionLength == 1)
                {
                    EnableSingleCharEncoding(true);
                    EnableMultipleCharEncoding(false);

                    SetCharEncoding();
                }
                else if (richTextBox1.SelectionLength > 1)
                {
                    EnableSingleCharEncoding(false);
                    EnableMultipleCharEncoding(true);

                    Set_UTF_8_16_32();
                }
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

        private void convertDigitsToASCIIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string digits = richTextBox1.SelectedText;
            richTextBox1.SelectedText = StringNormalizer.ConvertToAsciiDigits(digits);
        }

        private void removeNonPersianLettersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string text = richTextBox1.SelectedText;
            richTextBox1.SelectedText = StringNormalizer.RemoveNonPersianLetters(text);
        }

        #region Convert Case To
        private void lazecaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldText = richTextBox1.SelectedText;
            richTextBox1.SelectedText = StringCaseConvertor.ToLazyCase(oldText);
        }

        private void kebabcaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldText = richTextBox1.SelectedText;
            if (!StringCaseConvertor.IsSplitable(oldText))
            {
                StringCaseConvertor.MessageBoxWrongFormat("kebab");
                return;
            }

            richTextBox1.SelectedText = StringCaseConvertor.ToKebabCase(oldText);
        }

        private void snakecaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldText = richTextBox1.SelectedText;
            if (!StringCaseConvertor.IsSplitable(oldText))
            {
                StringCaseConvertor.MessageBoxWrongFormat("snake");
                return;
            }

            richTextBox1.SelectedText = StringCaseConvertor.ToSnakeCase(oldText);
        }

        private void dotcaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldText = richTextBox1.SelectedText;
            if (!StringCaseConvertor.IsSplitable(oldText))
            {
                StringCaseConvertor.MessageBoxWrongFormat("dot");
                return;
            }

            richTextBox1.SelectedText = StringCaseConvertor.ToDotCase(oldText);
        }

        private void spaceCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldText = richTextBox1.SelectedText;
            if (!StringCaseConvertor.IsSplitable(oldText))
            {
                StringCaseConvertor.MessageBoxWrongFormat("space");
                return;
            }

            richTextBox1.SelectedText = StringCaseConvertor.ToSpaceCase(oldText);
        }

        private void camelCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldText = richTextBox1.SelectedText;
            if (!StringCaseConvertor.IsSplitable(oldText))
            {
                StringCaseConvertor.MessageBoxWrongFormat("camel");
                return;
            }

            richTextBox1.SelectedText = StringCaseConvertor.ToCamelCase(oldText);
        }

        private void camelKebabCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldText = richTextBox1.SelectedText;
            if (!StringCaseConvertor.IsSplitable(oldText))
            {
                StringCaseConvertor.MessageBoxWrongFormat("camel kebab");
                return;
            }

            richTextBox1.SelectedText = StringCaseConvertor.ToCamelKebabCase(oldText);
        }

        private void camelSnakeCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldText = richTextBox1.SelectedText;
            if (!StringCaseConvertor.IsSplitable(oldText))
            {
                StringCaseConvertor.MessageBoxWrongFormat("camel snake");
                return;
            }

            richTextBox1.SelectedText = StringCaseConvertor.ToCamelSnakeCase(oldText);
        }

        private void camelDotCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldText = richTextBox1.SelectedText;
            if (!StringCaseConvertor.IsSplitable(oldText))
            {
                StringCaseConvertor.MessageBoxWrongFormat("camel dot");
                return;
            }

            richTextBox1.SelectedText = StringCaseConvertor.ToCamelDotCase(oldText);
        }

        private void sentenceCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldText = richTextBox1.SelectedText;
            if (!StringCaseConvertor.IsSplitable(oldText))
            {
                StringCaseConvertor.MessageBoxWrongFormat("sentence");
                return;
            }

            richTextBox1.SelectedText = StringCaseConvertor.ToSentenceCase(oldText);
        }

        private void pascalCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldText = richTextBox1.SelectedText;
            if (!StringCaseConvertor.IsSplitable(oldText))
            {
                StringCaseConvertor.MessageBoxWrongFormat("pascal");
                return;
            }

            richTextBox1.SelectedText = StringCaseConvertor.ToPascalCase(oldText);
        }

        private void trainCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldText = richTextBox1.SelectedText;
            if (!StringCaseConvertor.IsSplitable(oldText))
            {
                StringCaseConvertor.MessageBoxWrongFormat("train");
                return;
            }

            richTextBox1.SelectedText = StringCaseConvertor.ToTrainCase(oldText);
        }

        private void pascalSnakeCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldText = richTextBox1.SelectedText;
            if (!StringCaseConvertor.IsSplitable(oldText))
            {
                StringCaseConvertor.MessageBoxWrongFormat("pascal snake");
                return;
            }

            richTextBox1.SelectedText = StringCaseConvertor.ToPascalSnakeCase(oldText);
        }

        private void pascalDotCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldText = richTextBox1.SelectedText;
            if (!StringCaseConvertor.IsSplitable(oldText))
            {
                StringCaseConvertor.MessageBoxWrongFormat("pascal dot");
                return;
            }

            richTextBox1.SelectedText = StringCaseConvertor.ToPascalDotCase(oldText);
        }

        private void titleCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldText = richTextBox1.SelectedText;
            if (!StringCaseConvertor.IsSplitable(oldText))
            {
                StringCaseConvertor.MessageBoxWrongFormat("title");
                return;
            }

            richTextBox1.SelectedText = StringCaseConvertor.ToTitleCase(oldText);
        }

        private void sCREAMINGCASEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldText = richTextBox1.SelectedText;
            richTextBox1.SelectedText = StringCaseConvertor.ToScreamingCase(oldText);
        }

        private void cOBOLCASEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldText = richTextBox1.SelectedText;
            if (!StringCaseConvertor.IsSplitable(oldText))
            {
                StringCaseConvertor.MessageBoxWrongFormat("cobol");
                return;
            }

            richTextBox1.SelectedText = StringCaseConvertor.ToCobolCase(oldText);
        }

        private void sCREAMINGSNAKECASEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldText = richTextBox1.SelectedText;
            if (!StringCaseConvertor.IsSplitable(oldText))
            {
                StringCaseConvertor.MessageBoxWrongFormat("screming snake");
                return;
            }

            richTextBox1.SelectedText = StringCaseConvertor.ToScreamingSnakeCase(oldText);
        }

        private void sCREAMINGDOTCASEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldText = richTextBox1.SelectedText;
            if (!StringCaseConvertor.IsSplitable(oldText))
            {
                StringCaseConvertor.MessageBoxWrongFormat("screaming dot");
                return;
            }

            richTextBox1.SelectedText = StringCaseConvertor.ToScreamingDotCase(oldText);
        }

        private void uPPERSPACECASEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldText = richTextBox1.SelectedText;
            if (!StringCaseConvertor.IsSplitable(oldText))
            {
                StringCaseConvertor.MessageBoxWrongFormat("upper space");
                return;
            }

            richTextBox1.SelectedText = StringCaseConvertor.ToUpperSpaceCase(oldText);
        }

        private void iNVERTCASEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string oldText = richTextBox1.SelectedText;
            richTextBox1.SelectedText = StringCaseConvertor.ToInvertCase(oldText);
        }
        #endregion

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
                MessageBox.Show("Something went wrong",
                                "Texty failed to copy the selected text to the clipboard",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }






        #endregion

       
    }
}